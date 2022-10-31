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

public partial class staffs_advisor_courseAdvisingList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("_login.aspx");
            else if (String.IsNullOrEmpty(Session["user"].ToString()))
            {
                Response.Redirect("_login.aspx");
            }
        }
        catch (Exception erp) { Response.Redirect("_login.aspx"); }
        load_advising_message();
    }

    private void load_advising_message()
    {
        cls_tools onj_tools = new cls_tools();

        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_advising_message(Session["user"].ToString()));

        ds.Tables["advising_msg_list"].Columns.Add("sem");
        ds.Tables["advising_msg_list"].Columns.Add("code");
        foreach (DataRow dr in ds.Tables["advising_msg_list"].Rows)
        {
            dr["sem"] = "" + onj_tools.get_word_semester(dr["SEMESTER"].ToString()) + ", " + dr["YEAR"];
            dr["code"] = "" + dr["sid"] + "_" + dr["SEMESTER"].ToString() + "_" + dr["YEAR"].ToString();
        }
           
        GridView_advising.DataSource = ds;
        GridView_advising.DataMember = "advising_msg_list";
        GridView_advising.DataBind();
    }
}
