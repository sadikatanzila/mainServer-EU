using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

 
 
public class staff_webService : System.Web.Services.WebService
{
    Dts obj_db;

    public staff_webService()
    {
        obj_db = new Dts();
    }

    public DataTable get_allCourses_ofA_semester(string semester, string year)
    {
        string sql = "Select distinct WEB_VIEW_COURSE_TEACHER.*,OFFEREDCOURSE.DEPCODE from WEB_VIEW_COURSE_TEACHER left join OFFEREDCOURSE on OFFEREDCOURSE.COURSEKEY = WEB_VIEW_COURSE_TEACHER.COURSE_KEY   ";
        sql += " where COURSE_KEY like '" + semester + year + "%' and TEACHER_ID='" + Session["user"].ToString() + "' order by CNAME asc";
        return obj_db.get_viewData(sql, "coursList");
    }


    public DataTable get_taken_class(string semester, string year, string course_teacher_id)
    {
        string sql = "select max(TOTAL_ATT) takenclass from WEB_COURSE_STUDENT where COURSE_TEACHER_ID='" + course_teacher_id + "'";
        return obj_db.get_viewData(sql, "TakenClass");
    }


    public DataTable get_allCourses_ofA_semesterNew(string semester, string year)
    {
        string sql = "Select distinct WEB_VIEW_COURSE_TEACHER.* from WEB_VIEW_COURSE_TEACHER   ";
        sql += " where COURSE_KEY like '" + semester + year + "%' and TEACHER_ID='" + Session["user"].ToString() + "' order by CNAME asc";
        return obj_db.get_viewData(sql, "coursList");
    }

   /* public DataTable get_allStudent_ofA_CoursesTeacher(string course_teacher_id)
    {
        string sql = "Select * from  WEB_COURSE_STUDENT  ";
        sql += " where COURSE_TEACHER_ID= '" + course_teacher_id + "' order by SID asc";
        return obj_db.get_viewData(sql, "studentList");
    }*/


    public DataTable get_allStudent_ofA_CoursesTeacher(string course_teacher_id)
    {
        string sql = "Select DISTINCT WEB_COURSE_STUDENT.*, OFFERERINGANDGRADE.IS_FAIL , CASE WHEN OFFERERINGANDGRADE.IS_FAIL ='Y' and  WEB_COURSE_STUDENT.ggrade2 ='F' THEN 'Regular' WHEN OFFERERINGANDGRADE.IS_FAIL= 'X'   THEN 'Irregular' END AS FAIL_status ";
        sql += " from  WEB_COURSE_STUDENT left join OFFERERINGANDGRADE on  (OFFERERINGANDGRADE.REGKEY = WEB_COURSE_STUDENT.REGKEY and OFFERERINGANDGRADE.coursekey = WEB_COURSE_STUDENT.COURSE_KEY) ";
        sql += " where COURSE_TEACHER_ID= '" + course_teacher_id + "' order by SID asc";
        return obj_db.get_viewData(sql, "studentList");
    }


  public DataTable get_allStudent_ofCT_new(string Coursekey, string course_teacher_id, DateTime date, string section, string teacher)
  {
      //string sql = "SELECT distinct OG.REGKEY, NVL(WS.ATTEND,0) ATTEND, ws.CLASS_DATE, S.SID ,S.SNAME,S.SPROGRAM,WC.COURSE_TEACHER_ID  FROM   WEB_COURSE_TEACHER wct  ";
      //        sql += "  JOIN OFFERERINGANDGRADE og on WCT.COURSE_KEY = OG.COURSEKEY   join WEB_COURSE_TEACHER WC on WC.COURSE_TEACHER_ID=WCT.COURSE_TEACHER_ID  ";
      //        sql += "   join Student S on S.SID =  substr(OG.REGKEY,1,9) LEFT JOIN WEB_STUDENT_ATTENDANCE ws on WCT.COURSE_TEACHER_ID = WS.COURSE_TEACHER_ID and substr(OG.REGKEY,1,9) = WS.SID  ";
      //        sql += "    AND WS.CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(date) + "', 'dd/mm/yyyy hh24:mi:ss')   WHERE  wct.COURSE_KEY = '" + Coursekey + "' and WCT.COURSE_TEACHER_ID = '" + course_teacher_id + "'  order by S.SID asc";
      string sql = "SELECT substr(og.regkey,1,9) SID, S.SNAME,S.SPROGRAM, OG.REGKEY , WCT.COURSE_TEACHER_ID ,nvl(WS.ATTEND,0) ATTEND,OG.COURSEKEY   ";
            sql += " FROM WEB_COURSE_TEACHER wct JOIN OFFERERINGANDGRADE og on (WCT.COURSE_KEY = OG.COURSEKEY and wct.SECTION = og.ggroup)   ";
            sql += " join student S on S.SID = Substr(og.regkey,1,9) LEFT JOIN WEB_STUDENT_ATTENDANCE ws on WCT.COURSE_TEACHER_ID = WS.COURSE_TEACHER_ID and substr(OG.REGKEY,1,9) = WS.SID ";
            sql += " AND WS.CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(date) + "', 'dd/mm/yyyy hh24:mi:ss')";
            sql += " WHERE  wct.COURSE_KEY = '" + Coursekey + "' and WCT.TEACHER_ID = '" + teacher + "' AND WCT.SECTION ='" + section + "' and og.regkey like '%'||substr(wct.COURSE_KEY,1,5)  order by S.SID asc ";
      return obj_db.get_viewData(sql, "studentList");
  }



  public DataTable get_allStudent_ofCT_new(string Coursekey, string course_teacher_id, DateTime date, string section, string teacher, string timeslot)
  {
      //string sql = "SELECT distinct OG.REGKEY, NVL(WS.ATTEND,0) ATTEND, ws.CLASS_DATE, S.SID ,S.SNAME,S.SPROGRAM,WC.COURSE_TEACHER_ID  FROM   WEB_COURSE_TEACHER wct  ";
      //        sql += "  JOIN OFFERERINGANDGRADE og on WCT.COURSE_KEY = OG.COURSEKEY   join WEB_COURSE_TEACHER WC on WC.COURSE_TEACHER_ID=WCT.COURSE_TEACHER_ID  ";
      //        sql += "   join Student S on S.SID =  substr(OG.REGKEY,1,9) LEFT JOIN WEB_STUDENT_ATTENDANCE ws on WCT.COURSE_TEACHER_ID = WS.COURSE_TEACHER_ID and substr(OG.REGKEY,1,9) = WS.SID  ";
      //        sql += "    AND WS.CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(date) + "', 'dd/mm/yyyy hh24:mi:ss')   WHERE  wct.COURSE_KEY = '" + Coursekey + "' and WCT.COURSE_TEACHER_ID = '" + course_teacher_id + "'  order by S.SID asc";
      string sql = "SELECT substr(og.regkey,1,9) SID,ws.ATTEND_ID, S.SNAME,S.SPROGRAM, OG.REGKEY , WCT.COURSE_TEACHER_ID ,nvl(WS.ATTEND,0) ATTEND,OG.COURSEKEY   ";
      sql += " FROM WEB_COURSE_TEACHER wct JOIN OFFERERINGANDGRADE og on (WCT.COURSE_KEY = OG.COURSEKEY and wct.SECTION = og.ggroup)   ";
      sql += " join student S on S.SID = Substr(og.regkey,1,9) LEFT JOIN WEB_STUDENT_ATTENDANCE ws on WCT.COURSE_TEACHER_ID = WS.COURSE_TEACHER_ID and substr(OG.REGKEY,1,9) = WS.SID ";
      sql += " AND WS.CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(date) + "', 'dd/mm/yyyy hh24:mi:ss')  and  ws.C_ROUTINE_ID ='" + timeslot + "'  ";
      sql += " WHERE  wct.COURSE_KEY = '" + Coursekey + "' and WCT.TEACHER_ID = '" + teacher + "' AND WCT.SECTION ='" + section + "' and og.regkey like '%'||substr(wct.COURSE_KEY,1,5) order by S.SID asc ";
      return obj_db.get_viewData(sql, "studentList");
  }


  public DataTable get_allStudent_attendance(string course_teacher_id)
  {
      string sql = "Select DISTINCT * from WEB_STUDENT_CLASSATTENDANCE where COURSE_TEACHER_ID= '" + course_teacher_id + "' order by SID asc";
      return obj_db.get_viewData(sql, "studentList");
  }

  public DataTable get_allStudent_attendanceNEW(string course_teacher_id)
  {
      string sql = "Select DISTINCT * from WEB_STUDENT_CLSATTENDANCE where COURSE_TEACHER_ID= '" + course_teacher_id + "' order by SID asc";
      return obj_db.get_viewData(sql, "studentList");
  }


  public DataTable get_Student_attendance()
  {
      string sql = "select SID, CLASS_DATE,COURSE_TEACHER_ID, ATTEND ,case when ATTEND = 0 THEN  'A' when ATTEND = 1 THEN  'P' ";
             sql += "end Attentance from WEB_STUDENT_ATTENDANCE where course_teacher_ID='C_T_0000023673' order by SID";
      return obj_db.get_viewData(sql, "studentList");
  }
  public DataTable get_Student_attendanceDate()
  {
      string sql = "select Distinct CLASS_DATE from WEB_STUDENT_ATTENDANCE where course_teacher_ID='C_T_0000023673'";
      return obj_db.get_viewData(sql, "studentList");
  }

  public DataTable get_Student_List()
  {
      string sql = "select Distinct SID from WEB_STUDENT_ATTENDANCE where course_teacher_ID='C_T_0000023673'";
      return obj_db.get_viewData(sql, "studentList");
  }
    public string set_active_staff(string staff_id)
    {
        string sql = " update WEB_TEACHER_STAFF set STAFF_CTRL=1 where STAFF_ID='" + staff_id + "' "; 
        return obj_db.execute_query(sql);
    }

    public string set_inactive_staff(string staff_id)
    {
        string sql = " update WEB_TEACHER_STAFF set STAFF_CTRL=0 where STAFF_ID='" + staff_id + "' ";
        return obj_db.execute_query(sql);
    }

    public DataTable get_attendance_ofA_Courses(string Coursekey, string teacher, string section, string timeslot, string course_teacher_id, DateTime dt)
    {
        // previous........... string sql = "SELECT substr(og.regkey,1,9) SID, S.SNAME,S.SPROGRAM, OG.REGKEY , WCT.COURSE_TEACHER_ID ,nvl(WS.ATTEND,0) ATTEND,OG.COURSEKEY   ";
       string sql = "SELECT distinct substr(og.regkey,1,9) SID,ws.ATTEND_ID, S.SNAME,S.SPROGRAM, OG.REGKEY , WCT.COURSE_TEACHER_ID ,ws.C_ROUTINE_ID, nvl(WS.ATTEND,0) ATTEND,OG.COURSEKEY  ";
  sql += "  FROM WEB_COURSE_TEACHER wct JOIN OFFERERINGANDGRADE og on (WCT.COURSE_KEY = OG.COURSEKEY and wct.SECTION = og.ggroup)  join student S on S.SID = Substr(og.regkey,1,9)  ";
  sql += "  LEFT JOIN WEB_STUDENT_ATTENDANCE ws  on  ( WCT.COURSE_TEACHER_ID = WS.COURSE_TEACHER_ID and substr(OG.REGKEY,1,9) = WS.SID )  ";
  sql += "  AND WS.CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(dt) + "', 'dd/mm/yyyy hh24:mi:ss')   AND (ws.C_ROUTINE_ID is null or ws.C_ROUTINE_ID ='" + timeslot + "')  ";
  sql += "    WHERE  wct.COURSE_KEY =  '" + Coursekey + "' and WCT.TEACHER_ID = '" + teacher + "'  AND WCT.SECTION ='" + section + "' and og.regkey like '%'||substr(wct.COURSE_KEY,1,5)   order by og.regkey asc";

       // string sql = "SELECT substr(og.regkey,1,9) SID,ws.ATTEND_ID, S.SNAME,S.SPROGRAM, OG.REGKEY , WCT.COURSE_TEACHER_ID ,nvl(WS.ATTEND,0) ATTEND,OG.COURSEKEY   ";
      //  sql += " FROM WEB_COURSE_TEACHER wct JOIN OFFERERINGANDGRADE og on (WCT.COURSE_KEY = OG.COURSEKEY and wct.SECTION = og.ggroup)   ";
      //  sql += " join student S on S.SID = Substr(og.regkey,1,9) LEFT JOIN WEB_STUDENT_ATTENDANCE ws ";
      //  sql += "  ( WCT.COURSE_TEACHER_ID = WS.COURSE_TEACHER_ID and substr(OG.REGKEY,1,9) = WS.SID ) ";
      //  sql += "  AND  WS.CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(dt) + "', 'dd/mm/yyyy hh24:mi:ss')  AND (ws.C_ROUTINE_ID is null or ws.C_ROUTINE_ID='123') ";
      //  sql += "  on WCT.COURSE_TEACHER_ID = WS.COURSE_TEACHER_ID and substr(OG.REGKEY,1,9) = WS.SID ";
      //   sql += " WHERE  wct.COURSE_KEY = '" + Coursekey + "' and WCT.TEACHER_ID = '" + teacher + "' AND WCT.SECTION ='" + section + "' AND (ws.C_ROUTINE_ID ='" + timeslot + "' or ws.C_ROUTINE_ID is null) order by S.SID asc ";

        //  string sql = "Select * from WEB_ATTENDANCE  ";
        //  sql += " where COURSE_TEACHER_ID= '" + course_teacher_id + "' and CLASS_DATE  = TO_DATE('" + new cls_tools().get_database_formateDate(dt) + "', 'dd/mm/yyyy hh24:mi:ss') order by SID asc";
        return obj_db.get_viewData(sql, "studentList");
    }


    public DataTable get_attendanceStstus_ofA_Courses(string Coursekey, string teacher, string section, string timeslot, string course_teacher_id, DateTime dt)
    {
        // previous........... string sql = "SELECT substr(og.regkey,1,9) SID, S.SNAME,S.SPROGRAM, OG.REGKEY , WCT.COURSE_TEACHER_ID ,nvl(WS.ATTEND,0) ATTEND,OG.COURSEKEY   ";
        string sql = "SELECT DISTINCT WS.STATUS   ";
        sql += "  FROM WEB_COURSE_TEACHER wct JOIN OFFERERINGANDGRADE og on (WCT.COURSE_KEY = OG.COURSEKEY and wct.SECTION = og.ggroup)    ";
        sql += "  LEFT JOIN WEB_STUDENT_ATTENDANCE ws  on  ( WCT.COURSE_TEACHER_ID = WS.COURSE_TEACHER_ID and substr(OG.REGKEY,1,9) = WS.SID )  ";
        sql += "  AND WS.CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(dt) + "', 'dd/mm/yyyy hh24:mi:ss')   AND (ws.C_ROUTINE_ID is null or ws.C_ROUTINE_ID ='" + timeslot + "')  ";
        sql += "    WHERE  wct.COURSE_KEY =  '" + Coursekey + "' and WCT.TEACHER_ID = '" + teacher + "'  AND WCT.SECTION ='" + section + "'  ";
        return obj_db.get_viewData(sql, "studentList");
    }


    public DataTable get_attendance_ofAny_Courses(string Coursekey, string teacher, string section, string timeslot, string course_teacher_id, DateTime dt)
    {
        string sql = "SELECT distinct substr(og.regkey,1,9) SID,ws.ATTEND_ID, S.SNAME,S.SPROGRAM, OG.REGKEY , WCT.COURSE_TEACHER_ID ,ws.C_ROUTINE_ID, nvl(WS.ATTEND,0) ATTEND,OG.COURSEKEY  ";
        sql += "  FROM WEB_COURSE_TEACHER wct JOIN OFFERERINGANDGRADE og on (WCT.COURSE_KEY = OG.COURSEKEY and wct.SECTION = og.ggroup)  join student S on S.SID = Substr(og.regkey,1,9)  ";
        sql += "  LEFT JOIN WEB_STUDENT_ATTENDANCE ws  on  ( WCT.COURSE_TEACHER_ID = WS.COURSE_TEACHER_ID and substr(OG.REGKEY,1,9) = WS.SID )  ";
        sql += "  AND WS.CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(dt) + "', 'dd/mm/yyyy hh24:mi:ss')   AND (ws.C_ROUTINE_ID is null or ws.C_ROUTINE_ID ='" + timeslot + "')  ";
        sql += "    WHERE  wct.COURSE_KEY =  '" + Coursekey + "' and ws.COURSE_TEACHER_ID = '" + course_teacher_id + "'  AND WCT.SECTION ='" + section + "' and og.regkey like '%'||substr(wct.COURSE_KEY,1,5)   order by og.regkey asc";
        return obj_db.get_viewData(sql, "anystudentList");
    }
    public DataTable get_attendance_ofA_Courses(string Coursekey,string teacher,string section, string course_teacher_id, DateTime dt)
    {
       // string sql = "SELECT distinct OG.REGKEY, NVL(WS.ATTEND,0) ATTEND, ws.CLASS_DATE, S.SID ,S.SNAME,S.SPROGRAM,WC.COURSE_TEACHER_ID  FROM   WEB_COURSE_TEACHER wct  ";
      //  sql += "  JOIN OFFERERINGANDGRADE og on WCT.COURSE_KEY = OG.COURSEKEY   join WEB_COURSE_TEACHER WC on WC.COURSE_TEACHER_ID=WCT.COURSE_TEACHER_ID  ";
      //  sql += "   join Student S on S.SID =  substr(OG.REGKEY,1,9) LEFT JOIN WEB_STUDENT_ATTENDANCE ws on WCT.COURSE_TEACHER_ID = WS.COURSE_TEACHER_ID and substr(OG.REGKEY,1,9) = WS.SID  ";
      //  sql += "    AND WS.CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(dt) + "', 'dd/mm/yyyy hh24:mi:ss')   WHERE    WCT.COURSE_TEACHER_ID = '" + course_teacher_id + "'  order by S.SID asc";

        string sql = "SELECT distinct substr(og.regkey,1,9) SID, S.SNAME,S.SPROGRAM, OG.REGKEY , WCT.COURSE_TEACHER_ID ,nvl(WS.ATTEND,0) ATTEND,OG.COURSEKEY   ";
        sql += " FROM WEB_COURSE_TEACHER wct JOIN OFFERERINGANDGRADE og on (WCT.COURSE_KEY = OG.COURSEKEY and wct.SECTION = og.ggroup)   ";
        sql += " join student S on S.SID = Substr(og.regkey,1,9) LEFT JOIN WEB_STUDENT_ATTENDANCE ws on WCT.COURSE_TEACHER_ID = WS.COURSE_TEACHER_ID and substr(OG.REGKEY,1,9) = WS.SID ";
        sql += " AND WS.CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(dt) + "', 'dd/mm/yyyy hh24:mi:ss')";
        sql += " WHERE  wct.COURSE_KEY = '" + Coursekey + "' and WCT.TEACHER_ID = '" + teacher + "' AND WCT.SECTION ='" + section + "'  and og.regkey like '%'||substr(wct.COURSE_KEY,1,5)   order by og.regkey asc ";
        
      //  string sql = "Select * from WEB_ATTENDANCE  ";
      //  sql += " where COURSE_TEACHER_ID= '" + course_teacher_id + "' and CLASS_DATE  = TO_DATE('" + new cls_tools().get_database_formateDate(dt) + "', 'dd/mm/yyyy hh24:mi:ss') order by SID asc";
        return obj_db.get_viewData(sql, "studentList");
    }


    public string save_assignment_teacher(DataSet ds, ref string ids)
    { 
        string sql = "";
        foreach (DataRow dr in ds.Tables["assignment"].Rows)
        {
            String code = obj_db.get_pk_no("course_mat");
            ids = "Ass" + code;

            sql = @" insert into WEB_COURSE_MATERIALS_TEACHER (COURSE_MATERIALS_ID, COURSE_TEACHER_ID, TITLE, FILE_NAME, TYPE,UPLOAD_DATE, DUE_DATE, DESCRIPTION, CTRL)";
            sql += " values ('" + ids + "', '" + dr["COURSE_TEACHER_ID"] + "', '" + dr["TITLE"] + "', '" + dr["FILE_NAME"] + "', '" + dr["TYPE"] + "', "; // '" + dr["FILE_DATA"] + "',
            sql += " '" + dr["UPLOAD_DATE"] + "', TO_DATE('" + dr["DUE_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), '" + dr["DESCRIPTION"] + "', '" + dr["CTRL"] + "' ) ";
            new admin_webService().update_code("course_mat", code);
            break;
        } 
        return obj_db.execute_query(sql);    
    }

    public string save_lectures(DataSet ds, ref string ids)
    {  
        string sql = "";
        foreach (DataRow dr in ds.Tables["assignment"].Rows)
        {
            string code = obj_db.get_pk_no("course_mat");
            ids = "Lec" + code;
            sql = @" insert into WEB_COURSE_MATERIALS_TEACHER (COURSE_MATERIALS_ID, COURSE_TEACHER_ID, TITLE, FILE_NAME, TYPE,UPLOAD_DATE, DESCRIPTION, CTRL)";
            sql += " values ('" + ids + "', '" + dr["COURSE_TEACHER_ID"] + "', '" + dr["TITLE"] + "', '" + dr["FILE_NAME"] + "', '" + dr["TYPE"] + "', ";
            sql += " '" + dr["UPLOAD_DATE"] + "','" + dr["DESCRIPTION"] + "', '" + dr["CTRL"] + "' ) ";
            new admin_webService().update_code("course_mat", code);
            break;
        }
        return obj_db.execute_query(sql);
    }


    public int insert_final_advising(DataSet ds, ref string status)
    {
        string sql = "";
        int count=0;
       
        foreach (DataRow dr in ds.Tables["OFFERERINGANDGRADE"].Rows)
        {
            sql = @" insert into OFFERERINGANDGRADE (COURSEKEY, GGRADE, GTYPE, MARKS, REGKEY, GGROUP, CHOURS, GPOINT, GGRADE2,COURSE_INSERTIONDATE)";
            sql += " values ('" + dr["COURSEKEY"] + "', '" + dr["GGRADE"] + "', '" + dr["GTYPE"] + "', '" + dr["MARKS"] + "', '" + dr["REGKEY"] + "', ";
            sql += " '" + dr["GGROUP"] + "','" + dr["CHOURS"] + "', '" + dr["GPOINT"] + "', '" + dr["GGRADE2"] + "', '" + dr["COURSE_INSERTIONDATE"] + "'  ) ";

            if (obj_db.execute_query(sql) == "1")
            {
                count++;
                obj_db.execute_query("update WEB_COURSE_OFFERING_TEMP set CTRL='2', APPROVAL_TIME='" + dr["COURSE_INSERTIONDATE"] + "' where COURSEKEY='" + dr["COURSEKEY"] + "' and REGKEY ='" + dr["REGKEY"] + "' ");
            }
            else
                status += "" + dr["COURSEKEY"] + ", ";
        }

        return count;
       
    }

    public int insert_final_advisingCourse(DataSet ds, ref string status)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["OFFERERINGANDGRADE"].Rows)
        {
            sql = @" insert into OFFERERINGANDGRADE (COURSEKEY, GGRADE, GTYPE, MARKS, REGKEY, GGROUP, CHOURS, GPOINT, GGRADE2,COURSE_INSERTIONDATE, CREATED_DATE, CREATED_BY)";
            sql += " values ('" + dr["COURSEKEY"] + "', '" + dr["GGRADE"] + "', '" + dr["GTYPE"] + "', '" + dr["MARKS"] + "', '" + dr["REGKEY"] + "', ";
            sql += " '" + dr["GGROUP"] + "','" + dr["CHOURS"] + "', '" + dr["GPOINT"] + "', '" + dr["GGRADE2"] + "', '" + dr["COURSE_INSERTIONDATE"] + "' ,TO_DATE( '" + dr["CREATED_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss') , '" + dr["CREATED_BY"] + "'  ) ";

            if (obj_db.execute_query(sql) == "1")
            {
                count++;
                obj_db.execute_query("update WEB_COURSE_OFFERING_TEMP set CTRL='2', APPROVAL_TIME='" + dr["COURSE_INSERTIONDATE"] + "' where COURSEKEY='" + dr["COURSEKEY"] + "' and REGKEY ='" + dr["REGKEY"] + "' ");
            }
            else
                status += "" + dr["COURSEKEY"] + ", ";
        }

        return count;

    }

    public int Update_final_advising(DataSet ds, ref string status)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["OFFERERINGANDGRADE"].Rows)
        {
            sql = @" update OFFERERINGANDGRADE set GGROUP= '" + dr["GGROUP"] + "' , LAST_MODIFIED_BY= '" + dr["LAST_MODIFIED_BY"] + "' ,LAST_MODIFIED= TO_DATE('" + dr["LAST_MODIFIED"] + "', 'dd/mm/yyyy hh24:mi:ss')    where COURSEKEY='" + dr["COURSEKEY"] + "' and REGKEY='" + dr["REGKEY"] + "'  ";


            if (obj_db.execute_query(sql) == "1")
            {
                count++;
                obj_db.execute_query("update WEB_COURSE_OFFERING_TEMP set CTRL='2', APPROVAL_TIME='" + dr["COURSE_INSERTIONDATE"] + "' where COURSEKEY='" + dr["COURSEKEY"] + "' and REGKEY ='" + dr["REGKEY"] + "' ");
            }
            else
                status += "" + dr["COURSEKEY"] + ", ";
        }

        //return obj_db.execute_query(sql);
        return count;
    }
    public int insert_teacher_grading(DataSet ds, Boolean isApprove)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["OFFERERINGANDGRADE"].Rows)
        {
            sql = "";

            if (isApprove)
            {
                sql = @" update OFFERERINGANDGRADE set GGRADE='" + dr["GGRADE"] + "', GGRADE2='" + dr["GGRADE2"] + "',  GPOINT='" + dr["GPOINT"] + "',  ";
                sql += "  CREATED_BY='" + dr["CREATED_BY"] + "' , CREATED_DATE= TO_DATE('" + dr["CREATED_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), IS_FAIL='" + dr["IS_FAIL"] + "', APPROVE_CTRL=1  ";
                sql += " where COURSEKEY='" + dr["COURSEKEY"] + "' and REGKEY='" + dr["REGKEY"] + "' ";

            }
            else
            {
                sql = @" update OFFERERINGANDGRADE set GGRADE='" + dr["GGRADE"] + "', GGRADE2='" + dr["GGRADE2"] + "',  GPOINT='" + dr["GPOINT"] + "',  ";
                sql += "  CREATED_BY='" + dr["CREATED_BY"] + "' , CREATED_DATE= TO_DATE('" + dr["CREATED_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), IS_FAIL='" + dr["IS_FAIL"] + "'  ";
                sql += " where COURSEKEY='" + dr["COURSEKEY"] + "' and REGKEY='" + dr["REGKEY"] + "' ";

            }

            if (obj_db.execute_query(sql) == "1")
                count++;


        }

        return count;

    }

    public DataTable get_FailPolicy()
    {
        string sql = "  Select FAILPOLICY.*, CASE  when FAILTYPE ='Y' then 'Regular'  WHEN FAILTYPE = 'X'   THEN 'Irregular' WHEN FAILTYPE is null   THEN 'Select'  END AS FTYPE from FAILPOLICY  order by serial asc";
        return obj_db.get_viewData(sql, "FAILPOLICY");
    }
    
    public string save_attendance(DataSet ds, string course_teacherId, DateTime date)
    {
        String sql = "", count = "";
       obj_db.execute_query( " Delete from WEB_STUDENT_ATTENDANCE where CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(date) + "', 'dd/mm/yyyy hh24:mi:ss') and  COURSE_TEACHER_ID='" + course_teacherId + "' ") ;
      //  save_attendanceNew(ds);
        //return obj_db.insert_general(ds, "WEB_STUDENT_ATTENDANCE");
        foreach (DataRow dr in ds.Tables["WEB_STUDENT_ATTENDANCE"].Rows)
        {
            sql = "INSERT INTO WEB_STUDENT_ATTENDANCE (COURSE_TEACHER_ID, SID, CLASS_DATE, ATTEND) ";
            sql += " values ( '" + dr["COURSE_TEACHER_ID"] + "', '" + dr["SID"] + "', TO_DATE('" + dr["CLASS_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), '" + dr["ATTEND"] + "') ";//, '" + dr["INPUT_BY"] + "'
            if (obj_db.execute_query(sql) == "1")
            {
                count = "1";
            }
        }
      //  string i = obj_db.execute_query(sql);
        return count;
       
      //  return "";
    }

    public string save_attendance_new(DataSet ds, string course_teacherId, string timeslot, DateTime date)
    {
        String sql = "", count = "";
        //  obj_db.execute_query(" Delete from WEB_STUDENT_ATTENDANCE where CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(date) + "', 'dd/mm/yyyy hh24:mi:ss') and  COURSE_TEACHER_ID='" + course_teacherId + "' and ( C_ROUTINE_ID='" + timeslot + "' or C_ROUTINE_ID is null ) ");
        //  save_attendanceNew(ds);
        //return obj_db.insert_general(ds, "WEB_STUDENT_ATTENDANCE");
        foreach (DataRow dr in ds.Tables["WEB_STUDENT_ATTENDANCE"].Rows)
        {
            sql = "INSERT INTO WEB_STUDENT_ATTENDANCE (COURSE_TEACHER_ID, SID, CLASS_DATE, ATTEND, COURSEKEY, SECTION,YEAR,SEMESTER,C_ROUTINE_ID,INSERTED, STATUS) ";
            sql += " values ( '" + dr["COURSE_TEACHER_ID"] + "', '" + dr["SID"] + "', TO_DATE('" + dr["CLASS_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), '" + dr["ATTEND"] + "', ";
            sql += "'" + dr["COURSEKEY"] + "', '" + dr["SECTION"] + "','" + dr["YEAR"] + "', '" + dr["SEMESTER"] + "','" + dr["C_ROUTINE_ID"] + "','" + dr["INSERTED"] + "','" + dr["STATUS"] + "' ) ";
            if (obj_db.execute_query(sql) == "1")
            {
                count = "1";
            }
        }
        //  string i = obj_db.execute_query(sql);
        return count;

        //  return "";
    }

    public string update_attendance_new(DataSet ds)
    {
        String sql = "", count = "";
        foreach (DataRow dr in ds.Tables["WEB_STUDENT_ATTENDANCE"].Rows)
        {
            sql = "Update  WEB_STUDENT_ATTENDANCE set ATTEND='" + dr["ATTEND"] + "' , CLASS_DATE= TO_DATE('" + dr["CLASS_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss')  ";
            sql += " , C_ROUTINE_ID ='" + dr["C_ROUTINE_ID"] + "' , UPDATEID ='" + dr["UPDATED"] + "' where ATTEND_ID='" + dr["ATTEND_ID"] + "' ";
            if (obj_db.execute_query(sql) == "1")
            {
                count = "1";
            }
        }

        return count;
    }

    public DataTable get_TimeSlot_Teacher(string course_teacher_id)
    {
        string sql = "select value, C_ROUTINE.DAY||' '||C_ROUTINE.TIME slot from (  select TUT_CLS_1 as value from web_course_teacher where web_course_teacher.COURSE_TEACHER_ID ='" + course_teacher_id + "'   ";
        sql += "  union select TUT_CLS_2 as value from web_course_teacher where web_course_teacher.COURSE_TEACHER_ID='" + course_teacher_id + "' ) tt left join C_ROUTINE on C_ROUTINE.C_ROUTINE_ID=tt.value";
        return obj_db.get_viewData(sql, "TimeSlot_list");
    }


    public string UpdateIntoDemo(String Coursekey, String teacherID, String section, String timeslot, DateTime CLASS_DATE, string UPDATED)
    {
        //obj_db.execute_query(" Delete from WEB_STUDENT_ATTENDANCE_DEMO where CLASS_DATE = TO_DATE('" + new cls_tools().get_database_formateDate(CLASS_DATE) + "', 'dd/mm/yyyy hh24:mi:ss') and  COURSE_TEACHER_ID='" + teacherID + "' and ( C_ROUTINE_ID='" + timeslot + "' or C_ROUTINE_ID is null ) ");
        string sql = " ";
        sql = @" Insert into WEB_STUDENT_ATTENDANCE_DEMO (COURSE_TEACHER_ID, SID, CLASS_DATE, ATTEND, COURSEKEY, SECTION,YEAR,SEMESTER,C_ROUTINE_ID,INSERTED,UPDATED,STATUS) ";
        sql += "  (select COURSE_TEACHER_ID ,  SID , CLASS_DATE ,  ATTEND  , COURSEKEY  ,SECTION ,YEAR,SEMESTER,C_ROUTINE_ID,INSERTED,'" + UPDATED + "' as UPDATED,STATUS  ";
        sql += "  from WEB_STUDENT_ATTENDANCE  where COURSEKEY = '" + Coursekey + "' and COURSE_TEACHER_ID = '" + teacherID + "' and SECTION = '" + section + "' and CLASS_DATE= TO_DATE('" + new cls_tools().get_database_formateDate(CLASS_DATE) + "', 'dd/mm/yyyy hh24:mi:ss') and C_ROUTINE_ID = '" + timeslot + "' ) ";

        //  sql1 = "Update  WEB_STUDENT_ATTENDANCE_DEMO set UPDATED='" + UPDATED + "'  ";
        //  sql1 += " where COURSEKEY = '" + Coursekey + "' and COURSE_TEACHER_ID = '" + teacherID + "' and SECTION = '" + section + "' and CLASS_DATE= TO_DATE('" + new cls_tools().get_database_formateDate(CLASS_DATE) + "', 'dd/mm/yyyy hh24:mi:ss') and C_ROUTINE_ID = '" + timeslot + "' ";


        string i = obj_db.execute_query(sql);
        // string j = obj_db.execute_query(sql1);

        // if (i == "1" && j == "1")
        return i;
        // else
        //     return j;
    }

    public string DeletedIntoDemo(String Coursekey, String teacherID, String section, String timeslot, DateTime CLASS_DATE, string DELETED)
    {
        string sql = " ", sql1 = "";
        sql = @" Insert into WEB_STUDENT_ATTENDANCE_DEMO (COURSE_TEACHER_ID, SID, CLASS_DATE, ATTEND, COURSEKEY, SECTION,YEAR,SEMESTER,C_ROUTINE_ID,INSERTED,UPDATED,DELETED,STATUS) ";
        sql += "  (select COURSE_TEACHER_ID ,  SID , CLASS_DATE ,  ATTEND  , COURSEKEY  ,SECTION ,YEAR,SEMESTER,C_ROUTINE_ID,INSERTED ,UPDATEID, '" + DELETED + "' as DELETED , STATUS";
        sql += "  from WEB_STUDENT_ATTENDANCE  where COURSEKEY = '" + Coursekey + "' and COURSE_TEACHER_ID = '" + teacherID + "' and SECTION = '" + section + "' and CLASS_DATE= TO_DATE('" + new cls_tools().get_database_formateDate(CLASS_DATE) + "', 'dd/mm/yyyy hh24:mi:ss') and C_ROUTINE_ID = '" + timeslot + "' ) ";

        // sql1 = "Update  WEB_STUDENT_ATTENDANCE_DEMO set DELETED='" + DELETED + "'  ";
        //  sql1 += " where COURSEKEY = '" + Coursekey + "' and COURSE_TEACHER_ID = '" + teacherID + "' and SECTION = '" + section + "' and CLASS_DATE= TO_DATE('" + new cls_tools().get_database_formateDate(CLASS_DATE) + "', 'dd/mm/yyyy hh24:mi:ss') and C_ROUTINE_ID = '" + timeslot + "' ";

        string i = obj_db.execute_query(sql);

        return i;

    }

    public bool check_employee_login(string user_id, string user_pass)
    {
        DataSet ds = new DataSet();
        bool status = false;

        string sql = @"Select * from WEB_TEACHER_STAFF where VALUE ='" + user_id + "' and PASSWORD='" + user_pass + "' and  STAFF_CTRL=1  ";
        ds.Merge(obj_db.get_viewData(sql, "WEB_TEACHER_STAFF"));

        if (ds.Tables["WEB_TEACHER_STAFF"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["WEB_TEACHER_STAFF"].Rows[0];
            if (dr["VALUE"].ToString() != "" && dr["PASSWORD"].ToString() != "")
            {
                if (dr["VALUE"].ToString() == user_id && dr["PASSWORD"].ToString() == user_pass)
                {
                    status = true;
                }

            }
        }

        return status;
    }


    public DataTable get_user_field(string user_id)
    {
        string sql = " Select AD_EMPLOYEE_ROLE.*,AD_ROLE.ROLE_NAME from AD_EMPLOYEE_ROLE left join AD_ROLE on AD_ROLE.ID = AD_EMPLOYEE_ROLE.ROLE_ID where EMPLOYEE_ID='" + user_id + "' and IS_ACTIVE = 'Y' order by ROLE_NAME asc ";
        return obj_db.get_viewData(sql, "EmployeeList");
    }

    public DataTable get_HRT_fieldID(string user_id)
    {
        string sql = " Select * from WEB_TEACHER_STAFF where VALUE='" + user_id + "' and  STAFF_CTRL=1 ";
        return obj_db.get_viewData(sql, "StaffList");
    }


    public DataTable get_HRT_fieldStaffID(string user_id)
    {
        string sql = " Select * from WEB_TEACHER_STAFF where VALUE='" + user_id + "' and  STAFF_CTRL=1 ";
        return obj_db.get_viewData(sql, "StaffList");
    }
    public string drop_attendance_new(string Coursekey, string teacherID, string section, string timeslot, DateTime CLASS_DATE)
    {
        String status = "";

        // sql = " Delete from WEB_STUDENT_ATTENDANCE where COURSEKEY = '" + Coursekey + "' and COURSE_TEACHER_ID = '" + teacherID + "' and SECTION = '" + section + "' and CLASS_DATE= TO_DATE('" + new cls_tools().get_database_formateDate(CLASS_DATE) + "', 'dd/mm/yyyy hh24:mi:ss') and C_ROUTINE_ID = '" + timeslot + "' ";

        if (obj_db.execute_query(" Delete from WEB_STUDENT_ATTENDANCE where COURSEKEY = '" + Coursekey + "' and COURSE_TEACHER_ID = '" + teacherID + "' and SECTION = '" + section + "' and CLASS_DATE= TO_DATE('" + new cls_tools().get_database_formateDate(CLASS_DATE) + "', 'dd/mm/yyyy hh24:mi:ss') and C_ROUTINE_ID = '" + timeslot + "'  ") == "1")
            status = "1";

        return status;
    }
    public DataTable get_allStudent_ofA_advisorNEW(string advisor, string batch)
    {
        string sql = "Select distinct S.SID, S.SNAME,S.graduationYear, S.PHONE, VW_GET_CGPA_F.CGPA, COMP_CHRS,  WCHRS, TOTALCHRS, CASE WHEN cngch.reqcredithrs IS NOT NULL THEN cngch.reqcredithrs WHEN cngch.reqcredithrs IS NULL  THEN deptc.reqcredithrs ";
        sql += "  END reqch, CASE  WHEN S.graduationYear > 0   THEN 'Graduate'  END AS Grad_Status, Case When S.TR_CR_ELIGIBLE  = 1 then TCGPA   ELSE CGPA END as FINAL_CGPA from STUDENT S left join VW_GET_CGPA_F on VW_GET_CGPA_F.sid = S.SID LEFT JOIN departmentincollege deptc  ON s.sprogram = deptc.depcode ";
        sql += "  LEFT JOIN college  ON college.collegecode = deptc.collegecode LEFT JOIN change_reqcredithrs cngch ON deptc.depid = cngch.depid where ADVISOR_ID='" + advisor + "' and S.SID like '" + batch + "%' order by  S.graduationYear asc, S.SID desc ";

        return obj_db.get_viewData(sql, "studentList");
    }

    public DataTable get_allStudent_ofA_advisor(string advisor, string batch)
    {
        string sql = "Select * from STUDENT where ADVISOR_ID='" + advisor + "' and SID like '" + batch + "%'  ";
        return obj_db.get_viewData(sql, "studentList");
    }
    

    public string save_outLine(DataSet ds, ref string ids)
    {
        string sql = "";
        
            foreach (DataRow dr in ds.Tables["assignment"].Rows)
            {
                string code = obj_db.get_pk_no("course_mat");
                ids = "Out" + code; 
                sql = @" insert into WEB_COURSE_MATERIALS_TEACHER (COURSE_MATERIALS_ID, COURSE_TEACHER_ID, TITLE, FILE_NAME, TYPE,UPLOAD_DATE, DESCRIPTION, CTRL)";
                sql += " values ('" + ids + "', '" + dr["COURSE_TEACHER_ID"] + "', '" + dr["TITLE"] + "', '" + dr["FILE_NAME"] + "', '" + dr["TYPE"] + "', ";
                sql += " '" + dr["UPLOAD_DATE"] + "','" + dr["DESCRIPTION"] + "', '" + dr["CTRL"] + "' ) ";

                new admin_webService().update_code("course_mat", code);
                break;
            }
        return obj_db.execute_query(sql);
    }

    public string delete_assignment_teacher(string ids)
    {
        return obj_db.execute_query(" Delete from WEB_COURSE_MATERIALS_TEACHER where COURSE_MATERIALS_ID='" + ids + "' ");
    }

    public string delete_preOffer_courses(string courseKey, String regKey)
    {
        return obj_db.execute_query(" Delete from WEB_COURSE_OFFERING_TEMP where COURSEKEY='" + courseKey + "' and REGKEY='" + regKey + "' ");
    }

    public string delete_reAdvising_courses(string courseKey, String regKey)
    {
        obj_db.execute_query(" Delete from WEB_COURSE_OFFERING_TEMP where COURSEKEY='" + courseKey + "' and REGKEY='" + regKey + "' ");
        return obj_db.execute_query(" Delete from OFFERERINGANDGRADE where COURSEKEY='" + courseKey + "' and REGKEY='" + regKey + "' ");
    }

    public string upload_teacher_picture(string picName)
    {
        string sql = @" update WEB_TEACHER_STAFF set STAFF_PICTURE='" + picName + "' where STAFF_ID='" + Session["user"].ToString() + "' ";
        return obj_db.execute_query("" + sql);
    }



    public bool check_staff_login(string user_id, string user_pass)
    {
        DataSet ds = new DataSet();
        bool status = false;

        string sql = @"Select * from WEB_TEACHER_STAFF where STAFF_ID='" + user_id + "' and PASSWORD='" + user_pass + "' and  STAFF_CTRL=1  ";
        ds.Merge(obj_db.get_viewData(sql, "WEB_TEACHER_STAFF"));

        if (ds.Tables["WEB_TEACHER_STAFF"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["WEB_TEACHER_STAFF"].Rows[0];
            if (dr["STAFF_ID"].ToString() != "" && dr["PASSWORD"].ToString() != "")
            {
                if (dr["STAFF_ID"].ToString() == user_id && dr["PASSWORD"].ToString() == user_pass)
                {
                    status = true;
                }

            }
        }

        return status;
    }
     

    public DataTable get_all_assignment_ofA_course(string course_teacher_id)
    {
        string sql = " Select * from WEB_COURSE_MATERIALS_TEACHER where COURSE_TEACHER_ID='" + course_teacher_id + "' and TYPE=1 order by UPLOAD_DATE asc ";        
        return obj_db.get_viewData(sql, "assignmentList");
    }


    public DataTable get_all_lectures_ofA_course(string course_teacher_id)
    {
        string sql = " Select * from WEB_COURSE_MATERIALS_TEACHER where COURSE_TEACHER_ID='" + course_teacher_id + "' and TYPE=2 order by UPLOAD_DATE asc ";
        return obj_db.get_viewData(sql, "assignmentList");
    }

    public DataTable get_a_lecture_teacher(string lecture_id)
    {
        string sql = " Select * from WEB_COURSE_MATERIALS_TEACHER where COURSE_MATERIALS_ID='" + lecture_id + "' ";
        return obj_db.get_viewData(sql, "assignment");
    }

    public DataTable get_gradingPolicy(string profile)
    {
        string sql = " SELECT * FROM GRADINGPOLICY WHERE PROFILES='"+profile+"' ";
        return obj_db.get_viewData(sql, "GRADINGPOLICY"+profile);
    }


    public DataTable get_all_studentSubmitted_assignment(string ass_id)
    {
        string sql = @" Select * from WEB_COURSE_MATERIALS_STUDENT where COURSE_MAT_ID='" + ass_id + "'  ";
        return obj_db.get_viewData(sql, "s_courseMaterial");
    }

    public string get_a_courseName_onKey(string course_key)
    {
        string cname = "";

        DataSet ds = new DataSet();        
        string sql = " SELECT * FROM OFFEREDCOURSE, CHANGEDCOURSENAME ";
        sql += " WHERE COURSEKEY='" + course_key + "' AND OFFEREDCOURSE.COURSECODE=CHANGEDCOURSENAME.COURSECODE ";
        
        ds.Merge(obj_db.get_viewData(sql, "OFFEREDCOURSE"));
        if (ds.Tables["OFFEREDCOURSE"].Rows.Count > 0)
        {
            cname = ds.Tables["OFFEREDCOURSE"].Rows[0]["CNAME"].ToString();
        }
        return cname;
    }


    public string get_allocated_teacher(string course_key)
    {
        string cname = "";

        DataSet ds = new DataSet();
        string sql = " SELECT distinct * From WEB_VIEW_COURSE_TEACHER where COURSE_KEY='" + course_key + "'  ";

        ds.Merge(obj_db.get_viewData(sql, "OFFEREDCOURSE"));
        if (ds.Tables["OFFEREDCOURSE"].Rows.Count > 0)
        {
            cname = ds.Tables["OFFEREDCOURSE"].Rows[0]["TOTAL_STUDENT"].ToString();
            cname += "|";
            cname += ds.Tables["OFFEREDCOURSE"].Rows[0]["TOTAL_CAPACITY"].ToString();
        }
        return cname;
    }

    public DataTable get_all_publisher()
    {
        string sql = " Select * from PUBLISHERS  ";
        return obj_db.get_viewData(sql, "PUBLISHERS");
    }

    public DataTable get_a_bookDetails_Info(string bookId)
    {    
        string sql = " SELECT BOOK_MASTER.*, BOOK_SECONDARY.*, PUBLISHERS.*  ";
               sql += " FROM BOOK_MASTER,BOOK_SECONDARY,PUBLISHERS ";
               sql += " WHERE BOOK_MASTER.BOOK_ID=BOOK_SECONDARY.BOOK_ID ";
               sql += " AND BOOK_MASTER.PUB_CODE=PUBLISHERS.PUB_CODE ";
               sql += " AND BOOK_MASTER.BOOK_ID='" + bookId + "' ";
               sql += " AND BOOK_SECONDARY.STATUS='1' ";
               return obj_db.get_viewData(sql, "BOOK_MASTER");
    }

    public DataTable get_find_Teacher(string department, string name_id)
    {
        int flag = 0;
        string sql = @" SELECT ws.*, c.COLLEGENAME FROM WEB_TEACHER_STAFF ws, COLLEGE c WHERE ws.DEPARTMENT=c.COLLEGECODE  and JOB_CATEGORY='Teacher' ";

        if (department != "Select")
        {
            sql += " and ws.DEPARTMENT='" + department + "'";
            flag++;
        }

        if (!string.IsNullOrEmpty(name_id.Trim()))
        {

            sql += " and STAFF_NAME like '%" + name_id + "%' ";
            
        }

        return obj_db.get_viewData(sql, "staff");
    }

    public DataTable get_all_active_argument()
    {
        string sql = " Select * from WEB_TEACHER_EVAL_ARGUMENT ";
        return obj_db.get_viewData(sql, "argument");
    }

    public DataTable return_table(string sql, string tableName)
    {
        return obj_db.get_viewData(sql, tableName);
    }


    public DataTable get_course_teacher_Info(string courseTeacherID)
    {
        string sql = @" SELECT ct.* ,s.STAFF_NAME,s.DEPARTMENT, o.COURSECODE,cn.CNAME ";
        sql += " FROM WEB_COURSE_TEACHER ct, WEB_TEACHER_STAFF s, OFFEREDCOURSE o,CHANGEDCOURSENAME cn ";
        sql += " WHERE COURSE_TEACHER_ID ='" + courseTeacherID + "' ";
        sql += " AND ct.TEACHER_ID=s.STAFF_ID ";
        sql += " AND (ct.COURSE_KEY=o.COURSEKEY AND o.COURSECODE=cn.COURSECODE)  ";

        return obj_db.get_viewData(sql, "course_teacher_Info");

    }



    public DataTable get_searched_books(string publisher, string author, string dep, string title)
    {
        int flag = 0;
        string sql = " Select BOOK_MASTER.*,PUBLISHERS.*, ";
        sql += " (  SELECT COUNT(BOOK_ID) FROM BOOK_SECONDARY WHERE BOOK_SECONDARY.BOOK_ID=BOOK_MASTER.BOOK_ID ";
        sql += " AND STATUS='1' ) AS available ";
        sql += "  from BOOK_MASTER, PUBLISHERS ";

        if (!String.IsNullOrEmpty(author))
        {
            sql += "  where  AUTHORS like '%" + author.Trim() + "%' ";
            flag++;
        }

        if (publisher!="0")
        {
            if (flag==0)
                sql += "  WHERE  BOOK_MASTER.PUB_CODE ='" + publisher + "' ";
            else
                sql += "  and  BOOK_MASTER.PUB_CODE ='" + publisher + "' ";
            flag++;
        }

        if (dep != "Select")
        {
            if (flag == 0)
                sql += "  WHERE  DEP_NAME='" + dep+ "' ";
            else
                sql += "  and  DEP_NAME='" + dep+ "' ";
            flag++;
        }

        if (!String.IsNullOrEmpty(title))
        {
            if (flag == 0)
                sql += "  WHERE  TITLE like '%" + title.Trim()+ "%' ";
            else
                sql += "  AND TITLE like '%" + title.Trim() + "%'";
            flag++;
        }

        if (flag == 0)
        {
            sql += " where PUBLISHERS.PUB_CODE= BOOK_MASTER.PUB_CODE ";        
        }
        else
            sql += " and PUBLISHERS.PUB_CODE= BOOK_MASTER.PUB_CODE ";        



        return obj_db.get_viewData(sql, "BOOK_MASTER");
    }
    public DataTable get_STAFF_ID(string staffId)
    {
        string sql = @" Select * from WEB_TEACHER_STAFF where STAFF_ID='" + staffId + "'";
        return obj_db.get_viewData(sql, "WEB_TEACHER_STAFF");
    }

    public string get_teacher_picture()
    {
        string picName = "";
        DataSet ds = new DataSet();
        string sql = @" select * from  WEB_TEACHER_STAFF where STAFF_ID='" + Session["user"].ToString() + "' ";
        ds.Merge(obj_db.get_viewData(sql, "WEB_TEACHER_STAFF"));

        if (ds.Tables["WEB_TEACHER_STAFF"].Rows.Count > 0)
        {
            if (!String.IsNullOrEmpty(ds.Tables["WEB_TEACHER_STAFF"].Rows[0]["STAFF_PICTURE"].ToString()))
                picName = ds.Tables["WEB_TEACHER_STAFF"].Rows[0]["STAFF_PICTURE"].ToString();
        }
        return picName;
    }

    public string change_teacher_password(string previous_pass, string new_pass)
    {
        string sql = @" update WEB_TEACHER_STAFF set PASSWORD='" + new_pass + "' where STAFF_ID='" + Session["user"].ToString() + "' and PASSWORD= '" + previous_pass + "'  ";
        return obj_db.execute_query("" + sql);
    }

    public string change_teacher_password_from_admin(string teacher_id, string new_pass)
    {
        string sql = @" update WEB_TEACHER_STAFF set PASSWORD='" + new_pass + "' where STAFF_ID='" + teacher_id.Trim() + "' ";
        return obj_db.execute_query("" + sql);
    }

    public string change_teacher_password(string pre_pass,string userId, string new_pass)
    {
        string sql = @" update WEB_TEACHER_STAFF set PASSWORD='" + new_pass + "' where STAFF_ID='" + userId + "' and  PASSWORD='" + pre_pass + "'  ";
        return obj_db.execute_query("" + sql);
    }

    public string get_a_teacher_ofA_course(string courseKey, string section)
    {
        String staff_name = "";

        DataSet ds = new DataSet();
        string sql = " SELECT  ct.*,tf.STAFF_NAME FROM WEB_COURSE_TEACHER ct, WEB_TEACHER_STAFF tf ";
        sql += " WHERE ct.TEACHER_ID=tf.STAFF_ID AND ct.COURSE_KEY='" + courseKey + "' AND ct.SECTION='" + section + "' ";
        ds.Merge( obj_db.get_viewData(sql, "teacher"));

        if (ds.Tables["teacher"].Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(ds.Tables["teacher"].Rows[0]["STAFF_NAME"].ToString()))
            {
                staff_name = ds.Tables["teacher"].Rows[0]["STAFF_NAME"].ToString();
                staff_name += ":" + ds.Tables["teacher"].Rows[0]["COURSE_TEACHER_ID"].ToString();
            }        
        }

        return staff_name;
    }

    public string get_a_Routine_CT(string COURSE_TEACHER_ID)
    {
        String class_name = "";

        DataSet ds = new DataSet();
        string sql = " SELECT  * from web_course_teacher WHERE COURSE_TEACHER_ID='" + COURSE_TEACHER_ID + "' ";
        ds.Merge(obj_db.get_viewData(sql, "CT_routine"));

        if (ds.Tables["CT_routine"].Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(ds.Tables["CT_routine"].Rows[0]["SCH_CLS_1"].ToString()))
            {
                class_name = ds.Tables["CT_routine"].Rows[0]["SCH_CLS_1"].ToString();
                class_name += "_" + ds.Tables["CT_routine"].Rows[0]["SCH_CLS_2"].ToString();
            }
        }

        return class_name;
    }

    public DataTable get_teacher_notice()
    {
        string sql = @" Select * from WEB_NOTICE_BOARD where FOR_TEACHER=1 and CTRL=1 order by NOTICE_ID desc ";
        return obj_db.get_viewData(sql, "WEB_NOTICE_BOARD");
    }

    public DataTable get_staff_info(string staffId)
    {
        string sql = @" Select * from WEB_TEACHER_STAFF where STAFF_ID='" + staffId + "'";
        return obj_db.get_viewData(sql, "WEB_TEACHER_STAFF");
    }

    public DataTable get_advising_message(string staffId)
    {
        string sql = @" SELECT DISTINCT STUDENT.sid,sname,sprogram ,REGISTATUS.YEAR, REGISTATUS.SEMESTER, ";
        sql += "CASE WHEN REGISTATUS.SEMESTER = 1 THEN 'Spring' WHEN REGISTATUS.SEMESTER = 2 THEN 'Summer' WHEN REGISTATUS.SEMESTER = 3 THEN 'Fall' END AS SEMESTERN";
        sql += " FROM STUDENT, REGISTATUS ,WEB_COURSE_OFFERING_TEMP  ";
         sql += " WHERE  (REGISTATUS.SID=STUDENT.SID)AND ADVISOR_ID='" + staffId + "' ";
         sql += " AND (WEB_COURSE_OFFERING_TEMP.REGKEY=REGISTATUS.REGKEY) AND CTRL=1 ";
         sql += "  ORDER BY REGISTATUS.YEAR DESC,REGISTATUS.SEMESTER DESC, STUDENT.SID ASC ,SPROGRAM ASC ";
       
        return obj_db.get_viewData(sql, "advising_msg_list");
    }

    public DataTable get_re_advising_message(string staffId, string year, string sem)
    {
        string sql = @" SELECT DISTINCT STUDENT.sid,sname,sprogram ,REGISTATUS.YEAR, REGISTATUS.SEMESTER ";
        sql += " FROM STUDENT, REGISTATUS ,WEB_COURSE_OFFERING_TEMP ";
        sql += " WHERE  (REGISTATUS.SID=STUDENT.SID)AND ADVISOR_ID='" + staffId + "' ";
		sql += " AND WEB_COURSE_OFFERING_TEMP.REGKEY LIKE '%' || " + sem + year;
        sql += " AND (WEB_COURSE_OFFERING_TEMP.REGKEY=REGISTATUS.REGKEY) "; //AND CTRL=1 
        sql += "  ORDER BY REGISTATUS.YEAR DESC,REGISTATUS.SEMESTER DESC, STUDENT.SID ASC ,SPROGRAM ASC ";

        return obj_db.get_viewData(sql, "advising_msg_list");
    }

    public DataTable get_a_student_information(string student_id)
    {
        string sql = "  SELECT     a.*,SF.SFNAME Father,SM.SMNAME Mother, d .STAFF_NAME FROM STUDENT a LEFT OUTER JOIN ";
        sql += "  WEB_TEACHER_STAFF d   ON a.ADVISOR_ID  = d.STAFF_ID left join STUDFATHERDET SF on SF.SID = a.SID left join STUDMOTHERDET SM on SM.SID = a.SID WHERE a.sid='" + student_id + "' ";
        return obj_db.get_viewData(sql, "student");
    }
    
    public DataTable get_a_notice_details(string notice_id)
    {
        string sql = " SELECT  *from WEB_NOTICE_BOARD where NOTICE_ID='" + notice_id + "' and (FOR_TEACHER=1 or FOR_GENERAL=1 ) ";

        return obj_db.get_viewData(sql, "WEB_NOTICE_BOARD");
    }


    public DataTable get_completed_course_information(string student_id)
    {
        string sql = " SELECT DISTINCT OFFEREDCOURSE.COURSECODE, a.* FROM OFFERERINGANDGRADE a,OFFEREDCOURSE ";
        sql += " WHERE  REGKEY LIKE '" + student_id + "%' AND OFFEREDCOURSE.COURSEKEY=a.COURSEKEY AND GGRADE2<>'F' AND GGRADE2<>'W' ";       
        return obj_db.get_viewData(sql, "courseList");
    }

    public DataTable get_tableData(string sql, string tableName)
    {
        return obj_db.get_viewData(sql, tableName);
    }

    public double get_total_completed_credit(string sid)
    {
        DataSet ds= new DataSet();
        ds.Merge(get_completed_course_information(sid));

        for(int i=0;i<ds.Tables["courseList"].Rows.Count;i++)
        {
            for (int j=i+1; j<ds.Tables["courseList"].Rows.Count;j++)
            {
                if (ds.Tables["courseList"].Rows[i]["COURSECODE"].ToString() == ds.Tables["courseList"].Rows[j]["COURSECODE"].ToString())
                {
                    if (Convert.ToDouble(ds.Tables["courseList"].Rows[j]["GPOINT"].ToString()) <= Convert.ToDouble(ds.Tables["courseList"].Rows[i]["GPOINT"].ToString()))
                        ds.Tables["courseList"].Rows.RemoveAt(j--);                
                }
            }
        }
        
        double total_credit = 0;
        foreach (DataRow drt in ds.Tables["courseList"].Rows)
        {
            total_credit += Convert.ToDouble(drt["CHOURS"].ToString());
        }

        return total_credit;
    }


    public Double get_latest_cgpa(string sid)
    {
        string sem="", year=""; 
        DataSet ds = new DataSet();
        string sql = " SELECT * FROM REGISTATUS WHERE sid='" + sid + "' ORDER BY year DESC, SEMESTER DESC";        
        ds.Merge(obj_db.get_viewData(sql,"registatus"));

        if (ds.Tables["registatus"].Rows.Count > 0)
        {
            sem = ds.Tables["registatus"].Rows[0]["semester"].ToString(); 
            year = ds.Tables["registatus"].Rows[0]["year"].ToString(); 
            return get_CGPA_upto_semester(sid, year, sem);
        }
        else
        {
            return 0;
        }


    
    }


    public DataTable get_CGPA_CH_upto_semester(string sid, string year, string sem)
    {
        string sql = "select sum(CHOURS) as CHOURS, sum(tcg) as tcg from  ( SELECT distinct VIEW_OFFEREDCOURSE.coursecode,CHOURS, CHOURS*GPOINT AS tcg   "
        + " FROM VIEW_OFFEREDCOURSE,OFFERERINGANDGRADE,REGISTATUS,CHANGEDCREDIT   WHERE REGISTATUS.regkey LIKE '" + sid + "%' AND REGISTATUS.regKey=OFFERERINGANDGRADE.regkey AND   "
        + "  OFFERERINGANDGRADE.courseKey=VIEW_OFFEREDCOURSE.courseKey AND ((REGISTATUS.year<'" + year + "') OR (REGISTATUS.year='" + year + "' AND REGISTATUS.SEMESTER <='" + sem + "'))   "
         + " AND  GGRADE2!='I' AND GGRADE2!='W' AND GGRADE2!='R' AND  GGRADE2!='F' and CHANGEDCREDIT.coursecode=VIEW_OFFEREDCOURSE.coursecode  and CHANGEDCREDIT.FLAG!=1   "
         + " ORDER BY VIEW_OFFEREDCOURSE.coursecode)";



        return obj_db.get_viewData(sql, "courseCH_GPList");


    }

    public DataTable get_CGPA_CH_semesterupto(string sid, string year, string sem)
    {
        string sql = "select * from E_SEMCGPA where SID ='" + sid + "' and Year = '" + year + "' and semester ='" + sem + "' ";
        return obj_db.get_viewData(sql, "CGPList");


    }



    public DataTable get_GPA_upto_semester(string sid, string year, string sem)
    {
        double cgpa = 0;
        double total_credit = 0;
        double total_value = 0;



        string sql = "select sum(CHOURS) as CHOURS, sum(tcg) as tcg from "
                    + " (SELECT SUM(C.CHOURS) AS CHOURS, SUM(C.tcg) AS tcg  "
                    + "FROM (SELECT VIEW_OFFEREDCOURSE.coursecode, CHOURS AS CHOURS, CHOURS*MAX(GPOINT) AS tcg "
                    + "FROM VIEW_OFFEREDCOURSE,OFFERERINGANDGRADE,REGISTATUS,CHANGEDCREDIT   "
                    + "WHERE REGISTATUS.regkey LIKE '" + sid + "%' AND REGISTATUS.regKey=OFFERERINGANDGRADE.regkey AND   "
                    + "OFFERERINGANDGRADE.courseKey=VIEW_OFFEREDCOURSE.courseKey AND    "
                    + "((REGISTATUS.year<'" + year + "')OR(REGISTATUS.year='" + year + "' AND REGISTATUS.SEMESTER <='" + sem + "'))   "
                    + "AND GGRADE2!='I' AND GGRADE2!='W'  and CHANGEDCREDIT.coursecode=VIEW_OFFEREDCOURSE.coursecode  "
                    + "and CHANGEDCREDIT.FLAG!=1 "
                    + "GROUP BY VIEW_OFFEREDCOURSE.coursecode, CHOURS ORDER BY VIEW_OFFEREDCOURSE.coursecode asc) C "

                    + " UNION SELECT SUM(C.CHOURS) AS CHOURS, SUM(C.tcg) AS tcg  "
                    + "FROM (SELECT VIEW_OFFEREDCOURSE.coursecode, CHOURS AS CHOURS, CHOURS*(GPOINT) AS tcg "
                    + "FROM VIEW_OFFEREDCOURSE,OFFERERINGANDGRADE,REGISTATUS,CHANGEDCREDIT   "
                    + "WHERE REGISTATUS.regkey LIKE '" + sid + "%' AND REGISTATUS.regKey=OFFERERINGANDGRADE.regkey AND   "
                    + "OFFERERINGANDGRADE.courseKey=VIEW_OFFEREDCOURSE.courseKey AND    "
                    + "((REGISTATUS.year<'" + year + "')OR(REGISTATUS.year='" + year + "' AND REGISTATUS.SEMESTER <='" + sem + "'))   "
                    + "AND GGRADE2!='I' AND GGRADE2!='W'  and CHANGEDCREDIT.coursecode=VIEW_OFFEREDCOURSE.coursecode  "
                    + "and CHANGEDCREDIT.FLAG =1 "
                    + "ORDER BY VIEW_OFFEREDCOURSE.coursecode asc) C )";


        return obj_db.get_viewData(sql, "courseCGPList");

    }​

    public double get_CGPA_upto_semester(string sid, string year, string sem)
    {
        double cgpa = 0;
        double total_credit = 0;
        double total_value = 0;

        /*string sql = " SELECT g.coursekey, g.ggrade2, g.gtype, g.marks, g.regkey, g.ggroup, ";
        sql += " g.chours, g.gpoint, o.coursecode, r.sid, r.SEMESTER, r.YEAR, C.FLAG ";
        sql += " FROM OFFERERINGANDGRADE g, OFFEREDCOURSE o, REGISTATUS r, CHANGEDCREDIT c    ";
        sql += "  WHERE ((o.coursekey = g.coursekey) AND (r.regkey = g.regkey) AND GGRADE2!='I' AND GGRADE2!='W' AND GGRADE2!='R' ";//AND g.ggrade<>'F'
        sql += " AND (r.YEAR<'" + year + "' OR( r.YEAR='" + year + "'AND r.SEMESTER<='" + sem + "')) AND sid='" + sid + "' )  and  c.coursecode= o.coursecode";



        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData(sql, "courseList"));

        for (int i = 0; i < ds.Tables["courseList"].Rows.Count; i++)
        {
            for (int j = i + 1; j < ds.Tables["courseList"].Rows.Count; j++)
            {
                if (Convert.ToDouble(ds.Tables["courseList"].Rows[i]["FLAG"].ToString()) == 1) continue;

                if (ds.Tables["courseList"].Rows[i]["COURSECODE"].ToString() == ds.Tables["courseList"].Rows[j]["COURSECODE"].ToString())
                {
                    if (Convert.ToDouble(ds.Tables["courseList"].Rows[j]["GPOINT"].ToString()) <= Convert.ToDouble(ds.Tables["courseList"].Rows[i]["GPOINT"].ToString()))
                        ds.Tables["courseList"].Rows.RemoveAt(j--);
                    else
                    {
                        ds.Tables["courseList"].Rows.RemoveAt(i);
                        j--;
                    }
                }
            }
        }

        foreach (DataRow dr in ds.Tables["courseList"].Rows)
        {
            if (dr["GGRADE2"].ToString() != "I" && dr["GGRADE2"].ToString() != "W")
            {
                total_credit += Convert.ToDouble(dr["CHOURS"].ToString());
                total_value += (Convert.ToDouble(dr["CHOURS"].ToString()) * Convert.ToDouble(dr["GPOINT"].ToString()));
            }
        }

        cgpa = total_value / total_credit;
        return cgpa;*/

        string sql = "select sum(CHOURS) as CHOURS, sum(tcg) as tcg from "
                    + " (SELECT SUM(C.CHOURS) AS CHOURS, SUM(C.tcg) AS tcg  "
                    + "FROM (SELECT VIEW_OFFEREDCOURSE.coursecode, CHOURS AS CHOURS, CHOURS*MAX(GPOINT) AS tcg "
                    + "FROM VIEW_OFFEREDCOURSE,OFFERERINGANDGRADE,REGISTATUS,CHANGEDCREDIT   "
                    + "WHERE REGISTATUS.regkey LIKE '" + sid + "%' AND REGISTATUS.regKey=OFFERERINGANDGRADE.regkey AND   "
                    + "OFFERERINGANDGRADE.courseKey=VIEW_OFFEREDCOURSE.courseKey AND    "
                    + "((REGISTATUS.year<'" + year + "')OR(REGISTATUS.year='" + year + "' AND REGISTATUS.SEMESTER <='" + sem + "'))   "
                    + "AND GGRADE2!='I' AND GGRADE2!='W'  and CHANGEDCREDIT.coursecode=VIEW_OFFEREDCOURSE.coursecode  "
                    + "and CHANGEDCREDIT.FLAG!=1 "
                    + "GROUP BY VIEW_OFFEREDCOURSE.coursecode, CHOURS ORDER BY VIEW_OFFEREDCOURSE.coursecode asc) C "

                    + " UNION SELECT SUM(C.CHOURS) AS CHOURS, SUM(C.tcg) AS tcg  "
                    + "FROM (SELECT VIEW_OFFEREDCOURSE.coursecode, CHOURS AS CHOURS, CHOURS*(GPOINT) AS tcg "
                    + "FROM VIEW_OFFEREDCOURSE,OFFERERINGANDGRADE,REGISTATUS,CHANGEDCREDIT   "
                    + "WHERE REGISTATUS.regkey LIKE '" + sid + "%' AND REGISTATUS.regKey=OFFERERINGANDGRADE.regkey AND   "
                    + "OFFERERINGANDGRADE.courseKey=VIEW_OFFEREDCOURSE.courseKey AND    "
                    + "((REGISTATUS.year<'" + year + "')OR(REGISTATUS.year='" + year + "' AND REGISTATUS.SEMESTER <='" + sem + "'))   "
                    + "AND GGRADE2!='I' AND GGRADE2!='W'  and CHANGEDCREDIT.coursecode=VIEW_OFFEREDCOURSE.coursecode  "
                    + "and CHANGEDCREDIT.FLAG =1 "
                    + "ORDER BY VIEW_OFFEREDCOURSE.coursecode asc) C )";

        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData(sql, "courseList"));

        if (ds.Tables["courseList"].Rows.Count > 0)
        {
			try {
				total_credit = Convert.ToDouble(ds.Tables["courseList"].Rows[0]["CHOURS"].ToString());
				total_value = Convert.ToDouble(ds.Tables["courseList"].Rows[0]["tcg"].ToString());

				cgpa = total_value / total_credit;
				return cgpa;
			} catch(Exception ex){}
        }

        return 0;
    }



    public DataTable get_SSC_information(string student_id)
    {
        string sql = " SELECT * FROM ACADEMICBACK_SSC where SID='" + student_id + "' ";
        return obj_db.get_viewData(sql, "ACADEMICBACK_SSC");
    }


    public DataTable get_all_filterStaff(string status, string category, string department,string type)
    {
        string sql="";

        if(type=="All")
            sql = " SELECT * FROM WEB_TEACHER_STAFF,COLLEGE where (COLLEGECODE=DEPARTMENT) and   STAFF_CTRL='" + status + "' and JOB_CATEGORY='" + category + "' and DEPARTMENT='" + department + "' ";
        else
            sql = " SELECT * FROM WEB_TEACHER_STAFF,COLLEGE where (COLLEGECODE=DEPARTMENT) and JOB_TYPE='" + type + "'  and   STAFF_CTRL='" + status + "' and JOB_CATEGORY='" + category + "' and DEPARTMENT='" + department + "' ";

        return obj_db.get_viewData(sql, "WEB_TEACHER_STAFFs");
    }

    public DataTable get_all_Staff(string status, string category, string department, string type)
    {
        string sql = "";

        if (department == "0" || department == null)
        {
            if (type == "All")
                sql = "   SELECT * FROM WEB_TEACHER_STAFF  left join COLLEGE on COLLEGE.COLLEGECODE=WEB_TEACHER_STAFF.DEPARTMENT  where   STAFF_CTRL='" + status + "' and JOB_CATEGORY='" + category + "'  order by value";
           
              //  sql = " SELECT * FROM WEB_TEACHER_STAFF,COLLEGE where (COLLEGECODE=DEPARTMENT) and    STAFF_CTRL='" + status + "' and JOB_CATEGORY='" + category + "'   order by value  ";
            else

                sql = "   SELECT * FROM WEB_TEACHER_STAFF  left join COLLEGE on COLLEGE.COLLEGECODE=WEB_TEACHER_STAFF.DEPARTMENT  where JOB_TYPE='" + type + "'  and   STAFF_CTRL='" + status + "' and JOB_CATEGORY='" + category + "'  order by value";
             //   sql = " SELECT * FROM WEB_TEACHER_STAFF,COLLEGE where (COLLEGECODE=DEPARTMENT) and JOB_TYPE='" + type + "'  and   STAFF_CTRL='" + status + "' and JOB_CATEGORY='" + category + "'  order by value";
      
        }
        else
        {
            if (type == "All")
                sql = " SELECT * FROM WEB_TEACHER_STAFF,COLLEGE where (COLLEGECODE=DEPARTMENT) and (DEPARTMENT='" + department + "'   ) and    STAFF_CTRL='" + status + "' and JOB_CATEGORY='" + category + "'   order by value  ";
            else
                sql = " SELECT * FROM WEB_TEACHER_STAFF,COLLEGE where (COLLEGECODE=DEPARTMENT) and JOB_TYPE='" + type + "' and (DEPARTMENT='" + department + "'     )   and   STAFF_CTRL='" + status + "' and JOB_CATEGORY='" + category + "'  order by value";
        }
        return obj_db.get_viewData(sql, "WEB_TEACHER_STAFFs");
    }

    public DataTable get_a_teacherInfo(string staffId)
    {
        string sql = " SELECT * FROM WEB_TEACHER_STAFF  where STAFF_ID='" + staffId + "'  ";
        return obj_db.get_viewData(sql, "WEB_TEACHER_STAFFs");
    }

    public DataTable get_HSC_information(string student_id)
    {
        string sql = " SELECT * FROM ACADEMICBACK_HSC where SID='" + student_id + "' ";
        return obj_db.get_viewData(sql, "ACADEMICBACK_HSC");
    }

    public DataTable get_oLevel_information(string student_id)
    {
        string sql = " SELECT * FROM ACADEMICBACK_OLEVEL where SID='" + student_id + "' ";
        return obj_db.get_viewData(sql, "ACADEMICBACK_OLEVEL");
    }

    public DataTable get_ALevel_information(string student_id)
    {
        string sql = " SELECT * FROM ACADEMICBACK_ALEVEL where SID='" + student_id + "' ";
        return obj_db.get_viewData(sql, "ACADEMICBACK_ALEVEL");
    }

    public DataTable get_otherLevel_information(string student_id)
    {
        string sql = " SELECT * FROM ACADEMICBACK_OTHERS where SID='" + student_id + "' ";
        return obj_db.get_viewData(sql, "ACADEMICBACK_OTHERS");
    }


    public string delete_allocated_Course(string ids)
    {
        return obj_db.execute_query(" Delete from WEB_COURSE_MATERIALS_TEACHER where COURSE_MATERIALS_ID='" + ids + "' ");

       
    }

    public DataTable get_course_outline(string course_teacher_id)
    {
        string sql = " Select * from WEB_COURSE_MATERIALS_TEACHER where COURSE_TEACHER_ID='" + course_teacher_id + "' and TYPE=3 ";
        return obj_db.get_viewData(sql, "outline");        
    }

    public string execute_query(string query)
    {       
        return obj_db.execute_query(query);
    }


    public DataTable get_all_course_ofa_Department(string depCode_id)
    {
        string sql = " Select * from WEB_DEPARTMENT_COURSES where DEPCODE='" +new cls_tools().get_department_code(depCode_id) + "' ";
        return obj_db.get_viewData(sql, "WEB_DEPARTMENT_COURSES");
    }


     public DataTable getFinalCGPA(string sid)
    {
        string sql = "select VCGP.SID,VCGP.cgpa, Case When S.TR_CR_ELIGIBLE  = 1 then TCGPA ELSE CGPA END as FINAL_CGPA, VCGP.TCGPA, VCGP.COMP_CHRS, VCGP.TCHRS, VCGP.WCHRS, VCGP.TOTALCHRS,VCGP.HASF, VCGP.FCHRS ,substr(VCGP.SID, 1,3) SUB_ID from VW_GET_CGPA_F VCGP left join Student S on S.sid =VCGP.SID where VCGP.SID = '" + sid + "'";
        return obj_db.get_viewData(sql, "FinalCGPA");
    }


     public DataTable getStudentinfo(string sid)
     {
         string sql = "select distinct S.SID, S.Sname, S.GENDER,C_PROGINDEPT.Name Program, Case When S.GENDER  = 'M' then MALERATE WHEN S.GENDER = 'F' then FEMRATE end as Credit_Rate from student S left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID  left join CREDITRATE on CREDITRATE.YEAR = S.ADMINYEAR and CREDITRATE.SEMESTER = S.ADMINSEMETER and CREDITRATE.DEPCODE = S.SPROGRAM where S.sid = '" + sid + "'";
         return obj_db.get_viewData(sql, "Studentinfo");
     }


    public DataTable getAttemptCH(string sid)
    {
        string sql = "select SUM(og.CHOURS) as attempted from OFFERERINGANDGRADE og, registatus r, Student S where og.REGKEY= r.REGKEY and S.SID = r.SID and S.SID='" + sid + "'";
        return obj_db.get_viewData(sql, "AttemptCH");
    }


    public DataTable getRequiredCH(string sid)
    {
       // string sql = "select S.SID, S.SPROGRAM, DC.REQCREDITHRS from Student S, DEPARTMENTINCOLLEGE DC where S.SPROGRAM = DC.DEPCODE and S.SID='" + sid + "'";
        //string sql = " select distinct S.SID, S.SPROGRAM, DEPTC.*,CASE WHEN CNGCH.REQCREDITHRS is not null THEN CNGCH.REQCREDITHRS  "
        //            + " WHEN CNGCH.REQCREDITHRS is null THEN DEPTC.REQCREDITHRS END reqCH, CNGCH.REQCREDITHRS    "
        //            + " from DEPARTMENTINCOLLEGE DEPTC left join CHANGE_REQCREDITHRS  CNGCH on DEPTC.DEPID = CNGCH.DEPID  "
        //            + " left join student S on S.SPROGRAM = DEPTC.DEPCODE  where S.SID='" + sid + "'";


       string sql = "  select distinct S.SID, S.SPROGRAM, DEPTC.*, C_PROGINDEPT.*, CASE WHEN CNGCH.REQCREDITHRS is not null THEN cngch.reqcredithrs   WHEN CNGCH.REQCREDITHRS is null THEN TO_CHAR(C_PROGINDEPT.REQCREDITHRS) END reqCH,  "
                 + " CNGCH.REQCREDITHRS from Student S   left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID    "
                 + " left join C_DEPARTMENTINFACULTY DEPTC on DEPTC.DEPID =  C_PROGINDEPT.DEPID left join CHANGE_REQCREDITHRS  CNGCH on  (C_PROGINDEPT.C_PROGINDEPT_ID=CNGCH.DEPID  and CNGCH.BATCH = SUBSTR(S.SID, 1,3) )  "
                 + " where S.SID = '" + sid + "'";
        return obj_db.get_viewData(sql, "RequiredCH");
    }


public double get_latest_yearSemister()
    {
        double year_sem = 0;
        DataSet ds = new DataSet();

        //string sql = "select * from( SELECT substr(o.COURSEKEY,2,4)|| substr(o.COURSEKEY,1,1) as yearSem, substr(c.CHANGEYEAR,3)||c.CHANGESEMESTER as year_Sem, o.COURSEKEY, c.* FROM OFFEREDCOURSE o, CHANGEDCOURSENAME c WHERE o.COURSECODE=c.COURSECODE order by  substr(o.COURSEKEY,2,4)||substr(o.COURSEKEY,1,1) desc, c.CHANGEYEAR desc, c.CHANGESEMESTER desc)  where rownum = 1";

       string sql = "select * from (SELECT substr(Student.SID,1,3) as yearSem,C_PROGINDEPT.DEGREENAME , Student.* FROM Student  "
         + " left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = Student.PROGRAM_ID  left join C_DEPARTMENTINFACULTY DEPTC on DEPTC.DEPID =  C_PROGINDEPT.DEPID  "
         + " where C_PROGINDEPT.DEGREENAME not like '%Master%' order by SID desc) where rownum=1; ";

        ds.Merge(obj_db.get_viewData(sql, "Student"));

        if (ds.Tables["Student"].Rows.Count > 0)
        {
            year_sem = Convert.ToDouble(ds.Tables["Student"].Rows[0]["yearSem"].ToString());
            return year_sem;
        }
        else
        {
            return 0;
        }



    }





    public  string Encrypt(string InputText, string KeyString)
    {

        MemoryStream memoryStream = null;
        CryptoStream cryptoStream = null;
        try
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.KeySize = 128;
                AES.BlockSize = 128;
                byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(KeyString, Encoding.ASCII.GetBytes(KeyString.Length.ToString()));
                using (ICryptoTransform Encryptor = AES.CreateEncryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16)))
                {
                    using (memoryStream = new MemoryStream())
                    {
                        using (cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(PlainText, 0, PlainText.Length);
                            cryptoStream.FlushFinalBlock();
                            return Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                }
            }

        }
        catch
        {
            throw;
        }
        finally
        {
            if (memoryStream != null)
                memoryStream.Close();
            if (cryptoStream != null)
                cryptoStream.Close();
        }
    }

    public  string Decrypt(string InputText, string KeyString)
    {
        MemoryStream memoryStream = null;
        CryptoStream cryptoStream = null;
        try
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.KeySize = 128;
                AES.BlockSize = 128;
                byte[] EncryptedData = Convert.FromBase64String(InputText);
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(KeyString, Encoding.ASCII.GetBytes(KeyString.Length.ToString()));
                using (ICryptoTransform Decryptor = AES.CreateDecryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16)))
                {
                    using (memoryStream = new MemoryStream(EncryptedData))
                    {
                        using (cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read))
                        {
                            byte[] PlainText = new byte[EncryptedData.Length];
                            return Encoding.Unicode.GetString(PlainText, 0, cryptoStream.Read(PlainText, 0, PlainText.Length));
                        }
                    }
                }
            }

        }
        catch
        {
            throw;
        }
        finally
        {
            if (memoryStream != null)
                memoryStream.Close();
            if (cryptoStream != null)
                cryptoStream.Close();
        }
    }
}

