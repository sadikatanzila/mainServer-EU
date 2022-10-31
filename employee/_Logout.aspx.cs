using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class employee_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["ROLE_ID"] = "";
        Session["ctrl_admin_Id"] = "";
        Session["ctrlId"] = "";
        Session["user"] = "";
        Response.Redirect("../employee/_login.aspx");
    }
}