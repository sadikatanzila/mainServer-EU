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
using System.Collections.Generic;
using System.Globalization;


public partial class staffs_courses_teacherevaluation_result : System.Web.UI.Page
{
    staff_webService obj_staff = new staff_webService();
    string course_teacherId = "";
    string str_department = "";
    string year = "", Semester = "", SEM_RESULT_PUBLISH = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else if (String.IsNullOrEmpty(Session["user"].ToString()))
            {
                Response.Redirect("../_login.aspx");
            }
        }
        catch (Exception ert) { Response.Redirect("../_login.aspx"); }

        try
        {
            course_teacherId = Request.QueryString["code"].ToString();


            DataSet ds = new DataSet();
            ds.Merge(new admin_webService().getall_offeredCourses_yearSem(course_teacherId));
            if (ds.Tables["WEB_COURSE_TEACHER"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
                {
                    year = dr["Year"].ToString();
                    Semester = dr["Semester"].ToString();

                    DataSet dsChkDate = new DataSet();
                    dsChkDate.Merge(new admin_webService().get_pre_offerigDate(year, Semester));

                    foreach (DataRow dr1 in dsChkDate.Tables["WEB_PRE_OFFERING_DATE"].Rows)
                    {
                        SEM_RESULT_PUBLISH = dr1["SEM_RESULT_PUBLISH"].ToString();
                        DateTime Result_PublishDate = Convert.ToDateTime(SEM_RESULT_PUBLISH);

                        string current = DateTime.Now.ToString("MM/dd/yyyy");
                        DateTime Current_Time = DateTime.ParseExact(current, "MM/dd/yyyy", CultureInfo.CurrentCulture);
                        if (Result_PublishDate <= Current_Time)
                        {
                            lblmessage.Visible = false;
                            pnlEveReport.Visible = true;
                            get_course_teacherInfo();
                            load_listof_argument();
                        }
                        else
                        {
                            pnlEveReport.Visible = false;
                            lblmessage.Text = "Evalution Report will be viewed after the date of Result Publication";
                        }

                    }

                }
            }
            else
            {
                lblmessage.Text = "Teachers Evaluation report of this current year & Semester is not published";
            }
        }
        catch (Exception err) { Response.Redirect("../_login.aspx"); }






    }

    private void get_course_teacherInfo()
    {
        DataSet ds = new DataSet();
        ds.Merge(obj_staff.get_course_teacher_Info(course_teacherId));

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            //lbl_faculty.Text = new student_webService().getF dr["DEPARTMENT"];  

            string sems = "Fall";
            if (Session["sem"].ToString() == "1")
                sems = "Spring";
            else if (Session["sem"].ToString() == "2")
                sems = "Summer";
            lbl_teacherName.Text = dr["STAFF_NAME"].ToString();
            lbl_courseTitle.Text = dr["cname"].ToString();
            lbl_courseCode.Text = dr["coursecode"].ToString();
            lbl_semester.Text = "" + sems + " " + Session["year"];

            str_department = dr["DEPARTMENT"].ToString();

            try
            {
                lbl_faculty.Text = new Dts().get_viewData(" select * from COLLEGE where collegecode='" + dr["DEPARTMENT"] + "' ", "college").Rows[0]["COLLEGENAME"].ToString();
            }
            catch (Exception errr) { }
        }

    }


    private List<double> get_teacher_data()
    {
        DataSet ds_eval = new DataSet();

        string sql = " SELECT COUNT(*)AS total, SUM(s_1) AS s_1,SUM(s_2) AS s_2,SUM(s_3) AS s_3,SUM(s_4) AS s_4,";
        sql += " SUM(s_5) AS s_5,SUM(s_6) AS s_6,SUM(s_7) AS s_7,SUM(s_8) AS s_8,SUM(s_9) AS s_9, ";
        sql += " SUM(s_10) AS s_10,SUM(s_11) AS s_11,SUM(s_12) AS s_12,SUM(s_13) AS s_13,SUM(s_14) AS s_14,SUM(s_15) AS s_15,SUM(s_16) AS s_16 ";
        sql += " FROM WEB_VIEW_EVALUATION ";
        sql += " WHERE COURSE_TEACHER='" + course_teacherId + "'";

        ds_eval.Merge(obj_staff.return_table(sql, "VIEW_EVALUATION_teacher"));

        List<double> data_lst = new List<double>();

        int i = 0;
        foreach (DataRow dr in ds_eval.Tables[0].Rows)
        {

            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
            data_lst.Add(Convert.ToDouble("0" + dr[i++]));
        }

        return data_lst;
    }

    private void load_listof_argument()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_all_active_argument());


        DataSet ds_subjects = new DataSet();
        string sql = " SELECT DISTINCT course_teacher FROM WEB_VIEW_EVALUATION WHERE DEPARTMENT='" + str_department + "' and regkey LIKE '%" + Session["sem"] + Session["year"] + "'";
        ds_subjects.Merge(obj_staff.return_table(sql, "course_teacher"));


        Table tbl = new Table();
        tbl.CellPadding = 0;
        tbl.CellSpacing = 0;
        tbl.Width = new Unit("100%");

        PlaceHolder_data.Controls.Clear();
        PlaceHolder_data.Controls.Add(tbl);

        TableRow trHT = new TableRow();
        tbl.Controls.Add(trHT);

        TableCell td_itemNoHT = new TableCell();
        td_itemNoHT.HorizontalAlign = HorizontalAlign.Center;
        td_itemNoHT.ColumnSpan = 5;
        trHT.Controls.Add(td_itemNoHT);

        TableCell td_itemNoHT2 = new TableCell();
        td_itemNoHT2.HorizontalAlign = HorizontalAlign.Center;
        td_itemNoHT2.Text = "All teachers & All Courses";
        td_itemNoHT2.ColumnSpan = 2;
        td_itemNoHT2.BorderWidth = new Unit(1);
        td_itemNoHT2.Font.Bold = true;
        trHT.Controls.Add(td_itemNoHT2);

        //---------- Header---

        TableRow trH = new TableRow();
        tbl.Controls.Add(trH);

        TableCell td_itemNoH = new TableCell();
        td_itemNoH.Width = new Unit("70");
        td_itemNoH.Height = new Unit(20);
        td_itemNoH.HorizontalAlign = HorizontalAlign.Center;
        td_itemNoH.Text = "Item No.";
        td_itemNoH.BorderWidth = new Unit(1);
        td_itemNoH.Font.Bold = true;
        trH.Controls.Add(td_itemNoH);

        TableCell td_criteriaH = new TableCell();
        td_criteriaH.BorderWidth = new Unit(1);
        td_criteriaH.HorizontalAlign = HorizontalAlign.Center;
        td_criteriaH.Text = "Criteria";
        // td_criteriaH.Width = new Unit(400);
        td_criteriaH.Font.Bold = true;
        trH.Controls.Add(td_criteriaH);

        TableCell td_NoOfRespH = new TableCell();
        td_NoOfRespH.Text = "No of <br> Respondents";
        td_NoOfRespH.BorderWidth = new Unit(1);
        td_NoOfRespH.HorizontalAlign = HorizontalAlign.Center;
        td_NoOfRespH.Font.Bold = true;
        trH.Controls.Add(td_NoOfRespH);

        TableCell td_avgH = new TableCell();
        td_avgH.Text = "Avg";
        td_avgH.Width = new Unit(55);
        td_avgH.BorderWidth = new Unit(1);
        td_avgH.HorizontalAlign = HorizontalAlign.Center;
        td_avgH.Font.Bold = true;
        trH.Controls.Add(td_avgH);

        TableCell td_StDevH = new TableCell();
        td_StDevH.Text = "StDev";
        td_StDevH.Width = new Unit(55);
        td_StDevH.BorderWidth = new Unit(1);
        td_StDevH.HorizontalAlign = HorizontalAlign.Center;
        td_StDevH.Font.Bold = true;
        trH.Controls.Add(td_StDevH);

        TableCell td_AvgAllH = new TableCell();
        td_AvgAllH.Text = "Avg";
        td_AvgAllH.Width = new Unit(55);
        td_AvgAllH.BorderWidth = new Unit(1);
        td_AvgAllH.HorizontalAlign = HorizontalAlign.Center;
        td_AvgAllH.Font.Bold = true;
        trH.Controls.Add(td_AvgAllH);

        TableCell td_stDevH = new TableCell();
        td_stDevH.Text = "StDev";
        td_stDevH.Width = new Unit(55);
        td_stDevH.BorderWidth = new Unit(1);
        td_stDevH.HorizontalAlign = HorizontalAlign.Center;
        td_stDevH.Font.Bold = true;
        trH.Controls.Add(td_stDevH);

        int count = 0;

        List<double> data_lst = get_teacher_data();
        int i = 1;

        List<double> all_eval_lst = get_allteacher_eval_data();

        double avg_t = 0;
        double avg_all = 0;
        int s_count = 1;

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            TableRow tr = new TableRow();
            tbl.Controls.Add(tr);

            TableCell td_itemNo = new TableCell();
            td_itemNo.Text = "" + (++count);
            td_itemNo.Height = new Unit(18);
            td_itemNo.BorderWidth = new Unit(1);
            td_itemNo.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(td_itemNo);

            TableCell td_criteria = new TableCell();
            td_criteria.Text = "" + dr["argument"];
            td_criteria.BorderWidth = new Unit(1);
            tr.Controls.Add(td_criteria);

            TableCell td_NoOfResp = new TableCell();
            td_NoOfResp.Text = "" + Math.Round(data_lst[0], 2);
            td_NoOfResp.BorderWidth = new Unit(1);
            td_NoOfResp.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(td_NoOfResp);

            TableCell td_avg = new TableCell();
            avg_t += Math.Round((data_lst[i] / data_lst[0]), 2);
            td_avg.Text = "" + Math.Round((data_lst[i] / data_lst[0]), 2);
            td_avg.BorderWidth = new Unit(1);
            td_avg.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(td_avg);

            TableCell td_StDev = new TableCell();
            td_StDev.Text = "" + get_std_data(course_teacherId, "s_" + (s_count));
            td_StDev.BorderWidth = new Unit(1);
            td_StDev.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(td_StDev);

            TableCell td_AvgAll = new TableCell();
            avg_all += Math.Round((all_eval_lst[i] / all_eval_lst[0]), 2);
            td_AvgAll.Text = "" + Math.Round((all_eval_lst[i] / all_eval_lst[0]), 2);
            td_AvgAll.BorderWidth = new Unit(1);
            td_AvgAll.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(td_AvgAll);

            TableCell td_stDev = new TableCell();
            td_stDev.Text = "" + get_all_std_data("s_" + (s_count++), ds_subjects);
            td_stDev.BorderWidth = new Unit(1);
            td_stDev.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(td_stDev);

            i++;
        }

        TableRow trBEmpty = new TableRow();
        tbl.Controls.Add(trBEmpty);

        TableCell td_BBEmpty = new TableCell();
        td_BBEmpty.ColumnSpan = 7;
        td_BBEmpty.Height = new Unit(10);
        trBEmpty.Controls.Add(td_BBEmpty);

        TableRow trB = new TableRow();
        tbl.Controls.Add(trB);

        TableCell td_BB = new TableCell();
        trB.Controls.Add(td_BB);

        TableCell td_itemB = new TableCell();
        td_itemB.Text = "Total of averages out of a maximum possible of 75<br /> Average of Standard Deviations out of a maximum possible of 0";
        td_itemB.BorderWidth = new Unit(1);
        td_itemB.HorizontalAlign = HorizontalAlign.Center;
        trB.Controls.Add(td_itemB);

        TableCell td_NOR = new TableCell();
        td_NOR.Text = "";
        td_NOR.BorderWidth = new Unit(1);
        trB.Controls.Add(td_NOR);

        TableCell td_Avvg = new TableCell();
        td_Avvg.Text = "" + avg_t; ;
        td_Avvg.HorizontalAlign = HorizontalAlign.Center;
        td_Avvg.BorderWidth = new Unit(1);
        trB.Controls.Add(td_Avvg);

        TableCell td_NORSt = new TableCell();
        td_NORSt.Text = "";
        td_NORSt.HorizontalAlign = HorizontalAlign.Center;
        td_NORSt.BorderWidth = new Unit(1);
        trB.Controls.Add(td_NORSt);

        TableCell td_NORAAVG = new TableCell();
        td_NORAAVG.Text = "" + avg_all;
        td_NORAAVG.HorizontalAlign = HorizontalAlign.Center;
        td_NORAAVG.BorderWidth = new Unit(1);
        trB.Controls.Add(td_NORAAVG);

        TableCell td_NORASttv = new TableCell();
        td_NORASttv.Text = "";
        td_NORASttv.HorizontalAlign = HorizontalAlign.Center;
        td_NORASttv.BorderWidth = new Unit(1);
        trB.Controls.Add(td_NORASttv);



    }


    private List<double> get_allteacher_eval_data()
    {
        DataSet ds_eval = new DataSet();

        string sql = " SELECT COURSE_TEACHER,COUNT(*)AS total, (SUM(s_1)/COUNT(*))AS s_1, (SUM(s_2)/COUNT(*))AS s_2, (SUM(s_3)/COUNT(*)) AS s_3, (SUM(s_4)/COUNT(*)) AS s_4, " +
                    " (SUM(s_5)/COUNT(*)) AS s_5,(SUM(s_6)/COUNT(*)) AS s_6,(SUM(s_7)/COUNT(*)) AS s_7,(SUM(s_8)/COUNT(*)) AS s_8,(SUM(s_9)/COUNT(*)) AS s_9," +
                    " (SUM(s_10)/COUNT(*))AS s_10,(SUM(s_11)/COUNT(*)) AS s_11,(SUM(s_12)/COUNT(*)) AS s_12,(SUM(s_13)/COUNT(*)) AS s_13,(SUM(s_14)/COUNT(*)) AS s_14," +
                    " (SUM(s_15)/COUNT(*)) AS s_15, (SUM(s_16)/COUNT(*)) AS s_16 " +
                    " FROM WEB_VIEW_EVALUATION " +
                    " WHERE department='02' AND regkey LIKE'%" + Session["sem"] + Session["year"] + "'" +
                    " GROUP BY COURSE_TEACHER ";

        ds_eval.Merge(obj_staff.return_table(sql, "VIEW_EVALUATION_all"));

        double[] evl_data = new double[ds_eval.Tables[0].Rows.Count];
        int i = 0;

        foreach (DataRow dr in ds_eval.Tables[0].Rows)
        {
            i = 0;
            // evl_data[i++] += Convert.ToDouble("0" + dr["total"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_1"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_2"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_3"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_4"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_5"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_6"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_7"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_8"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_9"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_10"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_11"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_12"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_13"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_14"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_15"]);
            evl_data[i++] += Convert.ToDouble("0" + dr["s_16"]);
        }

        List<double> data_lst = new List<double>();

        data_lst.Add(Convert.ToDouble("0" + ds_eval.Tables[0].Rows.Count));

        for (int j = 0; j < evl_data.Length; j++)
        {
            data_lst.Add(Convert.ToDouble("0" + evl_data[j]));
        }

        return data_lst;

    }

    private double get_all_std_data(string argName, DataSet ds)
    {
        double std_sums = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            std_sums += get_std_data(dr["course_teacher"].ToString(), argName);
        }

        return Math.Round((std_sums / ds.Tables[0].Rows.Count), 2);
    }

    private double get_std_data(string c_teaId, string argName)
    {
        String sql = " SELECT  course_teacher, (" + argName + ")-(SELECT SUM(" + argName + ")/COUNT(*) FROM WEB_TEACHER_EVAL_VALUE WHERE course_teacher='" + c_teaId + "') as " + argName + " " +
            " FROM WEB_TEACHER_EVAL_VALUE " +
            " WHERE course_teacher='" + c_teaId + "' ";

        DataSet ds = new DataSet();
        ds.Merge(obj_staff.return_table(sql, "course_teacher"));

        double sums = 0;

        //wireless pass: 22662479

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sums += (Convert.ToDouble("" + dr[argName]) * Convert.ToDouble("" + dr[argName]));
        }

        double noOfItem = ds.Tables[0].Rows.Count - 1;

        double stDev = Math.Round((Math.Sqrt(sums) / Math.Sqrt(noOfItem)), 2);
        if ("" + stDev == "NaN")
            return 0;

        return stDev;
    }

}