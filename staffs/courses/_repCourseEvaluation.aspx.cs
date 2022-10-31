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

public partial class staffs_courses_repCourseEvaluation : System.Web.UI.Page
{
    string course = string.Empty;
    string year= string.Empty;
    string semester = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {

    }
}
