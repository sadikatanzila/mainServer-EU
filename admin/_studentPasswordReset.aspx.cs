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

public partial class admin_studentPasswordReset : System.Web.UI.Page
{
    string user = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                user = Session["ctrl_admin_Id"].ToString();
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

        btn_submit.Attributes.Add("onClick", " return save_check();");
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (new student_webService().change_student_password(txt_usetId.Text.Trim(), txt_newPass.Text.Trim()) == "1")
        {           
            lbl_message.Text = "Password changes successfully";
        }
    }
}
