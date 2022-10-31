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

public partial class admin_teacheractiveInactive : System.Web.UI.Page
{
    staff_webService obj_staff = new staff_webService();
    string dep = "";
    string user = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_inactive.Visible = false;
        btn_active.Visible = false;

        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                user = Session["ctrl_admin_Id"].ToString();
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }
        if (IsPostBack)
            dep = "" + cmb_department.SelectedValue.ToString();

        load_allDepartment();        

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        load_teacher();
    }
    

    private void load_teacher()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_all_filterStaff(cmb_activeInactive.SelectedValue.ToString(), "Teacher", cmb_department.SelectedValue.ToString(),cmb_type.Text));

        GridView_studentList.DataSource = ds;
        GridView_studentList.DataMember= "WEB_TEACHER_STAFFs";
        GridView_studentList.DataBind();

        if(ds.Tables["WEB_TEACHER_STAFFs"].Rows.Count>0)
        {
            if (cmb_activeInactive.SelectedValue.ToString() == "1")
                btn_inactive.Visible = true;
            else if (cmb_activeInactive.SelectedValue.ToString() == "0")
                btn_active.Visible = true;
        }
    }

    private void load_allDepartment()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allCollege());

        cmb_department.DataSource = ds.Tables["COLLEGE"];
        cmb_department.DataTextField = "COLLEGENAME";
        cmb_department.DataValueField = "COLLEGECODE";
        cmb_department.DataBind();

        if (!String.IsNullOrEmpty(dep))
            cmb_department.SelectedValue = dep;
    }

    protected void btn_inactive_Click(object sender, EventArgs e)
    {
        int count=0;
        string ids = "";
        foreach (GridViewRow gr in GridView_studentList.Rows)
        {
            if (((CheckBox)(gr.FindControl("chkSelect"))).Checked == true)
            {
                ids = ((Label)(gr.FindControl("STAFF_ID"))).Text;
                obj_staff.set_inactive_staff(ids);
                count++;
            }
        }
        load_teacher();
    }

    protected void btn_active_Click(object sender, EventArgs e)
    {
        int count = 0;
        string ids = "";
        foreach (GridViewRow gr in GridView_studentList.Rows)
        {
            if (((CheckBox)(gr.FindControl("chkSelect"))).Checked == true)
            {
                ids = ((Label)(gr.FindControl("STAFF_ID"))).Text;
                obj_staff.set_active_staff(ids);
                count++;
            }
        }
        load_teacher();
    }
    protected void GridView_studentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_studentList.PageIndex = e.NewPageIndex;
        load_teacher();
    }
}
