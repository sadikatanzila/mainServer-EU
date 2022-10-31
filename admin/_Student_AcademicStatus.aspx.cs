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
using System.Globalization;
using System.Threading;

public partial class admin_Student_AcademicStatus : System.Web.UI.Page
{
    string dep = "";
    string user = "";
    string degree_id = "";

    student_webService obj_studentWs = new student_webService();
    string sid = "", PROGRAM_ID = "";

    double TcrdtHrs = 0;
    double TerndPnt = 0;
    double CGPA = 0;


    double TrnsCH = 0,  WvCH=0;
    double TrnsGP = 0;

    //  ReportDocument crystalReport = null;

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


        lblErrorMsg.Text = "";


        btn_submit.Attributes.Add("onClick", " return chech_valid();");

    }




    protected void btn_submit_Click(object sender, EventArgs e)
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
                    
                    lblErrorMsg.Text = "No Data Found/ This Student ID is not available in your Department";
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
                  
                    lblErrorMsg.Text = "No Data Found";
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
            lblErrorMsg.Text = "Please Enter the Student ID";
        }




    }

 

  

    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        lblErrorMsg.Text = "";
        txtSID.Text = "";
        pnlAcademicStatus.Visible = false;
    }


    private void loadGradeSheet(string sid)
    {
        PlaceHolder_gradeSheet.Controls.Clear();

        DataSet ds = new DataSet();

        ds.Merge(obj_studentWs.get_allRegistred_semesters_ofA_student(sid));  

        #region Transfered Course

        ds.Merge(obj_studentWs.getTransferCourseList(sid));


        if (ds.Tables["TransferedCourse"].Rows.Count > 0)
        {
            DataSet dsUni = new DataSet();
            dsUni.Merge(obj_studentWs.getTransferUniName(sid));

            string uniname = "";

            
            foreach (DataRow row1 in dsUni.Tables["TransferedUni"].Rows)
            {
                uniname = row1["TRANSFERFROM"].ToString();
            }


            Table tblTransferUni = new Table();
            PlaceHolder_gradeSheet.Controls.Add(tblTransferUni);
            TableRow trTuni = new TableRow();
            tblTransferUni.Controls.Add(trTuni);
            TableCell tdTuni = new TableCell();
            tdTuni.Font.Bold = true;
            tdTuni.HorizontalAlign = HorizontalAlign.Right;
            tdTuni.Text = "Transfered from : " + uniname;
            trTuni.Controls.Add(tdTuni);



            PlaceHolder_gradeSheet.Controls.Add(new LiteralControl("&nbsp;<br/><strong>Transfered Course List</strong>"));
            Table tblTransferedCourse = new Table();

            TableRow tblTransferedCourseHeader = new TableRow();
            tblTransferedCourseHeader.BackColor = System.Drawing.Color.LightBlue;
            tblTransferedCourse.Controls.Add(tblTransferedCourseHeader);

            TableCell trCode = new TableCell();
            trCode.Text = "Course Code";
            trCode.Font.Bold = true;
            trCode.HorizontalAlign = HorizontalAlign.Center;
            tblTransferedCourseHeader.Controls.Add(trCode);

            TableCell trTitle = new TableCell();
            trTitle.Text = "Course Title";
            trTitle.Font.Bold = true;
            tblTransferedCourseHeader.Controls.Add(trTitle);


            TableCell trPCode = new TableCell();
            trPCode.Text = "P.Uni Course Code";
            trPCode.Font.Bold = true;
            trPCode.HorizontalAlign = HorizontalAlign.Center;
            tblTransferedCourseHeader.Controls.Add(trPCode);

            TableCell trPTitle = new TableCell();
            trPTitle.Text = "P.Uni Course Title";
            trPTitle.Font.Bold = true;
            tblTransferedCourseHeader.Controls.Add(trPTitle);



            TableCell trCreditHours = new TableCell();
            trCreditHours.Text = "Credit Hours";
            trCreditHours.Font.Bold = true;
            trCreditHours.HorizontalAlign = HorizontalAlign.Center;
            tblTransferedCourseHeader.Controls.Add(trCreditHours);


            TableCell trGrade = new TableCell();
            trGrade.Text = "Grade";
            trGrade.Font.Bold = true;
            trGrade.HorizontalAlign = HorizontalAlign.Center;
            tblTransferedCourseHeader.Controls.Add(trGrade);



            TableCell trGP = new TableCell();
            trGP.Text = "Grade Point";
            trGP.Font.Bold = true;
            trGP.HorizontalAlign = HorizontalAlign.Center;
            tblTransferedCourseHeader.Controls.Add(trGP);

            int trIndex = 0;
            foreach (DataRow row in ds.Tables["TransferedCourse"].Rows)
            {                
                TableRow tr = new TableRow();
                if ((trIndex % 2) == 1)
                    tr.BackColor = System.Drawing.Color.LavenderBlush;

                TableCell tdCode = new TableCell();
                tdCode.Text = row["COURSECODE"].ToString();
                tdCode.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdCode);

                TableCell tdTitle = new TableCell();
                tdTitle.Text = row["CNAME"].ToString();
                tr.Controls.Add(tdTitle);


                TableCell tdPCode = new TableCell();
                tdPCode.Text = row["CCODEPREUNI"].ToString();
                tdPCode.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdPCode);

                TableCell tdPTitle = new TableCell();
                tdPTitle.Text = row["CNAMEPREUNI"].ToString();
                tr.Controls.Add(tdPTitle);

                TableCell tdCreditHours = new TableCell();
                tdCreditHours.Text = row["CREDIT"].ToString();
                tdCreditHours.HorizontalAlign = HorizontalAlign.Center;
                TrnsCH = TrnsCH + Convert.ToDouble(row["CREDIT"].ToString());
                tr.Controls.Add(tdCreditHours);


                TableCell tdGrade = new TableCell();
                tdGrade.Text = row["GRADE"].ToString();
                tdGrade.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdGrade);


                TableCell tdGP = new TableCell();
                tdGP.Text = row["GP"].ToString();
                tdGP.HorizontalAlign = HorizontalAlign.Center;
                TrnsGP = TrnsGP + (Convert.ToDouble(row["GP"].ToString()) * Convert.ToDouble(row["CREDIT"].ToString()));
                tr.Controls.Add(tdGP);

                trIndex++;
                tblTransferedCourse.Controls.Add(tr);



            }

            //************** new add ****************
            TableRow trTCH = new TableRow();
            tblTransferedCourse.Controls.Add(trTCH);
            TableCell tdTCH = new TableCell();
            tdTCH.ColumnSpan = 4;
            tdTCH.Font.Bold = true;
            tdTCH.HorizontalAlign = HorizontalAlign.Right;
            tdTCH.Text = "Total transferred credit hours:" + Math.Round(TrnsCH, 2, MidpointRounding.AwayFromZero);
            trTCH.Controls.Add(tdTCH);



            TableRow trTGP = new TableRow();
            tblTransferedCourse.Controls.Add(trTGP);
            TableCell tdTGP = new TableCell();
            tdTGP.ColumnSpan = 4;
            tdTGP.Font.Bold = true;
            tdTGP.HorizontalAlign = HorizontalAlign.Right;
            tdTGP.Text = "Total transferred grade point:" + Math.Round(TrnsGP, 2, MidpointRounding.AwayFromZero);
            trTGP.Controls.Add(tdTGP);




            PlaceHolder_gradeSheet.Controls.Add(tblTransferedCourse);

            PlaceHolder_gradeSheet.Controls.Add(new LiteralControl("<br/>&nbsp;"));
        }



        #endregion





        #region Waived Course

        ds.Merge(obj_studentWs.getWaiveCourseList(sid));

        if (ds.Tables["WaivedCourse"].Rows.Count > 0)
        {
            PlaceHolder_gradeSheet.Controls.Add(new LiteralControl("&nbsp;<br/><strong>Waived Course List</strong>"));
            Table tblWaivedCourse = new Table();

            TableRow tblWaivedCourseHeader = new TableRow();
            tblWaivedCourseHeader.BackColor = System.Drawing.Color.LightBlue;
            tblWaivedCourse.Controls.Add(tblWaivedCourseHeader);

            TableCell wcHCode = new TableCell();
            wcHCode.Text = "Course Code";
            wcHCode.Font.Bold = true;
            wcHCode.HorizontalAlign = HorizontalAlign.Center;
            tblWaivedCourseHeader.Controls.Add(wcHCode);

            TableCell wcTitle = new TableCell();
            wcTitle.Text = "Course Title";
            wcTitle.Font.Bold = true;
            tblWaivedCourseHeader.Controls.Add(wcTitle);

            TableCell wcCreditHours = new TableCell();
            wcCreditHours.Text = "Credit Hours";
            wcCreditHours.Font.Bold = true;
            wcCreditHours.HorizontalAlign = HorizontalAlign.Center;
            tblWaivedCourseHeader.Controls.Add(wcCreditHours);

            int wcIndex = 0;
            foreach (DataRow row in ds.Tables["WaivedCourse"].Rows)
            {
                TableRow tr = new TableRow();
                if ((wcIndex % 2) == 1)
                    tr.BackColor = System.Drawing.Color.LavenderBlush;

                TableCell tdCode = new TableCell();
                tdCode.Text = row["COURSECODE"].ToString();
                tdCode.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdCode);

                TableCell tdTitle = new TableCell();
                tdTitle.Text = row["CNAME"].ToString();
                tr.Controls.Add(tdTitle);

                TableCell tdCreditHours = new TableCell();
                tdCreditHours.Text = row["CREDIT"].ToString();
                WvCH = WvCH + Convert.ToDouble(row["CREDIT"].ToString());
                tr.Controls.Add(tdCreditHours);

                wcIndex++;
                tblWaivedCourse.Controls.Add(tr);
            }

            TableRow trWCH = new TableRow();
            tblWaivedCourse.Controls.Add(trWCH);
            TableCell tdWCH = new TableCell();
            tdWCH.ColumnSpan = 4;
            tdWCH.Font.Bold = true;
            tdWCH.HorizontalAlign = HorizontalAlign.Right;
            tdWCH.Text = "Total credit hours waived:" + Math.Round(WvCH, 2, MidpointRounding.AwayFromZero);
            trWCH.Controls.Add(tdWCH);

            PlaceHolder_gradeSheet.Controls.Add(tblWaivedCourse);

            PlaceHolder_gradeSheet.Controls.Add(new LiteralControl("<br/>&nbsp;"));
        }


        


        #endregion



        Table tbl = new Table();
        PlaceHolder_gradeSheet.Controls.Add(tbl);

        string clearence = "";
        string msg = "";
        bool flag = false;
        string visible = "";

        string P_Year = "", prevSem = "", R_Sem = "", R_Year = "";


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

        foreach (DataRow drS in ds.Tables["registration"].Rows)
        {

            ds.Merge(new student_webService().get_semester_GradeSheetNew(drS["regkey"].ToString()));
            if (ds.Tables["gradeSheet"].Rows.Count == 0)
            {
                continue;
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



            TableRow trSelesterHeader = new TableRow();
            tbl.Controls.Add(trSelesterHeader);

            TableCell tdSemesterHeader = new TableCell();
            tdSemesterHeader.Font.Bold = true;
            tdSemesterHeader.ColumnSpan = 5;
            tdSemesterHeader.Text = "Semester: " + new cls_tools().get_word_semester(drS["semester"].ToString()) + ", " + drS["year"].ToString();
            trSelesterHeader.Controls.Add(tdSemesterHeader);

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

            TableCell tdHGrade = new TableCell();
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
            double creditHrs = 0;
            double tCrditHrs = 0, TcrdtHrs1 = 0, tEarnedPoint=0;



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
                    tdCreditHours.Text = "" + Math.Round(creditHrs, 2) + "";
                else
                    tdCreditHours.Text = "" + Math.Round(creditHrs, 2) + ".0";



                tdCreditHours.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdCreditHours);

                TableCell tdGrade = new TableCell();

                if (!flag)
                {
                    if (dr["ggrade2"].ToString() == "A" || dr["ggrade2"].ToString() == "B" || dr["ggrade2"].ToString() == "C" || dr["ggrade2"].ToString() == "D" || dr["ggrade2"].ToString() == "F")
                        tdGrade.Text = dr["ggrade2"].ToString() + "&nbsp;";
                    else
                        tdGrade.Text = dr["ggrade2"].ToString();
                }
                else
                {
                    tdGrade.Text = "I";
                }
                tdGrade.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdGrade);




                TableCell tdGradePoint = new TableCell();
                if (!flag)
                {
                    if (Convert.ToDouble(dr["gpoint"].ToString()) == 1 || Convert.ToDouble(dr["gpoint"].ToString()) == 2 || Convert.ToDouble(dr["gpoint"].ToString()) == 3 || Convert.ToDouble(dr["gpoint"].ToString()) == 4 || Convert.ToDouble(dr["gpoint"].ToString()) == 0)
                        tdGradePoint.Text = "" + Math.Round(Convert.ToDouble(dr["gpoint"].ToString()), 2) + ".0";
                    else
                        tdGradePoint.Text = "" + Math.Round(Convert.ToDouble(dr["gpoint"].ToString()), 2);// +".0";

                    
                    if (dr["ggrade2"].ToString() != "I" && dr["ggrade2"].ToString() != "W")
                        tgp += Convert.ToDouble(dr["gpoint"].ToString()) * creditHrs;
                }
                else
                {
                    tdGradePoint.Text = "0";
                }
                tdGradePoint.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdGradePoint);




                TableCell tdEarnedPoint = new TableCell();
                if (!flag)
                {

                    if (Convert.ToDouble(dr["gpoint"].ToString()) == 1 || Convert.ToDouble(dr["gpoint"].ToString()) == 2 || Convert.ToDouble(dr["gpoint"].ToString()) == 3 || Convert.ToDouble(dr["gpoint"].ToString()) == 4 || Convert.ToDouble(dr["gpoint"].ToString()) == 0)
                        tdEarnedPoint.Text = "" + Math.Round(Convert.ToDouble(dr["gpoint"].ToString()) * Convert.ToDouble(dr["chours"].ToString()), 2) + ".0";
                    else
                        tdEarnedPoint.Text = "" + Math.Round(Convert.ToDouble(dr["gpoint"].ToString()) * Convert.ToDouble(dr["chours"].ToString()), 2);// +".0";
                   


                    if (dr["ggrade2"].ToString() != "I" && dr["ggrade2"].ToString() != "W")
                        tEarnedPoint +=Convert.ToDouble(dr["gpoint"].ToString()) * Convert.ToDouble(dr["chours"].ToString()); //Convert.ToDouble(tdEarnedPoint.Text);

                   
                }
                else
                {
                    tdEarnedPoint.Text = "0";
                }
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
            tdGradePointE.Text = "SGPA:" + Math.Round((tgp / tCrditHrs), 2, MidpointRounding.AwayFromZero);
            trE.Controls.Add(tdGradePointE);



            TableCell tdEarnedPointE = new TableCell();
            tdEarnedPointE.Text = Convert.ToString(tEarnedPoint) ;           
            tdEarnedPointE.Font.Bold = true;
            tdEarnedPointE.HorizontalAlign = HorizontalAlign.Center;
            trE.Controls.Add(tdEarnedPointE);

            




            //*************** start new ************

            DataSet Crds = new DataSet();
            Crds.Merge(new staff_webService().get_CGPA_CH_semesterupto(sid, drS["year"].ToString(), drS["semester"].ToString()));

            foreach (DataRow dr1 in Crds.Tables["CGPList"].Rows)
            {
                if (dr1["CHRS"].ToString() != "" || dr1["CGPA"].ToString() != "")
                {
                    CGPA = Convert.ToDouble(dr1["CGPA"].ToString());
                    TcrdtHrs1 = Convert.ToDouble(dr1["CHRS"].ToString());

                }
            }



            TableRow trCGPA = new TableRow();
            tbl.Controls.Add(trCGPA);
            TableCell tdCGPA = new TableCell();
            tdCGPA.ColumnSpan = 5;
            tdCGPA.Font.Bold = true;
            tdCGPA.ForeColor = System.Drawing.Color.Crimson;
            tdCGPA.HorizontalAlign = HorizontalAlign.Right;
            tdCGPA.Text = "CGPA:" + Math.Round(CGPA, 2, MidpointRounding.AwayFromZero); trCGPA.Controls.Add(tdCGPA);
            


            TableRow trCH = new TableRow();
            tbl.Controls.Add(trCH);
            TableCell tdCH = new TableCell();
            tdCH.Font.Bold = true;
            tdCH.ColumnSpan = 4;
            // tdCH.ForeColor = System.Drawing.Color.Crimson;
            tdCH.HorizontalAlign = HorizontalAlign.Right;
            tdCH.Text = "Total Credit Hour Completed:" + Math.Round(TcrdtHrs1, 2, MidpointRounding.AwayFromZero);
            trCH.Controls.Add(tdCH);


            TableRow trEP = new TableRow();
            tbl.Controls.Add(trEP);
            TableCell tdEP = new TableCell();
            tdEP.Font.Bold = true;
            tdEP.ColumnSpan = 4;
            tdEP.ForeColor = System.Drawing.Color.Crimson;
            tdEP.HorizontalAlign = HorizontalAlign.Right;
            tdEP.Text = "Total GP Earned:" + Math.Round(TerndPnt, 2, MidpointRounding.AwayFromZero);
            trEP.Controls.Add(tdEP);
            tdEP.Visible = false;




            TableRow trBlank = new TableRow();
            tbl.Controls.Add(trBlank);

            TableCell tdBlank = new TableCell();
            tdBlank.Text = "&nbsp;";
            trBlank.Controls.Add(tdBlank);

            ds.Tables["gradeSheet"].Clear();
        }








        ///************************start table******************
        ///
        double AttemptedCH = 0, RequiredCH = 0;

        DataSet RequiredCreditH = new DataSet();
        RequiredCreditH.Merge(new staff_webService().getRequiredCH(sid));
        foreach (DataRow drR in RequiredCreditH.Tables["RequiredCH"].Rows)
        {
            RequiredCH = Convert.ToDouble(drR["reqCH"].ToString());
        }


        DataSet AttemptCreditH = new DataSet();
        AttemptCreditH.Merge(new staff_webService().getAttemptCH(sid));
        foreach (DataRow dr1 in AttemptCreditH.Tables["AttemptCH"].Rows)
        {
            AttemptedCH = Convert.ToDouble(dr1["attempted"].ToString());
        }





        string FinalMsg = "";
        double FinalCGPA = 0, WaivedCH = 0, TotalCGPA = 0, CompletedCH = 0, TransferCH = 0, HASF = 0, FCHRS = 0, TotalCompledCH = 0, SUB_ID = 0;
        DataSet final = new DataSet();
        final.Merge(new staff_webService().getFinalCGPA(sid));
        foreach (DataRow dr2 in final.Tables["FinalCGPA"].Rows)
        {
            FinalCGPA = Convert.ToDouble(dr2["FINAL_CGPA"].ToString());//d
            // FinalCGPA = Convert.ToDouble(dr2["CGPA"].ToString());//d
            TotalCGPA = Convert.ToDouble(dr2["TCGPA"].ToString());//c1+c2
            CompletedCH = Convert.ToDouble(dr2["COMP_CHRS"].ToString());//c1
            WaivedCH = Convert.ToDouble(dr2["WCHRS"].ToString());//c2
            TransferCH = Convert.ToDouble(dr2["TCHRS"].ToString());
            HASF = Convert.ToDouble(dr2["HASF"].ToString());//has  failed course
            FCHRS = Convert.ToDouble(dr2["FCHRS"].ToString());
            SUB_ID = Convert.ToDouble(dr2["SUB_ID"].ToString());

            //CGPA = TerndPnt / TcrdtHrs;
        }



        //double totalGP_withTrns = 0;
        //double totalCH_withTrns = 0;
        //double totalAvg = 0;
        //double CG = 0;


        Table tblTotalCal = new Table();
        PlaceHolder_gradeSheet.Controls.Add(tblTotalCal);



        TableRow trRQH = new TableRow();
        tblTotalCal.Controls.Add(trRQH);
        TableCell tdRQH = new TableCell();
        //tdRQH.Font.Bold = true;
        tdRQH.ColumnSpan = 6;
        tdRQH.ForeColor = System.Drawing.Color.Crimson;
        tdRQH.HorizontalAlign = HorizontalAlign.Right;
        tdRQH.Text = "Required Credit Hour: " + Math.Round(RequiredCH, 2, MidpointRounding.AwayFromZero);
        trRQH.Controls.Add(tdRQH);

        TableRow trATH = new TableRow();
        tblTotalCal.Controls.Add(trATH);
        TableCell tdATH = new TableCell();
        //tdATH.Font.Bold = true;
        tdATH.ColumnSpan = 6;
        tdATH.ForeColor = System.Drawing.Color.Crimson;
        tdATH.HorizontalAlign = HorizontalAlign.Right;
        tdATH.Text = "Attempt Credit Hour: " + Math.Round(AttemptedCH, 2, MidpointRounding.AwayFromZero);
        trATH.Controls.Add(tdATH);


        TableRow trCCH = new TableRow();
        tblTotalCal.Controls.Add(trCCH);
        TableCell tdCCH = new TableCell();
        // tdCCH.Font.Bold = true;
        tdCCH.ColumnSpan = 6;
        tdCCH.ForeColor = System.Drawing.Color.Crimson;
        tdCCH.HorizontalAlign = HorizontalAlign.Right;
        tdCCH.Text = "Completed Credit Hour: " + Math.Round(CompletedCH, 2, MidpointRounding.AwayFromZero);
        trCCH.Controls.Add(tdCCH);





        TableRow trTRH = new TableRow();
        tblTotalCal.Controls.Add(trTRH);
        TableCell tdTRH = new TableCell();
        //tdTRH.Font.Bold = true;
        tdTRH.ColumnSpan = 6;
        tdTRH.ForeColor = System.Drawing.Color.Crimson;
        tdTRH.HorizontalAlign = HorizontalAlign.Right;
        double Tranfer_Waived = 0;
        Tranfer_Waived = WaivedCH + TransferCH;
        tdTRH.Text = "Transfer/Waived Credit Hours: " + Math.Round(Tranfer_Waived, 2, MidpointRounding.AwayFromZero);
        trTRH.Controls.Add(tdTRH);



        TableRow trTH = new TableRow();
        tblTotalCal.Controls.Add(trTH);
        TableCell tdTH = new TableCell();
        tdTH.Font.Bold = true;
        tdTH.ColumnSpan = 6;
        tdTH.ForeColor = System.Drawing.Color.Crimson;
        tdTH.HorizontalAlign = HorizontalAlign.Right;
        if (Tranfer_Waived > 0)
        {
            TotalCompledCH = Tranfer_Waived + CompletedCH;
        }
        else
        {
            TotalCompledCH = CompletedCH;
        }
        tdTH.Text = "Total Completed Credit Hour: " + Math.Round(TotalCompledCH, 2, MidpointRounding.AwayFromZero);
 
        trTH.Controls.Add(tdTH);


         


        if (SUB_ID >= 161)
        {
            TableRow trTotalCH = new TableRow();
            tblTotalCal.Controls.Add(trTotalCH);
            TableCell tdTotalCH = new TableCell();
            tdTotalCH.Font.Bold = true;
            tdTotalCH.ColumnSpan = 6;
            tdTotalCH.ForeColor = System.Drawing.Color.Crimson;
            tdTotalCH.HorizontalAlign = HorizontalAlign.Right;
            //totalCH_withTrns = TcrdtHrs + TrnsCH;
            tdTotalCH.Text = "CGPA : " + Math.Round(FinalCGPA, 2, MidpointRounding.AwayFromZero); //"CGPA: " + Math.Round(TotalCGPA, 2, MidpointRounding.AwayFromZero);
             

            trTotalCH.Controls.Add(tdTotalCH);
        }
        else
        {
            TableRow trCGP = new TableRow();
            tblTotalCal.Controls.Add(trCGP);
            TableCell tdCGP = new TableCell();
            // tdCGP.Font.Bold = true;
            tdCGP.ColumnSpan = 6;
            tdCGP.ForeColor = System.Drawing.Color.Crimson;
            tdCGP.HorizontalAlign = HorizontalAlign.Right;
            // CG = TerndPnt / TcrdtHrs;
            tdCGP.Text = "CGPA as per Completed Credit Hour: " + Math.Round(FinalCGPA, 2, MidpointRounding.AwayFromZero);
            trCGP.Controls.Add(tdCGP);

            TableRow trTotalCH = new TableRow();
            tblTotalCal.Controls.Add(trTotalCH);
            TableCell tdTotalCH = new TableCell();
            tdTotalCH.Font.Bold = true;
            tdTotalCH.ColumnSpan = 6;
            tdTotalCH.ForeColor = System.Drawing.Color.Crimson;
            tdTotalCH.HorizontalAlign = HorizontalAlign.Right;
            //totalCH_withTrns = TcrdtHrs + TrnsCH;
            tdTotalCH.Text = "CGPA: " + Math.Round(TotalCGPA, 2, MidpointRounding.AwayFromZero);
            trTotalCH.Controls.Add(tdTotalCH);
        }


        TableRow trMSG = new TableRow();
        tblTotalCal.Controls.Add(trMSG);
        TableCell tdMAG = new TableCell();
        //tdMAG.Font.Bold = true;
        tdMAG.ColumnSpan = 6;
        tdMAG.ForeColor = System.Drawing.Color.Crimson;
        tdMAG.HorizontalAlign = HorizontalAlign.Right;

        //double CompletedCH = TransferCH + CompletedCH;
        if (TotalCompledCH < RequiredCH)
        {
            FinalMsg = "has not been fulfilled";
        }
        else
            if (Convert.ToInt32(HASF) == 1)
            {
                FinalMsg = "has not been fulfilled due to incomplete Course";
            }
            else
                if (TotalCGPA < 2.5)
                {
                    FinalMsg = "has not been fulfilled due to insufficient CGPA";
                }
                else
                {
                    FinalMsg = "has been fulfilled";
                }

        tdMAG.Text = "<br/>Degree Requirement " + FinalMsg;
        trMSG.Controls.Add(tdMAG);





        // tdTotalEP.Visible = false;


    }
}
