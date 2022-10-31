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

public partial class admin_StudentRoutine : System.Web.UI.Page
{
    string sid = "";
    string user = ""; 

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
    }


    private void load_class_toutine(string current_sem, string c_year)
    {
        //string current_sem = new admin_webService().get_currentSem_id();
        // string c_year = "" + DateTime.Today.Year;
        sid = txtStudentID.Text;
        this.Title = "Class Routine for " + new cls_tools().get_word_semester(current_sem) + " " + c_year;
        lbl_title.Text = "Class Routine for " + new cls_tools().get_word_semester(current_sem) + " " + c_year;
        try
        {
            lbl_student.Text = "" + sid + ": " + new staff_webService().get_a_student_information(sid).Rows[0]["sname"].ToString();
        }
        catch (Exception et) { }

        string regkey = "" + sid + "" + current_sem + c_year;

        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_semester_class_routine(regkey));

        GridView1.DataSource = ds;
        GridView1.DataMember = "class_routine";
        GridView1.DataBind();
    }


    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (txt_year.Text.Trim() != "" || cmb_semester.SelectedValue.ToString() != null)
        {
            string current_sem = cmb_semester.SelectedValue.ToString();
            string c_year = txt_year.Text;

            load_class_toutine(current_sem, c_year);
        }

    }
}
