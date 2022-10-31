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

public partial class staffs_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("../employee/_login.aspx");
       /* if (!IsPostBack)
        {
            Session["ctrl_admin_Id"] = "";
            Session["ctrlId"] = "";
            Session["user"] = "";


            Session.Clear();
            Session.Abandon();
            Session.Contents.Clear();


        }
        else
        {
           
        }


        lbl_message.Text = "";
        Session["user"] = "";
        Session["ctrlId"] = ""; 


        btn_submit.Attributes.Add("onClick", " return check_valid();");
        txt_id.Focus();*/

    }


    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (new staff_webService().check_staff_login(txt_id.Value.ToString().Trim(), txt_pass.Value.ToString().Trim()))
        {
            Session["user"] = txt_id.Value.ToString().Trim();
            Response.Redirect("_home.aspx");
        }
        else
        {
            lbl_message.Text = "Invalid user/password!";
            txt_pass.Focus();
        }
    }
}
