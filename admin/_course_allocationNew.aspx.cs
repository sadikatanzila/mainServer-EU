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

public partial class admin_course_allocationNew : System.Web.UI.Page
{
    admin_webService obj_adminWs = new admin_webService();




    string code = "";

    string teacher_id = "";
    string group = "";
    string room_id1 = "";
    string room_id2 = "";
    string room_id3 = "";
    string room_id4 = "";
    string faculty_id = "";

    string date1 = "";
    string date2 = "";
    string date3 = "";
    string date4 = "";

    string hour1 = "";
    string hour2 = "";
    string hour3 = "";
    string hour4 = "";

    string min1 = "";
    string min2 = "";
    string min3 = "";
    string min4 = "";

    string time1 = "";
    string time2 = "";
    string time3 = "";
    string time4 = "";

    string user = "";

    string slot_dtlid1 = "";
    string slot_dtlid2 = "";
    string slot_dtlid3 = "";
    string slot_dtlid4 = "";

   
 

    //string dep = "";
    //string advisor = "";

    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (HttpContext.Current.Session["ctrl_admin_Id"] != null && !String.IsNullOrEmpty(HttpContext.Current.Session["ctrl_admin_Id"].ToString()))
        {
            if (Session["ROLE_ID"].ToString() !="1"
                && !HttpContext.Current.Request.Url.AbsoluteUri.Contains("_login.aspx"))
            {
                 btn_delete.Visible = false;
            }
            else
            {
                btn_delete.Visible = true;
            }

        }

        

        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                user = Session["ctrl_admin_Id"].ToString();
            }
            else
                Response.Redirect("../employee/_login.aspx");

            /*if (!string.IsNullOrEmpty(Convert.ToString(Session["COURSECODE"])))
                if (Convert.ToString(Session["CourseFilup"]) != "full")//if (Request.QueryString["ids"] != null)
                    code = Convert.ToString(Session["COURSECODE"]);
                else
                    code = Convert.ToString(Session["code"]);*/

            if (string.IsNullOrEmpty(Convert.ToString(Session["COURSECODE"])))//if (Request.QueryString["ids"] != null)
                Response.Redirect("../admin/_semester_course_list.aspx");
            else
                code = Convert.ToString(Session["COURSECODE"]);
               
           // else
            //    Response.Redirect("../_login.aspx");
          
        }
        catch (Exception exp) {
            Response.Redirect("../admin/_semester_course_list.aspx");

          /*  if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                Response.Redirect("../admin/_semester_course_list.aspx");
            }
            else
                Response.Redirect("../employee/_login.aspx");*/
        
        
        }


        lbl_message.Text = "";
        lbl_teacher_message.Text = "";
        btn_submit.Attributes.Add("onClick", " return chech_valid(); ");


        if (IsPostBack)
        {
            

            load_initial();
          

        }
        else
        {
            load_courses();
            load_allocated_teachers();

            load_Faculty();
            load_group();
          


            load_teacher();


            load_Room_No1();
            load_Room_No2();
            load_Room_No3();

            //load_Room_No4();



            //load_Slot3();
            //load_Slot4();
        }

        lblHRT.Visible = false;
        lblFacultyID.Visible = false;

    

    }

    private void load_Room_No1()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_roomno());

        DataRow dr = ds.Tables["roomlist"].NewRow();
        dr["ROOM_NAME"] = "Select";
        dr["C_ROOM_ID"] = "Select";
        ds.Tables["roomlist"].Rows.Add(dr);

        cmb_Room1.DataSource = ds.Tables["roomlist"];
        cmb_Room1.DataTextField = "ROOM_NAME";
        cmb_Room1.DataValueField = "C_ROOM_ID";
        cmb_Room1.DataBind();

        if (room_id1 == "")
            cmb_Room1.SelectedValue = "Select";
        else
            cmb_Room1.SelectedValue = room_id1;

    }
    private void load_Room_No2()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_roomno());

        DataRow dr = ds.Tables["roomlist"].NewRow();
        dr["ROOM_NAME"] = "Select";
        dr["C_ROOM_ID"] = "Select";
        ds.Tables["roomlist"].Rows.Add(dr);

        cmb_Room2.DataSource = ds.Tables["roomlist"];
        cmb_Room2.DataTextField = "ROOM_NAME";
        cmb_Room2.DataValueField = "C_ROOM_ID";
        cmb_Room2.DataBind();

        if (room_id2 == "")
            cmb_Room2.SelectedValue = "Select";
        else
            cmb_Room2.SelectedValue = room_id2;
    }

    private void load_Room_No3()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_roomno());

        DataRow dr = ds.Tables["roomlist"].NewRow();
        dr["ROOM_NAME"] = "Select";
        dr["C_ROOM_ID"] = "Select";
        ds.Tables["roomlist"].Rows.Add(dr);

        cmb_Room3.DataSource = ds.Tables["roomlist"];
        cmb_Room3.DataTextField = "ROOM_NAME";
        cmb_Room3.DataValueField = "C_ROOM_ID";
        cmb_Room3.DataBind();

        if (room_id3 == "")
            cmb_Room3.SelectedValue = "Select";
        else
            cmb_Room3.SelectedValue = room_id3;

    }







    private void load_Faculty()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_department());

        DataRow dr = ds.Tables["deptlist"].NewRow();
        dr["COLLEGENAME"] = "Select";
        dr["COLLEGECODE"] = "Select";
        ds.Tables["deptlist"].Rows.Add(dr);

        cmb_Faculty.DataSource = ds.Tables["deptlist"];
        cmb_Faculty.DataTextField = "COLLEGENAME";
        cmb_Faculty.DataValueField = "COLLEGECODE";
        //cmb_Faculty.SelectedValue = null;
        cmb_Faculty.DataBind();


        if (faculty_id == "")
        {
            cmb_Faculty.SelectedValue = "Select";
            // cmb_teacher.SelectedValue = "Select";
        }
        else
            cmb_Faculty.SelectedValue = faculty_id;

    }



    private void reset_information()
    {
        cmb_Room1.SelectedValue = "Select";
        cmb_Room2.SelectedValue = "Select";
        cmb_Room3.SelectedValue = "Select";

        cmb_date1.SelectedValue = "Select";
        cmb_date2.SelectedValue = "Select";
        cmb_date3.SelectedValue = "Select";

        cmb_Faculty.SelectedValue = "Select";
        cmb_teacher.SelectedValue = "Select";
		
		if (Convert.ToInt32(Session["year"].ToString() + Session["sem"].ToString()) <= 20183)
        {
            cmb_group.DataSource = new cls_tools().grpPrev;
            cmb_group.DataBind();
            cmb_group.SelectedValue = group;
        }
        else
        {
            if (lbl_course_code.Text == "BUS 498" || lbl_course_code.Text == "BUS 509")
            {
                cmb_group.SelectedValue = "FIN";
            }
            else
            {
                cmb_group.SelectedValue = "1";
            }
        }
        //cmb_group.SelectedValue = "1";
        txt_Student.Text = "";
        lblFacultyID.Text = "";

        Session["Clsday1"] = "";
        Session["Clsday2"] = "";
        Session["Clsday3"] = "";
        // cmb_Slot1.SelectedValue = "";
        //  cmb_Slot2.SelectedValue = "";
        // cmb_Slot3.SelectedValue = "";


    }


    private void load_courses()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_offeredCourses(Session["year"].ToString(), Session["sem"].ToString()));



        foreach (DataRow dr in ds.Tables["CourseList"].Rows)
        {
            if (code == dr["COURSECODE"].ToString())
            {
                lbl_course_code.Text = dr["COURSECODE"].ToString();
                lbl_course_name.Text = dr["CNAME"].ToString();
                // lbl_credit_hours.Text = dr["CHOURS"].ToString();
                lbl_semester.Text = "" + new cls_tools().get_word_semester(Session["sem"].ToString()) + " " + Session["year"].ToString();
                //lbl_section.Text = dr["SECTION"].ToString();
                //lbl_total_student.Text = dr["TOTAL_STUDENT"].ToString();
                Session["CourseFilup"] = "full";
                Session["code"] = code;
                break;
            }
        }
    }

    private void load_allocated_teachers()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allcated_teacher_ofa_course(Session["sem"].ToString() + Session["year"].ToString() + code));

        GridView_courseList.DataSource = ds;
        GridView_courseList.DataMember = "WEB_VIEW_COURSE_TEACHER";
        GridView_courseList.DataBind();

        if (ds.Tables["WEB_VIEW_COURSE_TEACHER"].Rows.Count == 0)
        {
            lbl_teacher_message.Text = "" + new cls_message().getMessage(7);
            btn_delete.Visible = false;
            btn_bodify.Visible = false;
        }
        else
        {
            //  btn_delete.Visible = true;
            btn_bodify.Visible = true;
        }
    }

    private void load_initial()
    {
        slot_dtlid1 = cmb_Slot1.SelectedValue.ToString();
        slot_dtlid2 = cmb_Slot2.SelectedValue.ToString();
        slot_dtlid3 = cmb_Slot3.SelectedValue.ToString();


        faculty_id = cmb_Faculty.SelectedValue.ToString();

        teacher_id = cmb_teacher.SelectedValue.ToString();
        group = cmb_group.SelectedValue.ToString();




    }


    protected void Test_Click(object sender, EventArgs e)
    {
        //Session["COURSE_SEM_YEAR"] = (Session["sem"].ToString() + Session["year"].ToString()).ToString();

        //string webUrl = "../admin/_RoomDistribution.aspx";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('" + webUrl + "','_blank','width=850,height=450,resizable=yes,scrollbars = yes');", true);



        // OpenNewWindow("../admin/_addRoomNo.aspx");
    }

    //public void OpenNewWindow(string url)
    //{
    //    ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));
    //}


    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(cmb_Faculty.SelectedValue) == "Select" 
            || Convert.ToString(cmb_teacher.SelectedValue) == "Select" || txt_Student.Text == "")
        {
            lbl_message.Text = "Fail to save, Fill-up the necessary field completely";
        }
        else
        {


            string IsAlter1 = "";
            string IsAlter2 = "";
            string IsAlter3 = "";


            string ID = "";


            string day1 = "";
            string time1 = "";
            string room1 = "";

            string day2 = "";
            string time2 = "";
            string room2 = "";

            string day3 = "";
            string time3 = "";
            string room3 = "";

            string year = Session["year"].ToString();
            string semester = Session["sem"].ToString();



            if (cmb_date1.SelectedItem.ToString() != "Select")
                day1 = cmb_date1.SelectedItem.ToString();
            else
                day1 = null;

            if (cmb_Slot1.SelectedValue.ToString() != "")
                time1 = cmb_Slot1.SelectedValue.ToString();
            else
                time1 = null;

            if (cmb_Room1.SelectedValue.ToString() != "Select")
                room1 = cmb_Room1.SelectedValue.ToString();
            else
                room1 = null;





            if (cmb_date2.SelectedItem.ToString() != "Select")
                day2 = cmb_date2.SelectedItem.ToString();
            else
                day2 = null;

            if (cmb_Slot2.SelectedValue.ToString() != "")
                time2 = cmb_Slot2.SelectedValue.ToString();
            else
                time2 = null;

            if (cmb_Room2.SelectedValue.ToString() != "Select")
                room2 = cmb_Room2.SelectedValue.ToString();
            else
                room2 = null;






            if (cmb_date3.SelectedItem.ToString() != "Select")
                day3 = cmb_date3.SelectedItem.ToString();
            else
                day3 = null;

            if (cmb_Slot3.SelectedValue.ToString() != "")
                time3 = cmb_Slot3.SelectedValue.ToString();
            else
                time3 = null;

            if (cmb_Room3.SelectedValue.ToString() != "Select")
                room3 = cmb_Room3.SelectedValue.ToString();
            else
                room3 = null;


            if (btn_submit.Text == "Submit")
            {
                Session["ID"] = "";
                ID = "";
            }
            else
            {
                ID = Convert.ToString(Session["ID"]);
            }



            DataSet Chk_ds1 = new DataSet();
            DataSet Chk_ds2 = new DataSet();
            DataSet Chk_ds3 = new DataSet();


            //---------------check alter course

            DataSet Chk_AlterCourse = new DataSet();
            string course_Name = lbl_course_name.Text;
            string Course_Code = lbl_course_code.Text;


            //find out CourseCAT
            DataSet CourseCAT = new DataSet();
            CourseCAT.Merge(new admin_webService().match_CourseCAT(Course_Code));
            string CATAGORY = "";
            foreach (DataRow drCAT in CourseCAT.Tables["CourseCATList"].Rows)
            {
                CATAGORY = drCAT["COURSECAT"].ToString();
            }

            if (CATAGORY != "" || CATAGORY != null)
            {
                Chk_AlterCourse.Merge(new admin_webService().match_course_Name(CATAGORY, Course_Code));
            }

           

            //---------------for with out alter course
            if (Chk_AlterCourse.Tables["Courselist"].Rows.Count == 0)
            {
                if (ID == "")
                {
                    if (day1 != "Select" && time1 != "" && room1 != "Select")
                    {
                        Chk_ds1.Merge(new admin_webService().match_RoutineList(time1));
                        if (Chk_ds1.Tables["RtnList1"].Rows.Count != 0)
                        {
                            foreach (DataRow dr1 in Chk_ds1.Tables["RtnList1"].Rows)
                            {
                                string section = dr1["SECTION"].ToString();
                                string course = dr1["COURSE"].ToString();
                                string CourseName = dr1["coursename"].ToString();
                                string teacher = dr1["Teacher_Name"].ToString();
                                string room = dr1["ROOM"].ToString();

                                if (course_Name != CourseName)
                                {
                                    lbl_message.Text = "Room " + room + "  Comfilct with <br/>Faculty Name :" + teacher + " <br/>Course : " + course + " <br/>Group : " + section + "";
                                    break;
                                }
                                else if (Course_Code == course && course_Name == CourseName)
                                {
                                    lbl_message.Text = "Room " + cmb_Room1.SelectedItem.ToString() + ", on " + cmb_date1.SelectedItem.ToString() + " day, at " + cmb_Slot1.SelectedItem.ToString() + " already have booked";
                                    break;
                                }
                            }

                        }

                    }


                    if (day2 != "Select" && time2 != "" && room2 != "Select")
                    {
                        Chk_ds2.Merge(new admin_webService().match_RoutineList(time2));
                        if (Chk_ds2.Tables["RtnList1"].Rows.Count != 0)
                        {
                            foreach (DataRow dr2 in Chk_ds2.Tables["RtnList1"].Rows)
                            {
                                string section = dr2["SECTION"].ToString();
                                string course = dr2["COURSE"].ToString();
                                string CourseName = dr2["coursename"].ToString();
                                string teacher = dr2["Teacher_Name"].ToString();
                                string room = dr2["ROOM"].ToString();

                                if (CourseName != course_Name)
                                {
                                    lbl_message.Text = "Room " + room + "  Comfilct with <br/>Faculty Name :" + teacher + " <br/>Course : " + course + " <br/>Group : " + section + "";
                                    break;
                                }
                                else if (Course_Code == course && course_Name == CourseName)
                                {
                                    lbl_message.Text = "Room " + cmb_Room2.SelectedItem.ToString() + ", on " + cmb_date2.SelectedItem.ToString() + " day, at " + cmb_Slot2.SelectedItem.ToString() + " already have booked";
                                    break;
                                }
                            }

                        }

                    }


                    if (day3 != "Select" && time3 != "" && room3 != "Select")
                    {
                        Chk_ds3.Merge(new admin_webService().match_RoutineList(time3));
                        if (Chk_ds3.Tables["RtnList1"].Rows.Count != 0)
                        {
                            foreach (DataRow dr3 in Chk_ds3.Tables["RtnList1"].Rows)
                            {
                                string section = dr3["SECTION"].ToString();
                                string course = dr3["COURSE"].ToString();
                                string CourseName = dr3["coursename"].ToString();
                                string teacher = dr3["Teacher_Name"].ToString();
                                string room = dr3["ROOM"].ToString();

                                if (CourseName != course_Name)
                                {
                                    lbl_message.Text = "Room " + room + " Comfilct with <br/>Faculty Name :" + teacher + " <br/>Course : " + course + " <br/>Group : " + section + "";
                                    break;
                                }

                                else if (Course_Code == course && course_Name == CourseName)
                                {
                                    lbl_message.Text = "Room " + cmb_Room3.SelectedItem.ToString() + ", on " + cmb_date3.SelectedItem.ToString() + " day, at " + cmb_Slot3.SelectedItem.ToString() + " already have booked";
                                    break;
                                }

                            }

                        }

                    }

                }
                else
                {
                    if (day1 != "Select" && time1 != "" && room1 != "Select")
                    {
                        Chk_ds1.Merge(new admin_webService().match_RoutineList(time1, ID));
                        if (Chk_ds1.Tables["RtnList1"].Rows.Count != 0)
                        {
                            foreach (DataRow dr1 in Chk_ds1.Tables["RtnList1"].Rows)
                            {
                                string section = dr1["SECTION"].ToString();
                                string course = dr1["COURSE"].ToString();
                                string teacher = dr1["Teacher_Name"].ToString();
                                string room = dr1["ROOM"].ToString();
                                Session["C_ROUTINE_ID1"] = dr1["C_ROUTINE_ID"].ToString();
                                lbl_message.Text = "Room " + room + "  Comfilct with <br/>Faculty Name :" + teacher + " <br/>Course : " + course + " <br/>Group : " + section + "";
                                break;
                            }

                        }

                    }


                    if (day2 != "Select" && time2 != "" && room2 != "Select")
                    {
                        Chk_ds2.Merge(new admin_webService().match_RoutineList(time2, ID));
                        if (Chk_ds2.Tables["RtnList1"].Rows.Count != 0)
                        {
                            foreach (DataRow dr2 in Chk_ds2.Tables["RtnList1"].Rows)
                            {
                                string section = dr2["SECTION"].ToString();
                                string course = dr2["COURSE"].ToString();
                                string teacher = dr2["Teacher_Name"].ToString();
                                string room = dr2["ROOM"].ToString();

                                Session["C_ROUTINE_ID2"] = dr2["C_ROUTINE_ID"].ToString();

                                lbl_message.Text = "Room " + room + "  Comfilct with <br/>Faculty Name :" + teacher + " <br/>Course : " + course + " <br/>Group : " + section + "";
                                break;
                            }

                        }

                    }


                    if (day3 != "Select" && time3 != "" && room3 != "Select")
                    {
                        Chk_ds3.Merge(new admin_webService().match_RoutineList(time3, ID));
                        if (Chk_ds3.Tables["RtnList1"].Rows.Count != 0)
                        {
                            foreach (DataRow dr3 in Chk_ds3.Tables["RtnList1"].Rows)
                            {
                                string section = dr3["SECTION"].ToString();
                                string course = dr3["COURSE"].ToString();
                                string teacher = dr3["Teacher_Name"].ToString();
                                string room = dr3["ROOM"].ToString();

                                Session["C_ROUTINE_ID3"] = dr3["C_ROUTINE_ID"].ToString();

                                lbl_message.Text = "Room " + room + "  Comfilct with <br/>Faculty Name :" + teacher + " <br/>Course : " + course + " <br/>Group : " + section + "";
                                break;
                            }

                        }

                    }

                }

                if (Chk_ds1.Tables["RtnList1"].Rows.Count == 0 && Chk_ds2.Tables["RtnList1"].Rows.Count == 0
                    && Chk_ds3.Tables["RtnList1"].Rows.Count == 0)
                {

                    allocate_teacher();
                    load_allocated_teachers();
                }
            }






                //-----------------for alter courses------------------------------
            else
            {
                if (Chk_AlterCourse.Tables["Courselist"].Rows.Count > 0)
                {
                    foreach (DataRow dr in Chk_AlterCourse.Tables["Courselist"].Rows)
                    {
                        string AlternateCourse = dr["COURSENAME"].ToString();
                        string AlternateCOURSECAT = dr["COURSECAT"].ToString();
                        Session["AlternateCourse"] = AlternateCourse;
                        Session["AlternateCOURSECAT"] = AlternateCOURSECAT;
                    }





                    if (ID == "")
                    {
                        if (day1 != "Select" && time1 != "" && room1 != "Select")
                        {
                            Chk_ds1.Merge(new admin_webService().match_RoutineList(time1));
                            if (Chk_ds1.Tables["RtnList1"].Rows.Count != 0)
                            {
                                foreach (DataRow dr1 in Chk_ds1.Tables["RtnList1"].Rows)
                                {
                                    string section = dr1["SECTION"].ToString();
                                    string course = dr1["COURSE"].ToString();
                                    string ComflictCourse = dr1["COURSENAME"].ToString();
                                    string teacher = dr1["Teacher_Name"].ToString();
                                    string room = dr1["ROOM"].ToString();
                                    string ComflictCourseCAT = dr1["coursecat"].ToString();

                                    if (Course_Code == course && course_Name == ComflictCourse)
                                    {
                                        lbl_message.Text = "Room " + cmb_Room1.SelectedItem.ToString() + ", on " + cmb_date1.SelectedItem.ToString() + " day, at " + cmb_Slot1.SelectedItem.ToString() + " already have booked";
                                        break;
                                    }
                                    else
                                        if (Convert.ToString(Session["AlternateCOURSECAT"]) != ComflictCourseCAT )//Convert.ToString(Session["AlternateCourse"]) != ComflictCourse
                                            
                                        {
                                            lbl_message.Text = "Room " + room + "  Comfilct with <br/>Faculty Name :" + teacher + " <br/>Course : " + course + " <br/>Group : " + section + "";
                                            break;
                                        }

                                        else
                                        {
                                            // Chk_ds1.Tables["RtnList1"].Rows.Count = '0';
                                            IsAlter1 = "1";
                                        }
                                }

                            }
                            else
                            {
                                IsAlter1 = "1";
                            }



                            if (day2 != "Select" && time2 != "" && room2 != "Select")
                            {
                                Chk_ds2.Merge(new admin_webService().match_RoutineList(time2));
                                if (Chk_ds2.Tables["RtnList1"].Rows.Count != 0)
                                {
                                    foreach (DataRow dr2 in Chk_ds2.Tables["RtnList1"].Rows)
                                    {
                                        string section = dr2["SECTION"].ToString();
                                        string course = dr2["COURSE"].ToString();
                                        string ComflictCourse = dr2["COURSENAME"].ToString();
                                        string teacher = dr2["Teacher_Name"].ToString();
                                        string room = dr2["ROOM"].ToString();
                                        string ComflictCourseCAT = dr2["coursecat"].ToString();

                                        if (Course_Code == course && course_Name == ComflictCourse)
                                        {
                                            lbl_message.Text = "Room " + cmb_Room2.SelectedItem.ToString() + ", on " + cmb_date2.SelectedItem.ToString() + " day, at " + cmb_Slot2.SelectedItem.ToString() + " already have booked";
                                            break;
                                        }
                                        else
                                            if (Convert.ToString(Session["AlternateCOURSECAT"]) != ComflictCourseCAT)//Convert.ToString(Session["AlternateCourse"]) != ComflictCourse
                                
                                            {
                                                lbl_message.Text = "Room " + room + "  Comfilct with <br/>Faculty Name :" + teacher + " <br/>Course : " + course + " <br/>Group : " + section + "";
                                                break;
                                            }
                                            else
                                            {
                                                // Chk_ds1.Tables["RtnList1"].Rows.Count = '0';
                                                IsAlter2 = "1";
                                            }
                                    }

                                }
                                else
                                {
                                    IsAlter2 = "1";
                                }

                            }


                            if (day3 != "Select" && time3 != "" && room3 != "Select")
                            {
                                Chk_ds3.Merge(new admin_webService().match_RoutineList(time3));
                                if (Chk_ds3.Tables["RtnList1"].Rows.Count != 0)
                                {
                                    foreach (DataRow dr3 in Chk_ds3.Tables["RtnList1"].Rows)
                                    {
                                        string section = dr3["SECTION"].ToString();
                                        string course = dr3["COURSE"].ToString();
                                        string ComflictCourse = dr3["COURSENAME"].ToString();
                                        string teacher = dr3["Teacher_Name"].ToString();
                                        string room = dr3["ROOM"].ToString();
                                        string ComflictCourseCAT = dr3["coursecat"].ToString();

                                        if (Course_Code == course && course_Name == ComflictCourse)
                                        {
                                            lbl_message.Text = "Room " + cmb_Room3.SelectedItem.ToString() + ", on " + cmb_date3.SelectedItem.ToString() + " day, at " + cmb_Slot3.SelectedItem.ToString() + " already have booked";
                                            break;
                                        }
                                        else
                                            if (Convert.ToString(Session["AlternateCOURSECAT"]) != ComflictCourseCAT)
                                            {
                                                lbl_message.Text = "Room " + room + "  Comfilct with <br/>Faculty Name :" + teacher + " <br/>Course : " + course + " <br/>Group : " + section + "";
                                                break;
                                            }
                                            else
                                            {
                                                // Chk_ds1.Tables["RtnList1"].Rows.Count = '0';
                                                IsAlter3 = "1";
                                            }
                                    }

                                }
                                else
                                {
                                    IsAlter3 = "1";
                                }

                            }
                        }
                    }
                    else
                    {

                        //-----------------check comflict with own



                        if (day1 != "Select" && time1 != "" && room1 != "Select")
                        {
                            Chk_ds1.Merge(new admin_webService().match_RoutineList(time1, ID));
                            if (Chk_ds1.Tables["RtnList1"].Rows.Count != 0)
                            {
                                foreach (DataRow dr1 in Chk_ds1.Tables["RtnList1"].Rows)
                                {
                                    string section = dr1["SECTION"].ToString();
                                    string course = dr1["COURSE"].ToString();
                                    string ComflictCourse = dr1["COURSENAME"].ToString();
                                    string teacher = dr1["Teacher_Name"].ToString();
                                    string room = dr1["ROOM"].ToString();
                                    string ComflictCourseCAT = dr1["coursecat"].ToString();
                                    Session["C_ROUTINE_ID1"] = dr1["C_ROUTINE_ID"].ToString();



                                    if (Convert.ToString(Session["AlternateCOURSECAT"]) != ComflictCourseCAT)
                                    {
                                        lbl_message.Text = "Room " + room + "  Comfilct with <br/>Faculty Name :" + teacher + " <br/>Course : " + course + " <br/>Group : " + section + "";
                                        break;
                                    }
                                    else if (Course_Code == course && CATAGORY == ComflictCourseCAT)
                                    {
                                        lbl_message.Text = "Room " + cmb_Room1.SelectedItem.ToString() + ", on " + cmb_date1.SelectedItem.ToString() + " day, at " + cmb_Slot1.SelectedItem.ToString() + " already have booked";
                                        break;
                                    }
                                    else
                                    {
                                        // Chk_ds1.Tables["RtnList1"].Rows.Count = '0';
                                        IsAlter1 = "1";
                                    }
                                }

                            }
                            else
                            {
                                IsAlter1 = "1";
                            }

                        }


                        if (day2 != "Select" && time2 != "" && room2 != "Select")
                        {
                            Chk_ds2.Merge(new admin_webService().match_RoutineList(time2, ID));
                            if (Chk_ds2.Tables["RtnList1"].Rows.Count != 0)
                            {
                                foreach (DataRow dr2 in Chk_ds2.Tables["RtnList1"].Rows)
                                {
                                    string section = dr2["SECTION"].ToString();
                                    string course = dr2["COURSE"].ToString();
                                    string ComflictCourse = dr2["COURSENAME"].ToString();
                                    string teacher = dr2["Teacher_Name"].ToString();
                                    string room = dr2["ROOM"].ToString();
                                    string ComflictCourseCAT = dr2["coursecat"].ToString();

                                    Session["C_ROUTINE_ID2"] = dr2["C_ROUTINE_ID"].ToString();

                                    if (Convert.ToString(Session["AlternateCOURSECAT"]) != ComflictCourseCAT)
                                    {
                                        lbl_message.Text = "Room " + room + "  Comfilct with <br/>Faculty Name :" + teacher + " <br/>Course : " + course + " <br/>Group : " + section + "";
                                        break;
                                    }
                                    else if (Course_Code == course && CATAGORY == ComflictCourseCAT)
                                    {
                                        lbl_message.Text = "Room " + cmb_Room2.SelectedItem.ToString() + ", on " + cmb_date2.SelectedItem.ToString() + " day, at " + cmb_Slot2.SelectedItem.ToString() + " already have booked";
                                        break;
                                    }
                                    else
                                    {
                                        // Chk_ds1.Tables["RtnList1"].Rows.Count = '0';
                                        IsAlter2 = "1";
                                    }
                                }

                            }
                            else
                            {
                                IsAlter2 = "1";
                            }

                        }


                        if (day3 != "Select" && time3 != "" && room3 != "Select")
                        {
                            Chk_ds3.Merge(new admin_webService().match_RoutineList(time3, ID));
                            if (Chk_ds3.Tables["RtnList1"].Rows.Count != 0)
                            {
                                foreach (DataRow dr3 in Chk_ds3.Tables["RtnList1"].Rows)
                                {
                                    string section = dr3["SECTION"].ToString();
                                    string course = dr3["COURSE"].ToString();
                                    string ComflictCourse = dr3["COURSENAME"].ToString();
                                    string teacher = dr3["Teacher_Name"].ToString();
                                    string room = dr3["ROOM"].ToString();
                                    string ComflictCourseCAT = dr3["coursecat"].ToString();

                                    Session["C_ROUTINE_ID3"] = dr3["C_ROUTINE_ID"].ToString();

                                    if ( Convert.ToString(Session["AlternateCOURSECAT"]) != ComflictCourseCAT)
                                    {
                                        lbl_message.Text = "Room " + room + "  Comfilct with <br/>Faculty Name :" + teacher + " <br/>Course : " + course + " <br/>Group : " + section + "";
                                        break;
                                    }
                                    else if (Course_Code == course  && CATAGORY == ComflictCourseCAT)
                                    {
                                        lbl_message.Text = "Room " + cmb_Room3.SelectedItem.ToString() + ", on " + cmb_date3.SelectedItem.ToString() + " day, at " + cmb_Slot3.SelectedItem.ToString() + " already have booked";
                                        break;
                                    }
                                    else
                                    {
                                        // Chk_ds1.Tables["RtnList1"].Rows.Count = '0';
                                        IsAlter3 = "1";
                                    }
                                }

                            }
                            else
                            {
                                IsAlter3 = "1";
                            }

                        }












                        // break;
                    }




                    if (IsAlter1 == "1" && IsAlter2 == "1" && IsAlter3 == "1")
                    {
                        allocate_teacher();
                        load_allocated_teachers();
                    }


                }
            }













       //     Session["ID"] = "";


        }


    }




    private void allocate_teacher()
    {
        string year = Session["year"].ToString();
        string semester = Session["sem"].ToString();


        string day1 = "";
        string time1 = "";
        string room1 = "";

        string day2 = "";
        string time2 = "";
        string room2 = "";

        string day3 = "";
        string time3 = "";
        string room3 = "";

        if (cmb_date1.SelectedItem.ToString() != "Select")
            day1 = cmb_date1.SelectedItem.ToString();
        else
            day1 = null;

        if (cmb_Slot1.SelectedValue.ToString() != "" || cmb_Slot1.SelectedValue.ToString() != "Select")
            time1 = cmb_Slot1.SelectedValue.ToString();
        else
            time1 = null;

        if (cmb_Room1.SelectedValue.ToString() != "Select")
            room1 = cmb_Room1.SelectedValue.ToString();
        else
            room1 = null;




        if (cmb_date2.SelectedItem.ToString() != "Select")
            day2 = cmb_date2.SelectedItem.ToString();
        else
            day2 = null;

        if (cmb_Slot2.SelectedValue.ToString() != "" || cmb_Slot2.SelectedValue.ToString() != "Select")
            time2 = cmb_Slot2.SelectedValue.ToString();
        else
            time2 = null;

        if (cmb_Room2.SelectedValue.ToString() != "Select")
            room2 = cmb_Room2.SelectedValue.ToString();
        else
            room2 = null;



        if (cmb_date3.SelectedItem.ToString() != "Select")
            day3 = cmb_date3.SelectedItem.ToString();
        else
            day3 = null;

        if (cmb_Slot3.SelectedValue.ToString() != "" || cmb_Slot3.SelectedValue.ToString() != "Select")
            time3 = cmb_Slot3.SelectedValue.ToString();
        else
            time3 = null;

        if (cmb_Room3.SelectedValue.ToString() != "Select")
            room3 = cmb_Room3.SelectedValue.ToString();
        else
            room3 = null;


        DataSet Chk_Class = new DataSet();         // check multi class for any teacher in the same slot time

        string Tearcher_ID = lblFacultyID.Text;
        //string semester = lbl_semester.Text;



        //Chk_Class.Merge(new admin_webService().match_TeacherSlot(cmb_Slot1.SelectedItem.ToString(), day1, cmb_Slot2.SelectedItem.ToString(), day2, cmb_Slot3.SelectedItem.ToString(), day3, Tearcher_ID, year, semester));
        //if (Chk_Class.Tables["RtnList1"].Rows.Count > 0)
        //{
        //    lbl_message.Text = "" + new cls_message().getMessage(28);
        //}
        //else
        //{

        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        DataSet ds3 = new DataSet();






        DataSet ds = new DataSet();
        ds.Tables.Add("WEB_COURSE_TEACHER");

        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("C_ROUTINE_ID1");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("C_ROUTINE_ID2");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("C_ROUTINE_ID3");



        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("COURSE_TEACHER_ID");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("COURSE_KEY");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("TEACHER_ID");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("SECTION");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("TOTAL_CAPACITY");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("ACC_CTRL");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("SCH_CLS_1");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("SCH_CLS_2");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("SCH_CLS_3");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("TUT_CLS_1");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("TUT_CLS_2");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("TUT_CLS_3");


        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("CLS_DAY1");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("CLS_DAY2");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("CLS_DAY3");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("CLS_DAY4");

        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("ROOM_ID1");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("ROOM_ID2");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("ROOM_ID3");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("ROOM_ID4");


        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("SLOT_SL1");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("SLOT_SL2");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("SLOT_SL3");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("SLOT_SL4");

        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("DEPT_ID");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("YEAR");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("SEMESTER");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("slotID1");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("slotID2");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("slotID3");


        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("INSERTION_TIME");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("UPDATE_TIME");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("INSERTIONID");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("UPDATEID");




        DataRow dr = ds.Tables["WEB_COURSE_TEACHER"].NewRow();





        dr["DEPT_ID"] = "" + cmb_Faculty.SelectedValue.ToString();
        dr["YEAR"] = "" + Session["year"].ToString();
        dr["SEMESTER"] = "" + Session["sem"].ToString();
        dr["COURSE_TEACHER_ID"] = "teacherId";
        dr["COURSE_KEY"] = "" + Session["sem"].ToString() + Session["year"].ToString() + code;
        dr["TEACHER_ID"] = "" + cmb_teacher.SelectedValue.ToString();




        if (cmb_Slot1.SelectedValue.ToString() != "" && cmb_date1.SelectedItem.ToString() != "Select" && cmb_Room1.SelectedValue.ToString() != "Select")
        {
            dr["SCH_CLS_1"] = "" + cmb_date1.SelectedValue.ToString() + " " + cmb_Slot1.SelectedItem.ToString() + " " + cmb_Room1.SelectedItem.ToString();
            string cls1 = cmb_date1.SelectedValue.ToString() + " " + cmb_Slot1.SelectedItem.ToString() + " " + cmb_Room1.SelectedItem.ToString();
            dr["TUT_CLS_1"] = "" + cmb_Slot1.SelectedValue.ToString();
        }
        else
            dr["SCH_CLS_1"] = "";



        if (cmb_Slot2.SelectedValue.ToString() != "" && cmb_date2.SelectedItem.ToString() != "Select" && cmb_Room2.SelectedValue.ToString() != "Select")
        {
            dr["SCH_CLS_2"] = "" + cmb_date2.SelectedValue.ToString() + " " + cmb_Slot2.SelectedItem.ToString() + " " + cmb_Room2.SelectedItem.ToString();
            dr["TUT_CLS_2"] = "" + cmb_Slot2.SelectedValue.ToString();
        }
        else
            dr["SCH_CLS_2"] = "";



        if (cmb_Slot3.SelectedValue.ToString() != "" && cmb_date3.SelectedItem.ToString() != "Select" && cmb_Room3.SelectedValue.ToString() != "Select")
        {
            dr["SCH_CLS_3"] = "" + cmb_date3.SelectedValue.ToString() + " " + cmb_Slot3.SelectedItem.ToString() + " " + cmb_Room3.SelectedItem.ToString();
            dr["TUT_CLS_3"] = "" + cmb_Slot3.SelectedValue.ToString();
        }
        else
            dr["SCH_CLS_3"] = "";





        dr["TOTAL_CAPACITY"] = "" + txt_Student.Text;
        dr["SECTION"] = "" + cmb_group.SelectedValue.ToString();

        dr["ACC_CTRL"] = "1";







        ds.Tables["WEB_COURSE_TEACHER"].Rows.Add(dr);


        //if (new admin_webService().is_allocate_teacher_exists(Session["sem"].ToString() + Session["year"].ToString() + code, cmb_group.SelectedValue.ToString()))
        //{
        //    if (new admin_webService().is_allocate_teacher_exists(cmb_teacher.SelectedValue.ToString(), Session["sem"].ToString() + Session["year"].ToString() + code, cmb_group.SelectedValue.ToString()))
        //    {

        //    }
        //}

        //   ----------------------------------------------


        string Class1 = "", Class2 = "", Class3 = "", CourseName_New = "";

        if (btn_submit.Text == "Submit")
        {
            if (cmb_Slot1.SelectedValue.ToString() != "" || cmb_Slot1.SelectedValue.ToString() != "Select")
            {
                dr["C_ROUTINE_ID1"] = cmb_Slot1.SelectedValue.ToString();
                Class1 = cmb_Slot1.SelectedValue.ToString();
            }
            else
                dr["C_ROUTINE_ID1"] = "";

            if (cmb_Slot2.SelectedValue.ToString() != "" || cmb_Slot2.SelectedValue.ToString() != "Select")
            {
                dr["C_ROUTINE_ID2"] = cmb_Slot2.SelectedValue.ToString();
                Class2 = cmb_Slot2.SelectedValue.ToString();
            }
            else
                dr["C_ROUTINE_ID2"] = "";

            if (cmb_Slot3.SelectedValue.ToString() != "" || cmb_Slot3.SelectedValue.ToString() != "Select")
            {
                dr["C_ROUTINE_ID3"] = cmb_Slot3.SelectedValue.ToString();
                Class3 = cmb_Slot3.SelectedValue.ToString();
            }
            else
                dr["C_ROUTINE_ID3"] = "";


            dr["INSERTIONID"] = Convert.ToInt32(Session["ctrl_admin_Id"]);
            dr["INSERTION_TIME"] = Convert.ToString(Convert.ToDateTime(DateTime.Now));

            if (new admin_webService().allocate_Class_room(ds) == "1")
            {
                lbl_message.Text = "" + new cls_message().getMessage(2);
                reset_information();
            }



        }
        else//-------------------update info
        {
            string slot1 = "", slot2 = "", slot3 = "", course_teacher_ID = "";

            string SL1 = "";
            string SL2 = "";
            string SL3 = "";
            string IsActive = Convert.ToString(Session["IsActive"]);

            if (cmb_Slot1.SelectedValue.ToString() != "" || cmb_Slot1.SelectedValue.ToString() != "Select")
                SL1 = cmb_Slot1.SelectedValue.ToString();
            else
                SL1 = "";

            if (cmb_Slot2.SelectedValue.ToString() != "" || cmb_Slot2.SelectedValue.ToString() != "Select")
                SL2 = cmb_Slot2.SelectedValue.ToString();
            else
                SL2 = "";

            if (cmb_Slot3.SelectedValue.ToString() != "" || cmb_Slot3.SelectedValue.ToString() != "Select")
                SL3 = cmb_Slot3.SelectedValue.ToString();
            else
                SL3 = "";

            dr["UPDATEID"] = Convert.ToInt32(Session["ctrl_admin_Id"]);
            dr["UPDATE_TIME"] = Convert.ToString(Convert.ToDateTime(DateTime.Now));

            if (cmb_date1.SelectedItem.ToString() != "Select")
                slot1 = cmb_date1.SelectedItem.ToString() + " " + cmb_Slot1.SelectedItem.ToString();
            if (cmb_date2.SelectedItem.ToString() != "Select")
                slot2 = cmb_date2.SelectedItem.ToString() + " " + cmb_Slot2.SelectedItem.ToString();
            if (cmb_date3.SelectedItem.ToString() != "Select")
                slot3 = cmb_date3.SelectedItem.ToString() + " " + cmb_Slot3.SelectedItem.ToString();


            int flagOffer = 0, flagtaken = 0;


            if (!(slot1 == Convert.ToString(Session["Clsday1"]) &&
               slot2 == Convert.ToString(Session["Clsday2"]) &&
               slot3 == Convert.ToString(Session["Clsday3"]) &&
               cmb_group.SelectedValue.ToString() == Convert.ToString(Session["Grp"]))
               )
            {
                string offerstd = "", takenStd = "";
                //check either offeringinandgrade or web_course_offering_temp
                course_teacher_ID = Convert.ToString(Session["ID"]);

                //check web_course_offering_temp
                DataSet dsCoureOffer = new DataSet();
                dsCoureOffer.Merge(new student_webService().get_offeringStudent(Convert.ToString(Session["ID"])));
                if (dsCoureOffer.Tables["Offering"].Rows.Count > 0)
                {
                    foreach (DataRow drS in dsCoureOffer.Tables["Offering"].Rows) // schedule which is going to take
                    {
                        offerstd = drS["student_offer"].ToString();
                        if (Convert.ToInt32(offerstd) > 0)
                        {
                            lbl_msg.Text = offerstd + " Student(s) have already offered for this course, so You can not Update the Routine time/Section. Please contact with IT office";
                            flagOffer = 1;
                        }
                    }
                }

                //check either offeringinandgrade
                DataSet dsTakenStd = new DataSet();
                dsTakenStd.Merge(new student_webService().get_TakenStudent(Convert.ToString(Session["ID"])));
                if (dsTakenStd.Tables["Taken"].Rows.Count > 0)
                {
                    foreach (DataRow drT in dsTakenStd.Tables["Taken"].Rows) // schedule which is going to take
                    {
                        takenStd = drT["student_Taken"].ToString();

                        if (Convert.ToInt32(takenStd) > 0)
                        {
                            if (lbl_msg.Text != "")
                                lbl_msg.Text += "<br/>" + takenStd + " Student(s) have already taken this course, so You can not Update the Routine time/Section. Please contact with IT office";
                            else
                                lbl_msg.Text = takenStd + " Student(s) have already taken this course, so You can not Update the Routine time/Section. Please contact with IT office";

                            flagtaken = 1;
                        }
                    }
                }

                if (flagOffer != 0 || flagtaken != 0)
                {
                    //  load_allocated_teachers();
                    reset_information();
                }

            }
            if (flagOffer == 0 && flagtaken == 0)
            {
                foreach (GridViewRow gr in GridView_courseList.Rows)
                {
                    if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)//042400007- Moshiur Rahaman
                    {
                        //if (cmb_group.SelectedValue.ToString() == gr.Cells[2].Text && cmb_teacher.SelectedValue.ToString() == ((Label)(gr.FindControl("Label_teacherId"))).Text)
                        //{
                        string course_techId = ((Label)(gr.FindControl("Label_link"))).Text;

                        if (new admin_webService().update_Class_room(ds, course_techId, SL1, SL2, SL3) == "1")
                        {
                            lbl_message.Text = "" + new cls_message().getMessage(2);
                            load_allocated_teachers();
                            reset_information();
                        }
                    }
                }
                Session["C_ROUTINE_ID1"] = "";
                Session["C_ROUTINE_ID2"] = "";
                Session["C_ROUTINE_ID3"] = "";
                btn_submit.Text = "Submit";
            }
        }



        txt_Student.ReadOnly = false;

        //--------------------------------------------------
    }





    private void load_group()
    {

        cmb_date1.DataSource = new cls_tools().days;
        cmb_date1.DataBind();

        cmb_date2.DataSource = new cls_tools().days;
        cmb_date2.DataBind();

        cmb_date3.DataSource = new cls_tools().days;
        cmb_date3.DataBind();

        cmb_date4.DataSource = new cls_tools().days;
        cmb_date4.DataBind();



        if (Convert.ToInt32(Session["year"].ToString() + Session["sem"].ToString()) <= 20183)
        {
            cmb_group.DataSource = new cls_tools().grpPrev;
            cmb_group.DataBind();
            cmb_group.SelectedValue = group;
        }
        else
        {
            if (lbl_course_code.Text == "BUS 498" || lbl_course_code.Text == "BUS 509")
            {
                cmb_group.DataSource = new cls_tools().grpNew;
                cmb_group.DataBind();
                cmb_group.SelectedValue = group;
            }
            else
            {
                cmb_group.DataSource = new cls_tools().grp;
                cmb_group.DataBind();
                cmb_group.SelectedValue = group;
            }
        }


        cmb_date1.SelectedValue = date1;
        cmb_date2.SelectedValue = date2;
        cmb_date3.SelectedValue = date3;
        cmb_date4.SelectedValue = date4;
    }







    protected void btn_delete_Click(object sender, EventArgs e)
    {
        string status = "1";
        foreach (GridViewRow gr in GridView_courseList.Rows)
        {
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                if (!String.IsNullOrEmpty(((Label)(gr.FindControl("Label_link"))).Text))
                    //  if (new admin_webService().delete_teacher_Class(((Label)(gr.FindControl("Label_link"))).Text) != "1")
                    //, ((Label)(gr.FindControl("SCH_CLS_1"))).Text, ((Label)(gr.FindControl("SCH_CLS_2"))).Text)


                    if (new admin_webService().delete_Teachers_room(((Label)(gr.FindControl("Label_link"))).Text, Convert.ToInt32(Session["ctrl_admin_Id"]), Convert.ToString(Convert.ToDateTime(DateTime.Now))) != "1")
                        status = "1" + 1;
            }
        }
        if (status != "1")
            lbl_message.Text = "" + new cls_message().getMessage(6);

        load_allocated_teachers();

    }
    protected void btn_bodify_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GridView_courseList.Rows)
        {
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                if (!String.IsNullOrEmpty(((Label)(gr.FindControl("Label_link"))).Text))
                {
                    DataSet dsRoutine = new DataSet();
                    gr.Enabled = false;
                    Session["ID"] = ((Label)(gr.FindControl("Label_Id"))).Text;
                    dsRoutine.Merge(new student_webService().get_a_course_routine(Convert.ToString(Session["ID"])));

                    foreach (DataRow drS in dsRoutine.Tables["course_schedule"].Rows) // schedule which is going to take
                    {
                        Session["Clsday1"] = drS["Clsday1"].ToString();
                        Session["Clsday2"] = drS["Clsday2"].ToString();
                        Session["Clsday3"] = drS["Clsday3"].ToString();
                        Session["Grp"] = drS["SECTION"].ToString();
                    }

                    string j = ((Label)(gr.FindControl("Label_facultyId"))).Text;


                    lblFacultyID.Text = ((Label)(gr.FindControl("Label_teacherId"))).Text;

                    //chk STAFF_CTRL for active inactive Faculty

                    DataSet ds_STAFF_CTRL = new DataSet();
                    ds_STAFF_CTRL.Merge(new admin_webService().match_STAFF_CTRL(lblFacultyID.Text));
                    foreach (DataRow dr_STAFF_CTRL in ds_STAFF_CTRL.Tables["StaffList"].Rows)
                    {
                        if (dr_STAFF_CTRL["STAFF_CTRL"].ToString() != "0")
                        {
                            
                            //btn_Cancel_Click(sender, e);
 lbl_message.Text = " ";
                            cmb_Faculty.SelectedValue = ((Label)(gr.FindControl("Label_facultyId"))).Text;
                            cmb_Faculty_SelectedIndexChanged(sender, e);
                            cmb_teacher.SelectedValue = ((Label)(gr.FindControl("Label_teacherId"))).Text;



                            lblHRT.Visible = true;
                            lblFacultyID.Visible = true;

                            lblFacultyID.Text = ((Label)(gr.FindControl("Label_teacherId"))).Text;

                            try
                            {
                                cmb_group.SelectedValue = gr.Cells[2].Text;
                            }
                            catch
                            {
                                cmb_group.SelectedValue = "Regular & Executive";
                            }
                            txt_Student.Text = ((Label)(gr.FindControl("lblCapacity"))).Text;

                            string cls1 = ((Label)(gr.FindControl("lblday1"))).Text;
                            string cls2 = ((Label)(gr.FindControl("lblday2"))).Text;
                            string cls3 = ((Label)(gr.FindControl("lblday3"))).Text;


                            DataSet ds = new DataSet();

                            ds.Merge(new admin_webService().match_dataRoutine(cls1, cls2, cls3));

                            foreach (DataRow dr in ds.Tables["RoutineList"].Rows)
                            {
                                // Session["IsActive"] = dr["IsActive"].ToString();

                                if (dr["YEAR"].ToString() == Session["year"].ToString() && dr["SEMESTER"].ToString() == Session["sem"].ToString() && cls1 == dr["C_ROUTINE_ID"].ToString())
                                {

                                    cmb_date1.SelectedValue = dr["DAY"].ToString();
                                    cmb_Room1.SelectedValue = dr["ROOM_ID"].ToString();
                                    cmb_Room1_SelectedIndexChanged(sender, e);

                                    cmb_Slot1.SelectedValue = dr["C_ROUTINE_ID"].ToString();
                                    Session["C_ROUTINE_ID1"] = dr["C_ROUTINE_ID"].ToString();

                                }


                                if (dr["YEAR"].ToString() == Session["year"].ToString() && dr["SEMESTER"].ToString() == Session["sem"].ToString() && cls2 == dr["C_ROUTINE_ID"].ToString())
                                {

                                    cmb_date2.SelectedValue = dr["DAY"].ToString();
                                    cmb_Room2.SelectedValue = dr["ROOM_ID"].ToString();
                                    cmb_Room2_SelectedIndexChanged(sender, e);

                                    cmb_Slot2.SelectedValue = dr["C_ROUTINE_ID"].ToString();
                                    Session["C_ROUTINE_ID2"] = dr["C_ROUTINE_ID"].ToString();

                                }


                                if (dr["YEAR"].ToString() == Session["year"].ToString() && dr["SEMESTER"].ToString() == Session["sem"].ToString() && cls3 == dr["C_ROUTINE_ID"].ToString())
                                {

                                    cmb_date3.SelectedValue = dr["DAY"].ToString();
                                    cmb_Room3.SelectedValue = dr["ROOM_ID"].ToString();
                                    cmb_Room3_SelectedIndexChanged(sender, e);

                                    cmb_Slot3.SelectedValue = dr["C_ROUTINE_ID"].ToString();
                                    Session["C_ROUTINE_ID2"] = dr["C_ROUTINE_ID"].ToString();

                                }

                            }

                            if (HttpContext.Current.Session["ctrl_admin_Id"] != null && !String.IsNullOrEmpty(HttpContext.Current.Session["ctrl_admin_Id"].ToString()))
                            {
                                if (!ConfigurationManager.AppSettings["admins"].Contains(HttpContext.Current.Session["ctrl_admin_Id"].ToString())
                                    && !HttpContext.Current.Request.Url.AbsoluteUri.Contains("_login.aspx"))
                                {
                                    //txt_Student.ReadOnly = true;
                                    txt_Student.ReadOnly = false;
                                }
                                else
                                {
                                    txt_Student.ReadOnly = false;
                                }

                            }

                            if (new admin_webService().UpdateIntoDemo(((Label)(gr.FindControl("Label_link"))).Text, Convert.ToInt32(Session["ctrl_admin_Id"]), Convert.ToString(Convert.ToDateTime(DateTime.Now))) == "1")
                            {
                                btn_submit.Text = "Update";
                                break;
                            }
                        }
                        else
                        {
                            lbl_message.Text = "Your Selected Teacher has Resigned, so his Profile is deactivated. Please contact with IT";
                           //  btn_Cancel_Click(sender, e);
                           

                        }

                    }



                   

                }
            }
        }
    }




    protected void cmb_Faculty_SelectedIndexChanged(object sender, EventArgs e)
    {

        string teacher_id = "";
        //reset_Faculty();

        if (cmb_Faculty.SelectedValue.ToString() != "Select")
        {
            // cmb_teacher.SelectedValue = "Select";

            string Faculty_ID = cmb_Faculty.SelectedValue.ToString();
            load_teacher(Faculty_ID);
        }

    }

    private void load_teacher(string Faculty_ID)
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_teacherFaculty(Faculty_ID));

        DataRow dr = ds.Tables["advisorList"].NewRow();
        dr["STAFF_NAME"] = "Select";
        dr["STAFF_ID"] = "Select";
        ds.Tables["advisorList"].Rows.Add(dr);

        cmb_teacher.DataSource = ds.Tables["advisorList"];
        cmb_teacher.DataTextField = "STAFF_NAME";
        cmb_teacher.DataValueField = "STAFF_ID";

        //cmb_teacher.SelectedValue = null;

        cmb_teacher.DataBind();

        //if (teacher_id == "")
        //    cmb_teacher.SelectedValue = "Select";
        //else
        //    cmb_teacher.SelectedValue = teacher_id;

    }

    private void load_Slot2(string dateID, string roomID, string year, string semester)
    {
        DataSet ds = new DataSet();
        string yearID = Session["year"].ToString();
        string semesterID = Session["sem"].ToString();
        ds.Merge(new admin_webService().get_slots(yearID, semesterID, dateID, roomID));

        DataRow dr = ds.Tables["SLOT_VIEW"].NewRow();
        dr["TIME"] = "Select";
        dr["C_ROUTINE_ID"] = "Select";
        ds.Tables["SLOT_VIEW"].Rows.Add(dr);

        cmb_Slot2.DataSource = ds.Tables["SLOT_VIEW"];
        cmb_Slot2.DataTextField = "TIME";
        cmb_Slot2.DataValueField = "C_ROUTINE_ID";
        cmb_Slot2.DataBind();

        if (slot_dtlid2 == "")
            cmb_Slot2.SelectedValue = "Select";
        //else
        //    cmb_Slot2.SelectedValue = slot_dtlid2;

    }

    private void load_Slot3(string dateID, string roomID, string year, string semester)
    {
        DataSet ds = new DataSet();
        string yearID = Session["year"].ToString();
        string semesterID = Session["sem"].ToString();
        ds.Merge(new admin_webService().get_slots(yearID, semesterID, dateID, roomID));

        DataRow dr = ds.Tables["SLOT_VIEW"].NewRow();
        dr["TIME"] = "Select";
        dr["C_ROUTINE_ID"] = "Select";
        ds.Tables["SLOT_VIEW"].Rows.Add(dr);

        cmb_Slot3.DataSource = ds.Tables["SLOT_VIEW"];
        cmb_Slot3.DataTextField = "TIME";
        cmb_Slot3.DataValueField = "C_ROUTINE_ID";
        cmb_Slot3.DataBind();

        if (slot_dtlid3 == "")
            cmb_Slot3.SelectedValue = "Select";
        //else
        //    cmb_Slot2.SelectedValue = slot_dtlid2;

    }

    private void load_Slot1(string dateID, string roomID, string year, string semester)
    {
        DataSet ds = new DataSet();
        string yearID = Session["year"].ToString();
        string semesterID = Session["sem"].ToString();
        ds.Merge(new admin_webService().get_slots(yearID, semesterID, dateID, roomID));

        DataRow dr = ds.Tables["SLOT_VIEW"].NewRow();
        dr["TIME"] = "Select";
        dr["C_ROUTINE_ID"] = "Select";
        ds.Tables["SLOT_VIEW"].Rows.Add(dr);


        cmb_Slot1.DataSource = ds.Tables["SLOT_VIEW"];
        cmb_Slot1.DataTextField = "TIME";
        cmb_Slot1.DataValueField = "C_ROUTINE_ID";
        cmb_Slot1.DataBind();

        if (slot_dtlid1 == "")
            cmb_Slot1.SelectedValue = "Select";
        //else
        //    cmb_Slot1.SelectedValue = slot_dtlid1;



    }


    private void load_teacher()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_teacher());

        DataRow dr = ds.Tables["advisorList"].NewRow();
        dr["STAFF_NAME"] = "Select";
        dr["STAFF_ID"] = "Select";
        ds.Tables["advisorList"].Rows.Add(dr);

        cmb_teacher.DataSource = ds.Tables["advisorList"];
        cmb_teacher.DataTextField = "STAFF_NAME";
        cmb_teacher.DataValueField = "STAFF_ID";
        //cmb_teacher.SelectedValue = null;
        cmb_teacher.DataBind();

        if (teacher_id == "")
            cmb_teacher.SelectedValue = "Select";
        else
            cmb_teacher.SelectedValue = teacher_id;

    }


    protected void cmb_teacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = cmb_teacher.SelectedValue.ToString();
        lblFacultyID.Text = cmb_teacher.SelectedValue.ToString();

        lblHRT.Visible = true;
        lblFacultyID.Visible = true;

    }


    private void reset_Slot1()
    {
        string dateID = "";
        string roomID = "";
        string year = "";
        string semester = "";
        load_Slot1(dateID, roomID, year, semester);
    }



    private void reset_Slot2()
    {
        string dateID = "";
        string roomID = "";
        string year = "";
        string semester = "";
        load_Slot2(dateID, roomID, year, semester);
    }


    private void reset_Slot3()
    {
        string dateID = "";
        string roomID = "";
        string year = "";
        string semester = "";
        load_Slot3(dateID, roomID, year, semester);
    }
    protected void cmb_Room1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_date1.SelectedValue.ToString() != "Select" && cmb_Room1.SelectedValue.ToString() != "Select")
        {
            reset_Slot1();
            string dateID = cmb_date1.SelectedValue.ToString();
            string roomID = cmb_Room1.SelectedValue.ToString();
            string year = Session["year"].ToString();
            string semester = Session["sem"].ToString();
            load_Slot1(dateID, roomID, year, semester);

        }

    }
    protected void cmb_Room2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_date2.SelectedValue.ToString() != "Select" && cmb_Room2.SelectedValue.ToString() != "Select")
        {
            reset_Slot2();
            string dateID = cmb_date2.SelectedValue.ToString();
            string roomID = cmb_Room2.SelectedValue.ToString();
            string year = Session["year"].ToString();
            string semester = Session["sem"].ToString();
            load_Slot2(dateID, roomID, year, semester);

        }
    }


    protected void cmb_Room3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_date3.SelectedValue.ToString() != "Select" && cmb_Room3.SelectedValue.ToString() != "Select")
        {
            reset_Slot3();
            string dateID = cmb_date3.SelectedValue.ToString();
            string roomID = cmb_Room3.SelectedValue.ToString();
            string year = Session["year"].ToString();
            string semester = Session["sem"].ToString();
            load_Slot3(dateID, roomID, year, semester);

        }
    }


    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        reset_Slot1();
        reset_Slot2();
        reset_Slot3();
        reset_information();
        load_allocated_teachers();
        btn_submit.Text = "Submit";
        lbl_msg.Text = "";
        txt_Student.ReadOnly = false;
        //foreach (GridViewRow row in GridView_courseList.Rows)
        //{
        //    CheckBox checkBox1 = row.FindControl("chk_select") as CheckBox;
        //    checkBox1.Checked = false; // based on condition

        //}
    }


}
