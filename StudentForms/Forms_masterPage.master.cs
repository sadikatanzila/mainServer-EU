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

public partial class student_StudentForms_Forms_masterPage : System.Web.UI.MasterPage
{
    string sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("_login.aspx");
            else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
            {
                Response.Redirect("_login.aspx");
            }
            else
            {
                sid = Session["ctrlId"].ToString();
            }
        }
        catch (Exception exp) { Response.Redirect("_login.aspx"); }

       
        load_student_notice();
        load_Student_Message();

    }

    private void load_Student_Message()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_student_Message(sid));

        if (ds.Tables["WEB_Student_Message"].Rows.Count > 10)
            student_notice.Visible = true;

        for (int i = 0; i < ds.Tables["WEB_Student_Message"].Rows.Count; i++)
        {
            if (i == 10)
            {
                ds.Tables["WEB_Student_Message"].Rows.RemoveAt(i);
                i--;
            }
        }


        grdStdMsg.DataSource = ds;
        grdStdMsg.DataMember = "WEB_Student_Message";
        grdStdMsg.DataBind();
    }

    private void load_student_notice()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_student_notice());

        if (ds.Tables["WEB_NOTICE_BOARD"].Rows.Count > 10)
            student_notice.Visible = true;

        for (int i = 0; i < ds.Tables["WEB_NOTICE_BOARD"].Rows.Count; i++)
        {
            if (i == 10)
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
