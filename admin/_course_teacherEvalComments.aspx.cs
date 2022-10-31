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

public partial class admin_course_teacherEvalComments : System.Web.UI.Page
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
        if (cmb_faculty.SelectedValue.ToString() != null)
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

        if (cmbTeacher.SelectedIndex <= 0)
            Teacher = "0";
        else
            Teacher = cmbTeacher.SelectedValue.ToString();

        if (txt_year.Text == "")
            txt_year.Text = "0";

        ds.Merge(new admin_webService().get_teacher_CommentsSummery(cmb_faculty.SelectedValue.ToString(), Teacher, txt_year.Text, cmb_s_semester.SelectedValue.ToString()));


        grdTeacherEve.DataSource = ds;
        grdTeacherEve.DataMember = "Summery_EVALUATION";
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
            cmbTeacher.DataValueField = "STAFF_ID";
            cmbTeacher.DataBind();

            cmbTeacher.Items.Insert(0, " ");
        }
    }

    protected void grdTeacherEve_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int parent_index = e.NewEditIndex;

        grdTeacherEve.EditIndex = parent_index;
        grdTeacherEve.DataBind();
        GridViewRow row = grdTeacherEve.Rows[parent_index];
        Label pub_id_lbl = (Label)row.FindControl("lblTEACHER_ID");

        //save pub_id and edit_index in session for childgridview's use
        Session["lblTEACHER_ID"] = Convert.ToInt32(pub_id_lbl.Text);
        Session["ParentGridViewIndex"] = parent_index;
    }




    protected void grdTeacherEve_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#D3F2F8'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

            Label TEACHERID = (Label)e.Row.FindControl("lblTEACHER_ID");
            string TEACHER_ID = TEACHERID.Text;
            GridView gv = (GridView)e.Row.FindControl("gdEvalutionCmnts");
            DataTable dt = new DataTable();
            dt = new admin_webService().get_teacher_Evaluation_Comments(TEACHER_ID, cmb_faculty.SelectedValue.ToString(), txt_year.Text, cmb_s_semester.SelectedValue.ToString());

            gv.DataSource = dt;
            gv.DataBind();



        }



    }
    protected void grdTeacherEve_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTeacherEve.PageIndex = e.NewPageIndex;
        load_evaluation();
    }


    protected void gdEvalutionCmnts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#D3F2F8'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");



            Label COURSE_TEACHER = (Label)e.Row.FindControl("lblCOURSE_TEACHER_ID");
            string COURSE_TEACHER_ID = COURSE_TEACHER.Text;

            /*  Label Year_ = (Label)e.Row.FindControl("lblYEAR");
              string Year = Year_.Text;

              Label SEMESTERID = (Label)e.Row.FindControl("lblSemisterID");
              string SEMESTER_ID = SEMESTERID.Text;

              Label COURSE_NAME = (Label)e.Row.FindControl("lblCourse");
              string COURSENAME = COURSE_NAME.Text;

              Label SEC = (Label)e.Row.FindControl("lblSECTION");
              string SECTION = SEC.Text;*/

            GridView gv = (GridView)e.Row.FindControl("gdEvalutionCmntsdtl");
            DataTable dt = new DataTable();
            dt = new admin_webService().get_teacher_Evaluation_Commentsdtl(COURSE_TEACHER_ID);

            gv.DataSource = dt;
            gv.DataBind();



        }



    }
}
