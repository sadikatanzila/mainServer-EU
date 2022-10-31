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

public partial class admin_studentAcive_inactive : System.Web.UI.Page
{
    string user = "";
    string dep = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";

        btn_active.Visible = false;
        btn_inactive.Visible = false;

        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                user = Session["ctrl_admin_Id"].ToString();
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

        if (IsPostBack)
            dep = cmb_department.SelectedValue.ToString();

        load_department();
    }

    private void load_department()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allDepartment());

        cmb_department.DataSource = ds.Tables["departmentList"];
        cmb_department.DataTextField = "DEPCODE";
        cmb_department.DataValueField = "DEPCODE";
        cmb_department.DataBind();

        if (!String.IsNullOrEmpty(dep))
            cmb_department.SelectedValue = dep;
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        load_student();
    }

    private void load_student()
    {
        btn_active.Visible = true;
        btn_inactive.Visible = true;

        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_filterd_students(cmb_department.SelectedValue.ToString(), cmb_active_inactive.SelectedValue.ToString(), cmb_graduate.SelectedValue.ToString(), txt_batch.Text));

        ds.Tables["student"].Columns.Add("active");
        foreach (DataRow dr in ds.Tables["student"].Rows)
        {
            if (dr["S_CTRL"].ToString() == "1")
                dr["active"] = "true";
            else
                dr["active"] = "false";
        }

        if (ds.Tables["student"].Rows.Count == 0)
        {
            btn_active.Visible = false;
            btn_inactive.Visible = false;
            lbl_message.Text = "" + new cls_message().getMessage(1);
        }

        GridView_studentList.DataSource = ds;
        GridView_studentList.DataMember = "student";
        GridView_studentList.DataBind();
    }

    protected void btn_active_Click(object sender, EventArgs e)
    {
        int count = 0;
        string ids = "";
        student_webService obj_student=new student_webService();

        foreach (GridViewRow gr in GridView_studentList.Rows)
        {
            if (((CheckBox)(gr.FindControl("chkSelect"))).Checked == true)
            {
                obj_student.update_atudent_active_inactive(gr.Cells[1].Text,"1");
                count++;
            }
        }
        load_student();
    }
    protected void btn_inactive_Click(object sender, EventArgs e)
    {
        int count = 0;
        string ids = "";
        student_webService obj_student = new student_webService();

        foreach (GridViewRow gr in GridView_studentList.Rows)
        {
            if (((CheckBox)(gr.FindControl("chkSelect"))).Checked == true)
            {
                obj_student.update_atudent_active_inactive(gr.Cells[1].Text, "0");
                count++;
            }
        }
        load_student();
    }

    protected void GridView_studentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_studentList.PageIndex = e.NewPageIndex;
        load_student();
    }
}
