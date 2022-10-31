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

public partial class staffs_academic_calender : System.Web.UI.Page
{

    cls_tools obj_tools = new cls_tools();
    string sem = "";
    string year = "";
    string sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {   

        //DataSet ds = new DataSet();
        //ds.Merge(new admin_webService().get_semester_schedule());
        //DateTime dt = DateTime.Today;

        //foreach (DataRow dr in ds.Tables["SEMESTER"].Rows)
        //{
        //    if (Math.Abs(Convert.ToInt32(dr["STMONTH"].ToString()) - Convert.ToInt32(dr["ENDMONTH"].ToString())) > 3)
        //    {
        //        if ((dt.Month >= Convert.ToInt32(dr["STMONTH"].ToString())) && (dt.Month <= Convert.ToInt32(dr["ENDMONTH"].ToString()) || (Convert.ToInt32(dr["ENDMONTH"].ToString()) - 4) <= 0))
        //            sem = dr["SEMCODE"].ToString();
        //    }

        //}
        //year = "" + dt.Year;
        new cls_tools().get_current_sem_year(ref sem, ref year);
        PlaceHolder1.Controls.Clear();
        PlaceHolder2.Controls.Clear();
        PlaceHolder3.Controls.Clear();


        load_calender();
        load_calender_holidays();
        load_newSemester_starting();
    }

    private void load_calender()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_academic_calender_forA_semester(sem, year));
        lbl_title.Text = "" + new cls_tools().get_word_semester(sem) + " semester " + year;

        for (int i = 0; i < ds.Tables["WEB_ACADEMIC_CALENDER"].Rows.Count; i++)
            if (ds.Tables["WEB_ACADEMIC_CALENDER"].Rows[i]["CTRL"].ToString() == "0")
                ds.Tables["WEB_ACADEMIC_CALENDER"].Rows.RemoveAt(i--);

        Table tbl = new Table();
        PlaceHolder1.Controls.Clear();
        PlaceHolder1.Controls.Add(tbl);

        foreach (DataRow dr in ds.Tables["WEB_ACADEMIC_CALENDER"].Rows)
        {
            TableRow tr = new TableRow();
            tbl.Controls.Add(tr);
            tbl.Width = new Unit("100%");
            tr.Height = new Unit(20);


            TableCell tdTitle = new TableCell();
            tdTitle.Text = dr["EVENT"].ToString();
            tdTitle.BorderColor = System.Drawing.Color.FromName("inactivecaptiontext");
            tdTitle.BorderWidth = new Unit(1);
            tdTitle.Width = new Unit("50%");
            tdTitle.ForeColor = System.Drawing.Color.DodgerBlue;
            tdTitle.Font.Bold = true;
            tr.Controls.Add(tdTitle);

            TableCell tdBlank = new TableCell();
            tdBlank.Width = new Unit(10);
            // tdBlank.BorderColor = System.Drawing.Color.FromName("inactivecaptiontext");
            // tdBlank.BorderWidth = new Unit(1);
            tdBlank.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(tdBlank);

            Image img = new Image();
            tdBlank.Controls.Add(img);
            img.ImageUrl = "~/images/arrow3.gif";


            TableCell tddate = new TableCell();
            tddate.BorderColor = System.Drawing.Color.FromName("inactivecaptiontext");
            tddate.BorderWidth = new Unit(1);
            tddate.Font.Bold = true;


            if (dr["FROM_DATE"].ToString() == dr["to_DATE"].ToString())
            {
                tddate.Text = "" + obj_tools.get_academic_calender_formateDate(dr["FROM_DATE"].ToString(), dr["to_DATE"].ToString());
            }
            else
                tddate.Text = "" + obj_tools.get_academic_calender_formateDate(dr["FROM_DATE"].ToString(), dr["to_DATE"].ToString());

            if (!string.IsNullOrEmpty(dr["COMMENTS"].ToString()))
                tddate.Text += " <span style=\"font-size: 7pt;color:DodgerBlue; \">" + dr["COMMENTS"].ToString() + "</span>";

            tr.Controls.Add(tddate);
        }
    }

    private void load_calender_holidays()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_academic_holidays_forA_semester(sem, year));
        lbl_title.Text = "" + new cls_tools().get_word_semester(sem) + " semester " + year;

        for (int i = 0; i < ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Rows.Count; i++)
            if (ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Rows[i]["CTRL"].ToString() == "0")
                ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Rows.RemoveAt(i--);

        Table tbl = new Table();
        PlaceHolder2.Controls.Clear();
        PlaceHolder2.Controls.Add(tbl);

        foreach (DataRow dr in ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Rows)
        {
            TableRow tr = new TableRow();
            tbl.Controls.Add(tr);
            tbl.Width = new Unit("100%");


            TableCell tdTitle = new TableCell();
            tdTitle.Text = dr["DAY_TITLE"].ToString();
            tdTitle.BorderColor = System.Drawing.Color.FromName("inactivecaptiontext");
            tdTitle.BorderWidth = new Unit(1);
            tdTitle.Width = new Unit("50%");
            tdTitle.ForeColor = System.Drawing.Color.DodgerBlue;
            tdTitle.Font.Bold = true;
            tr.Controls.Add(tdTitle);

            TableCell tdBlank = new TableCell();
            tdBlank.Width = new Unit(10);
            tdBlank.Text = ":";
            // tdBlank.BorderWidth = new Unit(1);
            tdBlank.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(tdBlank);

            //Image img = new Image();
            //tdBlank.Controls.Add(img);
            //img.ImageUrl = "~/images/arrow3.gif";


            TableCell tddate = new TableCell();
            tddate.BorderColor = System.Drawing.Color.FromName("inactivecaptiontext");
            tddate.BorderWidth = new Unit(1);
            tddate.Font.Bold = true;


            if (dr["FROM_DATE"].ToString() == dr["to_DATE"].ToString())
            {
                tddate.Text = "" + obj_tools.get_academic_calender_formateDate(dr["FROM_DATE"].ToString(), dr["to_DATE"].ToString());
            }
            else
                tddate.Text = "" + obj_tools.get_academic_calender_formateDate(dr["FROM_DATE"].ToString(), dr["to_DATE"].ToString());

            if (!string.IsNullOrEmpty(dr["COMMENTS"].ToString()))
                tddate.Text += " <span style=\"font-size: 7pt;color:DodgerBlue; \">" + dr["COMMENTS"].ToString() + "</span>";

            tr.Controls.Add(tddate);
        }
    }

    private void load_newSemester_starting()
    {
        if (sem == "3")
        {
            sem = "1";
            year = "" + (Convert.ToInt32(year) + 1);
        }
        else
            sem = "" + (Convert.ToInt32("0" + sem) + 1);

        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_custom_academic_calender_forA_semester(sem, year));

        for (int i = 0; i < ds.Tables["WEB_ACADEMIC_CALENDER"].Rows.Count; i++)
            if (ds.Tables["WEB_ACADEMIC_CALENDER"].Rows[i]["CTRL"].ToString() == "0")
                ds.Tables["WEB_ACADEMIC_CALENDER"].Rows.RemoveAt(i--);

        Table tbl = new Table();
        PlaceHolder3.Controls.Clear();
        PlaceHolder3.Controls.Add(tbl);

        foreach (DataRow dr in ds.Tables["WEB_ACADEMIC_CALENDER"].Rows)
        {
            TableRow tr = new TableRow();
            tbl.Controls.Add(tr);
            tbl.Width = new Unit("100%");
            tr.Height = new Unit(20);


            TableCell tdTitle = new TableCell();
            tdTitle.Text = dr["EVENT"].ToString();
            tdTitle.BorderColor = System.Drawing.Color.Red;
            tdTitle.Font.Italic = true;
            tdTitle.BorderWidth = new Unit(1);
            tdTitle.Width = new Unit("50%");
            tdTitle.ForeColor = System.Drawing.Color.DodgerBlue;
            tdTitle.Font.Bold = true;
            tr.Controls.Add(tdTitle);

            TableCell tdBlank = new TableCell();
            tdBlank.Width = new Unit(10);
            tdBlank.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(tdBlank);
            Image img = new Image();
            tdBlank.Controls.Add(img);
            img.ImageUrl = "~/images/arrow3.gif";


            TableCell tddate = new TableCell();
            tddate.BorderColor = System.Drawing.Color.Red;
            tddate.BorderWidth = new Unit(1);
            tddate.Font.Bold = true;


            if (dr["FROM_DATE"].ToString() == dr["to_DATE"].ToString())
            {
                tddate.Text = "" + obj_tools.get_academic_calender_formateDate(dr["FROM_DATE"].ToString(), dr["to_DATE"].ToString());
            }
            else
                tddate.Text = "" + obj_tools.get_academic_calender_formateDate(dr["FROM_DATE"].ToString(), dr["to_DATE"].ToString());

            if (!string.IsNullOrEmpty(dr["COMMENTS"].ToString()))
                tddate.Text += " <span style=\"font-size: 7pt;color:DodgerBlue; \">" + dr["COMMENTS"].ToString() + "</span>";

            tr.Controls.Add(tddate);
        }
    }

}
