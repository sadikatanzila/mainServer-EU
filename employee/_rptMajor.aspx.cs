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
using System.Data;

public partial class employee_rptMajor : System.Web.UI.Page
{
    string dep = "";
    string user = "";
    student_webService obj_student = new student_webService();
    string DEPTCODE = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                user = Session["ctrl_admin_Id"].ToString();
                DEPTCODE = Session["DEPTCODE"].ToString();
            }
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }


        lbl_message.Text = "";

        btn_submit.Attributes.Add("onClick", " return chech_valid();");

        if (!IsPostBack)
        {
            loadProgram();
            loadMajors();
        }

    }

    public void loadProgram()
    {
        DataTable ds = new DataTable();
        if (Session["DEPTCODE"].ToString() != "")
        {
            ds.Merge(new student_webService().get_ProgramListDept(Session["DEPTCODE"].ToString(), "ProgramList"));
        }
        else
        {
            ds.Merge(new student_webService().get_ProgramList("ProgramList"));
        }

       
       // ds.Merge(new student_webService().get_ProgramList("ProgramList"));

        ddlProgram.DataSource = ds;
        ddlProgram.DataTextField = "PROGRAM";
        ddlProgram.DataValueField = "C_PROGINDEPT_ID";
        ddlProgram.DataBind();
    }

    public void loadMajors()
    {
        DataTable ds = new DataTable();
        ds.Merge(new student_webService().get_MajorList("MajorList"));

        DataRow dr = ds.NewRow();// Tables["District_list"].NewRow();
        dr["NAME"] = "Select";
        dr["C_MAJOR_ID"] = "0";
        ds.Rows.Add(dr);// .Tables["District_list"].Rows.Add(dr);

        ddlMajor.DataSource = ds;
        ddlMajor.DataTextField = "NAME";
        ddlMajor.DataValueField = "C_MAJOR_ID";
        ddlMajor.DataBind();
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        String SemYear="";
        if (ddlSemester.SelectedValue.ToString() != "Select" && txtYear.Text != "" && ddlProgram.SelectedValue.ToString() != "Select")
        {
            lblHeading.Text = "Major Corse Taken Student of " + ddlSemester.SelectedItem.Text + ", " + txtYear.Text ;
            int major = Convert.ToInt32(ddlMajor.SelectedValue);
            SemYear = ddlSemester.SelectedValue.ToString() + txtYear.Text;
            DataTable ds = new DataTable();

           
            if (Convert.ToInt32(ddlMajor.SelectedValue.ToString()) > 0)
            {
                ds.Merge(new student_webService().get_MajorStudentList(Convert.ToInt32(ddlProgram.SelectedValue.ToString()),
                Convert.ToInt32(ddlMajor.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue.ToString()),
                Convert.ToInt32(txtYear.Text), "MajorStudentList"));
            }
            else
            {
               
                ds.Merge(new student_webService().get_MajorStudentListAllMjr(Convert.ToInt32(ddlProgram.SelectedValue.ToString()),
                     Convert.ToInt32(SemYear), "MajorStudentList"));
            }


            
            if (ds.Rows.Count > 0)
            {
                GridView_student.DataSource = ds;
                GridView_student.DataMember = "MajorStudentList";
                GridView_student.DataBind();
            }




        }
        else
        {
            lbl_message.Text = "Please Enter the necessary Year & Semester";
        }
    }

    int j = 1; 
    protected void GridView_student_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = j.ToString();
            j++;
        }

        
    }
}