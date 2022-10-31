using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class staffs_Evaluation_MasterPage_Evaluation : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //check dean of depatment
        try
        {
            string teacher_id = Convert.ToString(Session["user"]);

            DataSet ds = new DataSet();
            ds.Merge(new admin_webService().Check_Dean(teacher_id));

            foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
            {
                if (Convert.ToInt32(dr["CHECK_DEAN"]) == 1)
                {
                    
                    load_teacher_notice();
                }
                else
                {
                   // Response.Redirect("../_home.aspx"); 
                }
            }
        }
        catch (Exception exp) { Response.Redirect("../_home.aspx"); }
        
    }

    private void load_teacher_notice()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_teacher_notice());

        if (ds.Tables["WEB_NOTICE_BOARD"].Rows.Count > 5)
            more_genNotice.Visible = true;

        for (int i = 0; i < ds.Tables["WEB_NOTICE_BOARD"].Rows.Count; i++)
        {
            if (i == 5)
            {
                ds.Tables["WEB_NOTICE_BOARD"].Rows.RemoveAt(i);
                i--;
            }
        }
        GridView_generalNotice.DataSource = ds;
        GridView_generalNotice.DataMember = "WEB_NOTICE_BOARD";
        GridView_generalNotice.DataBind();
    }
}
