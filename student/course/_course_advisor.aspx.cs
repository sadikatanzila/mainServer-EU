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

public partial class student_course_advisor : System.Web.UI.Page
{

    string sid="";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
            {
                Response.Redirect("../_login.aspx");
            }
            else
            {
                sid = Session["ctrlId"].ToString(); 
            }
        }
        catch (Exception ert) { Response.Redirect("../_login.aspx"); }

        load_advisor();
    }

    private void load_advisor()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_advisor_Info(sid));

        foreach (DataRow dr in ds.Tables["advisor_info"].Rows)
        {
            lbl_college.Text = dr["COLLEGENAME"].ToString();
            lbl_designation.Text = dr["JOB_DESIGNATION"].ToString();
            lbl_email.Text = dr["E_MAIL"].ToString();
            lbl_mobile.Text = dr["MOBILE"].ToString();
            lbl_name.Text = dr["STAFF_NAME"].ToString();
            lbl_phone.Text = dr["PHONE_NUMBER"].ToString();
            if (!String.IsNullOrEmpty(dr["STAFF_PICTURE"].ToString()))
            {
                img_myProfile.ImageUrl = "../../staffs/profile/staff_image/" + dr["ADVISOR_ID"].ToString() + "." + dr["STAFF_PICTURE"].ToString().Split('.')[dr["STAFF_PICTURE"].ToString().Split('.').Length - 1];
            }
            else
                img_myProfile.ImageUrl = "../../images/no_image.gif";



            break;
        }
    }

}
