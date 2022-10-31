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

public partial class employee_rptDueList : System.Web.UI.Page
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
            loadProgram();
        }
    }

    public void loadProgram()
    {
        DataTable ds = new DataTable();
        if (Session["DEPTCODE"].ToString() != "")
        {
            ds.Merge(new student_webService().get_ProgramListDept(Session["DEPTCODE"].ToString(), "ProgramList"));
        }
        else
        {
            ds.Merge(new student_webService().get_ProgramList("ProgramList"));
        }

      //  DataRow dr = ds.NewRow();
      //  dr["PROGRAM"] = "Select";
      //  dr["C_PROGINDEPT_ID"] = "0";
      //  ds.Rows.Add(dr);
        // ds.Merge(new student_webService().get_ProgramList("ProgramList"));

        ddlProgram.DataSource = ds;
        ddlProgram.DataTextField = "PROGRAM";
        ddlProgram.DataValueField = "C_PROGINDEPT_ID";
        ddlProgram.DataBind();
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (rtbtnTotal.Checked == true)
        {
            if (ddlSemester.SelectedValue.ToString() != "Select" && txtYear.Text != "")
            {
                lblHeading.Text = "Due List of Registered Student in " + ddlSemester.SelectedItem.Text + " " + txtYear.Text;


                DataTable ds = new DataTable();
                ds.Merge(new student().get_StudentDueListTotal(Convert.ToInt32(txtYear.Text), Convert.ToInt32(ddlSemester.SelectedValue.ToString()), "StudentDueList"));

                if (ds.Rows.Count > 0)
                {
                    GridView_student.Visible = true;
                    GridView_student.DataSource = ds;
                    GridView_student.DataMember = "StudentDueList";
                    GridView_student.DataBind();
                }
                else
                {
                    GridView_student.Visible = false;
                }



            }
            else
            {
                lbl_message.Text = "Please Enter the necessary Year & Semester";
            }
            

           
        }
        else if (rbtnBatch.Checked == true)
        {

            if (ddlSemester.SelectedValue.ToString() != "Select" && txtYear.Text != "")
            {
                lblHeading.Text = "Due List of " + ddlProgram.SelectedItem.Text + " Student in " + ddlSemester.SelectedItem.Text + " " + txtYear.Text;


                DataTable ds = new DataTable();
                ds.Merge(new student().get_StudentDueList(Convert.ToInt32(ddlProgram.SelectedValue.ToString()), Convert.ToInt32(txtBatch.Text), "StudentDueList"));

                if (ds.Rows.Count > 0)
                {
                    ds.Columns.Add("DUE");
                    // ds.Tables["StudentDueList"].Columns.Add("DUE");
                    foreach (DataRow dr in ds.Rows)
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
                    GridView_student.Visible = true;
                    GridView_student.DataSource = ds;
                    GridView_student.DataMember = "StudentDueList";
                    GridView_student.DataBind();

                }
                else
                {
                    GridView_student.Visible = false;
                }



            }
            else
            {
                lbl_message.Text = "Please Enter the necessary Year & Semester";
            }
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