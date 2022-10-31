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

public partial class admin_semester_course_listGED : System.Web.UI.Page
{
    string user = "";
    string sem = "";
    string DEPTCODE = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()) )
            {
                user = Session["ctrl_admin_Id"].ToString();
                DEPTCODE = "05";//Session["DEPTCODE"].ToString();
                Session["DEPTCODE"] = "05";
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
            GridView_courseList.Visible = true;
            grd_CourseListPrev.Visible = false;
            load_courses();
           
        }
        else
        {
            grd_CourseListPrev.Visible = true;
            GridView_courseList.Visible = false;
            loadPrev_courses();

        }

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
            ds.Merge(new admin_webService().get_all_offeredCourses(txt_year.Text, cmb_semester.SelectedValue.ToString() ));
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

        grd_CourseListPrev.DataSource = ds;
        grd_CourseListPrev.DataMember = "CourseList";
        grd_CourseListPrev.DataBind();

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
        int s=1;
        lbl_message.Text = ""+new cls_message().getMessage(1);

        foreach (DataRow dr in ds.Tables["CourseList"].Rows)
        {
            lbl_message.Text = "";
            dr["serial"] = s++;
        }

        GridView_courseList.DataSource = ds;
        GridView_courseList.DataMember = "CourseList";
        GridView_courseList.DataBind();

       
        
       // GridView_courseList_RowEditing(sender, e);

    }
    protected void GridView_courseList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView GridView_courseList = sender as GridView;
        GridViewRow row = GridView_courseList.Rows[e.NewEditIndex];
        String COURSECODE = Convert.ToString((row.Cells[0].FindControl("lblCOURSECODE") as Label).Text);

        Session["COURSECODE"] = COURSECODE;
        Session["sem"] = cmb_semester.SelectedValue.ToString();
        Session["year"] = txt_year.Text;

        //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('_course_allocationNew.aspx','_newtab');", true);

        Response.Write("<script>javascript:window.open('_course_allocationNew.aspx' );</script>");

        load_courses();
     //   Response.Redirect("_course_allocationNew.aspx");
        //Response.Write("<script>window.open( '_course_allocationNew.aspx' , 'popUpWindow' , 'height=750,width=620,left=50,top=30,resizable=false,scrollbars');</script>");
    }

    protected void grd_CourseListPrev_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView grd_CourseListPrev = sender as GridView;
        GridViewRow row = grd_CourseListPrev.Rows[e.NewEditIndex];
        String COURSECODE = Convert.ToString((row.Cells[0].FindControl("lblCOURSECODE") as Label).Text);

        Session["COURSECODE"] = COURSECODE;
        Session["sem"] = cmb_semester.SelectedValue.ToString();
        Session["year"] = txt_year.Text;

        Response.Write("<script>javascript:window.open('_course_allocation.aspx' );</script>");

        loadPrev_courses();

      //  Response.Redirect("_course_allocation.aspx");
    }
  
}
