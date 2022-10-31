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

public partial class staffs_courses_upload_handouts : System.Web.UI.Page
{
    string code = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else if (String.IsNullOrEmpty(Session["user"].ToString()))
            {
                Response.Redirect("../_login.aspx");
            }
        }
        catch (Exception ert) { Response.Redirect("../_login.aspx"); }
      

        if (String.IsNullOrEmpty(Session["sem"].ToString()) || String.IsNullOrEmpty(Session["year"].ToString()))
            Response.Redirect("_course_list.aspx");

        lbl_message.Text = "";
        lbl_assignment_msg.Text = "";
        lbl_message.Visible = false; 
        
        btn_fupload_submit.Attributes.Add("onclick", "return save_check();");

        if (!IsPostBack)
        {
            if (Request.QueryString["code"] != null)
            {
                code = Request.QueryString["code"].ToString();
            }
            loadinformations();
        }
        else
        {
            code = cmb_course.SelectedValue.ToString();
        }  
        
    } 

    private void loadinformations()
    {
        load_courses();
        load_lectures(); 
    }
     

    private void load_lectures()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_all_lectures_ofA_course(cmb_course.SelectedValue.ToString()));//assignmentList
                 
        ds.Tables["assignmentList"].Columns.Add("up_date");

        lbl_assignment_msg.Text = "" + new cls_message().getMessage(4);
        foreach (DataRow dr in ds.Tables["assignmentList"].Rows)
        {
            lbl_assignment_msg.Text = "";
            dr["up_date"] = new cls_tools().get_user_formateDate(dr["UPLOAD_DATE"].ToString()); 
        }

        GridView_assignment_list.DataSource = ds;
        GridView_assignment_list.DataMember = "assignmentList";
        GridView_assignment_list.DataBind();
    }



    private void load_courses()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_allCourses_ofA_semester(Session["sem"].ToString(), Session["year"].ToString()));

        foreach (DataRow dr in ds.Tables["coursList"].Rows)
        {
            dr["CNAME"] = dr["CNAME"].ToString() + "(" + dr["SECTION"].ToString() + ")";
        }

        cmb_course.DataSource = ds.Tables["coursList"];
        cmb_course.DataTextField = "CNAME";
        cmb_course.DataValueField = "COURSE_TEACHER_ID";
        cmb_course.DataBind();
        cmb_course.SelectedValue = code;

        foreach (DataRow dr in ds.Tables["coursList"].Rows)
        {
            if (code == dr["COURSE_TEACHER_ID"].ToString())
            {
                lbl_course_code.Text = dr["COURSECODE"].ToString();
                lbl_course_name.Text = dr["CNAME"].ToString();
                lbl_credit_hours.Text = dr["CHOURS"].ToString();
                lbl_semester.Text = "" + new cls_tools().get_word_semester(Session["sem"].ToString()) + " " + Session["year"].ToString();
                lbl_section.Text = dr["SECTION"].ToString();
                lbl_total_student.Text = dr["TOTAL_STUDENT"].ToString();
                break;
            }
        }
    }
    protected void btn_fupload_submit_Click(object sender, EventArgs e)
    {
        saveLecture();
        load_lectures();
    }

    private void saveLecture()
    {
        if ((fu_lectures.PostedFile != null) && (fu_lectures.PostedFile.ContentLength > 0))
        {
            int fileLen;
            fileLen = fu_lectures.PostedFile.ContentLength;
            byte[] input = new byte[fileLen];
            input = fu_lectures.FileBytes;

            string fn = System.IO.Path.GetFileName(fu_lectures.PostedFile.FileName);
            string[] fileExtension = fn.Split('.');
            string f_name = fn.Split('\\')[fn.Split('\\').Length - 1];

            DataSet ds = new DataSet();
            ds.Tables.Add("assignment");
            ds.Tables["assignment"].Columns.Add("COURSE_MATERIALS_ID");
            ds.Tables["assignment"].Columns.Add("COURSE_TEACHER_ID");
            ds.Tables["assignment"].Columns.Add("TITLE");
            ds.Tables["assignment"].Columns.Add("FILE_NAME");
            ds.Tables["assignment"].Columns.Add("TYPE");
            ds.Tables["assignment"].Columns.Add("UPLOAD_DATE");
            ds.Tables["assignment"].Columns.Add("DESCRIPTION");
            ds.Tables["assignment"].Columns.Add("CTRL");

            DataRow r = ds.Tables["assignment"].NewRow();
            r["COURSE_MATERIALS_ID"] = "test";
            r["COURSE_TEACHER_ID"] = cmb_course.SelectedValue.ToString();
            r["TITLE"] = txt_title.Text;
            r["FILE_NAME"] = fn.Split('\\')[fn.Split('\\').Length - 1];
            r["TYPE"] = 2;//Lecture 
            r["UPLOAD_DATE"] = new cls_tools().get_database_formateDate(DateTime.Today);         
            r["DESCRIPTION"] = txt_comments.Value.ToString();
            r["CTRL"] = 1;
            ds.Tables["assignment"].Rows.Add(r);

            String ids = "";
            lbl_message.Visible = true;
            if (new staff_webService().save_lectures(ds, ref ids) == "1")
            {
                try
                {
                    string SaveLocation = Server.MapPath("c_materials/") + ids + "." + fileExtension[fileExtension.Length - 1];
                    fu_lectures.PostedFile.SaveAs(SaveLocation);

                    txt_comments.Value = "";
                    txt_title.Text = "";
                    lbl_message.Text = "" + new cls_message().getMessage(2);
                }
                catch (Exception er)
                {
                    new staff_webService().delete_assignment_teacher(ids);
                    lbl_message.Text = "" + new cls_message().getMessage(3);
                }
            }
            else lbl_message.Text = "" + new cls_message().getMessage(3);

            txt_comments.Value = "";
            txt_title.Text = "";
        }

    }
    protected void GridView_assignment_list_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        int i = 0;
    }
    protected void gvAdStd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string status = "1";
        string serialNo = Convert.ToString(GridView_assignment_list.DataKeys[e.RowIndex].Value.ToString());

        if (!String.IsNullOrEmpty(serialNo))
            if (new staff_webService().delete_allocated_Course(serialNo) != "1")
                status = "1" + 1;


        load_lectures();
    }
}
