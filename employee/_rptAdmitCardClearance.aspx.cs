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

public partial class employee_rptAdmitCardClearance : System.Web.UI.Page
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
            lblHeading.Text = "Admit Card Clearance of " + ddlSemester.SelectedItem.Text + " " + txtYear.Text ;

            DataSet ds = new DataSet();
            ds.Merge(new student_webService().get_AdmitCardClearnce(ddlSemester.SelectedValue.ToString(), txtYear.Text));

            //ds.Columns.Add("DUE");
            ds.Tables["CardClearnce"].Columns.Add("DUE");


            foreach (DataRow dr in ds.Tables["CardClearnce"].Rows)
            {
                string DUE = "", SemDue = "", graceAmt = "";
                DataSet InsDate_ds = new DataSet();
                InsDate_ds.Merge(new student_webService().get_FN_GET_PER_SEM_DUE(dr["SID"].ToString(), txtYear.Text, ddlSemester.SelectedValue.ToString()));
                if (InsDate_ds.Tables["SEM_DUE"].Rows.Count > 0)
                {
                    foreach (DataRow InsDate_dr in InsDate_ds.Tables["SEM_DUE"].Rows)
                    {
                        DUE = Convert.ToString(InsDate_dr["DUE"]);

                        string[] code = DUE.Split('|'); //Request.QueryString["DUE"].ToString().Split('|');
                        if (code.Length > 0)
                        {
                            SemDue = code[0];
                            graceAmt = code[1];

                        }

                    }

                }
                dr["DUE"] = SemDue;

            }
            GridView_student.DataSource = ds;
            GridView_student.DataMember = "CardClearnce";
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