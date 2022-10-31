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

public partial class staffs_courses_print_studentGrade : System.Web.UI.Page
{
    string ccode="";

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_printDate.Text = "Printed on: " + DateTime.Today.ToLongDateString();

        try
        {
            if (String.IsNullOrEmpty(Request.QueryString["ccode"].ToString()) || String.IsNullOrEmpty(Session["user"].ToString()))
            {
                Response.Redirect("../_login.aspx");
            }
            else 
            {
                ccode = Request.QueryString["ccode"].ToString();
            }
        }
        catch (Exception ert) { Response.Redirect("../_login.aspx"); }

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
        ds.Merge(new staff_webService().get_allStudent_ofA_CoursesTeacher(ccode));
        ds.Merge(new staff_webService().get_gradingPolicy("2"));

        GridView_students.DataSource = ds;
        GridView_students.DataMember = "studentList";
        GridView_students.DataBind(); 
        
    }



    private void load_courses()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_allCourses_ofA_semester(Session["sem"].ToString(), Session["year"].ToString()));
       

        foreach (DataRow dr in ds.Tables["coursList"].Rows)
        {
            dr["CNAME"] = dr["CNAME"].ToString() + "(" + dr["SECTION"].ToString() + ")";
        } 

        foreach (DataRow dr in ds.Tables["coursList"].Rows)
        {
            if (ccode == dr["COURSE_TEACHER_ID"].ToString())
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

}
