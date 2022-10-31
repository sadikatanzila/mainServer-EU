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

public partial class staffs_courses_course_list : System.Web.UI.Page
{ 

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["sem"] = "";
        Session["year"] = "";

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
      

        lbl_message.Text = "";
        btn_submit.Attributes.Add("onClick", " return save_check(); " );
        GridView_courseList.Visible = false;
        
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        GridView_courseList.Visible = true;
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_allCourses_ofA_semesterNew(cmb_semester.SelectedValue.ToString(), txt_year.Text));

        GridView_courseList.DataSource = ds;
        GridView_courseList.DataMember = "coursList";
        GridView_courseList.DataBind();

        if (ds.Tables["coursList"].Rows.Count==0)
            lbl_message.Text=""+new cls_message().getMessage(1);

        Session["sem"] = "" + cmb_semester.SelectedValue.ToString();
        Session["year"] = "" + txt_year.Text;
    }


}
