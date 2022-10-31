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
using System.Text.RegularExpressions;

public partial class Convocation_EUConForm : System.Web.UI.Page
{
    String sid = "", stdImg = "";
    string TrunIDC = "";


    protected void Page_Load(object sender, EventArgs e)
        {

        if (Session.Count == 0)
            Response.Redirect("_ConvChkInfo.aspx");
        else if (String.IsNullOrEmpty(Session["CONVOSID"].ToString()))
        {
            Response.Redirect("_ConvChkInfo.aspx");
        }
        else
        {


            if (!IsPostBack)
            {
                //chkSID in EU_CONVOCATION
                

                sid = Session["CONVOSID"].ToString();
                DataTable dsConChk = new DataTable();//.Tables["EU_CONVOCHK"]

                dsConChk.Merge(new student().get_ConvocationStudent(sid, "EU_CONVOCHK"));
             //   dsConChk.Merge(new student_webService().get_ConvocationStudent(sid));
              //  dsConChk.Merge(new student().get_ConvocationStudent(sid,"EU_CONVOCHK"));
                if (dsConChk.Rows.Count > 0)
                {
                    foreach (DataRow drConChk in dsConChk.Rows)
                    {
                        txtdesg.Text = drConChk["DESIGNATION"].ToString();
                        txtOrgAdd.Text = drConChk["ORGADDR"].ToString();
                        txtOrgN.Text = drConChk["ORGNAME"].ToString();
                        txtOrgphn.Text = drConChk["ORGCONT"].ToString();
                        txtMail.Text = drConChk["EmailAdd"].ToString();
                        txtContact.Text = drConChk["PHONE"].ToString();
                        pnlPayRpt.Visible = true;
                        pnlPayment.Visible = false;

                        Radioyesno.SelectedValue = drConChk["PICKUP_POINT"].ToString();

                        if (Convert.ToString(Session["DOUBLE_CONVO"]) == "1")
                        {
                            lblRegfee.Text = txtRegFee.Text;
                            lblTotFee.Text = txtTotalFee.Text;
                        }
                        else
                        {
                            lblRegfee.Text = drConChk["REGISTERFEE"].ToString();
                            lblTotFee.Text = drConChk["TOTALFEE"].ToString();
                        }
                       
                        lblGstfee.Text = drConChk["GAUSTFEE"].ToString();



                        lblGstNum.Text = drConChk["GAUSTNO"].ToString();
                       


                        ddlEmployment.SelectedValue = drConChk["EMPSTATUS"].ToString();
                        img_myProfile.Visible = true;
                        Label6.Visible = false;


                        txtdesg.ReadOnly = true;
                        txtOrgAdd.ReadOnly = true;
                        txtOrgN.ReadOnly = true;
                        txtOrgphn.ReadOnly = true;
                        if (drConChk["EMAIL"].ToString() != "")
                            txtMail.ReadOnly = true;
                    }

                }

                load_student_information();

            }
            else
            {
                Label6.Visible = true;
                //if (txtTotalFee.Text != "")
                //{
                //    int a = Convert.ToInt32(txtTotalFee.Text);
                //}
                //else
                //{
                //    int a = 6000;
                //}
            }
        }

       

    }

    private void load_student_information()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_Convocation_student_information(sid));

        foreach (DataRow dr in ds.Tables["CONVOSTD"].Rows)
        {
            if (!String.IsNullOrEmpty(dr["ADDRESS"].ToString()))
                txtAddress.Text = dr["ADDRESS"].ToString();
            else
                txtAddress.Text = "";

            lbl_id.Text = dr["SID"].ToString();
            lbl_name.Text = dr["SNAME"].ToString();
            lbl_DegNa.Text = dr["DEGREENAME"].ToString();

            lblWcrt.Text = dr["Tranfer_Waived"].ToString();
            lblEcrdt.Text = dr["COMP_CHRS"].ToString();
            lblTcrdt.Text = dr["TotalCompledCH"].ToString();
            lblCGPA.Text = dr["CGPA"].ToString();

            lblDegSem.Text = dr["LRS"].ToString();
            lbl_DegSem.Text = dr["LSEMN"].ToString();

            lbl_DegYear.Text = dr["LRY"].ToString();


           /* if (Convert.ToInt32(dr["GRADUATIONYEAR"]) > 0)
            {
                txtTotalFee.Text = Convert.ToString(7000);
                txtRegFee.Text = Convert.ToString(7000);
            }
            else
            {
                txtTotalFee.Text = Convert.ToString(7300);
                txtRegFee.Text = Convert.ToString(7300);
            }*/

            if (Convert.ToString(Session["DOUBLE_CONVO"]) == "1")
            {
                if (Convert.ToInt32(dr["GRADUATIONYEAR"]) > 0)
                {
                    lblGradType.Text = Convert.ToInt32(dr["GRADUATIONYEAR"]).ToString();
                    if (chkboxPanel.Checked == false)
                        txtTotalFee.Text = Convert.ToString(5000);
                    else
                        txtTotalFee.Text = Convert.ToString(6000);

                    txtRegFee.Text = Convert.ToString(5000);
                }
                else
                {
                    lblGradType.Text = Convert.ToInt32(dr["GRADUATIONYEAR"]).ToString();
                    if (chkboxPanel.Checked == false)
                        txtTotalFee.Text = Convert.ToString(5300);
                    else
                        txtTotalFee.Text = Convert.ToString(6300);

                    txtRegFee.Text = Convert.ToString(5300);
                }
            }
            else
            {
                if (Convert.ToInt32(dr["GRADUATIONYEAR"]) > 0)
                {
                    lblGradType.Text = Convert.ToInt32(dr["GRADUATIONYEAR"]).ToString();
                    if (chkboxPanel.Checked == false)
                        txtTotalFee.Text = Convert.ToString(6000);
                    else
                        txtTotalFee.Text = Convert.ToString(7000);

                    txtRegFee.Text = Convert.ToString(6000);
                }
                else
                {
                    lblGradType.Text = Convert.ToInt32(dr["GRADUATIONYEAR"]).ToString();
                    if(chkboxPanel.Checked==false)
                        txtTotalFee.Text = Convert.ToString(6300);
                    else
                        txtTotalFee.Text = Convert.ToString(7300);

                    txtRegFee.Text = Convert.ToString(6300);
                }
               
            }

            if (Convert.ToString(dr["DUE"]) == "")
            {
                btnSubmit.Visible = false;
            }
            else
                if (Convert.ToInt32(dr["DUE"]) > 200)
                {
                    lbldue.Text = "You have TK. " + dr["DUE"].ToString() + " outstanding dues. Please clear your dues to complete the registration.<br/> (You cannot register for the convocation unless you clear your dues.)<br/>Thank You.";
                   btnSubmit.Visible = false;

                }
                else
                {
                    lbldue.Text = "";
                    btnSubmit.Visible = true;
                }

            if (!String.IsNullOrEmpty(dr["EMAIL"].ToString()))
                txtMail.Text = dr["EMAIL"].ToString();
            else
                txtMail.Text = "";

            if (!String.IsNullOrEmpty(dr["PHONE"].ToString()))
                txtContact.Text = dr["PHONE"].ToString();
            else
                txtContact.Text = "";



            if (String.IsNullOrEmpty(dr["S_PICTURE"].ToString()))
            {
                img_myProfile.ImageUrl = "~/student/profile/student_images/no_image.gif";
                stdImg = null;
                Session["SPLOC"] = null;
                Session["stdImg"] = stdImg;
            }
            else
            {
                //img_myProfile.ImageUrl = "~/student/profile/student_images/no_image.gif";
                //stdImg = null;
                //Session["SPLOC"] = null;
                //Session["stdImg"] = stdImg;

                Session["SPLOC"] = sid + "." + dr["S_PICTURE"].ToString().Split('.')[dr["S_PICTURE"].ToString().Split('.').Length - 1];
                img_myProfile.ImageUrl = "~/student/profile/student_images/" + sid + "." + dr["S_PICTURE"].ToString().Split('.')[dr["S_PICTURE"].ToString().Split('.').Length - 1];
                stdImg = "1";
                Session["stdImg"] = stdImg;
                Session["stdImgLoc"] = img_myProfile.ImageUrl;
            }
            break;
        }

    }

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


                SaveLocation = Server.MapPath("~/student/profile/student_images") + "/" + Convert.ToString(Session["CONVOSID"]) + "." + prePicName.Split('.')[prePicName.Split('.').Length - 1];
                File1.PostedFile.SaveAs(SaveLocation);
                Session["picloc"] = "~/student/profile/student_images" + "/" + Convert.ToString(Session["CONVOSID"]) + "." + prePicName.Split('.')[prePicName.Split('.').Length - 1];
                Session["SPLOC"] = Convert.ToString(Session["CONVOSID"]) + "." + prePicName.Split('.')[prePicName.Split('.').Length - 1];


                try
                {
                    File1.PostedFile.SaveAs(SaveLocation);
                    stdImg = "1";
                    Session["stdImg"] = stdImg;
                    img_myProfile.ImageUrl = "~/student/profile/student_images" + "/" + Convert.ToString(Session["CONVOSID"]) + "." + prePicName.Split('.')[prePicName.Split('.').Length - 1];
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

    protected string replace_(string st)
    {

        //Regex rx = new Regex(" ");
        //string s1 = rx.Replace(st, "&nbsp;");
        Regex ry = new Regex("\r\n|\n|\r");
        string s2 = ry.Replace(st, "<br/>");
        return s2;


    }

    protected string replaceOposite(string st)
    {

        //Regex rx = new Regex("&nbsp;");
        //string s1 = rx.Replace(st, " ");
        Regex ry = new Regex("<br/>");
        string s2 = ry.Replace(st, "\r\n");
        return s2;


    }
    //btnsubmit click
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dsConChk = new DataTable();//.Tables["EU_CONVOCHK"]
        sid = Session["CONVOSID"].ToString();
        //   dsConChk.Merge(new student().get_ConvocationStudent(sid, "EU_CONVOCHK"));

        dsConChk.Merge(new student().get_T_STUDENTDEBIT(sid, "EU_CONVOCHK"));
        if (dsConChk.Rows.Count > 0)
        {

            lbl_message.Text = "Convocation Registration Sucessful";
        }
        else
        {
            lbl_message.Text += "";
            decimal Gross = 0, total = 0, b = 0, GaustNo = 0, GaustFee = 0, RegFee = 0;
            string tempMarks = "", HEADSN = "";


            Session["sName"] = Convert.ToString(lbl_name.Text);
            Session["semail"] = Convert.ToString(txtMail.Text);


            Submit1_ServerClick(sender, e);
            //chk img null or empty img

            if (Session["picloc"] == null || Session["stdImg"] == null)
            {
                lbl_message.Text += "Please select a image file to upload.";
            }
            else
            {
                //if (txtTotalFee.Text != "")
                //{
                //    total = Convert.ToInt32(txtTotalFee.Text);
                //}
                //else
                //{
                //    total = Convert.ToDecimal(6000);
                //}
                //if (total > 6000)
                //{
                //    GaustFee = total - Convert.ToDecimal(6000);
                //    GaustNo = GaustFee / Convert.ToDecimal(500);
                //}
                //else
                //{
                //    GaustFee = Convert.ToDecimal(0);
                //    GaustNo = Convert.ToDecimal(0);
                //}

                GaustFee = Convert.ToDecimal(0);

                if (chkboxPanel.Checked == true)
                {
                    GaustNo = Convert.ToDecimal(1);
                }
                else
                {
                    GaustNo = Convert.ToDecimal(0);
                }

                DataSet ds = new DataSet();

                ds.Tables.Add("EU_CONVOCATION");
                ds.Tables["EU_CONVOCATION"].Columns.Add("SID");
                ds.Tables["EU_CONVOCATION"].Columns.Add("EMAIL");
                ds.Tables["EU_CONVOCATION"].Columns.Add("PHONE");
                ds.Tables["EU_CONVOCATION"].Columns.Add("S_PICTURE");

                ds.Tables["EU_CONVOCATION"].Columns.Add("YEAR");
                ds.Tables["EU_CONVOCATION"].Columns.Add("SEMESTER");
                ds.Tables["EU_CONVOCATION"].Columns.Add("GAUSTNO");
                ds.Tables["EU_CONVOCATION"].Columns.Add("GAUSTFEE");
                ds.Tables["EU_CONVOCATION"].Columns.Add("REGISTERFEE");
                ds.Tables["EU_CONVOCATION"].Columns.Add("TOTALFEE");
                ds.Tables["EU_CONVOCATION"].Columns.Add("PDATE");
                ds.Tables["EU_CONVOCATION"].Columns.Add("STATUS");

                ds.Tables["EU_CONVOCATION"].Columns.Add("EMPSTATUS");
                ds.Tables["EU_CONVOCATION"].Columns.Add("DESIGNATION");
                ds.Tables["EU_CONVOCATION"].Columns.Add("ORGNAME");
                ds.Tables["EU_CONVOCATION"].Columns.Add("ORGADDR");
                ds.Tables["EU_CONVOCATION"].Columns.Add("ORGCONT");
                ds.Tables["EU_CONVOCATION"].Columns.Add("HEADSN");
                ds.Tables["EU_CONVOCATION"].Columns.Add("REG_TYPE");
                ds.Tables["EU_CONVOCATION"].Columns.Add("PRESENT_ADDRESS");
                ds.Tables["EU_CONVOCATION"].Columns.Add("CONVOCATION_YEAR");
                ds.Tables["EU_CONVOCATION"].Columns.Add("PICKUP_POINT");
                // ds.Tables["EU_CONVOCATION"].Columns.Add("PDATE");

                DataRow dr = ds.Tables["EU_CONVOCATION"].NewRow();

                DateTime today = DateTime.Today;
                string s_today = today.ToString("MM/dd/yyyy");
                today = Convert.ToDateTime(s_today);
                //dr["PDATE"] = today;

                //ds.Tables["EU_CONVOCATION"].Columns.Add("GNAME2");
                //ds.Tables["EU_CONVOCATION"].Columns.Add("RELATIONSHIP2");
                //ds.Tables["EU_CONVOCATION"].Columns.Add("ADDRESS2");


                dr["SID"] = "" + lbl_id.Text;
                dr["REG_TYPE"] = "C";
                // Session["year"] = Convert.ToString(2017);
                // Session["Sem"] = Convert.ToString(1);
                dr["SEMESTER"] = "" + Convert.ToString(1);
                dr["YEAR"] = "" + Convert.ToString(2019);
                dr["EMAIL"] = "" + txtMail.Text;
                dr["PHONE"] = "" + txtContact.Text;

                dr["S_PICTURE"] = "" + Convert.ToString(Session["SPLOC"]);

                dr["PRESENT_ADDRESS"] = "" + replace_(txtAddress.Text);


                dr["EMPSTATUS"] = "" + Convert.ToString(ddlEmployment.SelectedValue);
                dr["DESIGNATION"] = "" + Convert.ToString(txtdesg.Text);
                dr["ORGNAME"] = "" + Convert.ToString(txtOrgN.Text);
                dr["ORGADDR"] = "" + Convert.ToString(txtOrgAdd.Text);
                dr["ORGCONT"] = "" + Convert.ToString(txtOrgphn.Text);

                // string companyname = Request.Form["companyname"];
                dr["GAUSTFEE"] = "" + GaustFee;
                dr["GAUSTNO"] = "" + GaustNo;
                dr["REGISTERFEE"] = "" + Convert.ToDecimal(txtTotalFee.Text);
                dr["PDATE"] = "" + new cls_tools().get_database_formateDate_new(DateTime.Today);

                // dr["PDATE"] = ""+Convert.ToString(Convert.ToDateTime(DateTime.Now));

                dr["HEADSN"] = "" + Convert.ToDecimal(28);



                if (Convert.ToString(Session["DOUBLE_CONVO"]) == "1")
                {
                    if (Convert.ToInt32(lblGradType.Text) > 0)
                    {

                        if (chkboxPanel.Checked == false)
                            txtTotalFee.Text = Convert.ToString(5000);
                        else
                            txtTotalFee.Text = Convert.ToString(6000);

                        txtRegFee.Text = Convert.ToString(5000);
                    }
                    else
                    {
                        if (chkboxPanel.Checked == false)
                            txtTotalFee.Text = Convert.ToString(5300);
                        else
                            txtTotalFee.Text = Convert.ToString(6300);

                        txtRegFee.Text = Convert.ToString(5300);
                    }
                }
                else
                {
                    if (Convert.ToInt32(lblGradType.Text) > 0)
                    {
                        if (chkboxPanel.Checked == false)
                            txtTotalFee.Text = Convert.ToString(6000);
                        else
                            txtTotalFee.Text = Convert.ToString(7000);

                        txtRegFee.Text = Convert.ToString(6000);
                    }
                    else
                    {
                        if (chkboxPanel.Checked == false)
                            txtTotalFee.Text = Convert.ToString(6300);
                        else
                            txtTotalFee.Text = Convert.ToString(7300);

                        txtRegFee.Text = Convert.ToString(6300);
                    }

                }
                if (lblTotFee.Text != "")
                    dr["TOTALFEE"] = lblTotFee.Text;
                else
                    dr["TOTALFEE"] = "" + Convert.ToDecimal(txtTotalFee.Text);

                dr["STATUS"] = "I";
                dr["CONVOCATION_YEAR"] = "2019";
                dr["PICKUP_POINT"] = "" + Radioyesno.SelectedValue.ToString();
                // dr["PDATE"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);
                /// dr["PDATE"] = "" + today;//today;

                ds.Tables["EU_CONVOCATION"].Rows.Add(dr);

                DataSet dsGst = new DataSet();
                dsGst.Tables.Add("EU_CONVOCATION_GUEST");
                dsGst.Tables["EU_CONVOCATION_GUEST"].Columns.Add("GNAME");
                dsGst.Tables["EU_CONVOCATION_GUEST"].Columns.Add("RELATIONSHIP");
                dsGst.Tables["EU_CONVOCATION_GUEST"].Columns.Add("ADDRESS");
                dsGst.Tables["EU_CONVOCATION_GUEST"].Columns.Add("SID");
                dsGst.Tables["EU_CONVOCATION_GUEST"].Columns.Add("GR_PHONE");


                DataRow drGst = dsGst.Tables["EU_CONVOCATION_GUEST"].NewRow();

                if (chkboxPanel.Checked == true)
                {
                    drGst["SID"] = "" + lbl_id.Text;
                    drGst["GNAME"] = "" + Convert.ToString(txtGstName1.Text);
                    drGst["RELATIONSHIP"] = "" + Convert.ToString(txtGstRl.Text);
                    drGst["ADDRESS"] = "" + Convert.ToString(txtGstAdd.Text);
                    drGst["GR_PHONE"] = "" + Convert.ToString(txtGstPhn.Text);
                }
                else
                {
                    //  GaustNo = Convert.ToDecimal(0);
                }


                dsGst.Tables["EU_CONVOCATION_GUEST"].Rows.Add(drGst);


                string TRANID = "";
                string UpstrudentInfo = new student_webService().UpstrudentInfo(ds);
                TRANID = new student_webService().insert_Con_stdDebit(ds, dsGst);
                //----------------------online payment

                string myname = TRANID;
                Session["TRAN_ID"] = TRANID;

                /*string TotalAmount = "";

                DataSet StdDebit = new DataSet();
                StdDebit.Merge(new student_webService().get_ConSTUDENT_Payment(TRANID));
                foreach (DataRow dr1 in StdDebit.Tables["EU_CONVOCATION"].Rows)
                {
                    TotalAmount = dr1["TOTALFEE"].ToString();
                    //  Session["sphone"] = dr1["PHONE"].ToString();
                }
                //Session["Total_Amount"] = Convert.ToString(TotalAmount);*/

                if (lblTotFee.Text != "")
                    Session["Total_Amount"] = lblTotFee.Text;
                else
                    Session["Total_Amount"] = Convert.ToDecimal(txtTotalFee.Text);
                //  Session["Total_Amount"] = Convert.ToString(txtTotalFee.Text);
                Session["year"] = Convert.ToString(2019);//Convert.ToString(lbl_DegYear.Text);
                Session["Sem"] = Convert.ToString(1);//Convert.ToString(lblDegSem.Text);
                Session["SemN"] = "Spring";//Convert.ToString(lbl_DegSem.Text);
                Session["semail"] = txtMail.Text;
                Session["sphone"] = txtContact.Text;


                if (Convert.ToString(Session["TRAN_ID"]) == "0")
                {
                    lbl_message.Text = "Fail to Insert, Please Try again";
                }
                else
                {

                    if (String.IsNullOrEmpty(Session["sName"].ToString()) ||
                        String.IsNullOrEmpty(Session["year"].ToString()) ||
                        String.IsNullOrEmpty(Session["Sem"].ToString()) ||
                        String.IsNullOrEmpty(Session["SemN"].ToString()) ||
                        String.IsNullOrEmpty(Session["semail"].ToString()) ||
                        String.IsNullOrEmpty(Session["CONVOSID"].ToString()) ||
                        String.IsNullOrEmpty(Session["TRAN_ID"].ToString()) ||
                        String.IsNullOrEmpty(Session["sphone"].ToString()) ||
                        String.IsNullOrEmpty(Session["Total_Amount"].ToString()))
                    {
                        lbl_message.Text = "To Pay Your Payment firstly complete 'Payment Amount' portion.";
                    }
                    else
                    {
                        Response.Redirect("_EUCOnlinePayment.aspx");
                    }
                }
            }

        }

        
            //  = new student_webService().UpstrudentInfo(ds);
           
         //   dsConChk.Merge(new student_webService().get_ConvocationStudent(sid));
           // DataSet dsConChk = new DataSet();
           // dsConChk.Merge(new student_webService().get_ConvocationStudent(Convert.ToString(Session["CONVOSID"])));

          /*  if (dsConChk.Rows.Count > 0)
            {
               
                
               foreach (DataRow drConChk in dsConChk.Rows)
                {

                    lblTrunID.Text = drConChk["TRANID"].ToString();
                    TrunIDC = drConChk["TRANID"].ToString();
                    txtdesg.Text = drConChk["DESIGNATION"].ToString();
                    txtOrgAdd.Text = drConChk["ORGADDR"].ToString();
                    txtOrgN.Text = drConChk["ORGNAME"].ToString();
                    txtOrgphn.Text = drConChk["ORGCONT"].ToString();
                    if (drConChk["Email"].ToString() != null || drConChk["Email"].ToString() != "")
                        txtMail.Text = drConChk["EmailAdd"].ToString();

                    txtContact.Text = drConChk["PHONE"].ToString();
                    txtAddress.Text = replaceOposite(drConChk["PRESENT_ADDRESS"].ToString());

                    Radioyesno.SelectedValue = drConChk["PICKUP_POINT"].ToString();
                    

                    pnlPayRpt.Visible = true;
                    pnlPayment.Visible = false;

                    if (Convert.ToString(Session["DOUBLE_CONVO"]) == "1")
                    {
                        lblRegfee.Text = txtRegFee.Text;
                        lblTotFee.Text = txtTotalFee.Text;
                    }
                    else
                    {
                        lblRegfee.Text = drConChk["REGISTERFEE"].ToString();
                        lblTotFee.Text = drConChk["TOTALFEE"].ToString();
                    }

                   // lblRegfee.Text = drConChk["REGISTERFEE"].ToString();
                    lblGstfee.Text = drConChk["GAUSTFEE"].ToString();
                    lblGstNum.Text = drConChk["GAUSTNO"].ToString();
                  //  lblTotFee.Text = drConChk["TOTALFEE"].ToString();


                    ddlEmployment.SelectedValue = drConChk["EMPSTATUS"].ToString();




                    txtdesg.ReadOnly = true;
                    txtOrgAdd.ReadOnly = true;
                    txtOrgN.ReadOnly = true;
                    txtOrgphn.ReadOnly = true;
                    if (drConChk["EMAIL"].ToString() != "")
                        txtMail.ReadOnly = true;
                }

                
                TRANID = new student_webService().update_STranID(ds, dsGst, TrunIDC);
               // if (Convert.ToInt32(TRANID) > 0)
                   

                //add guest table
                  

            }
            else
            {
                TRANID = new student_webService().insert_Con_stdDebit(ds, dsGst);
                //add guest table
            }*/


            
    }

/*
    protected void btnSubmit_Click_prev(object sender, EventArgs e)
    {
        decimal Gross = 0, total = 0, b = 0, GaustNo = 0, GaustFee = 0, RegFee = 0;
        string tempMarks = "", HEADSN = "";


        Session["sName"] = Convert.ToString(lbl_name.Text);
        Session["semail"] = Convert.ToString(txtMail.Text);


        Submit1_ServerClick(sender, e);
        //chk img null or empty img

        if (Session["picloc"] == null || Session["stdImg"] == null)
        {
            lbl_message.Text += "Please select a image file to upload.";
        }
        else
        {
            //if (txtTotalFee.Text != "")
            //{
            //    total = Convert.ToInt32(txtTotalFee.Text);
            //}
            //else
            //{
            //    total = Convert.ToDecimal(6000);
            //}
            //if (total > 6000)
            //{
            //    GaustFee = total - Convert.ToDecimal(6000);
            //    GaustNo = GaustFee / Convert.ToDecimal(500);
            //}
            //else
            //{
            //    GaustFee = Convert.ToDecimal(0);
            //    GaustNo = Convert.ToDecimal(0);
            //}

            GaustFee = Convert.ToDecimal(0);

            if (chkboxPanel.Checked == true)
            {
                GaustNo = Convert.ToDecimal(1);
            }
            else
            {
                GaustNo = Convert.ToDecimal(0);
            }

            DataSet ds = new DataSet();

            ds.Tables.Add("EU_CONVOCATION");
            ds.Tables["EU_CONVOCATION"].Columns.Add("SID");
            ds.Tables["EU_CONVOCATION"].Columns.Add("EMAIL");
            ds.Tables["EU_CONVOCATION"].Columns.Add("PHONE");
            ds.Tables["EU_CONVOCATION"].Columns.Add("S_PICTURE");

            ds.Tables["EU_CONVOCATION"].Columns.Add("YEAR");
            ds.Tables["EU_CONVOCATION"].Columns.Add("SEMESTER");
            ds.Tables["EU_CONVOCATION"].Columns.Add("GAUSTNO");
            ds.Tables["EU_CONVOCATION"].Columns.Add("GAUSTFEE");
            ds.Tables["EU_CONVOCATION"].Columns.Add("REGISTERFEE");
            ds.Tables["EU_CONVOCATION"].Columns.Add("TOTALFEE");
            ds.Tables["EU_CONVOCATION"].Columns.Add("PDATE");
            ds.Tables["EU_CONVOCATION"].Columns.Add("STATUS");

            ds.Tables["EU_CONVOCATION"].Columns.Add("EMPSTATUS");
            ds.Tables["EU_CONVOCATION"].Columns.Add("DESIGNATION");
            ds.Tables["EU_CONVOCATION"].Columns.Add("ORGNAME");
            ds.Tables["EU_CONVOCATION"].Columns.Add("ORGADDR");
            ds.Tables["EU_CONVOCATION"].Columns.Add("ORGCONT");
            ds.Tables["EU_CONVOCATION"].Columns.Add("HEADSN");
            ds.Tables["EU_CONVOCATION"].Columns.Add("REG_TYPE");
            ds.Tables["EU_CONVOCATION"].Columns.Add("PRESENT_ADDRESS");
            ds.Tables["EU_CONVOCATION"].Columns.Add("CONVOCATION_YEAR");
            ds.Tables["EU_CONVOCATION"].Columns.Add("PICKUP_POINT");
            // ds.Tables["EU_CONVOCATION"].Columns.Add("PDATE");

            DataRow dr = ds.Tables["EU_CONVOCATION"].NewRow();

            DateTime today = DateTime.Today;
            string s_today = today.ToString("MM/dd/yyyy");
            today = Convert.ToDateTime(s_today);
            //dr["PDATE"] = today;

            //ds.Tables["EU_CONVOCATION"].Columns.Add("GNAME2");
            //ds.Tables["EU_CONVOCATION"].Columns.Add("RELATIONSHIP2");
            //ds.Tables["EU_CONVOCATION"].Columns.Add("ADDRESS2");


            dr["SID"] = "" + lbl_id.Text;
            dr["REG_TYPE"] = "C";
            // Session["year"] = Convert.ToString(2017);
            // Session["Sem"] = Convert.ToString(1);
            dr["SEMESTER"] = "" + Convert.ToString(1);
            dr["YEAR"] = "" + Convert.ToString(2019);
            dr["EMAIL"] = "" + txtMail.Text;
            dr["PHONE"] = "" + txtContact.Text;

            dr["S_PICTURE"] = "" + Convert.ToString(Session["SPLOC"]);

            dr["PRESENT_ADDRESS"] = "" + replace_(txtAddress.Text);


            dr["EMPSTATUS"] = "" + Convert.ToString(ddlEmployment.SelectedValue);
            dr["DESIGNATION"] = "" + Convert.ToString(txtdesg.Text);
            dr["ORGNAME"] = "" + Convert.ToString(txtOrgN.Text);
            dr["ORGADDR"] = "" + Convert.ToString(txtOrgAdd.Text);
            dr["ORGCONT"] = "" + Convert.ToString(txtOrgphn.Text);

            // string companyname = Request.Form["companyname"];
            dr["GAUSTFEE"] = "" + GaustFee;
            dr["GAUSTNO"] = "" + GaustNo;
            dr["REGISTERFEE"] = "" + Convert.ToDecimal(txtTotalFee.Text);
            dr["PDATE"] = "" + new cls_tools().get_database_formateDate_new(DateTime.Today);

            // dr["PDATE"] = ""+Convert.ToString(Convert.ToDateTime(DateTime.Now));

            dr["HEADSN"] = "" + Convert.ToDecimal(28);



            if (Convert.ToString(Session["DOUBLE_CONVO"]) == "1")
            {
                if (Convert.ToInt32(lblGradType.Text) > 0)
                {

                    if (chkboxPanel.Checked == false)
                        txtTotalFee.Text = Convert.ToString(5000);
                    else
                        txtTotalFee.Text = Convert.ToString(6000);

                    txtRegFee.Text = Convert.ToString(5000);
                }
                else
                {
                    if (chkboxPanel.Checked == false)
                        txtTotalFee.Text = Convert.ToString(5300);
                    else
                        txtTotalFee.Text = Convert.ToString(6300);

                    txtRegFee.Text = Convert.ToString(5300);
                }
            }
            else
            {
                if (Convert.ToInt32(lblGradType.Text) > 0)
                {
                    if (chkboxPanel.Checked == false)
                        txtTotalFee.Text = Convert.ToString(6000);
                    else
                        txtTotalFee.Text = Convert.ToString(7000);

                    txtRegFee.Text = Convert.ToString(6000);
                }
                else
                {
                    if (chkboxPanel.Checked == false)
                        txtTotalFee.Text = Convert.ToString(6300);
                    else
                        txtTotalFee.Text = Convert.ToString(7300);

                    txtRegFee.Text = Convert.ToString(6300);
                }

            }
            if (lblTotFee.Text != "")
                dr["TOTALFEE"] = lblTotFee.Text;
            else
                dr["TOTALFEE"] = "" + Convert.ToDecimal(txtTotalFee.Text);

            dr["STATUS"] = "I";
            dr["CONVOCATION_YEAR"] = "2019";
            dr["PICKUP_POINT"] = "" + Radioyesno.SelectedValue.ToString();
            // dr["PDATE"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);
            /// dr["PDATE"] = "" + today;//today;

            ds.Tables["EU_CONVOCATION"].Rows.Add(dr);

            DataSet dsGst = new DataSet();
            dsGst.Tables.Add("EU_CONVOCATION_GUEST");
            dsGst.Tables["EU_CONVOCATION_GUEST"].Columns.Add("GNAME");
            dsGst.Tables["EU_CONVOCATION_GUEST"].Columns.Add("RELATIONSHIP");
            dsGst.Tables["EU_CONVOCATION_GUEST"].Columns.Add("ADDRESS");
            dsGst.Tables["EU_CONVOCATION_GUEST"].Columns.Add("SID");
            dsGst.Tables["EU_CONVOCATION_GUEST"].Columns.Add("GR_PHONE");


            DataRow drGst = dsGst.Tables["EU_CONVOCATION_GUEST"].NewRow();

            if (chkboxPanel.Checked == true)
            {
                drGst["SID"] = "" + lbl_id.Text;
                drGst["GNAME"] = "" + Convert.ToString(txtGstName1.Text);
                drGst["RELATIONSHIP"] = "" + Convert.ToString(txtGstRl.Text);
                drGst["ADDRESS"] = "" + Convert.ToString(txtGstAdd.Text);
                drGst["GR_PHONE"] = "" + Convert.ToString(txtGstPhn.Text);
            }
            else
            {
                //  GaustNo = Convert.ToDecimal(0);
            }


            dsGst.Tables["EU_CONVOCATION_GUEST"].Rows.Add(drGst);


            string TRANID = "";
            string UpstrudentInfo = new student_webService().UpstrudentInfo(ds);
            //  = new student_webService().UpstrudentInfo(ds);
            DataTable dsConChk = new DataTable();//.Tables["EU_CONVOCHK"]
            sid = Session["CONVOSID"].ToString();
            dsConChk.Merge(new student().get_ConvocationStudent(sid, "EU_CONVOCHK"));
            //   dsConChk.Merge(new student_webService().get_ConvocationStudent(sid));
            // DataSet dsConChk = new DataSet();
            // dsConChk.Merge(new student_webService().get_ConvocationStudent(Convert.ToString(Session["CONVOSID"])));

            if (dsConChk.Rows.Count > 0)
            {
                foreach (DataRow drConChk in dsConChk.Rows)
                {

                    lblTrunID.Text = drConChk["TRANID"].ToString();
                    TrunIDC = drConChk["TRANID"].ToString();
                    txtdesg.Text = drConChk["DESIGNATION"].ToString();
                    txtOrgAdd.Text = drConChk["ORGADDR"].ToString();
                    txtOrgN.Text = drConChk["ORGNAME"].ToString();
                    txtOrgphn.Text = drConChk["ORGCONT"].ToString();
                    if (drConChk["Email"].ToString() != null || drConChk["Email"].ToString() != "")
                        txtMail.Text = drConChk["EmailAdd"].ToString();

                    txtContact.Text = drConChk["PHONE"].ToString();
                    txtAddress.Text = replaceOposite(drConChk["PRESENT_ADDRESS"].ToString());

                    Radioyesno.SelectedValue = drConChk["PICKUP_POINT"].ToString();


                    pnlPayRpt.Visible = true;
                    pnlPayment.Visible = false;

                    if (Convert.ToString(Session["DOUBLE_CONVO"]) == "1")
                    {
                        lblRegfee.Text = txtRegFee.Text;
                        lblTotFee.Text = txtTotalFee.Text;
                    }
                    else
                    {
                        lblRegfee.Text = drConChk["REGISTERFEE"].ToString();
                        lblTotFee.Text = drConChk["TOTALFEE"].ToString();
                    }

                    // lblRegfee.Text = drConChk["REGISTERFEE"].ToString();
                    lblGstfee.Text = drConChk["GAUSTFEE"].ToString();
                    lblGstNum.Text = drConChk["GAUSTNO"].ToString();
                    //  lblTotFee.Text = drConChk["TOTALFEE"].ToString();


                    ddlEmployment.SelectedValue = drConChk["EMPSTATUS"].ToString();




                    txtdesg.ReadOnly = true;
                    txtOrgAdd.ReadOnly = true;
                    txtOrgN.ReadOnly = true;
                    txtOrgphn.ReadOnly = true;
                    if (drConChk["EMAIL"].ToString() != "")
                        txtMail.ReadOnly = true;
                }


                TRANID = new student_webService().update_STranID(ds, dsGst, TrunIDC);
                // if (Convert.ToInt32(TRANID) > 0)


                //add guest table

            }
            else
            {
                TRANID = new student_webService().insert_Con_stdDebit(ds, dsGst);
                //add guest table
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
                  //  Session["sphone"] = dr1["PHONE"].ToString();
              }
              //Session["Total_Amount"] = Convert.ToString(TotalAmount);


            if (lblTotFee.Text != "")
                Session["Total_Amount"] = lblTotFee.Text;
            else
                Session["Total_Amount"] = Convert.ToDecimal(txtTotalFee.Text);
            //  Session["Total_Amount"] = Convert.ToString(txtTotalFee.Text);
            Session["year"] = Convert.ToString(2019);//Convert.ToString(lbl_DegYear.Text);
            Session["Sem"] = Convert.ToString(1);//Convert.ToString(lblDegSem.Text);
            Session["SemN"] = "Spring";//Convert.ToString(lbl_DegSem.Text);
            Session["semail"] = txtMail.Text;
            Session["sphone"] = txtContact.Text;


            if (Convert.ToString(Session["TRAN_ID"]) == "0")
            {
                lbl_message.Text = "Fail to Insert, Please Try again";
            }
            else
            {

                if (String.IsNullOrEmpty(Session["sName"].ToString()) ||
                    String.IsNullOrEmpty(Session["year"].ToString()) ||
                    String.IsNullOrEmpty(Session["Sem"].ToString()) ||
                    String.IsNullOrEmpty(Session["SemN"].ToString()) ||
                    String.IsNullOrEmpty(Session["semail"].ToString()) ||
                    String.IsNullOrEmpty(Session["CONVOSID"].ToString()) ||
                    String.IsNullOrEmpty(Session["TRAN_ID"].ToString()) ||
                    String.IsNullOrEmpty(Session["sphone"].ToString()) ||
                    String.IsNullOrEmpty(Session["Total_Amount"].ToString()))
                {
                    lbl_message.Text = "To Pay Your Payment firstly complete 'Payment Amount' portion.";
                }
                else
                {
                    Response.Redirect("_EUCOnlinePayment.aspx");
                }
            }
        }
    }
*/
}
