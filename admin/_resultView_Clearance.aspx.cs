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

public partial class admin_resultView_Clearance : System.Web.UI.Page
{
    string dep = "";
    string user = "";
    student_webService obj_student = new student_webService();

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
            dep = cmb_department.SelectedValue.ToString();

        lbl_message.Text = "";

        btn_submit.Attributes.Add("onClick", " return chech_valid();");
        load_department();
    }

    private void load_student()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_registered_student(cmb_semester.SelectedValue.ToString(), txt_year.Text, cmb_department.SelectedValue.ToString(), txt_batch.Text));

        ds.Tables["student"].Columns.Add("acStatus");
        ds.Tables["student"].Columns.Add("evStatus");
        ds.Tables["student"].Columns.Add("AccountPayment");

        if (ds.Tables["student"].Rows.Count == 0)
            lbl_message.Text = "" + new cls_message().getMessage(1);

        foreach (DataRow dr in ds.Tables["student"].Rows)
        {
            if (dr["RESULTVIEW_STATUS"].ToString() == "1")
                dr["acStatus"] = "true";
            else
                dr["acStatus"] = "false";

            if (dr["Course_Count"].ToString() != "0")
                dr["evStatus"] = "true";
            else
                dr["evStatus"] = "false";

            if (Convert.ToDecimal(dr["AMOUNT"].ToString()) >= 100)
                dr["AccountPayment"] = "true";
            else
                dr["AccountPayment"] = "false";
        }

        GridView_student.DataSource = ds;
        GridView_student.DataMember = "student";
        GridView_student.DataBind();


    }



    private void load_department()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allDepartment());

        cmb_department.DataSource = ds.Tables["departmentList"];
        cmb_department.DataValueField = "DEPCODE";
        cmb_department.DataTextField = "DEPCODE";
        cmb_department.DataBind();

        if (!String.IsNullOrEmpty(dep))
            cmb_department.SelectedValue = dep;
    }




    protected void btn_submit_Click(object sender, EventArgs e)
    {
        load_student();
    }

    protected void btn_cleared_Click(object sender, EventArgs e)
    {
        string ids = "";
        int count = 0;

        foreach (GridViewRow gr in GridView_student.Rows)
        {
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                ids = gr.Cells[1].Text + cmb_semester.SelectedValue.ToString() + txt_year.Text;
                obj_student.set_student_resultView_clearence(ids, "1");
                count++;
            }
        }
        if (count > 0)
        {
            lbl_message.Text = "" + new cls_message().getMessage(2);
            load_student();
        }

    }
    protected void btn_noCleared_Click(object sender, EventArgs e)
    {
        string ids = "";
        int count = 0;

        foreach (GridViewRow gr in GridView_student.Rows)
        {
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                ids = gr.Cells[1].Text + cmb_semester.SelectedValue.ToString() + txt_year.Text;
                obj_student.set_student_resultView_clearence(ids, "0");
                count++;
            }
        }
        if (count > 0)
        {
            lbl_message.Text = "" + new cls_message().getMessage(2);
            load_student();
        }
    }
}
