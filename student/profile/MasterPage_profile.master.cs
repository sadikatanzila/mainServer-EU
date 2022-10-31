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

public partial class student_MasterPage_profile : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        student_notice.Visible = false;
        load_student_notice();
    }

    private void load_student_notice()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_student_notice());

        if (ds.Tables["WEB_NOTICE_BOARD"].Rows.Count > 4)
            student_notice.Visible = true;


        for (int i = 0; i < ds.Tables["WEB_NOTICE_BOARD"].Rows.Count; i++)
        {
            if (i == 4)
            {
                ds.Tables["WEB_NOTICE_BOARD"].Rows.RemoveAt(i);
                i--;
            }
        }



        GridView_studentNotice.DataSource = ds;
        GridView_studentNotice.DataMember = "WEB_NOTICE_BOARD";
        GridView_studentNotice.DataBind();
    }
}
