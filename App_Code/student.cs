using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.Common;


public class student : System.Web.Services.WebService
{
    Dts obj_db;
    
    public student()
    {
        obj_db = new Dts();
    }

    //count registered male female Program wise
    public DataTable get_MaleFemaleList(Int32 Semester, Int32 Year, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" SELECT C_ALL.SPROGRAM, NVL(sum(C_ALL.Male),0) Male, NVL(sum(C_ALL.Female),0) Female,  NVL(sum(C_ALL.Male),0)+ NVL(sum(C_ALL.Female),0) Total   ";
        query.CommandText += " from (select distinct S.SID,S.SPROGRAM,  CASE WHEN  S.GENDER = 'M'  THEN count(S.SID)  END as Male, CASE  WHEN  S.GENDER = 'F'  THEN count(S.SID)  END as Female   ";
        query.CommandText += " from registatus R left join Student S on S.SID=R.SID where R.REGKEY like '%'||?||?   ";
        query.CommandText += " group by S.SID,S.GENDER,S.SPROGRAM)C_ALL group by C_ALL.SPROGRAM order by C_ALL.SPROGRAM ";

        ds = obj_db.Table_GetAll(query.CommandText, Semester, Year, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    public DataTable get_ConvocationStudent(string student_id, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" SELECT EU_CONVOCATION.*, Student.EMAIL EmailAdd, Student.PHONE FROM EU_CONVOCATION  left join student on student.SID = EU_CONVOCATION.SID WHERE EU_CONVOCATION.sid=? ";

        ds = obj_db.Table_GetAll_Departmental(query.CommandText, student_id, tableName);

        return ds;
    }

    public DataTable get_T_PermittedStudent(string student_id, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select * from EU_COVOSTUDENTADD where SID= ? and year= 2019  ";

        ds = obj_db.Table_GetAll_Departmental(query.CommandText, student_id, tableName);

        return ds;
    }

    public DataTable get_T_STUDENTDEBIT(string student_id, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select * from T_studentdebit where SID= ? and year= 2019 and semester =1 and status='S' and HEADSN='28' ";

        ds = obj_db.Table_GetAll_Departmental(query.CommandText, student_id, tableName);

        return ds;
    }

    public DataTable get_allCollege(string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" Select * from COLLEGE order by COLLEGENAME asc";

        ds = obj_db.Table_CollegeGetAll(query.CommandText, tableName);

        return ds;


        //string sql = " Select * from COLLEGE order by COLLEGENAME asc  ";
       // return obj_db.get_viewData(sql, "COLLEGE");
    }
    public DataTable get_StaffInfo(string EmployeeID, string leaveType,string year, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"SELECT   LD.NAME, LD.LEAVE_ID, TS.VALUE, TS.STAFF_NAME,NVL(NVL(HB.BALANCE,LD.BALANCE),0) TOTAL_BALANCE,TS.JOB_DESIGNATION,
                                NVL(SUM( (TO_DATE- FROM_DATE )+1 ),0) TAKEN,                    
                        CASE 
                          WHEN LD.LEAVE_ID = 1 AND CONFIRMATION_DATE is not null OR (sysdate - JOIN_DATE > 365) 
                            THEN NVL(HB.BALANCE,LD.BALANCE) - NVL(SUM( (TO_DATE-FROM_DATE)+1 ),0)
                          WHEN LD.LEAVE_ID = 1 AND CONFIRMATION_DATE is null 
                            THEN floor((floor(sysdate - JOIN_DATE) )*(15/365)) - NVL(SUM( (TO_DATE-FROM_DATE)+1 ),0)
                         WHEN LD.LEAVE_ID = 2 AND CONFIRMATION_DATE is not null OR (sysdate - JOIN_DATE > 365) 
                            THEN NVL(HB.BALANCE,LD.BALANCE) - NVL(SUM( (TO_DATE-FROM_DATE)+1 ),0)
                          WHEN LD.LEAVE_ID = 2 AND CONFIRMATION_DATE is null 
                            THEN floor((floor(sysdate - JOIN_DATE) )*(7/365)) - NVL(SUM( (TO_DATE-FROM_DATE)+1 ),0)  
                            
                          ELSE NVL(  NVL(HB.BALANCE,LD.BALANCE),0) - NVL(SUM( (TO_DATE-FROM_DATE)+1 ),0) 
                       END REMAINING_BALANCE                                                       
                                FROM 
                                HR_LEAVE_MASTER LD 
                                JOIN WEB_TEACHER_STAFF TS ON 1 = 1  
                                LEFT JOIN HR_STAFF_LEAVE_INFO HLI ON (TS.VALUE = HLI.STAFF_ID AND LD.LEAVE_ID = HLI.LEAVE_ID and HLI.IS_ACTIVE=1 )
                                AND ((DECODE(TS.CONFIRMATION_DATE,NULL,TO_CHAR(TS.JOIN_DATE,'YYYY'),TO_CHAR(SYSDATE, 'YYYY')) = TO_CHAR(FROM_DATE, 'YYYY') 
                                AND DECODE(TS.CONFIRMATION_DATE,NULL,TO_CHAR(TS.JOIN_DATE,'YYYY'),TO_CHAR(SYSDATE, 'YYYY')) = TO_CHAR(TO_DATE, 'YYYY')) 
                                OR TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(TO_DATE, 'YYYY') 
                                )
                                LEFT JOIN HR_LEAVE_BALANCE HB ON TS.VALUE = HB.EMPLOYEE_ID AND LD.LEAVE_ID = HB.LEAVE_ID AND HB.YEAR =?
                                WHERE
                                TS.VALUE= ? and LD.LEAVE_ID=?
                                GROUP BY LD.NAME, LD.LEAVE_ID, TS.VALUE, TS.STAFF_NAME, LD.BALANCE, TS.JOB_DESIGNATION, HB.BALANCE,CONFIRMATION_DATE,JOIN_DATE  
                                order by LD.LEAVE_ID asc";

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, year, EmployeeID, leaveType, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    public DataTable get_STAFF_INFO(string year, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"SELECT  TS.STAFF_ID, TS.STAFF_NAME, TS.VALUE, TS.VALUE||' - '||TS.STAFF_NAME Name, TS.JOIN_DATE, TS.CONFIRMATION_DATE,
                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 7), 0) Earned,
                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 3), 0) Leave_without_Pay,
                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 5), 0) Semester_Brk,
                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 6), 0) Duty_Lv,
                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 8), 90) Maternity_Lv,

                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 2), 
                     (
                        CASE 
                          WHEN CONFIRMATION_DATE is not null OR (sysdate - JOIN_DATE > 365) THEN 7
                          WHEN CONFIRMATION_DATE is null THEN floor((floor(sysdate - JOIN_DATE) )*(7/365))
                       END
                       )
                     
                     ) Medical,
                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 1), 
                       (
                                    CASE 
                                        WHEN CONFIRMATION_DATE is not null OR (sysdate - JOIN_DATE > 365) THEN 15
                                        WHEN CONFIRMATION_DATE is null THEN floor((floor(sysdate - JOIN_DATE) )*(15/365))
                                    END
                                    )
                                    ) Casual
                    FROM WEB_TEACHER_STAFF TS                               
                    WHERE STAFF_CTRL=1  and value not like 'HRT%' 
                    ORDER BY VALUE ASC ";
        ds = obj_db.Table_get_STAFF_INFO(query.CommandText, year, year, year, year, year, year, year, tableName);

        return ds;
    //    return obj_db.get_viewData(sql, "STAFFINFO");
    }
    public DataTable get_StaffLeaveInfo(string EmployeeID, string Year, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"SELECT   LD.NAME, LD.LEAVE_ID, TS.VALUE, TS.STAFF_NAME,NVL(HB.BALANCE,LD.BALANCE) TOTAL_BALANCE,TS.JOB_DESIGNATION,
                                NVL(SUM( (TO_DATE- FROM_DATE )+1 ),0) TAKEN,                    
                        CASE 
                          WHEN LD.LEAVE_ID = 1 AND CONFIRMATION_DATE is not null OR (sysdate - JOIN_DATE > 365) 
                            THEN NVL(HB.BALANCE,LD.BALANCE) - NVL(SUM( (TO_DATE-FROM_DATE)+1 ),0)
                          WHEN LD.LEAVE_ID = 1 AND CONFIRMATION_DATE is null 
                            THEN floor((floor(sysdate - JOIN_DATE) )*(15/365)) - NVL(SUM( (TO_DATE-FROM_DATE)+1 ),0)
                         WHEN LD.LEAVE_ID = 2 AND CONFIRMATION_DATE is not null OR (sysdate - JOIN_DATE > 365) 
                            THEN NVL(HB.BALANCE,LD.BALANCE) - NVL(SUM( (TO_DATE-FROM_DATE)+1 ),0)
                          WHEN LD.LEAVE_ID = 2 AND CONFIRMATION_DATE is null 
                            THEN floor((floor(sysdate - JOIN_DATE) )*(7/365)) - NVL(SUM( (TO_DATE-FROM_DATE)+1 ),0)  
                            
                         ELSE NVL(  NVL(HB.BALANCE,LD.BALANCE),0) - NVL(SUM( (TO_DATE-FROM_DATE)+1 ),0) 
                       END REMAINING_BALANCE                                                       
                                FROM 
                                HR_LEAVE_MASTER LD 
                                JOIN WEB_TEACHER_STAFF TS ON 1 = 1  
                                LEFT JOIN HR_STAFF_LEAVE_INFO HLI ON (TS.VALUE = HLI.STAFF_ID AND LD.LEAVE_ID = HLI.LEAVE_ID and HLI.IS_ACTIVE=1  )
                                AND ((DECODE(TS.CONFIRMATION_DATE,NULL,TO_CHAR(TS.JOIN_DATE,'YYYY'),TO_CHAR(SYSDATE, 'YYYY')) = TO_CHAR(FROM_DATE, 'YYYY') 
                                AND DECODE(TS.CONFIRMATION_DATE,NULL,TO_CHAR(TS.JOIN_DATE,'YYYY'),TO_CHAR(SYSDATE, 'YYYY')) = TO_CHAR(TO_DATE, 'YYYY')) 
                                OR TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(TO_DATE, 'YYYY') 
                                )
                                LEFT JOIN HR_LEAVE_BALANCE HB ON TS.VALUE = HB.EMPLOYEE_ID AND LD.LEAVE_ID = HB.LEAVE_ID AND HB.YEAR =?
                                WHERE
                                TS.VALUE= ?
                                GROUP BY LD.NAME, LD.LEAVE_ID, TS.VALUE, TS.STAFF_NAME, LD.BALANCE, TS.JOB_DESIGNATION, HB.BALANCE,CONFIRMATION_DATE,JOIN_DATE  
                                order by LD.LEAVE_ID asc ";

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText,Year, EmployeeID, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }
    /*  public DataTable get_AttendanceEntryAll(string SemesterYear, string College, string tableName)
   {
       OracleCommand query = new OracleCommand();
       DataTable ds = new DataTable();

       query.CommandText = @"select distinct SUBSTR(WC.COURSE_KEY,6) COURSE_KEY, CNAME, WC.TEACHER_ID,WEB_TEACHER_STAFF.VALUE, WEB_TEACHER_STAFF.STAFF_NAME,COLLEGE.COLLEGENAME,
                           WC.SECTION,WC.COURSE_TEACHER_ID, NVL(OC.totalstd,0) totalstd
                           , count(atdEntry.class_date) taken
                           from web_course_teacher WC
                           left join  
                           (select count(regkey) totalstd, coursekey, ggroup from OFFERERINGANDGRADE
                           group by coursekey, ggroup
                           )OC on (OC.COURSEKEY = WC.COURSE_KEY and OC.GGROUP=WC.SECTION)
                           left join 
                           (
                           select distinct class_date ,
                           coursekey, Section, COURSE_TEACHER_ID from WEB_STUDENT_ATTENDANCE

                           ) atdEntry on (atdEntry.COURSEKEY = OC.COURSEKEY and atdEntry.section=OC.GGROUP)
                           left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID =WC.TEACHER_ID
                           left join COLLEGE on COLLEGE.COLLEGECODE=WEB_TEACHER_STAFF.DEPARTMENT
                           left join OFFEREDCOURSE on OFFEREDCOURSE.COURSEKEY=WC.COURSE_KEY   
                           left join CHANGEDCOURSENAME on OFFEREDCOURSE.COURSECODE=CHANGEDCOURSENAME.COURSECODE 

                           where WC.COURSE_KEY like  ?||'%' and COLLEGE.COLLEGECODE = ?

                           and WC.Section !='Waiting Group'
                           GROUP by WEB_TEACHER_STAFF.STAFF_NAME,COLLEGE.COLLEGENAME,WC.COURSE_KEY,WC.TEACHER_ID, WC.SECTION,WC.COURSE_TEACHER_ID, OC.totalstd,
                           atdEntry.COURSEKEY,atdEntry.section,CNAME,WEB_TEACHER_STAFF.VALUE
                           ORDER by COLLEGE.COLLEGENAME asc, WEB_TEACHER_STAFF.VALUE asc  ";

       ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, SemesterYear, College, tableName);

       return ds;

       //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
   }
   public DataTable get_AttendanceEntry(string Semester, string Year, string tableName)
   {
       OracleCommand query = new OracleCommand();
       DataTable ds = new DataTable();

       query.CommandText = @"select distinct SUBSTR(WC.COURSE_KEY,6) COURSE_KEY, CNAME, WC.TEACHER_ID,WEB_TEACHER_STAFF.VALUE, WEB_TEACHER_STAFF.STAFF_NAME,COLLEGE.COLLEGENAME,
                           WC.SECTION,WC.COURSE_TEACHER_ID, NVL(OC.totalstd,0) totalstd
                           , count(atdEntry.class_date) taken
                           from web_course_teacher WC
                           left join  
                           (select count(regkey) totalstd, coursekey, ggroup from OFFERERINGANDGRADE
                           group by coursekey, ggroup
                           )OC on (OC.COURSEKEY = WC.COURSE_KEY and OC.GGROUP=WC.SECTION)
                           left join 
                           (
                           select distinct class_date ,
                           coursekey, Section, COURSE_TEACHER_ID from WEB_STUDENT_ATTENDANCE

                           ) atdEntry on (atdEntry.COURSEKEY = OC.COURSEKEY and atdEntry.section=OC.GGROUP)
                           left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID =WC.TEACHER_ID
                           left join COLLEGE on COLLEGE.COLLEGECODE=WEB_TEACHER_STAFF.DEPARTMENT
                           left join OFFEREDCOURSE on OFFEREDCOURSE.COURSEKEY=WC.COURSE_KEY   
                           left join CHANGEDCOURSENAME on OFFEREDCOURSE.COURSECODE=CHANGEDCOURSENAME.COURSECODE 

                           where WC.COURSE_KEY like  ?||?||'%'

                           and WC.Section !='Waiting Group'
                           GROUP by WEB_TEACHER_STAFF.STAFF_NAME,COLLEGE.COLLEGENAME,WC.COURSE_KEY,WC.TEACHER_ID, WC.SECTION,WC.COURSE_TEACHER_ID, OC.totalstd,
                           atdEntry.COURSEKEY,atdEntry.section,CNAME,WEB_TEACHER_STAFF.VALUE
                           ORDER by COLLEGE.COLLEGENAME asc, WEB_TEACHER_STAFF.VALUE asc  ";

       ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, Semester, Year, tableName);

       return ds;

       //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
   }*/
    public DataTable get_AttendanceEntryAll(string SemesterYear, string College, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();
        query.CommandText = @"select distinct SUBSTR(WC.COURSE_KEY,6) COURSE_KEY,
                        CHANGEDCOURSENAME.CNAME, 
                        WC.TEACHER_ID,
                        WEB_TEACHER_STAFF.VALUE,
                        WEB_TEACHER_STAFF.STAFF_NAME,COLLEGE.COLLEGENAME,
                        WC.SECTION,
                        WC.COURSE_TEACHER_ID,
                        NVL(OC.totalstd,0) totalstd,NVL(CountTakenCls.taken,0) taken,  NVL(VWSTD.TAKENCLASS ,0) TAKENCLASS
                        from web_course_teacher WC
                        left join  
                          (
                            select count(regkey) totalstd, coursekey, ggroup from OFFERERINGANDGRADE
                            group by coursekey, ggroup
                          )
                          OC on (OC.COURSEKEY = WC.COURSE_KEY and OC.GGROUP=WC.SECTION)
    
                          left join 
                          (
                              select count(takenCls.class_date) taken,TAKENCLS.COURSE_TEACHER_ID
                              from
                              (
                                Select DISTINCT class_date ,  coursekey, Section, COURSE_TEACHER_ID from WEB_STUDENT_ATTENDANCE
                              )takenCls GROUP by TAKENCLS.COURSE_TEACHER_ID
      
                          )CountTakenCls  on COUNTTAKENCLS.COURSE_TEACHER_ID=WC.COURSE_TEACHER_ID
                        left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID =WC.TEACHER_ID
                        left join COLLEGE on COLLEGE.COLLEGECODE=WEB_TEACHER_STAFF.DEPARTMENT
                        left join OFFEREDCOURSE on OFFEREDCOURSE.COURSEKEY=WC.COURSE_KEY   
                        left join CHANGEDCOURSENAME on OFFEREDCOURSE.COURSECODE=CHANGEDCOURSENAME.COURSECODE 
                        left join WEB_STUDENT_CLSATTENDANCE VWSTD on VWSTD.GGROUP = WC.SECTION and VWSTD.COURSEKEY =WC.COURSE_KEY
                        where WC.COURSE_KEY like  ?||'%' and COLLEGE.COLLEGECODE = ?
                        and WC.Section !='Waiting Group'
                        ORDER by COLLEGE.COLLEGENAME asc, WEB_TEACHER_STAFF.VALUE asc  ";
      

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, SemesterYear, College, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }
    public DataTable get_AttendanceEntry(string Semester, string Year, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

                            query.CommandText = @"select distinct SUBSTR(WC.COURSE_KEY,6) COURSE_KEY,
                        CHANGEDCOURSENAME.CNAME, 
                        WC.TEACHER_ID,
                        WEB_TEACHER_STAFF.VALUE,
                        WEB_TEACHER_STAFF.STAFF_NAME,COLLEGE.COLLEGENAME,
                        WC.SECTION,
                        WC.COURSE_TEACHER_ID,
                        NVL(OC.totalstd,0) totalstd,NVL(CountTakenCls.taken,0) taken,NVL(VWSTD.TAKENCLASS ,0) TAKENCLASS
                        from web_course_teacher WC
                        left join  
                          (
                            select count(regkey) totalstd, coursekey, ggroup from OFFERERINGANDGRADE
                            group by coursekey, ggroup
                          )
                          OC on (OC.COURSEKEY = WC.COURSE_KEY and OC.GGROUP=WC.SECTION)
    
                          left join 
                          (
                              select count(takenCls.class_date) taken,TAKENCLS.COURSE_TEACHER_ID
                              from
                              (
                                Select DISTINCT class_date ,  coursekey, Section, COURSE_TEACHER_ID from WEB_STUDENT_ATTENDANCE
                              )takenCls GROUP by TAKENCLS.COURSE_TEACHER_ID
      
                          )CountTakenCls  on COUNTTAKENCLS.COURSE_TEACHER_ID=WC.COURSE_TEACHER_ID
                        left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID =WC.TEACHER_ID
                        left join COLLEGE on COLLEGE.COLLEGECODE=WEB_TEACHER_STAFF.DEPARTMENT
                        left join OFFEREDCOURSE on OFFEREDCOURSE.COURSEKEY=WC.COURSE_KEY   
                        left join CHANGEDCOURSENAME on OFFEREDCOURSE.COURSECODE=CHANGEDCOURSENAME.COURSECODE 
                        left join WEB_STUDENT_CLSATTENDANCE VWSTD on VWSTD.GGROUP = WC.SECTION and VWSTD.COURSEKEY =WC.COURSE_KEY
                        where WC.COURSE_KEY like  ?||'%'
                        and WC.Section !='Waiting Group'
                        ORDER by COLLEGE.COLLEGENAME asc, WEB_TEACHER_STAFF.VALUE asc  ";

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, Semester, Year, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }
    public DataTable get_StaffLeaveInfodetails(string EmployeeID, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"SELECT  distinct HLI.LEAVE_INFO_ID, LD.NAME,LD.LEAVE_ID,TS.VALUE, FROM_DATE,TO_DATE,
                               NVL(SUM( (TO_DATE- FROM_DATE )+1 ),0) TAKEN  
                                FROM 
                                HR_LEAVE_MASTER LD 
                                JOIN WEB_TEACHER_STAFF TS ON 1 = 1  
                                LEFT JOIN HR_STAFF_LEAVE_INFO HLI ON (TS.VALUE = HLI.STAFF_ID AND LD.LEAVE_ID = HLI.LEAVE_ID  )
                                AND ((TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(FROM_DATE, 'YYYY')
                                AND TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(TO_DATE, 'YYYY')) 
                                OR TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(TO_DATE, 'YYYY') )
                                WHERE
                                TS.VALUE =? and HLI.IS_ACTIVE =1
                                GROUP BY HLI.LEAVE_INFO_ID, LD.NAME,LD.LEAVE_ID,TS.VALUE, FROM_DATE,TO_DATE
                                order by FROM_DATE asc, LD.LEAVE_ID asc";

        ds = obj_db.Table_GetAll_Departmental(query.CommandText, EmployeeID, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    



    public DataTable get_StaffLeaveBalance(string EmployeeID, string leaveType, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

       query.CommandText = @"SELECT LD.NAME, LD.LEAVE_ID, TS.VALUE, TS.STAFF_NAME, TS.JOB_DESIGNATION, LD.BALANCE,  FROM_DATE, TO_DATE
                        FROM   HR_LEAVE_MASTER LD   JOIN WEB_TEACHER_STAFF TS ON 1 = 1  
                        LEFT JOIN HR_STAFF_LEAVE_INFO HLI ON TS.VALUE = HLI.STAFF_ID AND LD.LEAVE_ID = HLI.LEAVE_ID  
                        WHERE TS.VALUE=? and HLI.LEAVE_ID=?  and  ((TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(FROM_DATE, 'YYYY') AND TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(TO_DATE, 'YYYY')) 
                        OR TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(TO_DATE, 'YYYY') ) ";

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText,EmployeeID, leaveType,  tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    public DataTable get_StudentDueList(Int32 C_PROGINDEPT_ID, Int32 Batch, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select SID, Sname, PHONE, C_PROGINDEPT.Name Program, C_PROGINDEPT.C_PROGINDEPT_ID from student left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID=STUDENT.PROGRAM_ID where C_PROGINDEPT.C_PROGINDEPT_ID=? and Student.SID like ?||'%' ";

        ds = obj_db.Table_GetAll(query.CommandText, C_PROGINDEPT_ID, Batch, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }
    public DataTable get_StudentDueListTotal(Int32 year, Int32 semester, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" Select distinct   DUELIST.SID,NVL(DUELIST.DUE,0) DUE,student.Sname,STUDENT.SPROGRAM PROGRAM, STUDENT.PHONE, STUDENT.ADDRESS   from
                                (select distinct R.SID, FN_GET_PER_SEM_DUE_NEW(R.SID, ?,?) DUE 
                                from ( select distinct REGISTATUS.SID from REGISTATUS   )R
                                )DUELIST
                                left join student on student.sid =DUELIST.SID order by STUDENT.SPROGRAM asc,DUELIST.sid desc ";

        ds = obj_db.Table_GetAll(query.CommandText, year, semester, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    public DataTable get_StudenCompletedtDueList(Int32 Semester, Int32 Year, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select REGISTATUS.SID, student.SNAME Student_NAME,STUDENT.PHONE, student.SPROGRAM PROGRAM,
                            sum(T_STUDENTBALANCE.PAYABLE) Total_Receivable, sum(T_STUDENTBALANCE.PAID) Received,
                            sum(T_STUDENTBALANCE.PAYABLE)-sum(T_STUDENTBALANCE.PAID) DUES from REGISTATUS
                            left join student on student.sid = REGISTATUS.SID
                            left join T_STUDENTBALANCE on T_STUDENTBALANCE.SID=REGISTATUS.SID
                            where REGKEY like '%'||?||?
                            and T_STUDENTBALANCE.YEAR||T_STUDENTBALANCE.SEMESTER <= ?||?
                            GROUP by REGISTATUS.SID, student.SNAME,student.SPROGRAM,STUDENT.PHONE
                            order by SID desc ";

        ds = obj_db.Table_GetAll_DEPWISE_NEW(query.CommandText, Semester, Year,Year,Semester, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }
   
    public DataTable get_OfferingCourseDetails(string COURSE_KEY, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select distinct  WTS.DEPARTMENT,OC.COURSEKEY, CNG.COURSECODE, CNG.CNAME ,WCT.COURSE_TEACHER_ID,WCT.SECTION,  WCT.TEACHER_ID, WTS.STAFF_NAME,  WCT.SCH_CLS_1, WCT.SCH_CLS_2,WCT.TOTAL_CAPACITY, NVL(Taken.takenStd,0) TakenStd  from OFFEREDCOURSE OC ";
        query.CommandText += " left join CHANGEDCOURSENAME CNG on CNG.COURSECODE = OC.COURSECODE left join VW_COURSECODE VWC on VWC.COURSEKEY= OC.COURSEKEY left join WEB_COURSE_TEACHER WCT on WCT.COURSE_KEY = OC.COURSEKEY ";
        query.CommandText += " left join WEB_TEACHER_STAFF WTS on WTS.STAFF_ID = WCT.TEACHER_ID  left join  (select count(OFS.REGKEY) takenStd, OFS.COURSEKEY, OFS.GGROUP from OFFERERINGANDGRADE OFS ";
        query.CommandText += " group by OFS.COURSEKEY,OFS.GGROUP  )Taken on taken.COURSEKEY = OC.COURSEKEY and taken.GGROUP = WCT.SECTION  where OC.COURSEKEY = '" + COURSE_KEY + "'  order by CNG.COURSECODE asc, WCT.SECTION asc  ";

        ds = obj_db.Table_GetAll_Departmental(query.CommandText, COURSE_KEY, tableName);

        return ds;
    }

    //Count Probable Graduate From Year-Sem To Year-Sem or Specific one Year-Sem
    public DataTable get_ProbableGraduate(Int32 FrmSemester, Int32 FrmYear, Int32 ToSemester, Int32 ToYear, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

       query.CommandText = @"select * from VW_PROBABLEGRD_ALL where  (YEARSEM > = ?||? and YEARSEM <= ?||? ) order by LRY||LRS asc, DEPCODE asc, sid asc ";
       ds = obj_db.Table_ProbableGetAll(query.CommandText, FrmYear, FrmSemester, ToYear, ToSemester, tableName);

       return ds;
      //  return obj_db.get_viewData(sql, "ProbableGraduate");
    }

    //Count Probable Graduate From Year-Sem To Year-Sem or Specific one Year-Sem
    public DataTable get_ProbableGraduateDeptwise(Int32 FrmSemester, Int32 FrmYear, Int32 ToSemester, Int32 ToYear, string DEPTCODE,string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select * from VW_PROBABLEGRD_ALL where  (YEARSEM > = ?||? and YEARSEM <= ?||? ) and COLLEGECODE=? order by LRY||LRS asc, DEPCODE asc, sid asc ";
        ds = obj_db.Table_ProbableGetAllDeptwise(query.CommandText, FrmYear, FrmSemester, ToYear, ToSemester, DEPTCODE, tableName);

        return ds;
        //  return obj_db.get_viewData(sql, "ProbableGraduate");
    }

    //Count Registered in specific year-sem bt nonregistered in nxt specific year-sem 
    public DataTable get_nonRegisterStudent(Int32 RegSemester, Int32 RegYear, Int32 nonRegSemester, Int32 nonRegYear, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select C.SID, Student.SNAME,Student.SPROGRAM,STD_CRH.Total_Credit as completedCH, TAK_CRDT.credit as takenCrdt, DEPARTMENTINCOLLEGE.REQCREDITHRS, student.PHONE, NVL(STUDENT.ADVISOR_ID,0) ADVISOR_ID,WEB_TEACHER_STAFF.STAFF_NAME, WEB_TEACHER_STAFF.VALUE  ";
       query.CommandText += "from ( (select SID from registatus where REGKEY like '%'||?||?  and semester= ?  and year = ?  minus select SID from registatus where REGKEY like '%'||?||?  and semester=? and year = ? ) )C   ";
       query.CommandText += " inner join Student on C.SID= Student.SID  inner join DEPARTMENTINCOLLEGE  on DEPARTMENTINCOLLEGE.DEPCODE = Student.SPROGRAM  left join  (select SID, sum(COMP_CHRS+TCHRS) as Total_Credit, TOTALCHRS from VW_GET_CGPA_F group by SID,TOTALCHRS)STD_CRH on STD_CRH.SID=Student.SID  ";
       query.CommandText += " left join (select distinct SUBSTR(REGKEY,0,9) SID, sum(CHOURS) credit from OFFERERINGANDGRADE where REGKEY like '%'||?||?  group by regkey)TAK_CRDT on TAK_CRDT.SID = STUDEnt.SID  ";
       query.CommandText += " left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID = STUDENT.ADVISOR_ID where STD_CRH.Total_Credit < DEPARTMENTINCOLLEGE.REQCREDITHRS order by Student.SPROGRAM asc, SID desc ";

       ds = obj_db.Table_RegnonRegGetAll(query.CommandText, RegSemester, RegYear, nonRegYear, nonRegSemester, tableName);
       return ds;
     //   return obj_db.get_viewData(sql, "RegisterNonReg");
    }


    public DataTable get_RegSummary(Int32 RegSemester, Int32 RegYear, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select C.SID, Student.SNAME,Student.SPROGRAM,STD_CRH.Total_Credit as completedCH, TAK_CRDT.credit as takenCrdt, DEPARTMENTINCOLLEGE.REQCREDITHRS, student.PHONE, NVL(STUDENT.ADVISOR_ID,0) ADVISOR_ID,WEB_TEACHER_STAFF.STAFF_NAME, WEB_TEACHER_STAFF.VALUE  ";
        query.CommandText += "from ( (select SID from registatus where REGKEY like '%'||?||?  and semester= ?  and year = ?  minus select SID from registatus where REGKEY like '%'||?||?  and semester=? and year = ? ) )C   ";
        query.CommandText += " inner join Student on C.SID= Student.SID  inner join DEPARTMENTINCOLLEGE  on DEPARTMENTINCOLLEGE.DEPCODE = Student.SPROGRAM  left join  (select SID, sum(COMP_CHRS+TCHRS) as Total_Credit, TOTALCHRS from VW_GET_CGPA_F group by SID,TOTALCHRS)STD_CRH on STD_CRH.SID=Student.SID  ";
        query.CommandText += " left join (select distinct SUBSTR(REGKEY,0,9) SID, sum(CHOURS) credit from OFFERERINGANDGRADE where REGKEY like '%'||?||?  group by regkey)TAK_CRDT on TAK_CRDT.SID = STUDEnt.SID  ";
        query.CommandText += " left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID = STUDENT.ADVISOR_ID where STD_CRH.Total_Credit < DEPARTMENTINCOLLEGE.REQCREDITHRS order by Student.SPROGRAM asc, SID desc ";

        ds = obj_db.Table_GetAll(query.CommandText, RegSemester, RegYear, tableName);
        return ds;
        //   return obj_db.get_viewData(sql, "RegisterNonReg");
    }

    public DataTable get_StudentStatus(Int32 C_PROGINDEPT_ID, Int32 Batch, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select * from VW_STUDENT_STATUS_OVERALL where PROGRAM_ID=? and YEAR||SEMESTER =? order by PROGRAM_ID asc, FINAL_CGPA desc, SID asc ";

        ds = obj_db.Table_GetAll(query.CommandText, C_PROGINDEPT_ID, Batch, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    public DataTable get_semester_GradeSheetTotal(string SID, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select C.*, C.CHOURS*C.GPOINT Earned_Point  from
                (SELECT distinct cName, OFFEREDCOURSE.COURSECODE, max(O.GGRADE) GGRADE,  O.CHOURS,NVL(max(O.GPOINT),0)  GPOINT
                FROM OFFERERINGANDGRADE o,CHANGEDCOURSENAME,OFFEREDCOURSE  
                WHERE o.COURSEKEY=OFFEREDCOURSE.COURSEKEY  AND OFFEREDCOURSE.COURSECODE=CHANGEDCOURSENAME.COURSECODE  
                AND  REGKEY like '" + SID + "%' and ggrade != 'F' ";
        query.CommandText += "   and  ggrade != 'I'  and  ggrade != 'W'  GROUP BY cName,OFFEREDCOURSE.COURSECODE,O.CHOURS  order by 1,2)C  order by C.COURSECODE asc, C.CNAME asc ";

        ds = obj_db.Table_GetAll_Departmental(query.CommandText, SID, tableName);

        return ds;
    }

    public DataTable get_StudentStatusDeptwise(string DeptID, string Batch, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select * from VW_STUDENT_STATUS_OVERALL where COLLEGECODE=? and YEAR||SEMESTER =? order by PROGRAM_ID asc, FINAL_CGPA desc, SID asc ";

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, DeptID, Batch, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }
}

