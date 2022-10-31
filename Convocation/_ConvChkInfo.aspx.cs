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
using System.Text.RegularExpressions;

public partial class Convocation_ConvChkInfo : System.Web.UI.Page
{
    string faculty_id = "", SID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                load_Faculty();
            }
            else
            {
                faculty_id = cmb_Faculty.SelectedValue.ToString();
            }

            pnlDegreeRequired.Visible = true;
        }
        catch (Exception ert) { Response.Redirect("http://www.easternuni.edu.bd/"); }
    }

    protected void CheckStudent()
    {
        if (cmb_Faculty.SelectedValue != null && cmb_semester.SelectedValue != null)
        {
            SID = Convert.ToString(txtStudentID.Text);

            //check double convocation
            DataSet dsDoubleSID = new DataSet();
            dsDoubleSID.Merge(new student_webService().check_ConvoDoubleSID(SID));
            if (dsDoubleSID.Tables["EU_CONVODOUBLESID"].Rows.Count > 0)
            {

                string AlterSID = "";
                foreach (DataRow drConDouble in dsDoubleSID.Tables["EU_CONVODOUBLESID"].Rows)
                {
                    if (SID == drConDouble["SID"].ToString())
                        AlterSID = drConDouble["ALTER_SID"].ToString();
                    else
                        if (SID == drConDouble["ALTER_SID"].ToString())
                            AlterSID = drConDouble["SID"].ToString();

                }

                DataSet dsSuccessAlterSID = new DataSet();
                dsSuccessAlterSID.Merge(new student_webService().check_ConvoSuccess(AlterSID));

                if (dsDoubleSID.Tables["EU_CONVODOUBLESID"].Rows.Count > 0 && dsSuccessAlterSID.Tables["EU_CONVOSUCS"].Rows.Count > 0)
                {

                    Session["DOUBLE_CONVO"] = "1";

                }
                else
                {
                    Session["DOUBLE_CONVO"] = "0";
                }
            }



            DataSet dsSuccess = new DataSet();
            dsSuccess.Merge(new student_webService().check_ConvoSuccess(SID));
            if (dsSuccess.Tables["EU_CONVOSUCS"].Rows.Count > 0)
            {
                lbl_message.Text = "Your Convocation Registration is Already been Succeed";
            }
            else
            {
                DataSet dsConChk = new DataSet();
                dsConChk.Merge(new student_webService().check_ConvoBlock(SID));

                if (dsConChk.Tables["EU_CONVOBLK"].Rows.Count > 0)
                {
                    string msg = "You cannot register. Reason: ";
                    foreach (DataRow drConChk in dsConChk.Tables["EU_CONVOBLK"].Rows)
                    {
                        if (drConChk["PAPERMISSING"].ToString() != "")
                        {
                            msg += " " + drConChk["PAPERMISSING"].ToString() + " related Paper missing. <br/>";
                        }
                        if (drConChk["VARIFICATION"].ToString() != "")
                        {
                            msg += " varification Status of Paper Comment is " + drConChk["VARIFICATION"].ToString() + ". <br/>";
                        }
                        if (drConChk["LIBRARY"].ToString() != "")
                        {
                            msg += " " + drConChk["LIBRARY"].ToString() + " in Library Section. <br/>";
                        }
                        msg += " Please Contact with Registrar Office (mail to romijuddin@easternuni.edu.bd or Contact with 01819701501, 029676031-5 Ext: 208,209) .";
                        //= drConChk["PAPERMISSING"].ToString() + " " + drConChk["VARIFICATION"].ToString();
                        lbl_message.Text = Convert.ToString(msg);
                    }

                }

                else
                {
                    if (new student_webService().check_ConvoInfo(SID, cmb_Faculty.SelectedValue.ToString(), Convert.ToString(txtCGPA.Text),
                                        cmb_semester.SelectedValue.ToString(), Convert.ToString(txtGrYear.Text)))
                    {
                        Session["CONVOSID"] = SID;
                        Response.Redirect("_EUConForm.aspx");
                    }
                    else
                    {
                        lbl_message.Text = "Enter your Information Correctly";
                    }
                }
            }
            //check paper missing or not

        }
        else
        {
            lbl_message.Text = "Enter your Information Correctly";
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        DateTime t1 = DateTime.Now;
        DateTime last_Date = new DateTime(2019, 10, 1, 0, 0, 0);
        DateTime starting_date = new DateTime(2019, 7, 10, 0, 0, 0);

        if (t1 < last_Date && t1 > starting_date)
        {

            if (!IsPostBack)
            {
                load_Faculty();
            }
            else
            {
                faculty_id = cmb_Faculty.SelectedValue.ToString();
            }

            pnlDegreeRequired.Visible = true;
            CheckStudent();
        }
        /*  if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
          {
                
          }*/
        else
        {
            if (t1 < starting_date)
            {
                lbl_message.Text = "Convocation Registration is not started";
                pnlDegreeRequired.Visible = false;
            }
            else
                if (t1 > last_Date)
                {
                    DataTable dsConChk = new DataTable();
                    string sid = txtStudentID.Text;

                    dsConChk.Merge(new student().get_T_PermittedStudent(sid, "EU_CONVOCHK"));
                    if (dsConChk.Rows.Count > 0)
                    {
                        /* if (!IsPostBack)
                         {
                             load_Faculty();
                         }
                         else
                         {
                             faculty_id = cmb_Faculty.SelectedValue.ToString();
                         }*/

                        pnlDegreeRequired.Visible = true;

                        CheckStudent();
                    }

                    else
                    {
                        lbl_message.Text = "Convocation Registration Date is over";
                        pnlDegreeRequired.Visible = false;
                    }
                }

            //  Response.Redirect("_login.aspx");
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


        if (faculty_id == "")
        {
            cmb_Faculty.SelectedValue = "Select";
        }
        else
            cmb_Faculty.SelectedValue = faculty_id;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    public void Clear()
    {
        txtStudentID.Text = "";
        txtCGPA.Text = "";
        txtGrYear.Text = "";
        cmb_semester.SelectedValue = null;
        cmb_Faculty.SelectedValue = null;
    }
}
