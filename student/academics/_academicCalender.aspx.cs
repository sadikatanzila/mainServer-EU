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

public partial class student_academics_academicCalender : System.Web.UI.Page
{   
    cls_tools obj_tools = new cls_tools();
    string sem = "";
    string year = "";
    string sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
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


        if (!IsPostBack)
        {
            //  load_Faculty();
            DataSet Uds = new DataSet();
            Uds.Merge(new admin_webService().get_LatestCalenderSearch());
            foreach (DataRow dr in Uds.Tables["Calender"].Rows)
            {
                year = dr["Year"].ToString();
                sem = dr["Semester"].ToString();
            }

            load_calender_latest(sem, year);
            load_calenderHolidays_latest(sem, year);

        }
      
    }

    private void load_calender_latest(string semester, String Year)
    {
        string semister = "";
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_CalenderSearch(Year, semester));
        if (Convert.ToInt32(ds.Tables["CalenderList"].Rows.Count.ToString()) > 0)
        {
            GridView_ClenderList.DataSource = ds;
            GridView_ClenderList.DataMember = "CalenderList";
            GridView_ClenderList.DataBind();

            if (Convert.ToInt32(semester) == 1)
            {
                semister = "Spring";
            }
            else
                if (Convert.ToInt32(semester) == 2)
                {
                    semister = "Summer";
                }
                else
                    if (Convert.ToInt32(semester) == 3)
                    {
                        semister = "Fall";
                    }
            lbl_message.Text = "Academic Calendar of " + semister + ", " + Year;
            lblCalender.Text = "Academic Calendar";
        }
        else
        {
            lbl_message.Text = "No data Found";
            lblCalender.Text = "";
        }
        //  if(ds.Tables.Count.r

    }

    private void load_calenderHolidays_latest(string semester, String Year)
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_CalenderHolidaySearch(Year, semester));
        if (Convert.ToInt32(ds.Tables["CalenderHolidayList"].Rows.Count.ToString()) > 0)
        {
            GridView_HolidayList.DataSource = ds;
            GridView_HolidayList.DataMember = "CalenderHolidayList";
            GridView_HolidayList.DataBind();

            lblHoliday.Text = "Holiday";
        }
        else
        {
            lblHoliday.Text = "No data Found";
        }


    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (cmb_semester.SelectedValue.ToString() != null && txt_year.Text != "")
        {
            load_calender(cmb_semester.SelectedValue.ToString(), txt_year.Text);
            load_calenderHolidays(cmb_semester.SelectedValue.ToString(), txt_year.Text);

        }
        else
        {
            lbl_message.Text = "Please enter your necessary Year & Semester";
        }
    }

    private void load_calender(string semester, String Year)
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_CalenderSearch(txt_year.Text, cmb_semester.SelectedValue.ToString()));
        if (Convert.ToInt32(ds.Tables["CalenderList"].Rows.Count.ToString()) > 0)
        {
            GridView_ClenderList.DataSource = ds;
            GridView_ClenderList.DataMember = "CalenderList";
            GridView_ClenderList.DataBind();

            lbl_message.Text = "Academic Calendar of " + cmb_semester.SelectedItem.ToString() + ", " + txt_year.Text;
            lblCalender.Text = "Academic Calendar";
            GridView_ClenderList.Visible = true;
        }
        else
        {
            lbl_message.Text = "No data Found";
            GridView_ClenderList.Visible = false;
            lblCalender.Text = "";
        }
        //  if(ds.Tables.Count.r

    }

    private void load_calenderHolidays(string semester, String Year)
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_CalenderHolidaySearch(txt_year.Text, cmb_semester.SelectedValue.ToString()));
        if (Convert.ToInt32(ds.Tables["CalenderHolidayList"].Rows.Count.ToString()) > 0)
        {
            GridView_HolidayList.DataSource = ds;
            GridView_HolidayList.DataMember = "CalenderHolidayList";
            GridView_HolidayList.DataBind();

            lblHoliday.Text = "Holiday";
            GridView_HolidayList.Visible = true;
        }
        else
        {
            lblHoliday.Text = "No data Found";
            GridView_HolidayList.Visible = false;
        }


    }

    
}
