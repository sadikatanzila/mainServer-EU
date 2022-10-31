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

public partial class student_finance_accountLedger : System.Web.UI.Page
{
    string sid = "";
    student_webService obj_student = new student_webService();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
            {
                Response.Redirect("../_login.aspx");
            }
            else
            {
                sid = Session["ctrlId"].ToString();
            }
        }
        catch (Exception exp) { Response.Redirect("../_login.aspx"); }
        loadStudentinfo();
        load_ledget();
    }

    private void loadStudentinfo()
    {
        String SID = Session["ctrlId"].ToString();

        DataSet final = new DataSet();
        final.Merge(new staff_webService().getStudentinfo(sid));
        foreach (DataRow dr2 in final.Tables["Studentinfo"].Rows)
        {
            lblName.Text = dr2["Sname"].ToString();
            lblProgram.Text = dr2["Program"].ToString();
            lblCRate.Text = dr2["Credit_Rate"].ToString();
            lblSID.Text = (dr2["SID"].ToString());

        }
    }

    private void load_ledget()
    {
        Table tbl = new Table();
        tbl.BackImageUrl = "../../images1/bg_studentLedger.jpg";
        PlaceHolder1.Controls.Add(tbl);

        tbl.GridLines = GridLines.Both;

        tbl.Width = new Unit("100%");
        tbl.CellPadding = 0;
        tbl.BorderColor = System.Drawing.Color.Black;
        tbl.CellSpacing = 0;
        tbl.BorderWidth = new Unit(1);

        TableRow tr_header = new TableRow();
        tbl.Controls.Add(tr_header);

        TableCell td_receivable = new TableCell();
        td_receivable.Width = new Unit("8%");
        tr_header.Controls.Add(td_receivable);

        TableCell td_date = new TableCell();
        td_date.Width = new Unit("9%");
        td_date.Font.Bold = true;
        td_date.HorizontalAlign = HorizontalAlign.Center;
        td_date.Text = "Date";
        tr_header.Controls.Add(td_date);

        TableCell td_particular = new TableCell();
        td_particular.Width = new Unit("28%");
        td_particular.Font.Bold = true;
        td_particular.HorizontalAlign = HorizontalAlign.Center;
        td_particular.Text = "Particulars";
        tr_header.Controls.Add(td_particular);

        TableCell td_credit = new TableCell();
        td_credit.Width = new Unit("8%");
        td_credit.Font.Bold = true;
        td_credit.HorizontalAlign = HorizontalAlign.Center;
        td_credit.Text = "Credit";
        tr_header.Controls.Add(td_credit);

        TableCell td_MRNo = new TableCell();
        td_MRNo.Width = new Unit("10%");
        td_MRNo.Font.Bold = true;
        td_MRNo.HorizontalAlign = HorizontalAlign.Center;
        td_MRNo.Text = "MR No.";
        tr_header.Controls.Add(td_MRNo);

        TableCell td_amount = new TableCell();
        td_amount.Width = new Unit("8%");
        td_amount.Font.Bold = true;
        td_amount.HorizontalAlign = HorizontalAlign.Center;
        td_amount.Text = "Amount";
        tr_header.Controls.Add(td_amount);

        TableCell td_adjustment = new TableCell();
        td_adjustment.Width = new Unit("8%");
        td_adjustment.Font.Bold = true;
        td_adjustment.HorizontalAlign = HorizontalAlign.Center;
        td_adjustment.Text = "Adjustment";
        tr_header.Controls.Add(td_adjustment);

        TableCell td_loan = new TableCell();
        td_loan.Width = new Unit("5%");
        td_loan.Font.Bold = true;
        td_loan.HorizontalAlign = HorizontalAlign.Center;
        td_loan.Text = "Loan";
        tr_header.Controls.Add(td_loan);

        TableCell td_waiver = new TableCell();
        td_waiver.Width = new Unit("6%");
        td_waiver.Font.Bold = true;
        td_waiver.HorizontalAlign = HorizontalAlign.Center;
        td_waiver.Text = "Waiver";
        tr_header.Controls.Add(td_waiver);

        TableCell td_total = new TableCell();
        td_total.Width = new Unit("8%");
        td_total.Font.Bold = true;
        td_total.HorizontalAlign = HorizontalAlign.Center;
        td_total.Text = "Total";
        tr_header.Controls.Add(td_total);

        TableCell td_balance = new TableCell();
        td_balance.Width = new Unit("8%");
        td_balance.Font.Bold = true;
        td_balance.HorizontalAlign = HorizontalAlign.Center;
        td_balance.Text = "Balance";
        tr_header.Controls.Add(td_balance);

        DataSet ds = new DataSet();
        ds.Merge(obj_student.get_allRegistred_semesters_ofA_student_for_account(sid));
        ds.Merge(obj_student.get_all_Debit_semesters_ofA_student(sid));

        ds.Tables["registration"].Columns.Add("isRegi");
        foreach (DataRow dr in ds.Tables["registration"].Rows)
        {
            for (int p = 0; p < ds.Tables["DebitSemester"].Rows.Count; p++)
            {
                if ((ds.Tables["DebitSemester"].Rows[p]["YEAR"].ToString() == dr["YEAR"].ToString()) && (ds.Tables["DebitSemester"].Rows[p]["SEMESTER"].ToString() == dr["SEMESTER"].ToString()))
                {
                    ds.Tables["DebitSemester"].Rows.RemoveAt(p--);
                }
            }
            dr["isRegi"] = "1";
        }

        foreach (DataRow dr in ds.Tables["DebitSemester"].Rows)
        {
            DataRow drN = ds.Tables["registration"].NewRow();
            drN["SID"] = dr["SID"].ToString();
            drN["SEMESTER"] = dr["SEMESTER"].ToString();
            drN["YEAR"] = dr["YEAR"].ToString();
            drN["REGKEY"] = dr["SID"].ToString() + dr["SEMESTER"].ToString() + dr["YEAR"].ToString();
            drN["isRegi"] = "0";

            ds.Tables["registration"].Rows.Add(drN);
        }
        //int l = ds.Tables["DebitSemester"].Rows.Count;

        int i = 0;
        int rowSpan = 0;
        double regFee = 0;
        double adjustment = 0;
        int loan = 0;
        int waiver = 0;
        double tuitionFee = 0;
        double totaPable = 0;
        int k = 0;
        double paid = 0;
        double totalCredit = 0;
        double total_loan = 0;
        double total_waiver = 0;
        double total_paid = 0;
        double total_payable = 0;
        double credt = 0;
        double semDevelopment = 0, semExtracurricularFee = 0, semLabFee = 0, semLibraryFee = 0;
        bool isStuValidForDevFee = false;
        int index = 0;

        /*if ((int.Parse(ds.Tables["registration"].Rows[0]["YEAR"].ToString()) >= 2011 && int.Parse(ds.Tables["registration"].Rows[0]["SEMESTER"].ToString()) >= 2) || int.Parse(ds.Tables["registration"].Rows[0]["YEAR"].ToString()) >= 2012)
        {
            isStuValidForDevFee = true;
        }*/
        string adminYearSem = obj_student.get_year_and_semester_ofA_student(sid);

        foreach (DataRow dr in ds.Tables["registration"].Rows)
        {
            rowSpan = 0;
            regFee = 0;
            adjustment = 0;
            loan = 0;
            waiver = 0;
            tuitionFee = 0;
            totaPable = 0;
            k = 0;
            credt = 0;
            semDevelopment = 0;
            semExtracurricularFee = 0;
            semLabFee = 0;
            semLibraryFee = 0;

            TableRow tr_semester = new TableRow();
            tr_semester.BackColor = System.Drawing.Color.LightGray;
            tbl.Controls.Add(tr_semester);

            TableCell td_semester = new TableCell();
            td_semester.ColumnSpan = 10;
            td_semester.Font.Bold = true;
            td_semester.BorderColor = System.Drawing.Color.Black;
            td_semester.Text = "" + new cls_tools().get_word_semester(dr["SEMESTER"].ToString()) + ", " + dr["YEAR"].ToString();
            tr_semester.Controls.Add(td_semester);

            

            //----------- Payable-------------------------------------------------------------

            if (i == 0)
            {
                ds.Merge(obj_student.get_admission_creditNew(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
                if (ds.Tables["ADMISSIONCREDIT"].Rows.Count > 0)
                    totaPable = Convert.ToDouble("0" + ds.Tables["ADMISSIONCREDIT"].Rows[0]["ADMINFEE"].ToString());
                rowSpan = 1; // Admission  
                i++;
            }

            int STATUS =  new student_webService().get_Status(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString());

            if (STATUS == 0)
            {
               // isStuValidForDevFee = false;
            }
            else
            {
                if (int.Parse(adminYearSem) >= 20121)
                {
                    semDevelopment = 2000;
                    rowSpan += 1;
                }

                else
                    if (int.Parse(adminYearSem) >= 20112)
                    {
                        semDevelopment = 1000;
                        rowSpan += 1;
                    }

                if (int.Parse(adminYearSem) >= 20123)
                {
                    semExtracurricularFee = 1000;
                    semLabFee = 500;
                    semLibraryFee = 500;
                    rowSpan += 3;
                }
            }
            
            
            
            
            
            
            
            
            /*if (isStuValidForDevFee)
            {
                //if((int.Parse(dr["YEAR"].ToString()) >= 2011 && int.Parse(dr["SEMESTER"].ToString()) >= 2) || int.Parse(dr["YEAR"].ToString()) >= 2012)
                if (int.Parse(dr["YEAR"].ToString() + dr["SEMESTER"].ToString()) >= 20121)
                {
                    semDevelopment = 2000;
                }
                else if (int.Parse(dr["YEAR"].ToString() + dr["SEMESTER"].ToString()) >= 20112)
                {
                    semDevelopment = 1000;
                }
                rowSpan += 1;
            }*/

            ds.Merge(obj_student.get_registration_credit(dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            rowSpan += 1;// ds.Tables["ADMINREGISTRATIONRATE"].Rows.Count; //  registration            

            ds.Merge(obj_student.get_tuition_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            rowSpan += ds.Tables["STUDENTCREDIT"].Rows.Count;
            if (ds.Tables["STUDENTCREDIT"].Rows.Count > 0)
            {
                //if (dr["SEMESTER"].ToString() == "1" && dr["YEAR"].ToString() == "2005")
                //{
                if (!String.IsNullOrEmpty(ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString()) 
                    && !String.IsNullOrEmpty(ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString()))
                {
                    adjustment = Convert.ToDouble(ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString());
                    tuitionFee = Convert.ToDouble(ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString());
                }
                else
                {
                    adjustment = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString());
                    tuitionFee = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString());
                }
                //}
                //else
                //{
                //    adjustment = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString());
                //    tuitionFee = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString());
                //}


            }

            /*if (true)
            {*/
                double tmpRegFee = 0;
                foreach (DataRow drRR in ds.Tables["ADMINREGISTRATIONRATE"].Rows)
                {
                    tmpRegFee = Convert.ToDouble("0" + drRR["REGRATE"].ToString());
                }

                regFee = obj_student.get_registration_credit_by_id(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString());

                regFee = (regFee > 0 ? (regFee > tmpRegFee ? regFee : tmpRegFee) : 0);
            /*}
            else
            {
                foreach (DataRow drRR in ds.Tables["ADMINREGISTRATIONRATE"].Rows)
                {
                    regFee = Convert.ToDouble("0" + drRR["REGRATE"].ToString());
                }
            }*/
            /*if (tuitionFee > 0)//(dr["isRegi"].ToString() == "1")
                foreach (DataRow drRR in ds.Tables["ADMINREGISTRATIONRATE"].Rows)
                {
                    regFee = Convert.ToDouble("0" + drRR["REGRATE"].ToString());
                }
            else
                regFee = 0;*/

            ds.Merge(obj_student.get_loan_waiver_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            foreach (DataRow drL in ds.Tables["LOANSANDWAIVER"].Rows)
            {
                loan = Convert.ToInt32("0" + drL["LOAN"].ToString());
                waiver = Convert.ToInt32("0" + drL["WAIVER"].ToString());
            }

            if (tuitionFee > 0)
            {
                total_loan += Convert.ToInt32((((tuitionFee - regFee) * loan) / 100));
                // td_loans.Text = "" + Convert.ToInt32((((tuitionFee - regFee) * loan) / 100));
            }
            else
            {
                total_loan += 0;
                //td_loans.Text = "0";
            }

            if (tuitionFee > 0)
            {
                total_waiver += Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100));
                // td_waivers.Text = "" + Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100)); ;
            }
            else
            {
                total_waiver += 0;
                //td_waivers.Text = "0";
            }

            //tr_registration.Controls.Add(td_waivers);
            // aa;

            if (tuitionFee > 0)
            {
                totaPable += tuitionFee - ((((tuitionFee - regFee) * loan) / 100) + (((tuitionFee - regFee) * waiver) / 100) + adjustment);
            }
            else if (tuitionFee < 0)
            {
                totaPable += tuitionFee;// -regFee;
            }
            else
            {
                totaPable += regFee;// -adjustment;
            }

            totaPable += (semDevelopment + semExtracurricularFee + semLabFee + semLibraryFee);

            //if (semDevelopment > 0)
            //{
            //    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
            //    tdr["amount"] = 1000;
            //    tdr["HEADNAME"] = "Development Fee";
            //    tdr["HEADSN"] = "3";
            //    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
            //}

            cls_tools obj_tools = new cls_tools();
            ds.Merge(obj_student.get_semester_debit_ofA_student(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));

            paid = 0;
            foreach (DataRow drD in ds.Tables["STUDENTDEBIT"].Rows)
            {
                paid += Convert.ToDouble("0" + drD["AMOUNT"].ToString());
            }

            ds.Merge(obj_student.get_lateFee_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            //******************************************************  late fee for unpaid taka
            DateTime dt_firstIns = new DateTime();
            DateTime dt_secondIns = new DateTime();
            DateTime dt_thirdIns = new DateTime();
            DateTime dt_registration = new DateTime();
            DateTime dt_end = DateTime.Parse("30-SEP-2010");

            //*******************late fine section
            if (int.Parse(dr["YEAR"].ToString()) >= 2008)
            {
                DataTable fdt = new DataTable();
                fdt.Merge(obj_student.get_last_dates_of_payment(dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));

                if (fdt.Rows.Count > 0)
                {
                    dt_firstIns = Convert.ToDateTime(fdt.Rows[0]["INSONEDATE"]);
                    dt_secondIns = Convert.ToDateTime(fdt.Rows[0]["INSTWODATE"]);
                    dt_thirdIns = Convert.ToDateTime(fdt.Rows[0]["INSTHREEDATE"]);
                    dt_registration = Convert.ToDateTime(fdt.Rows[0]["REGISTRATIONDATE"]);
                }
            }
            

            {
                //(semCredit-adjustment-loan-waiver-semRegFee)
                double totaReceivable = tuitionFee - adjustment - Convert.ToInt32((((tuitionFee - regFee) * loan) / 100)) - Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100)) - regFee;


                if ((int.Parse(dr["YEAR"].ToString()) == 2010 && int.Parse(dr["SEMESTER"].ToString()) == 3) || int.Parse(dr["YEAR"].ToString()) > 2010)
                {
                    int fc = 0;
					foreach (DataRow drD in ds.Tables["STUDENTDEBIT"].Rows)
                    {
                        if (drD["HEADSN"].ToString() == "32")
                        {
                            DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                            tdr["AMOUNT"] = drD["AMOUNT"];
                            tdr["HEADNAME"] = drD["HEADNAME"];
                            tdr["HEADSN"] = drD["HEADSN"]; fc++;
                            ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                        }
                    }
                    if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "2"))
                    //if (DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "2"))
                    {
                        if (totaReceivable > 0)
                        {
                            if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "8")))
                            //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "8"))
                            {
                                if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                                //if (today.compareTo(dt_firstIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "5"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (1st Inst)";
                                        tdr["HEADSN"] = "25"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                                if (DateTime.Today.CompareTo(dt_secondIns) > 0)
                                //if (today.compareTo(dt_secondIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "6"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (2nd Inst)";
                                        tdr["HEADSN"] = "26"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                                if (DateTime.Today.CompareTo(dt_thirdIns) > 0)
                                //if (today.compareTo(dt_thirdIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "7"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                                        tdr["HEADSN"] = "27"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                            }
                        }//end if( isPaidOfThisHead(idd,yeari,semi,
                    }
                    //li = li + fc;

                }//end if((yeari==2010 && semi==3) ||yeari>2010)

                else if (int.Parse(dr["YEAR"].ToString()) == 2008 && int.Parse(dr["SEMESTER"].ToString()) == 1)
                {
                    int fc = 0;
                    double tempBalanceupto = 0;//balanceUpto;


                    //double paid = new student_webService().GetCurrentSemTotalTutionFeepayment(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString());
                    paid += tempBalanceupto;
                    if (totaReceivable - (paid) > cls_tools.finePlusMinus)
                    {
                        if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                        {
                            DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                            tdr["amount"] = (int)((totaReceivable - paid) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_thirdIns));
                            tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                            tdr["HEADSN"] = "27"; fc++;
                            ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                        }
                    }
                }//  end year=2008 and semester =1
                else if (int.Parse(dr["YEAR"].ToString()) >= 2008)
                {				
                    foreach (DataRow drLateFreeCredit in ds.Tables["LATEFEECREDIT"].Rows)
                    {
                        int head = int.Parse(drLateFreeCredit["HEADSN"].ToString());
                        if (head >= 24 && head <= 27)
                        {
                            drLateFreeCredit["amount"] = int.Parse(drLateFreeCredit["amount"].ToString()) / 2;
                        }
                    }

                    //**************************************for registration
                    int fc = 0;
                    double tempBalanceupto = 0;//balanceUpto;
                    double installmentAmout = totaReceivable / 3;
                    if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "8"))
                    {

                    }
                    else//course fee full not paid
                    {
                        //if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5"))//first inst
                        {
                            if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_firstIns));
                                    tdr["HEADNAME"] = "Late Fine (1st Inst)";
                                    tdr["HEADSN"] = "25"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }
                        // if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6"))//second inst
                        {
                            if (DateTime.Today.CompareTo(dt_secondIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_secondIns));
                                    tdr["HEADNAME"] = "Late Fine (2nd Inst)";
                                    tdr["HEADSN"] = "26"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }
                        // if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7"))//third inst
                        {
                            if (DateTime.Today.CompareTo(dt_thirdIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_thirdIns));
                                    tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                                    tdr["HEADSN"] = "27"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }

                    }//course fee full end                   
                }/// if(Integer.parseInt(syear[j])>=2008 && Integer.parseInt(ssem[j])==1)

            }


            //****************************************************** end late fee for unpaid taka
            rowSpan += ds.Tables["LATEFEECREDIT"].Rows.Count;

            foreach (DataRow drL in ds.Tables["LATEFEECREDIT"].Rows)
                totaPable += Convert.ToDouble(drL["amount"].ToString());


            // ---------- Registration --------------

            TableRow tr_registration = new TableRow();
            tbl.Controls.Add(tr_registration);

            TableCell td_receivables = new TableCell(); // Column Span
            td_receivables.RowSpan = rowSpan;
            // td_receivables.HorizontalAlign = HorizontalAlign.Center;
            td_receivables.Font.Bold = true;
            td_receivables.Text = "<span style=\"border-bottom: 2px #000000; color:Red\">Payable</span>";
            td_receivables.ForeColor = System.Drawing.Color.DodgerBlue;
            tr_registration.Controls.Add(td_receivables);

            TableCell td_datesR = new TableCell();
            td_datesR.Text = "&nbsp;";
            tr_registration.Controls.Add(td_datesR);

            TableCell td_particularsR = new TableCell();
            td_particularsR.Text = "Registration Fee";
            tr_registration.Controls.Add(td_particularsR);

            TableCell td_creditsR = new TableCell();
            td_creditsR.Text = "&nbsp;";
            tr_registration.Controls.Add(td_creditsR);

            TableCell td_mrnR = new TableCell();
            td_mrnR.Text = "&nbsp;";
            tr_registration.Controls.Add(td_mrnR);

            TableCell td_amountsR = new TableCell();
            td_amountsR.Text = "&nbsp;";
            //if (dr["isRegi"].ToString() == "1")
            //    foreach (DataRow drRR in ds.Tables["ADMINREGISTRATIONRATE"].Rows)
            //    {
            //        regFee = Convert.ToDouble("0" + drRR["REGRATE"].ToString());
            //    }
            //else
            //    regFee = 0;

            td_amountsR.Text = "" + regFee;
            td_amountsR.HorizontalAlign = HorizontalAlign.Center;
            tr_registration.Controls.Add(td_amountsR);

            TableCell td_adjustments = new TableCell();  //  Span
            td_adjustments.RowSpan = rowSpan;
            td_adjustments.HorizontalAlign = HorizontalAlign.Center;
            td_adjustments.Text = "" + adjustment;
            tr_registration.Controls.Add(td_adjustments);

            TableCell td_loans = new TableCell();  //  Span
            td_loans.RowSpan = rowSpan;
            td_loans.HorizontalAlign = HorizontalAlign.Center;

            if (tuitionFee > 0)
            {
                ///total_loan += Convert.ToInt32((((tuitionFee - regFee) * loan) / 100));
                td_loans.Text = "" + total_loan;
            }
            else
            {
                // total_loan += 0;
                td_loans.Text = "0";
            }
            tr_registration.Controls.Add(td_loans);


            TableCell td_waivers = new TableCell();   //  Span
            td_waivers.RowSpan = rowSpan;
            td_waivers.HorizontalAlign = HorizontalAlign.Center;

            if (tuitionFee > 0)
            {
                //total_waiver += Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100)); 
                td_waivers.Text = "" + Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100));
            }
            else
            {
                // total_waiver += 0;
                td_waivers.Text = "0";
            }

            tr_registration.Controls.Add(td_waivers);
            // aa;

            //if (tuitionFee > 0)
            //{
            //    totaPable += tuitionFee - ((((tuitionFee - regFee) * loan) / 100) + (((tuitionFee - regFee) * waiver) / 100) + adjustment);
            //}
            //else if (tuitionFee < 0)
            //{
            //    totaPable += tuitionFee;// -regFee;
            //}
            //else
            //{
            //    totaPable += regFee;// -adjustment;
            //}

            TableCell td_totals = new TableCell();   // Span
            td_totals.RowSpan = rowSpan;
            td_totals.HorizontalAlign = HorizontalAlign.Center;
            td_totals.Text = "" + Convert.ToInt32(totaPable);
            tr_registration.Controls.Add(td_totals);

            TableCell td_balancesR = new TableCell();
            td_balancesR.HorizontalAlign = HorizontalAlign.Center;
            td_balancesR.Text = "&nbsp;";
            tr_registration.Controls.Add(td_balancesR);

            foreach (DataRow drTC in ds.Tables["STUDENTCREDIT"].Rows)
            {
                TableRow tr_admissionTC = new TableRow();
                tbl.Controls.Add(tr_admissionTC);

                TableCell td_datesTC = new TableCell();
                td_datesTC.Text = "&nbsp;";
                tr_admissionTC.Controls.Add(td_datesTC);

                TableCell td_particularsTC = new TableCell();
                td_particularsTC.Text = "Tuition Fee";
                tr_admissionTC.Controls.Add(td_particularsTC);

                TableCell td_creditsTC = new TableCell();
                td_creditsTC.HorizontalAlign = HorizontalAlign.Center;
                credt = obj_student.get_semester_creditHrs_ofA_student(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString());
                totalCredit += credt;
                if ((credt % 1) > 0)
                    td_creditsTC.Text = "" + obj_student.get_semester_creditHrs_ofA_student(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString());
                else
                    td_creditsTC.Text = "" + obj_student.get_semester_creditHrs_ofA_student(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()) + ".0";

                tr_admissionTC.Controls.Add(td_creditsTC);

                TableCell td_mrnTC = new TableCell();
                td_mrnTC.HorizontalAlign = HorizontalAlign.Center;
                td_mrnTC.Text = "&nbsp;";
                tr_admissionTC.Controls.Add(td_mrnTC);

                TableCell td_amountsTC = new TableCell();
                td_amountsTC.HorizontalAlign = HorizontalAlign.Center;
                td_amountsTC.Text = "" + (tuitionFee > 0 ? (tuitionFee - regFee) : 0.0);
                tr_admissionTC.Controls.Add(td_amountsTC);

                TableCell td_balancesTC = new TableCell();
                td_balancesTC.Text = "&nbsp;";
                tr_admissionTC.Controls.Add(td_balancesTC);
            }

            // -----------------------Development Fee--------------------------------//
            if (semDevelopment > 0)
            {
                TableRow tr_developmentTD = new TableRow();
                tbl.Controls.Add(tr_developmentTD);

                TableCell td_datesTD = new TableCell();
                td_datesTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_datesTD);

                TableCell td_particularsTD = new TableCell();
                td_particularsTD.Text = "Development Fee";
                tr_developmentTD.Controls.Add(td_particularsTD);

                TableCell td_creditsTD = new TableCell();
                td_creditsTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_creditsTD);

                TableCell td_mrnTD = new TableCell();
                td_mrnTD.HorizontalAlign = HorizontalAlign.Center;
                td_mrnTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_mrnTD);

                TableCell td_amountsTD = new TableCell();
                td_amountsTD.HorizontalAlign = HorizontalAlign.Center;
                td_amountsTD.Text = semDevelopment.ToString();
                tr_developmentTD.Controls.Add(td_amountsTD);

                TableCell td_balancesTD = new TableCell();
                td_balancesTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_balancesTD);
            }
            ///////////////////////////////////////////////////////////////////////
            if (semExtracurricularFee > 0)
            {
                TableRow tr_developmentTD = new TableRow();
                tbl.Controls.Add(tr_developmentTD);

                TableCell td_datesTD = new TableCell();
                td_datesTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_datesTD);

                TableCell td_particularsTD = new TableCell();
                td_particularsTD.Text = "Extracurricular Fee";
                tr_developmentTD.Controls.Add(td_particularsTD);

                TableCell td_creditsTD = new TableCell();
                td_creditsTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_creditsTD);

                TableCell td_mrnTD = new TableCell();
                td_mrnTD.HorizontalAlign = HorizontalAlign.Center;
                td_mrnTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_mrnTD);

                TableCell td_amountsTD = new TableCell();
                td_amountsTD.HorizontalAlign = HorizontalAlign.Center;
                td_amountsTD.Text = semExtracurricularFee.ToString();
                tr_developmentTD.Controls.Add(td_amountsTD);

                TableCell td_balancesTD = new TableCell();
                td_balancesTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_balancesTD);
            }
            if (semLibraryFee > 0)
            {
                TableRow tr_developmentTD = new TableRow();
                tbl.Controls.Add(tr_developmentTD);

                TableCell td_datesTD = new TableCell();
                td_datesTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_datesTD);

                TableCell td_particularsTD = new TableCell();
                td_particularsTD.Text = "Library Fee";
                tr_developmentTD.Controls.Add(td_particularsTD);

                TableCell td_creditsTD = new TableCell();
                td_creditsTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_creditsTD);

                TableCell td_mrnTD = new TableCell();
                td_mrnTD.HorizontalAlign = HorizontalAlign.Center;
                td_mrnTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_mrnTD);

                TableCell td_amountsTD = new TableCell();
                td_amountsTD.HorizontalAlign = HorizontalAlign.Center;
                td_amountsTD.Text = semLibraryFee.ToString();
                tr_developmentTD.Controls.Add(td_amountsTD);

                TableCell td_balancesTD = new TableCell();
                td_balancesTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_balancesTD);
            }
            if (semLabFee > 0)
            {
                TableRow tr_developmentTD = new TableRow();
                tbl.Controls.Add(tr_developmentTD);

                TableCell td_datesTD = new TableCell();
                td_datesTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_datesTD);

                TableCell td_particularsTD = new TableCell();
                td_particularsTD.Text = "Lab Fee";
                tr_developmentTD.Controls.Add(td_particularsTD);

                TableCell td_creditsTD = new TableCell();
                td_creditsTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_creditsTD);

                TableCell td_mrnTD = new TableCell();
                td_mrnTD.HorizontalAlign = HorizontalAlign.Center;
                td_mrnTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_mrnTD);

                TableCell td_amountsTD = new TableCell();
                td_amountsTD.HorizontalAlign = HorizontalAlign.Center;
                td_amountsTD.Text = semLabFee.ToString();
                tr_developmentTD.Controls.Add(td_amountsTD);

                TableCell td_balancesTD = new TableCell();
                td_balancesTD.Text = "&nbsp;";
                tr_developmentTD.Controls.Add(td_balancesTD);
            }
            ///////////////////////////////////////////////////////////////////////////

            //************ Admission fees
            if (ds.Tables["ADMISSIONCREDIT"].Rows.Count > 0)
            {
                // ---- Admission ---------------------
                TableRow tr_admission = new TableRow();
                tbl.Controls.Add(tr_admission);

                TableCell td_datesT = new TableCell();
                td_datesT.Text = "&nbsp;";
                tr_admission.Controls.Add(td_datesT);

                TableCell td_particularsT = new TableCell();
                td_particularsT.Text = "Admission Fee";
                tr_admission.Controls.Add(td_particularsT);

                TableCell td_creditsT = new TableCell();
                td_creditsT.Text = "&nbsp;";
                tr_admission.Controls.Add(td_creditsT);

                TableCell td_mrnT = new TableCell();
                td_mrnT.Text = "&nbsp;";
                tr_admission.Controls.Add(td_mrnT);

                TableCell td_amountsT = new TableCell();
                td_amountsT.HorizontalAlign = HorizontalAlign.Center;
                td_amountsT.Text = "" + ds.Tables["ADMISSIONCREDIT"].Rows[0]["ADMINFEE"].ToString();
                tr_admission.Controls.Add(td_amountsT);

                TableCell td_balancesT = new TableCell();
                td_balancesT.Text = "&nbsp;";
                tr_admission.Controls.Add(td_balancesT);
            }

            foreach (DataRow drL in ds.Tables["LATEFEECREDIT"].Rows)
            {
                TableRow tr = new TableRow();
                tbl.Controls.Add(tr);

                TableCell td_dates = new TableCell();
                td_dates.Text = "&nbsp;";
                tr.Controls.Add(td_dates);

                TableCell td_particulars = new TableCell();
                td_particulars.Text = "" + drL["HEADNAME"].ToString();
                tr.Controls.Add(td_particulars);

                TableCell td_credits = new TableCell();
                td_credits.Text = "&nbsp;";
                tr.Controls.Add(td_credits);

                TableCell td_mrn = new TableCell();
                td_mrn.Text = "&nbsp;";
                tr.Controls.Add(td_mrn);

                TableCell td_amounts = new TableCell();
                td_amounts.HorizontalAlign = HorizontalAlign.Center;
                td_amounts.Text = "" + drL["amount"].ToString();
                tr.Controls.Add(td_amounts);

                TableCell td_balances = new TableCell();
                td_balances.Text = "&nbsp;";
                tr.Controls.Add(td_balances);
            }

            //----------- Paid-------------------------------------------------------------

            TableRow tr_bb = new TableRow();
            tbl.Controls.Add(tr_bb);

            TableCell td_bb = new TableCell();
            td_bb.ColumnSpan = 11;
            td_bb.Height = new Unit(2);
            td_bb.BackColor = System.Drawing.Color.Black;
            tr_bb.Controls.Add(td_bb);

            TableCell td_bb2 = new TableCell();
            td_bb2.BackColor = System.Drawing.Color.Black;
            // tr_bb.Controls.Add(td_bb2);




            // --- End Blank Black ---------------------------------------

            //cls_tools obj_tools = new cls_tools();
            //ds.Merge(obj_student.get_semester_debit_ofA_student(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));

            //paid = 0;            
            //foreach (DataRow drD in ds.Tables["STUDENTDEBIT"].Rows)
            //{
            //    paid+=Convert.ToDouble("0"+drD["AMOUNT"].ToString());
            //}

            foreach (DataRow drD in ds.Tables["STUDENTDEBIT"].Rows)
            {
                TableRow tr_Paid = new TableRow();
                tbl.Controls.Add(tr_Paid);

                if (k == 0)
                {
                    TableCell td_paid = new TableCell(); // Column Span
                    td_paid.RowSpan = ds.Tables["STUDENTDEBIT"].Rows.Count;
                    //td_paid.HorizontalAlign = HorizontalAlign.Center;
                    td_paid.Font.Bold = true;
                    td_paid.ForeColor = System.Drawing.Color.DodgerBlue;
                    td_paid.Text = "Paid";
                    tr_Paid.Controls.Add(td_paid);
                }

                TableCell td_datesP = new TableCell();
                td_datesP.Text = "" + obj_tools.get_user_short_formateDate(drD["PDATE"].ToString());
                tr_Paid.Controls.Add(td_datesP);

                TableCell td_particularsP = new TableCell();
                td_particularsP.Text = "" + drD["VOUCARNO"];
                tr_Paid.Controls.Add(td_particularsP);

                TableCell td_creditsP = new TableCell();
                td_creditsP.Text = "&nbsp;";
                tr_Paid.Controls.Add(td_creditsP);

                TableCell td_mrnP = new TableCell();
                td_mrnP.Text = "" + drD["MRNO"].ToString();
                td_mrnP.HorizontalAlign = HorizontalAlign.Center;
                tr_Paid.Controls.Add(td_mrnP);

                TableCell td_amountsP = new TableCell();
                td_amountsP.Text = "" + drD["AMOUNT"].ToString();
                td_amountsP.HorizontalAlign = HorizontalAlign.Center;
                tr_Paid.Controls.Add(td_amountsP);

                TableCell td_adjustmentsP = new TableCell();
                td_adjustmentsP.HorizontalAlign = HorizontalAlign.Center;
                td_adjustmentsP.Text = "&nbsp;";
                tr_Paid.Controls.Add(td_adjustmentsP);

                TableCell td_loansP = new TableCell();
                td_loansP.HorizontalAlign = HorizontalAlign.Center;
                td_loansP.Text = "&nbsp;";
                tr_Paid.Controls.Add(td_loansP);

                TableCell td_waiversP = new TableCell();
                td_waiversP.HorizontalAlign = HorizontalAlign.Center;
                td_waiversP.Text = "&nbsp;";
                tr_Paid.Controls.Add(td_waiversP);

                if (k == 0)
                {
                    TableCell td_totalsP = new TableCell();   // Span
                    td_totalsP.RowSpan = ds.Tables["STUDENTDEBIT"].Rows.Count;
                    td_totalsP.HorizontalAlign = HorizontalAlign.Center;
                    td_totalsP.Text = "" + Convert.ToInt32(paid);
                    tr_Paid.Controls.Add(td_totalsP);
                    k++;
                }

                TableCell td_balancesP = new TableCell();
                td_balancesP.HorizontalAlign = HorizontalAlign.Center;
                td_balancesP.Text = "&nbsp;";
                tr_Paid.Controls.Add(td_balancesP);
            }

            //----------------------- Semester Balance ---------------------------------------------

            TableRow tr_Balance = new TableRow();
            tbl.Controls.Add(tr_Balance);

            TableCell td_b1 = new TableCell(); // Column Span          
            td_b1.Text = "&nbsp;";
            tr_Balance.Controls.Add(td_b1);

            TableCell td_datesP1 = new TableCell();
            td_datesP1.Text = "&nbsp;";
            tr_Balance.Controls.Add(td_datesP1);

            TableCell td_particularsP1 = new TableCell();
            td_particularsP1.Text = "&nbsp;";
            tr_Balance.Controls.Add(td_particularsP1);

            TableCell td_creditsP1 = new TableCell();
            td_creditsP1.Text = "&nbsp;";
            tr_Balance.Controls.Add(td_creditsP1);

            TableCell td_mrnP1 = new TableCell();
            td_mrnP1.HorizontalAlign = HorizontalAlign.Center;
            td_mrnP1.Text = "" + "&nbsp;";
            tr_Balance.Controls.Add(td_mrnP1);

            TableCell td_amountsP1 = new TableCell();
            td_amountsP1.Text = "" + "&nbsp;";
            tr_Balance.Controls.Add(td_amountsP1);

            TableCell td_adjustmentsP1 = new TableCell();
            td_adjustmentsP1.Text = "&nbsp;";
            tr_Balance.Controls.Add(td_adjustmentsP1);

            TableCell td_loansP1 = new TableCell();
            td_loansP1.Text = "&nbsp;";
            tr_Balance.Controls.Add(td_loansP1);

            TableCell td_waiversP1 = new TableCell();
            td_waiversP1.Text = "&nbsp;";
            //td_waiversP1.BorderColor = System.Drawing.Color.Black;
            tr_Balance.Controls.Add(td_waiversP1);

            TableCell td_totalsP1 = new TableCell();
            td_totalsP1.Text = "&nbsp;";
            // td_totalsP1.BorderColor = System.Drawing.Color.Black;
            tr_Balance.Controls.Add(td_totalsP1);

            TableCell td_balancesP1 = new TableCell();
            td_balancesP1.HorizontalAlign = HorizontalAlign.Center;
            total_payable += totaPable;
            total_paid += paid;
            td_balancesP1.Text = "" + (Convert.ToInt32(totaPable) - Convert.ToInt32(paid));
            tr_Balance.Controls.Add(td_balancesP1);

            //------------------ clear All ---------------------------------------------------------
            ds.Tables["ADMINREGISTRATIONRATE"].Rows.Clear();
            ds.Tables["LATEFEECREDIT"].Rows.Clear();
            ds.Tables["ADMISSIONCREDIT"].Rows.Clear();
            ds.Tables["STUDENTCREDIT"].Rows.Clear();
            ds.Tables["LOANSANDWAIVER"].Rows.Clear();
            ds.Tables["STUDENTDEBIT"].Rows.Clear();

            index++;
        }

        //----------------------- Semester Balance ---------------------------------------------

        TableRow tr_tt = new TableRow();
        tr_tt.BackColor = System.Drawing.Color.LightGray;
        tbl.Controls.Add(tr_tt);

        TableCell td_b12 = new TableCell(); // Column Span          
        td_b12.Text = "Total";
        td_b12.Font.Bold = true;
        tr_tt.Controls.Add(td_b12);

        TableCell td_datesP12 = new TableCell();
        td_datesP12.Text = "&nbsp;";
        tr_tt.Controls.Add(td_datesP12);

        TableCell td_particularsP12 = new TableCell();
        td_particularsP12.Text = "&nbsp;";
        tr_tt.Controls.Add(td_particularsP12);

        TableCell td_creditsP12 = new TableCell();
        td_creditsP12.Text = "" + totalCredit + ".0";
        td_creditsP12.Font.Bold = true;
        td_creditsP12.HorizontalAlign = HorizontalAlign.Center;
        tr_tt.Controls.Add(td_creditsP12);

        TableCell td_mrnP12 = new TableCell();
        td_mrnP12.HorizontalAlign = HorizontalAlign.Center;
        td_mrnP12.Text = "" + "&nbsp;";
        tr_tt.Controls.Add(td_mrnP12);

        TableCell td_amountsP12 = new TableCell();
        td_amountsP12.Text = "" + "&nbsp;";
        tr_tt.Controls.Add(td_amountsP12);

        TableCell td_adjustmentsP12 = new TableCell();
        td_adjustmentsP12.Text = "&nbsp;";
        tr_tt.Controls.Add(td_adjustmentsP12);

        TableCell td_loansP12 = new TableCell();
        td_loansP12.Text = "" + total_loan;
        td_loansP12.HorizontalAlign = HorizontalAlign.Center;
        if (total_loan > 0)
            td_loansP12.BackColor = System.Drawing.Color.Red;
        td_loansP12.Font.Bold = true;
        tr_tt.Controls.Add(td_loansP12);

        TableCell td_waiversP12 = new TableCell();
        td_waiversP12.Text = "" + total_waiver;
        if (total_waiver > 0)
            td_waiversP12.BackColor = System.Drawing.Color.Green;
        td_waiversP12.HorizontalAlign = HorizontalAlign.Center;
        td_waiversP12.Font.Bold = true;
        tr_tt.Controls.Add(td_waiversP12);

        TableCell td_totalsP12 = new TableCell();
        td_totalsP12.Text = "&nbsp;";
        // td_totalsP1.BorderColor = System.Drawing.Color.Black;
        tr_tt.Controls.Add(td_totalsP12);

        TableCell td_balancesP12 = new TableCell();
        td_balancesP12.HorizontalAlign = HorizontalAlign.Center;
        td_balancesP12.Font.Bold = true;
        if ((Convert.ToInt32(Convert.ToInt32(total_payable) - total_paid)) < 0)
            td_balancesP12.BackColor = System.Drawing.Color.Green;
        else if ((Convert.ToInt32(total_payable) - Convert.ToInt32(total_paid)) > 0)
            td_balancesP12.BackColor = System.Drawing.Color.Red;
        td_balancesP12.Text = "" + (Convert.ToInt32(total_payable) - Convert.ToInt32(total_paid));
        tr_tt.Controls.Add(td_balancesP12);
    }
}
