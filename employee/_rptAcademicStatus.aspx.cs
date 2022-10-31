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
using System.Data.OracleClient ;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class employee_rptAcademicStatus : System.Web.UI.Page
{
    string dep = "", sid="";
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

        lbl_message.Text = "";
    }


    protected void btn_submit_Click(object sender, EventArgs e)
    {

        if (txtSID.Text != "")
        {
            DataTable ds = new DataTable();
            ds.Merge(new student_webService().get_AdmitCardList(txtSID.Text, "AcademicTranScript"));

            if (ds.Rows.Count > 0)
            {

                CrystalReportViewer1.Visible = true;
                crystalReport = new ReportDocument();
                crystalReport.Load(Server.MapPath("../employee/Report/_rptAcademicTranScript.rpt"));
                crystalReport.SetDataSource(ds);
                CrystalReportViewer1.ReportSource = crystalReport;
                crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AcademicTranScript");
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
            lbl_message.Text = "Please Enter the Student ID";

        }



    }









    protected void Img1_Click(object sender, ImageClickEventArgs e)
    {

        if (txtSID.Text != "")
        {
            DataTable ds = new DataTable();
            ds.Merge(new student_webService().get_AdmitCardList(txtSID.Text, "AcademicTranScript"));

            if (ds.Rows.Count > 0)
            {

                CrystalReportViewer1.Visible = true;
                crystalReport = new ReportDocument();
                crystalReport.Load(Server.MapPath("../employee/Report/_rptAcademicTranScript.rpt"));
                crystalReport.SetDataSource(ds);
                CrystalReportViewer1.ReportSource = crystalReport;
                crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AcademicTranScript");
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
            lbl_message.Text = "Please Enter the Student ID";

        }






    }

    protected void txtSID_TextChanged(object sender, EventArgs e)
    {

        sid = Convert.ToString(txtSID.Text);
        string DelT_stdbt = new student_webService().FindStdName(sid);
        lblSname.Text = DelT_stdbt;


    }
}