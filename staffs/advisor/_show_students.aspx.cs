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

public partial class staffs_advisor_show_students : System.Web.UI.Page
{    

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

        btn_submit.Attributes.Add("onClick", " return check_valid();");

        if (!Page.IsPostBack)
        {
            load_student();
        }
    }
  

    private void load_student()
    {
        GridView_studentList.Visible = true;
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_allStudent_ofA_advisorNEW(Session["user"].ToString(), txt_batch.Text)); //studentList chkSelect
     //   ds.Merge(new staff_webService().get_allStudent_ofA_advisor(Session["user"].ToString(), txt_batch.Text)); //studentList chkSelect
        //get_total_completed_credit
       /* ds.Tables["studentList"].Columns.Add("credit");
        ds.Tables["studentList"].Columns.Add("cgpa");

        foreach (DataRow dr in ds.Tables["studentList"].Rows)
        {
            dr["credit"] = ""+new staff_webService().get_total_completed_credit(dr["sid"].ToString());
            dr["cgpa"] = ""+Math.Round( new staff_webService().get_latest_cgpa(dr["sid"].ToString()),2);
            
        }*/

        GridView_studentList.DataSource = ds;
        GridView_studentList.DataMember = "studentList";
        GridView_studentList.DataBind(); 
    }

   

   
    protected void btn_submit_Click1(object sender, EventArgs e)
    {
       
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        GridView_studentList.Visible = true;
        load_student();
    }
}
