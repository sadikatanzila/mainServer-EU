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
using System.Net;


public partial class student_AdmitCard_AdmitCard : System.Web.UI.Page
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


        string DUE = "", SemDue = "", graceAmt = "";
        DataSet InsDate_ds = new DataSet();
        InsDate_ds.Merge(new student_webService().get_FN_GET_PER_SEM_DUE(sid, Convert.ToString(txt_year.Text), cmb_semester.SelectedValue.ToString()));
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

        if (Convert.ToDouble(graceAmt) > 0)
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
    //string AESKey = "Key@AES1234";

    private void loadTaken_courses()
    {
        string token = "";

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

        string s = StdID + "|" + Year + "|" + Semister + "|" + examtype;

       

        //string student = "sid =" + StdID + "&year =" + Year + "&sem =" + Semister + "&admitType =" + examtype;

        HttpWebRequest httpWebRequest;
        token = "sid=" + StdID + "&year=" + Year + "&sem=" + Semister + "&admitType=" + examtype;   //sid=053400001&year=2018&sem=1&admitType=M


        string url = "http://webportal.easternuni.edu.bd:8080/icampus/tokenProvider.action?{0}" + token;

        string responseBody = string.Empty;
        httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
        httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
        httpWebRequest.Accept = "*/*";
        httpWebRequest.Method = "GET";

        using (HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse)
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseBody = reader.ReadToEnd();
            }
        }
        string myString = responseBody.Replace("\r\n", string.Empty);

        String urlParameter = HttpContext.Current.Server.UrlEncode(myString);


        Response.Redirect(string.Format("http://webportal.easternuni.edu.bd:8080/icampus/admitCardShow.action?token={0}", urlParameter));



      /*  byte[] StringAscII = System.Text.Encoding.ASCII.GetBytes(s);
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

       // Response.Redirect("~/feedback.aspx" + "?PreviousPage=" + HttpContext.Current.Request.Url.AbsolutePath);

        Response.Redirect(string.Format("http://webportal.easternuni.edu.bd:8080/icampus/admitCardShow.action?token={0}", token));
       // Response.Redirect(string.Format("_PrintAdmitCard.aspx?token={0}", token));
        //
       // Page.RegisterClientScriptBlock("print", "<script>window.open('_PrintAdmitCard.aspx','','titlebar=yes,toolbar=no,scrollbars,resizable=true,height=650,width=900');</script>");
  
    //   http://localhost:3401/euweb/student/AdmitCard/std.aspx?StdID=133800044&Year=2016&Semister=Summer&ExType=F

        //http://localhost:3401/euweb/student/AdmitCard/std.aspx?StdID=unY8fFkKcIIO77iYejwq0n8br+sofnSYJfv4X895WWw=&Year=8OD3IO6EnLvL/OZCNC0KDg==&Semister=wjbhtJnRdrBIAVO4vn6bJg==&ExType=nQRu1uRH09Qdv3IsMtl2VQ==
   
       */
    }


    private void previous()
    {

        string token = "";

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

        string s = StdID + "|" + Year + "|" + Semister + "|" + examtype;

        string student = "sid =" + StdID + "&year =" + Year + "&sem =" + Semister + "&admitType =" + examtype;

        HttpWebRequest httpWebRequest;
        token = "sid=" + StdID + "&year=" + Year + "&sem=" + Semister + "&admitType=" + examtype;   //sid=053400001&year=2018&sem=1&admitType=M


        string url = "http://webportal.easternuni.edu.bd:8080/icampus/tokenProvider.action?{0}" + token;

        string responseBody = string.Empty;
        httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
        httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
        httpWebRequest.Accept = "*/*";
        httpWebRequest.Method = "GET";

        using (HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse)
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseBody = reader.ReadToEnd();
            }
        }
        string myString = responseBody.Replace("\r\n", string.Empty);
       // System.ou
      //  system.out.println(myString);

        //  string urlnxt = "http://webportal.easternuni.edu.bd:8080/icampus/tokenProvider.action?" + myString + "," + "UTF-8";


       // 
        
        
        
        
        
        ;
    }

  
}
