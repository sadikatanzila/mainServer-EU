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

public partial class student_library_bookDetails : System.Web.UI.Page
{
    string code = "";
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

        if (Request.QueryString["code"] != null)
        {
            code = Request.QueryString["code"].ToString();
            load_bookInfo();
        }
        else Response.Redirect("../_login.aspx");
    }

    private void load_bookInfo()
    {
        lbl_author.Text = "" ;
        lbl_availableCopies.Text = "";
        lbl_publisher.Text = "" ;
        lbl_title.Text = "";
        lbl_department.Text = "";

        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_a_bookDetails_Info(code));

        ds.Tables["BOOK_MASTER"].Columns.Add("serial");
        int i = 1;
        foreach (DataRow dr in ds.Tables["BOOK_MASTER"].Rows)
        {
            if (i == 1)
            {
                lbl_author.Text = "" + dr["AUTHORS"].ToString();
                lbl_availableCopies.Text = "" + ds.Tables["BOOK_MASTER"].Rows.Count;
                lbl_publisher.Text = "" + dr["PUBLISHER_NAME"].ToString();
                lbl_title.Text = "" + dr["TITLE"].ToString();
                lbl_department.Text = "" + dr["DEP_NAME"].ToString();
            }
            dr["serial"] = "" + i++;
        }

        GridView_bookList.DataSource = ds;
        GridView_bookList.DataMember = "BOOK_MASTER";
        GridView_bookList.DataBind();
        GridView_bookList.Visible = true; 

    }
}
