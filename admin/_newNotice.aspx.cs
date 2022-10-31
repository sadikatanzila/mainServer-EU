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

public partial class admin_newNotice : System.Web.UI.Page
{
    string user = "";
    string ntcId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                user = Session["ctrl_admin_Id"].ToString();
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }


        if (Request.QueryString["nid"] != null)
        {
            ntcId = Request.QueryString["nid"].ToString();

            if(!IsPostBack)
            load_notice();
        }
       // btn_calender.Attributes.Add("onClick", "loadCalender_stOpening();");
        btn_save.Attributes.Add("onClick", " return check_data();");

        lbl_message.Text = "";

    }


    private void load_notice()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_a_notice(ntcId));
        foreach (DataRow dr in ds.Tables["WEB_NOTICE_BOARD"].Rows)
        {
            txt_description.Value = dr["DESCRIPTION"].ToString();

            DateTime OPENING_DATE = Convert.ToDateTime(dr["PUBLISH_DATE"].ToString());
            txt_student_opening.Text = OPENING_DATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);

            txt_title.Text = dr["TITLE"].ToString();

            if (dr["CTRL"].ToString() == "1")
            {
                chk_status.Checked = true;
            }

            if (dr["FOR_GENERAL"].ToString() == "1")
            {
                chk_forGeneral.Checked = true;
            }

            if (dr["FOR_STUDENT"].ToString() == "1")
            {
                chk_student.Checked = true;
            }

            if (dr["FOR_TEACHER"].ToString() == "1")
            {
                chk_teacher.Checked = true;
            } 

        }
    
    }



    protected void btn_save_Click(object sender, EventArgs e)
    {
        save_notice();
    }


    private void save_notice()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("noticeBoard");

        ds.Tables["noticeBoard"].Columns.Add("NOTICE_ID");
        ds.Tables["noticeBoard"].Columns.Add("TITLE");
        ds.Tables["noticeBoard"].Columns.Add("DESCRIPTION");
        ds.Tables["noticeBoard"].Columns.Add("PUBLISH_DATE"); 
        ds.Tables["noticeBoard"].Columns.Add("INPUT_DATE");
        ds.Tables["noticeBoard"].Columns.Add("FOR_TEACHER");
        ds.Tables["noticeBoard"].Columns.Add("FOR_STUDENT");
        ds.Tables["noticeBoard"].Columns.Add("FOR_GENERAL");
        ds.Tables["noticeBoard"].Columns.Add("INPUT_BY");
        ds.Tables["noticeBoard"].Columns.Add("CTRL");

        DataRow dr = ds.Tables["noticeBoard"].NewRow();

        if (ntcId != "")
            dr["NOTICE_ID"] = ntcId;
        else
            dr["NOTICE_ID"] = "test";     

        dr["TITLE"] = "" + new cls_tools().get_formate_string(txt_title.Text);
        dr["DESCRIPTION"] = "" + new cls_tools().get_formate_string(txt_description.Value.ToString());
        dr["PUBLISH_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));
        //"" + new cls_tools().get_database_formateDate(Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_student_opening"].ToString()));
        dr["INPUT_DATE"] = ""+new cls_tools().get_database_formateDate(DateTime.Today);
          
        if(chk_teacher.Checked==true)
            dr["FOR_TEACHER"] = "1";
        else
            dr["FOR_TEACHER"] = "0";

        if (chk_student.Checked == true)
            dr["FOR_STUDENT"] = "1";
        else
            dr["FOR_STUDENT"] = "0";

        if (chk_forGeneral.Checked == true)
            dr["FOR_GENERAL"] = "1";
        else
            dr["FOR_GENERAL"] = "0";

        //dr["INPUT_BY"] = ""+Session[""];
        if(chk_status.Checked==true)
            dr["CTRL"] = "1";
        else
            dr["CTRL"] = "0";


        ds.Tables["noticeBoard"].Rows.Add(dr);
        
        string status="";

        if (ntcId == "")
            status = "" + new admin_webService().save_notice(ds);
        else
            status = "" + new admin_webService().update_notice(ds);

        if (status=="1")
            lbl_message.Text = "" + new cls_message().getMessage(2);
        else
            lbl_message.Text = "" + new cls_message().getMessage(14)+status;


    }

}
