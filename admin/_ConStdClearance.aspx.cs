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

public partial class admin_ConStdClearance : System.Web.UI.Page
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
        lblmsg.Text = "";

    }

    private void load_student()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_block_student());
        GridView_student.DataSource = ds;
        GridView_student.DataMember = "student";
        GridView_student.DataBind();


    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        sid = Convert.ToString(txtSid.Text);
        if (sid == null || sid == "")
        {
            lblmsg.Text = "Please Enter Student ID";
        }
        else
            if (chkPaper.Checked == false && chkVarification.Checked == false && chkLibrary.Checked ==false)
            {
                lblmsg.Text = "Please Choose Clearance related Option in Check box";
            }
            else
            {

                if (chkPaper.Checked == true)
                {
                    string DelT_stdbt = new student_webService().ClearStdPapermissing(sid);
                    if (Convert.ToInt32(DelT_stdbt) > 0)
                    {
                        lblmsg.Text = "Paper Missing clear of Student " + sid;
                    }
                    else
                    {
                        lblmsg.Text = "Fail to Clear Paper Missing of Student " + sid;
                    }
                }

                if (chkVarification.Checked == true)
                {
                    string DelT_stdbt = new student_webService().ClearStdPVarif(sid);
                    if (Convert.ToInt32(DelT_stdbt) > 0)
                    {
                        lblmsg.Text += "<br/>Paper Varification clear of Student " + sid;
                    }
                    else
                    {
                        lblmsg.Text += "<br/>Fail to Clear Paper Varification of Student " + sid;
                    }
                }

                if (chkLibrary.Checked == true)
                {
                    string DelT_stdbt = new student_webService().ClearStdLibrary(sid);
                    if (Convert.ToInt32(DelT_stdbt) > 0)
                    {
                        lblmsg.Text += "<br/>Library clear of Student " + sid;
                    }
                    else
                    {
                        lblmsg.Text += "<br/>Fail to Clear Library of Student " + sid;
                    }

                  
                }
                Clear();

            }

    }

    private void Clear()
    {
        txtSid.Text = "";
        txtStdName.Text = "";
        chkPaper.Checked = false;
        chkVarification.Checked = false;
        chkLibrary.Checked = false;
        load_student();
    }
    protected void txtSid_TextChanged(object sender, EventArgs e)
    {
        sid = Convert.ToString(txtSid.Text);
        string DelT_stdbt = new student_webService().FindStdName(sid);
        txtStdName.Text = DelT_stdbt;
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
}
