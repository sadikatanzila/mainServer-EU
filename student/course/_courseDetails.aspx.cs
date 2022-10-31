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

public partial class student_course_courseDetails : System.Web.UI.Page
{
    string sid = "", code="";

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_eval_message.Visible = false;
        hplink_eval_message.Visible = false;

        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
            {
                Response.Redirect("../_login.aspx");
            }
            else
            {
                sid = Session["ctrlId"].ToString();
                if (Request.QueryString["code"] != null)
                    code = Request.QueryString["code"].ToString();

                if (IsPostBack)
                    code = cmb_course.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(code)) // for showing message for Teacher evaluation process
                {
                    if (new student_webService().get_teacher_eval_dateRange(Session["sem"].ToString(), Session["year"].ToString()))
                    {
                        if (!new student_webService().is_evaluation_done(code, sid + Session["sem"].ToString() + Session["year"].ToString()))
                        {
                            lbl_eval_message.Visible = true;
                            hplink_eval_message.Visible = true;
                            Session["evl"] = "";
                            Session["evl"] = "" + code;
                            hplink_eval_message.NavigateUrl = "_teacherEvaluation.aspx";
                        }
                    }                
                }

            }
        }
        catch (Exception ert) { Response.Redirect("../_login.aspx"); }
         
        load_allInformation();
    }

    private void load_allInformation()
    {
        load_courses();
        load_assignments();
        load_outline();
        load_lectures();
    
    }

    private void load_outline()
    {
        hp_link_courseOutline.Text = "Not yet uploaded";
        hp_link_courseOutline.NavigateUrl = "#";

        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_course_outline(cmb_course.SelectedValue.ToString()));//assignmentList
        if (ds.Tables["outline"].Rows.Count > 0)
        {
            hp_link_courseOutline.Text = "" + ds.Tables["outline"].Rows[0]["TITLE"].ToString();
            hp_link_courseOutline.NavigateUrl = "~/staffs/courses/_lecture_write.aspx?code=" + ds.Tables["outline"].Rows[0]["COURSE_MATERIALS_ID"].ToString();
        }
    }

    private void load_lectures()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_all_lectures_ofA_course(cmb_course.SelectedValue.ToString()));//assignmentList

        ds.Tables["assignmentList"].Columns.Add("arrow");

        foreach (DataRow dr in ds.Tables["assignmentList"].Rows)
            dr["arrow"] = ">";

        GridView_lecture_list.DataSource = ds;
        GridView_lecture_list.DataMember = "assignmentList";
        GridView_lecture_list.DataBind();
    }



    private void load_assignments()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_all_assignment_ofA_course(cmb_course.SelectedValue.ToString()));//assignmentList

        ds.Tables["assignmentList"].Columns.Add("arrow");
        ds.Tables["assignmentList"].Columns.Add("due_dates");


        foreach (DataRow dr in ds.Tables["assignmentList"].Rows)
        {
            dr["arrow"] = ">";
            dr["due_dates"] = new cls_tools().get_user_formateDate(dr["DUE_DATE"].ToString());
        }

        GridView_assignment_list.DataSource = ds;
        GridView_assignment_list.DataMember = "assignmentList";
        GridView_assignment_list.DataBind();
    }

    private void load_courses()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_semester_GradeSheet(sid + Session["sem"].ToString() + Session["year"].ToString()));
        if (ds.Tables["gradeSheet"].Rows.Count == 0)
        {
            //lbl_message.Text = "" + new cls_message().getMessage(9);
        }
        else
        {

            for (int count = 0; count < ds.Tables["gradeSheet"].Rows.Count; count++)
            {
                DataRow drt = ds.Tables["gradeSheet"].Rows[0];
                for (int j = count + 1; j < ds.Tables["gradeSheet"].Rows.Count; j++)
                {
                    if (ds.Tables["gradeSheet"].Rows[count]["COURSECODE"].ToString() == ds.Tables["gradeSheet"].Rows[j]["COURSECODE"].ToString())
                    {
                        ds.Tables["gradeSheet"].Rows.RemoveAt(j);
                        j--;
                    }
                }
            }

            ds.Tables["gradeSheet"].Columns.Add("teacher");
            ds.Tables["gradeSheet"].Columns.Add("course_teacher_id");

            String values = "";
            foreach (DataRow dr in ds.Tables["gradeSheet"].Rows)
            {
                values = new staff_webService().get_a_teacher_ofA_course(dr["COURSEKEY"].ToString(), dr["GGROUP"].ToString());
                if (values != "")
                {
                    dr["teacher"] = values.Split(':')[0];
                    dr["COURSE_TEACHER_ID"] = values.Split(':')[1];
                }
            }
        }

        cmb_course.DataSource = ds.Tables["gradeSheet"];
        cmb_course.DataTextField = "CNAME";
        cmb_course.DataValueField = "COURSE_TEACHER_ID";
        cmb_course.DataBind();

        if (!String.IsNullOrEmpty(code))
            cmb_course.SelectedValue = code;
        
        foreach (DataRow dr in ds.Tables["gradeSheet"].Rows)
        {
            if (code == dr["COURSE_TEACHER_ID"].ToString() || String.IsNullOrEmpty(code))
            {
                lbl_course_code.Text = dr["COURSECODE"].ToString();
                lbl_course_name.Text = dr["cname"].ToString();
                lbl_credit_hours.Text = dr["CHOURS"].ToString();
                lbl_section.Text = dr["GGROUP"].ToString();
                lbl_semester.Text = new cls_tools().get_word_semester(Session["sem"].ToString())+" "+Session["year"].ToString();
                break;
            }
        }
    }
   
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        load_allInformation();
    }
}
