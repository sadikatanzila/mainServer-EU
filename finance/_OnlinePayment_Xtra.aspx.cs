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

public partial class student_finance_OnlinePayment_Xtra : System.Web.UI.Page
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

                    //TextBox txtAmountTotal = e.Row.FindControl("AmountTotal") as TextBox;
                    //txtAmountTotal.Attributes.Add("onload", "javascript:return GrandTotal();");
                }
                //else
                //{
                //    StdName.Value = Convert.ToString(Session["sName"]);
                //}

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

            if (b > 0)
            {
                drn["SID"] = "" + lblStdID.Text;
                drn["YEAR"] = "" + txtYear.Text;
                drn["SEMESTER"] = "" + cmb_semester.SelectedValue.ToString();
                drn["HEADSN"] = "" + Convert.ToString(((Label)row.FindControl("lblHEADSN")).Text);
                drn["AMOUNT"] = "" + Convert.ToString(((TextBox)row.FindControl("Amount")).Text);
                drn["STATUS"] = "1";
                drn["PDATE"] = "" + today;


                ds.Tables["T_STUDENTDEBIT"].Rows.Add(drn);
            }
            total = total + b;
        }

        int count = new admin_webService().insert_final_stdDebit(ds);


     //   string myname = Request.Form["first_name_txt"];


        total_amount.Value = Convert.ToString(total);
        value_b.Value = Convert.ToString(txtYear.Text);
        value_c.Value = cmb_semester.SelectedItem.ToString();
        value_d.Value = Convert.ToString(Session["sName"]);
        value_a.Value = Convert.ToString(sid);



        ssl_pay.Visible = true;
        //    Request.Form["value_a1"] = Convert.ToString(count);

        // Request.Form[

        //AddPassword_Click(sender, e);
        lbl_message.Text = "To Pay Your Payment with Online Payment, Press 'Pay Now'. "; //Convert.ToString(total);
        // Clear();

    }

    private void Clear()
    {
        txtYear.Text = "";
        load_Grid();

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

        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    TextBox txtAmountTotal = e.Row.FindControl("AmountTotal") as TextBox;
        //    txtAmountTotal.Attributes.Add("onload", "javascript:return GrandTotal();");
        //}


    }

    protected void Submit_onclick(object sender, EventArgs e)
    {
        btn_Submit_Click(sender, e);
    }
    
}
