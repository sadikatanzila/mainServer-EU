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

public partial class admin_Inquiry : System.Web.UI.Page
{
    string user = "";
    string stf_id = "";
    string dep = "";
    string Update = "";
    string ntcId = "";


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

        if (Request.QueryString["nid"] != null)
        {
            ntcId = Request.QueryString["nid"].ToString();

            if (!IsPostBack)
                load_StudentInfo();
        }

        if (!IsPostBack)
        {
          
            load_Faculty();// new load from C_PROGINDEPT
            load_District();
            load_Referrences();
            load_Institutions();
        }
    }

    private void load_StudentInfo()
    {
        
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_InquiryStudent_info(ntcId));
        if (ds.Tables["C_INQUIRY_ADMISSION"].Rows.Count > 0)
        {
            stf_id = ntcId;
            lblUpdate.Text = "1";
            foreach (DataRow dr in ds.Tables["C_INQUIRY_ADMISSION"].Rows)
            {

                txt_Contact.Text = dr["CONTACT"].ToString();
                txt_name.Text = dr["STUDENT_NAME"].ToString();
                cmb_Faculty.SelectedValue = dr["INTERESTED_PROGRAM"].ToString();

                txt_LivingArea.Text = dr["LIVING_AREA"].ToString();
                ddl_District.SelectedValue = dr["DISTRICT_ID"].ToString();

                if (ddl_Institution.SelectedValue != "0")
                    ddl_Institution.SelectedValue = dr["COLLEGE"].ToString();

                // txt_College.Text
                txtUni_others.Text = dr["OTHER_INSTITUTION"].ToString();
                txtNote.Text = dr["NOTE"].ToString();

                txt_Year.Text = dr["YEAR"].ToString();
                cmb_semester.SelectedValue = dr["SEMESTER"].ToString();

                ddlSubject.SelectedValue = dr["REFERRED_BY"].ToString();
                txtRef_Name.Text = dr["REFERRED_NAME"].ToString();
                txtRef_Contact.Text = dr["REFERRED_CONTACT"].ToString();

                //txt_Contact.ReadOnly = true;

            }
        }
        else
        {
            stf_id = "";
            txt_name.Text = "";
            txtRef_Contact.Text = "";
            txt_LivingArea.Text = "";
            txtRef_Name.Text = "";
            txtNote.Text = "";
            txt_Year.Text = "";
        }

    }


    private void load_Referrences()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allRef());

        DataRow dr = ds.Tables["REFFERENCE_list"].NewRow();
        dr["REFFERENCE"] = "Select";
        dr["C_REFFERENCE_ID"] = "Select";
        ds.Tables["REFFERENCE_list"].Rows.Add(dr);

        ddlSubject.DataSource = ds.Tables["REFFERENCE_list"];
        ddlSubject.DataTextField = "REFFERENCE";
        ddlSubject.DataValueField = "C_REFFERENCE_ID";
        ddlSubject.DataBind();
    }

    private void load_Institutions()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allInstitutions());

        DataRow dr = ds.Tables["Institutions_list"].NewRow();
        dr["INSTITUTE_NAME"] = "Select";
        dr["C_INSTITUTE_ID"] = "0";
        ds.Tables["Institutions_list"].Rows.Add(dr);

        ddl_Institution.DataSource = ds.Tables["Institutions_list"];
        ddl_Institution.DataTextField = "INSTITUTE_NAME";
        ddl_Institution.DataValueField = "C_INSTITUTE_ID";
        ddl_Institution.DataBind();
    }

    private void load_Faculty()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allDepartment_New());// new load from C_PROGINDEPT

        DataRow dr = ds.Tables["departmentList"].NewRow();
        dr["NAME"] = "Select";
        dr["C_PROGINDEPT_ID"] = "0";
        ds.Tables["departmentList"].Rows.Add(dr);

        cmb_Faculty.DataSource = ds.Tables["departmentList"];
        cmb_Faculty.DataTextField = "NAME";
        cmb_Faculty.DataValueField = "C_PROGINDEPT_ID";
        cmb_Faculty.DataBind();
    }


    private void load_District()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_allDistrict());

        DataRow dr = ds.Tables["District_list"].NewRow();
        dr["DISTRICT"] = "Select";
        dr["C_DISTRICT_ID"] = "0";
        ds.Tables["District_list"].Rows.Add(dr);

        ddl_District.DataSource = ds.Tables["District_list"];
        ddl_District.DataTextField = "DISTRICT";
        ddl_District.DataValueField = "C_DISTRICT_ID";
        ddl_District.DataBind();
    }

    private void load_Student_info(string Contact)
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_InquiryStudent_info(Contact));
        if (ds.Tables["C_INQUIRY_ADMISSION"].Rows.Count > 0)
        {
            stf_id = Contact;
            lblUpdate.Text = "1" ;
            foreach (DataRow dr in ds.Tables["C_INQUIRY_ADMISSION"].Rows)
            {

                txt_Contact.Text = dr["CONTACT"].ToString();
                txt_name.Text = dr["STUDENT_NAME"].ToString();
                cmb_Faculty.SelectedValue = dr["INTERESTED_PROGRAM"].ToString();
                
                txt_LivingArea.Text = dr["LIVING_AREA"].ToString();
                ddl_District.SelectedValue = dr["DISTRICT_ID"].ToString();

                if(ddl_Institution.SelectedValue != "0")
                ddl_Institution.SelectedValue = dr["COLLEGE"].ToString();
                
                // txt_College.Text
               // txt_Uni.Text = dr["UNIVERSITY"].ToString();
                txtNote.Text = dr["NOTE"].ToString();

                txt_Year.Text = dr["YEAR"].ToString();
                cmb_semester.SelectedValue = dr["SEMESTER"].ToString();

                ddlSubject.SelectedValue = dr["REFERRED_BY"].ToString();
                txtRef_Name.Text = dr["REFERRED_NAME"].ToString();
                txtRef_Contact.Text = dr["REFERRED_CONTACT"].ToString();

                //txt_Contact.ReadOnly = true;

            }
        }
        else
        {
            stf_id = "";
            txt_name.Text = "";
            txtRef_Contact.Text = "";
            txt_LivingArea.Text = "";
            txtRef_Name.Text = "";
            txtNote.Text = "";
            txt_Year.Text = "";
        }
    }


    private void clear()
    {
        txt_Contact.Text = "";
        txt_name.Text = "";
        txtRef_Contact.Text = "";
        txt_LivingArea.Text = "";
        txtRef_Name.Text = "";
        txtNote.Text = "";
        txt_Year.Text = "";
        ntcId = "";
    }

    private static string ddlText = "-- Please Select --";

 

    
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (txt_Contact.Text != "")
        {
            DataSet ds = new DataSet();
            ds.Merge(new admin_webService().get_InquiryStudent_info(txt_Contact.Text));
            if (ds.Tables["C_INQUIRY_ADMISSION"].Rows.Count > 0)
            {
                if (ntcId != "")
                {
                    save_staffInfo();
                }
                else
                    lbl_message.Text = "This Phone Number is already beensaved, Please Update your Information";
                
            }
            else
            {
                save_staffInfo();
            }
           
        }
        else
        {
            lbl_message.Text = "Please add Student Phone Number";
        }

        
       
    }

    private void save_staffInfo()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("C_INQUIRY_ADMISSION");

        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("CONTACT");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("STUDENT_NAME");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("BIRTH_DATE");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("BIRTH_PLACE");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("COMING_DATE");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("INTERESTED_PROGRAM");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("LIVING_AREA");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("DISTRICT_ID");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("COLLEGE");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("UNIVERSITY");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("REFERRED_BY");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("NOTE");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("INSERTED_BY");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("INSERTED_DATE");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("YEAR");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("SEMESTER");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("UPDATED_BY");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("UPDATED_DATE");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("REFERRED_NAME");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("REFERRED_CONTACT");
        ds.Tables["C_INQUIRY_ADMISSION"].Columns.Add("OTHER_INSTITUTION");

        DataRow dr = ds.Tables["C_INQUIRY_ADMISSION"].NewRow();
        
        
       /* if (!string.IsNullOrEmpty(stf_id))
            dr["CONTACT"] = "" + stf_id;
        else
            dr["CONTACT"] = "test";*/


        if (txt_Contact.Text != "" && txt_name.Text != "" && txt_Year.Text != "" && cmb_Faculty.SelectedValue.ToString() != "Select"
            && ddl_District.SelectedValue.ToString() != "Select" && ddl_Institution.SelectedValue.ToString() != "Select")
        {
            dr["CONTACT"] = "" + txt_Contact.Text;
            dr["STUDENT_NAME"] = "" + txt_name.Text;
            dr["YEAR"] = "" + txt_Year.Text;
            dr["SEMESTER"] = "" + cmb_semester.SelectedValue.ToString() ;

          /*  if (Convert.ToString(txt_BirthDate.Text) != "")
            {
                dr["BIRTH_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_BirthDate.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));
            }
            else
            {
                lbl_message.Text = "Please enter valid  date";
               // break; 
            }


           // dr["BIRTH_PLACE"] = "" + txt_BirthPlace.Text;

            if (Convert.ToString(txt_BirthDate.Text) != "")
            {
                dr["COMING_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_VisitingDate.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));               
            }
            else
            {
                lbl_message.Text = "Please enter valid  date";
            }
            */
            dr["COMING_DATE"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));

           //--------------multi select


            dr["INTERESTED_PROGRAM"] = "" + cmb_Faculty.SelectedValue.ToString();



            dr["LIVING_AREA"] = "" + txt_LivingArea.Text;
            if (Convert.ToInt32(ddl_District.SelectedValue) > 0)
                dr["DISTRICT_ID"] = "" + ddl_District.SelectedValue.ToString();
            else
                dr["DISTRICT_ID"] = "";


            dr["COLLEGE"] = "" + ddl_Institution.SelectedValue.ToString();
            dr["OTHER_INSTITUTION"] = "" + txtUni_others.Text;
            dr["REFERRED_BY"] = "" + ddlSubject.SelectedValue.ToString();
            dr["NOTE"] = "" + txtNote.Text;
            dr["REFERRED_NAME"] = "" + txtRef_Name.Text;
            dr["REFERRED_CONTACT"] = "" + txtRef_Contact.Text;
            

            ds.Tables["C_INQUIRY_ADMISSION"].Rows.Add(dr);

            string status = "";

            if (lblUpdate.Text == "1" || ntcId != "")
            {//(!string.IsNullOrEmpty(stf_id))
                dr["UPDATED_BY"] = Convert.ToInt32(Session["ctrl_admin_Id"]);
                dr["UPDATED_DATE"] = Convert.ToString(Convert.ToDateTime(DateTime.Now));
                status = new admin_webService().update_InquiryStudent_info(ds);
            }
            else
            {
                dr["INSERTED_BY"] = Convert.ToInt32(Session["ctrl_admin_Id"]);
                dr["INSERTED_DATE"] = Convert.ToString(Convert.ToDateTime(DateTime.Now));
                status = new admin_webService().save_InquiryStudent_info(ds);
            }


            if (status == "1")
            {// status = new admin_webService().save_Staff_Role(ds);           
               
                lbl_message.Text = "" + new cls_message().getMessage(2);
            }
            else
                lbl_message.Text = "" + new cls_message().getMessage(14);

            clear();
        }
        else
        {
            lbl_message.Text = "Please give Student Information Fully";

        }

        

    }
    protected void txt_Contact_TextChanged(object sender, EventArgs e)
    {
        
        load_Student_info(txt_Contact.Text);
    }



    /*

    private int Take
    {
        get
        {
            return (int)ViewState["Take"];
        }
        set
        {
            ViewState["Take"] = value;
        }
    }

    private int Skip
    {
        get
        {
            return (int)ViewState["Skip"];
        }
        set
        {
            ViewState["Skip"] = value;
        }
    }


    private void FetchData()
    {
        using (DataTable dc = new DataTable())
        {
            cboNames.Items.Clear();
            if (Skip > 0)
            {
                cboNames.Items.Add(new ListItem("<< Pre 10 Rows...", "Prev"));
                cboNames.AppendDataBoundItems = true;
            }

            var query = dc.Customers
                            .OrderBy(o => o.ContactName)
                            .Take(Take)
                            .Skip(Skip);
            cboNames.DataTextField = "ContactName";
            cboNames.DataSource = query.ToList();
            cboNames.DataBind();

            if (cboNames.Items.Count >= 10)
            {
                cboNames.Items.Add(new ListItem("Next 10 Rows >>", "Next"));
            }
            cboNames.SelectedIndex = Skip > 0 ? 1 : 0;
        }
    }


    protected void cboNames_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboNames.SelectedValue == "Next")
        {
            Take += 10;
            Skip += 10;
            FetchData();
        }
        else if (cboNames.SelectedValue == "Prev")
        {
            Take -= 10;
            Skip -= 10;
            FetchData();
        }
    }


    */

}
