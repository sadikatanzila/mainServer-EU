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

public partial class staffs_courses_studentAssignmentWrite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (Request.QueryString["code"] != null)
        {
            String[] ids = Request.QueryString["code"].Split('_');

            DataSet ds = new DataSet();
            ds.Merge(new student_webService().get_a_assignment_student(ids[0], ids[1]));
            foreach (DataRow dr in ds.Tables["assignment"].Rows)
            {
 
                Response.AddHeader("Content-Disposition", "attachment; filename=" + dr["FILE_NAME"].ToString());
                Response.WriteFile("../../student/course/c_materials_student/" + dr["ATT_FILENAME"].ToString());
            }
            //byte[] flByte = System.IO.File.ReadAllBytes(Server.MapPath("c_materials") + "/Lec11.zip");
            //Response.BinaryWrite(flByte);
        }
    }
}
