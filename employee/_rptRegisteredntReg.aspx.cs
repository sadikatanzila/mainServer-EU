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

public partial class employee_rptRegisteredntReg : System.Web.UI.Page
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
        
        if (ddlRegSemester.SelectedValue.ToString() != "Select" && ddlnonRegSemester.SelectedValue.ToString() != "Select" && txtRegYear.Text != "" && txtnonRegYear.Text != "")
        {
            lblHeading.Text = "Students Registered in " + ddlRegSemester.SelectedItem.Text + " " + txtRegYear.Text + " but not Registered in " + ddlnonRegSemester.SelectedItem.Text + " " + txtnonRegYear.Text + " (except Com CH)";


            DataTable ds = new DataTable();
            ds.Merge(new student().get_nonRegisterStudent(Convert.ToInt32(ddlRegSemester.SelectedValue.ToString()), Convert.ToInt32(txtRegYear.Text), Convert.ToInt32(ddlnonRegSemester.SelectedValue.ToString()), Convert.ToInt32(txtnonRegYear.Text), "RegisterNonReg"));
            GridView_student.DataSource = ds;
            GridView_student.DataMember = "RegisterNonReg";
            GridView_student.DataBind();




        }
        else
        {
            lbl_message.Text = "Please Enter the necessary Year & Semester";
        }
    }

    int j = 1;
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