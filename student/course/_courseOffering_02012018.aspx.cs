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

public partial class student_course_courseOffering_02012018 : System.Web.UI.Page
{
    string sid = "";
    string course = "";
    staff_webService obj_staff = new staff_webService();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session.Count == 0)
            Response.Redirect("../_login.aspx");
        else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
        {
            sid = Session["ctrlId"].ToString();
            Response.Redirect("../_login.aspx");
        }
        else
        {
            sid = Session["ctrlId"].ToString();
        }

        if (IsPostBack)
            course = cmb_course.SelectedValue.ToString();
        else
        {
            tbl_newOffering.Visible = false;
            tbl_offered_courses.Visible = false;
        }
        
        lbl_course.Text       = "";
        lbl_message.Text      = "";
        lbl_advice.Text       = "";
        lbl_total_credit.Text = "";
        btn_add.Visible       = false;

       //if(IsPostBack)
        

    }
	
	private bool IsOpenedForCurrentDept(string sid)
    {
        if (string.IsNullOrEmpty(AppSettings.CourseOfferingOpenedDepList)) return true;

        foreach (string dep in AppSettings.CourseOfferingOpenedDepList.Split(','))
        {
            if (sid.Substring(3, 2) == dep)
                return true;
        }

        return false;
    }
	
	private bool isCompletedHalfCreditHours(string sid)
    {
        bool isCompleted = false;

        double currentlyCompletedCreditHours = new staff_webService().get_total_completed_credit(sid);
        double totalCreditHours = 0.0;

        switch (sid.Substring(3, 2))
        {
            case "10": //LLB (Hons.)
                totalCreditHours = 130;
                break;
            case "11": //LL.M (1-year)
                totalCreditHours = 36;
                break;
            case "12": //LL.M (2-year)
                totalCreditHours = 63;
                break;
            case "20": //BBA
                totalCreditHours = 132;
                break;
            case "30": //B.A. ENG(Hons)
                totalCreditHours = 121;
                break;
            case "31": //M.A. in English
                totalCreditHours = 41;
                break;
            case "40": //CSE
                totalCreditHours = 157;
                break;
            case "50": //M.A. ELT (1 Year)
                totalCreditHours = 42;
                break;
            case "60": //MBA (Regular)
                totalCreditHours = 66;
                break;
            case "70": //MBA (Executive)
                totalCreditHours = 42;
                break;
            case "80": //EEE
                totalCreditHours = 148;
                break;
            case "82": //ETE
                totalCreditHours = 155;
                break;
            case "83": //ETE(Evening)
                totalCreditHours = 155;
                break;
        }

        if (currentlyCompletedCreditHours > totalCreditHours / 2.0)
            isCompleted = true;

        return isCompleted;
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string P_Year = "", prevSem = "", R_Sem = "", R_Year = "";
        //DataSet ds = new DataSet();
        //ds.Merge(new student_webService().get_OFFERERINGANDGRADE_Year_Semister_either(sid));

        //if (ds.Tables["Studentlist"].Rows.Count > 0)
        //{
        //    foreach (DataRow dr in ds.Tables["Studentlist"].Rows)
        //    {
        //        prevSem = dr["PREV_SEMESTER"].ToString();
        //        P_Year = dr["PREV_YEAR"].ToString();
        //    }
        //}
        //else
        //{
        //    DataSet dsN = new DataSet();
        //    dsN.Merge(new student_webService().get_OFFERERINGANDGRADE_Year_Semister(sid));

        //    if (dsN.Tables["Studentlist"].Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dsN.Tables["Studentlist"].Rows)
        //        {
        //            prevSem = dr["PREV_SEMESTER"].ToString();
        //            P_Year = dr["PREV_YEAR"].ToString();
        //        }
        //    }

        //}


        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_ADMINREGISTRATIONRATE_LYS());

        if (ds.Tables["Studentlist"].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables["Studentlist"].Rows)
            {
                prevSem = dr["semester"].ToString();
                P_Year = dr["year"].ToString();
            }
        }

        DataSet dscountStudent = new DataSet();
        dscountStudent.Merge(new student_webService().checkStudentCount());
        if (dscountStudent.Tables["StudentCount"].Rows.Count > 0)
        {
            foreach (DataRow countStudent in dscountStudent.Tables["StudentCount"].Rows)
            {
                int total = Convert.ToInt32(countStudent["total"].ToString());
                if (total <= 250)
                {
                    string DUE = "", SemDue = "", graceAmt = "";
                    DataSet InsDate_ds = new DataSet();
                    InsDate_ds.Merge(new student_webService().get_FN_GET_PER_SEM_DUE(sid, P_Year, prevSem));
                    if (InsDate_ds.Tables["SEM_DUE"].Rows.Count > 0)
                    {
                        foreach (DataRow InsDate_dr in InsDate_ds.Tables["SEM_DUE"].Rows)
                        {
                            DUE = Convert.ToString(InsDate_dr["DUE"]);

                            string[] code = DUE.Split('|'); //Request.QueryString["DUE"].ToString().Split('|');
                            if (code.Length > 0)
                            {
                                SemDue = code[0];
                                graceAmt = code[1];

                            }

                        }

                    }

                    if (Convert.ToDecimal(graceAmt) > 0)
                    {
                        if (!(new student_webService().is_valid_student(sid, txt_year.Text, cmb_semester.SelectedValue.ToString())))
                        {
                            lbl_message.Text += "" + new cls_message().getMessage(19);
                            return;
                        }
                    }

                    //--------------------------find out current going year & semester
                    double CrntYear_sem = 0;
                    CrntYear_sem = new staff_webService().get_latest_yearSemister();
                    int binding_YearSem = Convert.ToInt32(CrntYear_sem - 60);


                    if (int.Parse(sid.Substring(0, 3)) <= binding_YearSem && !(new student_webService().is_valid_student(sid, txt_year.Text, cmb_semester.SelectedValue.ToString())))
                    {
                        lbl_message.Text += "" + new cls_message().getMessage(26);
                        return;
                    }

                    #region CGPA continuous bellow 2.5 for 3 semester
                    /*
        double semester1, semester2, semester3;
        string lastSemester = "";

        semester1 = new staff_webService().get_CGPA_upto_semester(sid, txt_year.Text, cmb_semester.SelectedValue.ToString());
        if (int.Parse(cmb_semester.SelectedValue.ToString()) - 1 > 0)
        {
            semester2 = new staff_webService().get_CGPA_upto_semester(sid, txt_year.Text, (int.Parse(cmb_semester.SelectedValue.ToString()) - 1).ToString());
        }
        else
        {
            semester2 = new staff_webService().get_CGPA_upto_semester(sid, (int.Parse(txt_year.Text) - 1).ToString(), "3");
            lastSemester = "3";
        }
        if (int.Parse(cmb_semester.SelectedValue.ToString()) - 2 > 0)
        {
            semester3 = new staff_webService().get_CGPA_upto_semester(sid, txt_year.Text, (int.Parse(cmb_semester.SelectedValue.ToString()) - 2).ToString());
        }
        else
        {
            semester3 = new staff_webService().get_CGPA_upto_semester(sid, (int.Parse(txt_year.Text) - 1).ToString(), (lastSemester == "3" ? "2" : "3"));
        }

        if ((semester1 > 0.0 && semester1 < 2.5) && (semester2 > 0.0 && semester2 < 2.5) && (semester3 > 0.0 && semester3 < 2.5)
            && !(new student_webService().is_valid_student(sid, txt_year.Text, cmb_semester.SelectedValue.ToString())))
        {
            lbl_message.Text = "" + new cls_message().getMessage(27);
            return;
        }
		

        */
                    #endregion

                    //double cgpa = new staff_webService().get_latest_cgpa(sid);
                    //cgpa = cgpa == 0 ? 5.0 : cgpa;

                    //if (isCompletedHalfCreditHours(sid) && cgpa < 2.5 && !(new student_webService().is_valid_student(sid, txt_year.Text, cmb_semester.SelectedValue.ToString())))
                    //{
                    //    lbl_message.Text = "" + new cls_message().getMessage(27);
                    //    return;
                    //}

                    if (new student_webService().is_dropout_student(sid))
                    {
                        lbl_message.Text = "" + new cls_message().getMessage(25);
                        return;
                    }

                    if (!new student_webService().is_register_student(sid, cmb_semester.SelectedValue.ToString(), txt_year.Text))
                    {
                        lbl_message.Text = "" + new cls_message().getMessage(13);
                        return;
                    }



                  


                    if (is_already_approved())
                    {
                        lbl_message.Text = "Your advisor has approved your courses.";
                    }
                    else
                        if (!HasStudentCourseOffered())
                        {
                            DataSet dsC = new DataSet();
                            dsC.Merge(new staff_webService().get_a_student_information(sid));
                            if (dsC.Tables[0].Rows[0]["ADMINYEAR"].ToString() != txt_year.Text.Trim() || dsC.Tables[0].Rows[0]["ADMINSEMETER"].ToString() != cmb_semester.SelectedValue.ToString())
                            {
                                if (ConfigurationManager.AppSettings["LateAdvisingFeeStart"] == "y" & !new student_webService().IsStudentPaidAdvisingLateFee(sid, cmb_semester.SelectedValue.ToString(), txt_year.Text))
                                {
                                    lbl_message.Text = "You have not paid Late Advising fee (500/= Tk.). Please contact Account Department(Room No 601)";
                                    return;
                                }
                            }
                        }      
                    /********************************** for late advising
                    
                              

                       */





                    if (!is_already_approved())
                    {
                        load_available_courses();
                        load_temp_taken_courses();

                        lbl_message.Text += "After adding the courses Please Contact your Course Advisor for approval.";
                    }
                    else
                        lbl_message.Text = "Your advisor has approved your courses. " + new cls_message().getMessage(10);

                }
                else
                {
                    lbl_message.Text = "session is limited, try again few minutes later.";
                }
            }
        }







    }
	
	private Boolean HasStudentCourseOffered()
    {
        Boolean status = false;

        DataSet dsC = new DataSet();
        dsC.Merge(new student_webService().get_all_preOffered_coursesFull(sid, cmb_semester.SelectedValue.ToString(), txt_year.Text));

        foreach (DataRow dr in dsC.Tables["pree_offered"].Rows)
        {
            if (dr["CTRL"].ToString() == "1")
                status = true;
        }

        return status;
    }
	
    private Boolean is_already_approved()
    {
        Boolean status = false;

        DataSet dsC = new DataSet();
        dsC.Merge(new student_webService().get_all_preOffered_coursesFull(sid, cmb_semester.SelectedValue.ToString(), txt_year.Text));

        foreach (DataRow dr in dsC.Tables["pree_offered"].Rows)
        {
            if (dr["CTRL"].ToString() == "2")
                status = true;
        }

        return status;
    }

    private void load_temp_taken_courses()
    {
        DataSet ds = new DataSet();

        ds.Merge(new student_webService().get_all_preOffered_coursesFull(sid, cmb_semester.SelectedValue.ToString(), txt_year.Text));
        

        ds.Tables["pree_offered"].Columns.Add("cName");

        double ttCtrdit = 0;
        foreach (DataRow dr in ds.Tables["pree_offered"].Rows)
        {
            dr["cName"] = obj_staff.get_a_courseName_onKey(dr["COURSEKEY"].ToString());
            ttCtrdit += Convert.ToDouble("0" + dr["CHOURS"].ToString());
        }

        GridView_taken_list.DataSource = ds;
        GridView_taken_list.DataMember = "pree_offered";
        GridView_taken_list.DataBind();

        if (ds.Tables["pree_offered"].Rows.Count > 0)
        {
            lbl_total_credit.Text = "Total Credit is : " + ttCtrdit;
            btn_deleteCourse.Visible = true;
        }
        else{
            lbl_total_credit.Text = "";
            btn_deleteCourse.Visible = false;
        }

    }


    private void load_available_courses()
    {
        DateTime dToday = new DateTime();
        dToday = DateTime.Today;

        DataSet ds = new DataSet();
        Session["sem"] = "" + cmb_semester.SelectedValue.ToString();
        Session["year"] = "" + txt_year.Text;

        ds.Merge(new admin_webService().get_pre_offerigDate(txt_year.Text, cmb_semester.SelectedValue.ToString()));

        if (ds.Tables["WEB_PRE_OFFERING_DATE"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["WEB_PRE_OFFERING_DATE"].Rows[0];
            if (!String.IsNullOrEmpty(dr["OPENING_DATE"].ToString()) && !String.IsNullOrEmpty(dr["CLOSING_DATE"].ToString()))
            {
                DateTime dtFrom = Convert.ToDateTime(dr["OPENING_DATE"].ToString());
                DateTime dtTo = Convert.ToDateTime(dr["CLOSING_DATE"].ToString()).AddHours(23);

                 

                if (dToday >= dtFrom && dToday <= dtTo && IsOpenedForCurrentDept(sid))
                {
                    tbl_newOffering.Visible = true;
                    tbl_offered_courses.Visible = true;
                    
					DataTable dtCourseOfferedByDep = new student_webService().get_available_course_forOfferingByDep(sid, Session["sem"].ToString(), Session["year"].ToString(), new student_webService().get_program_ofA_student(sid));
                    if (dtCourseOfferedByDep.Rows.Count > 0)
                    {
                        ds.Merge(dtCourseOfferedByDep);
                    }
                    else
                    {
                        ds.Merge(new student_webService().get_available_course_forOffering(Session["sem"].ToString(), Session["year"].ToString(), new student_webService().get_program_ofA_student(sid)));
                    }
                    //ds.Merge(new student_webService().get_available_course_forOfferingByDep(Session["sem"].ToString(), Session["year"].ToString(), new student_webService().get_program_ofA_student(sid)));

                    for (int c = 0; c < ds.Tables["courses"].Rows.Count;c++ ) //delete duplicate courses
                    {
                        for (int d = c+1; d < ds.Tables["courses"].Rows.Count; d++)
                        {
                            if (ds.Tables["courses"].Rows[c]["COURSECODE"].ToString() == ds.Tables["courses"].Rows[d]["COURSECODE"].ToString())
                            {
                                ds.Tables["courses"].Rows.RemoveAt(d--);
                            }
                        }
                    }
/*
                    ds.Merge(new staff_webService().get_completed_course_information(sid)); // completed course
                    foreach (DataRow drC in ds.Tables["courseList"].Rows) // Delete completed cources
                    {
                        for (int k = 0; k < ds.Tables["courses"].Rows.Count; k++)
                        {
                            if (ds.Tables["courses"].Rows[k]["COURSECODE"].ToString() == drC["COURSECODE"].ToString())
                                ds.Tables["courses"].Rows.RemoveAt(k--);
                        }
                    }
*/
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
                    lbl_message.Text = "" + new cls_message().getMessage(10);
                }
            }
            else
                lbl_message.Text = "" + new cls_message().getMessage(11);
        }
        else
            lbl_message.Text = "" + new cls_message().getMessage(11);
    }

    protected void btn_show_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allocated_teacher(course));
        ds.Tables["WEB_VIEW_COURSE_TEACHER"].Columns.Add("available_seat");


        //foreach (DataRow dr in ds.Tables["WEB_VIEW_COURSE_TEACHER"].Rows)
        //{
        //    dr["available_seat"] = "" + dr["TEMP_TOTAL_STUDENT"].ToString() + "/" + dr["TOTAL_CAPACITY"].ToString();
        //    lbl_course.Text = dr["COURSECODE"].ToString() + " : " + dr["CNAME"].ToString();
        //}

        
        foreach (DataRow dr in ds.Tables["WEB_VIEW_COURSE_TEACHER"].Rows)
        {
            dr["available_seat"] = "" + (Convert.ToInt32(dr["TOTAL_STUDENT"]) > Convert.ToInt32(dr["TEMP_TOTAL_STUDENT"]) ? dr["TOTAL_STUDENT"].ToString() : dr["TEMP_TOTAL_STUDENT"].ToString()) + "/" + dr["TOTAL_CAPACITY"].ToString();
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
                if (Convert.ToInt32("0"+ gr.Cells[8].Text.Split('/')[0].ToString()) >= Convert.ToInt32("0"+ gr.Cells[8].Text.Split('/')[1].ToString()))
                    gr.Enabled = false;
            }
        }

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
                    //ds.Tables["WEB_COURSE_OFFERING_TEMP"].Columns.Add("APPROVAL_TIME");

                    DataRow dr = ds.Tables["WEB_COURSE_OFFERING_TEMP"].NewRow();
                    dr["COURSEKEY"] = "" + courseKey;
                    dr["REGKEY"] = "" + sid + Session["sem"].ToString() + Session["year"].ToString();
                    dr["GGROUP"] = "" + group;
                    dr["CHOURS"] = "" + new admin_webService().get_latest_creditHours_ofA_course(courseCode);
                    dr["CTRL"] = "1";
                    dr["INSERTION_TIME"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));
                  //  dr["APPROVAL_TIME"] = "";
                    //dr["TEACHER_COMMENTS"] = "";
                    //dr["STUDENT_COMMENTS"] = "";
                    ds.Tables["WEB_COURSE_OFFERING_TEMP"].Rows.Add(dr);

                    string INSERTION_TIME = Convert.ToString(Convert.ToDateTime(DateTime.Now));// new cls_tools().get_database_formateDate(DateTime.Today);
                    string INSERTIONID = Session["ctrlId"].ToString();

                    if (new student_webService().InsertIntoDemo(ds, INSERTION_TIME, INSERTIONID) >= 1)
                    {
                        new student_webService().save_pre_offering(ds);

                    }

                    load_temp_taken_courses();
                    load_available_courses();
                }   
            }
        }
    }

    private bool check_schedule(string courseKey,string group)
    {
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        ds.Merge(new student_webService().get_a_course_routine(courseKey, group));
       
        ds.Merge(new student_webService().get_all_preOffered_courses(sid,Session["sem"].ToString(),Session["year"].ToString()) );

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
                ds2.Merge(new student_webService().get_a_course_routine(dr["COURSEKEY"].ToString(),dr["GGROUP"].ToString()));
              
                foreach(DataRow drPre in ds2.Tables["course_schedule"].Rows) // schedule which is already taken
                {
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
                        if (drS["Clsday3"].ToString() == drPre["Clsday1"].ToString() || drS["Clsday3"].ToString() == drPre["Clsday2"].ToString() || drS["Clsday3"].ToString() == drPre["Clsday3"].ToString() )
                        {
                            lbl_message.Font.Bold = true;
                            lbl_message.Text = "Sorry, you have a routine conflict with \"" + new staff_webService().get_a_courseName_onKey(drPre["COURSE_KEY"].ToString()) + "\"";
                            return false;
                        }
                    }


                    //if (!string.IsNullOrEmpty(drS["TUT_CLS_1"].ToString()))
                    //{
                    //    if (drS["TUT_CLS_1"].ToString() == drPre["SCH_CLS_1"].ToString() || drS["TUT_CLS_1"].ToString() == drPre["SCH_CLS_2"].ToString() || drS["TUT_CLS_1"].ToString() == drPre["TUT_CLS_1"].ToString() )
                    //    {
                    //        lbl_message.Font.Bold = true;
                    //        lbl_message.Text = "Sorry, you have a routine conflict with \"" + new staff_webService().get_a_courseName_onKey(drPre["COURSE_KEY"].ToString()) + "\"";
                    //        return false;
                    //    }
                    //}
                    //if ((drS["SCH_CLS_1"].ToString() == drPre["SCH_CLS_1"].ToString()) || (drS["SCH_CLS_2"].ToString() == drPre["SCH_CLS_2"].ToString()) || (drS["SCH_CLS_1"].ToString() == drPre["SCH_CLS_2"].ToString()) ))
                    //{
                    //    lbl_message.Font.Bold = true;
                    //    lbl_message.Text = "Sorry, you have a routine conflict with \"" + new staff_webService().get_a_courseName_onKey(drPre["COURSE_KEY"].ToString())+"\""; 
                    //    return false;
                    //}
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
         
        foreach(DataRow dr in ds.Tables["prerequisiteList"].Rows)
        { 
            ds.Merge(new student_webService().get_prerequisite_done(sid, dr["PRECODE"].ToString()));
            if (ds.Tables["preRequisit"].Rows.Count > 0)
                status =true;
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
        string droptime = "";
        foreach (GridViewRow gr in GridView_taken_list.Rows)
        {
            if (((CheckBox)(gr.FindControl("CheckBox1"))).Checked == true)
            {
                courseKey = ((Label)(gr.FindControl("COURSEKEY"))).Text;
                regKey = ((Label)(gr.FindControl("REGKEY"))).Text;

                droptime = Convert.ToString(Convert.ToDateTime(DateTime.Now));
                if (new student_webService().DeletedDataIntoDemo(courseKey, regKey, droptime) == "1")
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
}
