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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;


public partial class student_AdmitCard_AdmitCardPrev : System.Web.UI.Page
{
    string sid = "";
    string course = "";
    staff_webService obj_staff = new staff_webService();

    protected void Page_Load(object sender, EventArgs e)
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
        }      

       
        lbl_message.Text = "";
    }


    protected void btn_submit_Click(object sender, EventArgs e)
    {

        //DataSet dscountStudent = new DataSet();
        //dscountStudent.Merge(new student_webService().checkStudentCount());
        //if (dscountStudent.Tables["StudentCount"].Rows.Count > 0)
        //{
        //foreach (DataRow countStudent in dscountStudent.Tables["StudentCount"].Rows)
        //{
        //    int total = Convert.ToInt32(countStudent["total"].ToString());
        //    if (total <= 250)
        //    {

        String examtype = "";
        examtype = ddlExamtype.SelectedValue.ToString();

        if (examtype == "1")
        {
            if (new student_webService().get_AdmitCard_dateRange(cmb_semester.SelectedValue.ToString(), Convert.ToString(txt_year.Text)))
            {
                Check_Clearance();
                Session["ExamType"] = Convert.ToString(ddlExamtype.SelectedItem);
            }
            else
            {
                lbl_message.Text = "Admit Card Distribution date is not Opened now.";
            }
        }
        else
        {
            if (examtype == "2")
            {
                if (new student_webService().get_MID_AdmitCard_dateRange(cmb_semester.SelectedValue.ToString(), Convert.ToString(txt_year.Text)))
                {
                    Check_Clearance();
                    Session["ExamType"] = Convert.ToString(ddlExamtype.SelectedItem);
                }
                else
                {
                    lbl_message.Text = "Admit Card Distribution date is not Opened now.";
                }
            }
        }


        

        //    }
        // }
        //}

    }

    private void Check_Clearance()
    {

        string accSem = "", accYear = "", Currentdate = "";
        string P_Year = "", prevSem = "", R_Sem = "", R_Year = "";      



        //--------------------------find out current going year & semester due upto 1st installment

        //with previous dues


        double firstSemDue = 0, graceAmt = 3000, prevdues = 0, totaldues = 0,
            perInstallment = 0, multiplier = 1, netFirstSemPayable = 0,
            totalPiad = 0, semesterRegistrationFee = 0, totalTutionFeePayable = 0;

        R_Sem = cmb_semester.SelectedValue.ToString();
        R_Year = Convert.ToString(txt_year.Text);


        //DataSet AdmitCard_dsp = new DataSet();
        //AdmitCard_dsp.Merge(new student_webService().get_AdmitCard_BalanceNew(sid, R_Sem, R_Year));

        DataSet AdmitCard_ds = new DataSet();
        AdmitCard_ds.Merge(new student_webService().get_AdmitCard_Balance(sid, R_Sem, R_Year));
        if (AdmitCard_ds.Tables["AdmitCard_Balance"].Rows.Count > 0)
        {
            foreach (DataRow AdmitCard_dr in AdmitCard_ds.Tables["AdmitCard_Balance"].Rows)
            {
                totalTutionFeePayable = (Convert.ToDouble(AdmitCard_dr["totalTutionFeePayable"]));
                totalPiad = Convert.ToDouble(AdmitCard_dr["totalPiad"]);
                netFirstSemPayable = Convert.ToDouble(AdmitCard_dr["netFirstSemPayable"]);
                semesterRegistrationFee = Convert.ToDouble(AdmitCard_dr["semesterRegistrationFee"]);
            }
        }

        //apply for mid & final exam

        DateTime secInsdate = new DateTime(), trdInsdate = new DateTime();

        Currentdate = new cls_tools().get_database_formateDate(DateTime.Today);

        DataSet InsDate_ds = new DataSet();
        InsDate_ds.Merge(new student_webService().get_IntallmentDate(R_Sem, R_Year));
        if (InsDate_ds.Tables["Per_IntallmentDate"].Rows.Count > 0)
        {
            foreach (DataRow InsDate_dr in InsDate_ds.Tables["Per_IntallmentDate"].Rows)
            {
                //fstInsdate = (Convert.ToDouble(InsDate_dr["INSONEDATE"]));
                secInsdate = Convert.ToDateTime(InsDate_dr["INSTWODATE"]);
                trdInsdate = Convert.ToDateTime(InsDate_dr["INSTHREEDATE"]);
            }
        }

        if (Convert.ToDateTime(Currentdate) <= secInsdate)
            multiplier = 1;
        else
            if (Convert.ToDateTime(Currentdate) <= trdInsdate)
                multiplier = 2;
            else
                if (Convert.ToDateTime(Currentdate) > trdInsdate)
                    multiplier = 3;



        perInstallment = (totalTutionFeePayable / 3) * multiplier;





        netFirstSemPayable = perInstallment + semesterRegistrationFee;
        firstSemDue = netFirstSemPayable - totalPiad;

        if (firstSemDue - graceAmt > 0)
        {
            String examtype = "", Exty = "";
            examtype = ddlExamtype.SelectedValue.ToString();

            if (examtype == "1")
            {
                Exty = "F";
            }
            else
                if (examtype == "2")
                {
                    Exty = "M";
                }

            if (!(new student_webService().is_valid_AdmitClearance(sid, txt_year.Text, cmb_semester.SelectedValue.ToString(), Exty)))
            {
                lbl_message.Text = "" + new cls_message().getMessage(19);
                return;
            }

            // lbl_message.Text = "Please Clear your Dues";
        }


        






        //--------------------------find out current going year & semester
        double CrntYear_sem = 0;
        CrntYear_sem = new staff_webService().get_latest_yearSemister();
        int binding_YearSem = Convert.ToInt32(CrntYear_sem - 60);


        //if (int.Parse(sid.Substring(0, 3)) <= binding_YearSem && !(new student_webService().is_valid_student(sid, txt_year.Text, cmb_semester.SelectedValue.ToString())))
        //{
        //    lbl_message.Text = "" + new cls_message().getMessage(26);
        //    return;
        //}



        if (new student_webService().is_dropout_student(sid))
        {
            lbl_message.Text = "" + new cls_message().getMessage(25);
            return;
        }

        if (!new student_webService().is_register_student(sid, cmb_semester.SelectedValue.ToString(), txt_year.Text))
        {
            lbl_message.Text = "" + new cls_message().getMessage(13);
            return;
        }




        loadTaken_courses();
    }
    string AESKey = "Key@AES1234";

    private void loadTaken_courses()
    {
        Session["sem"] = "" + cmb_semester.SelectedValue.ToString();
        Session["semName"] = "" + Convert.ToString(cmb_semester.SelectedItem);
        Session["year"] = "" + txt_year.Text.Trim();
        Session["sid"] = "" + sid;

        String StdID = sid;
        String Year = txt_year.Text.Trim();
        String Semister = Convert.ToString(cmb_semester.SelectedValue);

        String examtype = "";
        examtype = ddlExamtype.SelectedValue.ToString();

        if (examtype == "1")
        {
            examtype = "F";
        }
        else
            if (examtype == "2")
            {
                examtype = "M";
            }

        /*String StdID = obj_staff.Encrypt(sid, AESKey);
        String Year = obj_staff.Encrypt(txt_year.Text.Trim(), AESKey);
        String Semister = obj_staff.Encrypt(Convert.ToString(cmb_semester.SelectedItem), AESKey);

        String examtype = "";
        examtype = ddlExamtype.SelectedValue.ToString();

        if (examtype == "1")
        {
            examtype = obj_staff.Encrypt("F", AESKey);
        }
        else
            if (examtype == "2")
            {
                examtype = obj_staff.Encrypt("M", AESKey);
            }

        */
        string s = StdID + "|" + Year + "|" + Semister + "|" + examtype;

        byte[] StringAscII = System.Text.Encoding.ASCII.GetBytes(s);
        string a = "", token = "";
        int key = 2;

        for (int i = 0; i < StringAscII.Length; i++)
        {
            //if (StringAscII[i] != 124)
            //{
            //    StringAscII[i] = Convert.ToInt32(StringAscII[i]) + 2;
            //}
            if (StringAscII[i] == 124)
                a += Convert.ToString(StringAscII[i]) + "-";  //if i=0 a= 65, if i=1 a=66 and so on
            else
                a += Convert.ToString(StringAscII[i] + key) + "-";  //if i=0 a= 65, if i=1 a=66 and so on

           // a += Convert.ToString(StringAscII[i])+"-";//if i=0 a= 65, if i=1 a=66 and so on

        }
        token = Convert.ToString(a);

        Response.Redirect(string.Format("http://webportal.easternuni.edu.bd:9090/icampusnew/admitCardShow.action?token={0}", token));
       // Response.Redirect(string.Format("_PrintAdmitCard.aspx?token={0}", token));
        //
       // Page.RegisterClientScriptBlock("print", "<script>window.open('_PrintAdmitCard.aspx','','titlebar=yes,toolbar=no,scrollbars,resizable=true,height=650,width=900');</script>");
  
    //   http://localhost:3401/euweb/student/AdmitCard/std.aspx?StdID=133800044&Year=2016&Semister=Summer&ExType=F

        //http://localhost:3401/euweb/student/AdmitCard/std.aspx?StdID=unY8fFkKcIIO77iYejwq0n8br+sofnSYJfv4X895WWw=&Year=8OD3IO6EnLvL/OZCNC0KDg==&Semister=wjbhtJnRdrBIAVO4vn6bJg==&ExType=nQRu1uRH09Qdv3IsMtl2VQ==
    }

  
}
