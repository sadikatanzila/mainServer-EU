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

public partial class staffs_advisor_studentDetails : System.Web.UI.Page
{
    String sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else if (String.IsNullOrEmpty(Session["user"].ToString()))
            {
                Response.Redirect("../_login.aspx");
            }
        }
        catch (Exception ert) { Response.Redirect("../_login.aspx"); }

        if (Request.QueryString["ids"] != null)
            sid = Request.QueryString["ids"].ToString();

        hp_link_academic_background.NavigateUrl = "#"+sid;
        hp_link_academic_background.Attributes.Add("onClick", " return open_academic_backgrounf('" + sid + "');");

        hp_link_completed_course.NavigateUrl = "#"+ sid;
        hp_link_completed_course.Attributes.Add("onClick", " return open_completed_course('" + sid + "');");

        hpLink_academic_status.NavigateUrl = "#" + sid;
        hpLink_academic_status.Attributes.Add("onClick", " return open_current_academic_status('" + sid + "');");

        
        
        
        flash_info();
        load_student_information();
    }

    private void flash_info()
    {
        lbl_address.Text = "";
        lbl_admission.Text = "";
        lbl_admissionAs.Text = "";
        lbl_advisor.Text = "";
        lbl_cgpa.Text = "";
        lbl_comments.Text = "";
        lbl_completd_hrs.Text = "";
        lbl_dob.Text = "";
        lbl_email.Text = "";
        lbl_gender.Text = "";
        lbl_id.Text = "";
        lbl_major.Text = "";
        lbl_name.Text = "";
        lbl_phone.Text = "";
        lbl_program.Text = "";    
    }

    private void load_student_information()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_a_student_information(sid));

        foreach (DataRow dr in ds.Tables["student"].Rows)
        {
            if (!String.IsNullOrEmpty(dr["ADDRESS"].ToString()))
                lbl_address.Text = dr["ADDRESS"].ToString();
            else
                lbl_address.Text = "N/A";
            
            lbl_admission.Text =new cls_tools().get_word_semester(dr["ADMINSEMETER"].ToString()) + ", " + dr["ADMINYEAR"].ToString(); ;
           
            lbl_admissionAs.Text = dr["STATUS"].ToString();

            if (!String.IsNullOrEmpty(dr["ADVISOR_ID"].ToString()))
                lbl_advisor.Text = dr["STAFF_NAME"].ToString();
            else
                lbl_advisor.Text = "N/A";

            lbl_cgpa.Text = "" + Math.Round(new staff_webService().get_latest_cgpa(sid), 2);
            lbl_completd_hrs.Text = ""+ new staff_webService().get_total_completed_credit(sid);
            //dr["cgpa"] = "" + Math.Round(new staff_webService().get_latest_cgpa(dr["sid"].ToString()), 2);

            if (!String.IsNullOrEmpty(dr["COMMENTS"].ToString()))
                lbl_comments.Text = dr["COMMENTS"].ToString();
            else
                lbl_comments.Text = "N/A"; 
            
            

            if (!String.IsNullOrEmpty(dr["DOB"].ToString()))
                lbl_dob.Text = new cls_tools().get_user_formateDate(dr["DOB"].ToString());
            else
                lbl_dob.Text = "N/A";

            if (!String.IsNullOrEmpty(dr["EMAIL"].ToString()))
                lbl_email.Text = dr["EMAIL"].ToString();
            else
                lbl_email.Text = "N/A";  

            if (dr["GENDER"].ToString() == "F")
                lbl_gender.Text = "Female";
            else if (dr["GENDER"].ToString() == "M")
                lbl_gender.Text = "Male";

            lbl_id.Text = dr["SID"].ToString();

            if (!String.IsNullOrEmpty(dr["MAJOR"].ToString()))
                lbl_major.Text = dr["MAJOR"].ToString();
            else
                lbl_major.Text = "N/A"; 
            
            lbl_name.Text = dr["SNAME"].ToString();

            if (!String.IsNullOrEmpty(dr["PHONE"].ToString()))
                lbl_phone.Text = dr["PHONE"].ToString();
            else
                lbl_phone.Text = "N/A"; 
             
            lbl_program.Text = dr["SPROGRAM"].ToString();

            break;
        }
    
    }
}
