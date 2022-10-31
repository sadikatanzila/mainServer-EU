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

public partial class employee_CngPassword : System.Web.UI.Page
{
    string user = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                user = Session["ctrl_admin_Id"].ToString();
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

        btn_submit.Attributes.Add("onClick", " return save_check(); ");
      
      //  cngPass.Visible = true;
        lbl_message.Text = "";
    }




    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txt_previousPass.Text == "")
        {
            lbl_message.Text = "Please enter current password.";
        }
        else
        {
            if (txt_newPass.Text == "" || txt_confirmPass.Text == "")
            {
                if (txt_newPass.Text == "")
                {
                    lbl_message.Text = "Please enter new password.";
                }
                else
                    lbl_message.Text = "Please enter confirm password.";
            }
            else
            {

                if (txt_newPass.Text != txt_confirmPass.Text)
                {
                    lbl_message.Text = "Confirm password is not correct.";
                }
                else
                {

                    if (new admin_webService().change_staff_password(txt_previousPass.Text.Trim(), Session["ctrl_admin_Id"].ToString(), txt_newPass.Text.Trim()) == "1")
                    {
                       
                      //  cngPass.Visible = false;                        
                        lbl_message.Text = "Password changes successfully";
                    }
                }
            }
        }




    }
}