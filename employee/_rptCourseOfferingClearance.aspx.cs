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
using System.Data;

public partial class employee_rptCourseOfferingClearance : System.Web.UI.Page
{
    string dep = "";
    string user = "";
    student_webService obj_student = new student_webService();

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


        lbl_message.Text = "";

        btn_submit.Attributes.Add("onClick", " return chech_valid();");

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {

        if (ddlSemester.SelectedValue.ToString() != "Select" && txtYear.Text != "")
        {
            lblHeading.Text = "Course Offering Clearance of " + ddlSemester.SelectedItem.Text + " " + txtYear.Text;

            DataSet ds = new DataSet();
            ds.Merge(new student_webService().get_CourseOfferingClearnce(ddlSemester.SelectedValue.ToString(), txtYear.Text));
            GridView_student.DataSource = ds;
            GridView_student.DataMember = "CourseOfferingClearnce";
            GridView_student.DataBind();




        }
        else
        {
            lbl_message.Text = "Please Enter the necessary Year & Semester";
        }
    }

    int j = 1; decimal dPageTotal = 0, dMaleTotal = 0, dFemaleTotal = 0;
    protected void GridView_student_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = j.ToString();
            j++;


        }


    }
}