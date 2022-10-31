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

public partial class student_academics_semGradeSheet : System.Web.UI.Page
{
    String sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        lbl_semester.Text = "";
        btn_submit.Attributes.Add("onClick", " return save_check();");

        if (Session.Count == 0)
            Response.Redirect("../_login.aspx");
        else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
        {            
            Response.Redirect("../_login.aspx");
        }
        else
        {
            sid = Session["ctrlId"].ToString();
        }
       
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        double dues = 0.0;
        if (txt_year.Text + cmb_semester.SelectedValue.ToString() == "20152")
        {
            dues = new student_webService().GetAccountBalance(sid, cmb_semester.SelectedValue.ToString(), txt_year.Text);
        }
		
        if (dues > 5000.0)
        {
            lbl_message.Text = "Please clear account dues(" + dues.ToString() + " TK) for " + cmb_semester.SelectedItem.Text +  ", " + txt_year.Text;
            return;
        }
        else
        {
            loadGradeSheet();
        }
    }

    private void loadGradeSheet()
    {
        PlaceHolder_gradeSheet.Controls.Clear();
        bool clearance_status = true;

        if (Convert.ToInt32("0" + txt_year.Text.ToString()) >= 2008)
        {
            string clearence = "" + new student_webService().get_clearence_status(sid + cmb_semester.SelectedValue.ToString() + txt_year.Text.ToString(), cmb_semester.SelectedValue.ToString(), txt_year.Text.ToString());
            string[] stat = clearence.Split('_');

            //lbl_message.Text = "Sorry you have not clear your ";
            
            string msg = "";
            if (stat[3] != "1")
            {               
               msg += " Result not yet published";                
            }
            else {
                lbl_message.Text = "Please ";
                if (stat[0] != "1")
                {
                    //msg += " accounce";
                    if (!new student_webService().get_accounts_status(sid))
                        msg += "clear accounce dues";
                    // lbl_message.Text = "Please clear your dues and contact with IT department";
                }

                if (stat[1] != "1")
                {
                    if (msg == "")
                        msg += " get library clearence";
                    else
                        msg += ", get library clearence";
                }

                if (msg.Trim() != "")
                {
                    msg += " and contact with IT department";
                }
                if (stat[2] != "1")
                {
                    if (msg == "")
                        msg += " do teacher evaluation";
                    else
                        msg += "</br>.And do teacher evaluation";
                }
            }
            if (msg != "")
            {
                // msg += " please contact with particular department(s).<br>";
                msg += "</br>";
                lbl_message.Text += msg;
                clearance_status = false;
            }
            else
                lbl_message.Text = "";
        }
        

        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_semester_GradeSheet(sid+cmb_semester.SelectedValue.ToString()+txt_year.Text.ToString()));
        if (ds.Tables["gradeSheet"].Rows.Count == 0)
        {
            lbl_message.Text = ""+new cls_message().getMessage(9);
            return;
        }

        for (int count = 0; count < ds.Tables["gradeSheet"].Rows.Count; count++)
        {
            DataRow drt = ds.Tables["gradeSheet"].Rows[0];
            for (int j = count + 1; j < ds.Tables["gradeSheet"].Rows.Count; j++)
            {
                if (ds.Tables["gradeSheet"].Rows[count]["COURSECODE"].ToString() == ds.Tables["gradeSheet"].Rows[j]["COURSECODE"].ToString())
                {
                    ds.Tables["gradeSheet"].Rows.RemoveAt(j);
                    j--;
                }
            }
        }


        Table tbl = new Table();
        PlaceHolder_gradeSheet.Controls.Add(tbl);

        TableRow trHeader = new TableRow();
        trHeader.BackColor = System.Drawing.Color.LightBlue;
        tbl.Controls.Add(trHeader);
        
        TableCell tdHCode = new TableCell();
        tdHCode.Text = "Course Code";
        tdHCode.Font.Bold = true;
        tdHCode.HorizontalAlign = HorizontalAlign.Center;
        trHeader.Controls.Add(tdHCode);
        
        TableCell tdHTitle = new TableCell();
        tdHTitle.Text = "Course Title";
        tdHTitle.Font.Bold = true;
        //tdHTitle.HorizontalAlign = HorizontalAlign.Center;
        trHeader.Controls.Add(tdHTitle);
        
        TableCell tdHCreditHours = new TableCell();
        tdHCreditHours.Text = "Credit Hours";
        tdHCreditHours.Font.Bold = true;
        tdHCreditHours.HorizontalAlign = HorizontalAlign.Center;
        trHeader.Controls.Add(tdHCreditHours);

        TableCell tdHGrade= new TableCell();
        tdHGrade.Text = "Grade";
        tdHGrade.Font.Bold = true;
        tdHGrade.HorizontalAlign = HorizontalAlign.Center;
        trHeader.Controls.Add(tdHGrade);
        
        TableCell tdHGradePoint = new TableCell();
        tdHGradePoint.Text = "Grade Point";
        tdHGradePoint.Font.Bold = true;
        tdHGradePoint.HorizontalAlign = HorizontalAlign.Center;
        trHeader.Controls.Add(tdHGradePoint);

        int i = 0;
        double tgp = 0;
        double creditHrs=0;
        double tCrditHrs = 0;

        lbl_semester.Text = "" + cmb_semester.SelectedItem.Text + " Semester " + txt_year.Text;
        foreach (DataRow dr in ds.Tables["gradeSheet"].Rows)
        {
            TableRow tr = new TableRow();
            if ((i % 2) == 1)
                tr.BackColor = System.Drawing.Color.LavenderBlush;//Honeydew;
            //tr.BackColor = System.Drawing.Color.LightGray;
            tbl.Controls.Add(tr);
            i++;

            TableCell tdCode = new TableCell();
            tdCode.Text = dr["COURSECODE"].ToString();
            tdCode.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(tdCode);

            TableCell tdTitle = new TableCell();
            tdTitle.Text = dr["cName"].ToString();
           // tdTitle.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(tdTitle);

            TableCell tdCreditHours = new TableCell();
            creditHrs = Convert.ToDouble(dr["chours"].ToString());
            if (dr["ggrade2"].ToString() != "I" && dr["ggrade2"].ToString() != "W")
                tCrditHrs += creditHrs;
            if ((creditHrs % 1) > 0)
                tdCreditHours.Text = "" + Math.Round(creditHrs, 2) ;
            else
                tdCreditHours.Text = "" + Math.Round(creditHrs, 2)+ ".0";

            tdCreditHours.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(tdCreditHours);

            TableCell tdGrade = new TableCell();
            if (clearance_status == false)
                tdGrade.Text = "I";
            else
            {
                if (dr["ggrade2"].ToString() == "A" || dr["ggrade2"].ToString() == "B" || dr["ggrade2"].ToString() == "C" || dr["ggrade2"].ToString() == "D" || dr["ggrade2"].ToString() == "F")
                    tdGrade.Text = dr["ggrade2"].ToString() + "&nbsp;";
                tdGrade.Text = dr["ggrade2"].ToString();
            }
            tdGrade.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(tdGrade);

            TableCell tdGradePoint = new TableCell();
            if (clearance_status == false)
                tdGradePoint.Text = "0.0";
            else
            {
               if (Convert.ToDouble(dr["gpoint"].ToString()) == 1 || Convert.ToDouble(dr["gpoint"].ToString()) == 2 || Convert.ToDouble(dr["gpoint"].ToString()) == 3 || Convert.ToDouble(dr["gpoint"].ToString()) == 4)
                    tdGradePoint.Text = "" + Math.Round(Convert.ToDouble(dr["gpoint"].ToString()), 2) + ".0";
                else
                    tdGradePoint.Text = "" + Math.Round(Convert.ToDouble(dr["gpoint"].ToString()), 2);// +".0";
            }
            tdGradePoint.HorizontalAlign = HorizontalAlign.Center;
            if (dr["ggrade2"].ToString() != "I" && dr["ggrade2"].ToString() != "W")
                tgp += Convert.ToDouble(dr["gpoint"].ToString()) * creditHrs;
            tr.Controls.Add(tdGradePoint);
        }

        TableRow trE = new TableRow(); 
        tbl.Controls.Add(trE);
        i++;

        TableCell tdCodeE = new TableCell(); 
        trE.Controls.Add(tdCodeE);

        TableCell tdTitleE = new TableCell(); 
        trE.Controls.Add(tdTitleE);

        TableCell tdCreditHoursE = new TableCell();
        tdCreditHoursE.Text = "Total:" + Math.Round(tCrditHrs, 2) + ".0";
        tdCreditHoursE.Font.Bold = true;
        tdCreditHoursE.HorizontalAlign = HorizontalAlign.Center;
        trE.Controls.Add(tdCreditHoursE);

        TableCell tdGradeE = new TableCell();        
        trE.Controls.Add(tdGradeE);

        TableCell tdGradePointE = new TableCell();
        tdGradePointE.Font.Bold = true;
        tdGradePointE.HorizontalAlign = HorizontalAlign.Right;
        if (clearance_status == false)
            tdGradePointE.Text = "0.0";
        else
        {
            tdGradePointE.Text = "SGPA:" + Math.Round((tgp / tCrditHrs), 2);
        }
        trE.Controls.Add(tdGradePointE);

        TableRow trCGPA = new TableRow();
        tbl.Controls.Add(trCGPA);
        TableCell tdCGPA = new TableCell();
        tdCGPA.ColumnSpan = 5;
        tdCGPA.Font.Bold = true;
        tdCGPA.ForeColor = System.Drawing.Color.Crimson;
        trCGPA.HorizontalAlign = HorizontalAlign.Right;
        if (clearance_status == false)
            tdCGPA.Text = "0.0";
        else
        {
           tdCGPA.Text = "CGPA:" + Math.Round(new staff_webService().get_CGPA_upto_semester(sid, txt_year.Text, cmb_semester.SelectedValue.ToString()), 2);
        }
        trCGPA.Controls.Add(tdCGPA);  
    }
}
