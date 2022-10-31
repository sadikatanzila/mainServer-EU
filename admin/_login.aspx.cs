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

public partial class admin_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("../employee/_login.aspx");
      /*  if (!IsPostBack)
        {
            Session["ctrl_admin_Id"] = "";
            Session["ctrlId"] = "";
            Session["user"] = "";
            Session["DEPTCODE"] = "";


            Session.Clear();
            Session.Abandon();
            Session.Contents.Clear();
        }


        lbl_message.Text = "";*/
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (new admin_webService().check_login(txt_id.Text.Trim(), txt_pass.Text.Trim()))
        {
            Session["ctrl_admin_Id"] = txt_id.Text.Trim();
            DataSet ds = new DataSet();

            ds.Merge(new admin_webService().match_UserID(txt_id.Text.Trim(), txt_pass.Text.Trim()));
            if (ds.Tables["UserList"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["UserList"].Rows)
                {
                    Session["UserID"] = dr["USER_SL"].ToString();
                    Session["DEPTCODE"] = dr["DEPTCODE"].ToString();
                }
            }

            Response.Redirect("_semester_course_list.aspx");
        }
        else
        {
            lbl_message.Text = "Invalid user/password!";
            txt_pass.Focus();
        }
    }
}
