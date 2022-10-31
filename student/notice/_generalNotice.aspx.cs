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

public partial class student_notice_generalNotice : System.Web.UI.Page
{
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

        }
        catch (Exception exp) { Response.Redirect("_login.aspx"); }

        load_general_notice();
    }

    private void load_general_notice()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_general_notice());

        ds.Tables["WEB_NOTICE_BOARD"].Columns.Add("pub_date");

        foreach (DataRow dr in ds.Tables["WEB_NOTICE_BOARD"].Rows)
        {
            dr["pub_date"] = new cls_tools().get_user_short_formateDate(dr["PUBLISH_DATE"].ToString());
        }

        GridView1.DataSource = ds;
        GridView1.DataMember = "WEB_NOTICE_BOARD";
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        load_general_notice();
    }
}
