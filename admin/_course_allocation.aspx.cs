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

public partial class admin_course_allocation : System.Web.UI.Page
{
    string code = "";
    string teacher_id = "";
    string group = "";
    string room_id = "";

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





    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                user = Session["ctrl_admin_Id"].ToString();
            else
                Response.Redirect("../employee/_login.aspx");

            /*if (Request.QueryString["code"] != null)
                code = Request.QueryString["code"].ToString();
            else
                Response.Redirect("_login.aspx");*/

            if (string.IsNullOrEmpty(Convert.ToString(Session["COURSECODE"])))//if (Request.QueryString["ids"] != null)
                Response.Redirect("../employee/_login.aspx");
            else
                code = Convert.ToString(Session["COURSECODE"]);

        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }


        lbl_message.Text = "";
        lbl_teacher_message.Text = "";
        btn_submit.Attributes.Add("onClick", " return chech_valid(); ");


        if (IsPostBack)
        {
            load_initial();
        }
        else
        {
            load_allocated_teachers();
        }

        load_teacher();
        load_group();
        load_courses();



      //  load_Room_No();
    }


    private void reset_information()
    {
        //cmb_teacher.SelectedValue = "Select";

        cmb_teacher.SelectedValue = "Select";
        cmb_group.SelectedValue = "1";

        cmb_date1.SelectedValue = "Select";
        cmb_date2.SelectedValue = "Select";
        cmb_date3.SelectedValue = "Select";
        cmb_date4.SelectedValue = "Select";

        cmb_hour1.SelectedValue = "1";
        cmb_hour2.SelectedValue = "1";
        cmb_hour3.SelectedValue = "1";
        cmb_hour4.SelectedValue = "1";

        cmb_min1.SelectedValue = "1";
        cmb_min2.SelectedValue = "1";
        cmb_min3.SelectedValue = "1";
        cmb_min4.SelectedValue = "1";

        cmb_time1.SelectedValue = "AM";
        cmb_time2.SelectedValue = "AM";
        cmb_time3.SelectedValue = "AM";
        cmb_time4.SelectedValue = "AM";


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
                break;
            }
        }
    }

    private void load_allocated_teachers()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allcated_teacher_course(Session["sem"].ToString() + Session["year"].ToString() + code));

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
            btn_delete.Visible = true;
            btn_bodify.Visible = true;
        }
    }

    private void load_initial()
    {
        teacher_id = cmb_teacher.SelectedValue.ToString();
        group = cmb_group.SelectedValue.ToString();
        txt_capacity.Text = "";

        date1 = cmb_date1.SelectedValue.ToString();
        date2 = cmb_date2.SelectedValue.ToString();
        date3 = cmb_date3.SelectedValue.ToString();
        date4 = cmb_date4.SelectedValue.ToString();

        hour1 = cmb_hour1.SelectedValue.ToString();
        hour2 = cmb_hour2.SelectedValue.ToString();
        hour3 = cmb_hour3.SelectedValue.ToString();
        hour4 = cmb_hour4.SelectedValue.ToString();

        min1 = cmb_min1.SelectedValue.ToString();
        min2 = cmb_min2.SelectedValue.ToString();
        min3 = cmb_min3.SelectedValue.ToString();
        min4 = cmb_min4.SelectedValue.ToString();

        time1 = cmb_time1.SelectedValue.ToString();
        time2 = cmb_time2.SelectedValue.ToString();
        time3 = cmb_time3.SelectedValue.ToString();
        time4 = cmb_time4.SelectedValue.ToString();


    }





    protected void btn_submit_Click(object sender, EventArgs e)
    {
        allocate_teacher();
        load_allocated_teachers();
    }

    private void allocate_teacher()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("WEB_COURSE_TEACHER");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("COURSE_TEACHER_ID");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("COURSE_KEY");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("TEACHER_ID");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("SECTION");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("TOTAL_CAPACITY");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("ACC_CTRL");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("SCH_CLS_1");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("SCH_CLS_2");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("TUT_CLS_1");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("TUT_CLS_2");
        ds.Tables["WEB_COURSE_TEACHER"].Columns.Add("ROOM_NAME");

        DataRow dr = ds.Tables["WEB_COURSE_TEACHER"].NewRow();

        dr["COURSE_TEACHER_ID"] = "teacherId";
        dr["COURSE_KEY"] = "" + Session["sem"].ToString() + Session["year"].ToString() + code;
        dr["TEACHER_ID"] = "" + cmb_teacher.SelectedValue.ToString();

       //dr["ROOM_NAME"] = "" + cmb_Room.SelectedValue.ToString();


        dr["SECTION"] = "" + cmb_group.SelectedValue.ToString();
        dr["TOTAL_CAPACITY"] = Request["ctl00$ContentPlaceHolder_content$txt_capacity"];
        if (cmb_date1.SelectedValue.ToString() != "Select")
            dr["SCH_CLS_1"] = "" + cmb_date1.SelectedValue.ToString() + " " + cmb_hour1.SelectedValue.ToString() + ":" + cmb_min1.SelectedValue.ToString() + " " + cmb_time1.SelectedValue.ToString();
        else
            dr["SCH_CLS_1"] = "";

        if (cmb_date2.SelectedValue.ToString() != "Select")
            dr["SCH_CLS_2"] = "" + cmb_date2.SelectedValue.ToString() + " " + cmb_hour2.SelectedValue.ToString() + ":" + cmb_min2.SelectedValue.ToString() + " " + cmb_time2.SelectedValue.ToString();
        else
            dr["SCH_CLS_2"] = "";

        if (cmb_date3.SelectedValue.ToString() != "Select")
            dr["TUT_CLS_1"] = "" + cmb_date3.SelectedValue.ToString() + " " + cmb_hour3.SelectedValue.ToString() + ":" + cmb_min3.SelectedValue.ToString() + " " + cmb_time3.SelectedValue.ToString();
        else
            dr["TUT_CLS_1"] = "";

        if (cmb_date4.SelectedValue.ToString() != "Select")
            dr["TUT_CLS_2"] = "" + cmb_date4.SelectedValue.ToString() + " " + cmb_hour4.SelectedValue.ToString() + ":" + cmb_min4.SelectedValue.ToString() + " " + cmb_time4.SelectedValue.ToString();
        else
            dr["TUT_CLS_2"] = "";

        dr["ACC_CTRL"] = "1";

        ds.Tables["WEB_COURSE_TEACHER"].Rows.Add(dr);

        if (new admin_webService().is_allocate_teacher_exists(Session["sem"].ToString() + Session["year"].ToString() + code, cmb_group.SelectedValue.ToString()))
        {
            if (new admin_webService().is_allocate_teacher_exists(cmb_teacher.SelectedValue.ToString(), Session["sem"].ToString() + Session["year"].ToString() + code, cmb_group.SelectedValue.ToString()))
            {
                foreach (GridViewRow gr in GridView_courseList.Rows)
                {
                    if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)//042400007- Moshiur Rahaman
                    {
                        if (cmb_group.SelectedValue.ToString() == gr.Cells[2].Text && cmb_teacher.SelectedValue.ToString() == ((Label)(gr.FindControl("Label_teacherId"))).Text)
                        {
                            string course_techId = ((Label)(gr.FindControl("Label_link"))).Text;
                            if (new admin_webService().allocate_teacher_update(ds, course_techId) == "1")
                            {
                                lbl_message.Text = "" + new cls_message().getMessage(2);
                                load_allocated_teachers();
                                reset_information();
                            }
                        }
                    }
                }
            }
            else

                lbl_message.Text = "" + new cls_message().getMessage(5);
            return;
        }
        else
        {
            if (new admin_webService().allocate_teacherPrev(ds) == "1")
            {
                lbl_message.Text = "" + new cls_message().getMessage(2);
                reset_information();
            }
        }

    }





    private void load_group()
    {









        cmb_group.DataSource = new cls_tools().grp;
        cmb_group.DataBind();

        cmb_date1.DataSource = new cls_tools().daysPrev;
        cmb_date1.DataBind();

        cmb_date2.DataSource = new cls_tools().daysPrev;
        cmb_date2.DataBind();

        cmb_date3.DataSource = new cls_tools().daysPrev;
        cmb_date3.DataBind();

        cmb_date4.DataSource = new cls_tools().daysPrev;
        cmb_date4.DataBind();

        cmb_hour1.DataSource = new cls_tools().hours;
        cmb_hour1.DataBind();

        cmb_hour2.DataSource = new cls_tools().hours;
        cmb_hour2.DataBind();

        cmb_hour3.DataSource = new cls_tools().hours;
        cmb_hour3.DataBind();

        cmb_hour4.DataSource = new cls_tools().hours;
        cmb_hour4.DataBind();

        cmb_min1.DataSource = new cls_tools().min;
        cmb_min1.DataBind();

        cmb_min2.DataSource = new cls_tools().min;
        cmb_min2.DataBind();

        cmb_min3.DataSource = new cls_tools().min;
        cmb_min3.DataBind();

        cmb_min4.DataSource = new cls_tools().min;
        cmb_min4.DataBind();


        cmb_teacher.SelectedValue = teacher_id;
        cmb_group.SelectedValue = group;

        cmb_date1.SelectedValue = date1;
        cmb_date2.SelectedValue = date2;
        cmb_date3.SelectedValue = date3;
        cmb_date4.SelectedValue = date4;

        cmb_hour1.SelectedValue = hour1;
        cmb_hour2.SelectedValue = hour2;
        cmb_hour3.SelectedValue = hour3;
        cmb_hour4.SelectedValue = hour4;

        cmb_min1.SelectedValue = min1;
        cmb_min2.SelectedValue = min2;
        cmb_min3.SelectedValue = min3;
        cmb_min4.SelectedValue = min4;

        cmb_time1.SelectedValue = time1;
        cmb_time2.SelectedValue = time2;
        cmb_time3.SelectedValue = time3;
        cmb_time4.SelectedValue = time4;


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
        cmb_teacher.DataBind();
        if (teacher_id == "")
            cmb_teacher.SelectedValue = "Select";
        else
            cmb_teacher.SelectedValue = teacher_id;

    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        string status = "1";
        foreach (GridViewRow gr in GridView_courseList.Rows)
        {
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                if (!String.IsNullOrEmpty(((Label)(gr.FindControl("Label_link"))).Text))
                    if (new admin_webService().delete_allocated_teacher(((Label)(gr.FindControl("Label_link"))).Text) != "1")
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

                    gr.Enabled = false;
                    cmb_teacher.SelectedValue = ((Label)(gr.FindControl("Label_teacherId"))).Text;
                  //  cmb_Room.SelectedValue = ((Label)(gr.FindControl("Label_roomId"))).Text;


                    try
                    {
                        cmb_group.SelectedValue = gr.Cells[2].Text;
                    }
                    catch
                    {
                        cmb_group.SelectedValue = "Regular & Executive";
                    }
                    txt_capacity.Text = gr.Cells[3].Text;//  , '" + dr["TOTAL_CAPACITY"].ToString() + "'

                    if (!string.IsNullOrEmpty(gr.Cells[4].Text) && gr.Cells[4].Text != "&nbsp;")
                    {
                        cmb_date1.SelectedValue = gr.Cells[4].Text.Split(' ')[0];
                        cmb_time1.SelectedValue = gr.Cells[4].Text.Split(' ')[2];
                        cmb_hour1.SelectedValue = gr.Cells[4].Text.Split(' ')[1].Split(':')[0];
                        cmb_min1.SelectedValue = gr.Cells[4].Text.Split(' ')[1].Split(':')[1];
                    }

                    if (!string.IsNullOrEmpty(gr.Cells[5].Text) && gr.Cells[5].Text != "&nbsp;")
                    {
                        cmb_date2.SelectedValue = gr.Cells[5].Text.Split(' ')[0];
                        cmb_time2.SelectedValue = gr.Cells[5].Text.Split(' ')[2];
                        cmb_hour2.SelectedValue = gr.Cells[5].Text.Split(' ')[1].Split(':')[0];
                        cmb_min2.SelectedValue = gr.Cells[5].Text.Split(' ')[1].Split(':')[1];
                    }


                    if (!string.IsNullOrEmpty(gr.Cells[6].Text) && gr.Cells[6].Text != "&nbsp;")
                    {
                        cmb_date3.SelectedValue = gr.Cells[6].Text.Split(' ')[0];
                        cmb_time3.SelectedValue = gr.Cells[6].Text.Split(' ')[2];
                        cmb_hour3.SelectedValue = gr.Cells[6].Text.Split(' ')[1].Split(':')[0];
                        cmb_min3.SelectedValue = gr.Cells[6].Text.Split(' ')[1].Split(':')[1];
                    }

                    if (!string.IsNullOrEmpty(gr.Cells[7].Text) && gr.Cells[7].Text != "&nbsp;")
                    {
                        cmb_date4.SelectedValue = gr.Cells[7].Text.Split(' ')[0];
                        cmb_time4.SelectedValue = gr.Cells[7].Text.Split(' ')[2];
                        cmb_hour4.SelectedValue = gr.Cells[7].Text.Split(' ')[1].Split(':')[0];
                        cmb_min4.SelectedValue = gr.Cells[7].Text.Split(' ')[1].Split(':')[1];
                    }

                    break;
                }
            }
        }
    }
}
