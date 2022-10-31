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

public partial class HR_CONTROLLER_LeaveBalance : System.Web.UI.Page
{
    string user = "";
    string stf_id = "", staff_id = "";
    string degree_id = "";
    admin_webService obj_adminWeb = new admin_webService();

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
           // DateTime now = DateTime.Now;
            string year = DateTime.Now.ToString("yyyy");
            txtYear.Text = year;

           // load_Grid();
           
           // load_LeaveType();
          //  load_Stafftype();
        }
        else
        {
           
        }
    }




    private void load_Grid()
    {
       // string Year = "", semsester = "";
      //  Year = Convert.ToString(txtYear.Text);
      //  semsester = cmb_semester.SelectedValue.ToString();
        string Casual = "", Medical = "", Leave_without_Pay = "", Semester_Brk = "", Duty_Lv = "";
        DataTable ds = new DataTable();

        ds.Merge(new student().get_STAFF_INFO(Convert.ToString(txtYear.Text), "STAFFINFO"));


        grdLeavebalance.DataSource = ds;
        grdLeavebalance.DataMember = "STAFFINFO";
        grdLeavebalance.DataBind();



        foreach (GridViewRow gr in grdLeavebalance.Rows)
        {
            //((TextBox)(gr.FindControl("txtCasual"))).Enabled = false;
        }

    }
    int j = 1;
    protected void grdLeavebalance_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (txtYear.Text != "")
        {
            load_Grid();
        }
        else
        {
            lblmsg.Text = "Please Insert a Fixed Year";
        }
       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtYear.Text != "")
        {
            save();
        }
        else
        {
            lblmsg.Text = "Please Insert a Fixed Year";
        }
    }
    private void save()
    {
        int rowIndex = 0;
        string TRANID = "";
        DataSet ds = new DataSet();
        ds.Tables.Add("HR_LEAVE_BALANCE");
        ds.Tables["HR_LEAVE_BALANCE"].Columns.Add("EMPLOYEE_ID");
        ds.Tables["HR_LEAVE_BALANCE"].Columns.Add("YEAR");
        ds.Tables["HR_LEAVE_BALANCE"].Columns.Add("LEAVE_ID");
        ds.Tables["HR_LEAVE_BALANCE"].Columns.Add("BALANCE");
        ds.Tables["HR_LEAVE_BALANCE"].Columns.Add("CREATED");
        ds.Tables["HR_LEAVE_BALANCE"].Columns.Add("CREATEDBY");

        DateTime today = DateTime.Today;
        string s_today = today.ToString("MM/dd/yyyy");
        today = Convert.ToDateTime(s_today);

        for (int i = 0; i < (grdLeavebalance.Rows.Count); i++)
        {
            if (((CheckBox)grdLeavebalance.Rows[i].FindControl("chkBoxPages")).Checked)
            {

                GridViewRow row = grdLeavebalance.Rows[i];
                DataRow drn = null;
                //   DataRow drc = ds.Tables["HR_LEAVE_BALANCE"].NewRow();
                DataTable dtGotPermission = new DataTable();
                for (int j = 0; j < (grdLeavebalance.Columns.Count); j++)
                {
                    String header = grdLeavebalance.Columns[j].HeaderText;
                    String cellText = row.Cells[j].Text;

                    if (header == "Casual")
                    {
                        drn = ds.Tables["HR_LEAVE_BALANCE"].NewRow();
                        drn["EMPLOYEE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblVALUE")).Text);
                        drn["YEAR"] = "" + txtYear.Text;
                        drn["BALANCE"] = "" + Convert.ToString(((TextBox)row.FindControl("Casual")).Text);
                        drn["LEAVE_ID"] = "" + "1";

                        drn["CREATED"] = "" + DateTime.Today.ToString("dd-MM-yyyy");
                        drn["CREATEDBY"] = "" + Session["ctrl_admin_Id"].ToString();

                        rowIndex++;
                        ds.Tables["HR_LEAVE_BALANCE"].Rows.Add(drn);


                    }
                    else
                        if (header == "Medical")
                        {
                            drn = ds.Tables["HR_LEAVE_BALANCE"].NewRow();
                            drn["EMPLOYEE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblVALUE")).Text);
                            drn["YEAR"] = "" + txtYear.Text;
                            drn["BALANCE"] = "" + Convert.ToString(((TextBox)row.FindControl("Medical")).Text);
                            drn["LEAVE_ID"] = "" + "2";

                            drn["CREATED"] = "" + DateTime.Today.ToString("dd-MM-yyyy");
                            drn["CREATEDBY"] = "" + Session["ctrl_admin_Id"].ToString();

                            rowIndex++;
                            ds.Tables["HR_LEAVE_BALANCE"].Rows.Add(drn);


                        }
                        else
                            if (header == "Earned")
                            {
                                drn = ds.Tables["HR_LEAVE_BALANCE"].NewRow();
                                drn["EMPLOYEE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblVALUE")).Text);
                                drn["YEAR"] = "" + txtYear.Text;
                                drn["BALANCE"] = "" + Convert.ToString(((TextBox)row.FindControl("Earned")).Text);
                                drn["LEAVE_ID"] = "" + "7";

                                drn["CREATED"] = "" + DateTime.Today.ToString("dd-MM-yyyy");
                                drn["CREATEDBY"] = "" + Session["ctrl_admin_Id"].ToString();

                                rowIndex++;
                                ds.Tables["HR_LEAVE_BALANCE"].Rows.Add(drn);


                            }
                            else
                                if (header == "Semester Break")
                                {
                                    drn = ds.Tables["HR_LEAVE_BALANCE"].NewRow();
                                    drn["EMPLOYEE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblVALUE")).Text);
                                    drn["YEAR"] = "" + txtYear.Text;
                                    drn["BALANCE"] = "" + Convert.ToString(((TextBox)row.FindControl("Semester_Brk")).Text);
                                    drn["LEAVE_ID"] = "" + "5";

                                    drn["CREATED"] = "" + DateTime.Today.ToString("dd-MM-yyyy");
                                    drn["CREATEDBY"] = "" + Session["ctrl_admin_Id"].ToString();

                                    rowIndex++;
                                    ds.Tables["HR_LEAVE_BALANCE"].Rows.Add(drn);


                                }
                                else
                                    if (header == "Duty Leave")
                                    {
                                        drn = ds.Tables["HR_LEAVE_BALANCE"].NewRow();
                                        drn["EMPLOYEE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblVALUE")).Text);
                                        drn["YEAR"] = "" + txtYear.Text;
                                        drn["BALANCE"] = "" + Convert.ToString(((TextBox)row.FindControl("Duty_Lv")).Text);
                                        drn["LEAVE_ID"] = "" + "6";

                                        drn["CREATED"] = "" + DateTime.Today.ToString("dd-MM-yyyy");
                                        drn["CREATEDBY"] = "" + Session["ctrl_admin_Id"].ToString();

                                        rowIndex++;
                                        ds.Tables["HR_LEAVE_BALANCE"].Rows.Add(drn);


                                    }
                                    else
                                        if (header == "Leave without Pay")
                                        {
                                            drn = ds.Tables["HR_LEAVE_BALANCE"].NewRow();
                                            drn["EMPLOYEE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblVALUE")).Text);
                                            drn["YEAR"] = "" + txtYear.Text;
                                            drn["BALANCE"] = "" + Convert.ToString(((TextBox)row.FindControl("Leave_without_Pay")).Text);
                                            drn["LEAVE_ID"] = "" + "3";

                                            drn["CREATED"] = "" + DateTime.Today.ToString("dd-MM-yyyy");
                                            drn["CREATEDBY"] = "" + Session["ctrl_admin_Id"].ToString();

                                            rowIndex++;
                                            ds.Tables["HR_LEAVE_BALANCE"].Rows.Add(drn);


                                        }
                                        else
                                            if (header == "Maternity Leave")
                                            {
                                                drn = ds.Tables["HR_LEAVE_BALANCE"].NewRow();
                                                drn["EMPLOYEE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblVALUE")).Text);
                                                drn["YEAR"] = "" + txtYear.Text;
                                                drn["BALANCE"] = "" + Convert.ToString(((TextBox)row.FindControl("Maternity_Lv")).Text);
                                                drn["LEAVE_ID"] = "" + "8";

                                                drn["CREATED"] = "" + DateTime.Today.ToString("dd-MM-yyyy");
                                                drn["CREATEDBY"] = "" + Session["ctrl_admin_Id"].ToString();

                                                rowIndex++;
                                                ds.Tables["HR_LEAVE_BALANCE"].Rows.Add(drn);


                                            }





                }
            }//CheckBox end

        }
        //either insert or update
        TRANID = new admin_webService().LeaveBalance_Add(ds);
        if (Convert.ToInt32(TRANID) > 0)
        {
            lblmsg.Text = "Information Successfully Saved";
            load_Grid();
        }
        // dtGotPermission = obj_adminWeb.GetPermission_Controls_DataTable_Update(Convert.ToString(((Label)row.FindControl("lblVALUE")).Text), drn["LEAVE_ID"]);

    }
    protected void grdLeavebalance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdLeavebalance.PageIndex = e.NewPageIndex;
        save();
        lblmsg.Text = "";
        load_Grid();
    }
    protected void grdLeavebalance_RowEditing(object sender, GridViewEditEventArgs e)
    {
        for (int i = 0; i < (grdLeavebalance.Rows.Count); i++)
        {
            if (((CheckBox)grdLeavebalance.Rows[i].FindControl("chkBoxPages")).Checked)
            {
                TextBox itmCasual = (grdLeavebalance.Rows[e.NewEditIndex].FindControl("Casual") as TextBox);
                itmCasual.Enabled = true;

                TextBox itmMedical = (grdLeavebalance.Rows[e.NewEditIndex].FindControl("Medical") as TextBox);
                itmMedical.Enabled = true;


                TextBox itmEarned = (grdLeavebalance.Rows[e.NewEditIndex].FindControl("Earned") as TextBox);
                itmEarned.Enabled = true;

                TextBox itmlpay = (grdLeavebalance.Rows[e.NewEditIndex].FindControl("Leave_without_Pay") as TextBox);
                itmlpay.Enabled = true;

                TextBox itmSbrk = (grdLeavebalance.Rows[e.NewEditIndex].FindControl("Semester_Brk") as TextBox);
                itmSbrk.Enabled = true;
            }
        }
    }
}