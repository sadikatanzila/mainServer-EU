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

public partial class staffs_courses_upload_courseOutline : System.Web.UI.Page
{
    string code = "";

    staff_webService obj_staff_webS = new staff_webService();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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

            btn_fupload_submit.Attributes.Add("onclick", "return save_check();");

            lbl_message.Text = "";

            if (!IsPostBack)
            {
                if (Request.QueryString["code"] != null)
                {
                    code = Request.QueryString["code"].ToString();
                }
            }
            else
            {
                code = cmb_course.SelectedValue.ToString();
            }
            loadinformations();
        }
        catch (Exception exp)
        {
            Response.Write("1. - "+exp.Message);
        }
    }

    private void loadinformations()
    {
        load_courses();
        load_outline();
    }


    private void load_outline()
    {
        hp_link_outline.Visible = false;

        DataSet ds = new DataSet();
        ds.Merge(obj_staff_webS.get_course_outline(cmb_course.SelectedValue.ToString()));//assignmentList 

        foreach (DataRow dr in ds.Tables["outline"].Rows)
        {
            hp_link_outline.Visible = true;
            hp_link_outline.Text = dr["TITLE"].ToString();
            hp_link_outline.NavigateUrl = "_lecture_write.aspx?code=" + dr["COURSE_MATERIALS_ID"].ToString();
        }

    }



    private void load_courses()
    {
        DataSet ds = new DataSet();
        ds.Merge(obj_staff_webS.get_allCourses_ofA_semester(Session["sem"].ToString(), Session["year"].ToString()));

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
        try
        {
            saveCourseOutline();
            load_outline();
        }
        catch (Exception exp)
        {
            Response.Write("2. - " + exp.Message);
        }
    }

    private void saveCourseOutline()
    {
        if ((fu_outline.PostedFile != null) && (fu_outline.PostedFile.ContentLength > 0))
        {
			//Response.Write("2. - Error");
            int fileLen;
            fileLen = fu_outline.PostedFile.ContentLength;
            byte[] input = new byte[fileLen];
            input = fu_outline.FileBytes;

            string fn = System.IO.Path.GetFileName(fu_outline.PostedFile.FileName);
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
            r["TYPE"] = 3;//Outline 
            r["UPLOAD_DATE"] = new cls_tools().get_database_formateDate(DateTime.Today);
            r["DESCRIPTION"] = txt_comments.Value.ToString();
            r["CTRL"] = 1;
            ds.Tables["assignment"].Rows.Add(r);

            String ids = "";
            lbl_message.Visible = true;


            ds.Merge(obj_staff_webS.get_course_outline(cmb_course.SelectedValue.ToString()));
            if (ds.Tables["outline"].Rows.Count > 0)
            {
                ids = ds.Tables["outline"].Rows[0]["COURSE_MATERIALS_ID"].ToString();
                if (!String.IsNullOrEmpty(ids))
                {
                   
                    string exFileExtension = ds.Tables["outline"].Rows[0]["FILE_NAME"].ToString().Split('.')[ds.Tables["outline"].Rows[0]["FILE_NAME"].ToString().Split('.').Length - 1];
                    System.IO.File.Delete(Server.MapPath("c_materials") + "/" + ids + "." + exFileExtension/*[exFileExtension.Length - 1]*/);
                    obj_staff_webS.execute_query("update WEB_COURSE_MATERIALS_TEACHER SET FILE_NAME='" + fn.Split('\\')[fn.Split('\\').Length - 1] + "', TITLE='" + txt_title.Text + "', DESCRIPTION='" + txt_comments.Value.ToString() + "' where COURSE_MATERIALS_ID='" + ids + "'");
                    try
                    {
                        string SaveLocation = Server.MapPath("c_materials/") + ids + "." + fileExtension[fileExtension.Length - 1];
                        fu_outline.PostedFile.SaveAs(SaveLocation);

                        txt_comments.Value = "";
                        txt_title.Text = "";
                        lbl_message.Text = "" + new cls_message().getMessage(2);
                    }
                    catch (Exception er)
                    {
                        obj_staff_webS.delete_assignment_teacher(ids);
                        lbl_message.Text = "" + new cls_message().getMessage(3);
                    }
                }

            }
            else
            {
                if (obj_staff_webS.save_outLine(ds, ref ids) == "1")
                {
                    try
                    {
                        string SaveLocation = Server.MapPath("c_materials/") + ids + "." + fileExtension[fileExtension.Length - 1];
                        fu_outline.PostedFile.SaveAs(SaveLocation);

                        lbl_message.Text = "" + new cls_message().getMessage(2);
                   }
                    catch (Exception er)
                    {
                        obj_staff_webS.delete_assignment_teacher(ids);
                        lbl_message.Text = "" + new cls_message().getMessage(3);
                    }
                }
                else lbl_message.Text = "" + new cls_message().getMessage(3);

                txt_comments.Value = "";
                txt_title.Text = "";
            }
        }
		//else
		//{
		//	Response.Write("2. - not received");
		//}
    }
}
