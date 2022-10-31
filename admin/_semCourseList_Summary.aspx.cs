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

public partial class admin_semCourseList_Summary : System.Web.UI.Page
{
    string user = "";
    string sem = "";
    string DEPTCODE = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                user = Session["ctrl_admin_Id"].ToString();
                DEPTCODE = Session["DEPTCODE"].ToString();
            }
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

        if (IsPostBack)
            sem = Request["cmb_semester"];
        else
            sem = cmb_semester.SelectedValue.ToString();


        //GridView_courseList.Visible = false;
        //GridView_courseList.Visible = false;

        Session["sem"] = "";
        Session["year"] = "";

        lbl_message.Text = "";
        btn_submit.Attributes.Add("onClick", " return chech_valid();");

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txt_year.Text.Trim() == "")
            return;

        int year_Sem = Convert.ToInt32(txt_year.Text + "" + cmb_semester.SelectedValue.ToString());


        //int semester = Convert.ToInt32(cmb_semester.SelectedValue.ToString());
        //int year = Convert.ToInt32(txt_year.Text);

        Session["sem"] = cmb_semester.SelectedValue.ToString();
        Session["year"] = txt_year.Text;

        if (year_Sem >= 20152)
        {
            grd_CourseList.Visible = true;
         //   grd_CourseList.Visible = false;
            load_courses();

        }
        else
        {
            grd_CourseList.Visible = true;
          //  GridView_courseList.Visible = false;
            loadPrev_courses();

        }
        lblmsg.Text = "Routine Allocation Summary of Selected Course List " + cmb_semester.SelectedItem.Text + ", " + txt_year.Text;
        //GridView_courseList.Visible = true;

    }

    private void loadPrev_courses()
    {
        DataSet ds = new DataSet();
        if (Session["DEPTCODE"].ToString() != "")
        {
            ds.Merge(new admin_webService().get_all_offeredCourses(txt_year.Text, cmb_semester.SelectedValue.ToString(), Session["DEPTCODE"].ToString()));
        }
        else
        {
            ds.Merge(new admin_webService().get_all_offeredCourses(txt_year.Text, cmb_semester.SelectedValue.ToString()));
        }

        for (int i = 0; i < ds.Tables["CourseList"].Rows.Count; i++)
        {
            DataRow dr = ds.Tables["CourseList"].Rows[i];
            for (int j = i + 1; j < ds.Tables["CourseList"].Rows.Count; j++)
            {
                DataRow drs = ds.Tables["CourseList"].Rows[j];

                if ((dr["COURSECODE"].ToString() == drs["COURSECODE"].ToString()) && (dr["CNAME"].ToString() == drs["CNAME"].ToString()))
                {
                    ds.Tables["CourseList"].Rows.RemoveAt(j);
                    j--;
                }
            }
        }

        ds.Tables["CourseList"].Columns.Add("serial");
        int s = 1;
        lbl_message.Text = "" + new cls_message().getMessage(1);

        foreach (DataRow dr in ds.Tables["CourseList"].Rows)
        {
            lbl_message.Text = "";
            dr["serial"] = s++;
        }

        grd_CourseList.DataSource = ds;
        grd_CourseList.DataMember = "CourseList";
        grd_CourseList.DataBind();

    }

    private void load_courses()
    {
        DataSet ds = new DataSet();
        if (Session["DEPTCODE"].ToString() != "")
        {
            ds.Merge(new admin_webService().get_all_offeredCourses(txt_year.Text, cmb_semester.SelectedValue.ToString(), Session["DEPTCODE"].ToString()));
        }
        else
        {
            ds.Merge(new admin_webService().get_all_offeredCourses(txt_year.Text, cmb_semester.SelectedValue.ToString()));
        }

        for (int i = 0; i < ds.Tables["CourseList"].Rows.Count; i++)
        {
            DataRow dr = ds.Tables["CourseList"].Rows[i];
            for (int j = i + 1; j < ds.Tables["CourseList"].Rows.Count; j++)
            {
                DataRow drs = ds.Tables["CourseList"].Rows[j];

                if ((dr["COURSECODE"].ToString() == drs["COURSECODE"].ToString()) && (dr["CNAME"].ToString() == drs["CNAME"].ToString()))
                {
                    ds.Tables["CourseList"].Rows.RemoveAt(j);
                    j--;
                }
            }
        }

        ds.Tables["CourseList"].Columns.Add("serial");
        int s = 1;
        lbl_message.Text = "" + new cls_message().getMessage(1);

        foreach (DataRow dr in ds.Tables["CourseList"].Rows)
        {
            lbl_message.Text = "";
            dr["serial"] = s++;
        }

        grd_CourseList.DataSource = ds;
        grd_CourseList.DataMember = "CourseList";
        grd_CourseList.DataBind();

        

        // GridView_courseList_RowEditing(sender, e);

    }

    protected void gdCourseOfferingdtl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView grd_CourseList = sender as GridView;
        GridViewRow row = grd_CourseList.Rows[e.NewEditIndex];
        String COURSECODE = Convert.ToString((row.Cells[0].FindControl("lblCOURSECODE") as Label).Text);

        Session["COURSECODE"] = COURSECODE;
        Session["sem"] = cmb_semester.SelectedValue.ToString();
        Session["year"] = txt_year.Text;

        //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('_course_allocationNew.aspx','_newtab');", true);

        Response.Write("<script>javascript:window.open('_course_allocationNew.aspx' );</script>");


    }


    protected void GridView_courseList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView grd_CourseList = sender as GridView;
        GridViewRow row1 = grd_CourseList.Rows[e.NewEditIndex];
        if (row1.RowType == DataControlRowType.DataRow)
        {
            String COURSECODE = Convert.ToString((row1.Cells[0].FindControl("lblCOURSECODE") as Label).Text);

            Session["COURSECODE"] = COURSECODE;
            Session["sem"] = cmb_semester.SelectedValue.ToString();
            Session["year"] = txt_year.Text;

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('_course_allocationNew.aspx','_newtab');", true);

            Response.Write("<script>javascript:window.open('_course_allocationNew.aspx' );</script>");
        }
        else
        {
            int year_Sem = Convert.ToInt32(txt_year.Text + "" + cmb_semester.SelectedValue.ToString());

            if (year_Sem >= 20152)
            {
                grd_CourseList.Visible = true;
                // grd_CourseList.Visible = false;
                load_courses();

            }
            else
            {
                grd_CourseList.Visible = true;
                //   GridView_courseList.Visible = false;
                loadPrev_courses();

            }

            int parent_index = e.NewEditIndex;

            grd_CourseList.EditIndex = parent_index;
            grd_CourseList.DataBind();
            GridViewRow row = grd_CourseList.Rows[parent_index];
            Label CourseKey = (Label)row.FindControl("lblCOURSEKEY");

            //save pub_id and edit_index in session for childgridview's use
            Session["lblCOURSEKEY"] = Convert.ToString(CourseKey.Text);
            Session["ParentGridViewIndex"] = parent_index;
        }

        

    }


    protected void GridView_courseList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
         //   e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#D3F2F8'");
          //  e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

            Label COURSEKEY = (Label)e.Row.FindControl("lblCOURSEKEY");
            string COURSE_KEY = COURSEKEY.Text;
            GridView gv = (GridView)e.Row.FindControl("gdCourseOfferingdtl");
            DataTable dt = new DataTable();
            dt.Merge(new student().get_OfferingCourseDetails(COURSE_KEY, "CourseDetails"));
            gv.DataSource = dt;
            gv.DataMember = "CourseDetails";
            gv.DataBind();

          //  dt = new admin_webService().get_OfferingCourseDetails(COURSE_KEY);
            
           // gv.DataSource = dt;
           // gv.DataBind();

          ///  Session["lblCOURSEKEY"] = Convert.ToString(COURSEKEY.Text);
         //   Session["ParentGridViewIndex"] = parent_index;

        }




    }
}