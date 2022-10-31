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
using System.Configuration;
using System.IO;
using System.Data.OracleClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class employee_rptAttendanceSheet : System.Web.UI.Page
{
    string dep = "";
    string user = "";
    student_webService obj_student = new student_webService();
    string degree_id = "";

    ReportDocument crystalReport = null;

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
            // loadProgram();
        }


        lbl_message.Text = "";

        btn_submit.Attributes.Add("onClick", " return chech_valid();");

    }

    /*  public void loadProgram()
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



          DataRow dr = ds.NewRow();
          dr["PROGRAM"] = "Select";
          dr["C_PROGINDEPT_ID"] = "0";
          ds.Rows.Add(dr);
          ddlProgram.DataSource = ds;
          ddlProgram.DataTextField = "PROGRAM";
          ddlProgram.DataValueField = "C_PROGINDEPT_ID";
          ddlProgram.DataBind();
      }
      */



    public void loadCourse(string Year, String Semester)
    {
        DataTable ds = new DataTable();

        if (Session["DEPTCODE"].ToString() != "")
        {
            ds.Merge(new admin_webService().get_all_offeredCourses(Year, Semester, Session["DEPTCODE"].ToString()));
        }
        else
        {
            ds.Merge(new admin_webService().get_all_offeredCourses(Year, Semester));
        }

        for (int i = 0; i < ds.Rows.Count; i++)
        {
            DataRow dr = ds.Rows[i];
            for (int j = i + 1; j < ds.Rows.Count; j++)
            {
                DataRow drs = ds.Rows[j];

                if ((dr["COURSECODE"].ToString() == drs["COURSECODE"].ToString()) && (dr["CNAME"].ToString() == drs["CNAME"].ToString()))
                {
                    ds.Rows.RemoveAt(j);
                    j--;
                }
            }
        }

        // DataRow dr1 = ds.NewRow();
        // dr1["COURSECODE"] = "Select";
        // dr1["COURSECODE"] = "0";
        // ds.Rows.Add(dr1);
        ddlCourse.DataSource = ds;
        ddlCourse.DataTextField = "COURSECODE";
        ddlCourse.DataValueField = "COURSEKEY";
        ddlCourse.DataBind();

        pnlCourse.Visible = true;
        btnsubmitYearSem.Visible = false;
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {

        if (ddlSemester.SelectedValue.ToString() != "Select" && txtYear.Text != "")
        {
            if (ddlCourse.SelectedValue.ToString() != "Select" && txtSection.Text != "")
            {
                //    lblHeading.Text = "Eligible Admit Card for " + ddlProgram.SelectedItem.Text + " " + ddlSemester.SelectedItem.Text + " " + txtYear.Text;
                DataTable ds = new DataTable();

                ds.Merge(new student_webService().get_AttendanceSheet(ddlCourse.SelectedValue.ToString(), txtSection.Text, "AttendanceList"));

                  

                if (ds.Rows.Count > 0)
                {
                    //  CrystalReportViewer1.Visible = true;
                    crystalReport = new ReportDocument();
                    Img1.Visible = true;
                    Label1.Visible = true;
                    lbl_message.Text = "";
                    crystalReport.Load(Request.MapPath(Request.ApplicationPath) + "/staffs/report/_rptClassAttendance.rpt");
                    crystalReport.SetDataSource(ds);
                    BinaryReader stream = new BinaryReader(crystalReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
                    Response.Flush();
                    Response.Close();
                }
                else
                {
                    //  CrystalReportViewer1.Visible = false;
                    //   Img1.Visible = false;
                    //   Label1.Visible = false;
                    lbl_message.Text = "No data Found";
                }




            }
            else
            {
                lbl_message.Text = "Please Enter the necessary Course & Section";
            }
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
    protected void btnsubmitYearSem_Click(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedValue.ToString() != "Select" && txtYear.Text != "")
        {
            Img1.Visible = true;
            Label1.Visible = true;
            lbl_message.Text = "";

            loadCourse(txtYear.Text, ddlSemester.SelectedValue.ToString());
        }
        else
        {
            lbl_message.Text = "Please Enter the necessary Year & Semester";

        }
    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        pnlCourse.Visible = false;
        btnsubmitYearSem.Visible = true;
        Label1.Visible = false;
        Img1.Visible = false;
        CrystalReportViewer1.Visible = false;

    }


    protected void Img1_Click(object sender, ImageClickEventArgs e)
    {

        if (ddlSemester.SelectedValue.ToString() != "Select" && txtYear.Text != "")
        {
            if (ddlCourse.SelectedValue.ToString() != "Select" && txtSection.Text != "")
            {
                DataTable ds = new DataTable();
                ds.Merge(new student_webService().get_AttendanceSheet(ddlCourse.SelectedValue.ToString(), txtSection.Text, "AttendanceList"));

                  

                if (ds.Rows.Count > 0)
                {

                    CrystalReportViewer1.Visible = true;
                    crystalReport = new ReportDocument();
                    crystalReport.Load(Server.MapPath("~/staffs/report/_rptClassAttendance.rpt"));
                    crystalReport.SetDataSource(ds);
                    CrystalReportViewer1.ReportSource = crystalReport;
                    crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "StudentAttendance");



                }
                else
                {
                    CrystalReportViewer1.Visible = false;
                    Label1.Visible = false;
                    lbl_message.Text = "No Data Found";
                }




            }
            else
            {
                lbl_message.Text = "Please Enter the necessary Course & Section";
            }
        }
        else
        {
            lbl_message.Text = "Please Enter the necessary Year & Semester";

        }






    }
}