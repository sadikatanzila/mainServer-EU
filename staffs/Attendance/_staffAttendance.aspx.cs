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
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;
using System.Net;

public partial class staffs_Attendance_staffAttendance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("_login.aspx");
            else if (String.IsNullOrEmpty(Session["user"].ToString()))
            {
                Response.Redirect("_login.aspx");
            }
        }
        catch (Exception erp) { Response.Redirect("_login.aspx"); }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txt_student_opening.Text != "" && txt_student_closing.Text != "")
        {
            string token = "", empCode = "", fDate = "", tDate = "";
            DataSet ds = new DataSet();
            ds.Merge(new staff_webService().get_STAFF_ID(Session["user"].ToString()));

            if (ds.Tables["WEB_TEACHER_STAFF"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
                {
                    empCode = "" + dr["VALUE"];
                }
            }

            HttpWebRequest httpWebRequest;


            //empCode = "110001";//Session["user"].ToString();
            fDate = (DateTime.ParseExact(txt_student_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)).ToString("dd-MMM-yyyy");
            tDate = (DateTime.ParseExact(txt_student_closing.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)).ToString("dd-MMM-yyyy");

            token = "empCode=" + empCode + "&fDate=" + fDate + "&tDate=" + tDate;


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

            Response.Redirect(string.Format("http://webportal.easternuni.edu.bd:8080/icampus/attendanceReport.action?token={0}", urlParameter));

        }
        else
        {
            lbl_message.Text = "Please select From Date & To Date";
        }
    }
}