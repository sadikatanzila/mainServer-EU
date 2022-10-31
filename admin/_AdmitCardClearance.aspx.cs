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

public partial class admin_AdmitCardClearance : System.Web.UI.Page
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
           // load_student();
         //   cmb_semester.SelectedValue = "2";
         //   cmb_exam.SelectedValue = "F";
        }
        else
        {
            btnCheck_Click(sender, e);
           // cmb_semester.SelectedValue = "2";
          //  cmb_exam.SelectedValue = "F";
        }
        lblmsg.Text = "";
    }

    private void load_student()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_AdmitDueClearance_student());
        GridView_student.DataSource = ds;
        GridView_student.DataMember = "student";
        GridView_student.DataBind();


    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string P_Year = "", prevSem = "", R_Sem = "", R_Year = "";

        DataSet ds = new DataSet();
        ds.Tables.Add("E_STUDENTOPENING_CONTENT");

        ds.Tables["E_STUDENTOPENING_CONTENT"].Columns.Add("SID");
        ds.Tables["E_STUDENTOPENING_CONTENT"].Columns.Add("SEMESTER");
        ds.Tables["E_STUDENTOPENING_CONTENT"].Columns.Add("YEAR");
        ds.Tables["E_STUDENTOPENING_CONTENT"].Columns.Add("EXAMTYPE");
        ds.Tables["E_STUDENTOPENING_CONTENT"].Columns.Add("INSERTIONID");
        ds.Tables["E_STUDENTOPENING_CONTENT"].Columns.Add("INSERTION_TIME");
        ds.Tables["E_STUDENTOPENING_CONTENT"].Columns.Add("APPROVAL_DUES");

        DataRow dr = ds.Tables["E_STUDENTOPENING_CONTENT"].NewRow();

        DataSet Last_ds = new DataSet();
        Last_ds.Merge(new student_webService().get_ADMINREGISTRATIONRATE_LYS());

        if (Last_ds.Tables["Studentlist"].Rows.Count > 0)
        {
            foreach (DataRow Last_dr in Last_ds.Tables["Studentlist"].Rows)
            {
                prevSem = Last_dr["semester"].ToString();
                P_Year = Last_dr["year"].ToString();
            }
        }

        string DUE = "", SemDue = "", graceAmt = "";
        DataSet InsDate_ds = new DataSet();
        InsDate_ds.Merge(new student_webService().get_FN_GET_PER_SEM_DUE(txtSid.Text, P_Year, prevSem));
        if (InsDate_ds.Tables["SEM_DUE"].Rows.Count > 0)
        {
            foreach (DataRow InsDate_dr in InsDate_ds.Tables["SEM_DUE"].Rows)
            {
                DUE = Convert.ToString(InsDate_dr["DUE"]);

                string[] code = DUE.Split('|'); //Request.QueryString["DUE"].ToString().Split('|');
                if (code.Length > 0)
                {
                    SemDue = code[0];
                    graceAmt = code[1];

                }

            }

        }



















        dr["SID"] = "" + txtSid.Text;
        dr["SEMESTER"] = "" + cmb_semester.SelectedValue.ToString();
        dr["YEAR"] = "" + txtYear.Text;
        dr["EXAMTYPE"] = "" + cmb_exam.SelectedValue.ToString();
        dr["INSERTIONID"] = "" + Convert.ToString(Session["ctrl_admin_Id"]);
        dr["INSERTION_TIME"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));
        dr["APPROVAL_DUES"] = "" + SemDue;

        ds.Tables["E_STUDENTOPENING_CONTENT"].Rows.Add(dr);

        string status = "";

        //if (!string.IsNullOrEmpty(stf_id))
        //    status = new admin_webService().update_teacher_info(ds);
        //else
        status = new student_webService().save_STUDENTOPENING_info(ds);


        if (status == "1")
        {
            clear();
            lbl_message.Text = "" + new cls_message().getMessage(2);
            load_student(Session["year"].ToString(), Session["Sem"].ToString());
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
      //  txtYear.Text = "";
        txtStdName.Text = "";
    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        string Year = txtYear.Text;
        string Sem = cmb_semester.SelectedValue.ToString();
        Session["year"] = txtYear.Text;
        Session["Sem"] = cmb_semester.SelectedValue.ToString();
        pnlClearance.Visible = true;
        load_student(Year, Sem);
    }

    private void load_student(String Year, String Sem)
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_AdmitDueClearance_YearSem(Year, Sem));
        GridView_student.DataSource = ds;
        GridView_student.DataMember = "student";
        GridView_student.DataBind();


    }


}
