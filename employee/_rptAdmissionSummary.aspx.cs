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

public partial class employee_rptAdmissionSummary : System.Web.UI.Page
{
    string dep = "";
    string user = "";
    student_webService obj_student = new student_webService();
    string DEPTCODE = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                user = Session["ctrl_admin_Id"].ToString();
                DEPTCODE = Session["DEPTCODE"].ToString();
            }
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }


        lbl_message.Text = "";

        btn_submit.Attributes.Add("onClick", " return chech_valid();");

        if (!IsPostBack)
        {


            //maximum year semester from Student table
            string YRSEM = "", YEAR = "", SEMETER = "";
            DataTable ds = new DataTable();
            ds.Merge(new student_webService().get_MaximumYearSemester("YearSem"));
            if (ds.Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Rows)
                {
                    YRSEM = Convert.ToString(dr["YRSEM"]);

                    string[] code = YRSEM.Split('_'); //Request.QueryString["DUE"].ToString().Split('|');
                    if (code.Length > 0)
                    {
                        YEAR = code[0];
                        SEMETER = code[1];

                        ddlSemester.SelectedValue = SEMETER;
                        txtYear.Text = YEAR;
                      //  ddlProgram.SelectedValue = "0";
 
                        btn_submit_Click( sender,  e);
                    }
                    else
                    {

                    }

                }

            }
        }

    }

    public void loadGridReport()
    {
        DataTable ds = new DataTable();
      

        if (Session["DEPTCODE"].ToString() != "")
        {
            ds.Merge(new student_webService().get_PermanentCampusStudentListProg(Convert.ToString(Session["DEPTCODE"].ToString()), "PermanentList"));
            //ds.Merge(new admin_webService().get_all_offeredCourses(txt_year.Text, cmb_semester.SelectedValue.ToString(), ));
        }
        else
        {
            ds.Merge(new student_webService().get_PermanentCampusStudentList("PermanentList"));
        }

       

        if (ds.Rows.Count > 0)
        {
            GridView_student.DataSource = ds;
            GridView_student.DataMember = "PermanentList";
            GridView_student.DataBind();
        }
    }

   

   

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        String SemYear = ""; String str = "", substrYear = "";
        DataTable ds = new DataTable();
        if ((ddlSemester.SelectedValue.ToString() != "Select" && txtYear.Text != ""))
        {
            //all program
            lblHeading.Text = "Admitted Student of " + ddlSemester.SelectedItem.Text + ", " + txtYear.Text;

            if (txtYear.Text != "")
            {
                str = txtYear.Text;
               // substrYear = str.Substring(str.Length - 2);
            }
            // SemYear = substrYear + ddlSemester.SelectedValue.ToString();

            ds.Merge(new student_webService().get_StdStatus(Convert.ToInt32(txtYear.Text), Convert.ToInt32(ddlSemester.SelectedValue.ToString()), "PermanetStudentList"));

        }
        else
        {
            lblHeading.Text = "";
            lbl_message.Text = "Please Insert a specific Year and Semester for Report";
        }

        GridView_student.DataSource = ds;
        GridView_student.DataMember = "PermanetStudentList";
        GridView_student.DataBind();
    }

    int j = 1;
    decimal dPageTotal = 0, dMaleTotal = 0, dFemaleTotal = 0;
    protected void GridView_student_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = j.ToString();
            j++;

            Label lblMale = (Label)e.Row.FindControl("lblRegular");
            dMaleTotal += Decimal.Parse(lblMale.Text);

            Label lblFemale = (Label)e.Row.FindControl("lblTransfer");
            dFemaleTotal += Decimal.Parse(lblFemale.Text);

            Label lblTotal = (Label)e.Row.FindControl("lblTotal");
            dPageTotal += Decimal.Parse(lblTotal.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl_Male = (Label)e.Row.FindControl("lblRegularTotal");
            lbl_Male.Text = dMaleTotal.ToString();

            Label lbl_Female = (Label)e.Row.FindControl("lblTransferTotal");
            lbl_Female.Text = dFemaleTotal.ToString();


            Label lbl_Totaltutionfee = (Label)e.Row.FindControl("lblGrandTotal");
            lbl_Totaltutionfee.Text = dPageTotal.ToString();

        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        lblHeading.Text = "Admitted Student in Permanent Campus";
        loadGridReport();
        txtYear.Text = "";

    }
}