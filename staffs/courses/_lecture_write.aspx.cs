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

public partial class staffs_courses_lecture_write : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["code"] != null)
        {
            DataSet ds = new DataSet();
            ds.Merge(new staff_webService().get_a_lecture_teacher(Request.QueryString["code"].ToString()));
            foreach (DataRow dr in ds.Tables["assignment"].Rows)
            {
                Response.AddHeader("Content-Disposition", "attachment; filename=" + dr["FILE_NAME"].ToString());// + arrFile[arrFile.Length - 1].Trim());
                Response.WriteFile("c_materials/" + dr["COURSE_MATERIALS_ID"].ToString() + "." + dr["FILE_NAME"].ToString().Split('.')[dr["FILE_NAME"].ToString().Split('.').Length-1]);
            } 
              //byte[] flByte = System.IO.File.ReadAllBytes(Server.MapPath("c_materials") + "/Lec11.zip");
              //Response.BinaryWrite(flByte);
        }   
    }
}
