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

public partial class staffs_courses_downloadAssignment : System.Web.UI.Page
{
    string code = "";

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

        if (!IsPostBack)
        {
            if (Request.QueryString["code"] != null)
            {
                code = Request.QueryString["code"].ToString();
                load_assignment_info();
            }
        }
    }


    private void load_assignment_info()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_a_assignmentInfo(code));

        if (ds.Tables["courseMaterial"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["courseMaterial"].Rows[0];
            lbl_description.Text = dr["DESCRIPTION"].ToString();
            lbl_dueDate.Text = new cls_tools().get_user_formateDate(dr["DUE_DATE"].ToString());
            lbl_title.Text = dr["TITLE"].ToString();
            hp_link_aaaisnment.Text = dr["FILE_NAME"].ToString();
            hp_link_aaaisnment.NavigateUrl = "~/staffs/courses/_lecture_write.aspx?code=" + dr["COURSE_MATERIALS_ID"].ToString();
        }

        ds.Merge(new staff_webService().get_all_studentSubmitted_assignment(code));
        foreach (DataRow dr in ds.Tables["s_courseMaterial"].Rows)
        {
            dr["COURSE_MAT_ID"] = "" + dr["SID"] + "_" + dr["COURSE_MAT_ID"];
        }

        GridView_assignment_list.DataSource = ds;
        GridView_assignment_list.DataMember = "s_courseMaterial";
        GridView_assignment_list.DataBind();

    }
}
