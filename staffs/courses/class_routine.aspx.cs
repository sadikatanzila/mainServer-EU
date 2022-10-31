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

public partial class staffs_class_routine : System.Web.UI.Page
{
    string teacherId= "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_student.Text = "";

        try
        {

            if (!String.IsNullOrEmpty(Session["user"].ToString()))
            {
                teacherId = Session["user"].ToString();
            }
            else
            {
                Response.Redirect("../_login.aspx");
            }
        }
        catch (Exception er) { Response.Redirect("../_login.aspx"); }

        load_class_toutine();
    }

    private void load_class_toutine()
    {
        string current_sem = new admin_webService().get_currentSem_id();
        string c_year = "" + DateTime.Today.Year;

        this.Title = "Class Routine for " + new cls_tools().get_word_semester(current_sem) + " " + c_year;
        lbl_title.Text = "Class Routine for " + new cls_tools().get_word_semester(current_sem) + " " + c_year;
        try
        {
            lbl_student.Text = "" + teacherId + ": " + new staff_webService().get_a_teacherInfo(teacherId).Rows[0]["STAFF_NAME"].ToString();
        }
        catch (Exception et) { }

       // string regkey = "" + sid + "" + current_sem + c_year;

       
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_allCourses_ofA_semester(current_sem, c_year));
        
        for (int i = 0; i < ds.Tables["coursList"].Rows.Count; i++)
        {
            for (int j = i+1; j < ds.Tables["coursList"].Rows.Count; j++)
            {
                if (ds.Tables["coursList"].Rows[i]["COURSE_TEACHER_ID"].ToString() == ds.Tables["coursList"].Rows[j]["COURSE_TEACHER_ID"].ToString())
                    ds.Tables["coursList"].Rows.RemoveAt(j--);
            }
        }

        GridView1.DataSource = ds;
        GridView1.DataMember = "coursList";
        GridView1.DataBind();

        
    }

}
