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
using System.Data.OracleClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class student_finance_OfflinePayment : System.Web.UI.Page
{
    string sid = "", TRAN_ID = "";
    ReportDocument crystalReport = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else
                if (String.IsNullOrEmpty(Session["ctrlId"].ToString()) || String.IsNullOrEmpty(Session["TRAN_ID"].ToString()) || String.IsNullOrEmpty(Session["Total_Amount"].ToString()) ||
                    String.IsNullOrEmpty(Session["Year"].ToString()) || String.IsNullOrEmpty(Session["Semister"].ToString()))
                {
                    sid = Session["ctrlId"].ToString();
                    Response.Redirect("../_login.aspx");
                }
                else
                {
                    sid = Session["ctrlId"].ToString();
                    lblStdID.Text = sid;
                    if (!IsPostBack)
                    {
                        DataSet dsStudent = new DataSet();
                        dsStudent.Merge(new student_webService().check_student_Info(sid));
                        foreach (DataRow dr in dsStudent.Tables["STUDENT"].Rows)
                        {
                            Session["sName"] = dr["SNAME"].ToString();
                        }

                        loaddata();

                        pnlStudent.Visible = true;

                    }

                }
        }
        catch (Exception exp) { Response.Redirect("../_login.aspx"); }



    }

    private void loaddata()
    {
        TRAN_ID = Convert.ToString(Session["TRAN_ID"]);
        lblYear.Text = Convert.ToString(Session["Year"]);
        lblSem.Text = Convert.ToString(Session["Semister"]);
        lblTotalAmount.Text = Convert.ToString(Session["Total_Amount"]);

        load_Grid(TRAN_ID);

        lblCode.Text = TRAN_ID;
    }
    private void load_Grid(string TRAN_ID)
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_STUDENT_Payment(TRAN_ID));
        GridView4.DataSource = ds;
        GridView4.DataMember = "T_STUDENTDEBIT";
        GridView4.DataBind();

    }



    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //for (int i = 0; i < GridView4.Rows.Count - 1; i++)
        //{
        //    GridView4.Rows[0].Enabled = false;
        //    GridView4.Rows[1].Enabled = false;
        //}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtTempResult = e.Row.FindControl("Amount") as TextBox;
            decimal SubTotal = Convert.ToDecimal(txtTempResult.Text);
            txtTempResult.Attributes.Add("onkeyup", "javascript:return GetTotal('" + txtTempResult.ClientID + "');");


        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TextBox txtAmountTotal = e.Row.FindControl("AmountTotal") as TextBox;
            txtAmountTotal.Attributes.Add("onload", "javascript:return GrandTotal();");
        }


    }


  
    protected void Img1_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["TRAN_ID"].ToString() != "")
        {
            DataTable ds = new DataTable();
            ds.Merge(new student_webService().get_STUDENT_Payment(Session["TRAN_ID"].ToString(), "PaymentSlip"));

            if (ds.Rows.Count > 0)
            {

              //  CrystalReportViewer1.Visible = true;
                crystalReport = new ReportDocument();
                crystalReport.Load(Server.MapPath("~/student/finance/Report/_rptRegStudents.rpt"));
                crystalReport.SetDataSource(ds);
               // CrystalReportViewer1.ReportSource = crystalReport;
                crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PaymentSlip");



            }
            else
            {
             
                lbl_message.Text = "No Data Found";
            }
        }

    }
}
