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

public partial class admin_CourseOfferingStudent : System.Web.UI.Page
{
    string course = "";
    staff_webService obj_staff = new staff_webService();
    string sid = "";
    string sem = "";
    string year = "";
    string DEPTCODE = "";

    string user = "";
    string stf_id = "";
    string dep = "";
  //  string modify = "0";

    protected void Page_Load(object sender, EventArgs e)
        {
        if (HttpContext.Current.Session["ctrl_admin_Id"] != null && !String.IsNullOrEmpty(HttpContext.Current.Session["ctrl_admin_Id"].ToString()))
        {
            if (Session["ROLE_ID"].ToString() != "1"
                && !HttpContext.Current.Request.Url.AbsoluteUri.Contains("_login.aspx"))
            {
                btn_deleteCourse.Visible = false;
            }
            else
            {
                btn_deleteCourse.Visible = true;
            }

          

        }
       

        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                user = Session["ctrl_admin_Id"].ToString();
                DEPTCODE = Session["DEPTCODE"].ToString();
            }
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

        if (!IsPostBack)
        {
            // loadProgram();
        }
    }

    /*
    private void load_course()
    {
        DataSet ds = new DataSet();

        ds.Merge(new student_webService().get_available_course_forOffering("1", "2019", "EEE"));

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


        foreach (DataRow drs in ds.Tables["courses"].Rows)
        {
            drs["CNAME"] = drs["COURSECODE"].ToString() + " : " + drs["CNAME"].ToString();
        }
        cmb_course.DataSource = ds.Tables["courses"];
        cmb_course.DataTextField = "CNAME";
        cmb_course.DataValueField = "COURSEKEY";
        cmb_course.DataBind();

    }
   
    */
    
    protected void txtSID_TextChanged(object sender, EventArgs e)
    {
        sid = Convert.ToString(txtSID.Text);


        if (Session["DEPTCODE"].ToString() != "")
        {
            string DelT_stdbt = new student_webService().get_StudentName(sid, Session["DEPTCODE"].ToString());
            if (DelT_stdbt != "")
            {
                lblStudent.Text = DelT_stdbt;
                pnlViewStudentInfo.Visible = true;
                lbl_Error.Text = "";
                if (txtYear.Text != "" && cmb_semester.SelectedValue.ToString() != "")
                    btnSubmit_Click(sender, e);
            }
            else
            {
                pnlViewStudentInfo.Visible = false;
                lbl_Error.Text = "No Data Found, this Student ID is not available in your department";
            }
        }
        else
        {
            string DelT_stdbt = new student_webService().FindStdName(sid);
            if (DelT_stdbt != "")
            {
                lblStudent.Text = DelT_stdbt;
                pnlViewStudentInfo.Visible = true;
                lbl_Error.Text = "";
                if (txtYear.Text != "" && cmb_semester.SelectedValue.ToString() != "")
                    btnSubmit_Click(sender, e);
            }
            else
            {
                pnlViewStudentInfo.Visible = false;
                lbl_Error.Text = "No Data Found";
                
            }

          //  ds.Merge(new admin_webService().get_all_offeredCourses(txt_year.Text, cmb_semester.SelectedValue.ToString()));
        }


       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
      {
        
        DataSet ds = new DataSet();
        sem = cmb_semester.SelectedValue.ToString();
        year = txtYear.Text;
        sid = txtSID.Text;

        DataSet ds_running = new DataSet();
       

        //find out maximum running year semester
        if (Session["DEPTCODE"].ToString() != "")
            ds_running.Merge(new student_webService().Check_RuuningYearSemesterDepwise(Session["DEPTCODE"].ToString()));
        else
            ds_running.Merge(new student_webService().Check_RuuningYearSemester(sid));



        if (ds_running.Tables["Ruuning"].Rows.Count > 0)
        {
            foreach (DataRow dr in ds_running.Tables["Ruuning"].Rows)
            {
                //check only first semister student only
                if (txtYear.Text + cmb_semester.SelectedValue.ToString() == dr["running"].ToString())
                {

                    ds.Merge(new student_webService().Check_Registration_Student(sid, sem, year));

                    if (ds.Tables["Registatus"].Rows.Count > 0)
                    {
                        lbl_Error.Text = "";
                        DataSet maxds = new DataSet();
                        string sem_year = "";
                        maxds.Merge(new student_webService().Check_MinRegistration_Student(sid, sem, year));
                        if (maxds.Tables["MaxRegistatus"].Rows.Count > 0)
                        {
                            load_taken_courses(txtSID.Text, cmb_semester.SelectedValue.ToString(), txtYear.Text);
                            load_available_courses();
                            Session["modify"] = "0";

                            if (HttpContext.Current.Session["ctrl_admin_Id"] != null && !String.IsNullOrEmpty(HttpContext.Current.Session["ctrl_admin_Id"].ToString()))
                            {
                                if (Session["ROLE_ID"].ToString() != "1")
                                {
                                    btn_deleteCourse.Visible = false;
                                }
                                else
                                {
                                    btn_deleteCourse.Visible = true;
                                }

                            }

                            pnlCourseTaken.Visible = true;
                            pnlCourseOffer.Visible = true;
                            pnlViewStudentInfo.Visible = true;
                        }
                        else
                        {
                            lbl_Error.Text = "Course Offering in valid only for 1st Semester";
                            pnlViewStudentInfo.Visible = false;
                        }
                    }
                    else
                    {
                        lbl_Error.Text = "This Student ID is not Registered for this Current Year & Semester";
                        pnlCourseTaken.Visible = false;
                        pnlCourseOffer.Visible = false;
                        pnlViewStudentInfo.Visible = false;
                    }
                }
                else
                {
                    lbl_Error.Text = "Course Offering in valid only for 1st Semester";
                    pnlCourseTaken.Visible = false;
                    pnlCourseOffer.Visible = false;
                    pnlViewStudentInfo.Visible = false;
                }
                  
            }
        }
        //check last running year sem

        
       
      //  refreshdata();
    }


    private void load_taken_courses(string sid, string sem, string year)
    {
        DataSet ds = new DataSet();

        ds.Merge(new student_webService().get_all_Offered_coursesFull(sid, sem, year));

        ds.Tables["OfferedCourses"].Columns.Add("cName");

        string CAPACITY = "", Section = "", TOTAL_STUDENT = "", TOTAL_CAPACITY = "", totalCourse = "";
        double ttCtrdit = 0;
        foreach (DataRow dr in ds.Tables["OfferedCourses"].Rows)
        {
            dr["cName"] = obj_staff.get_a_courseName_onKey(dr["COURSEKEY"].ToString());
            ttCtrdit += Convert.ToDouble("0" + dr["CHOURS"].ToString());
            totalCourse += Convert.ToString(dr["course"]) + ", ";
        }
        lbl_Approval.Text = totalCourse + " course(s) already have taken.";



        Session["StudentTakenCourses"] = ds;
        GridView_taken_list.DataSource = ds;
        GridView_taken_list.DataMember = "OfferedCourses";
        GridView_taken_list.DataBind();

        if (ds.Tables["OfferedCourses"].Rows.Count > 0)
        {
            lbl_total_credit.Text = "Total Credit is : " + ttCtrdit;
            GridView_taken_list.Visible = true;
            pnlCourseTaken.Visible = true;
        }
        else
        {
            lbl_total_credit.Text = "";
            GridView_taken_list.Visible = false;
            lbl_Approval.Text = "";
            pnlCourseTaken.Visible = false;
        }

    }


    private void load_available_courses()
    {
        DateTime dToday = new DateTime();
        dToday = DateTime.Today;

        DataSet ds = new DataSet();
        sid = txtSID.Text;
        Session["sem"] = "" + cmb_semester.SelectedValue.ToString() ;
        Session["year"] = "" + txtYear.Text ;

        ds.Merge(new student_webService().get_available_course_forOffering(Session["sem"].ToString(), Session["year"].ToString(), new student_webService().get_program_ofA_student(sid)));

        if (ds.Tables["courses"].Rows.Count > 0)
        {
            pnlCourseOffer.Visible = true;

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
            pnlCourseOffer.Visible = false;
        }


    }
  
    public void refreshdata()
    {

      /*  con.Open();

        SqlCommand cmd = new SqlCommand("select * from tbl_employee", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        con.Close();
        GridView1.DataSource = dt;
        GridView1.DataBind();

        */

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
                //  enrolledStudent = new student_webService().GetEnrolledStudentOfCourse_New(courseKey, group);
                enrolledStudent = new student_webService().GetEnrolledStudentOfCourse(courseKey, group);
                totalCapacity = int.Parse(tmp[1]);
                gr.Cells[7].Text = enrolledStudent.ToString() + "/" + totalCapacity.ToString();

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
        else
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
                        ds.Tables["WEB_COURSE_OFFERING_TEMP"].Columns.Add("APPROVAL_TIME");

                        DataRow dr = ds.Tables["WEB_COURSE_OFFERING_TEMP"].NewRow();
                        dr["COURSEKEY"] = "" + courseKey;
                        dr["REGKEY"] = "" + sid + Session["sem"].ToString() + Session["year"].ToString();
                        dr["GGROUP"] = "" + group;
                        dr["CHOURS"] = "" + new admin_webService().get_latest_creditHours_ofA_course(courseCode);
                        dr["CTRL"] = "1";
                        //dr["TEACHER_COMMENTS"] = "";
                        //dr["STUDENT_COMMENTS"] = "";
                        dr["INSERTION_TIME"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));
                        dr["APPROVAL_TIME"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));
                        ds.Tables["WEB_COURSE_OFFERING_TEMP"].Rows.Add(dr);



                        string NewINSERTIONTIMETec = Convert.ToString(Convert.ToDateTime(DateTime.Now)); //newly course add by teacher  at readvising periord
                        string NewINSERTIONIDTec = Session["user"].ToString();



                        if (Convert.ToString(Session["modify"]) == "0")
                        {
                            if (new student_webService().New_InsertIntoDemo(ds, NewINSERTIONTIMETec, NewINSERTIONIDTec) == "1")
                            {
                                new student_webService().save_pre_offering(ds);
                                final_offering_Click(courseKey, group);
                            }


                        }
                        else
                            if (Convert.ToString(Session["modify"]) == "1")
                            {
                                if (check_schedule_alter(courseKey, group))
                                {
                                    // (courseKey, group)
                                    if (new student_webService().New_InsertIntoDemo(ds, NewINSERTIONTIMETec, NewINSERTIONIDTec) == "1")
                                    {
                                        new student_webService().Update_offering(ds);
                                        final_offering_Click(courseKey, group);
                                    }
                                }

                            }




                        sem = cmb_semester.SelectedValue.ToString();
                        year = txtYear.Text;
                        sid = txtSID.Text;

                        load_taken_courses(sid, sem, year);

                        btn_show_Click(sender, e);
                        //  load_available_courses();
                        //   load_temp_taken_courses();

                    }
                    /*else
                        if (Convert.ToString(Session["modify"]) == "1")
                        {
                            if (check_schedule_alter(courseKey, group))
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
                                ds.Tables["WEB_COURSE_OFFERING_TEMP"].Columns.Add("APPROVAL_TIME");

                                DataRow dr = ds.Tables["WEB_COURSE_OFFERING_TEMP"].NewRow();
                                dr["COURSEKEY"] = "" + courseKey;
                                dr["REGKEY"] = "" + sid + Session["sem"].ToString() + Session["year"].ToString();
                                dr["GGROUP"] = "" + group;
                                dr["CHOURS"] = "" + new admin_webService().get_latest_creditHours_ofA_course(courseCode);
                                dr["CTRL"] = "1";
                                dr["INSERTION_TIME"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));
                                dr["APPROVAL_TIME"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));
                                ds.Tables["WEB_COURSE_OFFERING_TEMP"].Rows.Add(dr);



                                string NewINSERTIONTIMETec = Convert.ToString(Convert.ToDateTime(DateTime.Now)); //newly course add by teacher  at readvising periord
                                string NewINSERTIONIDTec = Session["user"].ToString();



                                if (Convert.ToString(Session["modify"]) == "1")
                                {
                                    if (new student_webService().New_InsertIntoDemo(ds, NewINSERTIONTIMETec, NewINSERTIONIDTec) == "1")
                                    {
                                        new student_webService().Update_offering(ds);
                                        final_offering_Click(courseKey, group);
                                    }


                                }




                                sem = cmb_semester.SelectedValue.ToString();
                                year = txtYear.Text;
                                sid = txtSID.Text;

                                load_taken_courses(sid, sem, year);

                                btn_show_Click(sender, e);


                            }
                        }

                    */
                    
                }


            }
    }

    private void changeGrp(string courseKey, string group)
    {
        string courseCode = courseKey.Substring(5, courseKey.Length - 5);


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



        string NewINSERTIONTIMETec = Convert.ToString(Convert.ToDateTime(DateTime.Now)); //newly course add by teacher  at readvising periord
        string NewINSERTIONIDTec = Session["user"].ToString();




        if (Convert.ToString(Session["modify"]) == "1")
        {

            if (new student_webService().New_InsertIntoDemo(ds, NewINSERTIONTIMETec, NewINSERTIONIDTec) == "1")
            {
                new student_webService().Update_offering(ds);
                final_offering_Click(courseKey, group);
            }

        }


        sem = cmb_semester.SelectedValue.ToString();
        year = txtYear.Text;
        sid = txtSID.Text;

        load_taken_courses(sid, sem, year);
       

    }
    private bool check_prerequisite(string courseCode)
    {
        bool status = true;

        sid = txtSID.Text;

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

    private bool check_schedule_alter(string courseKey, string group)
    {
        sid = txtSID.Text;
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        ds.Merge(new student_webService().get_a_course_routine(courseKey, group));

        ds.Merge(new student_webService().get_all_preOffered_coursesNew(sid, Session["sem"].ToString(), Session["year"].ToString(), courseKey));

        foreach (DataRow drS in ds.Tables["course_schedule"].Rows) // schedule which is going to take
        {
            foreach (DataRow dr in ds.Tables["pree_offered"].Rows) //courses taken (pre)
            {
                if (dr["COURSEKEY"].ToString() == courseKey && Convert.ToString(Session["modify"]) == "0")
                {
                    lbl_message.Font.Bold = true;
                    lbl_message.Text = "Already you have taken this course";
                    return false;
                }
                /*  else
                  if (modify == "1")
                  {
                      changeGrp(courseKey, group);
                  }*/


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
                    lbl_message.Visible = true;

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


    private bool check_schedule(string courseKey, string group)
    {
        sid = txtSID.Text;
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        ds.Merge(new student_webService().get_a_course_routine(courseKey, group));

        ds.Merge(new student_webService().get_all_preOffered_courses(sid, Session["sem"].ToString(), Session["year"].ToString()));

        foreach (DataRow drS in ds.Tables["course_schedule"].Rows) // schedule which is going to take
        {
            foreach (DataRow dr in ds.Tables["pree_offered"].Rows) //courses taken (pre)
            {
                if (dr["COURSEKEY"].ToString() == courseKey && Convert.ToString(Session["modify"]) == "0")
                {
                    lbl_message.Font.Bold = true;
                    lbl_message.Text = "Already you have taken this course";
                    return false;
                }
              /*  else
                if (modify == "1")
                {
                    changeGrp(courseKey, group);
                }*/
                    

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
                    lbl_message.Visible = true;

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
    private void load_temp_taken_courses()
    {
        DataSet ds = new DataSet();

        sid = txtSID.Text;
        sem =   cmb_semester.SelectedValue.ToString();
        year =  txtYear.Text;
        ds.Merge(new student_webService().get_all_preOffered_coursesFull(sid, sem, year));

        ds.Tables["pree_offered"].Columns.Add("cName");
        ds.Tables["pree_offered"].Columns.Add("available_seat");
        string CAPACITY = "", Section = "", TOTAL_STUDENT = "", TOTAL_CAPACITY = "", totalCourse = "";
        double ttCtrdit = 0;

        if (ds.Tables["pree_offered"].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables["pree_offered"].Rows)
            {
                dr["cName"] = obj_staff.get_a_courseName_onKey(dr["COURSEKEY"].ToString());
                ttCtrdit += Convert.ToDouble("0" + dr["CHOURS"].ToString());
                totalCourse += Convert.ToString(dr["course"]) + ", ";
            }
            lbl_Approval.Text = totalCourse + " course(s) are approved.";

            Session["StudentTakenCourses"] = ds;
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



       

    }

    protected void final_offering_Click(string courseKey, string group)
    {
        int Flag = 0;

        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_all_preOffered_coursesFull(sid, sem, year));
        DataSet ApprovalCourse_ds = new DataSet();
        ds.Tables["pree_offered"].Columns.Add("cName");

        double ttCtrdit = 0;

        ds.Tables.Add("OFFERERINGANDGRADE");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("COURSEKEY");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GGRADE");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GTYPE");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("MARKS");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("REGKEY");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GGROUP");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("CHOURS");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GPOINT");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("GGRADE2");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("COURSE_INSERTIONDATE");

        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("CREATED_DATE");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("CREATED_BY");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("LAST_MODIFIED");
        ds.Tables["OFFERERINGANDGRADE"].Columns.Add("LAST_MODIFIED_BY");



        DataRow dr = ds.Tables["OFFERERINGANDGRADE"].NewRow();
        dr["COURSEKEY"] = "" + courseKey;
        dr["GGRADE"] = "I";
        dr["GTYPE"] = "";
        dr["MARKS"] = "";
        dr["REGKEY"] = "" + sid + Session["sem"].ToString() + Session["year"].ToString();
        dr["GGROUP"] = "" + group;
        string courseCode = courseKey.Substring(5, courseKey.Length - 5);
        dr["CHOURS"] = "" + new admin_webService().get_latest_creditHours_ofA_course(courseCode);
        dr["GPOINT"] = "0";
        dr["GGRADE2"] = "I";
        dr["COURSE_INSERTIONDATE"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));

        dr["CREATED_BY"] = Session["ctrl_admin_Id"].ToString();
        dr["CREATED_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);


        dr["LAST_MODIFIED_BY"] = Session["ctrl_admin_Id"].ToString();
       // dr["LAST_MODIFIED"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));
        dr["LAST_MODIFIED"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);
        ds.Tables["OFFERERINGANDGRADE"].Rows.Add(dr);


        string NEWAPPROVETIME = Convert.ToString(Convert.ToDateTime(DateTime.Now));//student requested course approve by teacher at readvising periord
        string NEWAPPROVEID = Session["user"].ToString();

        if (new student_webService().Approve_IntoDemo(ds, NEWAPPROVETIME, NEWAPPROVEID) >= 1)
        {
            string str_msg = ""; int count = 0;
            if (Convert.ToString(Session["modify"]) == "0")
            {
                count = new staff_webService().insert_final_advisingCourse(ds, ref str_msg);
            }
            else if (Convert.ToString(Session["modify"]) == "1")
            {
                count = new staff_webService().Update_final_advising(ds, ref str_msg);
            }

            //   ApprovalCourse_ds.Merge(new student_webService().get_all_FinalAdvising_coursesFull(sid, sem, year));
            if (count > 0 && Convert.ToString(Session["modify"]) == "1")
            {
                Session["modify"] = "0";
                cmb_course.Enabled = true;
                lbl_message.Font.Bold = true;
                lbl_message.Text = "Course Group has been Updated";
                           
                return;
            }
            else
            if (count > 0)
                return;
        }


    }


    protected void btn_deleteCourse_Click(object sender, EventArgs e)
    {
        string courseKey = "";
        string regKey = "";
        string ReDROPTIME = Convert.ToString(Convert.ToDateTime(DateTime.Now));// DateTime.Now.ToString("dd/mm/yyyy");
        string ReDROPID = Session["user"].ToString();
        sid = txtSID.Text;
        foreach (GridViewRow gr in GridView_taken_list.Rows)
        {
            if (((CheckBox)(gr.FindControl("CheckBox1"))).Checked == true)
            {
                courseKey = ((Label)(gr.FindControl("COURSEKEY"))).Text;
                regKey = ((Label)(gr.FindControl("REGKEY"))).Text;
                if (new student_webService().Deleted_DataTecIntoDemo(courseKey, regKey, ReDROPID, ReDROPTIME) == "1") //student requested course delete by teacher  at readvising periord
                {
                    obj_staff.delete_reAdvising_courses(courseKey, regKey);
                }
            }
        }

        load_taken_courses(sid, cmb_semester.SelectedValue.ToString(), txtYear.Text);
        btn_add.Visible = false;
        btn_show_Click(sender, e);
      //  load_temp_taken_courses();
        lbl_Error.Visible = false;
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        sid = txtSID.Text;
        course = cmb_course.SelectedValue.ToString();
        ds.Merge(new admin_webService().get_allocated_teacher(course));
        ds.Tables["WEB_VIEW_COURSE_TEACHER"].Columns.Add("available_seat");


        foreach (DataRow dr in ds.Tables["WEB_VIEW_COURSE_TEACHER"].Rows)
        {
            //dr["available_seat"] = "" + dr["TEMP_TOTAL_STUDENT"].ToString() + "/" + dr["TOTAL_CAPACITY"].ToString();
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
            if (!String.IsNullOrEmpty(gr.Cells[7].Text))
            {
                if (Convert.ToInt32("0" + gr.Cells[7].Text.Split('/')[0].ToString()) >= Convert.ToInt32("0" + gr.Cells[7].Text.Split('/')[1].ToString()))
                    gr.Enabled = false;
            }
        }

        load_taken_courses(sid, cmb_semester.SelectedValue.ToString(), txtYear.Text);
       // load_temp_taken_courses();
    }
    protected void btn_prerequisit_Click(object sender, EventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        pnlCourseTaken.Visible = false;
        pnlCourseOffer.Visible = false;
    }

    protected void btnModifyCourse_Click(object sender, EventArgs e)
    {
        string courseKey = "";
        string regKey = "";
        string ReDROPTIME = Convert.ToString(Convert.ToDateTime(DateTime.Now));// DateTime.Now.ToString("dd/mm/yyyy");
        string ReDROPID = Session["user"].ToString();
        sid = txtSID.Text;
        foreach (GridViewRow gr in GridView_taken_list.Rows)
        {
            if (((CheckBox)(gr.FindControl("CheckBox1"))).Checked == true)
            {
                courseKey = ((Label)(gr.FindControl("COURSEKEY"))).Text;
                regKey = ((Label)(gr.FindControl("REGKEY"))).Text;
                if (new student_webService().Deleted_DataTecIntoDemo(courseKey, regKey, ReDROPID, ReDROPTIME) == "1") //student requested course delete by teacher  at readvising periord
                {
                    Session["modify"] = 1;
                  //  modify = "1";
                    cmb_course.SelectedValue = courseKey;
                    cmb_course.Enabled = false;
                    btn_show_Click(sender, e);
                   // obj_staff.delete_reAdvising_courses(courseKey, regKey);
                }
            }
        }

      //  load_taken_courses(sid, cmb_semester.SelectedValue.ToString(), txtYear.Text);

      //  btn_show_Click(sender, e);

       
    }
}