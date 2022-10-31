using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;



public partial class admin_newMessage : System.Web.UI.Page
{
    string user = "";
    string msgId = "";
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
            msgId = Request.QueryString["nid"].ToString();
           

            if (!IsPostBack)
                load_message();
            
        }
        //btn_calender.Attributes.Add("onClick", "loadCalender_stOpening();");
       // btn_calenderB.Attributes.Add("onClick", "loadCalender_BstOpening();");
       // btn_calenderAll.Attributes.Add("onClick", "loadCalender_GstOpening();");
       // btn_calenderG.Attributes.Add("onClick", "loadCalender_AstOpening();");


        btn_save.Attributes.Add("onClick", " return check_data();");
        btnSumbitBatch.Attributes.Add("onClick", " return check_data();");
        btnSumbitGroup.Attributes.Add("onClick", " return check_data();");
        btnSumbitAll.Attributes.Add("onClick", " return check_data();");

        lbl_message.Text = "";

    }
    

   

    #region Specific_Student

    
    protected void btn_save_Click(object sender, EventArgs e)
    {
        save_message();
    }

    

    private void save_message()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("messagelist");

        ds.Tables["messagelist"].Columns.Add("MESSAGE_ID");
        ds.Tables["messagelist"].Columns.Add("TITLE");
        ds.Tables["messagelist"].Columns.Add("DESCRIPTION");
        ds.Tables["messagelist"].Columns.Add("PUBLISH_DATE");
        ds.Tables["messagelist"].Columns.Add("INPUT_DATE");
        ds.Tables["messagelist"].Columns.Add("RECIVERSTUDENT_ID");
      //  ds.Tables["messagelist"].Columns.Add("RECIVERTEACHER_ID");
        ds.Tables["messagelist"].Columns.Add("INPUT_BY");
        ds.Tables["messagelist"].Columns.Add("CTRL");

        DataRow dr = ds.Tables["messagelist"].NewRow();

        if (msgId != "")
            dr["MESSAGE_ID"] = msgId;
        else
            dr["MESSAGE_ID"] = "test";

        dr["RECIVERSTUDENT_ID"] = "" + new cls_tools().get_formate_string(txt_StdID.Text);
        dr["TITLE"] = "" + new cls_tools().get_formate_string(txt_title.Text);
        dr["DESCRIPTION"] = "" + replace_(new cls_tools().get_formate_string(txt_description.Value.ToString()));
       
        dr["PUBLISH_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));
       
      //  dr["PUBLISH_DATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_student_opening"].ToString()));
      
        dr["INPUT_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);

     //   entity.Details = replace_(txtDetails.Text);

        //dr["INPUT_BY"] = ""+Session[""];
        if (chk_status.Checked == true)
            dr["CTRL"] = "1";
        else
            dr["CTRL"] = "0";


        ds.Tables["messagelist"].Rows.Add(dr);

        string status = "";

        if (msgId == "")
            status = "" + new admin_webService().save_message(ds);
        else
            status = "" + new admin_webService().update_message(ds);

        if (status == "1")
            lbl_message.Text = "" + new cls_message().getMessage(2);
        else
            lbl_message.Text = "" + new cls_message().getMessage(14) + status;


        Clear();


    }

    #endregion


    #region Specific_Batch

    protected void btnSumbitBatch_Click(object sender, EventArgs e)
    {
        save_Bmessage();
    }


    private void save_Bmessage()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("messageBlist");

        ds.Tables["messageBlist"].Columns.Add("MESSAGE_ID");
        ds.Tables["messageBlist"].Columns.Add("TITLE");
        ds.Tables["messageBlist"].Columns.Add("DESCRIPTION");
        ds.Tables["messageBlist"].Columns.Add("PUBLISH_DATE");
        ds.Tables["messageBlist"].Columns.Add("INPUT_DATE");
       // ds.Tables["messageBlist"].Columns.Add("RECIVERSTUDENT_ID");

        ds.Tables["messageBlist"].Columns.Add("BATCHID");

        ds.Tables["messageBlist"].Columns.Add("INPUT_BY");
        ds.Tables["messageBlist"].Columns.Add("CTRL");

        DataRow dr = ds.Tables["messageBlist"].NewRow();

        if (msgId != "")
            dr["MESSAGE_ID"] = msgId;
        else
            dr["MESSAGE_ID"] = "test";

        dr["BATCHID"] = "" + new cls_tools().get_formate_string(txtBatchID.Text);
        dr["TITLE"] = "" + new cls_tools().get_formate_string(txt_BTitle.Text);
        dr["DESCRIPTION"] = "" + replace_(new cls_tools().get_formate_string(txt_Bdescription.Value.ToString()));
        dr["PUBLISH_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_closing.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));
        //dr["PUBLISH_DATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_student_closing"].ToString()));
        dr["INPUT_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);

        //   entity.Details = replace_(txtDetails.Text);

        //dr["INPUT_BY"] = ""+Session[""];
        if (chk_statusB.Checked == true)
            dr["CTRL"] = "1";
        else
            dr["CTRL"] = "0";


        ds.Tables["messageBlist"].Rows.Add(dr);

        string status = "";

        if (msgId == "")
            status = "" + new admin_webService().save_Bmessage(ds);
        else
            status = "" + new admin_webService().update_Bmessage(ds);

        if (status == "1")
            lbl_message.Text = "" + new cls_message().getMessage(2);
        else
            lbl_message.Text = "" + new cls_message().getMessage(14) + status;


        Clear();


    }


    #endregion


    #region Specific_StudentGroup

    protected void btnSumbitGroup_Click(object sender, EventArgs e)
    {
        save_Gmessage();
    }

    private void save_Gmessage()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("messageGlist");

        ds.Tables["messageGlist"].Columns.Add("MESSAGE_ID");
        ds.Tables["messageGlist"].Columns.Add("TITLE");
        ds.Tables["messageGlist"].Columns.Add("DESCRIPTION");
        ds.Tables["messageGlist"].Columns.Add("PUBLISH_DATE");
        ds.Tables["messageGlist"].Columns.Add("INPUT_DATE");

        ds.Tables["messageGlist"].Columns.Add("STDGRPID");

        ds.Tables["messageGlist"].Columns.Add("INPUT_BY");
        ds.Tables["messageGlist"].Columns.Add("CTRL");

        DataRow dr = ds.Tables["messageGlist"].NewRow();

        if (msgId != "")
            dr["MESSAGE_ID"] = msgId;
        else
            dr["MESSAGE_ID"] = "test";

        dr["STDGRPID"] = "" + new cls_tools().get_formate_string(txt_StdIDG.Text);
        dr["TITLE"] = "" + new cls_tools().get_formate_string(txt_titleG.Text);
        dr["DESCRIPTION"] = "" + replace_(new cls_tools().get_formate_string(txt_descriptionG.Value.ToString()));
        dr["PUBLISH_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_teacher_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));

      //  dr["PUBLISH_DATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_teacher_opening"].ToString()));
        dr["INPUT_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);

        //   entity.Details = replace_(txtDetails.Text);

        //dr["INPUT_BY"] = ""+Session[""];
        if (chk_statusG.Checked == true)
            dr["CTRL"] = "1";
        else
            dr["CTRL"] = "0";


        ds.Tables["messageGlist"].Rows.Add(dr);

        string status = "";

        if (msgId == "")
            status = "" + new admin_webService().save_Gmessage(ds);
        else
            status = "" + new admin_webService().update_Gmessage(ds);

        if (status == "1")
            lbl_message.Text = "" + new cls_message().getMessage(2);
        else
            lbl_message.Text = "" + new cls_message().getMessage(14) + status;


        Clear();


    }
    #endregion

    #region All_Students

    protected void btnSumbitAll_Click(object sender, EventArgs e)
    {
        save_Amessage();
    }


    private void save_Amessage()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("messageAlist");

        ds.Tables["messageAlist"].Columns.Add("MESSAGE_ID");
        ds.Tables["messageAlist"].Columns.Add("TITLE");
        ds.Tables["messageAlist"].Columns.Add("DESCRIPTION");
        ds.Tables["messageAlist"].Columns.Add("PUBLISH_DATE");
        ds.Tables["messageAlist"].Columns.Add("INPUT_DATE");

        ds.Tables["messageAlist"].Columns.Add("INPUT_BY");
        ds.Tables["messageAlist"].Columns.Add("CTRL");

        DataRow dr = ds.Tables["messageAlist"].NewRow();

        if (msgId != "")
            dr["MESSAGE_ID"] = msgId;
        else
            dr["MESSAGE_ID"] = "test";


        dr["TITLE"] = "" + new cls_tools().get_formate_string(txt_TitleAll.Text);
        dr["DESCRIPTION"] = "" + replace_(new cls_tools().get_formate_string(txt_DescriptionAll.Value.ToString()));
        dr["PUBLISH_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_teacher_closing.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));

     //   dr["PUBLISH_DATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_teacher_closing"].ToString()));
        dr["INPUT_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);

        //   entity.Details = replace_(txtDetails.Text);

        //dr["INPUT_BY"] = ""+Session[""];
        if (chk_statusG.Checked == true)
            dr["CTRL"] = "1";
        else
            dr["CTRL"] = "0";


        ds.Tables["messageAlist"].Rows.Add(dr);

        string status = "";

        if (msgId == "")
            status = "" + new admin_webService().save_Amessage(ds);
        else
            status = "" + new admin_webService().update_Amessage(ds);

        if (status == "1")
            lbl_message.Text = "" + new cls_message().getMessage(2);
        else
            lbl_message.Text = "" + new cls_message().getMessage(14) + status;


        Clear();


    }
    #endregion




    #region common

    private void load_message()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_a_message(msgId));
        foreach (DataRow dr in ds.Tables["WEB_STUDENT_MESSAGE"].Rows)
        {

            if (dr["RECIVERSTUDENT_ID"].ToString() != "")
            {
                pnl_specificStd.Visible = true;

                txt_StdID.Text = dr["RECIVERSTUDENT_ID"].ToString();
                txt_title.Text = dr["TITLE"].ToString();

                txt_description.Value = replaceOposite(dr["DESCRIPTION"].ToString());
                txt_student_opening.Text = new cls_tools().get_user_formateDate(dr["PUBLISH_DATE"].ToString());



                if (dr["CTRL"].ToString() == "1")
                {
                    chk_status.Checked = true;
                }

            }
            else
                if (dr["BATCHID"].ToString() != "")
                {
                    pnlBatch.Visible = true;

                    txtBatchID.Text = dr["BATCHID"].ToString();
                    txt_BTitle.Text = dr["TITLE"].ToString();

                    txt_Bdescription.Value = replaceOposite(dr["DESCRIPTION"].ToString());
                    txt_student_closing.Text = new cls_tools().get_user_formateDate(dr["PUBLISH_DATE"].ToString());

                    if (dr["CTRL"].ToString() == "1")
                    {
                        chk_statusB.Checked = true;
                    }

                }
                else
                    if (dr["STDGRPID"].ToString() != "")
                    {
                        pnlGroup.Visible = true;

                        txt_StdIDG.Text = dr["STDGRPID"].ToString();
                        txt_titleG.Text = dr["TITLE"].ToString();

                        txt_descriptionG.Value = replaceOposite(dr["DESCRIPTION"].ToString());
                        txt_teacher_opening.Text = new cls_tools().get_user_formateDate(dr["PUBLISH_DATE"].ToString());



                        if (dr["CTRL"].ToString() == "1")
                        {
                            chk_statusG.Checked = true;
                        }

                    }
                    else
                        if ((dr["RECIVERSTUDENT_ID"].ToString() == "") && (dr["BATCHID"].ToString() == "") && (dr["STDGRPID"].ToString() == ""))
                        {
                            pnlMessage_All.Visible = true;

                            txt_TitleAll.Text = dr["TITLE"].ToString();

                            txt_DescriptionAll.Value = replaceOposite(dr["DESCRIPTION"].ToString());
                            txt_teacher_closing.Text = new cls_tools().get_user_formateDate(dr["PUBLISH_DATE"].ToString());

                            if (dr["CTRL"].ToString() == "1")
                            {
                                chk_statusA.Checked = true;
                            }

                        }





        }

    }


    protected string replaceOposite(string st)
    {

        //Regex rx = new Regex("&nbsp;");
        //string s1 = rx.Replace(st, " ");
        Regex ry = new Regex("<br/>");
        string s2 = ry.Replace(st, "\r\n");
        return s2;


    }
    protected string replace_(string st)
    {

        //Regex rx = new Regex(" ");
        //string s1 = rx.Replace(st, "&nbsp;");
        Regex ry = new Regex("\r\n|\n|\r");
        string s2 = ry.Replace(st, "<br/>");
        return s2;


    }

    private void Clear()
    {
        ClearSpecific();
        ClearBatch();
        ClearGroup();
        ClearAll();


    }
    private void ClearSpecific()
    {
        txt_StdID.Text = "";
        txt_title.Text = "";
        txt_student_opening.Text = "";
        txt_description = null;
    }

    private void ClearBatch()
    {
        txtBatchID.Text = "";
        txt_BTitle.Text = "";
        txt_student_closing.Text = "";
        txt_Bdescription = null;
    }

    private void ClearAll()
    {

        txt_TitleAll.Text = "";
        txt_teacher_closing.Text = "";
        txt_DescriptionAll = null;
    }

    private void ClearGroup()
    {
        txt_StdIDG.Text = "";
        txt_titleG.Text = "";
        txt_teacher_opening.Text = "";
        txt_descriptionG = null;
    }
    #endregion

    #region select_Panel

    protected void ddlMsgSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ddlMsgSystem.SelectedValue) == "1")
        {
            pnl_specificStd.Visible = true;
            pnlBatch.Visible = false;
            pnlMessage_All.Visible = false;
            pnlGroup.Visible = false;
        }
        else
            if (Convert.ToString(ddlMsgSystem.SelectedValue) == "2")
            {
                pnlBatch.Visible = true;
                pnl_specificStd.Visible = false;
                pnlMessage_All.Visible = false;
                pnlGroup.Visible = false;
            }
            else
                if (Convert.ToString(ddlMsgSystem.SelectedValue) == "3")
                {
                    pnlGroup.Visible = true;
                    pnlMessage_All.Visible = false;
                    pnlBatch.Visible = false;
                    pnl_specificStd.Visible = false;
                    
                }
            else
                if (Convert.ToString(ddlMsgSystem.SelectedValue) == "4")
                {
                    pnlMessage_All.Visible = true;
                    pnlBatch.Visible = false;
                    pnl_specificStd.Visible = false;
                    pnlGroup.Visible = false;
                }
            else
                    if (Convert.ToString(ddlMsgSystem.SelectedValue) == "-1")
                    {
                        lblErrorMsg.Text = "Select at least one system for sending message to the student(s)";
                        pnlGroup.Visible = false;
                        pnlMessage_All.Visible = false;
                        pnlBatch.Visible = false;
                        pnl_specificStd.Visible = false;
                    }
    }

    #endregion



   
    
    
}
