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
using System.Data;
using System.Configuration;
using System.Data.OracleClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;

public partial class HR_CONTROLLER_LeaveEntry : System.Web.UI.Page
{
    string user = "";
    string stf_id = "", staff_id = "";
    string degree_id = "";
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

        if (!IsPostBack)
        {
            //   load_employee();
            load_LeaveType();
            load_Staff();
        }
        else
        {
            //  Session["Employee_ID"] = ddlEmployee.SelectedValue.ToString();
            degree_id = ddlLeave.SelectedValue.ToString();
        }
    }

    private void load_employee()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_Staff());

        DataRow dr = ds.Tables["stafflist"].NewRow();
        dr["STAFF_NAME"] = "None";
        dr["VALUE"] = "0";
        ds.Tables["stafflist"].Rows.Add(dr);

        ddlHandledEmpID.DataSource = ds.Tables["stafflist"];
        ddlHandledEmpID.DataTextField = "STAFF_NAME";
        ddlHandledEmpID.DataValueField = "VALUE";
        ddlHandledEmpID.DataBind();


        if (staff_id == "")
        {
            ddlHandledEmpID.SelectedValue = "Select";
        }
        else
            ddlHandledEmpID.SelectedValue = staff_id;

    }

    private void load_Staff()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_Staff());

        DataRow dr = ds.Tables["stafflist"].NewRow();
        dr["NAME"] = "Select";
        dr["VALUE"] = "0";
        ds.Tables["stafflist"].Rows.Add(dr);

        ddlEmployee.DataSource = ds.Tables["stafflist"];
        ddlEmployee.DataTextField = "NAME";
        ddlEmployee.DataValueField = "VALUE";
        ddlEmployee.DataBind();


        if (staff_id == "")
        {
            ddlHandledEmpID.SelectedValue = "Select";
        }
        else
            ddlHandledEmpID.SelectedValue = staff_id;

    }

    private void load_LeaveType()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_LeaveType());

        DataRow dr = ds.Tables["leavelist"].NewRow();
        //   dr["NAME"] = "Select";
        // dr["LEAVE_ID"] = "Select";
        //   ds.Tables["leavelist"].Rows.Add(dr);

        ddlLeave.DataSource = ds.Tables["leavelist"];
        ddlLeave.DataTextField = "NAME";
        ddlLeave.DataValueField = "LEAVE_ID";
        ddlLeave.DataBind();


        if (degree_id == "")
        {
            ddlLeave.SelectedValue = "Select";
        }
        else
            ddlLeave.SelectedValue = degree_id;

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        save_LeaveInfo();
    }

    private void save_LeaveInfo()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("HR_STAFF_LEAVE_INFO");

        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("STAFF_ID");
        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("LEAVE_ID");
        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("FROM_DATE");
        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("TO_DATE");
        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("CONTACT_ADDRESS");
        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("LEAVE_CONTACT");
        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("LEAVE_REASON");
        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("HANDLED_ON");
        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("APPROVED_BY");
        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("APPROVED_ON");
        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("CREATED");
        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("CREATED_BY");
        ds.Tables["HR_STAFF_LEAVE_INFO"].Columns.Add("IS_ACTIVE");



        DataRow dr = ds.Tables["HR_STAFF_LEAVE_INFO"].NewRow();
        if (!string.IsNullOrEmpty(stf_id))
            dr["STAFF_ID"] = "" + stf_id;
        else
            dr["STAFF_ID"] = "" + Convert.ToString(ddlEmployee.SelectedValue);

        dr["LEAVE_ID"] = "" + ddlLeave.SelectedValue.ToString();
        dr["LEAVE_REASON"] = "" + txtReason.Text;
        dr["CONTACT_ADDRESS"] = "" + txtAddress.Text;

        dr["FROM_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txtfromDate.Text, "dd/MMM/yyyy", CultureInfo.CurrentCulture));
        dr["TO_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txtToDate.Text, "dd/MMM/yyyy", CultureInfo.CurrentCulture));

        //  dr["CONTACT_ADDRESS"] = "" + txtAddress.Text;
        dr["LEAVE_CONTACT"] = "" + txtLvContact.Text;
        dr["HANDLED_ON"] = "" + ddlHandledEmpID.SelectedValue.ToString();

        dr["APPROVED_BY"] = "";
        dr["APPROVED_ON"] = "";
        dr["CREATED"] = Convert.ToString(Convert.ToDateTime(DateTime.Now));
        dr["CREATED_BY"] = "200029";
        dr["IS_ACTIVE"] = "1";

        ds.Tables["HR_STAFF_LEAVE_INFO"].Rows.Add(dr);

        string status = "";

        if (!string.IsNullOrEmpty(stf_id))
            status = new admin_webService().update_staff_info(ds);
        else
            status = new admin_webService().save_Staff_leave_info(ds);
        //   status = new admin_webService().save_Staff_info(ds);


        if (status != "")
        {
            clear();
            lbl_message.Text = "" + new cls_message().getMessage(2);
            lbl_TranID.Text = status;
        }
        else
            lbl_message.Text = "" + new cls_message().getMessage(14);

    }

    private void clear()
    {
        //  txtEmployeeID.Text = "";
        //  txtEmployeeName.Text = "";
        txtDept.Text = "";
        txtDesignation.Text = "";
        //  ddlLeave.SelectedValue = "";
        txtReason.Text = "";
        txtfromDate.Text = "";
        txtToDate.Text = "";

        txtAddress.Text = "";
        txtLvContact.Text = "";
        //  txtHandledEmpID.Text = "";
        //  lblEmpHandledName.Text = "";
        // lblBalance.Text = "";
        //lblStatus.Text = "";

        // txtAppBy.Text = "";

        //   txtAppDate.Text = "";
    }

    int i = 1, j = 1; decimal dPageTotal = 0, dMaleTotal = 0, dFemaleTotal = 0;
    protected void GridView_LeaveBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerialdtl = (Label)e.Row.FindControl("lblSerialdtl");
            lblSerialdtl.Text = i.ToString();
            i++;

        }




    }

    protected void grdLvBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = j.ToString();
            j++;

        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        lblLvdtl.Visible = true;
        lblLvSum.Visible = true;

        string year = DateTime.Now.ToString("yyyy");

        DataTable ds = new DataTable();
        ds.Merge(new student().get_StaffLeaveInfo(Convert.ToString(ddlEmployee.SelectedValue), year, "StaffInfo"));

        grdLvBalance.DataSource = ds;
        grdLvBalance.DataMember = "StaffInfo";
        grdLvBalance.DataBind();


        DataTable ds1 = new DataTable();
        ds1.Merge(new student().get_StaffLeaveInfodetails(Convert.ToString(ddlEmployee.SelectedValue), "StaffInfodtl"));

        GridView_LeaveBalance.DataSource = ds1;
        GridView_LeaveBalance.DataMember = "StaffInfodtl";
        GridView_LeaveBalance.DataBind();
    }
    protected void ddlLeave_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEmployee_SelectedIndexChanged(sender, e);



    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable ds = new DataTable();

        string year = DateTime.Now.ToString("yyyy");

        ds.Merge(new student().get_StaffInfo(Convert.ToString(ddlEmployee.SelectedValue), Convert.ToString(ddlLeave.SelectedValue), year, "StaffInfo"));
        decimal total = 0;
        if (ds.Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Rows)
            {
                txtEmployeeID.Text = Convert.ToString(dr["VALUE"]);
                txtDesignation.Text = Convert.ToString(dr["JOB_DESIGNATION"]);
                txtEmployeeName.Text = Convert.ToString(dr["STAFF_NAME"]);

                if (Convert.ToString(dr["REMAINING_BALANCE"]) != "")
                {
                    //total = Convert.ToDecimal(dr["TAKEN"]) + Convert.ToDecimal(dr["REMAINING_BALANCE"]);
                    lblRemaining.Text = "Due Leave :" + Convert.ToString(dr["REMAINING_BALANCE"]);
                }
                else
                {
                    total = 0;
                    lblRemaining.Text = "Due Leave : 0";
                }


                lblTotal.Text = "Total :" + Convert.ToString(dr["TOTAL_BALANCE"]);
                lblTaken.Text = "Taken :" + Convert.ToString(dr["TAKEN"]);

            }
        }
        else
        {
            txtEmployeeID.Text = "";
            txtDesignation.Text = "";
            txtEmployeeName.Text = "";

            lblTotal.Text = "";
            lblTaken.Text = "";
            lblRemaining.Text = "";
        }

        lbl_message.Text = "";
        lbl_TranID.Text = "";
    }
    protected void txtUni_TextChanged(object sender, EventArgs e)
        {
        decimal total = 0;
        DataTable ds = new DataTable();
        string year = DateTime.Now.ToString("yyyy");
        ds.Merge(new student().get_StaffInfo(txtUni.Text, Convert.ToString(ddlLeave.SelectedValue), year, "StaffInfo"));
        if (ds.Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Rows)
            {
                txtEmployeeID.Text = Convert.ToString(dr["VALUE"]);
                txtDesignation.Text = Convert.ToString(dr["JOB_DESIGNATION"]);
                txtEmployeeName.Text = Convert.ToString(dr["STAFF_NAME"]);

                if (Convert.ToString(dr["REMAINING_BALANCE"]) != "")
                {
                    //total = Convert.ToDecimal(dr["TAKEN"]) + Convert.ToDecimal(dr["REMAINING_BALANCE"]);
                    lblRemaining.Text = "Due Leave :" + Convert.ToString(dr["REMAINING_BALANCE"]);
                }
                else
                {
                    total = 0;
                    lblRemaining.Text = "Due Leave : 0";
                }


                lblTotal.Text = "Total :" + Convert.ToString(dr["TOTAL_BALANCE"]);
                lblTaken.Text = "Taken :" + Convert.ToString(dr["TAKEN"]);

            }
        }
        else
        {
            txtEmployeeID.Text = "";
            txtDesignation.Text = "";
            txtEmployeeName.Text = "";

            lblTotal.Text = "";
            lblTaken.Text = "";
            lblRemaining.Text = "";
        }

        lbl_message.Text = "";
        lbl_TranID.Text = "";
    }


    protected void GridView_LeaveBalance_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string SerialNo = Convert.ToString(GridView_LeaveBalance.DataKeys[e.RowIndex].Value);
        string status = "";
        string datetime = Convert.ToString(Convert.ToDateTime(DateTime.Now));
        if (!string.IsNullOrEmpty(SerialNo))
            status = new admin_webService().delete_staff_Lvinfo(user, SerialNo, datetime);

        if (status != "")
        {
            lbl_message.Text = "Data is being Deleted";
            btnView_Click(sender, e);
        }



    }
}