using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class staffs_Evaluation_show_Evaluation : System.Web.UI.Page
{
    admin_webService obj_admin = new admin_webService();
    string user = "";
    string dep = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else
                if (String.IsNullOrEmpty(Session["user"].ToString()))
                {
                    Response.Redirect("../_login.aspx");
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
          
           

        }
        catch (Exception exp) { Response.Redirect("../_home.aspx"); }

       
        //if (!IsPostBack)
        //    load_Teacher();
       
        //load_faculty();
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
        if ( (txt_s_year.Text != "" && cmb_s_semester.SelectedValue.ToString() != null)|| (txt_year.Text != "" && cmb_semester.SelectedValue.ToString() != null) )
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
            lblError.Text = "Please select Semester & Year";

        }
    }

    private void load_evaluation()
    {
        DataSet ds = new DataSet();
        string Teacher = "0";
        //if (txt_s_year.Text != "" && cmb_s_semester.SelectedValue.ToString() != null)
        //{
        if (Convert.ToString(Session["TeacherID"]) != "")
        {
            Teacher = Convert.ToString(Session["user"]);
            ds.Merge(new admin_webService().get_teacher_FinalEve(txt_year.Text, cmb_semester.SelectedValue.ToString(), Convert.ToString(Session["ChkEv_deptid"]), Teacher));
        }
        else
        {

            if (cmbTeacher.SelectedIndex <= 0)
                Teacher = "0";
            else
                Teacher = cmbTeacher.SelectedValue.ToString();
            ds.Merge(new admin_webService().get_teacher_FinalEve(txt_s_year.Text, cmb_s_semester.SelectedValue.ToString(), Convert.ToString(Session["ChkEv_deptid"]), Teacher));
        }


        //ds.Merge(new admin_webService().get_teacher_FinalEve(txt_s_year.Text, cmb_s_semester.SelectedValue.ToString(), Convert.ToString(Session["ChkEv_deptid"]), Teacher));
        //}


        grdTeacherEve.DataSource = ds;
        grdTeacherEve.DataMember = "Final_EVALUATION";
        grdTeacherEve.DataBind();

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

            if (Convert.ToString(Session["TeacherID"]) != "")
            {
                dt = new admin_webService().get_teacher_Evaluation(TEACHER_ID, txt_year.Text, cmb_semester.SelectedValue.ToString(), Convert.ToString(Session["ChkEv_deptid"]));
           
            }
            else
            {
                dt = new admin_webService().get_teacher_Evaluation(TEACHER_ID, txt_s_year.Text, cmb_s_semester.SelectedValue.ToString(), Convert.ToString(Session["ChkEv_deptid"]));
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


    protected void gdEvalutiondtl_RowEditing(object sender, GridViewEditEventArgs e)
    {

        GridView gdEvalutiondtl = sender as GridView;
        GridViewRow row = gdEvalutiondtl.Rows[e.NewEditIndex];
        String courseTeacherID = Convert.ToString((row.Cells[0].FindControl("lblCOURSE_TEACHER_ID") as Label).Text);
        Session["courseTeacherID"] = courseTeacherID;
        // Response.Redirect("_course_teacherEvalDetails.aspx");
        Response.Write("<script>window.open( '_course_teacherEvalDetails.aspx' , 'popUpWindow' , 'height=750,width=620,left=50,top=30,resizable=false,scrollbars');</script>");

    }

 
}
