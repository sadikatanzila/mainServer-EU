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

public partial class Convocation_EUAnniversayOnlinePayment : System.Web.UI.Page
{
    string sid = "", TRAN_ID="" ;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("_RegistrationAC.aspx");
            else
                if ( String.IsNullOrEmpty(Session["ANNICELID"].ToString()) 
                    || String.IsNullOrEmpty(Session["TRAN_ID"].ToString()) 
                    || String.IsNullOrEmpty(Session["Total_Amount"].ToString())
                    || String.IsNullOrEmpty(Session["year"].ToString())
                    || String.IsNullOrEmpty(Session["Sem"].ToString())
                     || String.IsNullOrEmpty(Session["semail"].ToString())
                    || String.IsNullOrEmpty(Session["sphone"].ToString()) 
                    )
                {
                    sid = Session["ANNICELID"].ToString();
                    Response.Redirect("_RegistrationAC.aspx");
                }
                else
                {
                    sid = Session["ANNICELID"].ToString();
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

                      //  pnlStudent.Visible = true;

                    }

                }
        }
        catch (Exception exp) { Response.Redirect("_RegistrationAC.aspx"); }



    }


    private void loaddata()
    {
        TRAN_ID = Convert.ToString(Session["TRAN_ID"]);

        //   load_Grid(TRAN_ID);

        tran_id.Value = Convert.ToString(TRAN_ID);
        total_amount.Value = Convert.ToString(Session["Total_Amount"]);
        value_b.Value = Convert.ToString(Session["year"]);
        value_c.Value = Convert.ToString(Session["SemN"]);
        value_d.Value = Convert.ToString(Session["sName"]);
        value_a.Value = Convert.ToString(sid);

        cus_name.Value = Convert.ToString(Session["sName"]);
        cus_phone.Value = Convert.ToString(Session["sphone"]);
        cus_email.Value = Convert.ToString(Session["semail"]);
        lblRegFee.Text = Convert.ToString(Session["Total_Amount"]);
    }

 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Year = "", Semister = "", Vourcher = "", HEADSN = "";
        Year = Convert.ToString(Session["year"]);
        Semister = Convert.ToString(Session["Sem"]);
        sid = Convert.ToString(Session["ANNICELID"]);
        Vourcher = Convert.ToString(txtVourcher.Text);
        HEADSN = Convert.ToString(33);
        TRAN_ID = Convert.ToString(Session["TRAN_ID"]);

        DataSet StdDebit = new DataSet();
        StdDebit.Merge(new student_webService().get_ConSTUDENT_Vourcher(Year, Semister, sid, Vourcher, HEADSN));

        if (StdDebit.Tables["STUDENTDEBIT"].Rows.Count > 0)
        {
            //
            string UpstrudentInfo = new student_webService().UpEU_Convocation(Vourcher, sid, TRAN_ID);

            if (Convert.ToInt32(UpstrudentInfo) > 0)
            {
                string DelT_stdbt = new student_webService().delete_T_StudentDebit(sid, HEADSN);

                if (Convert.ToInt32(DelT_stdbt) > 0)
                {
                    //delete from T_student debit
                    lbl_Confirm.Text = "Payment has been completed Sucessfully";
                    pnlCong.Visible = true;
                }


                else
                {
                    string UpstrudentInfo_Fail = new student_webService().UpEU_ConvocationFail(Vourcher, sid, TRAN_ID);
                    pnlCong.Visible = false;
                }
            }
        }
        else
        {
            lbl_Confirm.Text = "Submit Your Correct Vourcher No!";
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Year = "", Semister = "", Vourcher = "", HEADSN = "", ProbGr = "";
        int amount = 0;
        Year = Convert.ToString(Session["year"]);
        Semister = Convert.ToString(Session["Sem"]);
        sid = Convert.ToString(Session["ANNICELID"]);
        HEADSN = Convert.ToString(33);
        TRAN_ID = Convert.ToString(Session["TRAN_ID"]);


        DataSet StdDebit = new DataSet();
       // StdDebit.Merge(new student_webService().match_STUDENTDEBIT(Year, Semister, sid, HEADSN));
        StdDebit.Merge(new student_webService().match_STUDENTDEBITN(Year, Semister, sid, HEADSN));

        if (StdDebit.Tables["STUDENTDEBIT_NEW"].Rows.Count > 0)
        {
            foreach (DataRow dr in StdDebit.Tables["STUDENTDEBIT_NEW"].Rows)
            {

                //Vourcher = dr["VOUCARNO"].ToString();
                amount = Convert.ToInt32(dr["AMOUNT"]);
                ProbGr = dr["status"].ToString();

                if ((ProbGr == "Graduate" && amount >= Convert.ToInt32(2000)) || (ProbGr == "Probable" && amount >= Convert.ToInt32(500)))
                {
                    DataSet StdDebit1 = new DataSet();
                    StdDebit1.Merge(new student_webService().match_STUDENTDEBIT(Year, Semister, sid, HEADSN));
                  
                    
                    foreach (DataRow dr1 in StdDebit1.Tables["STUDENTDEBIT"].Rows)
                    {
                        Vourcher = dr1["VOUCARNO"].ToString();
                        string UpstrudentInfo = new student_webService().UpStd_Convocation(Vourcher, sid);

                        if (Convert.ToInt32(UpstrudentInfo) > 0)
                        {
                            string DelT_stdbt = new student_webService().delete_T_StudentDebit(sid, HEADSN);

                            if (Convert.ToInt32(DelT_stdbt) > 0)
                            {
                                //delete from T_student debit
                                lbl_Confirm.Text = "Payment has been completed Sucessfully";
                                pnlCong.Visible = true;
                            }


                            else
                            {
                                string UpstrudentInfo_Fail = new student_webService().UpEU_ConvocationFail(Vourcher, sid, TRAN_ID);
                                lbl_Confirm.Text = "Payment is not updated, please try again";
                                pnlCong.Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    lbl_Confirm.Text = "Please Fullfill your Convocation related Payment and try again.";
                }


                
            }
        }
        else
        {
            lbl_Confirm.Text = "Payment is not completed";
        }
    }
}
