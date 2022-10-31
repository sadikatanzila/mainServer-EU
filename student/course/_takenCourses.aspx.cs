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

public partial class student_course_takenCourses : System.Web.UI.Page
{
    string sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        lbl_semester.Text = "";
        btn_submit.Attributes.Add("onClick", " return save_check();");

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

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        loadTaken_courses();
    }


    private void loadTaken_courses()
    {
        Session["sem"] = "" + cmb_semester.SelectedValue.ToString();
        Session["year"] = "" + txt_year.Text.Trim();

        if (!new student_webService().is_register_student(sid, cmb_semester.SelectedValue.ToString(), txt_year.Text.ToString()))
        {
            lbl_message.Text = "" + new cls_message().getMessage(9);
            return;
        }

        DataSet ds = new DataSet();
        // ds.Merge(new student_webService().get_semester_GradeSheet(sid + cmb_semester.SelectedValue.ToString() + txt_year.Text.ToString()));
        ds.Merge(new student_webService().get_semester_takenCourse_Evalute(sid + cmb_semester.SelectedValue.ToString() + txt_year.Text.ToString()));


        if (ds.Tables["gradeSheet"].Rows.Count == 0)
        {
            if (new student_webService().get_all_preOffered_courses(sid, cmb_semester.SelectedValue.ToString(), txt_year.Text.ToString()).Rows.Count > 0)
                lbl_message.Text = "" + new cls_message().getMessage(15);
            else
                lbl_message.Text = "" + new cls_message().getMessage(16);

        }
        else
        {

            for (int count = 0; count < ds.Tables["gradeSheet"].Rows.Count; count++)
            {
                DataRow drt = ds.Tables["gradeSheet"].Rows[0];
                for (int j = count + 1; j < ds.Tables["gradeSheet"].Rows.Count; j++)
                {
                    if (ds.Tables["gradeSheet"].Rows[count]["COURSECODE"].ToString() == ds.Tables["gradeSheet"].Rows[j]["COURSECODE"].ToString())
                    {
                        ds.Tables["gradeSheet"].Rows.RemoveAt(j);
                        j--;
                    }
                }
            }

            ds.Tables["gradeSheet"].Columns.Add("teacher");
            ds.Tables["gradeSheet"].Columns.Add("course_teacher_id");

            ds.Tables["gradeSheet"].Columns.Add("SCH_CLS_1");
            ds.Tables["gradeSheet"].Columns.Add("SCH_CLS_2");

            String values = "";
            String values1 = "";
            foreach (DataRow dr in ds.Tables["gradeSheet"].Rows)
            {
                values = new staff_webService().get_a_teacher_ofA_course(dr["COURSEKEY"].ToString(), dr["GGROUP"].ToString());
                if (values != "")
                {
                    dr["teacher"] = values.Split(':')[0];
                    dr["COURSE_TEACHER_ID"] = values.Split(':')[1];

                    values1 = new staff_webService().get_a_Routine_CT(dr["COURSE_TEACHER_ID"].ToString());
                    if (values1 != "")
                    {
                        dr["SCH_CLS_1"] = values1.Split('_')[0];
                        dr["SCH_CLS_2"] = values1.Split('_')[1];
                    }
                }
            }
        }
        GridView_courseList.DataSource = ds;
        GridView_courseList.DataMember = "gradeSheet";
        GridView_courseList.DataBind();
    }
}
