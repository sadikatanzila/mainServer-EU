using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CommonFun
/// </summary>
public class CommonFun
{
    //public static void CheckAdmin()
    //{
    //    if (HttpContext.Current.Session["ctrl_admin_Id"] != null && !String.IsNullOrEmpty(HttpContext.Current.Session["ctrl_admin_Id"].ToString()))
    //    {
    //        if (!ConfigurationManager.AppSettings["admins"].Contains(HttpContext.Current.Session["ctrl_admin_Id"].ToString())
    //            && !HttpContext.Current.Request.Url.AbsoluteUri.Contains("_login.aspx"))
    //        {
    //            if (!(HttpContext.Current.Request.Url.AbsoluteUri.Contains("_semester_course_list.aspx")
    //                || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_allocation.aspx")
    //                || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_allocationNew.aspx")
    //                || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_add_student_advisor.aspx")
    //                || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_studentDetails.aspx")
    //                || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_teacherEvalListNew.aspx")
    //                || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_teacheractiveInactive.aspx")
    //                 || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_StudentRoutine.aspx")
    //                ))
    //            {
    //                HttpContext.Current.Response.Redirect("~/admin/_semester_course_list.aspx", false);
    //            }
    //        }
    //    }
    //}
    public static void CheckAdmin()
    {
        if (HttpContext.Current.Session["ctrl_admin_Id"] != null && !String.IsNullOrEmpty(HttpContext.Current.Session["ctrl_admin_Id"].ToString()))
        {
            if (Convert.ToString(HttpContext.Current.Session["ctrl_admin_Id"]) == "nafiun")
            {
                if (!(HttpContext.Current.Request.Url.AbsoluteUri.Contains("_semester_course_list.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_allocation.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_allocationNew.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_add_student_advisor.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_studentDetails.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_teacherEvalListNew.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_teacherEvalSummery.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_teacherEvalComments.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_teacheractiveInactive.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_StudentRoutine.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_add_teacher.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_teacherAttendance.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_formsAcntFinance.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_formsDean.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_formsExamController.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_formsHR.aspx")
                    || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_formsLogistics.aspx")
                    ))
                {
                    HttpContext.Current.Response.Redirect("~/admin/_semester_course_list.aspx", false);
                }
            }
            else
                if (Convert.ToString(HttpContext.Current.Session["ctrl_admin_Id"]) == "romij")
                {
                    if (!(HttpContext.Current.Request.Url.AbsoluteUri.Contains("_semester_course_list.aspx")
                            || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_allocation.aspx")
                            || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_allocationNew.aspx")
                            || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_add_student_advisor.aspx")
                            || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_studentDetails.aspx")
                            || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_teacherEvalListNew.aspx")
                            || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_teacheractiveInactive.aspx")
                             || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_StudentRoutine.aspx")

                              || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_ConStdClearance.aspx")
                            || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_ConvoStdInfoChk.aspx")
                            || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_ConvoRegStdudent.aspx")
                             || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_ConvoAddNewStdudent.aspx")
                              || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_ConvoRegStds.aspx")
                               || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_AdmitCardClearance.aspx")
                                 || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_PrntAdmitCard.aspx")
                            ))
                    {
                        HttpContext.Current.Response.Redirect("~/admin/_semester_course_list.aspx", false);
                    }
                }

                else
                    if (Convert.ToString(HttpContext.Current.Session["ctrl_admin_Id"]) == "khaled" ||
                        Convert.ToString(HttpContext.Current.Session["ctrl_admin_Id"]) == "AtiqExam")
                    {
                        if (!(HttpContext.Current.Request.Url.AbsoluteUri.Contains("_semester_course_list.aspx")                               
                                  || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_ConvoRegStds.aspx")
                                   || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_PrntAdmitExam.aspx")
                                )
                            )
                        {
                            HttpContext.Current.Response.Redirect("~/admin/_semester_course_list.aspx", false);
                        }
                    }

                    else
                        if (Convert.ToString(HttpContext.Current.Session["ctrl_admin_Id"]) == "Farhana" ||
                            Convert.ToString(HttpContext.Current.Session["ctrl_admin_Id"]) == "Munira")
                        {
                            if (!(HttpContext.Current.Request.Url.AbsoluteUri.Contains("_semester_course_list.aspx")
                                      || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_AdmitCardClearance.aspx")
                                       || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_ConStdClearance.aspx")
                                       || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_PrntAdmitCard.aspx")
                                    )
                                )
                            {
                                HttpContext.Current.Response.Redirect("~/admin/_semester_course_list.aspx", false);
                            }
                        }
           /* else
              if (!ConfigurationManager.AppSettings["admins"].Contains(HttpContext.Current.Session["ctrl_admin_Id"].ToString())
                   && !HttpContext.Current.Request.Url.AbsoluteUri.Contains("_login.aspx"))
               {
                   if (!(HttpContext.Current.Request.Url.AbsoluteUri.Contains("_semester_course_list.aspx")
                       || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_allocation.aspx")
                       || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_allocationNew.aspx")
                       || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_add_student_advisor.aspx")
                       || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_studentDetails.aspx")
                       || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_course_teacherEvalListNew.aspx")
                       || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_teacheractiveInactive.aspx")
                        || HttpContext.Current.Request.Url.AbsoluteUri.Contains("_StudentRoutine.aspx")
                       ))
                   {
                       HttpContext.Current.Response.Redirect("~/admin/_semester_course_list.aspx", false);
                   }
               }*/
        }
    }
    public string get_pk_no(String obj)
    {     
        DatabaseHelper objhelper = new DatabaseHelper();
        string query = " Select OBJECT, SERIAL from WEB_CODES where OBJECT=? ";
        objhelper.AddParameter("@OBJECT", obj);
        DataSet ds = new DataSet();
        string ids = "";
        try
        {
            ds.Merge(objhelper.ExecuteDataSet(query));
        }
        catch
        {
        }
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
                ids = ds.Tables[0].Rows[0]["SERIAL"].ToString();
        }
        return ids;
    }

    public int update_code(string objects, string stNo)
    {
        string code = "";

        int no = Convert.ToInt32(stNo);
        no++;
        if (no < 10)
            code = "00000000" + no;
        else if (no < 100)
            code = "0000000" + no;
        else if (no < 1000)
            code = "000000" + no;
        else if (no < 10000)
            code = "00000" + no;
        else if (no < 100000)
            code = "0000" + no;
        else if (no < 1000000)
            code = "000" + no;
        else if (no < 10000000)
            code = "00" + no;


        DatabaseHelper objhelper = new DatabaseHelper();
        string query = " update WEB_CODES set SERIAL=? where OBJECT=? ";
        objhelper.AddParameter("@SERIAL", code);
        objhelper.AddParameter("@OBJECT", objects);

        return objhelper.ExecuteNonQuery(query);
    }

    public bool isVaidLogin(string user_id, string user_pass)
    {
        DataSet ds = new DataSet();
        bool status = false;

        string sql = @"Select * from USERLOGON where NAME='" + user_id + "' and PASS='" + user_pass + "'  ";
        DatabaseHelper objHelper = new DatabaseHelper();
        ds.Merge(objHelper.ExecuteDataSet(sql));

        if (ds.Tables[0].Rows.Count > 0)
        {
           status = true;
        }

        return status;
    }

    public int changePassword(string user_id,string pre_pass,  string user_pass)
    {
       
         DatabaseHelper objhelper = new DatabaseHelper();
         string query = " update USERLOGON set PASS=? where NAME=? and PASS=? ";
         objhelper.AddParameter("@PASS", user_pass);
         objhelper.AddParameter("@NAME", user_id);
         objhelper.AddParameter("@PASS", pre_pass);

        return objhelper.ExecuteNonQuery(query);
    }

    public string get_a_departmentName(string depId)
    {
        string depName = "";

        string sql = " Select * from COLLEGE where COLLEGECODE='" + depId + "'   ";
        DatabaseHelper objHelper = new DatabaseHelper();
        DataSet ds = new DataSet();
        ds.Merge(objHelper.ExecuteDataSet(sql));

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            depName = dr["COLLEGENAME"].ToString();
        }

        return depName;
    }
    public string getFormattedDate(DateTime date)
    {
        return date.ToString("d MMMM yyyy");
    }

     


    
    /*
    private OleDbConnection con;
    private OleDbDataAdapter da;

    public CommonFun() 
    {
         con = new OleDbConnection(get_con_string());
    }

    private string get_con_string()
    {
            //return "Provider=MSDAORA.1;Data Source=GLOBAL;Persist Security Info=True;User ID=adminuser;password=user ";

            return  "Provider= msdaORA; Data Source=(DESCRIPTION=(ADDRESS_LIST="
          + "(ADDRESS=(PROTOCOL=TCP)(HOST=euc)(PORT=1521)))"
          + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=GLOBAL)));"
          + "User Id=adminuser;Password=user;";
            
    }
    public string get_pk_no(String obj)
    {
        string ids = "";

        DataSet ds = new DataSet();
        da = new OleDbDataAdapter(" Select * from WEB_CODES where OBJECT='" + obj + "' ", con);
        try
        {
            con.Open();
            da.Fill(ds, "WEB_CODES");
        }
        catch (Exception e)
        {
        }
        finally { con.Close(); }
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables["WEB_CODES"].Rows.Count > 0)
                ids = ds.Tables["WEB_CODES"].Rows[0]["SERIAL"].ToString();
        }
        return ids;
    }
    */

}
