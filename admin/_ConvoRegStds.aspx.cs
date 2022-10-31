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

public partial class admin_ConvoRegStds : System.Web.UI.Page
{
    string sid = "";
    string user = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                user = Session["ctrl_admin_Id"].ToString();
              

            }
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

        if (!IsPostBack)
        {
            string year = txtYear.Text;
            load_student(year);
            load_Faculty();
        }
    }


    private void load_Faculty()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_department());

        DataRow dr = ds.Tables["deptlist"].NewRow();
        dr["COLLEGENAME"] = "Select";
        dr["COLLEGECODE"] = "Select";
        ds.Tables["deptlist"].Rows.Add(dr);

        cmb_Faculty.DataSource = ds.Tables["deptlist"];
        cmb_Faculty.DataTextField = "COLLEGENAME";
        cmb_Faculty.DataValueField = "COLLEGECODE";
        cmb_Faculty.DataBind();


        //if (faculty_id == "")
        //{
        //    cmb_Faculty.SelectedValue = "Select";
        //    // cmb_teacher.SelectedValue = "Select";
        //}
        //else
        //    cmb_Faculty.SelectedValue = faculty_id;

    }
    private void load_student(string year)
    {

        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_RegConvo_student(year));
        GridView_student.DataSource = ds;
        GridView_student.DataMember = "EU_RegConvoStd";
        GridView_student.DataBind();

        DataSet StdTotal = new DataSet();
        StdTotal.Merge(new student_webService().get_RegConvo_studentTotal(year));
        if (StdTotal.Tables["EU_RegConvoTotal"].Rows.Count > 0)
        {
            foreach (DataRow dr in StdTotal.Tables["EU_RegConvoTotal"].Rows)
            {
                Session["TotalStd"] = Convert.ToString(dr["Total"]);
                lblTotal.Text = "Total Registered : " + Convert.ToString(Session["TotalStd"]);
                if (user == "200011" || user == "200083" || user == "200019" || user == "200048" || user == "200030")
                    lblTotal.Visible = true;
                else
                    lblTotal.Visible = false;
            }
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


            if (user == "200011" || user == "200083" || user == "200019" || user == "200048")
                GridView_student.Columns[0].Visible = true;
            else
                GridView_student.Columns[0].Visible = false;
            //when mouse is over the row, save original color to new attribute, and change it to highlight yellow color
           // e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
            //when mouse leaves the row, change the bg color to its original value   
           // e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
        }
    }



    protected void btnClear_Click(object sender, EventArgs e)
    {
        string year = txtYear.Text;
        load_student(year);
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
      //  load_student();
        if (cmb_Faculty.SelectedValue.ToString() != "Select")
        {
            string Faculty_ID = cmb_Faculty.SelectedValue.ToString();
            DataSet ds = new DataSet();
            ds.Merge(new student_webService().get_all_StudentFacultyWise(Faculty_ID, txtYear.Text));
            GridView_student.DataSource = ds;
            GridView_student.DataMember = "EU_RegConvoStdDept";
            GridView_student.DataBind();



            DataSet StdTotalDept = new DataSet();
            StdTotalDept.Merge(new student_webService().get_Convo_studentTotalDeptwise(Faculty_ID, txtYear.Text));
            if (StdTotalDept.Tables["EU_RegConvoTotalDept"].Rows.Count > 0)
            {
                foreach (DataRow dr in StdTotalDept.Tables["EU_RegConvoTotalDept"].Rows)
                {
                    Session["TotalStd"] = Convert.ToString(dr["Total"]);
                    lblTotal.Text = "Total Registered : " + Convert.ToString(Session["TotalStd"]);
                }
            }

        }
        else
        {
            load_student(txtYear.Text);
        }
        
    }
}
