using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Convocation_ConvoRegStds : System.Web.UI.Page
{
    string sid = "";
    string user = "";

    string faculty_id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                load_student();
                load_Faculty();

            }
        }
        catch (Exception exp) { 
        
        }

       

    }

    private void load_student()
    {

        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_RegConvo_studentPublic());
        DataList1.DataSource = ds;
        DataList1.DataMember = "EU_RegConvoStd";
        DataList1.DataBind();


        DataSet StdTotal = new DataSet();
        StdTotal.Merge(new student_webService().get_RegConvo_studentTotal());
        if (StdTotal.Tables["EU_RegConvoTotal"].Rows.Count > 0)
        {
            foreach (DataRow dr in StdTotal.Tables["EU_RegConvoTotal"].Rows)
            {
                Session["TotalStd"] = Convert.ToString(dr["Total"]);
                lblTotal.Text = "Total Registered : " + Convert.ToString(Session["TotalStd"]);
            }
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue.ToString() != "Select"
            && ddlDept.SelectedValue.ToString() != "Select")
        {
            try
            {
                lblError.Visible = false;
                load_StudentDeptWise();
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.ToString();
            }
        }
        else
            if (ddlFaculty.SelectedValue.ToString() != "Select")
            {
                try
                {
                    lblError.Visible = false;
                    load_StudentFacultyWise();
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = ex.ToString();
                }
            }
            else
            {
                load_student();
            }
    }

    private void load_StudentDeptWise()
    {

        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_RegFacultyDept_student(ddlFaculty.SelectedValue.ToString(), ddlDept.SelectedValue.ToString()));
        DataList1.DataSource = ds;
        DataList1.DataMember = "EU_RegFacultyDeptStd";
        DataList1.DataBind();


        DataSet StdTotalDept = new DataSet();
        StdTotalDept.Merge(new student_webService().get_Convo_studentTotalFacultyDeptwise(ddlFaculty.SelectedValue.ToString(), ddlDept.SelectedValue.ToString()));
        if (StdTotalDept.Tables["EU_RegConvoTotalDept"].Rows.Count > 0)
        {
            foreach (DataRow dr in StdTotalDept.Tables["EU_RegConvoTotalDept"].Rows)
            {
                Session["TotalStd"] = Convert.ToString(dr["Total"]);
                lblTotal.Text = "Total Registered : " + Convert.ToString(Session["TotalStd"]);
            }
        }

    }

    private void load_StudentFacultyWise()
    {

        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_RegFaculty_student(ddlFaculty.SelectedValue.ToString()));
        DataList1.DataSource = ds;
        DataList1.DataMember = "EU_RegFacultyStd";
        DataList1.DataBind();


        DataSet StdTotalDept = new DataSet();
        StdTotalDept.Merge(new student_webService().get_Convo_studentTotalDeptwise(ddlFaculty.SelectedValue.ToString()));
        if (StdTotalDept.Tables["EU_RegConvoTotalDept"].Rows.Count > 0)
        {
            foreach (DataRow dr in StdTotalDept.Tables["EU_RegConvoTotalDept"].Rows)
            {
                Session["TotalStd"] = Convert.ToString(dr["Total"]);
                lblTotal.Text = "Total Registered : " + Convert.ToString(Session["TotalStd"]);
            }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        load_student();
    }

    private void load_Faculty()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_department());

        DataRow dr = ds.Tables["deptlist"].NewRow();
        dr["COLLEGENAME"] = "Select";
        dr["COLLEGECODE"] = "Select";
        ds.Tables["deptlist"].Rows.Add(dr);

        ddlFaculty.DataSource = ds.Tables["deptlist"];
        ddlFaculty.DataTextField = "COLLEGENAME";
        ddlFaculty.DataValueField = "COLLEGECODE";
        //cmb_Faculty.SelectedValue = null;
        ddlFaculty.DataBind();


        if (faculty_id == "")
        {
            ddlFaculty.SelectedValue = "Select";
            // ddlDept.SelectedValue = "Select";
        }
        else
            ddlFaculty.SelectedValue = faculty_id;

    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        string teacher_id = "";
        //reset_Faculty();

        if (ddlFaculty.SelectedValue.ToString() != "Select")
        {
            string Faculty_ID = ddlFaculty.SelectedValue.ToString();
            load_department(Faculty_ID);
        }
    }

    private void load_department(string Faculty_ID)
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_all_DeptFaculty(Faculty_ID));

        DataRow dr = ds.Tables["DepartmentList"].NewRow();
        dr["DEGREE"] = "Select";
        dr["DEPCODE"] = "Select";
        ds.Tables["DepartmentList"].Rows.Add(dr);

        ddlDept.DataSource = ds.Tables["DepartmentList"];
        ddlDept.DataTextField = "DEGREE";
        ddlDept.DataValueField = "DEPCODE";

        //ddlDept.SelectedValue = null;

        ddlDept.DataBind();

        //if (teacher_id == "")
        //    ddlDept.SelectedValue = "Select";
        //else
        //    ddlDept.SelectedValue = teacher_id;

    }
}
