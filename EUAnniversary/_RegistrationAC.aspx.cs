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

public partial class EUAnniversary_RegistrationAC : System.Web.UI.Page
{

    String SID = "", Birthdate="", stdImg = "";
    string TrunIDC = "";
    string TRANID = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
               // load_Faculty();
            }
            else
            {
              //  faculty_id = cmb_Faculty.SelectedValue.ToString();
            }

        }
        catch (Exception ert) { Response.Redirect("http://webportal.easternuni.edu.bd/EUAnniversary/_RegistrationAC.aspx"); }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        decimal Gross = 0, total = 0, b = 0, GaustNo = 0, GaustFee = 0, RegFee = 0;
        string tempMarks = "", HEADSN = "";


      


     //   Submit1_ServerClick(sender, e);
        //chk img null or empty img

       /* if (Session["picloc"] == null || Session["stdImg"] == null)
        {
           lbl_message.Text += "Please select a image file to upload.";
        }
        else
        {*/

            DataSet ds = new DataSet();

            ds.Tables.Add("EU_CONVOCATION");
            ds.Tables["EU_CONVOCATION"].Columns.Add("SID");
            ds.Tables["EU_CONVOCATION"].Columns.Add("EMAIL");
            ds.Tables["EU_CONVOCATION"].Columns.Add("CONTACT");
            ds.Tables["EU_CONVOCATION"].Columns.Add("PICTURE");
            ds.Tables["EU_CONVOCATION"].Columns.Add("REGISTERFEE");
            ds.Tables["EU_CONVOCATION"].Columns.Add("TOTALFEE");
            ds.Tables["EU_CONVOCATION"].Columns.Add("PDATE");
            ds.Tables["EU_CONVOCATION"].Columns.Add("STATUS");
            ds.Tables["EU_CONVOCATION"].Columns.Add("EMPSTATUS");
            ds.Tables["EU_CONVOCATION"].Columns.Add("HEADSN");
            ds.Tables["EU_CONVOCATION"].Columns.Add("REG_TYPE");
            ds.Tables["EU_CONVOCATION"].Columns.Add("PRESENT_ADDRESS");
            ds.Tables["EU_CONVOCATION"].Columns.Add("YEAR");
            ds.Tables["EU_CONVOCATION"].Columns.Add("SEMESTER");

            ds.Tables["EU_CONVOCATION"].Columns.Add("PAYMENT_YEAR");
            ds.Tables["EU_CONVOCATION"].Columns.Add("PAYMENT_SEMESTER");

            DataRow dr = ds.Tables["EU_CONVOCATION"].NewRow();

            dr["SID"] = "" + lblSID.Text;
            dr["YEAR"] = "" + lblYear.Text;
            dr["SEMESTER"] = "" + lblSem.Text;

            dr["PAYMENT_YEAR"] = "" + "2018";
            dr["PAYMENT_SEMESTER"] = "" + "3";

            dr["EMAIL"] = "" + txtEmail.Text;
            dr["CONTACT"] = "" + txtMobile.Text;
            dr["PICTURE"] = "" + Convert.ToString(Session["SPLOC"]);
            dr["REGISTERFEE"] = "" + Convert.ToDecimal(lblRegFee.Text);
            dr["PDATE"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);
            dr["STATUS"] = "I";
            dr["EMPSTATUS"] = "" + Convert.ToString(ddlEmployment.SelectedValue);
            dr["HEADSN"] = "" + Convert.ToDecimal(33);
            dr["TOTALFEE"] = "" + Convert.ToDecimal(lblRegFee.Text);
            dr["REG_TYPE"] = "" + "A";
            dr["PRESENT_ADDRESS"] = "" + txtAddress.Text;

            ds.Tables["EU_CONVOCATION"].Rows.Add(dr);

            if (btnSubmit.Text == "Submit")
            {
                TRANID = new student_webService().insert_AnniversaryRegistration(ds);
            }
            else
            {
             //   TRANID = Convert.ToString(Session["TrunIDC"]);
                TRANID = new student_webService().update_AnniversaryRegistration(ds,Convert.ToString(Session["TrunIDC"]));
            }


            //----------------------online payment

            string myname = TRANID;
            Session["TRAN_ID"] = TRANID;

            string TotalAmount = "";

            DataSet StdDebit = new DataSet();
            StdDebit.Merge(new student_webService().get_ConSTUDENT_Payment(TRANID));
            foreach (DataRow dr1 in StdDebit.Tables["EU_CONVOCATION"].Rows)
            {
                TotalAmount = dr1["TOTALFEE"].ToString();
                Session["sCONTACT"] = dr1["CONTACT"].ToString();
            }
            Session["Total_Amount"] = Convert.ToString(TotalAmount);
            Session["year"] = Convert.ToString(2018);//Convert.ToString(lbl_DegYear.Text);
            Session["Sem"] = Convert.ToString(3);//Convert.ToString(lblDegSem.Text);
            Session["SemN"] = "Fall";//Convert.ToString(lbl_DegSem.Text);
            Session["sName"] = Convert.ToString(lblName.Text);
            Session["semail"] = Convert.ToString(txtEmail.Text);
            Session["sphone"] = txtMobile.Text;

            if (String.IsNullOrEmpty(Session["sName"].ToString()) ||
                String.IsNullOrEmpty(Session["year"].ToString()) ||
                String.IsNullOrEmpty(Session["Sem"].ToString()) ||
                String.IsNullOrEmpty(Session["SemN"].ToString()) ||
                String.IsNullOrEmpty(Session["semail"].ToString()) ||
                String.IsNullOrEmpty(Session["ANNICELID"].ToString()) ||
                String.IsNullOrEmpty(Session["TRAN_ID"].ToString()) ||
                 String.IsNullOrEmpty(Session["sphone"].ToString()) ||
                String.IsNullOrEmpty(Session["Total_Amount"].ToString()))
            {
                lbl_message.Text = "To Pay Your Payment firstly complete 'Payment Amount' portion.";
            }
            else
            {
                Response.Redirect("_EUAnniversayOnlinePayment.aspx");
            }
       // }
    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
       // btnCancel.Visible = false;
      //  btnCheck.Visible = false;


        SID = Convert.ToString(txtStudentID.Text);
        Session["ANNICELID"] = SID;
        //  Session["CONVOSID"] = SID;
        string Status = "";


        //  Birthdate = Convert.ToString(DateTime.ParseExact(txtDOB.Text, "dd-MMM-yy", CultureInfo.CurrentCulture));
        DataSet dsRegGoing = new DataSet();
        dsRegGoing.Merge(new student_webService().get_RegistrationStudent(SID));

        if (dsRegGoing.Tables["RegGoing_CHK"].Rows.Count > 0)
        {
            foreach (DataRow drRegGoingChk in dsRegGoing.Tables["RegGoing_CHK"].Rows)
            {
                pnlInfoFillup.Visible = true;
                txtAddress.Text = drRegGoingChk["PRESENT_ADDRESS"].ToString();
                txtMobile.Text = drRegGoingChk["CONTACT"].ToString();
                txtEmail.Text = drRegGoingChk["EMAIL"].ToString();
                //  img_myProfile.Visible = true;
                Session["TrunIDC"] = drRegGoingChk["TRANID"].ToString();
                txtAddress.Text = drRegGoingChk["PRESENT_ADDRESS"].ToString();

                lblSID.Text = drRegGoingChk["SID"].ToString();
                lblName.Text = drRegGoingChk["SNAME"].ToString();
                lblProgram.Text = drRegGoingChk["DEPNAME"].ToString();
                lblYear.Text = drRegGoingChk["ADMINYEAR"].ToString();
                lblSem.Text = drRegGoingChk["ADMINSEMETER"].ToString();
                ddlSem.SelectedValue = drRegGoingChk["ADMINSEMETER"].ToString();
               

                txtAddress.ReadOnly = true;
                txtMobile.ReadOnly = true;
                txtEmail.ReadOnly = true;
                if (drRegGoingChk["EMAIL"].ToString() != "")
                    txtEmail.ReadOnly = true;

                btnSubmit.Text = "Update";



                if (Convert.ToInt32(drRegGoingChk["REGISTERFEE"]) ==2000)
                {
                    Status = "G";
                    lblPaymentMsg.Text = "You are a Graduated Student, Your Payment Fee is 2000 tk. ";
                    lblRegFee.Text = "2000";
                }
                else
                {
                    Status = "R";
                    lblPaymentMsg.Text = "You are a Running/Probable Student, Your Payment Fee is 500 tk. ";
                    lblRegFee.Text = "500";
                }
            }

        }


        else
        {
            DataSet dsStudent = new DataSet();
            dsStudent.Merge(new student_webService().check_Student_Reg(SID, txtDOB.Text));//Convert.ToString(DateTime.ParseExact(txtDOB.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture))
            if (dsStudent.Tables["STUDENT"].Rows.Count > 0)
            {
                txtDOB.ReadOnly = true;
                txtStudentID.ReadOnly = true;

                foreach (DataRow drStudent in dsStudent.Tables["STUDENT"].Rows)
                {
                    lblSID.Text = drStudent["SID"].ToString();
                    lblName.Text = drStudent["SNAME"].ToString();
                    lblProgram.Text = drStudent["DEPNAME"].ToString();
                    lblYear.Text = drStudent["ADMINYEAR"].ToString();
                    lblSem.Text = drStudent["ADMINSEMETER"].ToString();
                    ddlSem.SelectedValue = drStudent["ADMINSEMETER"].ToString();

                    if (Convert.ToInt32(drStudent["GRADUATIONYEAR"]) > 0)
                    {
                        Status = "G";
                        lblPaymentMsg.Text = "You are a Graduated Student, Your Payment Fee is 2000 tk. ";
                        lblRegFee.Text = "2000";
                    }
                    else
                    {
                        Status = "R";
                        lblPaymentMsg.Text = "You are a Running/Probable Student, Your Payment Fee is 500 tk. ";
                        lblRegFee.Text = "500";
                    }
                }

                pnlInfoFillup.Visible = true;
            }
            else
            {
                lblErrors.Text = "Please Give your information Correctly";
            }
        }
    }
      

    
    /*
    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);
    }


    private void InitializeComponent()
    {
        this.Submit1.ServerClick += new System.EventHandler(this.Submit1_ServerClick);
        this.Load += new System.EventHandler(this.Page_Load);

    }

    private void Submit1_ServerClick(object sender, System.EventArgs e)
    {

        int fileLen;
        fileLen = File1.PostedFile.ContentLength;
        byte[] input = new byte[fileLen];
        string SaveLocation = "";
        // input = File1.FileBytes;

        string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);
        string[] fileExtension = fn.Split('.');
        string f_name = fn.Split('\\')[fn.Split('\\').Length - 1];


        if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength < (300 * 1024)))
        {
            if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
            {
                string prePicName = System.IO.Path.GetFileName(File1.PostedFile.FileName);


                SaveLocation = Server.MapPath("EU_Convo") + "/" + Convert.ToString(Session["ANNICELID"]) + "." + prePicName.Split('.')[prePicName.Split('.').Length - 1];
                File1.PostedFile.SaveAs(SaveLocation);
                Session["picloc"] = "EU_Convo" + "/" + Convert.ToString(Session["ANNICELID"]) + "." + prePicName.Split('.')[prePicName.Split('.').Length - 1];
                Session["SPLOC"] = Convert.ToString(Session["ANNICELID"]) + "." + prePicName.Split('.')[prePicName.Split('.').Length - 1];


                try
                {
                    File1.PostedFile.SaveAs(SaveLocation);
                    stdImg = "1";
                    Session["stdImg"] = stdImg;
                    img_myProfile.ImageUrl = "EU_Convo" + "/" + Convert.ToString(Session["ANNICELID"]) + "." + prePicName.Split('.')[prePicName.Split('.').Length - 1];
                    img_myProfile.Visible = true;
                    lblmsg.Text = "The file has been uploaded.";
                }
                catch (Exception ex)
                {

                    lbl_message.Text = "Error to upload file";
                }
            }
            else
            {
                if (((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0)) && (Session["stdImg"] == null) || Session["SPLOC"] == null)
                {
                    lbl_message.Text = "Please Select an image to upload";

                }
                else
                {
                    Session["picloc"] = Convert.ToString(Session["stdImgLoc"]);
                }
            }
        }
        else
        {
            lbl_message.Text = "Filesize of image is too large; please select an image under 300 KB";
        }

        if (Session["picloc"] == null || Convert.ToString(Session["picloc"]) == "" || stdImg == null)
        {
            lblmsg.Text = "Please select a image file to upload.";
        }



    }


    #endregion
    */


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlInfoFillup.Visible = false;
        txtDOB.ReadOnly = false;
        txtStudentID.ReadOnly = false;
        txtDOB.Text = "";
        txtStudentID.Text = "";

        btnCancel.Visible = true;
        btnCheck.Visible = true;
    }
}