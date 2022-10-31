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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;
using System.Net;

public partial class employee_RoleDistribution : System.Web.UI.Page
{
    admin_webService obj_admin = new admin_webService();
    string user = "";
    string dep = "", code = "";
    DataTable dtRolePermission = null;

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
            dep = cmb_EmpID.SelectedValue.ToString();
            code = cmbRole.SelectedValue.ToString();
        }

        if (!IsPostBack)
        {
            BindRole();
            load_EmployeeID();
            load_Role();
        }
    }

    private void load_EmployeeID()
    {
        DataSet ds = new DataSet();
        ds.Merge(obj_admin.get_allEmployeeID());

        cmb_EmpID.DataSource = ds.Tables["EMPLOYEE"];
        cmb_EmpID.DataTextField = "VALUE";
        cmb_EmpID.DataValueField = "VALUE";
        cmb_EmpID.DataBind();

        if (!string.IsNullOrEmpty(dep))
            cmb_EmpID.SelectedValue = dep;

        cmb_EmpID_SelectedIndexChanged(null, null);
    }

    private void BindRole()
    {
        try
        {

            dtRolePermission = obj_admin.CreateRolelist();
            gvPagesList.DataSource = dtRolePermission;
            gvPagesList.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void load_Role()
    {
        DataSet ds = new DataSet();
        ds.Merge(obj_admin.get_allRole());

        cmbRole.DataSource = ds.Tables["ROLE"];
        cmbRole.DataTextField = "ROLE_NAME";
        cmbRole.DataValueField = "ID";
        cmbRole.DataBind();
        if (code == "")
        {

        }
        else
            cmbRole.SelectedValue = code;
    }

    protected void gvPages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            //when mouse is over the row, save original color to new attribute, and change it to highlight color
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#0AD4CD'");

            // when mouse leaves the row, change the bg color to its original value  
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

        }
    }
    protected void cmb_EmpID_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Employee_ID = Convert.ToString(cmb_EmpID.SelectedValue);

        if (cmb_EmpID.SelectedIndex > -1)
        {
            DataSet ds = new DataSet();

            ds.Merge(obj_admin.get_allEmployee_Fulltime(cmb_EmpID.SelectedValue));
            foreach (DataRow dr in ds.Tables["EmployeeList"].Rows)
            {
                lblName.Text = dr["STAFF_NAME"].ToString();
                lblDesignation.Text = dr["JOB_DESIGNATION"].ToString();
            }




            if (Convert.ToString(cmb_EmpID.SelectedValue) != "-- Please Select --")
            {
                try
                {
                    DataTable dtGotPermission = null;
                    DataTable dtHR_User = null;
                    int BranchId = 0;

                    if (cmb_EmpID.SelectedIndex < 0)
                    {
                        BindRole();
                    }
                    else
                    {

                        if (gvPagesList.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvPagesList.Rows.Count; i++)
                            {

                                dtGotPermission = new DataTable();
                                dtGotPermission = obj_admin.GetRole_Permission_Controls(Convert.ToInt32(gvPagesList.DataKeys[i].Value), Convert.ToString(cmb_EmpID.SelectedValue));

                                if (dtGotPermission.Rows.Count > 0)
                                {
                                    ((CheckBox)gvPagesList.Rows[i].FindControl("chkBoxPages")).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)gvPagesList.Rows[i].FindControl("chkBoxPages")).Checked = false;
                                }
                                dtGotPermission = null;
                                dtHR_User = null;
                            }

                            //  btnPermission.Text = "Permission Update";
                        }//End of if statement
                    }
                }
                catch (Exception ex)
                {
                    lbl_message.Text = ex.Message.ToString();
                }
            }
            else
            {
                lbl_message.Text = "Please Select the fixed User.";
            }






            //  DataSet Roleds = new DataSet();
            //   ds.Merge(obj_admin.get_Employee_Role(cmb_EmpID.SelectedValue));


        }
    }

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("AD_EMPLOYEE_ROLE");

        ds.Tables["AD_EMPLOYEE_ROLE"].Columns.Add("EMPLOYEE_ID");
        ds.Tables["AD_EMPLOYEE_ROLE"].Columns.Add("ROLE_ID");
        DataRow dr = ds.Tables["AD_EMPLOYEE_ROLE"].NewRow();

        dr["EMPLOYEE_ID"] = "" + Convert.ToString(cmb_EmpID.SelectedValue);
        dr["ROLE_ID"] = "" + Convert.ToString(cmbRole.SelectedValue);

        ds.Tables["AD_EMPLOYEE_ROLE"].Rows.Add(dr);
        string status = "";
        status = new admin_webService().save_Employee_Role(ds);

        if (status == "1")
        {
            lbl_message.Text = "" + new cls_message().getMessage(2);
        }
        else
            lbl_message.Text = "" + new cls_message().getMessage(14);

    }
    protected void btnPermission_Click(object sender, EventArgs e)
    {
        int isChecked = 0;

        if (cmb_EmpID.SelectedIndex < 0)
        {
            //lblMessage.Text = "Sorry! please select HR_User.";
            //lblMessage.ForeColor = Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", " alert('Sorry! Please select User Role.');", true);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please select HR_User.');", true);

            return;
        }

        if (gvPagesList.Rows.Count > 0)
        {
            Save();
        }
    }


    private void Save()
    {
        string TRANID = "";

        DataTable dtHR_User = new DataTable();
        DataTable dtGotPermission = new DataTable();
        int BranchId = 0;


        for (int head = 0; head < gvPagesList.Rows.Count; head++)
        {
            int pageID = Convert.ToInt32(gvPagesList.DataKeys[head].Value);
            int RoleID = Convert.ToInt32(((Label)gvPagesList.Rows[head].FindControl("lblRoleID")).Text);
            string user = Convert.ToString(cmb_EmpID.SelectedValue);


            if (((CheckBox)gvPagesList.Rows[head].FindControl("chkBoxPages")).Checked)
            {
                dtGotPermission = obj_admin.GetRole_Permission_Update(Convert.ToInt32(gvPagesList.DataKeys[head].Value), Convert.ToString(cmb_EmpID.SelectedValue));
                if (dtGotPermission.Rows.Count <= 0)
                {
                    //Add new record
                    DataSet Pageds = new DataSet();
                    Pageds.Tables.Add("AD_EMPLOYEE_ROLE");
                    Pageds.Tables["AD_EMPLOYEE_ROLE"].Columns.Add("EMPLOYEE_ID");
                    Pageds.Tables["AD_EMPLOYEE_ROLE"].Columns.Add("ROLE_ID");
                    Pageds.Tables["AD_EMPLOYEE_ROLE"].Columns.Add("IS_ACTIVE");


                    GridViewRow row = gvPagesList.Rows[head];
                    DataRow drn = Pageds.Tables["AD_EMPLOYEE_ROLE"].NewRow();
                    drn["EMPLOYEE_ID"] = "" + Convert.ToString(cmb_EmpID.SelectedValue);
                    drn["ROLE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblRoleID")).Text);
                    drn["IS_ACTIVE"] = "Y";
                    Pageds.Tables["AD_EMPLOYEE_ROLE"].Rows.Add(drn);
                    TRANID = new admin_webService().RolePermission_Add(Pageds);
                }
                else
                {
                    //Update on existing record
                    dtGotPermission = obj_admin.GetRole_Permission_Update(Convert.ToInt32(gvPagesList.DataKeys[head].Value), Convert.ToString(cmb_EmpID.SelectedValue));
                    if (dtGotPermission.Rows.Count > 0)
                    {
                        DataSet Pageds = new DataSet();
                        Pageds.Tables.Add("AD_EMPLOYEE_ROLE");
                        Pageds.Tables["AD_EMPLOYEE_ROLE"].Columns.Add("EMPLOYEE_ID");
                        Pageds.Tables["AD_EMPLOYEE_ROLE"].Columns.Add("ROLE_ID");
                        Pageds.Tables["AD_EMPLOYEE_ROLE"].Columns.Add("IS_ACTIVE");

                        GridViewRow row = gvPagesList.Rows[head];
                        DataRow drn = Pageds.Tables["AD_EMPLOYEE_ROLE"].NewRow();
                        drn["EMPLOYEE_ID"] = "" + Convert.ToString(cmb_EmpID.SelectedValue);
                        drn["ROLE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblRoleID")).Text);
                        drn["IS_ACTIVE"] = "Y";
                        Pageds.Tables["AD_EMPLOYEE_ROLE"].Rows.Add(drn);
                        TRANID = new admin_webService().RolePermission_Update(Pageds);
                    }
                }

            }
            else
            {
                dtGotPermission = obj_admin.GetRole_Permission_Update(Convert.ToInt32(gvPagesList.DataKeys[head].Value), Convert.ToString(cmb_EmpID.SelectedValue));
                if (dtGotPermission.Rows.Count > 0)
                {
                    DataSet Pageds = new DataSet();
                    Pageds.Tables.Add("AD_EMPLOYEE_ROLE");
                    Pageds.Tables["AD_EMPLOYEE_ROLE"].Columns.Add("EMPLOYEE_ID");
                    Pageds.Tables["AD_EMPLOYEE_ROLE"].Columns.Add("ROLE_ID");
                    Pageds.Tables["AD_EMPLOYEE_ROLE"].Columns.Add("IS_ACTIVE");

                    GridViewRow row = gvPagesList.Rows[head];
                    DataRow drn = Pageds.Tables["AD_EMPLOYEE_ROLE"].NewRow();
                    drn["EMPLOYEE_ID"] = "" + Convert.ToString(cmb_EmpID.SelectedValue);
                    drn["ROLE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblRoleID")).Text);
                    drn["IS_ACTIVE"] = "N";
                    Pageds.Tables["AD_EMPLOYEE_ROLE"].Rows.Add(drn);
                    TRANID = new admin_webService().RolePermission_Update(Pageds);
                }

            }


        }

        if (TRANID == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Role permission has not been Updated.');", true);

            // lblMessage.Text = "Menu permission has not been Updated";
        }
        else
            if (Convert.ToInt32(TRANID) > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Role permission has been given.');", true);
                code = "";               
                load_Role();
                BindRole();
                load_EmployeeID();

            }


    }

}