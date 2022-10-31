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

public partial class student_finance_OnlineFinance : System.Web.UI.Page
{
    string sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
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
                    // string sName = "";
                    DataSet dsStudent = new DataSet();
                    dsStudent.Merge(new student_webService().check_student_Info(sid));
                    foreach (DataRow dr in dsStudent.Tables["STUDENT"].Rows)
                    {
                        Session["sName"] = dr["SNAME"].ToString();
                    }

                    load_Grid();
                    pnlStudent.Visible = true;
                    ssl_pay.Visible = false;
                    pnlStdAmount.Visible = false;
                    //TextBox txtAmountTotal = e.Row.FindControl("AmountTotal") as TextBox;
                    //txtAmountTotal.Attributes.Add("onload", "javascript:return GrandTotal();");
                }
                else
                {
                    chkSelect_CheckedChanged( sender,  e);
                }
                
            }
        }
        catch (Exception exp) { Response.Redirect("../_login.aspx"); }



    }


    private void load_Grid()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_STUDENT_HEADINFO());
        GridView4.DataSource = ds;
        GridView4.DataMember = "STUDENTHEADINFO";
        GridView4.DataBind();

    }

   

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        decimal Gross = 0, total = 0, b = 0;
        string tempMarks = "", HEADSN = "";

        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_STUDENT_HEADINFO());
        ds.Tables["STUDENTHEADINFO"].Columns.Add("YAER");
        ds.Tables["STUDENTHEADINFO"].Columns.Add("SEMESTER");
        ds.Tables["STUDENTHEADINFO"].Columns.Add("SID");


        ds.Tables.Add("T_STUDENTDEBIT");
        ds.Tables["T_STUDENTDEBIT"].Columns.Add("SID");
        ds.Tables["T_STUDENTDEBIT"].Columns.Add("YEAR");
        ds.Tables["T_STUDENTDEBIT"].Columns.Add("SEMESTER");
        ds.Tables["T_STUDENTDEBIT"].Columns.Add("HEADSN");
        ds.Tables["T_STUDENTDEBIT"].Columns.Add("AMOUNT");
        ds.Tables["T_STUDENTDEBIT"].Columns.Add("PDATE");
        ds.Tables["T_STUDENTDEBIT"].Columns.Add("STATUS");



        for (int i = 0; i < (GridView4.Rows.Count); i++)
        {
            GridViewRow row = GridView4.Rows[i];
            DataRow drn = ds.Tables["T_STUDENTDEBIT"].NewRow();
            b = Convert.ToDecimal(((TextBox)row.FindControl("Amount")).Text);

            DateTime today = DateTime.Today;
            string s_today = today.ToString("MM/dd/yyyy");
            today = Convert.ToDateTime(s_today);

            //if (chkSelect.Checked)
            //{
                if (b > 0)
                {
                    drn["SID"] = "" + lblStdID.Text;
                    drn["YEAR"] = "" + txtYear.Text;
                    drn["SEMESTER"] = "" + cmb_semester.SelectedValue.ToString();
                    drn["HEADSN"] = "" + Convert.ToString(((Label)row.FindControl("lblHEADSN")).Text);
                    drn["AMOUNT"] = "" + Convert.ToString(((TextBox)row.FindControl("Amount")).Text);
                    drn["STATUS"] = "I";
                    drn["PDATE"] = "" + today;
                    ds.Tables["T_STUDENTDEBIT"].Rows.Add(drn);
                }
                total = total + b;
            //}
            
        }

        string TRANID = new admin_webService().insert_final_stdDebit(ds);


        string myname = TRANID;
        Session["TRAN_ID"] = TRANID;

        string Year = "", Sem = "", Total = "";

        DataSet StdDebit = new DataSet();
        StdDebit.Merge(new admin_webService().get_StdPayment_TranID(TRANID));
        foreach (DataRow dr in StdDebit.Tables["T_studentDebit_Amount"].Rows)
        {
            Year = dr["YEAR"].ToString();
            Sem = dr["Sem"].ToString();
            Total = dr["Total_Amount"].ToString();

        }

        Session["Year"] = Convert.ToString(Year);
        Session["Semister"] = Convert.ToString(Sem);
        Session["Total_Amount"] = Convert.ToString(Total);

        if (String.IsNullOrEmpty(Session["ctrlId"].ToString()) || String.IsNullOrEmpty(Session["TRAN_ID"].ToString()) || String.IsNullOrEmpty(Session["Total_Amount"].ToString()) ||
                   String.IsNullOrEmpty(Session["Year"].ToString()) || String.IsNullOrEmpty(Session["Semister"].ToString()))
        {
            lbl_message.Text = "To Pay Your Payment firstly Select 'Year' , 'Semister' and payable 'Particulars Amount'.";
        }
        else
        {
            Response.Redirect("_OnlinePayment.aspx");
            //ssl_pay.Visible = true;
            //lbl_message.Text = "To Pay Your Payment with Online Payment, Press 'Pay Now'. "; //Convert.ToString(total);
            Clear();
        }

       // UncheckAll();
       

    }

    private void Clear()
    {
        txtYear.Text = "";
        load_Grid();

    }

    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //
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

    protected void Submit_onclick(object sender, EventArgs e)
    {
        btn_Submit_Click(sender, e);
    }
    protected void ssl_pay_onclick(object sender, EventArgs e)
    {
       // lbl_message.Text = "2588";
        btn_Submit_Click(sender, e);
    }

    protected void btnSubmit_onclick(object sender, EventArgs e)
    {
        string year= txtYear.Text;
        string sem= cmb_semester.SelectedValue.ToString();
        load_StdAmountGrid(sid, sem, year);

        pnlStdAmount.Visible = true;
    }

    private void load_StdAmountGrid(string sid, string sem, string year)
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_StdBalance_Year_Semister(sid, sem, year));
        grdStdAmount.DataSource = ds;
        grdStdAmount.DataMember = "Balance";
        grdStdAmount.DataBind();

    }


    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkTest = (CheckBox)sender;
        GridViewRow grdRow = (GridViewRow)chkTest.NamingContainer;
        TextBox Amount = (TextBox)grdRow.FindControl("Amount");
        if (chkTest.Checked)
        {
            Amount.ReadOnly = false;
            Amount.ForeColor = System.Drawing.Color.Black;
        }
        else
        {
            Amount.ReadOnly = true;
            Amount.ForeColor = System.Drawing.Color.Blue;
        }
    }
    private void UncheckAll()
    {
        foreach (GridViewRow row in GridView4.Rows)
        {
            CheckBox chkUncheck = (CheckBox)
                         row.FindControl("chkSelect");
            TextBox Amount = (TextBox)
                           row.FindControl("Amount");
           
            chkUncheck.Checked = false;
            Amount.ReadOnly = true;
            Amount.ForeColor = System.Drawing.Color.Blue;
        }
    }
}
