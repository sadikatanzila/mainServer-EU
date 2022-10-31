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

public partial class admin_StdsemGradeSheet : System.Web.UI.Page
{
    String sid = ""; string PROGRAM_ID = "";
    string dep = "";
    student_webService obj_studentWs = new student_webService();
    string user = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                dep = Session["DEPTCODE"].ToString();
                user = Session["ctrl_admin_Id"].ToString();
            }
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

        if (!IsPostBack)
        {
            //  loadProgram();
        }


        lbl_message.Text = "";


        btn_submit.Attributes.Add("onClick", " return chech_valid();");
       
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string P_Year = "", prevSem = "", R_Sem = "", R_Year = "", Rmin_Sem = "", Rmin_Year="";
        sid = txtSID.Text;
        DataSet S_ds = new DataSet();
        S_ds.Merge(new student_webService().get_PROGRAM_ID(sid));

        if (S_ds.Tables["Student"].Rows.Count > 0)
        {
            foreach (DataRow R_dr in S_ds.Tables["Student"].Rows)
            {
                PROGRAM_ID = R_dr["PROGRAM_ID"].ToString();
                Session["PROGRAM_ID"] = PROGRAM_ID;
            }
        }


        DataSet R_ds = new DataSet();
        R_ds.Merge(new student_webService().get_Registatus_Year_Semister(sid));

        if (R_ds.Tables["Registedlist"].Rows.Count > 0)
        {
            foreach (DataRow R_dr in R_ds.Tables["Registedlist"].Rows)
            {
                R_Sem = R_dr["PREV_SEMESTER"].ToString();
                R_Year = R_dr["PREV_YEAR"].ToString();
            }
        }

        DataSet Rmin_ds = new DataSet();
        Rmin_ds.Merge(new student_webService().get_min_Registatus_Year_Semister(sid));

        if (Rmin_ds.Tables["Registedlist"].Rows.Count > 0)
        {
            foreach (DataRow Rmin_dr in Rmin_ds.Tables["Registedlist"].Rows)
            {
                Rmin_Sem = Rmin_dr["PREV_SEMESTER"].ToString();
                Rmin_Year = Rmin_dr["PREV_YEAR"].ToString();
            }
        }
       

        double dues = 0.0;
        if ((Convert.ToInt32(txt_year.Text.ToString() + "" + cmb_semester.SelectedValue.ToString()) >= Convert.ToInt32(Rmin_Year + "" + Rmin_Sem))
            && (Convert.ToInt32(txt_year.Text.ToString() + "" + cmb_semester.SelectedValue.ToString()) <= Convert.ToInt32(R_Year + "" + R_Sem)))
        {
            if (txtSID.Text != "")
            {
                DataTable ds = new DataTable();
                if (Session["DEPTCODE"].ToString() != "")
                {
                    string DelT_stdbt = new student_webService().get_StudentName(txtSID.Text, Session["DEPTCODE"].ToString());
                    if (DelT_stdbt != "")
                    {
                        ds.Merge(new student_webService().get_StudentInfo(txtSID.Text, "StudentProfile"));

                    }
                    else
                    {

                        lbl_message.Text = "No Data Found/ This Student ID is not available in your Department";
                    }


                }
                else
                {
                    string DelT_stdbt = new student_webService().FindStdName(txtSID.Text);
                    if (DelT_stdbt != "")
                    {
                        ds.Merge(new student_webService().get_StudentInfo(txtSID.Text, "StudentProfile"));

                    }
                    else
                    {

                        lbl_message.Text = "No Data Found";
                    }

                }

                if (ds.Rows.Count > 0)
                {
                    sid = txtSID.Text;

                    pnlAcademicStatus.Visible = true;
                    #region StudentInfo

                    DataSet dsStd = new DataSet();
                    dsStd.Merge(obj_studentWs.getStudentInfo(sid));

                    //string uniname = "";

                    if (dsStd.Tables["StudentInfo"].Rows.Count > 0)
                    {
                        foreach (DataRow rowStd in dsStd.Tables["StudentInfo"].Rows)
                        {
                            lblSid.Text = rowStd["SID"].ToString();
                            lblName.Text = rowStd["SNAME"].ToString();
                            lblAdmissionYS.Text = rowStd["Semester"].ToString() + " / " + rowStd["ADMINYEAR"].ToString();
                            lblFaculty.Text = rowStd["COLLEGENAME"].ToString();
                            lblProgram.Text = rowStd["NAME"].ToString();
                            lblMajor.Text = rowStd["MAJOR"].ToString();
                        }
                    }
                    #endregion

                    loadGradeSheet(sid);
                }
            }
            else
            {
                lbl_message.Text = "Please give the Student ID";
            }
           
        }
        else
        {
            lbl_message.Text = "Student is not registered for this semester";
        }
    }

    private void loadGradeSheet(string sid)
    {
        
        PlaceHolder_gradeSheet.Controls.Clear();
        bool clearance_status = true;
        double CGPA = 0;
        
        

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

        TableCell tdHEarnedPoint = new TableCell();
        tdHEarnedPoint.Text = "Earned Point";
        tdHEarnedPoint.Font.Bold = true;
        tdHEarnedPoint.HorizontalAlign = HorizontalAlign.Center;
        trHeader.Controls.Add(tdHEarnedPoint);


        int i = 0;
        double tgp = 0;
        double creditHrs=0;
        double tCrditHrs = 0, tEarnedPoint = 0;

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


            TableCell tdEarnedPoint = new TableCell();


            if (Convert.ToDouble(dr["gpoint"].ToString()) == 1 || Convert.ToDouble(dr["gpoint"].ToString()) == 2 || Convert.ToDouble(dr["gpoint"].ToString()) == 3 || Convert.ToDouble(dr["gpoint"].ToString()) == 4 || Convert.ToDouble(dr["gpoint"].ToString()) == 0)
                tdEarnedPoint.Text = "" + Math.Round(Convert.ToDouble(dr["gpoint"].ToString()) * Convert.ToDouble(dr["chours"].ToString()), 2) + ".0";
            else
                tdEarnedPoint.Text = "" + Math.Round(Convert.ToDouble(dr["gpoint"].ToString()) * Convert.ToDouble(dr["chours"].ToString()), 2);// +".0";

            if (dr["ggrade2"].ToString() != "I" && dr["ggrade2"].ToString() != "W")
                tEarnedPoint += Convert.ToDouble(tdEarnedPoint.Text);


            tdEarnedPoint.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(tdEarnedPoint);
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

        TableCell tdEarnedPointE = new TableCell();
        tdEarnedPointE.Text = Convert.ToString(tEarnedPoint);
        tdEarnedPointE.Font.Bold = true;
        tdEarnedPointE.HorizontalAlign = HorizontalAlign.Center;
        trE.Controls.Add(tdEarnedPointE);


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
            DataSet Crds = new DataSet();
            Crds.Merge(new staff_webService().get_CGPA_CH_semesterupto(sid, txt_year.Text, cmb_semester.SelectedValue.ToString()));

            foreach (DataRow dr1 in Crds.Tables["CGPList"].Rows)
            {
                if (dr1["CHRS"].ToString() != "" || dr1["CGPA"].ToString() != "")
                {
                    CGPA = Convert.ToDouble(dr1["CGPA"].ToString());
                  //  TcrdtHrs1 = Convert.ToDouble(dr1["CHRS"].ToString());

                }
            }

            tdCGPA.Text = "CGPA:" + Math.Round(CGPA, 2, MidpointRounding.AwayFromZero);
           
        }
        trCGPA.Controls.Add(tdCGPA);  
    }
}
