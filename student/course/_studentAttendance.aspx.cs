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

public partial class student_course_studentAttendance : System.Web.UI.Page
{
    string sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        lbl_semester.Text = "";
        btn_submit.Attributes.Add("onClick", " return save_check();");

        if (Session.Count == 0)
            Response.Redirect("../_login.aspx");
        else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
        {
            sid = Session["ctrlId"].ToString();
            Response.Redirect("../_login.aspx");
        }
        else
        {
            sid = Session["ctrlId"].ToString();
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        load_Attentance();
    }

    private void load_Attentance()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_Student_Persentance_NEW(sid, cmb_semester.SelectedValue.ToString() + txt_year.Text.ToString()));
        gvAttendance.DataSource = ds;
        gvAttendance.DataMember = "StudentPersentance";
        gvAttendance.DataBind();
    }
}