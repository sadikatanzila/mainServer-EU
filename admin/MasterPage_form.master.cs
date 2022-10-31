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

public partial class staffs_advisor_MasterPage_form : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("_login.aspx");
            else if (String.IsNullOrEmpty(Session["user"].ToString()))
            {
                Response.Redirect("../_login.aspx");
            }
        }
        catch (Exception erp) { Response.Redirect("../_login.aspx"); }

        load_teacher_notice();
    }

    private void load_teacher_notice()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_teacher_notice());

        if (ds.Tables["WEB_NOTICE_BOARD"].Rows.Count > 5)
            more_genNotice.Visible = true;

        for (int i = 0; i < ds.Tables["WEB_NOTICE_BOARD"].Rows.Count; i++)
        {
            if (i == 5)
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
