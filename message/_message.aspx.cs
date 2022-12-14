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

public partial class student_message_message : System.Web.UI.Page
{
    string code = "";
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
                if (!String.IsNullOrEmpty(Request.QueryString["code"].ToString()))
                {
                    code = Request.QueryString["code"].ToString();
                }
                else Response.Redirect("_login.aspx");

        }
        catch (Exception exp) { Response.Redirect("_login.aspx"); }
        load_notice();
    }

    private void load_notice()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_a_message_details(code));

        foreach (DataRow dr in ds.Tables["WEB_STUDENT_MESSAGE"].Rows)
        {
            lbl_title.Text = dr["TITLE"].ToString();
            lbl_pub_date.Text = new cls_tools().get_user_formateDate(dr["PUBLISH_DATE"].ToString());
            lbl_description.Text = dr["DESCRIPTION"].ToString();
            break;
        }
    }
}
