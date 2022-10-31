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

public partial class admin_StudentNameCorrection : System.Web.UI.Page
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
            load_student();
         //   cmb_semester.SelectedValue = "2";
         //   cmb_exam.SelectedValue = "F";
        }
        else
        {
         
           // cmb_semester.SelectedValue = "2";
          //  cmb_exam.SelectedValue = "F";
        }
        lblmsg.Text = "";
    }

    private void load_student()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_StudentNameChange_list());
        GridView_student.DataSource = ds;
        GridView_student.DataMember = "student";
        GridView_student.DataBind();


    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string P_Year = "", prevSem = "", R_Sem = "", R_Year = "";

        DataSet ds = new DataSet();
        ds.Tables.Add("SNAME_CORRECTION");

        ds.Tables["SNAME_CORRECTION"].Columns.Add("SID");
        ds.Tables["SNAME_CORRECTION"].Columns.Add("SNAME");
        ds.Tables["SNAME_CORRECTION"].Columns.Add("SNAME_PREV");
        ds.Tables["SNAME_CORRECTION"].Columns.Add("UPDATEDBY");
        ds.Tables["SNAME_CORRECTION"].Columns.Add("UPDATETIME");

        DataRow dr = ds.Tables["SNAME_CORRECTION"].NewRow();

       

        dr["SID"] = "" + txtSid.Text;
        dr["SNAME"] = "" + txtStdUpName.Text;
        dr["SNAME_PREV"] = "" + txtStdName.Text;

        dr["UPDATEDBY"] = "" + Convert.ToString(Session["ctrl_admin_Id"]);
        dr["UPDATETIME"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));


        ds.Tables["SNAME_CORRECTION"].Rows.Add(dr);

        string status = "";

        //if (!string.IsNullOrEmpty(stf_id))
        //    status = new admin_webService().update_teacher_info(ds);
        //else
        status = new student_webService().save_StudentName_info(ds);


        if (status == "1")
        {
            clear();
            lbl_message.Text = "" + new cls_message().getMessage(2);
            load_student();
        }
        else
            lbl_message.Text = "" + new cls_message().getMessage(14);
    }

    protected void txtSid_TextChanged(object sender, EventArgs e)
    {
        sid = Convert.ToString(txtSid.Text);
        string DelT_stdbt = new student_webService().FindStdName(sid);
        txtStdName.Text = DelT_stdbt;
    }

    private void clear()
    {
        txtSid.Text = "";
        txtStdUpName.Text = "";
        txtStdName.Text = "";
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
