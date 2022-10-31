using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;


public partial class staffs_courses_attendance : System.Web.UI.Page
{

    string code = "", teacher_ID = "", slot_id = "";

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
            load_timeslots();
        }
        else
        {
            code = cmb_course.SelectedValue.ToString();
            slot_id = ddlSlot.SelectedValue.ToString();
            load_courses();
            //  load_timeslots_course();
            //  load_timeslots();
        }
    }

    private void load_timeslots()
    {
        string COURSE_TEACHER_ID = Request.QueryString["code"].ToString();
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_TimeSlot_Teacher(COURSE_TEACHER_ID));

        ddlSlot.DataSource = ds.Tables["TimeSlot_list"];
        ddlSlot.DataTextField = "slot";
        ddlSlot.DataValueField = "value";
        ddlSlot.DataBind();


        if (slot_id == "")
        {
            // ddlSlot.SelectedValue = "Select";
            // cmb_teacher.SelectedValue = "Select";
        }
        else
            ddlSlot.SelectedValue = slot_id;

        //   ddlSlot.SelectedValue = slot_id;
    }


    private void load_students()
    {
        string Coursekey = "", teacherID = "", section = "", teacher = "", timeslot = "";
        DateTime now = DateTime.Now;
        //string date = ;
        //DateTime dt = (DateTime.ParseExact(now, "dd-MMM-yyyy", CultureInfo.CurrentCulture));

        Coursekey = Convert.ToString(Session["sem"].ToString()) + Convert.ToString(Session["year"].ToString()) + lbl_course_code.Text;
        teacherID = code;
        section = lbl_section.Text;
        timeslot = ddlSlot.SelectedValue.ToString();
        teacher = Convert.ToString(Session["user"]);
        DataSet ds = new DataSet();
        // ds.Merge(new staff_webService().get_attendance_ofA_Courses(Coursekey, teacher, section, timeslot, cmb_course.SelectedValue.ToString(), DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)));

        // ds.Merge(new staff_webService().get_allStudent_ofA_CoursesTeacher(cmb_course.SelectedValue.ToString()));
        ds.Merge(new staff_webService().get_allStudent_ofCT_new(Coursekey, cmb_course.SelectedValue.ToString(), now, section, teacher, timeslot));
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
                lbl_course_key.Text = dr["COURSE_KEY"].ToString();
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
        load_timeslots_course();
        pnlAttendance.Visible = false;
    }

    private void load_timeslots_course()
    {
        string COURSE_TEACHER_ID = cmb_course.SelectedValue.ToString();//Request.QueryString["code"].ToString();
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_TimeSlot_Teacher(COURSE_TEACHER_ID));

        ddlSlot.DataSource = ds.Tables["TimeSlot_list"];
        ddlSlot.DataTextField = "slot";
        ddlSlot.DataValueField = "value";
        ddlSlot.DataBind();


        /*   if (slot_id == "")
           {
               // ddlSlot.SelectedValue = "Select";
               // cmb_teacher.SelectedValue = "Select";
           }
           else
               ddlSlot.SelectedValue = slot_id;*/

        //   ddlSlot.SelectedValue = slot_id;
    }
    private void set_attendance()
    {

        DataSet ds = new DataSet();
        ds.Tables.Add("WEB_STUDENT_ATTENDANCE");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("COURSE_TEACHER_ID");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("SID");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("CLASS_DATE");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("ATTEND");




        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("COURSEKEY");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("SECTION");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("YEAR");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("SEMESTER");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("C_ROUTINE_ID");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("INSERTED");

        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("UPDATED");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("ATTEND_ID");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("STATUS");


        int isChecked = 0, check = 0;


        foreach (GridViewRow row in GridView_students.Rows)
        {
            DataRow dr = ds.Tables["WEB_STUDENT_ATTENDANCE"].NewRow();


            isChecked = 0;
            if (((CheckBox)row.FindControl("chkSelected")).Checked == true)
            {
                isChecked = 1;
                check = 1;
            }

            dr["SID"] = "" + row.Cells[1].Text;
            dr["ATTEND_ID"] = "" + ((Label)(row.FindControl("lblAttendID"))).Text;
            dr["ATTEND"] = "" + isChecked;

            dr["COURSEKEY"] = lbl_course_key.Text;
            dr["SECTION"] = lbl_section.Text;
            dr["YEAR"] = Session["year"].ToString();
            dr["SEMESTER"] = Session["sem"].ToString();
            dr["C_ROUTINE_ID"] = ddlSlot.SelectedValue.ToString();
            dr["COURSE_TEACHER_ID"] = cmb_course.SelectedValue.ToString();

            if (rbtnRegular.Checked == true)
                dr["STATUS"] = 1;
            else
                if (rtbnMakeup.Checked == true)
                    dr["STATUS"] = 2;
                else
                    if (rtbnExtra.Checked == true)
                        dr["STATUS"] = 3;



            if (Convert.ToString(txt_date.Text) != "")
            {
                dr["CLASS_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));//Convert.ToDateTime(txt_student_opening.Text)); 
                //  new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_student_opening"].ToString()));
            }

            else
            {
                lbl_message.Text = "Please enter valid  date";
            }
            dr["INSERTED"] = Convert.ToString(Convert.ToDateTime(DateTime.Now));



            ds.Tables["WEB_STUDENT_ATTENDANCE"].Rows.Add(dr);
        }



        if (btn_set_attendance.Text == "Set Attendance")
        {



            if (new staff_webService().save_attendance_new(ds, cmb_course.SelectedValue.ToString(), ddlSlot.SelectedValue.ToString(), DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)) == "1")
            {
                load_student_attendance();
                lbl_message.Text = "" + new cls_message().getMessage(2);
                btn_set_attendance.Text = "Set Attendance";
                load_students();
                pnlAttendance.Visible = false;
            }
        }
        else
        {


            string Coursekey = "", teacherID = "", section = "", teacher = "", timeslot = "";
            Coursekey = Convert.ToString(Session["sem"].ToString()) + Convert.ToString(Session["year"].ToString()) + lbl_course_code.Text;
            teacherID = cmb_course.SelectedValue.ToString(); // teacherID = code;
            section = lbl_section.Text;
            teacher = Convert.ToString(Session["user"]);
            timeslot = ddlSlot.SelectedValue.ToString();
            // CLASS_DATE =  new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));


            if (new staff_webService().UpdateIntoDemo(Coursekey, teacherID, section, timeslot, (DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)), Convert.ToString(Convert.ToDateTime(DateTime.Now))) == "1")
            {
                if (new staff_webService().drop_attendance_new(Coursekey, teacherID, section, timeslot, (DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture))) == "1")
                {
                    if (new staff_webService().save_attendance_new(ds, cmb_course.SelectedValue.ToString(), ddlSlot.SelectedValue.ToString(), DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)) == "1")
                    {
                        load_student_attendance();
                        lbl_message.Text = "" + new cls_message().getMessage(2);
                        btn_set_attendance.Text = "Set Attendance";
                        load_students();
                        pnlAttendance.Visible = false;
                    }
                }

                /*  if (new staff_webService().update_attendance_new(ds) == "1")
                  {
                      load_student_attendance();
                      lbl_message.Text = "Changed Successfully";//+ new cls_message().getMessage(2);
                      btn_set_attendance.Text = "Set Attendance";
                      load_students();

                  }*/
            }
        }

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

    protected void btn_show_Click(object sender, EventArgs e)
    {
        load_student_attendance();
        pnlAttendance.Visible = true;
    }

    private void load_student_attendance()
    {
        //  txt_date.Text = Request["ctl00$ContentPlaceHolder_content$txt_date"].ToString();
        string Coursekey = "", teacherID = "", section = "", teacher = "", timeslot = "";
        int check = 0;

        Coursekey = Convert.ToString(Session["sem"].ToString()) + Convert.ToString(Session["year"].ToString()) + lbl_course_code.Text;
        teacherID = code;
        section = lbl_section.Text;
        teacher = Convert.ToString(Session["user"]);
        timeslot = ddlSlot.SelectedValue.ToString();

        if (txt_date.Text != "")
        {
            string isCheck = "0";
            DataSet ds = new DataSet();
            ds.Merge(new staff_webService().get_attendance_ofAny_Courses(Coursekey, teacher, section, timeslot, cmb_course.SelectedValue.ToString(), DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)));
            if (ds.Tables["anystudentList"].Rows.Count > 0)
            {
                btn_set_attendance.Text = "Update Attendance";
            }

            else
            {
                btn_set_attendance.Text = "Set Attendance";
            }





            DataSet dsn = new DataSet();
            dsn.Merge(new staff_webService().get_attendance_ofA_Courses(Coursekey, teacher, section, timeslot, cmb_course.SelectedValue.ToString(), DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)));

            if (dsn.Tables["studentList"].Rows.Count > 0)
            {
                DataSet dsn_Status = new DataSet();
                dsn_Status.Merge(new staff_webService().get_attendanceStstus_ofA_Courses(Coursekey, teacher, section, timeslot, cmb_course.SelectedValue.ToString(), DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)));

                foreach (DataRow dr_Status in dsn_Status.Tables["studentList"].Rows)
                {
                    if (Convert.ToString(dr_Status["STATUS"]) == "1")
                        rbtnRegular.Checked = true;
                    else if (Convert.ToString(dr_Status["STATUS"]) == "2")
                        rtbnMakeup.Checked = true;
                    else
                        if (Convert.ToString(dr_Status["STATUS"]) == "3")
                            rtbnExtra.Checked = true;
                }

                foreach (DataRow dr in dsn.Tables["studentList"].Rows)
                {
                    dr["SID"] = "" + dr["SID"].ToString() + ":" + dr["ATTEND"].ToString();
                }
                GridView_students.DataSource = dsn;
                GridView_students.DataMember = "studentList";
                GridView_students.DataBind();

                foreach (GridViewRow row in GridView_students.Rows)
                {
                    isCheck = row.Cells[1].Text.Split(':')[1];
                    row.Cells[1].Text = row.Cells[1].Text.Split(':')[0];

                    if (isCheck == "1")
                    {
                        ((CheckBox)row.FindControl("chkSelected")).Checked = true;
                        check = 1;
                    }
                }



                /*
                if (check == 1)
                {
                   
                }
                else
                {
                    btn_set_attendance.Text = "Set Attendance";
                }*/

            }
            else
            {
                btn_set_attendance.Text = "Set Attendance";
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
            set_attendance();
          


        }

        else
        {
            lbl_message.Text = "Please enter valid  date";
        }

    }
    protected void btn_Drop_attendance_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("WEB_STUDENT_ATTENDANCE");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("COURSE_TEACHER_ID");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("SID");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("CLASS_DATE");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("ATTEND");




        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("COURSEKEY");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("SECTION");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("YEAR");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("SEMESTER");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("C_ROUTINE_ID");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("INSERTED");

        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("UPDATED");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("DELETED");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("ATTEND_ID");
        ds.Tables["WEB_STUDENT_ATTENDANCE"].Columns.Add("STATUS");

        int isChecked = 0;


        foreach (GridViewRow row in GridView_students.Rows)
        {
            DataRow dr = ds.Tables["WEB_STUDENT_ATTENDANCE"].NewRow();
            isChecked = 0;
            if (((CheckBox)row.FindControl("chkSelected")).Checked == true)
                isChecked = 1;

            dr["COURSE_TEACHER_ID"] = cmb_course.SelectedValue.ToString();
            dr["SID"] = "" + row.Cells[1].Text;
            dr["ATTEND_ID"] = "" + ((Label)(row.FindControl("lblAttendID"))).Text;

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

            dr["COURSEKEY"] = lbl_course_key.Text;
            dr["SECTION"] = lbl_section.Text;
            dr["YEAR"] = Session["year"].ToString();
            dr["SEMESTER"] = Session["sem"].ToString();

            dr["C_ROUTINE_ID"] = ddlSlot.SelectedValue.ToString();
            dr["DELETED"] = Convert.ToString(Convert.ToDateTime(DateTime.Now));

            if (rbtnRegular.Checked == true)
                dr["STATUS"] = 1;
            else
                if (rtbnMakeup.Checked == true)
                    dr["STATUS"] = 2;
                else
                    if (rtbnExtra.Checked == true)
                        dr["STATUS"] = 3;
            ds.Tables["WEB_STUDENT_ATTENDANCE"].Rows.Add(dr);


        }


        string Coursekey = "", teacherID = "", section = "", teacher = "", timeslot = "";
        Coursekey = Convert.ToString(Session["sem"].ToString()) + Convert.ToString(Session["year"].ToString()) + lbl_course_code.Text;
        teacherID = code;
        section = lbl_section.Text;
        teacher = Convert.ToString(Session["user"]);
        timeslot = ddlSlot.SelectedValue.ToString();
        // CLASS_DATE =  new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));


        if (new staff_webService().DeletedIntoDemo(Coursekey, teacherID, section, timeslot, (DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)), Convert.ToString(Convert.ToDateTime(DateTime.Now))) == "1")
        {
            if (new staff_webService().drop_attendance_new(Coursekey, teacherID, section, timeslot, (DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture))) == "1")
            {
                load_student_attendance();
                lbl_message.Text = "Deleted Successfully";//+ new cls_message().getMessage(2);
                pnlAttendance.Visible = false;

            }
        }

    }
    protected void cmb_course_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_submit_Click(sender, e);
    }
}
