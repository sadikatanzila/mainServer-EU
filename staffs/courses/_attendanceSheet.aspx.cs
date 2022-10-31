using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using System.Data.OracleClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Web.Security;


public partial class staffs_courses_attendanceSheet : System.Web.UI.Page
{

    string code = "", teacher_ID = "", slot_id = "";
    ReportDocument crystalReport = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else if (String.IsNullOrEmpty(Session["user"].ToString()))
            {
                Response.Redirect("../_login.aspx");
            }
        }
        catch (Exception ert) { Response.Redirect("../_login.aspx"); }

        if (Session["user"].ToString() == "")
            Response.Redirect("_course_list.aspx");
        if (String.IsNullOrEmpty(Session["sem"].ToString()) || String.IsNullOrEmpty(Session["year"].ToString()))
            Response.Redirect("_course_list.aspx");

      

        if (!IsPostBack)
        {
            if (Request.QueryString["code"] != null)
            {
                code = Request.QueryString["code"].ToString();
                teacher_ID = Convert.ToString(Session["user"]);
            }
            load_courses();
           
        }
        else
        {
            code = cmb_course.SelectedValue.ToString();
        }
    }

    


 
    private void load_courses()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_allCourses_ofA_semesterNew(Session["sem"].ToString(), Session["year"].ToString()));

        foreach (DataRow dr in ds.Tables["coursList"].Rows)
        {
            dr["CNAME"] = dr["COURSECODE"].ToString()+" - "+dr["CNAME"].ToString() + "(" + dr["SECTION"].ToString() + ")";         


        }

        cmb_course.DataSource = ds.Tables["coursList"];
        cmb_course.DataTextField = "CNAME";
        cmb_course.DataValueField = "COURSE_TEACHER_ID";
        cmb_course.DataBind();
        cmb_course.SelectedValue = code;

        


    }


    protected void btn_submit_Click(object sender, EventArgs e)
    {
        
     //   txt_date.Text = "";
        load_AttendanceSheet();
     //   load_student_attendance();
    //    load_timeslots_course();
    }


    private void load_AttendanceSheet()
    {
        DataTable ds = new DataTable();

        if (cmb_course.SelectedValue.ToString() != "Select" || lblCourseKey.Text != "" || lblGGROUP.Text != "")
        {
            ds.Merge(new student_webService().get_AttendanceSheet(cmb_course.SelectedValue.ToString(), "AttendanceList"));
        }


        if (ds.Rows.Count > 0)
        {
            //  CrystalReportViewer1.Visible = true;
            crystalReport = new ReportDocument();
            Img1.Visible = true;
            Label1.Visible = true;
            lbl_message.Text = "";
            crystalReport.Load(Request.MapPath(Request.ApplicationPath) + "/staffs/report/_rptClassAttendance.rpt");
            crystalReport.SetDataSource(ds);
            BinaryReader stream = new BinaryReader(crystalReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
            Response.Flush();
            Response.Close();
        }
        else
        {
            //  CrystalReportViewer1.Visible = false;
            //   Img1.Visible = false;
            //   Label1.Visible = false;
            lbl_message.Text = "No data Found";
        }
    }

 
    protected void btn_show_Click(object sender, EventArgs e)
    {
       // load_student_attendance();
    }

    private void load_student_attendance()
    {
        //  txt_date.Text = Request["ctl00$ContentPlaceHolder_content$txt_date"].ToString();
        string Coursekey = "", teacherID = "", section = "", teacher = "", timeslot = "";
        int check = 0;

     //   Coursekey = Convert.ToString(Session["sem"].ToString()) + Convert.ToString(Session["year"].ToString()) + lbl_course_code.Text;
        teacherID = code;
        teacher = Convert.ToString(Session["user"]);

        
            
            DataSet dsn = new DataSet();
         //   dsn.Merge(new staff_webService().get_attendance_ofA_Courses(Coursekey, teacher, section, timeslot, cmb_course.SelectedValue.ToString(), DateTime.ParseExact(txt_date.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)));

            if (dsn.Tables["studentList"].Rows.Count > 0)
            {

               

            }
            else
            {
               

            }

       

    }


   
    protected void Img1_Click(object sender, ImageClickEventArgs e)
    {
        DataTable ds = new DataTable();


        if (cmb_course.SelectedValue.ToString() != "Select" || lblCourseKey.Text != "" || lblGGROUP.Text != "")
        {
            ds.Merge(new student_webService().get_AttendanceSheet(cmb_course.SelectedValue.ToString(), "AttendanceList"));
        }


        if (ds.Rows.Count > 0)
        {

            CrystalReportViewer1.Visible = true;
            crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/staffs/report/_rptClassAttendance.rpt"));
            crystalReport.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = crystalReport;
            crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AttendanceListpdf");



        }
        else
        {
            CrystalReportViewer1.Visible = false;
            Label1.Visible = false;
            lbl_message.Text = "No Data Found";
        }
    }
}
