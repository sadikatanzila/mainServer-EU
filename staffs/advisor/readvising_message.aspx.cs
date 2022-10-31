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

public partial class staffs_advisor_Default : System.Web.UI.Page
{
    string sid = "", sem_Int = "", sem = "", year = "", totalCourse = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sid = Convert.ToString(Session["P_StudentId"]);
        lblSemester.Text = Session["P_Semester"].ToString();
        //sem_Year = Convert.ToString(Session["P_Semester"]);
        year = lblSemester.Text.Split(',')[1].Trim();
        sem = lblSemester.Text.Split(',')[0].Trim();

        DataSet ApprovalCourse_ds = new DataSet();
        if (sem == "Spring")
        {
            sem_Int = "1";
        }
        else
            if (sem == "Summer")
            {
                sem_Int = "2";
            }
            else
            {
                sem_Int = "3";
            }


        ApprovalCourse_ds.Merge(new student_webService().get_all_FinalAdvising_approvedCourse(sid, sem_Int, year));

        foreach (DataRow dr in ApprovalCourse_ds.Tables["approvedCourse"].Rows)
        {
            totalCourse += Convert.ToString(dr["Approvedcourse"]) + ", ";
        }
        lbl_message.Text = totalCourse + " course(s) are approved.";
        //btnPrint_Click();
    }


    //protected void btnPrint_Click()
    //{
    //    Session["P_Title"] = "Student Course Advising";
    //    Session["P_StudentId"] = lbl_sid.Text;
    //    Session["P_StudentName"] = lbl_sName.Text;
    //    Session["P_StudentDepartment"] = lbl_program.Text;
    //    Session["P_Semester"] = (sem == "1" ? "Spring" : (sem == "2" ? "Summer" : "Fall")) + ", " + year.ToString();
    //    Session["P_Date"] = DateTime.Now.ToString("dd/mm/yyyy");
    //    Session["P_AdvisorId"] = Session["user"];
    //    Session["P_AdvisorName"] = new staff_webService().get_staff_info(Session["user"].ToString()).Rows[0]["STAFF_NAME"].ToString();

    //    Page.RegisterClientScriptBlock("print", "<script>window.open('_studentCourseAdvisingReport.aspx','','titlebar=yes,toolbar=no,scrollbars,resizable=true,height=650,width=900');</script>");
    //}
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        Page.RegisterClientScriptBlock("print", "<script>window.open('_studentCourseAdvisingReport.aspx','','titlebar=yes,toolbar=no,scrollbars,resizable=true,height=650,width=900');</script>");
    }
}

