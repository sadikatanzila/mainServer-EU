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

public partial class student_academics_class_routine : System.Web.UI.Page
{
    string sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_student.Text = "";

        try
        {

            if (!String.IsNullOrEmpty(Session["ctrlId"].ToString()))
            {
                sid = Convert.ToString(Session["ctrlId"]);  // Request.QueryString["sid"].ToString(); 
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

     
}
