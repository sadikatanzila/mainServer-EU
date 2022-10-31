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

public partial class admin_CourseOfferingClearanceDUE : System.Web.UI.Page
{
    string dep = "";
    string user = "";
    student_webService obj_student = new student_webService();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                user = Session["ctrl_admin_Id"].ToString();
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

      
        lbl_message.Text = "";

        btn_submit.Attributes.Add("onClick", " return chech_valid();");
       
    }

    private void load_student()
    {
        DataSet ds = new DataSet();

        ds.Merge(new student_webService().get_registered_Course_OfferingStdIT(cmb_semester.SelectedValue.ToString(),
            txt_year.Text, txt_batch.Text));
       // ds.Merge(new student_webService().get_registered_Course_OfferingStd(cmb_semester.SelectedValue.ToString(),
         //   txt_year.Text, cmb_department.SelectedValue.ToString(), txt_batch.Text));

        ds.Tables["RegStudent"].Columns.Add("acStatus");
       // ds.Tables["student"].Columns.Add("chk_select");

        if (ds.Tables["RegStudent"].Rows.Count == 0)
            lbl_message.Text = "" + new cls_message().getMessage(1);

        foreach (DataRow dr in ds.Tables["RegStudent"].Rows)
        {
            if (dr["VALID"].ToString() == "1")
                dr["acStatus"] = "true";

            else
                dr["acStatus"] = "false";

          
        }

        GridView_student.DataSource = ds;
        GridView_student.DataMember = "RegStudent";
        GridView_student.DataBind();


    }



  


    protected void btn_submit_Click(object sender, EventArgs e)
    {
        load_student();
    }

    protected void btn_cleared_Click(object sender, EventArgs e)
    {
        string ids = "";
        int count = 0;

        DataTable dtGotPermission = new DataTable();

        foreach (GridViewRow gr in GridView_student.Rows)
        {
            string P_Year = "", prevSem = "", R_Sem = "", R_Year = "";

           
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                //either Update
                dtGotPermission = obj_student.GetSID_CourseOfferingPermission(gr.Cells[1].Text, txt_year.Text, cmb_semester.SelectedValue.ToString());
                // or insertion
                DataSet ds = new DataSet();
                ds.Tables.Add("CRS_OFFERING_VALID_STU");

                ds.Tables["CRS_OFFERING_VALID_STU"].Columns.Add("SID");
                ds.Tables["CRS_OFFERING_VALID_STU"].Columns.Add("SEMESTER");
                ds.Tables["CRS_OFFERING_VALID_STU"].Columns.Add("YEAR");
                ds.Tables["CRS_OFFERING_VALID_STU"].Columns.Add("CLEAREDBY");
                ds.Tables["CRS_OFFERING_VALID_STU"].Columns.Add("CLEARED_TIME");
                ds.Tables["CRS_OFFERING_VALID_STU"].Columns.Add("APPROVE_DUES");

                DataRow dr = ds.Tables["CRS_OFFERING_VALID_STU"].NewRow();



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
                InsDate_ds.Merge(new student_webService().get_FN_GET_PER_SEM_DUE(gr.Cells[1].Text, P_Year, prevSem));
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


                dr["SID"] = "" + gr.Cells[1].Text;
                dr["SEMESTER"] = "" + cmb_semester.SelectedValue.ToString();
                dr["YEAR"] = "" + txt_year.Text;
                dr["CLEAREDBY"] = "" + Convert.ToString(Session["ctrl_admin_Id"]);
                dr["CLEARED_TIME"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));
                dr["APPROVE_DUES"] = "" + SemDue;

                ds.Tables["CRS_OFFERING_VALID_STU"].Rows.Add(dr);
                string status = "";
                string statusUpdate = "";
                // ids = gr.Cells[1].Text + cmb_semester.SelectedValue.ToString() + txt_year.Text;


                statusUpdate = new student_webService().set_student_CourseAdvising_Update(ds);


               /* if (dtGotPermission.Rows.Count <= 0)
                {
                   status = new student_webService().save_CourseOfferClearence(ds);

                }
                else
                {
                    statusUpdate = new student_webService().set_student_CourseAdvising_Update(ds);
                }*/
                count++;




            }
        }
        if (count > 0)
        {
            lbl_message.Text = "" + new cls_message().getMessage(2);
            load_student();
        }

    }
  
}