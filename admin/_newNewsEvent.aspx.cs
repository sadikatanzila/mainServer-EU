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
using System.IO;

public partial class admin_newNewsEvent : System.Web.UI.Page
{
    string user = "";
    string ids = "";
    string ext="";

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
            ids = Request.QueryString["nid"].ToString();
            if (!IsPostBack)
                load_news_events();
        }
        lbl_message.Text = "";
        btn_calender_from.Attributes.Add("onClick", "return loadCalender_stOpening();");
        btn_calender_to.Attributes.Add("onClick", "return  loadCalender_stClosing();");
        btn_save.Attributes.Add("onClick", "return  chech_valid_data();");              
    }


    private void load_news_events()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_a_news_event(ids));
        foreach (DataRow dr in ds.Tables["WEB_NEWS_EVENTS"].Rows)
        {
            txt_description.Value = dr["DESCRIPTION"].ToString();
            txt_student_opening.Text = new cls_tools().get_user_formateDate(dr["FROM_DATE"].ToString());
            txt_student_closing.Text = new cls_tools().get_user_formateDate(dr["TO_DATE"].ToString());
            txt_title.Text = dr["TITLE"].ToString();
            cmb_type.SelectedValue = dr["TYPE"].ToString();

            if (dr["EVENT_CTRL"].ToString() == "1")
                chk_status.Checked = true;
            else
                chk_status.Checked = false;

            if (!String.IsNullOrEmpty( dr["EVENT_IMAGE"].ToString()))
                ext = dr["EVENT_IMAGE"].ToString().Split('.')[dr["EVENT_IMAGE"].ToString().Split('.').Length-1];
        }
    }


    protected void btn_save_Click(object sender, EventArgs e)
    {
        save_news_events();
    }

    private void save_news_events()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("WEB_NEWS_EVENTS");

        ds.Tables["WEB_NEWS_EVENTS"].Columns.Add("NEWS_EVENT_ID");
        ds.Tables["WEB_NEWS_EVENTS"].Columns.Add("TITLE");
        ds.Tables["WEB_NEWS_EVENTS"].Columns.Add("DESCRIPTION");
        ds.Tables["WEB_NEWS_EVENTS"].Columns.Add("FROM_DATE");
        ds.Tables["WEB_NEWS_EVENTS"].Columns.Add("TO_DATE");
        ds.Tables["WEB_NEWS_EVENTS"].Columns.Add("INPUT_DATE");
        ds.Tables["WEB_NEWS_EVENTS"].Columns.Add("EVENT_IMAGE");
        ds.Tables["WEB_NEWS_EVENTS"].Columns.Add("INPUT_BY");
        ds.Tables["WEB_NEWS_EVENTS"].Columns.Add("TYPE");
        ds.Tables["WEB_NEWS_EVENTS"].Columns.Add("EVENT_CTRL");

        DataRow dr = ds.Tables["WEB_NEWS_EVENTS"].NewRow();
        if (ids != "")
            dr["NEWS_EVENT_ID"] = ids;
        else
            dr["NEWS_EVENT_ID"] = "test";

        dr["TITLE"] = "" + new cls_tools().get_formate_string(txt_title.Text);
        dr["DESCRIPTION"] = "" + new cls_tools().get_formate_string(txt_description.Value.ToString());
        dr["FROM_DATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_student_opening"].ToString()));
        dr["TO_DATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_student_closing"].ToString()));
        dr["INPUT_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);

        string fileExtension = "";
        if (cmb_type.SelectedValue == "1")
        {
            if ((FileUpload_image.PostedFile != null) && (FileUpload_image.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(FileUpload_image.PostedFile.FileName);
                fileExtension = fn.Split('.')[fn.Split('.').Length - 1];
                string f_name = fn.Split('\\')[fn.Split('\\').Length - 1];
                dr["EVENT_IMAGE"] = "" + f_name;
            }
        }

        dr["INPUT_BY"] = ""+user;
        dr["TYPE"] = ""+cmb_type.SelectedValue.ToString();

        if (chk_status.Checked==true)
            dr["EVENT_CTRL"] = "1";
        else
            dr["EVENT_CTRL"] = "0";
        ds.Tables["WEB_NEWS_EVENTS"].Rows.Add(dr);

        string status = "";
        if (ids == "")
            status = new admin_webService().save_news_Events(ds, ref ids);
        else
            status = new admin_webService().update_news_Events(ds); 

        if (cmb_type.SelectedValue.ToString() == "1")
         {
             string SavedLocation = "";
             /* Big image delete  */
             try
             { 
                SavedLocation = Server.MapPath("news_event_images") + "/" + ids + "." + ext;
                File.Delete("" + SavedLocation);
             }
             catch (Exception ert) { }

             /* small image delete  */
             try
             {
                 SavedLocation = Server.MapPath("news_event_images") + "/" + ids + "_b." + ext;
                 File.Delete("" + SavedLocation);
             }
             catch (Exception ert) { }
            
             /* big image create*/
             try
             {
                 SavedLocation = Server.MapPath("news_event_images/") + ids + "." + fileExtension;
                 FileUpload_image.PostedFile.SaveAs(SavedLocation);
             }
             catch (Exception ert) { status = "13"; }

             /* small image create  */

             try
             {
                 SavedLocation = Server.MapPath("news_event_images/") + ids + "_b." + fileExtension;
                 FileUpload_image_b.PostedFile.SaveAs(SavedLocation);
             }
             catch (Exception ert) { status = "13"; }

         }

        if (status == "1")
            lbl_message.Text = "" + new cls_message().getMessage(2);
        else
            lbl_message.Text = "" + new cls_message().getMessage(14) + status; 
    }
}
