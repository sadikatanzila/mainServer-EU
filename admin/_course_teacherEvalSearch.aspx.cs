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

public partial class admin_course_teacherEvalSearch : System.Web.UI.Page
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

       
        //if (!IsPostBack) load_faculty();
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
                    pnlOffice.Visible = false;
                    Session["TeacherID"] = Convert.ToString(Session["user"]);
                }
                else
                {

                    Session["Chk_deptid"] = Convert.ToString(dr["DEPARTMENT"]);
                    Session["TeacherID"] = "";
                    pnlOffice.Visible = true;

                }
            }
        }
    }


    int j = 1; decimal dPageTotal = 0, dMaleTotal = 0, dFemaleTotal = 0;
    protected void grdTeacherEve_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = j.ToString();
            j++;


        }


    }
   
    protected void btn_show_Click(object sender, EventArgs e)
    {
        if (cmb_s_semester.SelectedValue.ToString() != null || txt_s_year.Text != "")
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
            lblError.Text = "Please select Faculty/Department";
        }
    }

    private void load_evaluation()
    {
        DataSet ds = new DataSet();
        string Teacher = "0";

        ds.Merge(new admin_webService().get_teacher_EveSummerySearch(txt_s_year.Text, cmb_s_semester.SelectedValue.ToString(), txtMark.Text, ddlMark.SelectedItem.Text));


       


        grdTeacherEve.DataSource = ds;
        grdTeacherEve.DataMember = "Summery_EVALUATION";
        grdTeacherEve.DataBind();

        lblHeading.Text = "Teachers' Evaluation Report Summary of "+cmb_s_semester.SelectedItem.Text+", "+ txt_s_year.Text + " of around " +txtMark.Text;

    }

   



  
}
