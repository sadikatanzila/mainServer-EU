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

public partial class admin_semester_course_listPrevious : System.Web.UI.Page
{
    string user = "";
    string sem = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                user=  Session["ctrl_admin_Id"].ToString();
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }
  
        if (IsPostBack)
            sem = Request["cmb_semester"];
        else
            sem = cmb_semester.SelectedValue.ToString();

        Session["sem"] = "";
        Session["year"] = "";

        lbl_message.Text = "";
        btn_submit.Attributes.Add("onClick", " return chech_valid();"); 
        
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txt_year.Text.Trim() == "")
            return;

        Session["sem"] = cmb_semester.SelectedValue.ToString();
        Session["year"] = txt_year.Text;

        GridView_courseList.Visible = true;
        load_courses();
    }


    private void load_courses()
    {
        DataSet ds = new DataSet();
        ds.Merge( new admin_webService().get_all_offeredCourses( txt_year.Text,cmb_semester.SelectedValue.ToString()));

        for (int i = 0; i < ds.Tables["CourseList"].Rows.Count; i++)
        {
            DataRow dr = ds.Tables["CourseList"].Rows[i];
            for (int j = i + 1; j < ds.Tables["CourseList"].Rows.Count; j++)
            {
                DataRow drs = ds.Tables["CourseList"].Rows[j];

                if ((dr["COURSECODE"].ToString() == drs["COURSECODE"].ToString()) && (dr["CNAME"].ToString() == drs["CNAME"].ToString()))
                {
                    ds.Tables["CourseList"].Rows.RemoveAt(j);
                    j--;
                }
            }
        }

        ds.Tables["CourseList"].Columns.Add("serial");
        int s=1;
        lbl_message.Text = ""+new cls_message().getMessage(1);

        foreach (DataRow dr in ds.Tables["CourseList"].Rows)
        {
            lbl_message.Text = "";
            dr["serial"] = s++;
        }

        GridView_courseList.DataSource = ds;
        GridView_courseList.DataMember = "CourseList";
        GridView_courseList.DataBind();

    }
}
