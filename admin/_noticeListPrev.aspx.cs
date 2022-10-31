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

public partial class admin_noticeListPrev : System.Web.UI.Page
{
    string user = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                user = Session["ctrl_admin_Id"].ToString();
            else
                Response.Redirect("_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("_login.aspx"); }


        btn_from.Attributes.Add("onClick", " loadCalender_stOpening();");
        btn_to.Attributes.Add("onClick", " loadCalender_stClosing();");
        btn_submit.Attributes.Add("onClick", " return chech_valid_data();");

        btn_active.Visible = false;
        btn_inactive.Visible = false;
        lbl_message.Text = "";
             
    }


    private void load_notice()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_filtered_notice(Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_student_opening"].ToString()), Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_student_closing"].ToString()), cmb_messageType.SelectedValue.ToString()));

        ds.Tables["WEB_NOTICE_BOARD"].Columns.Add("student");
        ds.Tables["WEB_NOTICE_BOARD"].Columns.Add("teacher"); 
        ds.Tables["WEB_NOTICE_BOARD"].Columns.Add("general");
        ds.Tables["WEB_NOTICE_BOARD"].Columns.Add("pub_date");

        lbl_message.Text = "" + new cls_message().getMessage(1);

        foreach (DataRow dr in ds.Tables["WEB_NOTICE_BOARD"].Rows)
        {
            if (dr["FOR_STUDENT"].ToString() == "1")
                dr["student"] = "true";
            else
                dr["student"] = "false";

            if (dr["FOR_TEACHER"].ToString() == "1")
                dr["teacher"] = "true";
            else
                dr["teacher"] = "false";

            if (dr["FOR_GENERAL"].ToString() == "1")
                dr["general"] = "true";
            else
                dr["general"] = "false";

            dr["pub_date"] = "" + new cls_tools().get_user_short_formateDate(dr["PUBLISH_DATE"].ToString());
            
        }

        if (ds.Tables["WEB_NOTICE_BOARD"].Rows.Count>0)
        {
            if(cmb_messageType.SelectedValue.ToString()=="1")
                btn_inactive.Visible = true;
            else
                btn_active.Visible = true;
            lbl_message.Text = "";
        }



        GridView_noticeList.DataSource = ds;
        GridView_noticeList.DataMember = "WEB_NOTICE_BOARD";
        GridView_noticeList.DataBind();

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        load_notice();
    }


    protected void btn_active_Click(object sender, EventArgs e)
    {
        string ids = "";
        int count = 0;

        foreach (GridViewRow gr in GridView_noticeList.Rows)
        {
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                ids = ((Label)(gr.FindControl("NOTICE_ID"))).Text;
                new admin_webService().make_notice_active_inactive(ids, "1");
                count++;
            }
        }
        if (count > 0)
            lbl_message.Text = "" + new cls_message().getMessage(2);

    }


    protected void btn_inactive_Click(object sender, EventArgs e)
    {
        string ids = "";
        int count = 0;

        foreach (GridViewRow gr in GridView_noticeList.Rows)
        {
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                ids = ((Label)(gr.FindControl("NOTICE_ID"))).Text;
                new admin_webService().make_notice_active_inactive(ids,"0");
                count++;
            }
        }

        if (count > 0)
            lbl_message.Text = "" + new cls_message().getMessage(2);
    }
}
