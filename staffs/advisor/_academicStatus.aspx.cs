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

public partial class staffs_advisor_academicStatus : System.Web.UI.Page
{
    string sid = "";
    student_webService obj_studentWs = new student_webService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ids"] != null)
            sid = Request.QueryString["ids"].ToString();
        else
            Response.Redirect("../_login.aspx");

        loadGradeSheet();
    }

    private void loadGradeSheet()
    {
        PlaceHolder_gradeSheet.Controls.Clear();

        DataSet ds = new DataSet();

        ds.Merge(obj_studentWs.get_allRegistred_semesters_ofA_student(sid));

		#region Transfered Course

        ds.Merge(obj_studentWs.getTransferCourseList(sid));

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

        TableCell trCreditHours = new TableCell();
        trCreditHours.Text = "Credit Hours";
        trCreditHours.Font.Bold = true;
        trCreditHours.HorizontalAlign = HorizontalAlign.Center;
        tblTransferedCourseHeader.Controls.Add(trCreditHours);

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

            TableCell tdCreditHours = new TableCell();
            tdCreditHours.Text = row["CREDIT"].ToString();
            tr.Controls.Add(tdCreditHours);

            trIndex++;
            tblTransferedCourse.Controls.Add(tr);
        }
        PlaceHolder_gradeSheet.Controls.Add(tblTransferedCourse);

        PlaceHolder_gradeSheet.Controls.Add(new LiteralControl("<br/>&nbsp;"));

        #endregion

        #region Waived Course

        ds.Merge(obj_studentWs.getWaiveCourseList(sid));

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
            tr.Controls.Add(tdCreditHours);

            wcIndex++;
            tblWaivedCourse.Controls.Add(tr);
        }
        PlaceHolder_gradeSheet.Controls.Add(tblWaivedCourse);

        PlaceHolder_gradeSheet.Controls.Add(new LiteralControl("<br/>&nbsp;"));

        #endregion

		
        Table tbl = new Table();
        PlaceHolder_gradeSheet.Controls.Add(tbl);

        // lbl_message.Text = ""+new cls_message().getMessage(1);

        foreach (DataRow drS in ds.Tables["registration"].Rows)
        {


            ds.Merge(new student_webService().get_semester_GradeSheet(drS["regkey"].ToString()));
            if (ds.Tables["gradeSheet"].Rows.Count == 0)
            {
                //       lbl_message.Text = "" + new cls_message().getMessage(9);
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

            int i = 0;
            double tgp = 0;
            double creditHrs = 0;
            double tCrditHrs = 0;


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

                tdCreditHours.Text = "" + Math.Round(creditHrs) + ".0";
                tdCreditHours.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdCreditHours);

                TableCell tdGrade = new TableCell();
                if (dr["ggrade2"].ToString() == "A" || dr["ggrade2"].ToString() == "B" || dr["ggrade2"].ToString() == "C" || dr["ggrade2"].ToString() == "D" || dr["ggrade2"].ToString() == "F")
                    tdGrade.Text = dr["ggrade2"].ToString() + "&nbsp;";
                else
                    tdGrade.Text = dr["ggrade2"].ToString();
                tdGrade.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdGrade);

                TableCell tdGradePoint = new TableCell();
                if (Convert.ToDouble(dr["gpoint"].ToString()) == 1 || Convert.ToDouble(dr["gpoint"].ToString()) == 2 || Convert.ToDouble(dr["gpoint"].ToString()) == 3 || Convert.ToDouble(dr["gpoint"].ToString()) == 4 || Convert.ToDouble(dr["gpoint"].ToString()) == 0)
                    tdGradePoint.Text = "" + Math.Round(Convert.ToDouble(dr["gpoint"].ToString()), 2) + ".0";
                else
                    tdGradePoint.Text = "" + Math.Round(Convert.ToDouble(dr["gpoint"].ToString()), 2);// +".0";

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
            tdGradePointE.Text = "SGPA:" + Math.Round((tgp / tCrditHrs), 2);
            trE.Controls.Add(tdGradePointE);

            TableRow trCGPA = new TableRow();
            tbl.Controls.Add(trCGPA);
            TableCell tdCGPA = new TableCell();
            tdCGPA.ColumnSpan = 5;
            tdCGPA.Font.Bold = true;
            tdCGPA.ForeColor = System.Drawing.Color.Crimson;
            tdCGPA.HorizontalAlign = HorizontalAlign.Right;
            tdCGPA.Text = "CGPA:" + Math.Round(new staff_webService().get_CGPA_upto_semester(sid, drS["year"].ToString(), drS["semester"].ToString()), 2);
            trCGPA.Controls.Add(tdCGPA);



            TableRow trBlank = new TableRow();
            tbl.Controls.Add(trBlank);

            TableCell tdBlank = new TableCell();
            tdBlank.Text = "&nbsp;";
            trBlank.Controls.Add(tdBlank);

            ds.Tables["gradeSheet"].Clear();
        }

    }
}
