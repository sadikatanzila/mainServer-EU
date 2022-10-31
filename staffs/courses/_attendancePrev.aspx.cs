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
using System.Globalization;
using System.Threading;


public partial class staffs_courses_attendancePrev : System.Web.UI.Page
{

    string code = "", teacher_ID="";

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

        if (Session["user"].ToString() == "")
            Response.Redirect("_course_list.aspx");
        if (String.IsNullOrEmpty(Session["sem"].ToString()) || String.IsNullOrEmpty(Session["year"].ToString()))
            Response.Redirect("_course_list.aspx");

        //  btn_calender.Attributes.Add("onclick", "loadCalender()");
        btn_set_attendance.Attributes.Add("onclick", "return save_check()");
        btn_show.Attributes.Add("onclick", "return save_check()");
        lbl_message.Text = "";

        if (!IsPostBack)
        {
            if (Request.QueryString["code"] != null)
            {
                code = Request.QueryString["code"].ToString();
                teacher_ID = Convert.ToString(Session["user"]);
            }
            load_courses();
            load_students();
        }
        else
        {
            code = cmb_course.SelectedValue.ToString();
            load_courses();
        }
    }


    private void load_students()
    {
        string Coursekey="", teacherID = "", section = "",teacher="";
        DateTime now = DateTime.Now;
        //string date = ;
        //DateTime dt = (DateTime.ParseExact(now, "dd-MMM-yyyy", CultureInfo.CurrentCulture));

        Coursekey = Convert.ToString(Session["sem"].ToString()) + Convert.ToString(Session["year"].ToString()) + lbl_course_code.Text;
        teacherID = code;
        section = lbl_section.Text;
        teacher = Convert.ToString(Session["user"]);
        DataSet ds = new DataSet();
       // ds.Merge(new staff_webService().get_allStudent_ofA_CoursesTeacher(cmb_course.SelectedValue.ToString()));
        ds.Merge(new staff_webService().get_allStudent_ofCT_new(Coursekey, cmb_course.SelectedValue.ToString(), now, section, teacher));
        ds.Tables["studentList"].Columns.Add("tic");

        foreach (DataRow dr in ds.Tables["studentList"].Rows)
        {
            dr["tic"] = "true";
        }

        GridView_students.DataSource = ds;
        GridView_students.DataMember = "studentList";
        GridView_students.DataBind();
        //studentList
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


    protected void btn_submit_Click(object sender, EventArgs e)
    {
        txt_date.Text = "";
        load_courses();
        load_student_attendance();
    }


    private void set_attendance()
    {

        DataSet ds = new DataSet();
        ds.Tables.Add("WEB_STUDENT_ATTENDANCE");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("COURSE_TEACHER_ID");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("SID");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("CLASS_DATE");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("ATTEND");

        int isChecked = 0;
        foreach (GridViewRow row in GridView_students.Rows)
        {
            isChecked = 0;
            if (((CheckBox)row.FindControl("chkSelected")).Checked == true)
                isChecked = 1;
            DataRow dr = ds.Tables["WEB_STUDENT_ATTENDANCE"].NewRow();
            dr["COURSE_TEACHER_ID"] = cmb_course.SelectedValue.ToString();
            dr["SID"] = "" + row.Cells[1].Text;
            if (Convert.ToString(txt_date.Text) != "")
            {
                dr["CLASS_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));//Convert.ToDateTime(txt_student_opening.Text)); 
                //  new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_student_opening"].ToString()));
            }
           
            else
            {
                lbl_message.Text = "Please enter valid  date";
            }

            //  dr["CLASS_DATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_content$txt_date"].ToString()));
            dr["ATTEND"] = "" + isChecked;
            ds.Tables["WEB_STUDENT_ATTENDANCE"].Rows.Add(dr);
        }

        if (new staff_webService().save_attendance(ds, cmb_course.SelectedValue.ToString(), DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)) == "1")
        {
            load_student_attendance();
            lbl_message.Text = "" + new cls_message().getMessage(2);

        }
    }

    protected void btn_show_Click(object sender, EventArgs e)
    {
        load_student_attendance();
    }


    protected void chckchanged(object sender, EventArgs e)
    {

        CheckBox chckheader = (CheckBox)GridView_students.HeaderRow.FindControl("CheckBox1");

        foreach (GridViewRow row in GridView_students.Rows)
        {

            CheckBox chckrw = (CheckBox)row.FindControl("chkSelected");

            if (chckheader.Checked == true)
            {
                chckrw.Checked = true;
            }
            else
            {
                chckrw.Checked = false;
            }

        }

    }  


    private void load_student_attendance()
    {
        //  txt_date.Text = Request["ctl00$ContentPlaceHolder_content$txt_date"].ToString();
        string Coursekey = "", teacherID = "", section = "", teacher = "";
       
        Coursekey = Convert.ToString(Session["sem"].ToString()) + Convert.ToString(Session["year"].ToString()) + lbl_course_code.Text;
        teacherID = code;
        section = lbl_section.Text;
        teacher = Convert.ToString(Session["user"]);

        if (txt_date.Text != "")
        {
            string isCheck = "0";
            DataSet ds = new DataSet();
            ds.Merge(new staff_webService().get_attendance_ofA_Courses(Coursekey, teacher, section, cmb_course.SelectedValue.ToString(), DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)));

            if (ds.Tables["studentList"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["studentList"].Rows)
                {
                    dr["SID"] = "" + dr["SID"].ToString() + ":" + dr["ATTEND"].ToString();
                }
                GridView_students.DataSource = ds;
                GridView_students.DataMember = "studentList";
                GridView_students.DataBind();

                foreach (GridViewRow row in GridView_students.Rows)
                {
                    isCheck = row.Cells[1].Text.Split(':')[1];
                    row.Cells[1].Text = row.Cells[1].Text.Split(':')[0];

                    if (isCheck == "1")
                    {
                        ((CheckBox)row.FindControl("chkSelected")).Checked = true;
                    }
                }
            }
            else
            {
                foreach (GridViewRow row in GridView_students.Rows)
                {

                    ((CheckBox)row.FindControl("chkSelected")).Checked = false;

                }

            }

        }
        else
        {
            //lbl_message.Text = "To see attendance please select date first.";
            load_students();

        }

    }

    protected void btn_set_attendance_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(txt_date.Text) != "")
        {
            DateTime txtdate = DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture);
            DateTime now = DateTime.Now;
            if (txtdate <= now)
            {
                set_attendance();
            }
            else
            {
                lbl_message.Text = "Please use a valid date less or equal than Current Date";
            }

           
        }

        else
        {
            lbl_message.Text = "Please enter valid  date";
        }
       
    }
}
