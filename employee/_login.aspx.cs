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

public partial class staffs_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Session["ctrl_admin_Id"] = "";
            Session["ctrlId"] = "";
            Session["user"] = "";


            Session.Clear();
            Session.Abandon();
            Session.Contents.Clear();
        }
        else
        {

        }


        lbl_message.Text = "";
        Session["user"] = "";
        Session["ctrlId"] = "";


        btn_submit.Attributes.Add("onClick", " return check_valid();");
        txt_id.Focus();

    }


    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (new staff_webService().check_employee_login(txt_id.Value.ToString().Trim(), txt_pass.Value.ToString().Trim()))
        {
            DataSet ds = new DataSet();
            ds.Merge(new staff_webService().get_user_field(txt_id.Value.ToString().Trim()));

            if (ds.Tables["EmployeeList"].Rows.Count > 0)
            {
                pnlPassword.Visible = false;

                cmb_employee.Visible = true;
                Label1.Visible = true;
                btn_submitN.Visible = true;

                cmb_employee.DataSource = ds.Tables["EmployeeList"];
                cmb_employee.DataTextField = "ROLE_NAME";
                cmb_employee.DataValueField = "ROLE_ID";
                cmb_employee.DataBind();

            }
            else
            {
                if ((new staff_webService().check_staff_login(txt_id.Value.ToString().Trim(), txt_pass.Value.ToString().Trim())))
                {
                    Session["user"] = txt_id.Value.ToString();
                    Response.Redirect("../staffs/_home.aspx");
                }
            }

            //  Session["user"] = txt_id.Value.ToString().Trim();
            //  Response.Redirect("_home.aspx");



        }
        else
        {
            lbl_message.Text = "Invalid user/password!";
            txt_pass.Focus();
        }
    }
    protected void btn_submitN_Click(object sender, EventArgs e)
    {
       /* if ((cmb_employee.SelectedValue == "1") || (cmb_employee.SelectedValue == "5") ||
            (cmb_employee.SelectedValue == "6") || (cmb_employee.SelectedValue == "7"))
        {

            DataSet ds = new DataSet();
            ds.Merge(new staff_webService().get_HRT_fieldStaffID(txt_id.Value.ToString().Trim()));

            if (ds.Tables["StaffList"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["StaffList"].Rows) //courses taken (pre)
                {
                    Session["ctrl_admin_Id"] = dr["VALUE"].ToString();
                    Session["DEPTCODE"] = dr["DEPARTMENT"].ToString();
                    Session["ROLE_ID"] = cmb_employee.SelectedValue.ToString();
                }
            }

            //Session["user"] = txt_id.Value.ToString().Trim();
            Response.Redirect("_viewAttendance.aspx");
        }
        else*/
            if (cmb_employee.SelectedValue == "8")
            {

                DataSet ds = new DataSet();
                ds.Merge(new staff_webService().get_HRT_fieldID(txt_id.Value.ToString().Trim()));

                if (ds.Tables["StaffList"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["StaffList"].Rows) //courses taken (pre)
                    {
                        Session["user"] = dr["STAFF_ID"].ToString();
                    }
                }
               

                //Session["user"] = txt_id.Value.ToString().Trim();
                Response.Redirect("../staffs/_home.aspx");
            }
           
           /* else
                if (cmb_employee.SelectedValue == "9")
                {

                    DataSet ds = new DataSet();
                    ds.Merge(new staff_webService().get_HRT_fieldStaffID(txt_id.Value.ToString().Trim()));

                    if (ds.Tables["StaffList"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["StaffList"].Rows) //courses taken (pre)
                        {
                            Session["ctrl_admin_Id"] = dr["VALUE"].ToString();
                            Session["DEPTCODE"] = dr["DEPARTMENT"].ToString();
                            Session["ROLE_ID"] = cmb_employee.SelectedValue.ToString();
                        }
                    }

                    //Session["user"] = txt_id.Value.ToString().Trim();
                    Response.Redirect("_viewAttendance.aspx");
                }
                else
                    if (cmb_employee.SelectedValue == "3")
                    {

                        DataSet ds = new DataSet();
                        ds.Merge(new staff_webService().get_HRT_fieldStaffID(txt_id.Value.ToString().Trim()));

                        if (ds.Tables["StaffList"].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables["StaffList"].Rows) //courses taken (pre)
                            {
                                Session["ctrl_admin_Id"] = dr["VALUE"].ToString();
                                Session["DEPTCODE"] = dr["DEPARTMENT"].ToString();
                                Session["ROLE_ID"] = cmb_employee.SelectedValue.ToString();
                            }
                        }

                        //Session["user"] = txt_id.Value.ToString().Trim();
                        Response.Redirect("_viewAttendance.aspx");
                    }*/
                    else
                    {
                        DataSet ds = new DataSet();
                        ds.Merge(new staff_webService().get_HRT_fieldStaffID(txt_id.Value.ToString().Trim()));

                        if (ds.Tables["StaffList"].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables["StaffList"].Rows) //courses taken (pre)
                            {
                                Session["ctrl_admin_Id"] = dr["VALUE"].ToString();
                                Session["DEPTCODE"] = dr["DEPARTMENT"].ToString();
                                Session["ROLE_ID"] = cmb_employee.SelectedValue.ToString();
                            }
                        }

                        //Session["user"] = txt_id.Value.ToString().Trim();
                       // Response.Redirect("_viewAttendance.aspx");
                        Response.Redirect("../employee/_home.aspx");
                    }
    }
}
