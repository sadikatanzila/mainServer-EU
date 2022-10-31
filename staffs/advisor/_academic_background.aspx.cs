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

public partial class staffs_courses_academic_background : System.Web.UI.Page
{
    string sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ids"] != null)
            sid = Request.QueryString["ids"].ToString();
        flash_info();
        load_student_information();

        load_academic_info();
    }

    private void load_academic_info()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("academic");
        ds.Tables["academic"].Columns.Add("deg_name");
        ds.Tables["academic"].Columns.Add("col_name");
        ds.Tables["academic"].Columns.Add("board");
        ds.Tables["academic"].Columns.Add("marks");
        ds.Tables["academic"].Columns.Add("pass_year");
        ds.Tables["academic"].Columns.Add("GPA");

        ds.Merge(new staff_webService().get_SSC_information(sid));
        ds.Merge(new staff_webService().get_oLevel_information(sid));
        ds.Merge(new staff_webService().get_HSC_information(sid));
        ds.Merge(new staff_webService().get_ALevel_information(sid));
        ds.Merge(new staff_webService().get_otherLevel_information(sid));

        foreach (DataRow dr in ds.Tables["ACADEMICBACK_SSC"].Rows) // for SSC
         {
             DataRow drS=ds.Tables["academic"].NewRow();
             drS["deg_name"] = "SSC";
             drS["col_name"] = dr["SCHOOL"].ToString();
             drS["board"] = dr["SBOARD"].ToString();
             drS["marks"] = dr["STMARKS"].ToString();
             drS["pass_year"] = dr["SPASSYEAR"].ToString();
           
            if (!String.IsNullOrEmpty(dr["SGPA"].ToString()))
             {
                 if (Convert.ToDouble(dr["SGPA"].ToString()) == 1)
                     drS["GPA"] = "1st Division";
                 else if (Convert.ToDouble(dr["SGPA"].ToString()) == 2)
                     drS["GPA"] = "2nd Division";
                 else if (Convert.ToDouble(dr["SGPA"].ToString()) == 3)
                     drS["GPA"] = "3rd Division";
                 else drS["GPA"] = dr["SGPA"].ToString();
             }
             else drS["GPA"] = "N/A";

             ds.Tables["academic"].Rows.Add(drS);

             break;
         }
         foreach (DataRow dr in ds.Tables["ACADEMICBACK_OLEVEL"].Rows) // O-Level
         {
             DataRow drS = ds.Tables["academic"].NewRow();
             drS["deg_name"] = "O-Level";
             drS["col_name"] = dr["OSCHOOL"].ToString();
             drS["marks"] = dr["HTMARKS"].ToString();
             drS["board"] = "N/A";
             drS["marks"] = "N/A";
             drS["pass_year"] = dr["OPASSYEAR"].ToString();
             drS["GPA"] = dr["OGPA"].ToString();
             ds.Tables["academic"].Rows.Add(drS);
             break;
         }

         foreach (DataRow dr in ds.Tables["ACADEMICBACK_HSC"].Rows) // for SSC
         {
             DataRow drS = ds.Tables["academic"].NewRow();
             drS["deg_name"] = "HSC";
             drS["col_name"] = dr["COLLEGE"].ToString();
             drS["board"] = dr["HBOARD"].ToString();
             drS["marks"] = dr["HTMARKS"].ToString();
             drS["pass_year"] = dr["HPASSYEAR"].ToString();

             if (!String.IsNullOrEmpty(dr["HGPA"].ToString()))
             {
                 if (Convert.ToDouble(dr["HGPA"].ToString()) == 1)
                     drS["GPA"] = "1st Division";
                 else if (Convert.ToDouble(dr["HGPA"].ToString()) == 2)
                     drS["GPA"] = "2nd Division";
                 else if (Convert.ToDouble(dr["HGPA"].ToString()) == 3)
                     drS["GPA"] = "3rd Division";
                 else drS["GPA"] = dr["HGPA"].ToString();
             }
             else drS["GPA"] = "N/A";

             ds.Tables["academic"].Rows.Add(drS);

             break;
         }
         foreach (DataRow dr in ds.Tables["ACADEMICBACK_ALEVEL"].Rows) // O-Level
         {
             DataRow drS = ds.Tables["academic"].NewRow();
             drS["deg_name"] = "A-Level";
             drS["col_name"] = dr["ACOLLEGE"].ToString();
             drS["board"] = "N/A";
             drS["marks"] = "N/A";
             drS["pass_year"] = dr["APASSYEAR"].ToString();
             drS["GPA"] = dr["AGPA"].ToString();
             ds.Tables["academic"].Rows.Add(drS);
             break;
         }


         foreach (DataRow dr in ds.Tables["ACADEMICBACK_OTHERS"].Rows) // for SSC
         {
             if (!String.IsNullOrEmpty(dr["BUNIVERSITY"].ToString()))
             {
                 DataRow drS = ds.Tables["academic"].NewRow();
                 drS["deg_name"] = "Bachelor";
                 drS["col_name"] = dr["BUNIVERSITY"].ToString();
                 drS["board"] = "N/A";
                 drS["marks"] = dr["BMARKS"].ToString();
                 drS["pass_year"] = dr["BYEAROFPASSING"].ToString();

                 if (!String.IsNullOrEmpty(dr["BCLASS"].ToString()))
                 {
                     if (Convert.ToDouble(dr["BCLASS"].ToString()) == 1)
                         drS["GPA"] = "1st Division";
                     else if (Convert.ToDouble(dr["BCLASS"].ToString()) == 2)
                         drS["GPA"] = "2nd Division";
                     else if (Convert.ToDouble(dr["BCLASS"].ToString()) == 3)
                         drS["GPA"] = "3rd Division";
                     else drS["GPA"] = dr["BCLASS"].ToString();
                 }
                 else drS["GPA"] = "N/A";
                 ds.Tables["academic"].Rows.Add(drS);
             }
             if (!String.IsNullOrEmpty(dr["MUNIVERSITY"].ToString()))
             {
                 DataRow drS = ds.Tables["academic"].NewRow();
                 drS["deg_name"] = "Masters";
                 drS["col_name"] = dr["MUNIVERSITY"].ToString();
                 drS["board"] = "N/A";
                 drS["marks"] = dr["MMARKS"].ToString();
                 drS["pass_year"] = dr["MYEAROFPASSING"].ToString();

                 if (!String.IsNullOrEmpty(dr["MCLASS"].ToString()))
                 {
                     if (Convert.ToDouble(dr["MCLASS"].ToString()) == 1)
                         drS["GPA"] = "1st Division";
                     else if (Convert.ToDouble(dr["MCLASS"].ToString()) == 2)
                         drS["GPA"] = "2nd Division";
                     else if (Convert.ToDouble(dr["MCLASS"].ToString()) == 3)
                         drS["GPA"] = "3rd Division";
                     else drS["GPA"] = dr["MCLASS"].ToString();
                 }
                 else drS["GPA"] = "N/A";
                 ds.Tables["academic"].Rows.Add(drS);
             } 
             break;
         }
         GridView_academic.DataSource = ds;
         GridView_academic.DataMember = "academic";
         GridView_academic.DataBind();
    
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

            lbl_admission.Text = new cls_tools().get_word_semester(dr["ADMINSEMETER"].ToString()) + ", " + dr["ADMINYEAR"].ToString(); ;

            lbl_admissionAs.Text = dr["STATUS"].ToString();

            if (!String.IsNullOrEmpty(dr["ADVISOR_ID"].ToString()))
                lbl_advisor.Text = dr["STAFF_NAME"].ToString();
            else
                lbl_advisor.Text = "N/A";

            lbl_cgpa.Text = "N/A";

            if (!String.IsNullOrEmpty(dr["COMMENTS"].ToString()))
                lbl_comments.Text = dr["COMMENTS"].ToString();
            else
                lbl_comments.Text = "N/A";

            lbl_completd_hrs.Text = "N/A";

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
