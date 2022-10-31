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
        if (Session.Count == 0)
            Response.Redirect("../_login.aspx");
        else
            if (String.IsNullOrEmpty(Session["user"].ToString()))
            {
                Response.Redirect("../_login.aspx");
            }

        if (IsPostBack)
        {
            dep = Convert.ToString(Session["ChkEv_deptid"]);//cmb_faculty.SelectedValue.ToString();
        }

        if (!Page.IsPostBack)
        {
            string teacher_id = Convert.ToString(Session["user"]);


            DataSet ds = new DataSet();
            ds.Merge(new admin_webService().Check_Dean(teacher_id));

            foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
            {
                if (Convert.ToInt32(dr["CHECK_DEAN"]) == 1)
                {
                    Session["ChkEv_deptid"] = Convert.ToString(dr["DEPARTMENT"]);
                    //dep = Convert.ToString(Session["ChkEv_deptid"]);
                    load_Teacher();
                    Session["TeacherID"] = "";
                    pnlDean.Visible = true;
                    pnlTeacher.Visible = false;
                }
                else
                {
                    Session["ChkEv_deptid"] = Convert.ToString(dr["DEPARTMENT"]);
                    pnlDean.Visible = false;
                    pnlTeacher.Visible = true;
                    Session["TeacherID"] = Convert.ToString(Session["user"]);
                    //Response.Redirect("../_home.aspx");
                }
            }
        }
        if (!IsPostBack) load_Teacher();
    }

    private void load_Teacher()
    {
        DataSet ds = new DataSet();

        ds.Merge(obj_admin.get_allAdvisor(Convert.ToString(Session["ChkEv_deptid"])));

        cmbTeacher.DataSource = ds.Tables["advisorList"];
        cmbTeacher.DataTextField = "STAFF_NAME";
        cmbTeacher.DataValueField = "STAFF_ID";
        cmbTeacher.DataBind();

        cmbTeacher.Items.Insert(0, " ");
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        if (cmbTeacher.SelectedValue.ToString() != null)
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

    protected void btnShow_Click(object sender, EventArgs e)
    {
        Session["TeacherID"] = Convert.ToString(Session["user"]);
        load_evaluation();
    }

    private void load_evaluation()
    {
        DataSet ds = new DataSet();
        string Teacher = "0";

        if (Convert.ToString(Session["TeacherID"]) != "")
        {
            Teacher = Convert.ToString(Session["user"]);
            ds.Merge(new admin_webService().get_teacher_EveSummery(Convert.ToString(Session["ChkEv_deptid"]), Teacher));
        }
        else
        {
            if (cmbTeacher.SelectedIndex <= 0)
                Teacher = "0";
            else
                Teacher = cmbTeacher.SelectedValue.ToString();


            ds.Merge(new admin_webService().get_teacher_EveSummery(Convert.ToString(Session["ChkEv_deptid"]), Teacher));
        }
       


        grdTeacherEve.DataSource = ds;
        grdTeacherEve.DataMember = "Summery_EVALUATION";
        grdTeacherEve.DataBind();

    }

 /*   protected void cmb_faculty_SelectedIndexChanged(object sender, EventArgs e)
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
    }*/

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

            /*if (Convert.ToString(Session["TeacherID"]) != "")
            {
            }
            else
            {
               
            }*/
            dt = new admin_webService().get_teacher_Evaluation_Summery(TEACHER_ID, Convert.ToString(Session["ChkEv_deptid"]));
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
