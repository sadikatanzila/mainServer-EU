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

public partial class admin_ConvoVourcherNo : System.Web.UI.Page
{
    string sid = "";
    string user = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                user = Session["ctrl_admin_Id"].ToString();

            }
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

        if (!IsPostBack)
        {
            load_student();
        }
    }

    private void load_student()
    {

        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_ConvoStd_VourcherNew());
        GridView_student.DataSource = ds;
        GridView_student.DataMember = "ConvoStd_Vourcher";
        GridView_student.DataBind();

    }



    int j = 1;
    protected void GridView_student_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = j.ToString();
            j++;

            if (user == "200011" || user == "200083" || user == "200019" || user == "200048")
                GridView_student.Columns[0].Visible = true;
            else
                GridView_student.Columns[0].Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtYear.Text != "")
        {

            DataSet ds = new DataSet();
            ds.Merge(new student_webService().get_ConvoStd_VourcherChk(txtYear.Text));
            GridView_student.DataSource = ds;
            GridView_student.DataMember = "ConvoStd_Vourcher";
            GridView_student.DataBind();
        }
    }
}
