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

public partial class admin_final_result_publish : System.Web.UI.Page
{
    string user = "";
    string sem = "";


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

        if (IsPostBack)
            sem = Request["cmb_semester"];
        else
            sem = cmb_semester.SelectedValue.ToString();

        Session["sem"] = "";
        Session["year"] = "";

        lbl_message.Text = "";
        btn_submit.Attributes.Add("onClick", " return chech_valid();");

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txt_year.Text.Trim() == "")
            return;

        Session["sem"] = cmb_semester.SelectedValue.ToString();
        Session["year"] = txt_year.Text;

        if (Convert.ToInt32(Session["year"]) >= 2011)
            Publish_Result();
        else
            this.lbl_message.Text = "Year must be greater or equal to 2011";
    }

    private void Publish_Result()
    {
        try
        {
            new admin_webService().Publish_Result(cmb_semester.SelectedValue.ToString(), txt_year.Text.Trim());

            this.lbl_message.Text = "Successfully result publish for " + cmb_semester.SelectedItem.Text + ", " + txt_year.Text;
        }
        catch (Exception ex)
        {
            this.lbl_message.Text = ex.Message.ToString();
        }
    }
}
