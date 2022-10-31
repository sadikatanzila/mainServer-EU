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

public partial class staffs_Evaluation_showAttendance : System.Web.UI.Page
{
    admin_webService obj_admin = new admin_webService();
    string user = "";
    string dep = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else
                if (String.IsNullOrEmpty(Session["user"].ToString()))
                {
                    Response.Redirect("../_login.aspx");
                }


            if (!Page.IsPostBack)
            {
                string teacher_id = Convert.ToString(Session["user"]);


                DataSet ds = new DataSet();
                ds.Merge(new admin_webService().Check_Dean(teacher_id));

                foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
                {
                    if (Convert.ToInt32(dr["CHECK_DEAN"]) == 1)
                    {
                        Session["ChkEv_deptid"] = Convert.ToString(dr["DEPARTMENT"]);
                        //dep = Convert.ToString(Session["ChkEv_deptid"]);
                        load_teacher();
                    }
                    else
                    {
                        Response.Redirect("../_home.aspx");
                    }
                }
            }
            //try
            //{
            //    if (Convert.ToString(Session["ChkEv_deptid"]) != null )
            //        dep = Convert.ToString(Session["ChkEv_deptid"]);
            //    else
            //        Response.Redirect("../_home.aspx");
            //}
            //catch (Exception exp) { Response.Redirect("../_home.aspx"); }


        }
        catch (Exception exp) { Response.Redirect("../_home.aspx"); }
    }


    private void load_teacher()
    {
        DataSet ds = new DataSet();

        ds.Merge(obj_admin.get_allAdvisor_Fulltime(Convert.ToString(Session["ChkEv_deptid"])));

        cmbTeacher.DataSource = ds.Tables["advisorList"];
        cmbTeacher.DataTextField = "STAFF_NAME";
        cmbTeacher.DataValueField = "STAFF_ID";
        cmbTeacher.DataBind();

        cmbTeacher.Items.Insert(0, " ");
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        if (cmbTeacher.SelectedIndex > 0)
        {
            if (txt_student_opening.Text != "" && txt_student_closing.Text != "")
            {
                try
                {
                    lblError.Visible = false;
                    load_attendance();
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = ex.ToString();
                }
            }
            else
            {
                lblError.Text = "Please select From Date & To Date";
            }
        }
        else
        {
            lblError.Text = "Please select Teacher";
        }
    }

    private void load_attendance()
    {
        string token = "", empCode = "", fDate = "", tDate = "";

        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_STAFF_ID(cmbTeacher.SelectedValue.ToString()));

        if (ds.Tables["WEB_TEACHER_STAFF"].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
            {
                empCode = "" + dr["VALUE"];
            }
        }


        // empCode = cmbTeacher.SelectedValue.ToString();

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

  
}