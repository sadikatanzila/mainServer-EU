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

public partial class admin_add_student_advisor : System.Web.UI.Page
{
    admin_webService obj_adminWs = new admin_webService();
    staff_webService obj_staff = new staff_webService();
    string dep = "";
    string advisor = "";
    string user = "";
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

        btn_show.Attributes.Add("onClick","return chech_valid(); ");
        btn_set_advisor.Attributes.Add("onClick", "return chech_valid(); ");


        if (IsPostBack)
        {
            dep = Request["ctl00$ContentPlaceHolder_tracker$cmb_department"];
            advisor = Request["ctl00$ContentPlaceHolder_tracker$cmb_advisor"];
        }
        else
        {
            loadinformations();
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        load_student();
    }

    private void load_student()
    {
        GridView_studentList.Visible = true;
        DataSet ds = new DataSet();
        //ds.Merge(obj_adminWs.get_allStudent_ofA_aBatch(cmb_department.SelectedValue.ToString(), txt_batch.Text)); //studentList chkSelect

        string tr_value = ddlTranfered.SelectedValue.ToString();

        if (tr_value == "0")
        {
            ds.Merge(obj_adminWs.get_allStudent_ofA_aBatch_transperred(cmb_department.SelectedValue.ToString(), txt_batch.Text));
        }
        else
            if (tr_value == "1")
            {
                ds.Merge(obj_adminWs.get_allStudent_ofA_aBatch_transperred_Only(cmb_department.SelectedValue.ToString(), txt_batch.Text));
            }
            else
            {
                ds.Merge(obj_adminWs.get_allStudent_ofA_aBatch_Not_transperred(cmb_department.SelectedValue.ToString(), txt_batch.Text));
            }
        ds.Tables["studentList"].Columns.Add("CGPA");
        ds.Tables["studentList"].Columns.Add("credit");

       
        foreach (DataRow dr in ds.Tables["studentList"].Rows)
        {

            dr["CGPA"] = "";// +Math.Round(obj_staff.get_latest_cgpa(dr["sid"].ToString()),2);
            dr["credit"] = "";// + obj_staff.get_total_completed_credit(dr["sid"].ToString());

            if (dr["ADVISOR_ID"].ToString() == cmb_advisor.SelectedValue.ToString())
            {
                dr["credit"] = dr["credit"].ToString() + ":1";
            }
            else
                dr["credit"] = dr["credit"].ToString() + ":0";
        }

        GridView_studentList.DataSource = ds;
        GridView_studentList.DataMember = "studentList";
        GridView_studentList.DataBind();

        string isCheck = "";
        foreach (GridViewRow row in GridView_studentList.Rows)
        {
            isCheck = row.Cells[3].Text.Split(':')[1];
            row.Cells[3].Text = row.Cells[3].Text.Split(':')[0];

            if (isCheck == "1")
            {
                ((CheckBox)row.FindControl("chkSelect")).Checked = true;
            }
        }  
    
    }

    private void loadinformations()
    {
        //load_courses();
        //load_assignments();
        //load_lectures();
        //load_outline();
        load_deprtment();
        load_teacher();
    }
    private void  load_deprtment()
    {
        DataSet ds = new DataSet();
        ds.Merge(obj_adminWs.get_allDepartment());

        cmb_department.DataSource = ds.Tables["departmentList"];
        cmb_department.DataTextField = "DEPCODE";
        cmb_department.DataValueField = "DEPCODE";
        cmb_department.DataBind();
		
		ds.Merge(obj_adminWs.get_allCollege());
        ddlAdvisorFaculty.DataSource = ds.Tables["COLLEGE"];
        ddlAdvisorFaculty.DataTextField = "COLLEGENAME";
        ddlAdvisorFaculty.DataValueField = "COLLEGECODE";
        ddlAdvisorFaculty.DataBind();
        ddlAdvisorFaculty.Items.Insert(0, "All");
		
        if (!string.IsNullOrEmpty(dep)) cmb_department.SelectedValue = dep;
    }

    private void load_teacher()
    {
        DataSet ds = new DataSet();
        ds.Merge(obj_adminWs.get_allAdvisor());

        cmb_advisor.DataSource = ds.Tables["advisorList"];
        cmb_advisor.DataTextField = "STAFF_NAME";
        cmb_advisor.DataValueField = "STAFF_ID";
        cmb_advisor.DataBind();

        if (!string.IsNullOrEmpty(advisor)) cmb_advisor.SelectedValue = advisor;

    }



    protected void btn_set_advisor_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("STUDENT");
        ds.Tables["STUDENT"].Columns.Add("sid");
        ds.Tables["STUDENT"].Columns.Add("ADVISOR_ID");

        foreach (GridViewRow row in GridView_studentList.Rows)
        {
            if (((CheckBox)row.FindControl("chkSelect")).Checked == true)
            {
               // row.Cells[row.Cells[1].]
              DataRow dr = ds.Tables["STUDENT"].NewRow();
              dr["sid"] = ((HyperLink)row.FindControl("hp_link_sid")).Text;
              dr["ADVISOR_ID"] = advisor;
              ds.Tables["STUDENT"].Rows.Add(dr);
            }
        }

       obj_adminWs.save_advisor(ds);
       load_student();

    }
	
	protected void ddlAdvisorFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdvisorFaculty.SelectedIndex > -1)
        {
            DataSet ds = new DataSet();

            if (ddlAdvisorFaculty.SelectedIndex == 0)
            {
                ds.Merge(obj_adminWs.get_allAdvisor());
            }
            else
            {
                ds.Merge(obj_adminWs.get_allAdvisor(ddlAdvisorFaculty.SelectedValue));
            }

            cmb_advisor.DataSource = ds.Tables["advisorList"];
            cmb_advisor.DataTextField = "STAFF_NAME";
            cmb_advisor.DataValueField = "STAFF_ID";
            cmb_advisor.DataBind();
        }
    }

    protected void chkAllOrNone_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAllOrNone = sender as CheckBox;

        foreach (GridViewRow row in GridView_studentList.Rows)
        {
            ((CheckBox)row.FindControl("chkSelect")).Checked = chkAllOrNone.Checked;
        }
    }
}
