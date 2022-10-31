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

public partial class staffs_home : System.Web.UI.Page
{
    
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


        hpLink_academic_calender.Attributes.Add("onClick", "return loadAcademicCalender();");
        hplink_classRoutine.Attributes.Add("onClick", " return open_class_routine();");

        lbl_login.Text = "Login ID#" + Session["user"].ToString();
        lbltypes.Text = "Teacher Login";
        lbl_name.Text = new staff_webService().get_staff_info(Session["user"].ToString()).Rows[0]["STAFF_NAME"].ToString();
        
        HyperLink1.Visible = false; 
        more_genNotice.Visible = false;

        load_teacher_information();
        load_teacher_notice();
        load_general_notice();
        load_advising_message();

       // hpLink_advising_list.Text = "";
    }

    private void load_teacher_information()
    {//WEB_TEACHER_STAFF
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_staff_info(Session["user"].ToString()));

        foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
        {
            //lbl_login.Text = "Login ID# " + sid;
            lbl_name.Text = dr["STAFF_NAME"].ToString();

            if (String.IsNullOrEmpty(dr["STAFF_PICTURE"].ToString()))
                img_myPicture.ImageUrl = "profile/staff_image/no_image.gif";
            else
                img_myPicture.ImageUrl = "profile/staff_image/" + Session["user"].ToString() + "." + dr["STAFF_PICTURE"].ToString().Split('.')[dr["STAFF_PICTURE"].ToString().Split('.').Length - 1];
            //img_myProfile.ImageUrl = "http://" + Request.Url.Host + "/student_images/" + sid + "." + dr["S_PICTURE"].ToString().Split('.')[dr["S_PICTURE"].ToString().Split('.').Length - 1];

            break;
        }

    }





    private void load_teacher_notice()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_teacher_notice());

        if (ds.Tables["WEB_NOTICE_BOARD"].Rows.Count > 10)
            HyperLink1.Visible = true;

        for (int i = 0; i < ds.Tables["WEB_NOTICE_BOARD"].Rows.Count; i++)
        {
            if (i == 10)
            {
                ds.Tables["WEB_NOTICE_BOARD"].Rows.RemoveAt(i);
                i--;
            }
        }
        GridView_teacher.DataSource = ds;
        GridView_teacher.DataMember = "WEB_NOTICE_BOARD";
        GridView_teacher.DataBind();
    }

    private void load_advising_message()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_advising_message(Session["user"].ToString()));

        if (ds.Tables["advising_msg_list"].Rows.Count == 0)
        {
            hpLink_advising_list.Text="There is no pending advising.";
            hpLink_advising_list.Visible = true;
        }
        else //if (ds.Tables["advising_msg_list"].Rows.Count > 10)
        {
            hpLink_advising_list.Visible = true;
            hpLink_advising_list.Font.Bold = true;
            hpLink_advising_list.Text = "There are " + ds.Tables["advising_msg_list"].Rows.Count + " pending advising..";
            hpLink_advising_list.NavigateUrl = "advisor/_courseAdvisingList.aspx";
        }

        for (int i = 0; i < ds.Tables["advising_msg_list"].Rows.Count; i++)
        {
            if (i == 10)
            {
                ds.Tables["advising_msg_list"].Rows.RemoveAt(i);
                i--;
            }
        }

        cls_tools onj_tools = new cls_tools();

        ds.Tables["advising_msg_list"].Columns.Add("code");
        foreach (DataRow dr in ds.Tables["advising_msg_list"].Rows)
        {
             dr["code"] = "" + dr["sid"] + "_" + dr["SEMESTER"].ToString() + "_" + dr["YEAR"].ToString();
        }
    
        GridView_advising.DataSource = ds;
        GridView_advising.DataMember = "advising_msg_list";
        GridView_advising.DataBind();
    }

    private void load_general_notice()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_general_notice());

        if (ds.Tables["WEB_NOTICE_BOARD"].Rows.Count > 10)
            more_genNotice.Visible = true;

        for (int i = 0; i < ds.Tables["WEB_NOTICE_BOARD"].Rows.Count; i++)
        {
            if (i == 10)
            {
                ds.Tables["WEB_NOTICE_BOARD"].Rows.RemoveAt(i);
                i--;
            }
        }
        GridView_generalNotice.DataSource = ds;
        GridView_generalNotice.DataMember = "WEB_NOTICE_BOARD";
        GridView_generalNotice.DataBind();
    }
}
