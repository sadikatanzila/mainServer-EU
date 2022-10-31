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

public partial class staffs_courses_finalGradingPrev : System.Web.UI.Page
{
    string code = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_msg.Text = "";
        btn_save.Visible = true;
        btn_approve.Visible = true;
        btn_print_grade.Visible = false;


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
                loadinformations();
            }
        }
        else
        {
            code = cmb_course.SelectedValue.ToString();
           
        }

    }



    private void loadinformations()
    {
        load_courses();
        load_students();
    }

    private void load_students()
    {
        int status = 0;
        // status=0 --- not approved (active Save+Approve)
        // status=1 -- Approved (Only Print);
        // status=2 -- handaled by Exam controller(Only Print);

        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_allStudent_ofA_CoursesTeacher(cmb_course.SelectedValue.ToString()));
        ds.Merge(new staff_webService().get_gradingPolicy("2"));

        foreach (DataRow dr in ds.Tables["studentList"].Rows) // check for status 1
        {
            if (dr["APPROVE_CTRL"].ToString() == "1")
            {
                status = 1;
                break;
            }        
        }

        if (status == 0)
        {
            foreach (DataRow dr in ds.Tables["studentList"].Rows) // check for status 2
            {
                if (!String.IsNullOrEmpty(dr["CREATED_BY"].ToString()))
                if (dr["CREATED_BY"].ToString() != dr["TEACHER_ID"].ToString())
                {
                    //status = 2;
                    //break;
                }
            }
        }

        if (status != 0)
        { 
            btn_save.Visible = false;
            btn_approve.Visible = false;
            btn_print_grade.Visible = true;
            btn_print_grade.Attributes.Add("onClick"," return printGradesheet('"+code+"')");

            if (status == 1)
                lbl_msg.Text = ""+new cls_message().getMessage(23);
            else if (status == 2)
                lbl_msg.Text = "" + new cls_message().getMessage(24);
        }

        GridView_students.DataSource = ds;
        GridView_students.DataMember = "studentList";
        GridView_students.DataBind();

        foreach (GridViewRow gr in GridView_students.Rows)
        {

            ((DropDownList)(gr.FindControl("cmb_grades"))).DataTextField = "GRADETYPE";
            ((DropDownList)(gr.FindControl("cmb_grades"))).DataValueField = "GRADETYPE";
            ((DropDownList)(gr.FindControl("cmb_grades"))).DataSource = ds.Tables["GRADINGPOLICY2"];
            ((DropDownList)(gr.FindControl("cmb_grades"))).DataBind();
            ((DropDownList)(gr.FindControl("cmb_grades"))).Text = ((Label)(gr.FindControl("Label1"))).Text;
            if (status != 0)
                ((DropDownList)(gr.FindControl("cmb_grades"))).Enabled = false; 
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

   
    protected void btn_submit_Click1(object sender, EventArgs e)
    {
        loadinformations();
        
    }
     
    protected void btn_save_Click(object sender, EventArgs e)
    {
        save_approve_teacherGrading(false);
        loadinformations();
    }


    private void save_approve_teacherGrading(Boolean status)
    {
        staff_webService obj_staff = new staff_webService();
        DataSet ds = new DataSet();
        //ds.Merge(new staff_webService().get_tableData("Select * from OFFERERINGANDGRADE ","OFFERERINGANDGRADE"));
        //ds.Tables["OFFERERINGANDGRADE"].Rows.Clear();
        ds.Tables.Add("OFFERERINGANDGRADE");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("COURSEKEY");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("REGKEY");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GGRADE2");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("CREATED_BY");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("CREATED_DATE");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GGRADE");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GPOINT");
        
        ds.Merge(new cls_tools().get_gradingPolicyTable());

        foreach (GridViewRow gr in GridView_students.Rows)
        {
            DataRow dr = ds.Tables["OFFERERINGANDGRADE"].NewRow();
            dr["COURSEKEY"] = ((Label)(gr.FindControl("lbl_COURSEKEY"))).Text;

            dr["REGKEY"] = ((Label)(gr.FindControl("lbl_REGKEY"))).Text;
            dr["GGRADE2"] = ((DropDownList)(gr.FindControl("cmb_grades"))).Text;
            dr["CREATED_BY"] = "" + Session["user"].ToString();
            dr["CREATED_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);

            foreach (DataRow drG in ds.Tables["GRADINGPOLICY"].Rows)
            {
                if (drG["GGRADE2"].ToString() == ((DropDownList)(gr.FindControl("cmb_grades"))).Text)
                {
                    dr["GGRADE"] = drG["GGRADE"].ToString();
                    dr["GPOINT"] = drG["GPOINT"].ToString();
                    break;
                }
            }

            ds.Tables["OFFERERINGANDGRADE"].Rows.Add(dr);
        }

        int count = obj_staff.insert_teacher_grading(ds, status);

        if (count == ds.Tables["OFFERERINGANDGRADE"].Rows.Count)
            lbl_msg.Text = ""+new cls_message().getMessage(2);
        else
            lbl_msg.Text = "Only "+count+" record(s) " + new cls_message().getMessage(2);
    } 

    protected void btn_approve_Click(object sender, EventArgs e)
    {
        save_approve_teacherGrading(true);
        load_students();

    }
}
