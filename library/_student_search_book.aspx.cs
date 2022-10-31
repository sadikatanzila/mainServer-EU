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

public partial class student_library_search__book : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    string pub = "";
    string dep = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
            {
                Response.Redirect("../_login.aspx");
            }
        }
        catch (Exception ert) { Response.Redirect("../_login.aspx"); }


        if (IsPostBack)
            pub = cmb_publisher.SelectedValue.ToString();
        dep = cmb_department.SelectedValue.ToString();
        load_publisher();
        load_department();

    }

    private void load_publisher()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_all_publisher());

        DataRow dr = ds.Tables["PUBLISHERS"].NewRow();
        dr["PUB_CODE"] = "0";
        dr["PUBLISHER_NAME"] = "Select";
        ds.Tables["PUBLISHERS"].Rows.Add(dr);

        cmb_publisher.DataSource = ds.Tables["PUBLISHERS"];
        cmb_publisher.DataTextField = "PUBLISHER_NAME";
        cmb_publisher.DataValueField = "PUB_CODE";
        cmb_publisher.DataBind();
        cmb_publisher.SelectedValue = "0";

        if (!String.IsNullOrEmpty(pub))
            cmb_publisher.SelectedValue = pub;
    }

    private void load_department()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allDepartment());

        DataRow dr = ds.Tables["departmentList"].NewRow();
        dr["COLLEGECODE"] = "xx";
        dr["DEPCODE"] = "Select";
        dr["DEPID"] = "0";
        ds.Tables["departmentList"].Rows.Add(dr);

        cmb_department.DataSource = ds.Tables["departmentList"];
        cmb_department.DataTextField = "DEPCODE";
        cmb_department.DataValueField = "DEPCODE";
        cmb_department.DataBind();
        cmb_department.SelectedValue = "Select";

        if (!String.IsNullOrEmpty(dep))
            cmb_department.SelectedValue = dep;
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_searched_books(cmb_publisher.SelectedValue.ToString(), txt_author.Text, cmb_department.SelectedValue.ToString(), txt_title.Text));

        ds.Tables["BOOK_MASTER"].Columns.Add("serial");
        int i = 1;
        foreach (DataRow dr in ds.Tables["BOOK_MASTER"].Rows)
        {
            dr["serial"] = "" + i++;
        }

        GridView_bookList.DataSource = ds;
        GridView_bookList.DataMember = "BOOK_MASTER";
        GridView_bookList.DataBind();

        GridView_bookList.Visible = true;
    }
}
