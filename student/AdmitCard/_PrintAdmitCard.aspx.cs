using System;
using System.Data;

public partial class student_AdmitCard_PrintAdmitCard : System.Web.UI.Page
{
    string sid = "";

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

        //if (Convert.ToString(Session["ctrlId"]) != null
        //   || Convert.ToString(Session["ctrlId"]) != null
        //    || Convert.ToString(Session["ctrlId"]) != null
        //    || Convert.ToString(Session["ExamType"]) != null
        //     || Convert.ToString(Session["semName"]) != null)
        //{
        //    loadTaken_courses();
        //}
        //else
        //{
        //    Response.Redirect("../_login.aspx");
        //}
       
    }

    //private void loadTaken_courses()
    //{
    //    String semister = "", year = "", sid = "", ExamType="" , SemisterName="";
    //    semister = Convert.ToString(Session["sem"]);
    //    year = Convert.ToString(Session["year"]);
    //    sid = Convert.ToString(Session["sid"]);
    //    ExamType = Convert.ToString(Session["ExamType"]);
    //    SemisterName = Convert.ToString(Session["semName"]);
    //    lblTitle.Text = ExamType + ", " + SemisterName + " " + year;

     
    //    DataSet ds = new DataSet();
    //    //  ds.Merge(new student_webService().get_semester_takenCourse_Evalute(sid + cmb_semester.SelectedValue.ToString() + txt_year.Text.ToString()));
    //    ds.Merge(new student_webService().get_AdmitCard_takenCourse(sid + semister + year));



    //    if (ds.Tables["CourseList"].Rows.Count == 0)
    //    {
    //        pnlExamCourse.Visible = false;

    //    }
    //    else
    //    {
    //        pnlExamCourse.Visible = true;
    //        GridView_courseList.DataSource = ds;
    //        GridView_courseList.DataMember = "CourseList";
    //        GridView_courseList.DataBind();

    //        foreach (DataRow dr in ds.Tables["CourseList"].Rows)
    //        {
    //            lblStudentId.Text = dr["SID"].ToString();
    //            lblStudentName.Text = dr["SNAME"].ToString();
    //            lblFaculty.Text = dr["COLLEGENAME"].ToString();
    //            lblDepartment.Text = dr["SPROGRAM"].ToString();

    //            if (String.IsNullOrEmpty(dr["S_PICTURE"].ToString()))
    //                img_myPicture.ImageUrl = "~/student/profile/student_images/no_image.gif";
    //            else
    //                img_myPicture.ImageUrl = "~/student/profile/student_images/" + sid + "." + dr["S_PICTURE"].ToString().Split('.')[dr["S_PICTURE"].ToString().Split('.').Length - 1];
           

    //        }
    //    }

    //}
}
