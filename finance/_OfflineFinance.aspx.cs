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

public partial class student_finance_OfflineFinance : System.Web.UI.Page
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
                    dsStudent.Merge(new student_webService().check_student_InfoNew(sid));
                   // dsStudent.Merge(new student_webService().check_student_Info(sid));
                    foreach (DataRow dr in dsStudent.Tables["STUDENT"].Rows)
                    {
                        Session["sName"] = dr["SNAME"].ToString();
                        Session["semail"] = dr["mail"].ToString();
                        Session["sphone"] = dr["PHONE"].ToString();

                       
                    }

                    lblStdName.Text = Convert.ToString(Session["sName"]);
                    txtCusMobile.Text = Convert.ToString(Session["sphone"]);
                    txtCusEmail.Text = Convert.ToString(Session["semail"]);
                  
                  
                    //TextBox txtAmountTotal = e.Row.FindControl("AmountTotal") as TextBox;
                    //txtAmountTotal.Attributes.Add("onload", "javascript:return GrandTotal();");
                }
                
            }
        }
        catch (Exception exp) { Response.Redirect("../_login.aspx"); }



    }


    private void load_Grid()
    {
        string Year = "", semsester = "";
        Year = Convert.ToString(txtYear.Text);
        semsester = cmb_semester.SelectedValue.ToString();

        DataSet ds = new DataSet();

        ds.Merge(new admin_webService().get_STUDENT_HEADINFO_NEW(Year, semsester));
        GridView4.DataSource = ds;
        GridView4.DataMember = "STUDENTHEADINFO";
        GridView4.DataBind();

    }

    private void PayPayemntOffline()
    {
        Label5.Text = "";

        decimal Gross = 0, total = 0, b = 0, poss = 0;
        string tempMarks = "", HEADSN = "", HEADNAME = "";

        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_STUDENT_HEADINFO());
        // ds.Tables["STUDENTHEADINFO"].Columns.Add("YAER");
        //ds.Tables["STUDENTHEADINFO"].Columns.Add("SEMESTER");
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

            HEADNAME = Convert.ToString(((TextBox)row.FindControl("HEADNAME")).Text);

            // (TextBox)row.Rows[i].Cells[1].FindControl("TextBox3");
            b = Convert.ToDecimal(((TextBox)row.FindControl("Amount")).Text);

            DateTime today = DateTime.Today;
            string s_today = today.ToString("MM/dd/yyyy");
            today = Convert.ToDateTime(s_today);

            if (HEADNAME == "Registration Fee" && b > 0)
            {
                if (Convert.ToInt32(Convert.ToString(((TextBox)row.FindControl("YEAR")).Text) + Convert.ToString(((DropDownList)row.FindControl("ddlsemester")).SelectedValue)) >= 20193)
                {
                    if (HEADNAME == "Registration Fee" && b < 3000)
                    {
                        Label5.Text = "Registration Fee is not less than 3000 tk";
                        poss = 1;
                        break;

                    }
                    else
                        if (HEADNAME == "Registration Fee" && b > 3000)
                        {
                            Label5.Text = "Registration Fee is not more than 3000 tk";
                            poss = 1;
                            break;
                        }
                        else
                            if (HEADNAME == "Registration Fee" && b == 3000)
                            {
                                drn["SID"] = "" + lblStdID.Text;
                                if (Convert.ToString(((TextBox)row.FindControl("YEAR")).Text) != "")
                                    drn["YEAR"] = "" + Convert.ToString(((TextBox)row.FindControl("YEAR")).Text);
                                else
                                {
                                    lbl_message.Text = "Please enter your year value.";
                                    break;
                                }//txtYear.Text;

                                drn["SEMESTER"] = "" + Convert.ToString(((DropDownList)row.FindControl("ddlsemester")).SelectedValue);
                                //cmb_semester.SelectedValue.ToString();
                                // string selectvalue = uxFrontLineCostddlst.SelectedValue;
                                drn["HEADSN"] = "" + Convert.ToString(((Label)row.FindControl("lblHEADSN")).Text);
                                drn["AMOUNT"] = "" + Convert.ToString(((TextBox)row.FindControl("Amount")).Text);
                                drn["STATUS"] = "I";
                                drn["PDATE"] = "" + today;


                                ds.Tables["T_STUDENTDEBIT"].Rows.Add(drn);
                            }
                }
                else
                {

                    if (HEADNAME == "Registration Fee" && b < 2000)
                    {
                        Label5.Text = "Registration Fee is not less than 2000 tk";
                        poss = 1;
                        break;

                    }
                    else
                        if (HEADNAME == "Registration Fee" && b > 2000)
                        {
                            Label5.Text = "Registration Fee is not more than 2000 tk";
                            poss = 1;
                            break;
                        }
                        else
                            if (HEADNAME == "Registration Fee" && b == 2000)
                            {
                                drn["SID"] = "" + lblStdID.Text;
                                if (Convert.ToString(((TextBox)row.FindControl("YEAR")).Text) != "")
                                    drn["YEAR"] = "" + Convert.ToString(((TextBox)row.FindControl("YEAR")).Text);
                                else
                                {
                                    lbl_message.Text = "Please enter your year value.";
                                    break;
                                }//txtYear.Text;

                                drn["SEMESTER"] = "" + Convert.ToString(((DropDownList)row.FindControl("ddlsemester")).SelectedValue);
                                //cmb_semester.SelectedValue.ToString();
                                // string selectvalue = uxFrontLineCostddlst.SelectedValue;
                                drn["HEADSN"] = "" + Convert.ToString(((Label)row.FindControl("lblHEADSN")).Text);
                                drn["AMOUNT"] = "" + Convert.ToString(((TextBox)row.FindControl("Amount")).Text);
                                drn["STATUS"] = "I";
                                drn["PDATE"] = "" + today;


                                ds.Tables["T_STUDENTDEBIT"].Rows.Add(drn);
                            }
                }

            }
            else
            {
                if (b > 0)
                {


                    drn["SID"] = "" + lblStdID.Text;
                    if (Convert.ToString(((TextBox)row.FindControl("YEAR")).Text) != "")
                        drn["YEAR"] = "" + Convert.ToString(((TextBox)row.FindControl("YEAR")).Text);
                    else
                    {
                        lbl_message.Text = "Please enter your year value.";
                        break;
                    }//txtYear.Text;

                    drn["SEMESTER"] = "" + Convert.ToString(((DropDownList)row.FindControl("ddlsemester")).SelectedValue);
                    //cmb_semester.SelectedValue.ToString();
                    // string selectvalue = uxFrontLineCostddlst.SelectedValue;
                    drn["HEADSN"] = "" + Convert.ToString(((Label)row.FindControl("lblHEADSN")).Text);
                    drn["AMOUNT"] = "" + Convert.ToString(((TextBox)row.FindControl("Amount")).Text);
                    drn["STATUS"] = "I";
                    drn["PDATE"] = "" + today;


                    ds.Tables["T_STUDENTDEBIT"].Rows.Add(drn);

                }

                total = total + b;
            }
            /* if ((HEADNAME == "Registration Fee" && b == 3000) || HEADNAME != "Registration Fee")
             {
                    
             }*/


        }


        if (poss != 1)
        {
            string TRANID = new admin_webService().insert_final_stdDebitOffline(ds);


            string myname = TRANID;
            Session["TRAN_ID"] = TRANID;

            string Year = "", Sem = "", Total = "";

            DataSet StdDebit = new DataSet();
            StdDebit.Merge(new admin_webService().get_StdPaymentNew_TranID(TRANID));
            foreach (DataRow dr in StdDebit.Tables["T_studentDebit_Amount"].Rows)
            {

                Total = dr["Total_Amount"].ToString();

            }
            Year = Convert.ToString(txtYear.Text);
            Sem = cmb_semester.SelectedItem.ToString();



            Session["Year"] = Convert.ToString(Year);
            Session["Semister"] = Convert.ToString(Sem);
            Session["Total_Amount"] = Convert.ToString(Total);

            if (String.IsNullOrEmpty(Session["ctrlId"].ToString()) ||
                String.IsNullOrEmpty(Session["TRAN_ID"].ToString()) ||
                String.IsNullOrEmpty(Session["Total_Amount"].ToString()) ||
               String.IsNullOrEmpty(Session["Year"].ToString()) ||
               String.IsNullOrEmpty(Session["Semister"].ToString())
                )
            {
                lbl_message.Text = "To Pay Your Payment firstly Select 'Year' , 'Semister' and payable 'Particulars Amount'.";
            }
            else
            {
                Response.Redirect("_OfflinePayment.aspx");
                Clear();
            }
        }
    }

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        //check duplicate reg fee
        decimal amount = 0;
        string SID_chk = "", YEAR = "", SEM = "", HEADSN_Chk = "", REG_COUNT = "";

        for (int j = 0; j < (GridView4.Rows.Count); j++)
        {
            GridViewRow row1 = GridView4.Rows[j];

            SID_chk = lblStdID.Text;

            amount = Convert.ToDecimal(((TextBox)row1.FindControl("Amount")).Text);

            HEADSN_Chk = Convert.ToString(((Label)row1.FindControl("lblHEADSN")).Text);

            if (Convert.ToString(((TextBox)row1.FindControl("YEAR")).Text) != "")
                YEAR = Convert.ToString(((TextBox)row1.FindControl("YEAR")).Text);

            SEM = Convert.ToString(((DropDownList)row1.FindControl("ddlsemester")).SelectedValue);

            if (amount > 0 )
            {
                if (HEADSN_Chk == "2")
                {
                    DataSet Chk_ds = new DataSet();
                    Chk_ds.Merge(new admin_webService().get_STUDENT_REGINFO(SID_chk, HEADSN_Chk, YEAR, SEM));

                    if (Chk_ds.Tables["STUDENTREGINFO"].Rows.Count > 0)
                    {
                        foreach (DataRow drCAT in Chk_ds.Tables["STUDENTREGINFO"].Rows)
                        {
                            REG_COUNT = drCAT["REG_COUNT"].ToString();
                        }


                        if (Convert.ToInt32(REG_COUNT) > 0)
                        {
                            Label5.Text = "You already have paid Registration Fee for this selected Year & Semester";
                            break;
                        }
                        else
                        {
                            PayPayemntOffline();

                        }
                    }
                }
                else
                {

                    PayPayemntOffline();
                }
                

                
                // else

            }

        }










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
        string year = txtYear.Text;
        string sem = cmb_semester.SelectedValue.ToString();
        ViewDue();
        load_StdAmountGrid(sid, sem, year);
        load_Grid();
        pnlStdAmount.Visible = true;
       
        /*if (txtYear.Text == "" || cmb_semester.SelectedValue.ToString() == "0")
        {
            lbl_message.Text = "Please select Payment Year & Semester.";
        }
        else
        {
            
        }*/
    }
    private void ViewDue()
    {
        string P_Year = "", prevSem = "", R_Sem = "", R_Year = "";
        string DUE = "", SemDue = "", graceAmt = "";

        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_ADMINREGISTRATIONRATE_LYS());

        if (ds.Tables["Studentlist"].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables["Studentlist"].Rows)
            {
                prevSem = dr["semester"].ToString();
                P_Year = dr["year"].ToString();
            }
        }

        DataSet InsDate_ds = new DataSet();
        InsDate_ds.Merge(new student_webService().get_FN_GET_PER_SEM_DUE(sid, P_Year, prevSem));
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

        if (Convert.ToDecimal(SemDue) >= 0)
        lblDue.Text = "Due upto current date is: " + SemDue;
        else
            lblDue.Text = "Due upto current date is: 0" ;

    }
    private void load_StdAmountGrid(string sid, string sem, string year)
    {
        //DataSet ds = new DataSet();
        //ds.Merge(new student_webService().get_StdBalance_Year_Semister(sid, sem, year));
        //grdStdAmount.DataSource = ds;
        //grdStdAmount.DataMember = "Balance";
        //grdStdAmount.DataBind();

        lblCusName.Text = Convert.ToString(Session["sName"]);
        lblEmail.Text = Convert.ToString(Session["semail"]);
        lblCusMbl.Text = Convert.ToString(Session["sphone"]);

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       //lblErrmsg.Text= "This Service is down for today. Sorry for the Inconvenience!";
        if (txtCusMobile.Text != "" && txtCusEmail.Text != "")
        {
           
           string SName= Convert.ToString(lblStdName.Text);
           string Mobile = Convert.ToString(txtCusMobile.Text);
           string Mail = Convert.ToString(txtCusEmail.Text);

          
            if (new student_webService().Update_studentInfo(sid, Mobile, Mail) == "1")
            {
                Session["semail"] = Mail;
                Session["sphone"] = Mobile;
                pnlStdPaymentView.Visible = true;
                //load_Grid();
                pnlCheckInfo.Visible = false;
                pnlStdAmount.Visible = false;
                pnlStudent.Visible = true;
                ssl_pay.Visible = false;
                pnlStdAmount.Visible = false;
            }
           // string count = new student_webService().Update_studentInfo(ds);

          
        }
        else
        {
            lblErrmsg.Text = "Please enter your email id & contact no";
        }
    }
}
