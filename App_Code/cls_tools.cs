using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for cls_tools
/// </summary>
public class cls_tools
{
    public static int getDiffIndays(DateTime gdate, DateTime pdate)
    {
        TimeSpan tsp = gdate - pdate;        
        return (tsp.Days-1);
    }
    public static double finePlusMinus=500;
    public static double fineMultiplier = .00125;//.0025;
    public String[] grpPrev = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "Regular", "Executive", "Regular & Executive", "Waiting Group" };

    public String[] grp = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "Regular & Executive", "Waiting Group" };

    public String[] grpNew = { "FIN", "MKT", "HRM", "ACT" };
    
    public String[] days = { "Select", "SAT", "SUN", "MON", "TUES", "WED", "THUS", "FRI" };

    public String[] daysPrev = { "Select", "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
    
    public String[] hours ={ "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"};
    public String[] min ={ "0","1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15",
                           "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26","27", "28", "29", "30",
                           "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45",
                           "46", "47", "48", "49", "50", "51", "52","53", "54", "55", "56", "57", "58", "59"};

    public cls_tools()
    {
    }

    public string get_word_semester(string semID)
    {
        string sem="";

        if (semID == "1")
            sem="Spring";
        else if (semID == "2")
            sem="Summer";
        else if (semID == "3")
            sem="Fall";
        return sem;
    }
    public string get_user_formateDate(string dbDt)
    {
        if (String.IsNullOrEmpty(dbDt))
            return "";

        DateTime dt = new DateTime();
        dt =Convert.ToDateTime(dbDt);
        string date = "";
        
        date = "" + word_stringMonth(dt.Month);
        date += " " + dt.Day;
        date += ", " + dt.Year;

        return date;
    }


    public string get_user_short_formateDate(string dbDt)
    {
        if (String.IsNullOrEmpty(dbDt))
            return "";

        DateTime dt = new DateTime();
        dt = Convert.ToDateTime(dbDt);
        string date = "";

        try
        {
            date = "" + dt.Day;
            date += " " + word_stringMonth(dt.Month).Substring(0, 3);
            date += " " + dt.Year.ToString().Substring(2, 2);
        }
        catch
        {
        }

        return date;
    }


    public string get_academic_calender_formateDate(string fromDt, string toDt )
    {
        string date = "";
        if (String.IsNullOrEmpty(fromDt))
            return "";

        DateTime dt = new DateTime();
        dt = Convert.ToDateTime(fromDt);
        
        DateTime dt2 = new DateTime();
        dt2 = Convert.ToDateTime(toDt);

        if (dt == dt2)
        {
            date = " " + word_stringMonth(dt.Month) + " " + dt.Day + " (" + dt.ToLongDateString().Split(',')[0] + ")"; ;
        } 
        else
            date = " " + word_stringMonth(dt.Month) + " " + dt.Day + "-" + word_stringMonth(dt2.Month) + " " + dt2.Day + " (" + dt.ToLongDateString().Split(',')[0].Substring(0, 3) + "-" + dt2.ToLongDateString().Split(',')[0].Substring(0, 3) + ")"; ;
 
        return date;
    }


    public DataTable get_gradingPolicyTable()
    {        
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_gradingPolicy("1"));
        ds.Merge(new staff_webService().get_gradingPolicy("2"));
        ds.Tables.Add("GRADINGPOLICY");
        ds.Tables["GRADINGPOLICY"].Columns.Add("GGRADE");
        ds.Tables["GRADINGPOLICY"].Columns.Add("GGRADE2");
        ds.Tables["GRADINGPOLICY"].Columns.Add("GPOINT");

        foreach (DataRow dr1 in ds.Tables["GRADINGPOLICY1"].Rows)
        {
            foreach (DataRow dr2 in ds.Tables["GRADINGPOLICY2"].Rows)
            {
                if (dr1["GRADEPOINT"].ToString() != "0.00" )
                {
                    if (dr1["GRADEPOINT"].ToString() == dr2["GRADEPOINT"].ToString())
                    {
                        DataRow dr = ds.Tables["GRADINGPOLICY"].NewRow();
                        dr["GGRADE"] = dr1["GRADETYPE"].ToString();
                        dr["GGRADE2"] = dr2["GRADETYPE"].ToString();
                        dr["GPOINT"] = dr1["GRADEPOINT"].ToString();
                        ds.Tables["GRADINGPOLICY"].Rows.Add(dr);
                        break;
                    }
                }
            }
        }

        DataRow drw = ds.Tables["GRADINGPOLICY"].NewRow();
        drw["GGRADE"] = "W" ;
        drw["GGRADE2"] =  "W" ;
        drw["GPOINT"] =  "0" ;
        ds.Tables["GRADINGPOLICY"].Rows.Add(drw);

        DataRow drr = ds.Tables["GRADINGPOLICY"].NewRow();
        drr["GGRADE"] = "R";
        drr["GGRADE2"] ="R";
        drr["GPOINT"] = "0";
        ds.Tables["GRADINGPOLICY"].Rows.Add(drr);

        DataRow drI = ds.Tables["GRADINGPOLICY"].NewRow();
        drI["GGRADE"] = "I";
        drI["GGRADE2"] = "I";
        drI["GPOINT"] = "0";
        ds.Tables["GRADINGPOLICY"].Rows.Add(drI);

        DataRow drF = ds.Tables["GRADINGPOLICY"].NewRow();
        drF["GGRADE"] = "F";
        drF["GGRADE2"] = "F";
        drF["GPOINT"] = "0";
        ds.Tables["GRADINGPOLICY"].Rows.Add(drF);

        return ds.Tables["GRADINGPOLICY"];
    }


    public string get_department_code(string sid)
    {
        String depCode = "";
        DataSet ds = new DataSet();
        ds.Merge(new Dts().get_viewData("Select * from DEPARTMENTINCOLLEGE where DEPID='" + sid.Substring(3, 2) + "' ", "DEPARTMENTINCOLLEGE"));
        if (ds.Tables["DEPARTMENTINCOLLEGE"].Rows.Count > 0)
        {
            depCode = ds.Tables["DEPARTMENTINCOLLEGE"].Rows[0]["DEPCODE"].ToString();
        }
        return depCode;
    }

    public string get_sem_text(string digit)
    {
        string text = "";
        if (digit == "1")
            text = "1st";
        else if (digit == "2")
            text = "2nd";
        else if (digit == "3")
            text = "3rd";
        else if (digit == "4")
            text = "4th";
        else if (digit == "5")
            text = "5th";
        else if (digit == "6")
            text = "6th";
        else if (digit == "7")
            text = "7th";
        else if (digit == "8")
            text = "8th";
        else if (digit == "9")
            text = "9th";
        else if (digit == "10")
            text = "10th";
        else if (digit == "11")
            text = "11th";
        else if (digit == "12")
            text = "12th";
        return text;
    }

    public string get_formate_string(String str)
    {
        return String.Format("{0:d}",str);
    }

    public bool send_email(string mailto, string mailcc, string mailfrom, string mailsubject, string mailbody)
    {
        bool status = true;

        System.Web.Mail.MailMessage email = new System.Web.Mail.MailMessage();
        email.BodyFormat = System.Web.Mail.MailFormat.Html;
        email.From = mailfrom;
        email.To = mailto;
        email.Subject = mailsubject;
        email.Body = mailbody;
        email.Cc = mailcc;
        // System.Web.Mail.SmtpMail.SmtpServer = "smtp.fasthosts.co.uk";        
        System.Web.Mail.SmtpMail.SmtpServer = "localhost";
                           
        try
        {
            System.Web.Mail.SmtpMail.Send(email);
        }
        catch (Exception er) { status = false; }

        return status;
    }  

    public void get_current_sem_year(ref String sem, ref String year)
    {

        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_semester_schedule());
        DateTime dt = DateTime.Today;

        foreach (DataRow dr in ds.Tables["SEMESTER"].Rows)
        {
            int difference = Convert.ToInt32(dr["ENDMONTH"].ToString()) - Convert.ToInt32(dr["STMONTH"].ToString());
            if (difference > 0)
            {
                if (
                    (dt.Month >= Convert.ToInt32(dr["STMONTH"].ToString()))
                    && (dt.Month <= Convert.ToInt32(dr["ENDMONTH"].ToString()))
                    )
                {
                    sem = dr["SEMCODE"].ToString();
                    break;
                }
            }
            else if (difference < 0)
            {
                sem = dr["SEMCODE"].ToString();

                if (
                    dt.Month<=Convert.ToInt32(dr["ENDMONTH"].ToString())
                    //(dt.Month + 12 >= Convert.ToInt32(dr["STMONTH"].ToString()))
                    //&& (dt.Month <= Convert.ToInt32(dr["ENDMONTH"].ToString()))
                    )
                {                   
                    dt=dt.AddYears(-1);
                    break;
                }
            }

        }
        year = "" + dt.Year;
    }
   

    public string get_database_formateDate(DateTime dt)
    {
        string date = "";
        date = "" + dt.Day;
        date += " " + word_stringMonth(dt.Month);
        date += " " + dt.Year;

        return date;
    }


    public string get_database_formateDate_new(DateTime dt)
    {
        string date = "";
        date = "" + dt.Day;
        date += " " + word_stringMonth(dt.Month);
        date += " " + dt.Year;

        return date;
    }

    private string word_stringMonth(int month)
    {
        if (month == 1)
            return "January";
        else if (month == 2)
            return "February";
        else if (month == 3)
            return "March";
        else if (month == 4)
            return "April";
        else if (month == 5)
            return "May";
        else if (month == 6)
            return "June";
        else if (month == 7)
            return "July";
        else if (month == 8)
            return "August";
        else if (month == 9)
            return "September";
        else if (month == 10)
            return "October";
        else if (month == 11)
            return "November";
        else if (month == 12)
            return "December";
        else return "";

    }

}
