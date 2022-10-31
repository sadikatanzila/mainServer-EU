using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class student_course_submitAssignment : System.Web.UI.Page
{
    string sid = "";
    string code = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";

        if (Session.Count == 0)
            Response.Redirect("../_login.aspx");
        else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
        {
            sid = Session["ctrlId"].ToString();
            Response.Redirect("../_login.aspx");
        }
        else
        {
            sid = Session["ctrlId"].ToString();
            if (!String.IsNullOrEmpty(Request.QueryString["code"].ToString()))
                code = Request.QueryString["code"].ToString();
        }
        
        hp_link_s_assignment.Text = "";

        load_assignment();
    }

    private void load_assignment()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_a_assignmentInfo(code));

        if (ds.Tables["courseMaterial"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["courseMaterial"].Rows[0];

            lbl_description.Text = dr["DESCRIPTION"].ToString();
            lbl_dueDate.Text =new cls_tools().get_user_formateDate(dr["DUE_DATE"].ToString());
            lbl_title.Text = dr["TITLE"].ToString();
            hp_link_aaaisnment.Text = dr["FILE_NAME"].ToString();
            hp_link_aaaisnment.NavigateUrl = "~/staffs/courses/_lecture_write.aspx?code=" + dr["COURSE_MATERIALS_ID"].ToString();
        }

        ds.Merge(new student_webService().get_a_student_assignmentInfo(code, sid));


        foreach (DataRow dr in ds.Tables["s_courseMaterial"].Rows)
        {
            btn_submit.Visible = false;
            fu_attachment.Visible = false;

            hp_link_s_assignment.Text = dr["FILE_NAME"].ToString();
            hp_link_s_assignment.NavigateUrl = "_assignmentWrite.aspx?code="+code;// +dr["COURSE_MATERIALS_ID"].ToString();
        }

         


   }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if ((fu_attachment.PostedFile != null) && (fu_attachment.PostedFile.ContentLength > 0))
        {
            string fn = System.IO.Path.GetFileName(fu_attachment.PostedFile.FileName);
            string[] fileExtension = fn.Split('.');
            string f_name = fn.Split('\\')[fn.Split('\\').Length - 1];
            
            DataSet ds = new DataSet();
            ds.Tables.Add("WEB_COURSE_MATERIALS_STUDENT");
            ds.Tables["WEB_COURSE_MATERIALS_STUDENT"].Columns.Add("COURSE_MAT_ID");
            ds.Tables["WEB_COURSE_MATERIALS_STUDENT"].Columns.Add("SID");
            ds.Tables["WEB_COURSE_MATERIALS_STUDENT"].Columns.Add("FILE_NAME");
            ds.Tables["WEB_COURSE_MATERIALS_STUDENT"].Columns.Add("SUBMIT_DATE");
            ds.Tables["WEB_COURSE_MATERIALS_STUDENT"].Columns.Add("CTRL");
            ds.Tables["WEB_COURSE_MATERIALS_STUDENT"].Columns.Add("ATT_FILENAME");

            DataRow dr = ds.Tables["WEB_COURSE_MATERIALS_STUDENT"].NewRow();

            dr["COURSE_MAT_ID"] = "" + code;
            dr["SID"] = "" + sid;
            dr["SUBMIT_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);
            dr["FILE_NAME"] = "" + f_name;
            dr["CTRL"] = "1";
            dr["ATT_FILENAME"] = "" + code + sid +"."+ fileExtension[fileExtension.Length-1];

            ds.Tables["WEB_COURSE_MATERIALS_STUDENT"].Rows.Add(dr);

            String ids = "";
            lbl_message.Visible = true;
            if (new student_webService().submit_assignment(ds, "WEB_COURSE_MATERIALS_STUDENT") == "1")
            {
                try
                {
                    string SaveLocation = Server.MapPath("c_materials_student/") + code + sid +"."+ fileExtension[fileExtension.Length - 1]; ;
                    fu_attachment.PostedFile.SaveAs(SaveLocation);
                    lbl_message.Text = "" + new cls_message().getMessage(2);
                    load_assignment();

                }
                catch (Exception er)
                {
                    new student_webService().delete_assignment_student(sid,code );
                    lbl_message.Text = "" + new cls_message().getMessage(3);
                }
            }
            else lbl_message.Text = "" + new cls_message().getMessage(3);
        }
    }    

}