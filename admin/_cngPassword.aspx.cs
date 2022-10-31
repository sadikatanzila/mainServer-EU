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

public partial class admin_cngPassword : System.Web.UI.Page
{
    string user = "";
    string dep = "", code = "";
    admin_webService obj_admin = new admin_webService();

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                user = Session["ctrl_admin_Id"].ToString();
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

        btn_submit.Attributes.Add("onClick", " return save_check();");
        if (IsPostBack)
        {
            dep = cmb_EmpID.SelectedValue.ToString();
          //  code = cmbRole.SelectedValue.ToString();
        }

        if (!IsPostBack)
        {           
            load_EmployeeID();
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


    protected void btn_submit_Click(object sender, EventArgs e)
    {

        if (txt_confirmPass.Text == "" || txt_newPass.Text == "")
        {
            lbl_message.Text = "Please enter new password.";
        }
        else
        {
            if (txt_confirmPass.Text != txt_newPass.Text )
            {
                lbl_message.Text = "Confirm password is not correct.";
            }
            else
                if (new admin_webService().change_staff_passwordReset(Convert.ToString(cmb_EmpID.SelectedValue), txt_newPass.Text.Trim()) == "1")
                {
                    lbl_message.Text = "Password changes successfully";
                    Clear();
                }
        }
    }


    private void Clear()
    {
        txt_newPass.Text = "";
        txt_confirmPass.Text = "";

    }


    protected void cmb_EmpID_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Employee_ID = Convert.ToString(cmb_EmpID.SelectedValue);

        if (cmb_EmpID.SelectedIndex > -1)
        {
            DataSet ds = new DataSet();

            ds.Merge(obj_admin.get_allEmployee_Fulltime(Employee_ID));
            foreach (DataRow dr in ds.Tables["EmployeeList"].Rows)
            {
                lblName.Text = dr["STAFF_NAME"].ToString();
                lblPrevPassword.Text = dr["PASSWORD"].ToString();
            }
        }
        else
        {
            lbl_message.Text = "Please Select the fixed User.";
        }
    }
}