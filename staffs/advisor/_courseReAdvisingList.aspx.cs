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

public partial class staffs_advisor_courseReAdvisingList : System.Web.UI.Page
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

		if (!IsPostBack)
        {
            DataSet maxds = new DataSet();
            maxds.Merge(new admin_webService().get_max_YearSem());
            if (maxds.Tables["WEB_PRE_OFFERING_DATE"].Rows.Count > 0)
            {
                foreach (DataRow dr1 in maxds.Tables["WEB_PRE_OFFERING_DATE"].Rows)
                {
                    Session["year"]  = dr1["YEAR"].ToString();
                    Session["sem"] = dr1["SEMESTER"].ToString();
                }
            }
           // Session["sem"] = 2;
           // Session["year"] = 2016;
        }
		
        if (!check_is_readvising_start())
        {
            GridView_advising.Visible = false;
        }
        else
        {
            load_advising_message();
        }
    }

    private bool check_is_readvising_start()
    {
        DateTime dToday = new DateTime();
        dToday = DateTime.Today;

        DataSet ds = new DataSet();
        string sem = Session["sem"].ToString();
        string year = Session["year"].ToString();

        ds.Merge(new admin_webService().get_pre_offerigDate(year, sem));

        if (ds.Tables["WEB_PRE_OFFERING_DATE"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["WEB_PRE_OFFERING_DATE"].Rows[0];
            if (!String.IsNullOrEmpty(dr["TEACHER_RE_OPENINGDATE"].ToString()) && !String.IsNullOrEmpty(dr["TEACHER_RE_CLOSINGDATE"].ToString()))
            {
                DateTime dtFrom = Convert.ToDateTime(dr["TEACHER_RE_OPENINGDATE"].ToString());
                DateTime dtTo = Convert.ToDateTime(dr["TEACHER_RE_CLOSINGDATE"].ToString()).AddHours(23);

                if (dToday >= dtFrom && dToday <= dtTo)
                {
                    return true;
                }
                else
                {
                    lbl_message.Text = "" + new cls_message().getMessage(17);
                }
            }
            else
                lbl_message.Text = "" + new cls_message().getMessage(11);
        }
        else
            lbl_message.Text = "" + new cls_message().getMessage(11);


        return false;
    }

    private void load_advising_message()
    {
        cls_tools onj_tools = new cls_tools();

        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_re_advising_message(Session["user"].ToString(), Session["year"].ToString(), Session["sem"].ToString()));

        ds.Tables["advising_msg_list"].Columns.Add("sem");
        ds.Tables["advising_msg_list"].Columns.Add("code");
        foreach (DataRow dr in ds.Tables["advising_msg_list"].Rows)
        {
            dr["sem"] = "" + onj_tools.get_word_semester(dr["SEMESTER"].ToString()) + ", " + dr["YEAR"];
            dr["code"] = "" + dr["sid"] + "_" + dr["SEMESTER"].ToString() + "_" + dr["YEAR"].ToString();

            Session["sem"] = dr["SEMESTER"].ToString();
            Session["year"] = dr["YEAR"].ToString();
        }

        GridView_advising.DataSource = ds;
        GridView_advising.DataMember = "advising_msg_list";
        GridView_advising.DataBind();
    }
}
