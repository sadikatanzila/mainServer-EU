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
using System.Globalization;
using System.Threading;

public partial class admin_add_staff : System.Web.UI.Page
{
    string user = "";
    string stf_id = "";
    string dep = "";
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

    
        btn_save.Attributes.Add("onClick", " return check_Data(); ");

        lbl_message.Text = "";

        if (!String.IsNullOrEmpty(Request.QueryString["ids"]))
        {
            stf_id = Request.QueryString["ids"].ToString();
        }

        if (!IsPostBack)
            load_teacher_info(stf_id);
    }


    private void load_teacher_info(string staffId)
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_staff_info(staffId));

        foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
        {
            txt_email.Text = dr["E_MAIL"].ToString();
            txt_mobile.Text = dr["MOBILE"].ToString();
            txt_name.Text = dr["STAFF_NAME"].ToString();
            txt_password.Text = dr["PASSWORD"].ToString();
            txt_passwordConfirm.Text = dr["PASSWORD"].ToString();
            txt_permanentAddress.Text = dr["PER_ADDRESS"].ToString();
            txt_phone.Text = dr["PHONE_NUMBER"].ToString();
            txt_presendAddress.Text = dr["P_ADDRESS"].ToString();
            txt_ID.Text = dr["VALUE"].ToString();
            txt_ID.ReadOnly = true;


            if (dr["JOIN_DATE"].ToString() != "")
            {
                DateTime OPENING_DATE = Convert.ToDateTime(dr["JOIN_DATE"].ToString());
                txt_student_opening.Text = OPENING_DATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }

            if (dr["CONFIRMATION_DATE"].ToString() != "")
            {
                DateTime Confirmation = Convert.ToDateTime(dr["CONFIRMATION_DATE"].ToString());
                txt_student_Confirmation.Text = Confirmation.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }

            cmb_jb_category.SelectedValue = dr["JOB_CATEGORY"].ToString();
            cmb_teacher_department.SelectedValue = dr["DEPARTMENT"].ToString();
            cmb_job_type.SelectedValue = dr["JOB_TYPE"].ToString();
            cmb_staff_designation.SelectedValue = dr["JOB_DESIGNATION"].ToString();
        }
    }


    private void clear()
    {
        txt_ID.Text = "";
        txt_email.Text = "";
        txt_mobile.Text = "";
        txt_name.Text = "";
        txt_password.Text = "";
        txt_passwordConfirm.Text = "";
        txt_permanentAddress.Text = "";
        txt_phone.Text = "";
        txt_presendAddress.Text = "";
        txt_student_opening.Text = "";
        txt_student_Confirmation.Text = "";


    }

    private static string ddlText = "-- Please Select --";

 /*   private void load_allDepartment()
    {
        CommonBinder.AdmissionType_LoadForDll(cmb_teacher_department);
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allCollege());

        cmb_teacher_department.DataSource = ds.Tables["COLLEGE"];
        cmb_teacher_department.DataTextField = "COLLEGENAME";
        cmb_teacher_department.DataValueField = "COLLEGECODE";
        cmb_teacher_department.DataBind();

        cmb_teacher_department.Items.Insert(0, new ListItem(ddlText, "0"));

        if (!string.IsNullOrEmpty(dep))
            cmb_teacher_department.SelectedValue = dep;

      
    }
   */ 

    protected void btn_submit_Click(object sender, EventArgs e)
    {

    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (txt_ID.Text != "")
        {
            save_staffInfo();
        }
        else
        {
            lbl_message.Text = "Please add Employee ID";
        }
    }

    private void save_staffInfo()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("WEB_TEACHER_STAFF");

        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("STAFF_ID");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("STAFF_NAME");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("P_ADDRESS");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("PER_ADDRESS");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("PHONE_NUMBER");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("MOBILE");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("E_MAIL");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("DEPARTMENT");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("JOB_TYPE");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("JOB_CATEGORY");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("JOB_DESIGNATION");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("JOIN_DATE");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("CONFIRMATION_DATE");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("LOGIN_NAME");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("PASSWORD");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("STAFF_CTRL");
        ds.Tables["WEB_TEACHER_STAFF"].Columns.Add("VALUE");
        

        DataRow dr = ds.Tables["WEB_TEACHER_STAFF"].NewRow();
        if (!string.IsNullOrEmpty(stf_id))
            dr["STAFF_ID"] = "" + stf_id;
        else
            dr["STAFF_ID"] = "test";

        dr["STAFF_NAME"] = "" + txt_name.Text;
        dr["P_ADDRESS"] = "" + txt_presendAddress.Text;
        dr["PER_ADDRESS"] = "" + txt_permanentAddress.Text;
        dr["PHONE_NUMBER"] = "" + txt_phone.Text;
        dr["MOBILE"] = "" + txt_mobile.Text;
        dr["E_MAIL"] = "" + txt_email.Text;
        //= "" + cmb_teacher_department.SelectedValue.ToString();
        if (cmb_teacher_department.SelectedValue.ToString() != "0")
        {
            dr["DEPARTMENT"] = "" + cmb_teacher_department.SelectedValue.ToString();
        }
        else
        {
            dr["DEPARTMENT"] = "";// + cmb_teacher_department.SelectedValue.ToString();
        }


        dr["JOB_TYPE"] = "" + cmb_job_type.SelectedValue.ToString();
        dr["JOB_CATEGORY"] = "Staff";
        dr["JOB_DESIGNATION"] = "" + cmb_staff_designation.SelectedValue.ToString();

        if (Convert.ToString(txt_student_opening.Text) != "")
        {
            dr["JOIN_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));

        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }

        if (Convert.ToString(txt_student_Confirmation.Text) != "")
        {
            dr["CONFIRMATION_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_Confirmation.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));

        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }
        // dr["JOIN_DATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_student_opening"].ToString()));

        dr["LOGIN_NAME"] = "";
        dr["PASSWORD"] = "" + txt_password.Text;
        dr["STAFF_CTRL"] = "1";
        dr["VALUE"] = "" + txt_ID.Text;

        ds.Tables["WEB_TEACHER_STAFF"].Rows.Add(dr);

        string status = "";

        if (!string.IsNullOrEmpty(stf_id))
            status = new admin_webService().update_staff_info(ds);
        else
            status = new admin_webService().save_Staff_info(ds);


        if (status == "1")
        {// status = new admin_webService().save_Staff_Role(ds);           
            clear();
            lbl_message.Text = "" + new cls_message().getMessage(2);
        }
        else
            lbl_message.Text = "" + new cls_message().getMessage(14);

    }
}
