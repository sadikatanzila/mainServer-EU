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

public partial class student_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Session["ctrl_admin_Id"] = "";
            Session["ctrlId"] = "";
            Session["user"] = "";

            Session.Clear();
            Session.Abandon();
            Session.Contents.Clear();
        }
        lblmsg1.Text = "";
        lbl_message.Text = "";
        lblmsg.Text = "";
        btn_submit.Attributes.Add("onClick", " return check_valid();");
        txt_id.Focus();

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (new student_webService().check_student_login(txt_id.Value.ToString().Trim(), txt_pass.Value.ToString().Trim()))
        {

           
            string sid = txt_id.Value.ToString().Trim();
            if (new student_webService().is_dropout_student(sid))
            {
                lblmsg1.Text = "\n You are a dropout student.\n Please contact with registry department.";
                lbl_message.Text = "";
            }


            DataSet Chk_Student = new DataSet();
            Chk_Student.Merge(new student_webService().check_student_Block(txt_id.Value.ToString().Trim(), txt_pass.Value.ToString().Trim()));

            if (Chk_Student.Tables["Studentlist"].Rows.Count > 0)
            {
                foreach (DataRow dr in Chk_Student.Tables["Studentlist"].Rows)
                {
                    string Block = dr["REASON"].ToString();
                    lblmsg.Text = "Your account has been blocked for " + Block + ", Please contact Registrar's Office. Room 609, Building 1, House 26, Road 5, Dhanmondi, Dhaka - 1205.";
                    lbl_message.Text = "";
                    //txt_pass.Focus();
                }
            }
            else
                if (lblmsg1.Text == "" && lblmsg.Text == "")
                {
                    Session["ctrlId"] = txt_id.Value.ToString().Trim();
                    Response.Redirect("_home.aspx");
                }

            //Session["ctrlId"] = txt_id.Value.ToString().Trim();
            //Response.Redirect("_home.aspx");
        }
        else
        {
            lbl_message.Text = "Invalid user/password!";
            lblmsg.Text = "";
            lblmsg1.Text = "";
            txt_pass.Focus();
        }
    }
}