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

public partial class admin_viewLedger : System.Web.UI.Page
{
    string sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../employee/_login.aspx");
            else
                if (String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                {
                    Response.Redirect("../employee/_viewLedger.aspx");
                }
               


           
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }
       
    }


    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txtSID.Text != "")
        {
            Session["SID"] = txtSID.Text;

            if (String.IsNullOrEmpty(Session["SID"].ToString()))
            {
                Response.Redirect("../employee/_login.aspx");
            }
            else
            {
                if (Session["DEPTCODE"].ToString() != "")
                {
                    string DelT_stdbt = new student_webService().get_StudentName(txtSID.Text, Session["DEPTCODE"].ToString());
                    if (DelT_stdbt != "")
                    {
                        sid = Session["SID"].ToString();
                        lbl_message.Text = "";
                        Response.Redirect("_stdLedger.aspx");
                    }
                    else
                    {
                        lbl_message.Text = "No Data Found, this Student ID is not available in your department";
                    }
                }
                else
                {
                    sid = Session["SID"].ToString();
                    
                    Response.Redirect("_stdLedger.aspx");
                    btn_submit.Attributes.Add("onClick", " loadstudentLedger(" + sid + ");");
                }

            }
        }
    }
}