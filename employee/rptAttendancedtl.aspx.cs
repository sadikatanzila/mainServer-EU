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
using System.Data;
using System.Configuration;
using System.IO;
using System.Data.OracleClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class employee_rptAttendancedtl : System.Web.UI.Page
{
    admin_webService obj_admin = new admin_webService();
    college obj_college = new college();
    string user = "";
    string dep = "";
    ReportDocument crystalReport = null;

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

        if (!Page.IsPostBack)
        {
            string employee_ID = Convert.ToString(Session["ctrl_admin_Id"]);


            DataSet ds = new DataSet();
            ds.Merge(new admin_webService().Check_Department(employee_ID));

            foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
            {
                if (Convert.ToString(dr["DEPARTMENT"]) != "")
                {
                    Session["Chk_deptid"] = Convert.ToString(dr["DEPARTMENT"]);
                    pnlAdmin.Visible = false;
                    pnlDept.Visible = true;
                    Session["TeacherID"] = Convert.ToString(Session["user"]);
                    load_teacher(Convert.ToString(dr["DEPARTMENT"]));
                }
                else
                {

                    Session["Chk_deptid"] = Convert.ToString(dr["DEPARTMENT"]);
                    load_faculty();
                    Session["TeacherID"] = "";
                    pnlAdmin.Visible = true;
                    pnlDept.Visible = false;

                }
            }
        }
        //    if (!IsPostBack) load_faculty();
    }

    protected void load_teacher(String DeptID)
    {

        DataSet ds = new DataSet();

        ds.Merge(obj_admin.get_allAdvisor(DeptID));

        ddlTeacher.DataSource = ds.Tables["advisorList"];
        ddlTeacher.DataTextField = "STAFF_NAME";
        ddlTeacher.DataValueField = "VALUE";
        ddlTeacher.DataBind();

        ddlTeacher.Items.Insert(0, " ");

    }
    private void load_faculty()
    {
        DataTable ds = new DataTable();
        ds.Merge(obj_college.get_allCollege("COLLEGE"));

        cmb_faculty.DataSource = ds;
        cmb_faculty.DataTextField = "COLLEGENAME";
        cmb_faculty.DataValueField = "COLLEGECODE";
        cmb_faculty.DataBind();

        if (!string.IsNullOrEmpty(dep))
            cmb_faculty.SelectedValue = dep;

        cmb_faculty_SelectedIndexChanged(null, null);
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        if ((txt_s_year.Text != "" && cmb_s_semester.SelectedValue.ToString() != null)
            || txt_year.Text != "" && cmb_semester.SelectedValue.ToString() != null)
        {
            try
            {
                lblError.Visible = false;
                load_evaluation();
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.ToString();
            }
        }
        else
        {
            lblError.Text = "Please select Semester & Year";
        }
    }

    private void load_evaluation()
    {
        DataSet ds = new DataSet();
        string Teacher = "0";

        if (cmbTeacher.SelectedIndex > 0 || ddlTeacher.SelectedIndex > 0)
        {
            if (Convert.ToString(Session["Chk_deptid"]) != "")
                Teacher = ddlTeacher.SelectedValue.ToString();
            else
                Teacher = cmbTeacher.SelectedValue.ToString();
            //  Teacher = "0";
        }
        else
        {
            Teacher = "0";
        }


        if (Convert.ToString(Session["Chk_deptid"]) != "")
        {
            if (txt_s_year.Text != "")
                ds.Merge(new admin_webService().get_TeachersCourseList_Deptwise(txt_s_year.Text, cmb_semester.SelectedValue.ToString(), Session["Chk_deptid"].ToString(), Teacher));
            else if (txt_year.Text != "")
                ds.Merge(new admin_webService().get_TeachersCourseList_Deptwise(txt_year.Text, cmb_semester.SelectedValue.ToString(), Session["Chk_deptid"].ToString(), Teacher));

            //ds.Merge(new admin_webService().get_teacher_FinalEve(txt_year.Text, cmb_semester.SelectedValue.ToString(), Convert.ToString(Session["Chk_deptid"]), Teacher));
        }
        else
        {

            if (cmb_faculty.SelectedValue.ToString() != "0")
                if (txt_s_year.Text != "")
                    ds.Merge(new admin_webService().get_TeachersCourseList_Deptwise(txt_s_year.Text, cmb_semester.SelectedValue.ToString(), cmb_faculty.SelectedValue.ToString(), Teacher));
                else
                    if (txt_year.Text != "")
                        ds.Merge(new admin_webService().get_TeachersCourseList_Deptwise(txt_year.Text, cmb_semester.SelectedValue.ToString(), cmb_faculty.SelectedValue.ToString(), Teacher));

                    else
                        if (txt_s_year.Text != "")
                            ds.Merge(new admin_webService().get_TeachersCourseList(cmb_s_semester.SelectedValue.ToString(), txt_s_year.Text, Teacher));
                        else
                            if (txt_year.Text != "")
                                ds.Merge(new admin_webService().get_TeachersCourseList(cmb_s_semester.SelectedValue.ToString(), txt_year.Text, Teacher));
            //  ds.Merge(new admin_webService().get_teacher_FinalEve(txt_s_year.Text, cmb_s_semester.SelectedValue.ToString(), cmb_faculty.SelectedValue.ToString(), Teacher));

        }


        grdTeacherEve.DataSource = ds;
        grdTeacherEve.DataMember = "Final_EVALUATION";
        grdTeacherEve.DataBind();
    }

    protected void cmb_faculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_faculty.SelectedIndex > -1)
        {
            DataSet ds = new DataSet();

            ds.Merge(obj_admin.get_allAdvisor(cmb_faculty.SelectedValue));

            cmbTeacher.DataSource = ds.Tables["advisorList"];
            cmbTeacher.DataTextField = "STAFF_NAME";
            cmbTeacher.DataValueField = "VALUE";
            cmbTeacher.DataBind();

            cmbTeacher.Items.Insert(0, " ");
        }
    }


    protected void grdTeacherEve_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (cmbTeacher.SelectedIndex > 0 || ddlTeacher.SelectedIndex > 0)
                grdTeacherEve.Columns[1].Visible = false;
            else
                grdTeacherEve.Columns[1].Visible = true;

            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#D3F2F8'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

            Label TEACHERID = (Label)e.Row.FindControl("lblTEACHER_ID");
            string TEACHER_ID = TEACHERID.Text;

            Label Course_TeachersID = (Label)e.Row.FindControl("lblCOURSE_TEACHER_ID");
            string Course_Teachers_ID = Course_TeachersID.Text;
            /* GridView gv = (GridView)e.Row.FindControl("gdEvalutiondtl");
             DataTable dt = new DataTable();

             if (Convert.ToString(Session["Chk_deptid"]) != "")
             {
               //  dt = new admin_webService().get_teacher_Evaluation(TEACHER_ID, txt_year.Text, cmb_semester.SelectedValue.ToString(), Convert.ToString(Session["Chk_deptid"]));
                 dt = new admin_webService().get_TeachersCourseList(TEACHER_ID, txt_year.Text, cmb_semester.SelectedValue.ToString(), Convert.ToString(Session["Chk_deptid"]));
         
             }
             else
             {

                 dt = new admin_webService().get_TeachersCourseListAll(TEACHER_ID, txt_s_year.Text, cmb_s_semester.SelectedValue.ToString(), cmb_faculty.SelectedValue.ToString());

             }


             gv.DataSource = dt;
             gv.DataBind();*/



        }



    }
    protected void grdTeacherEve_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTeacherEve.PageIndex = e.NewPageIndex;
        load_evaluation();
    }



    /*
  private void load_AttendanceSheet()
   {
       DataTable ds = new DataTable();

       if (cmb_course.SelectedValue.ToString() != "Select" )
       {
           ds.Merge(new student_webService().get_AttendanceSheetdtl_Teacherwise(cmb_course.SelectedValue.ToString(), "AttendanceListdtl"));
       }


       if (ds.Rows.Count > 0)
       {
           //  CrystalReportViewer1.Visible = true;
           crystalReport = new ReportDocument();
           Img1.Visible = true;
           Label1.Visible = true;
           lbl_message.Text = "";
           crystalReport.Load(Request.MapPath(Request.ApplicationPath) + "/employee/Report/_AttendanceCrosstab.rpt");
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
   
   protected void Img1_Click(object sender, ImageClickEventArgs e)
   {

       if (cmb_s_semester.SelectedValue.ToString() != "Select" && txt_s_year.Text != "")
       {
           if (ddlCourse.SelectedValue.ToString() != "Select" && txtSection.Text != "")
           {
               DataTable ds = new DataTable();
               ds.Merge(new student_webService().get_AttendanceSheetdtl(ddlCourse.SelectedValue.ToString(), txtSection.Text, "AttendanceListdtl"));

                  

               if (ds.Rows.Count > 0)
               {

                   CrystalReportViewer1.Visible = true;
                   crystalReport = new ReportDocument();
                   crystalReport.Load(Server.MapPath("~/employee/Report/_AttendanceCrosstab.rpt"));
                   crystalReport.SetDataSource(ds);
                   CrystalReportViewer1.ReportSource = crystalReport;
                   crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "StudentAttendanceDtl");



               }
               else
               {
                   CrystalReportViewer1.Visible = false;
                   Label1.Visible = false;
                   lbl_message.Text = "No Data Found";
               }




           }
           else
           {
               lbl_message.Text = "Please Enter the necessary Course & Section";
           }
       }
       else
       {
           lbl_message.Text = "Please Enter the necessary Year & Semester";

       }






   }
    
 */
    protected void grdTeacherEve_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable ds = new DataTable();

        GridView GridView_courseList = sender as GridView;
        GridViewRow row = GridView_courseList.Rows[e.NewEditIndex];
        String COURSECODE = Convert.ToString((row.Cells[0].FindControl("lblCOURSE_TEACHER_ID") as Label).Text);

        if (COURSECODE != "")
        {
            ds.Merge(new student_webService().get_AttendanceSheetdtl_Teacherwise(COURSECODE, "AttendanceListdtl"));
        }
        if (ds.Rows.Count > 0)
        {
            lbl_message.Text = "";
            //  CrystalReportViewer1.Visible = true;
            crystalReport = new ReportDocument();
            // Img1.Visible = true;
            //  Label1.Visible = true;
            lbl_message.Text = "";
            crystalReport.Load(Request.MapPath(Request.ApplicationPath) + "/employee/Report/_AttendanceCrosstab.rpt");
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
        /* <asp:HyperLinkField DataTextField="COURSECODE" HeaderText="Code" 
                                                     NavigateUrl="~/staffs/courses/_singe_course.aspx" DataNavigateUrlFields="COURSE_TEACHER_ID"
                                                      DataNavigateUrlFormatString="~/staffs/courses/_singe_course.aspx?code={0}" >
                                                     <ItemStyle Width="60px" />
                                                 </asp:HyperLinkField>*/
    }
    int j = 1;
    protected void grdTeacherEve_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = j.ToString();
            j++;


            if (cmbTeacher.SelectedIndex > 0 || ddlTeacher.SelectedIndex > 0)
                grdTeacherEve.Columns[2].Visible = false;
            else
                grdTeacherEve.Columns[2].Visible = true;
        }
    }
}