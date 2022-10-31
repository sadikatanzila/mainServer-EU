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

public partial class staffs_courses_students : System.Web.UI.Page
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

        lbl_TotalTaknCls.Text = "";
        loadinformations();
    }


    private void loadinformations()
    {
        load_courses();
        load_students();
    }

    private void load_students()
    {
        DataSet ds = new DataSet();
        //ds.Merge(new staff_webService().get_allStudent_ofA_CoursesTeacher(cmb_course.SelectedValue.ToString()));
        //ds.Merge(new staff_webService().get_allStudent_attendance(cmb_course.SelectedValue.ToString()));
        ds.Merge(new staff_webService().get_allStudent_attendanceNEW(cmb_course.SelectedValue.ToString()));
        GridView_students.DataSource = ds;
        GridView_students.DataMember = "studentList";
        GridView_students.DataBind();
        //studentList

       

        foreach (DataRow dr2 in ds.Tables["studentList"].Rows)
        {

            lbl_TotalTaknCls.Text = dr2["TakenClass"].ToString();
            // lbl_TotalTaknCls.Text = dr2["taken_class"].ToString();

        }
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
        cmb_course.SelectedValue = code;


        DataSet ds1 = new DataSet();
        ds1.Merge(new staff_webService().get_allCourses_ofA_semester(Session["sem"].ToString(), Session["year"].ToString()));

        foreach (DataRow dr1 in ds1.Tables["coursList"].Rows)
        {
            if (code == dr1["COURSE_TEACHER_ID"].ToString())
            {
                lbl_course_code.Text = dr1["COURSECODE"].ToString();
                lbl_course_name.Text = dr1["CNAME"].ToString();
                lbl_credit_hours.Text = dr1["CHOURS"].ToString();
                lbl_semester.Text = "" + new cls_tools().get_word_semester(Session["sem"].ToString()) + " " + Session["year"].ToString(); 
                lbl_section.Text = dr1["SECTION"].ToString();
                lbl_total_student.Text = dr1["TOTAL_STUDENT"].ToString();
                lbl_Program.Text = dr1["DEPCODE"].ToString();
                
                break;
            }
        }


        
    }



    protected void btn_submit_Click(object sender, EventArgs e)
    {

    }
}
