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

public partial class student_academics_academic_status : System.Web.UI.Page
{
    student_webService obj_studentWs = new student_webService();
    string sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
       // lbl_message.Text = "";
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
            {
                sid = Session["ctrlId"].ToString();
                Response.Redirect("../_login.aspx");
            }
            else
            {
                sid = Session["ctrlId"].ToString();
            }
        }
        catch (Exception exp) { Response.Redirect("../_login.aspx"); }

        lbl_message.Text = "";
        loadGradeSheet();
    }

    private void loadGradeSheet()
    {
        PlaceHolder_gradeSheet.Controls.Clear();

        DataSet ds = new DataSet();

        ds.Merge(obj_studentWs.get_allRegistred_semesters_ofA_student(sid));

        Table tbl = new Table();
        PlaceHolder_gradeSheet.Controls.Add(tbl);

        string clearence = "";
        string msg = "";
        bool flag = false;

        foreach (DataRow drS in ds.Tables["registration"].Rows)
        {

            ds.Merge(new student_webService().get_semester_GradeSheet(drS["regkey"].ToString()));
            if (ds.Tables["gradeSheet"].Rows.Count == 0)
            {         
                continue;
            }

     /**---------------------  Check for -------------------------------------**/

            //clearence = "" + new student_webService().get_clearence_status(sid + drS["semester"].ToString() + drS["year"].ToString());
            //if (clearence != "-1")
            //{
                flag = false;
                if (Convert.ToInt32("0" + drS["year"].ToString()) >= 2008)
                {
                    clearence = "" + new student_webService().get_clearence_status(sid + drS["semester"].ToString() + drS["year"].ToString(), drS["semester"].ToString(),drS["year"].ToString());
                    string[] stat = clearence.Split('_');
                    //msg = "You have not clear in '" + new cls_tools().get_word_semester(drS["semester"].ToString()) + ", " + drS["year"].ToString() + "' for ";
                    msg="</br>For '" + new cls_tools().get_word_semester(drS["semester"].ToString()) + ", " + drS["year"].ToString() + "'";
                   

                    if (stat[3] != "1")
                    {
                        msg += " Result not yet published";
                        flag = true;
                    }
                    else
                    {
                        msg += " please ";
                        if (stat[0] != "1")
                        {
                            //msg += " accounce";
                            msg += "clear accounce dues";
                            flag = true;
                        }

                        if (stat[1] != "1")
                        {
                            if (flag == false)
                            {
                                //msg += " library";
                                msg += " get library clearence";
                            }
                            else
                            {
                                //msg += ",library";
                                msg += ", get library clearence";
                            }

                            flag = true;
                        }

                        if (flag)
                        {
                            msg += " and contact with IT department";
                        }

                        if (stat[2] != "1")
                        {
                            if (flag == false)
                            {
                                msg += " do teacher evaluation";
                                //msg += " teacher evaluation";
                            }
                            else
                            {
                                //   msg += ",teacher evaluation";
                                msg += ". And do teacher evaluation";
                            }

                            flag = true;
                        }
                    }

                    if (flag == true)
                    {
                       // msg += " please contact with particular department(s).<br>";
                        lbl_message.Text += msg;
                       // clearance_status = false;
                        flag = true;

                    }
                    //else
                    //    ds.Tables["gradeSheet"].Rows.Clear();

                       /* if (flag == true)
                        {
                            ds.Tables["gradeSheet"].Rows.Clear();
                            continue;
                        }*/
                }



                //if (clearence == "3")
                //{
                //    lbl_message.Text += "For '" + new cls_tools().get_word_semester(drS["semester"].ToString()) + ", " + drS["year"].ToString()+"' " + new cls_message().getMessage(18)+"<br>";
                //}
                //else if (clearence == "2")
                //{
                //    lbl_message.Text += "For '" + new cls_tools().get_word_semester(drS["semester"].ToString()) + ", " + drS["year"].ToString() + "' " + new cls_message().getMessage(20) + "<br>";
                //}
                //else if (clearence == "1")
                //{
                //    lbl_message.Text += "For '" + new cls_tools().get_word_semester(drS["semester"].ToString()) + ", " + drS["year"].ToString() + "' " + new cls_message().getMessage(19) + "<br>";
                //}

               
           // }
     /*--------------------------------------------------------*/


            for (int count = 0; count < ds.Tables["gradeSheet"].Rows.Count;count++ )
            {
                DataRow drt = ds.Tables["gradeSheet"].Rows[0];
                for (int j=count+1 ; j < ds.Tables["gradeSheet"].Rows.Count; j++)
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
            tdSemesterHeader.Font.Bold=true;
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
                if ((creditHrs%1)>0)
                    tdCreditHours.Text = "" + Math.Round(creditHrs,2) + "";
                else
                    tdCreditHours.Text = "" + Math.Round(creditHrs,2) + ".0";
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
