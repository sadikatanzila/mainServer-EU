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

public partial class student_course_assignmentWrite : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        String sid = "";
        //if (Session.Count == 0)
        //    Response.Redirect("../_login.aspx");
        //else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
        //{        
        //    Response.Redirect("../_login.aspx");
        //}
        //else
        sid = Session["ctrlId"].ToString();

        if (Request.QueryString["code"] != null)
        {
            DataSet ds = new DataSet();
            ds.Merge(new student_webService().get_a_assignment_student(sid, Request.QueryString["code"].ToString()));
            foreach (DataRow dr in ds.Tables["assignment"].Rows)
            {
                Response.AddHeader("Content-Disposition", "attachment; filename=" + dr["FILE_NAME"].ToString());
                Response.WriteFile("c_materials_student/" + dr["ATT_FILENAME"].ToString()); 
            }
            //byte[] flByte = System.IO.File.ReadAllBytes(Server.MapPath("c_materials") + "/Lec11.zip");
            //Response.BinaryWrite(flByte);
        }


    }
}
