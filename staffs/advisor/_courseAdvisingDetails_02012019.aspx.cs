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

public partial class staffs_advisor_courseAdvisingDetails_02012019 : System.Web.UI.Page
{
    string course = "";
    staff_webService obj_staff = new staff_webService();
    string sid = "";
    string sem = "";
    string year = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("_login.aspx");
            else if (String.IsNullOrEmpty(Session["user"].ToString()))
            {
                Response.Redirect("_login.aspx");
            }
        }
        catch (Exception erp) { Response.Redirect("_login.aspx"); }

        if (Request.QueryString["code"] != null)
        {
            string[] code = Request.QueryString["code"].ToString().Split('_');
            if (code.Length > 0)
            {
                sid = code[0];
                sem = code[1];
                year = code[2];
            }
            else Response.Redirect("_login.aspx");
        }
        else Response.Redirect("_login.aspx");

        lbl_course.Text = "";
        lbl_message.Text = "";
        lbl_advice.Text = "";
        lbl_total_credit.Text = "";
        btn_add.Visible = false;


        if (IsPostBack)
            course = cmb_course.SelectedValue.ToString();
        else
        {
            // tbl_newOffering.Visible = false;
            // tbl_offered_courses.Visible = false;
            load_available_courses();
            load_temp_taken_courses();
        
        }

        load_student();


    }

    private void load_student()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_a_student_information(sid));

        foreach (DataRow dr in ds.Tables["student"].Rows)
        {
            lbl_sid.Text = "" + sid;
            lbl_sName.Text = dr["SNAME"].ToString();
            lbl_program.Text = dr["SPROGRAM"].ToString();
            // hplnk_academicStat.Attributes.Add("onClick","");

            hplnk_academicStat.NavigateUrl = "#" + sid;
            hplnk_academicStat.Attributes.Add("onClick", " return open_current_academic_status('" + sid + "');");


        }

    }

    private void load_temp_taken_courses()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_all_preOffered_coursesFull(sid, sem, year));

        ds.Tables["pree_offered"].Columns.Add("cName");
        ds.Tables["pree_offered"].Columns.Add("available_seat");

        double ttCtrdit = 0; string CAPACITY = "",Section="", TOTAL_STUDENT = "", TOTAL_CAPACITY="";
        foreach (DataRow dr in ds.Tables["pree_offered"].Rows)
        {
            dr["cName"] = obj_staff.get_a_courseName_onKey(dr["COURSEKEY"].ToString());
           // Section = GridView_taken_list.Rows[gr.RowIndex].Cells[4].Text;
          /*  CAPACITY = obj_staff.get_allocated_teacher(dr["COURSEKEY"].ToString());
           //  = Convert.ToString(dr["DUE"]);
            string[] code = CAPACITY.Split('|');
            if (code.Length > 0)
            {
                TOTAL_STUDENT = code[0];
                TOTAL_CAPACITY = code[1];
                dr["available_seat"] = TOTAL_STUDENT + "/" + TOTAL_CAPACITY;
            }
            // dr["available_seat"] = "" + dr["TOTAL_STUDENT"].ToString() + "/" + dr["TOTAL_CAPACITY"].ToString();
            */
            ttCtrdit += Convert.ToDouble("0" + dr["CHOURS"].ToString());
        }

      

       // Session["StudentTakenCourses"] = ds;
        GridView_taken_list.DataSource = ds;
        GridView_taken_list.DataMember = "pree_offered";
        GridView_taken_list.DataBind();

        if (ds.Tables["pree_offered"].Rows.Count > 0)
        {
            lbl_total_credit.Text = "Total Credit is : " + ttCtrdit;
            btn_deleteCourse.Visible = true;
        }
        else
        {
            lbl_total_credit.Text = "";
            btn_deleteCourse.Visible = false;
        }

    }
  
    private void load_available_courses()
    {
        DateTime dToday = new DateTime();
        dToday = DateTime.Today;

        DataSet ds = new DataSet();
        Session["sem"] = "" + sem;
        Session["year"] = "" + year;

        ds.Merge(new admin_webService().get_pre_offerigDate(year, sem));

        if (ds.Tables["WEB_PRE_OFFERING_DATE"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["WEB_PRE_OFFERING_DATE"].Rows[0];
            if (!String.IsNullOrEmpty(dr["OPENING_DATE"].ToString()) && !String.IsNullOrEmpty(dr["CLOSING_DATE"].ToString()))
            {
                DateTime dtFrom = Convert.ToDateTime(dr["TEACHER_OPENINGDATE"].ToString());
                DateTime dtTo = Convert.ToDateTime(dr["TEACHER_CLOSINGDATE"].ToString()).AddHours(23);

                if (dToday >= dtFrom && dToday <= dtTo)
                {
                    tbl_newOffering.Visible = true;
                    tbl_offered_courses.Visible = true;

                    /*DataTable dtCourseOfferedByDep = new student_webService().get_available_course_forOfferingByDep(sid, Session["sem"].ToString(), Session["year"].ToString(), new student_webService().get_program_ofA_student(sid));
                    if (dtCourseOfferedByDep.Rows.Count > 0)
                    {
                        ds.Merge(dtCourseOfferedByDep);
                    }
                    else
                    {
                        ds.Merge(new student_webService().get_available_course_forOffering(Session["sem"].ToString(), Session["year"].ToString(), new student_webService().get_program_ofA_student(sid)));
                    }*/
                    ds.Merge(new student_webService().get_available_course_forOffering(Session["sem"].ToString(), Session["year"].ToString(), new student_webService().get_program_ofA_student(sid)));

                    for (int c = 0; c < ds.Tables["courses"].Rows.Count; c++) //delete duplicate courses
                    {
                        for (int d = c + 1; d < ds.Tables["courses"].Rows.Count; d++)
                        {
                            if (ds.Tables["courses"].Rows[c]["COURSECODE"].ToString() == ds.Tables["courses"].Rows[d]["COURSECODE"].ToString())
                            {
                                ds.Tables["courses"].Rows.RemoveAt(d--);
                            }
                        }
                    }

                    /*         forhad vie said teacher will see all cources although it is completed   (that's why it is commented)    */

                    //ds.Merge(new staff_webService().get_completed_course_information(sid)); // completed course
                    //foreach (DataRow drC in ds.Tables["courseList"].Rows) // Delete completed cources
                    //{
                    //    for (int k = 0; k < ds.Tables["courses"].Rows.Count; k++)
                    //    {
                    //        if (ds.Tables["courses"].Rows[k]["COURSECODE"].ToString() == drC["COURSECODE"].ToString())
                    //            ds.Tables["courses"].Rows.RemoveAt(k--);
                    //    }
                    //}



                    foreach (DataRow drs in ds.Tables["courses"].Rows)
                    {
                        drs["CNAME"] = drs["COURSECODE"].ToString() + " : " + drs["CNAME"].ToString();
                    }
                    cmb_course.DataSource = ds.Tables["courses"];
                    cmb_course.DataTextField = "CNAME";
                    cmb_course.DataValueField = "COURSEKEY";
                    cmb_course.DataBind();
                }
                else
                {
                    btn_add.Visible = false;
                    btn_deleteCourse.Visible = false;
                    btn_prerequisit.Visible = false;
                    btn_show.Visible = false;
                    lbl_message.Text = "" + new cls_message().getMessage(17);
                    btn_final_offering.Visible = false;
                    cmb_course.Visible = false;
                }
            }
            else
                lbl_message.Text = "" + new cls_message().getMessage(11);
        }
        else
            lbl_message.Text = "" + new cls_message().getMessage(11);
    }

    private bool check_schedule(string courseKey, string group)
    {
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        ds.Merge(new student_webService().get_a_course_routine(courseKey, group));

        ds.Merge(new student_webService().get_all_preOffered_courses(sid, Session["sem"].ToString(), Session["year"].ToString()));

        foreach (DataRow drS in ds.Tables["course_schedule"].Rows) // schedule which is going to take
        {
            foreach (DataRow dr in ds.Tables["pree_offered"].Rows) //courses taken (pre)
            {
                if (dr["COURSEKEY"].ToString() == courseKey)
                {
                    lbl_message.Font.Bold = true;
                    lbl_message.Text = "Already you have taken this course";
                    return false;
                }

                ds2.Tables.Clear();
                ds2.Merge(new student_webService().get_a_course_routine(dr["COURSEKEY"].ToString(), dr["GGROUP"].ToString()));

                foreach (DataRow drPre in ds2.Tables["course_schedule"].Rows) // schedule which is already taken
                {
                    //if ((drS["SCH_CLS_1"].ToString() == drPre["SCH_CLS_1"].ToString()) || (drS["SCH_CLS_2"].ToString() == drPre["SCH_CLS_2"].ToString()) || (drS["SCH_CLS_1"].ToString() == drPre["SCH_CLS_2"].ToString()) || (drS["SCH_CLS_2"].ToString() == drPre["SCH_CLS_1"].ToString()))
                    //{
                    //    lbl_message.Font.Bold = true;
                    //    lbl_message.Text = "Sorry, you have a routine conflict with \"" + new staff_webService().get_a_courseName_onKey(drPre["COURSE_KEY"].ToString()) + "\"";
                    //    return false;
                    //}


                    if (!string.IsNullOrEmpty(drS["Clsday1"].ToString()))
                    {
                        if (drS["Clsday1"].ToString() == drPre["Clsday1"].ToString() || drS["Clsday1"].ToString() == drPre["Clsday2"].ToString() || drS["Clsday1"].ToString() == drPre["Clsday3"].ToString())
                        {
                            lbl_message.Font.Bold = true;
                            lbl_message.Text = "Sorry, you have a routine conflict with \"" + new staff_webService().get_a_courseName_onKey(drPre["COURSE_KEY"].ToString()) + "\"";
                            return false;
                        }
                    }

                    if (!string.IsNullOrEmpty(drS["Clsday2"].ToString()))
                    {
                        if (drS["Clsday2"].ToString() == drPre["Clsday1"].ToString() || drS["Clsday2"].ToString() == drPre["Clsday2"].ToString() || drS["Clsday2"].ToString() == drPre["Clsday3"].ToString())
                        {
                            lbl_message.Font.Bold = true;
                            lbl_message.Text = "Sorry, you have a routine conflict with \"" + new staff_webService().get_a_courseName_onKey(drPre["COURSE_KEY"].ToString()) + "\"";
                            return false;
                        }
                    }


                    if (!string.IsNullOrEmpty(drS["Clsday3"].ToString()))
                    {
                        if (drS["Clsday3"].ToString() == drPre["Clsday1"].ToString() || drS["Clsday3"].ToString() == drPre["Clsday2"].ToString() || drS["Clsday3"].ToString() == drPre["Clsday3"].ToString())
                        {
                            lbl_message.Font.Bold = true;
                            lbl_message.Text = "Sorry, you have a routine conflict with \"" + new staff_webService().get_a_courseName_onKey(drPre["COURSE_KEY"].ToString()) + "\"";
                            return false;
                        }
                    }

                }
            }
        }

        return true;
    }

    private bool check_prerequisite(string courseCode)
    {
        bool status = true;

        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_prerequisite(courseCode, new student_webService().get_program_ofA_student(sid)));

        foreach (DataRow dr in ds.Tables["prerequisiteList"].Rows)
        {
            ds.Merge(new student_webService().get_prerequisite_done(sid, dr["PRECODE"].ToString()));
            if (ds.Tables["preRequisit"].Rows.Count > 0)
                status = true;
            else
            {
                ds.Merge(new student_webService().get_prerequisite_from_preOffered(sid, dr["PRECODE"].ToString()));
                if (ds.Tables["per_preRequisit"].Rows.Count > 0)
                    status = true;
                else
                {
                    status = false;
                    lbl_message.Text = "Without completing " + dr["PRECODE"].ToString() + " you cannot take " + courseCode + " because is it prerequisit requirement.";
                    break;
                }
            }
        }
        return status;
    }

    protected void btn_deleteCourse_Click(object sender, EventArgs e)
    {
        string courseKey = "";
        string regKey = "";

        string DROP_TIME = Convert.ToString(Convert.ToDateTime(DateTime.Now));//new cls_tools().get_database_formateDate(DateTime.Today);
        //  string COURSE_TEACHER_ID = DateTime.Now.ToString("dd/mm/yyyy");
        string DROPID = Session["user"].ToString();

        foreach (GridViewRow gr in GridView_taken_list.Rows)
        {
            if (((CheckBox)(gr.FindControl("CheckBox1"))).Checked == true)
            {
                courseKey = ((Label)(gr.FindControl("COURSEKEY"))).Text;
                regKey = ((Label)(gr.FindControl("REGKEY"))).Text;
                if (new student_webService().DeletedDataTecIntoDemo(courseKey,regKey,DROPID, DROP_TIME) == "1")
                {
                    obj_staff.delete_preOffer_courses(courseKey, regKey);
                }
            }
        }
        load_temp_taken_courses();
    }
    protected void btn_prerequisit_Click(object sender, EventArgs e)
    {

    }

    protected void btn_show_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allocated_teacher(course));
        ds.Tables["WEB_VIEW_COURSE_TEACHER"].Columns.Add("available_seat");

      /*  foreach (DataRow dr in ds.Tables["WEB_VIEW_COURSE_TEACHER"].Rows)
        {
            dr["available_seat"] = "" + (Convert.ToInt32(dr["TOTAL_STUDENT"]) > Convert.ToInt32(dr["TEMP_TOTAL_STUDENT"]) ? dr["TOTAL_STUDENT"].ToString() : dr["TEMP_TOTAL_STUDENT"].ToString()) + "/" + dr["TOTAL_CAPACITY"].ToString();
            lbl_course.Text = dr["COURSECODE"].ToString() + " : " + dr["CNAME"].ToString();
        }*/

        foreach (DataRow dr in ds.Tables["WEB_VIEW_COURSE_TEACHER"].Rows)
        {
            dr["available_seat"] = "" + (Convert.ToInt32(dr["TOTAL_STUDENT"]) > Convert.ToInt32(dr["TEMP_TOTAL_STUDENT"]) ? dr["TOTAL_STUDENT"].ToString() : dr["TEMP_TOTAL_STUDENT"].ToString()) + "/" + dr["TOTAL_CAPACITY"].ToString();
                    //  dr["available_seat"] = "" + dr["TEMP_TOTAL_STUDENT"].ToString() + "/" + dr["TOTAL_CAPACITY"].ToString();
            lbl_course.Text = dr["COURSECODE"].ToString() + " : " + dr["CNAME"].ToString();
        }
        if (ds.Tables["WEB_VIEW_COURSE_TEACHER"].Rows.Count > 0)
        {
            lbl_advice.Text = "" + new cls_message().getMessage(12);
            btn_add.Visible = true;
        }

        GridView_availableCourses.DataSource = ds;
        GridView_availableCourses.DataMember = "WEB_VIEW_COURSE_TEACHER";
        GridView_availableCourses.DataBind();


        // disable if capacity and enrolled is same
        foreach (GridViewRow gr in GridView_availableCourses.Rows)
        {
            if (!String.IsNullOrEmpty(gr.Cells[8].Text))
            {
                if (Convert.ToInt32("0" + gr.Cells[8].Text.Split('/')[0].ToString()) >= Convert.ToInt32("0" + gr.Cells[8].Text.Split('/')[1].ToString()))
                    gr.Enabled = false;
            }
        }

        load_temp_taken_courses();

    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        String courseKey = "";
        String group = "";
		int totalCapacity = 0, enrolledStudent = 0;

        foreach (GridViewRow gr in GridView_availableCourses.Rows)
        {
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                courseKey = ((Label)(gr.FindControl("lbl_course_key"))).Text;
                group = ((Label)(gr.FindControl("lbl_group"))).Text;
                string[] tmp = gr.Cells[8].Text.Split('/');
                enrolledStudent = new student_webService().GetEnrolledStudentOfCourse(courseKey, group);
               // enrolledStudent = new student_webService().GetEnrolledStudentOfCourse_New(courseKey, group);
                totalCapacity = int.Parse(tmp[1]);
                gr.Cells[8].Text = enrolledStudent.ToString() + "/" + totalCapacity.ToString();

                if (enrolledStudent >= totalCapacity)
                {
                    ((CheckBox)(gr.FindControl("chk_select"))).Checked = false;
                    gr.Enabled = false;
                }

                break;
            }
        }
		
		if (enrolledStudent >= totalCapacity)
        {
            lbl_message.Text = "Sorry, group is full. Choose another.";
            return;
        }

        if (!String.IsNullOrEmpty(courseKey) && !String.IsNullOrEmpty(group))
        {
            string courseCode = courseKey.Substring(5, courseKey.Length - 5);
            if (check_prerequisite(courseCode))
            {
                if (check_schedule(courseKey, group))
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add("WEB_COURSE_OFFERING_TEMP");
                    ds.Tables["WEB_COURSE_OFFERING_TEMP"].Columns.Add("COURSEKEY");
                    ds.Tables["WEB_COURSE_OFFERING_TEMP"].Columns.Add("REGKEY");
                    ds.Tables["WEB_COURSE_OFFERING_TEMP"].Columns.Add("GGROUP");
                    ds.Tables["WEB_COURSE_OFFERING_TEMP"].Columns.Add("CHOURS");
                    ds.Tables["WEB_COURSE_OFFERING_TEMP"].Columns.Add("CTRL");
                    ds.Tables["WEB_COURSE_OFFERING_TEMP"].Columns.Add("TEACHER_COMMENTS");
                    ds.Tables["WEB_COURSE_OFFERING_TEMP"].Columns.Add("STUDENT_COMMENTS");
                    ds.Tables["WEB_COURSE_OFFERING_TEMP"].Columns.Add("INSERTION_TIME");

                    DataRow dr = ds.Tables["WEB_COURSE_OFFERING_TEMP"].NewRow();
                    dr["COURSEKEY"] = "" + courseKey;
                    dr["REGKEY"] = "" + sid + Session["sem"].ToString() + Session["year"].ToString();
                    dr["GGROUP"] = "" + group;
                    dr["CHOURS"] = "" + new admin_webService().get_latest_creditHours_ofA_course(courseCode);
                    dr["CTRL"] = "1";
                    //dr["TEACHER_COMMENTS"] = "";
                    //dr["STUDENT_COMMENTS"] = "";
                    dr["INSERTION_TIME"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));
                    ds.Tables["WEB_COURSE_OFFERING_TEMP"].Rows.Add(dr);

                   // string regKey = sid + Session["sem"].ToString() + Session["year"].ToString();


                    string INSERTIONTIME_TEC = Convert.ToString(Convert.ToDateTime(DateTime.Now));// new cls_tools().get_database_formateDate(DateTime.Today);
                    string INSERTIONIDTEC = Session["user"].ToString();


                    if (new student_webService().NewInsertIntoDemo(ds, INSERTIONTIME_TEC, INSERTIONIDTEC) == "1")
                    {
                        new student_webService().save_pre_offering(ds);
                    }

                   // new staff_webService().Insert_NewOffer_courses(ds, New_INSERTION_TIME, NEW_INSERTIONID);

                    load_temp_taken_courses();
                    load_available_courses();
                }
            }
        }
    }

    protected void btn_final_offering_Click(object sender, EventArgs e)
    {
        int Flag = 0;
        String capacity = "", TOTAL_STUDENT = "", TOTAL_CAPACITY = "", courseKey = "",courseName="", Grp="";
        if (sid != null)
        {
            foreach (GridViewRow gr in GridView_taken_list.Rows)
            {
                TOTAL_STUDENT = ((Label)(gr.FindControl("lblTOTAL_STUDENT"))).Text;
                TOTAL_CAPACITY = ((Label)(gr.FindControl("lblTOTAL_CAPACITY"))).Text;
               // capacity = GridView_taken_list.Rows[gr.RowIndex].Cells[10].Text;
                courseKey = ((Label)(gr.FindControl("lblCourseID"))).Text;
                courseName = ((Label)(gr.FindControl("lblCourse"))).Text;
                Grp = GridView_taken_list.Rows[gr.RowIndex].Cells[4].Text;

              /*  string[] code = capacity.Split('/');
                if (code.Length > 0)
                {
                    TOTAL_STUDENT = code[0];
                    TOTAL_CAPACITY = code[1];
                }
                */
                if (Convert.ToInt32(TOTAL_CAPACITY) > Convert.ToInt32(TOTAL_STUDENT))
                {
                    lbl_Error.Text = "";
                    Flag = 1;
                }
                else
                {
                    Flag = 0;
                    lbl_Error.Text = "The capacity of this selected Course " + courseKey + " (" + courseName + "), Section" + Grp + " is full, Please Select another Section for this course";
                    break;
                }
            }

            if (Flag == 1)
            {
                lbl_Error.Text = "";
                DataSet ds = new DataSet();
                DataSet ApprovalCourse_ds = new DataSet();
                ds.Merge(new student_webService().get_all_preOffered_coursesFull(sid, sem, year));

                ds.Tables["pree_offered"].Columns.Add("cName");

                double ttCtrdit = 0;
                //foreach (DataRow dr in ds.Tables["pree_offered"].Rows)
                //{
                //    dr["cName"] = obj_staff.get_a_courseName_onKey(dr["COURSEKEY"].ToString());
                //    ttCtrdit += Convert.ToDouble("0" + dr["CHOURS"].ToString());
                //}

                ds.Tables.Add("OFFERERINGANDGRADE");
                ds.Tables["OFFERERINGANDGRADE"].Columns.Add("COURSEKEY");//
                ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GGRADE");
                ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GTYPE");
                ds.Tables["OFFERERINGANDGRADE"].Columns.Add("MARKS");
                ds.Tables["OFFERERINGANDGRADE"].Columns.Add("REGKEY");//
                ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GGROUP");//
                ds.Tables["OFFERERINGANDGRADE"].Columns.Add("CHOURS");//
                ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GPOINT");
                ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GGRADE2");
                ds.Tables["OFFERERINGANDGRADE"].Columns.Add("COURSE_INSERTIONDATE");

                foreach (DataRow dr in ds.Tables["pree_offered"].Rows)
                {
                    DataRow drn = ds.Tables["OFFERERINGANDGRADE"].NewRow();

                    drn["COURSEKEY"] = "" + dr["COURSEKEY"];
                    drn["GGRADE"] = "I";
                    drn["GTYPE"] = "";
                    drn["MARKS"] = "";
                    drn["REGKEY"] = "" + dr["REGKEY"];
                    drn["GGROUP"] = "" + dr["GGROUP"];
                    drn["CHOURS"] = "" + dr["CHOURS"].ToString();
                    drn["GPOINT"] = "0";
                    drn["GGRADE2"] = "I";

                    drn["COURSE_INSERTIONDATE"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));

                    ds.Tables["OFFERERINGANDGRADE"].Rows.Add(drn);
                }

                string str_msg = "";


                string APPROVETIME = Convert.ToString(Convert.ToDateTime(DateTime.Now));// new cls_tools().get_database_formateDate(DateTime.Today);
                string APPROVEID = Session["user"].ToString();


                if (new student_webService().ApproveIntoDemo(ds, APPROVETIME, APPROVEID) >= 1)
                {
                    int count = new staff_webService().insert_final_advising(ds, ref str_msg);


                    ApprovalCourse_ds.Merge(new student_webService().get_all_FinalAdvising_coursesFull(sid, sem, year));

                    if (ds.Tables["OFFERERINGANDGRADE"].Rows.Count == count)
                    {
                        Session["StudentTakenCourses"] = ApprovalCourse_ds;

                        Session["P_Title"] = "Student Course Advising";
                        Session["P_StudentId"] = lbl_sid.Text;
                        Session["P_StudentName"] = lbl_sName.Text;
                        Session["P_StudentDepartment"] = lbl_program.Text;
                        Session["P_Semester"] = (sem == "1" ? "Spring" : (sem == "2" ? "Summer" : "Fall")) + ", " + year.ToString();
                        Session["P_Date"] = DateTime.Now.ToString("dd/mm/yyyy");
                        Session["P_AdvisorId"] = Session["user"];
                        Session["P_AdvisorName"] = new staff_webService().get_staff_info(Session["user"].ToString()).Rows[0]["STAFF_NAME"].ToString();


                        Response.Redirect("advising_message.aspx?msg=" + "Advising completed Successfully");



                    }
                    else
                    {


                        Session["StudentTakenCourses"] = ApprovalCourse_ds;

                        Session["P_Title"] = "Student Course Advising";
                        Session["P_StudentId"] = lbl_sid.Text;
                        Session["P_StudentName"] = lbl_sName.Text;
                        Session["P_StudentDepartment"] = lbl_program.Text;
                        Session["P_Semester"] = (sem == "1" ? "Spring" : (sem == "2" ? "Summer" : "Fall")) + ", " + year.ToString();
                        Session["P_Date"] = DateTime.Now.ToString("dd/mm/yyyy");
                        Session["P_AdvisorId"] = Session["user"];
                        Session["P_AdvisorName"] = new staff_webService().get_staff_info(Session["user"].ToString()).Rows[0]["STAFF_NAME"].ToString();


                        Response.Redirect("advising_message.aspx? and " + str_msg + " Course(s) failed to advising, please contact with IT department");
                    }

                }
            }
        }
      //  else
        
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
		Session["P_Title"] = "Student Course Advising";
        Session["P_StudentId"] = lbl_sid.Text;
        Session["P_StudentName"] = lbl_sName.Text;
        Session["P_StudentDepartment"] = lbl_program.Text;
        Session["P_Semester"] = (sem == "1" ? "Spring" : (sem == "2" ? "Summer" : "Fall")) + ", " + year.ToString();
        Session["P_Date"] = DateTime.Now.ToString("dd/mm/yyyy");
        Session["P_AdvisorId"] = Session["user"];
        Session["P_AdvisorName"] = new staff_webService().get_staff_info(Session["user"].ToString()).Rows[0]["STAFF_NAME"].ToString();

        Page.RegisterClientScriptBlock("print", "<script>window.open('_studentCourseAdvisingReport.aspx','','titlebar=yes,toolbar=no,scrollbars,resizable=true,height=650,width=900');</script>");
    }
}
