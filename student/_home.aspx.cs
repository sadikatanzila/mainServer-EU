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

public partial class student_home : System.Web.UI.Page
{
    string sid = "", PROGRAM_ID = "", COLLEGECODE="";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("_login.aspx");
            else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
            {   
                Response.Redirect("_login.aspx");
            }
            else
            {
                sid = Session["ctrlId"].ToString();

                DataSet S_ds = new DataSet();
                S_ds.Merge(new student_webService().get_PROGRAM_ID(Session["ctrlId"].ToString()));

                if (S_ds.Tables["Student"].Rows.Count > 0)
                {
                    foreach (DataRow R_dr in S_ds.Tables["Student"].Rows)
                    {
                        PROGRAM_ID = R_dr["PROGRAM_ID"].ToString();
                        COLLEGECODE = R_dr["COLLEGECODE"].ToString();
                        Session["PROGRAM_ID"] = PROGRAM_ID;
                        Session["COLLEGECODE"] = COLLEGECODE;
                        if (COLLEGECODE == "01")
                        {
                            pnlEngStudents.Visible = true;
                        }
                        else
                        {
                            pnlEngStudents.Visible = false;
                        }
                    }
                }
            }
        }
        catch (Exception exp) { Response.Redirect("_login.aspx"); }

        more_genNotice.Visible = false;
        student_notice.Visible = false;

        hplink_classRoutine.Attributes.Add("onClick", " return open_class_routine('"+sid+"');");

        load_student_information();
        load_student_notice();
        load_general_notice();
        load_Student_Message();
    }

    private void load_Student_Message()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_student_Message(sid));

        if (ds.Tables["WEB_Student_Message"].Rows.Count > 10)
            student_notice.Visible = true;

        for (int i = 0; i < ds.Tables["WEB_Student_Message"].Rows.Count; i++)
        {
            if (i == 10)
            {
                ds.Tables["WEB_Student_Message"].Rows.RemoveAt(i);
                i--;
            }
        }


        grdStdMsg.DataSource = ds;
        grdStdMsg.DataMember = "WEB_Student_Message";
        grdStdMsg.DataBind();
    }

    private void load_student_notice()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_student_notice());

        if (ds.Tables["WEB_NOTICE_BOARD"].Rows.Count > 10)
            student_notice.Visible = true;

        for (int i = 0; i < ds.Tables["WEB_NOTICE_BOARD"].Rows.Count; i++)
        {
            if (i == 10)
            {
                ds.Tables["WEB_NOTICE_BOARD"].Rows.RemoveAt(i);
                i--;
            }
        }
       

        GridView_studentNotice.DataSource = ds;
        GridView_studentNotice.DataMember = "WEB_NOTICE_BOARD";
        GridView_studentNotice.DataBind(); 
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


    private void load_student_information()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_a_student_information(sid));

        foreach (DataRow dr in ds.Tables["student"].Rows)
        {
            lbl_login.Text = "Login ID# "+sid;
            lbl_name.Text = dr["SNAME"].ToString();   

            if (String.IsNullOrEmpty(dr["S_PICTURE"].ToString()))
                img_myPicture.ImageUrl = "profile/student_images/no_image.gif";
            else
                img_myPicture.ImageUrl = "profile/student_images/" + sid + "." + dr["S_PICTURE"].ToString().Split('.')[dr["S_PICTURE"].ToString().Split('.').Length - 1];
            //img_myProfile.ImageUrl = "http://" + Request.Url.Host + "/student_images/" + sid + "." + dr["S_PICTURE"].ToString().Split('.')[dr["S_PICTURE"].ToString().Split('.').Length - 1];

            break;
        }

    }
    
}
