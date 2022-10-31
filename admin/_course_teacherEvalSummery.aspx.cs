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

public partial class admin_course_teacherEvalSummery : System.Web.UI.Page
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
                    pnlDept.Visible = true;
                    Session["TeacherID"] = Convert.ToString(Session["user"]);
                    load_teacher(Convert.ToString(dr["DEPARTMENT"]));
                }
                else
                {

                    Session["Chk_deptid"] = Convert.ToString(dr["DEPARTMENT"]);
                    load_faculty();
                    Session["TeacherID"] = "";
                    pnlOffice.Visible = true;
                    pnlDept.Visible = false;

                }
            }
        }
    }

    protected void load_teacher(String DeptID)
    {

        DataSet ds = new DataSet();

        ds.Merge(obj_admin.get_allAdvisor(DeptID));

        ddlTeacher.DataSource = ds.Tables["advisorList"];
        ddlTeacher.DataTextField = "STAFF_NAME";
        ddlTeacher.DataValueField = "STAFF_ID";
        ddlTeacher.DataBind();

        ddlTeacher.Items.Insert(0, " ");

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
        if (cmb_faculty.SelectedValue.ToString() != null || cmbTeacher.SelectedValue.ToString() != null)
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


        if (Convert.ToString(Session["Chk_deptid"]) != "")
        {

            if (ddlTeacher.SelectedIndex <= 0)
                Teacher = "0";
            else
                Teacher = ddlTeacher.SelectedValue.ToString();


            ds.Merge(new admin_webService().get_teacher_EveSummery(Convert.ToString(Session["Chk_deptid"]), Teacher));
        }
        else
        {

            if (cmbTeacher.SelectedIndex <= 0)
                Teacher = "0";
            else
                Teacher = cmbTeacher.SelectedValue.ToString();


            ds.Merge(new admin_webService().get_teacher_EveSummery(cmb_faculty.SelectedValue.ToString(), Teacher));
        }
       


       


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
            GridView gv = (GridView)e.Row.FindControl("gdEvalutiondtl");
            DataTable dt = new DataTable();


            if (Convert.ToString(Session["Chk_deptid"]) != "")
            {
               // ds.Merge(new admin_webService().get_teacher_EveSummery(Convert.ToString(Session["Chk_deptid"]), Teacher));
                dt = new admin_webService().get_teacher_Evaluation_Summery(TEACHER_ID, Convert.ToString(Session["Chk_deptid"]));
            }
            else
            {
                dt = new admin_webService().get_teacher_Evaluation_Summery(TEACHER_ID, cmb_faculty.SelectedValue.ToString());
            }
            gv.DataSource = dt;
            gv.DataBind();



        }



    }
    protected void grdTeacherEve_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTeacherEve.PageIndex = e.NewPageIndex;
        load_evaluation();
    }



  
}
