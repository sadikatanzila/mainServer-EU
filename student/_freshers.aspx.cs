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

public partial class student_freshers : System.Web.UI.Page
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
    }
}