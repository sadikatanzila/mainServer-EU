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

public partial class admin_ConvoAddNewStdudent : System.Web.UI.Page
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
        ds.Merge(new student_webService().get_NewConvo_student(txtYear.Text));
        GridView_student.DataSource = ds;
        GridView_student.DataMember = "EU_NewConvoStd";
        GridView_student.DataBind();

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string TRANID = "";

        ds.Tables.Add("EU_CONVO_EXTRAID");
        ds.Tables["EU_CONVO_EXTRAID"].Columns.Add("SID");
        ds.Tables["EU_CONVO_EXTRAID"].Columns.Add("YEAR");
        DataRow dr = ds.Tables["EU_CONVO_EXTRAID"].NewRow();

        dr["SID"] = "" + Convert.ToString(txtStudentID.Text);
        dr["YEAR"] = "" + Convert.ToString(txtYear.Text);
        ds.Tables["EU_CONVO_EXTRAID"].Rows.Add(dr);

        TRANID = new student_webService().insert_ConStdNew(ds);
        if (Convert.ToInt32(TRANID) > 0)
        {
            lblmsg.Text = "Insertion Sucess";
            load_student();
        }
        else
        {
            lblmsg.Text = "Insertion Fail";
        }
    }

    int j = 1;
    protected void GridView_student_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = j.ToString();
            j++;
            //when mouse is over the row, save original color to new attribute, and change it to highlight yellow color
            // e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
            //when mouse leaves the row, change the bg color to its original value   
            // e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        load_studentall();
    }

    private void load_studentall()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_NewConvo_studentAll());
        GridView_student.DataSource = ds;
        GridView_student.DataMember = "EU_NewConvoStd";
        GridView_student.DataBind();
    }
}
