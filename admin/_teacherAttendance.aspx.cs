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

public partial class admin_teacherAttendance : System.Web.UI.Page
{
    admin_webService obj_admin = new admin_webService();
    string user = "";
    string dep = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                user = Session["ctrl_admin_Id"].ToString();
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

        if (IsPostBack)
        {
            dep = cmb_faculty.SelectedValue.ToString();
        }

        if (!IsPostBack) load_faculty();
    }

    private void load_faculty()
    {
        DataSet ds = new DataSet();
        ds.Merge(obj_admin.get_allCollege());

        cmb_faculty.DataSource = ds.Tables["COLLEGE"];
        cmb_faculty.DataTextField = "COLLEGENAME";
        cmb_faculty.DataValueField = "COLLEGECODE";
        cmb_faculty.DataBind();

        if (!string.IsNullOrEmpty(dep))
            cmb_faculty.SelectedValue = dep;

        cmb_faculty_SelectedIndexChanged(null, null);
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

    protected void cmb_faculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_faculty.SelectedIndex > -1)
        {
            DataSet ds = new DataSet();

            ds.Merge(obj_admin.get_allAdvisor_Fulltime(cmb_faculty.SelectedValue));

            cmbTeacher.DataSource = ds.Tables["advisorList"];
            cmbTeacher.DataTextField = "STAFF_NAME";
            cmbTeacher.DataValueField = "STAFF_ID";
            cmbTeacher.DataBind();

            cmbTeacher.Items.Insert(0, " ");
        }
    }


}