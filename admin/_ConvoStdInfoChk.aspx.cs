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

public partial class admin_ConvoStdInfoChk : System.Web.UI.Page
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

       

    }

    private void load_student()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_Convo_student(Convert.ToString(txtBatch.Text)));
        GridView_student.DataSource = ds;
        GridView_student.DataMember = "Convo_STUDENT";
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
            //when mouse is over the row, save original color to new attribute, and change it to highlight yellow color
            // e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
            //when mouse leaves the row, change the bg color to its original value   
            // e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        load_student();
    }
}
