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
using System.Globalization;
using System.Threading;

public partial class admin_noticeList : System.Web.UI.Page
{
    string dep = "";
    string user = "";
    student_webService obj_student = new student_webService();
    string degree_id = "";

    //  ReportDocument crystalReport = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                dep = Session["DEPTCODE"].ToString();
                user = Session["ctrl_admin_Id"].ToString();
            }
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

        if (!IsPostBack)
        {
            //  loadProgram();
        }


        lblErrorMsg.Text = "";


        btn_submit.Attributes.Add("onClick", " return chech_valid();");

    }




    protected void btn_submit_Click(object sender, EventArgs e)
    {


        if (txtSID.Text != "")
        {
            DataTable ds = new DataTable();
            if (Session["DEPTCODE"].ToString() != "")
            {
                string DelT_stdbt = new student_webService().get_StudentName(txtSID.Text, Session["DEPTCODE"].ToString());
                if (DelT_stdbt != "")
                {
                    ds.Merge(new student_webService().get_StudentInfo(txtSID.Text, "StudentProfile"));
                    dsStudentProfile.Visible = true;
                }
                else
                {
                    dsStudentProfile.Visible = false;
                    lblErrorMsg.Text = "No Data Found/ This Student ID is not available in your Department";
                }


            }
            else
            {
                string DelT_stdbt = new student_webService().FindStdName(txtSID.Text);
                if (DelT_stdbt != "")
                {
                    ds.Merge(new student_webService().get_StudentInfo(txtSID.Text, "StudentProfile"));
                    dsStudentProfile.Visible = true;
                }
                else
                {
                    dsStudentProfile.Visible = false;
                    lblErrorMsg.Text = "No Data Found";
                }

            }

            if (ds.Rows.Count > 0)
            {
                dsStudentProfile.DataSource = ds;
                dsStudentProfile.DataBind();
            }
           

        }
        else
        {
            lblErrorMsg.Text = "Please Enter the Student ID";
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

    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        dsStudentProfile.Visible = false;
        txtSID.Text = "";
        //  Label1.Visible = false;
        // Img1.Visible = false;
        //  CrystalReportViewer1.Visible = false;

    }


   
}
