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


public partial class employee_rptAttendanceEntry : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            loadFaculty();
        }
    }

    public void loadFaculty()
    {
        DataTable ds = new DataTable();
        ds.Merge(new student().get_allCollege("Department"));

        DataRow dr = ds.NewRow();// Tables["District_list"].NewRow();
        dr["COLLEGENAME"] = "All";
        dr["COLLEGECODE"] = "0";
        ds.Rows.Add(dr);

        ddlFaculty.DataSource = ds;
        ddlFaculty.DataTextField = "COLLEGENAME";
        ddlFaculty.DataValueField = "COLLEGECODE";
        ddlFaculty.DataBind();
    }
  
    protected void btn_submit_Click(object sender, EventArgs e)
    {

        if (ddlSemester.SelectedValue.ToString() != "Select" && txtYear.Text != "")
        {
            lblHeading.Text = "Attendance Entry Report of " + ddlSemester.SelectedItem.Text + " " + txtYear.Text +" " + ddlFaculty.SelectedItem.Text;


            DataTable ds = new DataTable();
            if (ddlFaculty.SelectedValue != "0")
            {
                string faculty = ddlFaculty.SelectedValue.ToString();
                ds.Merge(new student().get_AttendanceEntryAll(ddlSemester.SelectedValue.ToString() + txtYear.Text, faculty, "Attendance"));
                
            }
            else
            {
                ds.Merge(new student().get_AttendanceEntry(ddlSemester.SelectedValue.ToString(), txtYear.Text, "Attendance"));
            }
           
            GridView_student.DataSource = ds;
            GridView_student.DataMember = "Attendance";
            GridView_student.DataBind();




        }
        else
        {
            lbl_message.Text = "Please Enter the necessary Year & Semester";
        }
    }

    int j = 1;
    decimal dPageTotal = 0;
    protected void GridView_student_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = j.ToString();
            j++;

            if (ddlFaculty.SelectedValue != "0")
                GridView_student.Columns[5].Visible = false;
            else
                GridView_student.Columns[5].Visible = true;
        }

       
    }
}