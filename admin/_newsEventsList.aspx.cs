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

public partial class admin_newsEventsList : System.Web.UI.Page
{
    string user = "";
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


        btn_from.Attributes.Add("onClick", " loadCalender_stOpening();");
        btn_to.Attributes.Add("onClick", " loadCalender_stClosing();");
        btn_submit.Attributes.Add("onClick", " return chech_valid_data();");  
        lbl_message.Text = "";        
    } 


    private void load_news_events()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_filtered_news_event(Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_student_opening"].ToString()), Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_student_closing"].ToString())));

        ds.Tables["WEB_NEWS_EVENTS"].Columns.Add("newsOrEvent");
        foreach (DataRow dr in ds.Tables["WEB_NEWS_EVENTS"].Rows)
        {
            if (dr["TYPE"].ToString() == "1")
                dr["newsOrEvent"] = "News";
            else
                dr["newsOrEvent"] = "Events";
        } 
          
        GridView_noticeList.DataSource = ds;
        GridView_noticeList.DataMember = "WEB_NEWS_EVENTS";
        GridView_noticeList.DataBind();

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        load_news_events();
    }
}
