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


public partial class admin_generatePass_student : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        loadStudent();
       
    }

    private void loadStudent()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_batchwise_student(txt_batch.Text));

        GridView1.DataSource = ds;
        GridView1.DataMember = "STUDENT";
        GridView1.DataBind();
    } 


    protected void btn_set_advisor_Click1(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_batchwise_student(txt_batch.Text));

        Random rdm = new Random();
        foreach (DataRow dr in ds.Tables["STUDENT"].Rows)
        {
            dr["SPASSWORD"] = "" + rdm.Next(1000, 32000);
        }
        int count = new admin_webService().set_genarated_pass(ds);
        lbl_msg.Text = "" + count + " password updated";

        loadStudent();
    }
}
