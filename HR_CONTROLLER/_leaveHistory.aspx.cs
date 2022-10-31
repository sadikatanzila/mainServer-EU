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

public partial class HR_CONTROLLER_leaveHistory : System.Web.UI.Page
{
    string user = "";
    string stf_id = "", staff_id = "";
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

        staff_id = Convert.ToString(Session["Employee_ID"]);

        Load_Data(staff_id);

    }

    protected void Load_Data(string staff_id)
    {
        DataTable ds = new DataTable();
        ds.Merge(new student().get_StaffInfo(staff_id, "StaffInfo"));

       
        if (ds.Rows.Count > 0)
        {
            GridView_LeaveBalance.Visible = true;
            GridView_LeaveBalance.DataSource = ds;
            GridView_LeaveBalance.DataMember = "StaffInfo";
            GridView_LeaveBalance.DataBind();
            Session["Employee_ID"] = null;
        }
        else
        {
            GridView_LeaveBalance.Visible = false;
        }
    }
}