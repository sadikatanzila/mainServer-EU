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

public partial class student_finance_studentLedger : System.Web.UI.Page
{
    string sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
            {
                sid = Session["ctrlId"].ToString();
                Response.Redirect("../_login.aspx");
            }
            else
            {
                sid = Session["ctrlId"].ToString();
            }
        }
        catch (Exception exp) { Response.Redirect("../_login.aspx"); }

        btn_submit.Attributes.Add("onClick", " loadstudentLedger(" + sid + ");");
    }

    
}
