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

public partial class staffs_courses_singe_course : System.Web.UI.Page
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


    private void loadinformations()
    {
        load_courses();
        load_assignments();
        load_lectures();
        load_outline();
        load_links();
    }
    private void load_links()
    { 
        hp_link_student.NavigateUrl = "_students.aspx?code=" + code;
        hp_link_addnewAssignment.NavigateUrl = "_new_assignment.aspx?code=" + code;
        hp_link_class_lecture.NavigateUrl = "_upload_lectures.aspx?code=" + code;
        hp_link_course_outline.NavigateUrl = "_upload_courseOutline.aspx?code=" + code;
      //  hp_link_courseOutline.NavigateUrl = "_lecture_write.aspx?code=" + code;
        hp_link_attendance.NavigateUrl = "_attendance.aspx?code=" + code;
        hp_link_final_gradding.NavigateUrl = "_finalGrading.aspx?code=" + code;
        hp_link_final_evaluation.NavigateUrl = "teacherevaluation_result.aspx?code=" + code;
        hp_link_attendanceSheet.NavigateUrl = "_attendanceSheet.aspx?code=" + code;
        hp_link_attendanceReport.NavigateUrl = "_attendanceReport.aspx?code=" + code;  

        hp_link_final_evaluation.Attributes.Add("onclick", " return open_class_evaluation(\"" + code + "\")");
         
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
            hp_link_courseOutline.NavigateUrl = "_lecture_write.aspx?code=" + ds.Tables["outline"].Rows[0]["COURSE_MATERIALS_ID"].ToString();
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
        ds.Merge(new staff_webService().get_allCourses_ofA_semesterNew(Session["sem"].ToString(), Session["year"].ToString()));

        foreach (DataRow dr in ds.Tables["coursList"].Rows)
        {
            dr["CNAME"] = dr["CNAME"].ToString() + "(" + dr["SECTION"].ToString() + ")";
        }
        
        
        cmb_course.DataSource = ds.Tables["coursList"];
        cmb_course.DataTextField= "CNAME";
        cmb_course.DataValueField = "COURSE_TEACHER_ID";
        cmb_course.DataBind();

        if (code != "")
            cmb_course.SelectedValue = code;
        else code = cmb_course.SelectedValue.ToString();

        foreach (DataRow dr in ds.Tables["coursList"].Rows)
        {
            if (code == dr["COURSE_TEACHER_ID"].ToString() )
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



    protected void btn_submit_Click(object sender, EventArgs e)
    {

    }
}
