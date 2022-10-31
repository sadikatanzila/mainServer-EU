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

public partial class employee_rptProbableGraduate : System.Web.UI.Page
{
    string dep = "";
    string user = "";
    student_webService obj_student = new student_webService();


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                user = Session["ctrl_admin_Id"].ToString();
                dep = Session["DEPTCODE"].ToString();
            }
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }


        lbl_message.Text = "";

        btn_submit.Attributes.Add("onClick", " return chech_valid();");

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {


        if (ddlFrmSemester.SelectedValue.ToString() != "Select" && ddlToSemester.SelectedValue.ToString() != "Select" && txtFrmYear.Text != "" && txtToYear.Text != "")
        {
            if (txtFrmYear.Text == txtToYear.Text && ddlFrmSemester.SelectedValue.ToString() == ddlToSemester.SelectedValue.ToString())
            {

                lblHeading.Text = "Probable Graduate List of " + ddlFrmSemester.SelectedItem.Text + ", " + txtFrmYear.Text ;


            }
            else
            {
                lblHeading.Text = "Probable Graduate List : From " + ddlFrmSemester.SelectedItem.Text + ", " + txtFrmYear.Text + " To " + ddlToSemester.SelectedItem.Text + ", " + txtToYear.Text;
         
            }

            DataTable ds = new DataTable();

            if (Session["DEPTCODE"].ToString() != "")
            {

                ds.Merge(new student().get_ProbableGraduateDeptwise(Convert.ToInt32(ddlFrmSemester.SelectedValue.ToString()), Convert.ToInt32(txtFrmYear.Text), Convert.ToInt32(ddlToSemester.SelectedValue.ToString()), Convert.ToInt32(txtToYear.Text), Session["DEPTCODE"].ToString(), "ProbableGraduate"));
            }
            else
            {

                ds.Merge(new student().get_ProbableGraduate(Convert.ToInt32(ddlFrmSemester.SelectedValue.ToString()), Convert.ToInt32(txtFrmYear.Text), Convert.ToInt32(ddlToSemester.SelectedValue.ToString()), Convert.ToInt32(txtToYear.Text), "ProbableGraduate"));
         
            }
            
            GridView_student.DataSource = ds;
            GridView_student.DataMember = "ProbableGraduate";
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