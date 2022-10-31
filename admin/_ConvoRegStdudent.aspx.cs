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

public partial class admin_ConvoRegStdudent : System.Web.UI.Page
{
    string sid = "";
    string user = "", degree_id="";

    protected void Page_Load(object sender, EventArgs e)
    {
       /* try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                user = Session["ctrl_admin_Id"].ToString();

            }
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }*/

        if (!IsPostBack)
        {
            load_student(txtConvo.Text);
            load_degree();
        }

    }
    private void load_degree()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_degreeName());

        DataRow dr = ds.Tables["degreetlist"].NewRow();
        dr["NAME"] = "Select";
        dr["C_PROGINDEPT_ID"] = "0";
        ds.Tables["degreetlist"].Rows.Add(dr);

        cmb_Degree.DataSource = ds.Tables["degreetlist"];
        cmb_Degree.DataTextField = "NAME";
        cmb_Degree.DataValueField = "C_PROGINDEPT_ID";
        cmb_Degree.DataBind();


        if (degree_id == "")
        {
            cmb_Degree.SelectedValue = "0";
        }
        else
            cmb_Degree.SelectedValue = degree_id;

    }
    private void load_student(string txtConvo)
    {

        DataSet ds = new DataSet(txtConvo);
        ds.Merge(new student_webService().get_RegConvo_studentNew(txtConvo));
        DataList1.DataSource = ds;
        DataList1.DataMember = "EU_RegConvoStd";
        DataList1.DataBind();

        DataSet StdTotal = new DataSet();
        StdTotal.Merge(new student_webService().get_RegConvo_studentTotal(txtConvo));
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
       // load_student( );

        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_RegConvo_studentSearch(cmb_Degree.SelectedValue.ToString(),txtYear.Text,cmb_semester.SelectedValue.ToString(), txtConvo.Text));
        DataList1.DataSource = ds;
        DataList1.DataMember = "EU_RegConvoStd";
        DataList1.DataBind();

        DataSet StdTotal = new DataSet();
        StdTotal.Merge(new student_webService().get_RegConvo_studentTotalSearchwise(cmb_Degree.SelectedValue.ToString(), txtYear.Text, cmb_semester.SelectedValue.ToString(), txtConvo.Text));
        if (StdTotal.Tables["EU_RegConvoTotal"].Rows.Count > 0)
        {
            foreach (DataRow dr in StdTotal.Tables["EU_RegConvoTotal"].Rows)
            {
                Session["TotalStd"] = Convert.ToString(dr["Total"]);
                lblTotal.Text = "Total Registered : " + Convert.ToString(Session["TotalStd"]);
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        load_student(txtConvo.Text);
    }
}
