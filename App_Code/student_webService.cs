using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.OracleClient;
using System.Data.OleDb;
 
public class student_webService : System.Web.Services.WebService
{
    Dts obj_db;
    public student_webService()
    {
        obj_db = new Dts();
    }

	public DataTable getWaiveCourseList(string sid)
    {
        string sql = @"select CHANGEDCOURSENAME.COURSECODE, CHANGEDCOURSENAME.CNAME, max(COURSEWAIVER.CREDIT) CREDIT from COURSEWAIVER, CHANGEDCOURSENAME
                        where COURSEWAIVER.EUCCODE = CHANGEDCOURSENAME.COURSECODE and COURSEWAIVER.SID = '" + sid + @"'
                        group by CHANGEDCOURSENAME.COURSECODE, CHANGEDCOURSENAME.CNAME  order by CHANGEDCOURSENAME.COURSECODE asc";

        return obj_db.get_viewData(sql, "WaivedCourse");
    }


    public DataTable getTransferCourseList(string sid)
    {
        string sql = @"select CHANGEDCOURSENAME.COURSECODE, CHANGEDCOURSENAME.CNAME, max(TRANSFER.EUCREDIT) CREDIT , max(transfer.GP) GP, transfer.GRADE,TRANSFER.CCODEPREUNI, TRANSFER.CNAMEPREUNI  from TRANSFER, CHANGEDCOURSENAME
                        where TRANSFER.COURSECODE = CHANGEDCOURSENAME.COURSECODE and TRANSFER.SID = '" + sid + @"'
                        group by CHANGEDCOURSENAME.COURSECODE, CHANGEDCOURSENAME.CNAME, transfer.GRADE,TRANSFER.CCODEPREUNI, TRANSFER.CNAMEPREUNI  order by CHANGEDCOURSENAME.COURSECODE asc";

        return obj_db.get_viewData(sql, "TransferedCourse");
    }

    public DataTable getTransferUniName(string sid)
    {
        string sql = @"select DISTINCT  TRANSFER.TRANSFERFROM  from TRANSFER where TRANSFER.SID = '" + sid + "' ";

        return obj_db.get_viewData(sql, "TransferedUni");
    }

    public DataTable getStudentInfo(string sid)
    {
        string sql = @" select distinct S.SID, S.SNAME, S.ADMINYEAR, S.ADMINSEMETER,     
      CASE  WHEN S.ADMINSEMETER = 1    THEN 'Spring'  WHEN S.ADMINSEMETER = 2  THEN 'Summer'
      WHEN S.ADMINSEMETER = 3  THEN 'Fall'  END AS Semester,
      C_PROGINDEPT.NAME,MajorCourse.MAJOR,COLLEGE.COLLEGENAME from student S 
      left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID
      left join  (select  distinct OFC.regkey, C_MAJOR.NAME MAJOR from 
      OFFERERINGANDGRADE OFC   left join COURSEDETAILS CSD on CSD.COURSECODE = SUBSTR(OFC.COURSEKEY,6)  
      left join C_MAJOR on C_MAJOR.C_MAJOR_ID=CSD.MAJOR_ID
      where  C_MAJOR.NAME is not null )MajorCourse  on Substr(MajorCourse.regkey,1,9) = S.SID
      left join C_departmentinFAculty on C_DEPARTMENTINFACULTY.DEPID =C_PROGINDEPT.DEPID
      left join college on COLLEGE.COLLEGECODE = C_DEPARTMENTINFACULTY.COLLEGECODE
      where S.SID = '" + sid + "'   ";

        return obj_db.get_viewData(sql, "StudentInfo");
    }
    public bool check_student_login(string user_id, string user_pass)
    {
        DataSet ds = new DataSet();
        bool status = false;
 
        string sql = @"Select * from STUDENT where SID='" + user_id + "' and SPASSWORD='" + user_pass + "' and  S_CTRL=1  ";
        ds.Merge(obj_db.get_viewData(sql, "STUDENT"));

        if (ds.Tables["STUDENT"].Rows.Count > 0)
        { 
            DataRow dr=ds.Tables["STUDENT"].Rows[0];
            if (dr["SID"].ToString() != "" && dr["SPASSWORD"].ToString() != "")
            {
                if (dr["SID"].ToString() == user_id && dr["SPASSWORD"].ToString() == user_pass)
                {
                    status = true;
                }
            }
        }
        return status;
    }



    public DataTable get_Student_Persentance(String SID, String batch)
    {
        string sql = " SELECT WEB_STUDENT_CLASSATTENDANCE.* from WEB_STUDENT_CLASSATTENDANCE where SID ='" + SID + "' and COURSE_KEY like '" + batch + "%' order by COURSE_KEY  ";
        return obj_db.get_viewData(sql, "StudentPersentance");
    }

    public DataTable get_Student_Persentance_NEW(String SID, String batch)
    {
        string sql = " SELECT WEB_STUDENT_CLSATTENDANCE.* from WEB_STUDENT_CLSATTENDANCE where SID ='" + SID + "' and COURSEKEY like '" + batch + "%' order by COURSECODE asc  ";
        return obj_db.get_viewData(sql, "StudentPersentance");
    }

    public DataTable check_student_Block(string user_id, string user_pass)
    {
       // string sql = @" Select STUDENT.* ,C_STUDENTBLOCK.CREATED,C_REASON.NAME as REASON from STUDENT inner join C_STUDENTBLOCK on C_STUDENTBLOCK.SID =  STUDENT.SID inner join C_REASON on C_REASON.C_REASON_ID = C_STUDENTBLOCK.C_REASON_ID where STUDENT.ISBLOCK = 'Y' and STUDENT.SID='" + user_id + "' and STUDENT.SPASSWORD='" + user_pass + "' and  S_CTRL=1 and ROWNUM =1 order by C_STUDENTBLOCK.CREATED desc ";


        string sql = @" SELECT * from
                         (
                         Select STUDENT.* ,C_STUDENTBLOCK.CREATED,C_REASON.NAME as 
                        REASON ,C_STUDENTBLOCK.DESCRIPTION,
                        row_number() over (order by C_STUDENTBLOCK.CREATED desc) rnk
                        from STUDENT inner join C_STUDENTBLOCK on C_STUDENTBLOCK.SID =  STUDENT.SID 
                        inner join C_REASON on C_REASON.C_REASON_ID = C_STUDENTBLOCK.C_REASON_ID
                        where
                        STUDENT.ISBLOCK = 'Y' and STUDENT.SID='" + user_id + @"' and 
                        STUDENT.SPASSWORD='" + user_pass + @"' and  S_CTRL=1 
                        )
                        where rnk<=1";



        return obj_db.get_viewData(sql, "Studentlist");
    }




    public DataTable check_student_Info(string user_id)
    {
        string sql = @"Select * from STUDENT where SID='" + user_id + "'  and  S_CTRL=1  ";
        //select student.* ,CASE WHEN student.email is not null THEN student.email WHEN student.email is null THEN '' END as mail from student
        return obj_db.get_viewData(sql, "STUDENT");
    }

    //public DataTable check_student_InfoNew(string user_id)
    //{
    //    string sql = @"select student.* ,decode(email,null,'na',email) mail  where SID='" + user_id + "'  and  S_CTRL=1  ";
    // //   string sql = @"select student.* ,CASE WHEN student.email is not null THEN student.email WHEN student.email is null THEN ' ' END as mail from student where SID='" + user_id + "'  and  S_CTRL=1  ";
    //    return obj_db.get_viewData(sql, "STUDENT");
    //}

    public DataTable check_student_InfoNew(string user_id)
    {
        string sql = @"select student.* ,CASE WHEN student.email is not null THEN student.email WHEN student.email is null THEN null END as mail from student where SID='" + user_id + "'  and  S_CTRL=1  ";
        return obj_db.get_viewData(sql, "STUDENT");
    }

    public string Update_studentInfo(string sid, string Mobile, string Mail)
    {
        string sql = @" update student set EMAIL='" + Mail + "', PHONE='" + Mobile + "' where sid='" + sid + "'   ";
        return obj_db.execute_query("" + sql);
    }

    public DataTable get_all_preOffered_coursesNew(string sid, string sem, string year, string courseKey)
    {
        string sql = " Select * from WEB_COURSE_OFFERING_TEMP where REGKEY='" + sid + sem + year + "' and COURSEKEY !=  '" + courseKey + "'";
        return obj_db.get_viewData(sql, "pree_offered");
    }

    public string Update_offering(DataSet ds)
    {
        string sql = " ";
        string sql1 = " ";

        foreach (DataRow dr in ds.Tables["WEB_COURSE_OFFERING_TEMP"].Rows)
        {
            sql = @" update WEB_COURSE_OFFERING_TEMP set GGROUP= '" + dr["GGROUP"] + "', APPROVAL_TIME='" + dr["APPROVAL_TIME"] + "'   where COURSEKEY='" + dr["COURSEKEY"] + "' and REGKEY='" + dr["REGKEY"] + "'  ";
            break;
        }

        return obj_db.execute_query(sql);

    }








    public string change_student_password(string sid, string new_pass)
    {
        string sql = @" update student set SPASSWORD='" + new_pass + "' where sid='" + sid + "'   ";
        return obj_db.execute_query(""+sql);
    }

    public string change_student_password(string pre_pass,string sid, string new_pass)
    {
        string sql = @" update student set SPASSWORD='" + new_pass + "' where sid='" + sid + "' and  SPASSWORD='" + pre_pass + "'  ";
        return obj_db.execute_query("" + sql);
    }


    public DataTable get_a_assignmentInfo(string ass_id)
    {
        string sql = @" Select * from WEB_COURSE_MATERIALS_TEACHER where TYPE=1 and COURSE_MATERIALS_ID='" + ass_id + "'  ";
        return obj_db.get_viewData( sql, "courseMaterial");
    }


    public DataTable get_student_notice()
    {
        string sql = @" Select * from WEB_NOTICE_BOARD where FOR_STUDENT=1 and CTRL=1 order by NOTICE_ID desc ";
        return obj_db.get_viewData(sql, "WEB_NOTICE_BOARD");
    }

    public DataTable get_batchwise_student(string batch)
    {
        string sql = @" Select * from STUDENT where SID like '" + batch + "%' order by SID asc";
        return obj_db.get_viewData(sql, "STUDENT");
    }

    public DataTable get_a_notice_details(string notice_id)
    {
        string sql = " SELECT  * from WEB_NOTICE_BOARD where NOTICE_ID='" + notice_id + "'   ";

        return obj_db.get_viewData(sql, "WEB_NOTICE_BOARD");
    }


    public DataTable get_a_news_events(string news_event_id)
    {
        string sql = " SELECT  * from WEB_NEWS_EVENTS where NEWS_EVENT_ID='" + news_event_id + "' ";

        return obj_db.get_viewData(sql, "WEB_NEWS_EVENTS");
    }
     

    public DataTable get_a_course_routine(string courseKey, string group)
    {
       // string sql = @" Select * from WEB_COURSE_TEACHER where COURSE_KEY='"+courseKey+"' and SECTION='" + group+ "'  ";
        
       string sql = @" SELECT  ws.* ,SUBSTR (ws.SCH_CLS_1,1, 13) AS Clsday1,SUBSTR (ws.SCH_CLS_2,1, 13) AS Clsday2, ";
       sql += "SUBSTR (ws.SCH_CLS_3,1, 13) AS Clsday3 FROM WEB_COURSE_TEACHER ws where COURSE_KEY='" + courseKey + "' and SECTION='" + group + "' ";
        
        return obj_db.get_viewData(sql, "course_schedule");
    }

    public DataTable get_semester_class_routine(string regkey)
    {
        string sql = @" SELECT DISTINCT od.*,  ws.* ,";
        sql += " SUBSTR (ws.SCH_CLS_1,1, 13) AS Clsday1,SUBSTR (ws.SCH_CLS_2,1, 13) AS Clsday2, ";
        sql += "SUBSTR (ws.SCH_CLS_3,1, 13) AS Clsday3 ";
        sql += " FROM OFFERERINGANDGRADE od,WEB_VIEW_COURSE_TEACHER ws  ";
               sql += " WHERE od.regkey='" + regkey + "' ";
               sql += " AND (od.COURSEKEY=ws.COURSE_KEY ";
               sql += " AND od.GGROUP=ws.SECTION ) ";

        return obj_db.get_viewData(sql, "class_routine");
    }


    public DataTable get_al_routine_time(string regkey)
    {
        string sql = @" SELECT od.* ,wt.*  ";
                sql += " FROM OFFERERINGANDGRADE od, WEB_COURSE_TEACHER wt ";
                sql += " WHERE regkey ='"+regkey+"' ";
                sql += " AND (od.COURSEKEY=wt.COURSE_KEY AND od.GGROUP=wt.SECTION)  ";
        return obj_db.get_viewData(sql, "routineTime_sch");
    }

    public DataTable get_singlDay_routine_time(string regkey, string day)
    {
        string sql = @" SELECT od.* ,wt.*  ";
        sql += " FROM OFFERERINGANDGRADE od, WEB_COURSE_TEACHER wt ";
        sql += " WHERE regkey ='" + regkey + "' ";
        sql += " AND (od.COURSEKEY=wt.COURSE_KEY AND od.GGROUP=wt.SECTION)  ";
        sql += " AND (sch_cls_1 LIKE'"+day+"%'OR sch_cls_2 LIKE'"+day+"%' OR  tut_cls_1 LIKE'"+day+"%' OR tut_cls_2 LIKE'"+day+"%') ";
        return obj_db.get_viewData(sql, "routineTime_days");
    }


    public bool get_teacher_eval_dateRange(string sem, string year)
    {
        bool status = false;
        DataSet ds = new DataSet();
        string sql = @" Select * from WEB_PRE_OFFERING_DATE where SEMESTER ='" + sem + "' and YEAR='" + year + "' and ( TO_DATE('" + new cls_tools().get_database_formateDate(DateTime.Today) + "', 'dd/mm/yyyy hh24:mi:ss')>= EVAL_OPENING and TO_DATE('" + new cls_tools().get_database_formateDate(DateTime.Today) + "', 'dd/mm/yyyy hh24:mi:ss') <= EVAL_CLOSING )  ";
        ds.Merge(obj_db.get_viewData(sql, "WEB_PRE_OFFERING_DATE"));

        foreach (DataRow dr in ds.Tables["WEB_PRE_OFFERING_DATE"].Rows)
        {
            if (!String.IsNullOrEmpty(dr["EVAL_CLOSING"].ToString()))
            {
                status = true;
               
            }
        }
        return status; 
    }


    public bool is_evaluation_done(string course_teacherId, string regkey)
    {
        bool status = false;
        DataSet ds = new DataSet(); 
        ds.Merge(obj_db.get_viewData("select * from WEB_TEACHER_EVAL_VALUE where COURSE_TEACHER='" + course_teacherId + "' and REGKEY='" + regkey + "'", "eval_status"));

        foreach (DataRow dr in ds.Tables["eval_status"].Rows)
        {
            if (!String.IsNullOrEmpty(dr["COURSE_TEACHER"].ToString()))
            {
                status = true;
                break;
            }

        }
        return status;

    }



    public string submit_assignment(DataSet ds, string table)
    {
        return obj_db.insert_general(ds, table);
         
    }

    public DataTable get_a_assignment_student(string sid, string COURSE_MAT_ID)
    {
        string sql = " Select * from WEB_COURSE_MATERIALS_STUDENT where COURSE_MAT_ID='" + COURSE_MAT_ID + "' and sid='" + sid + "'  ";
        return obj_db.get_viewData(sql, "assignment");
    }

    public string get_a_departmentName(string depId)
    {
        string depName = "";

        string sql = " Select * from COLLEGE where COLLEGECODE='" + depId + "'   ";
        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData(sql, "DEPARTMENTINCOLLEGE"));

        foreach (DataRow dr in ds.Tables["DEPARTMENTINCOLLEGE"].Rows)
        {
            depName = dr["COLLEGENAME"].ToString();
        }

        return depName;
    }

    public string set_student_Acc_clearence(string regKey, string status)
    {
        return obj_db.execute_query(" update REGISTATUS set ACC_STATUS='" + status + "' where REGKEY='" + regKey + "'  ");
    }

    public string set_student_lib_clearence(string regKey, string status)
    {
        return obj_db.execute_query(" update REGISTATUS set LIB_STATUS='" + status + "' where REGKEY='" + regKey + "'  ");
    }

    public string insert_teacher_evaluation(DataSet ds)
    {
        string sql = " ";
        int count = 0;
        foreach (DataRow dr in ds.Tables["WEB_TEACHER_EVAL_VALUE"].Rows)
        {
            sql = @" Insert into WEB_TEACHER_EVAL_VALUE (COURSE_TEACHER,REGKEY,DATES,S_1,S_2,S_3,S_4,S_5,S_6,S_7,S_8,S_9,S_10,S_11,S_12,S_13,S_14,S_15) ";
            sql += " values ('" + dr["COURSE_TEACHER"] + "' ,  '" + dr["REGKEY"] + "' , TO_DATE('" + dr["DATES"] + "', 'dd/mm/yyyy hh24:mi:ss'), ";
            sql += " '" + dr["S_1"] + "',  '" + dr["S_2"] + "','" + dr["S_3"] + "','" + dr["S_4"] + "','" + dr["S_5"] + "','" + dr["S_6"] + "','" + dr["S_7"] + "', '" + dr["S_8"] + "','" + dr["S_9"] + "','" + dr["S_10"] + "', ";
            sql += "   '" + dr["S_11"] + "','" + dr["S_12"] + "','" + dr["S_13"] + "','" + dr["S_14"] + "','" + dr["S_15"] + "' ) ";
            /* if (obj_db.execute_query(sql) == "1")
             {
                 count++;
             }*/

        }


        return obj_db.execute_query(sql);

        /*string sql = "insert into WEB_TEACHER_EVAL_VALUE (";
        int i=0;
        foreach (DataRow dr in ds.Tables["WEB_TEACHER_EVAL_VALUE"].Rows)
        {
            foreach (DataColumn dc in ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns)
            {
                if (!String.IsNullOrEmpty(dr["" + dc.ColumnName].ToString()))
                {
                    if (i == 0)
                        sql += "" + dc.ColumnName;
                    else
                        sql += "," + dc.ColumnName;
                }
                i++;
            }
            sql+= ")";
            break;
        }
        sql += " values (";
        i = 0;
        foreach (DataRow dr in ds.Tables["WEB_TEACHER_EVAL_VALUE"].Rows)
        {
            foreach (DataColumn dc in ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns)
            {
                if (!String.IsNullOrEmpty(dr["" + dc.ColumnName].ToString()))
                {
                    if (i == 0)
                        sql += "'" + dr["" + dc.ColumnName].ToString() + "'";
                    else
                        sql += ",'" + dr["" + dc.ColumnName].ToString()+"'";
                }
                i++;
            }
            sql += ")";
            break;
        } 
        
        return obj_db.execute_query(sql);*/
    }

    public DataTable get_StdparentsInfo(string YearSemester, string Program, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select std.SID ,std.SNAME , sf.SFNAME ,std.phone,sm.SMNAME,SF.SFPHONE , SM.SMPHONE  from STUDENT std left join STUDFATHERDET sf on std.SID=sf.SID left join STUDMOTHERDET sm on std.SID=sm.SID where std.ADMINYEAR||std.ADMINSEMETER=? and STD.PROGRAM_ID = ? order by std.SID asc ";



        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, YearSemester, Program, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    public DataTable get_MajorStudentListAllMjr(Int32 Program, Int32 SemesterYear, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" Select DISTINCT SUBSTR(OFC.REGKEY,0,9) SID, C_MAJOR.NAME MAJOR, S.SNAME, C_PROGINDEPT.NAME PROGRAM ,SUBSTR( OFC.REGKEY, 10) SEMYEAR,S.PHONE,NVL(VW_GET_CGPA_F.COMP_CHRS,0) COMP_CHRS  ";
        query.CommandText += "from OFFERERINGANDGRADE OFC left join COURSEDETAILS CSD on CSD.COURSECODE = SUBSTR(OFC.COURSEKEY,6)  ";
        query.CommandText += "left join C_MAJOR on C_MAJOR.C_MAJOR_ID=CSD.MAJOR_ID left join Student S on S.SID = SUBSTR(OFC.REGKEY,0,9) left join VW_GET_CGPA_F on VW_GET_CGPA_F.SID =SUBSTR(OFC.REGKEY,0,9) ";
        query.CommandText += "left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID where C_MAJOR.C_MAJOR_ID is not null   ";
        query.CommandText += "and C_PROGINDEPT_ID =? and SUBSTR(OFC.REGKEY, 10) = ?  order by SUBSTR(OFC.REGKEY,0,9) desc  ";



        ds = obj_db.Table_GetAll(query.CommandText, Program, SemesterYear, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }


    public DataTable get_MajorStudentList(Int32 Program, Int32 Major, Int32 Semester, Int32 Year, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" Select DISTINCT SUBSTR(OFC.REGKEY,0,9) SID, C_MAJOR.NAME MAJOR, S.SNAME, C_PROGINDEPT.NAME PROGRAM ,SUBSTR( OFC.REGKEY, 10) SEMYEAR,S.PHONE,NVL(VW_GET_CGPA_F.COMP_CHRS,0) COMP_CHRS  ";
        query.CommandText += "from OFFERERINGANDGRADE OFC left join COURSEDETAILS CSD on CSD.COURSECODE = SUBSTR(OFC.COURSEKEY,6)  ";
        query.CommandText += "left join C_MAJOR on C_MAJOR.C_MAJOR_ID=CSD.MAJOR_ID left join Student S on S.SID = SUBSTR(OFC.REGKEY,0,9) left join VW_GET_CGPA_F on VW_GET_CGPA_F.SID =SUBSTR(OFC.REGKEY,0,9) ";
        query.CommandText += "left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID where C_MAJOR.C_MAJOR_ID = ?   ";
        query.CommandText += "and C_PROGINDEPT_ID =? and SUBSTR(OFC.REGKEY, 10) = ?||?  order by SUBSTR(OFC.REGKEY,0,9) desc  ";



        ds = obj_db.Table_ProbableGetAll(query.CommandText, Major, Program, Semester, Year, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }


    public DataTable get_ProgramList(string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct C_PROGINDEPT_ID, NAME PROGRAM from C_PROGINDEPT order by NAME asc";

        ds = obj_db.Table_CollegeGetAll(query.CommandText, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    public DataTable get_DeparmentList(string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct COLLEGECODE, COLLEGENAME  from College order by COLLEGENAME asc";

        ds = obj_db.Table_CollegeGetAll(query.CommandText, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }
    public DataTable get_CourseList( string Semester,string Year ,string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"Select distinct WEB_VIEW_COURSE_TEACHER.* from WEB_VIEW_COURSE_TEACHER   where COURSE_KEY like ?||?||'%'  order by STAFF_NAME asc, CNAME asc, SECTION asc";

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, Semester,Year, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }


    public DataTable get_CourseList_Deptwise(string SemesterYear, string DEPTCODE,string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"Select distinct WEB_VIEW_COURSE_TEACHER.* from WEB_VIEW_COURSE_TEACHER   where COURSE_KEY like  ?||'%'  and DEPARTMENT_ID =?   order by STAFF_NAME asc, CNAME asc, SECTION asc";

       // ds = obj_db.Table_CollegeGetAll(query.CommandText, tableName);
        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, SemesterYear, DEPTCODE, tableName);
        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

  
    public DataTable get_MajorList(string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select  C_MAJOR_ID, NAME from C_MAJOR where IS_ACTIVE =1 order by NAME asc";

        ds = obj_db.Table_CollegeGetAll(query.CommandText, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }
    public DataTable get_a_course_routine(string COURSE_TEACHER_ID)
    {
        // string sql = @" Select * from WEB_COURSE_TEACHER where COURSE_KEY='"+courseKey+"' and SECTION='" + group+ "'  ";

        string sql = @" SELECT  ws.* ,TRIM(SUBSTR (ws.SCH_CLS_1,1, 13)) AS Clsday1,TRIM(SUBSTR (ws.SCH_CLS_2,1, 13)) AS Clsday2, ";
        sql += "SUBSTR (ws.SCH_CLS_3,1, 13) AS Clsday3 FROM WEB_COURSE_TEACHER ws where ws.COURSE_TEACHER_ID ='" + COURSE_TEACHER_ID + "' ";

        return obj_db.get_viewData(sql, "course_schedule");
    }

    public DataTable get_offeringStudent(string COURSE_TEACHER_ID)
    {
        // string sql = @" Select * from WEB_COURSE_TEACHER where COURSE_KEY='"+courseKey+"' and SECTION='" + group+ "'  ";

        string sql = @"   select count(CTMP.REGKEY) student_offer from WEB_COURSE_OFFERING_TEMP CTMP   left join WEB_COURSE_TEACHER WT on WT.COURSE_KEY= CTMP.COURSEKEY and WT.SECTION = CTMP.GGROUP where WT.COURSE_TEACHER_ID ='" + COURSE_TEACHER_ID + "' ";

        return obj_db.get_viewData(sql, "Offering");
    }

    public DataTable get_TakenStudent(string COURSE_TEACHER_ID)
    {
        // string sql = @" Select * from WEB_COURSE_TEACHER where COURSE_KEY='"+courseKey+"' and SECTION='" + group+ "'  ";

        string sql = @" select count(COFR.REGKEY) student_Taken from OFFERERINGANDGRADE COFR   left join WEB_COURSE_TEACHER WT on WT.COURSE_KEY= COFR.COURSEKEY and WT.SECTION = COFR.GGROUP where WT.COURSE_TEACHER_ID ='" + COURSE_TEACHER_ID + "' ";

        return obj_db.get_viewData(sql, "Taken");
    }

    public DataTable get_PermanentCampusStudentListProg(string DEPTCODE, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select S.SID,S.SNAME, C_PROGINDEPT.NAME Program ,COLLEGE.COLLEGECODE, COLLEGE.COLLEGENAME, S.DOB,S.GENDER, S.PLACEOFBIRTH, S.PLACEOFBIRTH, S.PHONE,S.EMAIL,S.STATUS, ";
        query.CommandText += "  S.ADMISSION_DATE, S.PROGRAM_ID, S.ADVISOR_ID, WEB_TEACHER_STAFF.STAFF_NAME Advisor  ";
        query.CommandText += "  from student S left join T_PERMANENTCAMPUS PERM on PERM.SID = S.SID left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID  ";
        query.CommandText += " left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID = S.ADVISOR_ID left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID=C_PROGINDEPT.DEPID ";
        query.CommandText += " left join COLLEGE on COLLEGE.COLLEGECODE = C_DEPARTMENTINFACULTY.COLLEGECODE  where PERM.SID is not null and COLLEGE.COLLEGECODE = ?  order by C_PROGINDEPT.NAME asc, S.sid asc ";

        ds = obj_db.Table_GetAll_Departmental(query.CommandText, DEPTCODE, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    public DataTable get_ProgramListDept(string DEPTCODE, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct C_PROGINDEPT_ID, NAME PROGRAM from C_PROGINDEPT left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID=C_PROGINDEPT.DEPID
left join COLLEGE on COLLEGE.COLLEGECODE = C_DEPARTMENTINFACULTY.COLLEGECODE where  COLLEGE.COLLEGECODE = ? order by NAME asc";

        ds = obj_db.Table_GetAll_Departmental(query.CommandText, DEPTCODE, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }


    public DataTable get_MaximumYearSemester(string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select max(ADMINYEAR||'_'||ADMINSEMETER) YRSEM from student";

        ds = obj_db.Table_CollegeGetAll(query.CommandText, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }


    public DataTable get_PermanentCampusStudentList(string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select S.SID,S.SNAME, C_PROGINDEPT.NAME Program , S.DOB,S.GENDER, S.PLACEOFBIRTH, S.PLACEOFBIRTH, S.PHONE,S.EMAIL,S.STATUS, ";
         query.CommandText += "  S.ADMISSION_DATE, S.PROGRAM_ID, S.ADVISOR_ID, WEB_TEACHER_STAFF.STAFF_NAME Advisor  ";
         query.CommandText += "  from student S left join T_PERMANENTCAMPUS PERM on PERM.SID = S.SID left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID  ";
         query.CommandText += " left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID = S.ADVISOR_ID  where PERM.SID is not null  order by C_PROGINDEPT.NAME asc, S.sid asc";

        ds = obj_db.Table_CollegeGetAll(query.CommandText, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }
    public DataTable get_PermanetStdProg(Int32 Program, Int32 YearSemester, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select S.SID,S.SNAME, C_PROGINDEPT.NAME Program ,S.DOB,S.GENDER,  S.PLACEOFBIRTH,S.PHONE,S.EMAIL,S.STATUS, ";
         query.CommandText += "S.ADMISSION_DATE, S.PROGRAM_ID,S.ADVISOR_ID, WEB_TEACHER_STAFF.STAFF_NAME Advisor from student S left join T_PERMANENTCAMPUS PERM on PERM.SID = S.SID ";
         query.CommandText += "left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID = S.ADVISOR_ID ";
         query.CommandText += " where PERM.SID is not null and S.PROGRAM_ID=? and S.sid like ?||'%' order by C_PROGINDEPT.NAME asc, S.sid asc   ";



        ds = obj_db.Table_GetAll(query.CommandText, Program, YearSemester, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    public DataTable get_PermanetStd(Int32 Year, Int32 Semester, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" select S.SID,S.SNAME, C_PROGINDEPT.NAME Program ,S.DOB,S.GENDER,  S.PLACEOFBIRTH,S.PHONE,S.EMAIL,S.STATUS,
                                S.ADMISSION_DATE, S.PROGRAM_ID,S.ADVISOR_ID, WEB_TEACHER_STAFF.STAFF_NAME Advisor from student S left join T_PERMANENTCAMPUS PERM on PERM.SID = S.SID
                                left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID = S.ADVISOR_ID
                                where PERM.SID is not null and S.sid like ?||?||'%' order by C_PROGINDEPT.NAME asc, S.sid asc   ";



        ds = obj_db.Table_GetAll(query.CommandText, Year, Semester, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    #region Anniversary

    public DataTable get_StdStatus(Int32 Year, Int32 Semester, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @" Select NAME as Program, NVL(Sum(Regular),0) Regular , NVL(Sum(Transfer),0)  Transfer, NVL(Sum(Regular),0)+NVL(Sum(Transfer),0) Total from
                    (select COLLEGE.COLLEGENAME,  C_PROGINDEPT.NAME,  case    when S.status='Reguler' then NVL(count(S.SID),0)     else 0     end as Regular,
                    case     when S.status='Transfer' then NVL(count(S.SID),0)     else 0     end as Transfer,
                    count(S.SID) StudentNum  from student S left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID
                    left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID =C_PROGINDEPT.DEPID left join COLLEGE on COLLEGE.COLLEGECODE =C_DEPARTMENTINFACULTY.COLLEGECODE
                    where S.ADMINYEAR||S.ADMINSEMETER = ?||?
                    group by COLLEGE.COLLEGENAME,C_PROGINDEPT.NAME ,S.status order by COLLEGE.COLLEGENAME, C_PROGINDEPT.NAME asc )group by NAME order by NAme asc  ";



        ds = obj_db.Table_GetAll(query.CommandText, Year, Semester, tableName);

        return ds;

        //  return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    public DataTable get_RegistrationStudent(string student_id)
    {
        string sql = " SELECT EU_CONVOCATION.*,  Student.SNAME,Student.SPROGRAM DEPNAME,  Student.EMAIL, Student.PHONE, Student.ADMINYEAR, Student.ADMINSEMETER FROM EU_CONVOCATION  inner join student on student.SID = EU_CONVOCATION.SID WHERE EU_CONVOCATION.sid='" + student_id + "' and REG_TYPE='A' ";
        return obj_db.get_viewData(sql, "RegGoing_CHK");
    }


    public string update_AnniversaryRegistration(DataSet ds, string TrunIDC)
    {
        string sql = " ";
        string sql1 = " ";
        string sql2 = " ";
        int count = 0;

    //    string code = "" + obj_db.get_pk_no("T_STUDENTDEBIT");
        Session["TRANID"] = TrunIDC;

        foreach (DataRow dr in ds.Tables["EU_CONVOCATION"].Rows)
        {
            sql = @" update EU_CONVOCATION set TRANID= '" + TrunIDC + "' where SID='" + dr["SID"] + "'  ";

            sql1 = @" insert into T_STUDENTDEBIT (TRANID,HEADSN,SID,YEAR,SEMESTER, AMOUNT, STATUS,CREATEDBY,PDATE)";
            sql1 += " values ('" + TrunIDC + "', '" + dr["HEADSN"] + "','" + dr["SID"] + "','" + dr["YEAR"] + "','" + dr["SEMESTER"] + "', '" + dr["TOTALFEE"] + "', '" + dr["STATUS"] + "', ";
            sql1 += "'" + dr["SID"] + "',  '" + dr["PDATE"] + "' ) ";
            // sql1 = @" update T_STUDENTDEBIT set TRANID= 'EU" + code + "' where SID='" + dr["SID"] + "' and HEADSN= '" + dr["HEADSN"] + "' ";


            break;


        }

        string i = obj_db.execute_query(sql);
        string a = obj_db.execute_query(sql1);
        // string b = obj_db.execute_query(sql2);

        if (i == "1" && a == "1")
        {
            count++;
        }

     //   update_code("T_STUDENTDEBIT", code);
        return Convert.ToString(Session["TRANID"]);


    }

    public string insert_AnniversaryRegistration(DataSet ds)
    {
        string sql = "", sql1 = "";
        int count = 0;
        string code = "";

        string objectCode = "T_STUDENTDEBIT";
        code = obj_db.call_procedure("SP_GET_SEQ_VALUE", objectCode);

        code = format_code(code);

        foreach (DataRow dr in ds.Tables["EU_CONVOCATION"].Rows)
        {
            sql = @" insert into EU_CONVOCATION (TRANID,SID,YEAR,SEMESTER, EMAIL,CONTACT, PICTURE, REGISTERFEE, STATUS,CREATEDBY,PDATE,EMPSTATUS,REG_TYPE,PRESENT_ADDRESS,TOTALFEE)";
            sql += " values ('EU" + code + "', '" + dr["SID"] + "','" + dr["YEAR"] + "','" + dr["SEMESTER"] + "', '" + dr["EMAIL"] + "','" + dr["CONTACT"] + "',  '" + dr["PICTURE"] + "', '" + dr["REGISTERFEE"] + "', '" + dr["STATUS"] + "', ";
            sql += "'" + dr["SID"] + "',  TO_DATE('" + dr["PDATE"] + "', 'dd/mm/yyyy hh24:mi:ss'),  '" + dr["EMPSTATUS"] + "' ,  '" + dr["REG_TYPE"] + "', '" + dr["PRESENT_ADDRESS"] + "', '" + dr["TOTALFEE"] + "') ";

            //if (obj_db.execute_query(sql) == "1")
            //{

            //}
            sql1 = @" insert into T_STUDENTDEBIT (TRANID,HEADSN,SID,YEAR,SEMESTER, AMOUNT, STATUS,CREATEDBY,PDATE)";
            sql1 += " values ('EU" + code + "', '" + dr["HEADSN"] + "','" + dr["SID"] + "','" + dr["PAYMENT_YEAR"] + "','" + dr["PAYMENT_SEMESTER"] + "', '" + dr["TOTALFEE"] + "', '" + dr["STATUS"] + "', ";
            sql1 += "'" + dr["SID"] + "',  TO_DATE('" + dr["PDATE"] + "', 'dd/mm/yyyy hh24:mi:ss') ) ";

            //    string GstIns = insertGst(ds, Convert.ToString(Session["TRANID"]));


        }

        if (obj_db.execute_query(sql) == "1" && obj_db.execute_query(sql1) == "1")
        {
            count++;
        }



        //update_code("T_STUDENTDEBIT", code);

        return Convert.ToString(Session["TRANID"]);

    }


    public DataTable check_Student_Reg(string user_id, string DOB)
    {


        string sql = @"Select STUDENT.*,TO_CHAR (Student.dob, 'DD/MM/YYYY') AS matchpass, DP.DEPNAME from STUDENT left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = STUDENT.PROGRAM_ID left join C_DEPARTMENTINFACULTY DP on DP.DEPID =  C_PROGINDEPT.DEPID   where SID ='" + user_id + "' and TO_CHAR (Student.dob, 'DD/MM/YYYY') ='" + DOB + "'  ";
        return obj_db.get_viewData(sql, "STUDENT");
       
    }

    #endregion










    #region rpt_REGISTRAR_OFFICE

    public DataTable get_ProbableGraduate(string FrmSemester, string FrmYear, string ToSemester, string ToYear)
    {
        string sql = @"select * from VW_PROBABLEGRD_ALL where  (YEARSEM > = '" + FrmYear + "" + FrmSemester + "' and YEARSEM <= '" + ToYear + "" + ToSemester + "' ) order by LRY||LRS asc, DEPCODE asc, sid asc ";


        return obj_db.get_viewData(sql, "ProbableGraduate");
    }
    public DataTable get_nonRegisterStudent(string RegSemester, string RegYear, string nonRegSemester, string nonRegYear)
    {
        string sql = @" select C.SID, Student.SNAME,Student.SPROGRAM,STD_CRH.Total_Credit as completedCH, TAK_CRDT.credit as takenCrdt, DEPARTMENTINCOLLEGE.REQCREDITHRS, student.PHONE  ";
                sql += "from ( (select SID from registatus where REGKEY like '%" + RegSemester + "" + RegYear + "' and semester= '" + RegSemester + "'  and year = '" + RegYear + "'  minus select SID from registatus where REGKEY like '%" + nonRegSemester + "" + nonRegYear + "' and semester='" + nonRegSemester + "' and year = '" + nonRegYear + "' ) )C   ";
                 sql += " inner join Student on C.SID= Student.SID  inner join DEPARTMENTINCOLLEGE  on DEPARTMENTINCOLLEGE.DEPCODE = Student.SPROGRAM  left join  (select SID, sum(COMP_CHRS+TCHRS) as Total_Credit, TOTALCHRS from VW_GET_CGPA_F group by SID,TOTALCHRS)STD_CRH on STD_CRH.SID=Student.SID  ";
                 sql += " left join (select distinct SUBSTR(REGKEY,0,9) SID, sum(CHOURS) credit from OFFERERINGANDGRADE where REGKEY like '%" + RegSemester + "" + RegYear + "' group by regkey)TAK_CRDT on TAK_CRDT.SID = STUDEnt.SID  ";
                 sql += " where STD_CRH.Total_Credit < DEPARTMENTINCOLLEGE.REQCREDITHRS order by Student.SPROGRAM asc, SID desc ";
       
     
        return obj_db.get_viewData(sql, "RegisterNonReg");
    }


    public DataTable get_WaiverList(string Semester, string Year)
    {
        string sql = @" SELECT SPROGRAM,SID,SNAME,PHONE,NVL(WAIVER,0) WAIVER,NVL(CR,0) CR,NVL(CH,0) CH, ROUND((NVL(CH,0)*NVL(CR,0)*NVL(WAIVER,0)  / 100)) WR FROM (SELECT S.SPROGRAM, S.SID,S.SNAME,WAIVER,S.PHONE,  (CASE WHEN S.GENDER='M' THEN CR.MALERATE ELSE CR.FEMRATE END ) CR,  ";
                sql += "SUM(OG.CHOURS) CH FROM   STUDENT S  JOIN LOANSANDWAIVER L ON (S.SID = L.SID) JOIN CreditRate CR ON (S.SPROGRAM=CR.DEPCODE AND CR.YEAR=(2000+SUBSTR(S.SID,0,2)) AND (CR.SEMESTER=SUBSTR(S.SID,3,1)) )  ";
                sql += "LEFT JOIN REGISTATUS RS ON (RS.REGKEY LIKE S.SID||'%' AND RS.YEAR='" + Year + "' AND RS.SEMESTER= '" + Semester + "') LEFT JOIN OFFERERINGANDGRADE OG ON (RS.REGKEY=OG.REGKEY)  ";
                sql += "WHERE WAIVER>0 and (L.YEAR = '" + Year + "') AND (L.SEMESTER ='" + Semester + "') GROUP BY S.SPROGRAM, S.SID,S.SNAME,S.PHONE,WAIVER, S.GENDER, CR.MALERATE, CR.FEMRATE ) P ORDER BY SPROGRAM asc, SID desc ";


        return obj_db.get_viewData(sql, "WaiverList");
    }

    public DataTable get_MaleFemaleList(string Semester, string Year)
    {
        string sql = @" SELECT SPROGRAM, NVL(sum(Male),0) Male, NVL(sum(Female),0) Female,  NVL(sum(Male),0)+ NVL(sum(Female),0) Total   ";
                sql += " from (select distinct S.SID,S.SPROGRAM,  CASE WHEN  S.GENDER = 'M'  THEN count(S.SID)  END as Male, CASE  WHEN  S.GENDER = 'F'  THEN count(S.SID)  END as Female   ";
                sql += " from registatus R left join Student S on S.SID=R.SID where R.REGKEY like '%" + Semester + "" + Year + "'   ";
                sql += " group by S.SID,S.GENDER,S.SPROGRAM) group by SPROGRAM order by SPROGRAM ";


        return obj_db.get_viewData(sql, "RegMaleFemaleList");
    }

    public DataTable get_AdmitCardClearnce(string Semester, string Year)
    {
        string sql = @" select ADC.SID, S.SNAME,S.SPROGRAM, ADC.YEAR, ADC.SEMESTER,ADC.APPROVAL_DUES, CASE  WHEN ADC.SEMESTER = 1 THEN 'Spring'  WHEN ADC.SEMESTER = 2 THEN 'Summer'  WHEN ADC.SEMESTER = 3  THEN 'Fall' END AS SEMESTER_Name,   ";
                 sql += "  ADC.EXAMTYPE, CASE WHEN ADC.EXAMTYPE  = 'M'  THEN 'Mid'  WHEN ADC.EXAMTYPE  = 'F' THEN 'Final' END AS Exam, ADC.LEDGER_UPDATE, ADC.INSERTIONID, WTS.STAFF_NAME, ADC.INSERTION_TIME INSERTION_DATETIME, TO_DATE(ADC.INSERTION_TIME, 'MM/DD/YYYY HH:MI:SS AM') INSERTIONDATE,   ";
                 sql += "  TO_CHAR ((TO_DATE(ADC.INSERTION_TIME, 'MM/DD/YYYY HH:MI:SS AM')), 'HH12:MI:SS AM') INSERTIONTIME from E_STUDENTOPENING_CONTENT ADC left join WEB_TEACHER_STAFF WTS on WTS.VALUE= ADC.INSERTIONID   ";
                 sql += " left join Student S on S.sid=ADC.SID  where ADC.YEAR='" + Year + "' and ADC.SEMESTER='" + Semester + "' order by TO_DATE(ADC.INSERTION_TIME, 'MM/DD/YYYY HH:MI:SS AM') asc,ADC.EXAMTYPE desc, S.SPROGRAM asc, S.SID desc ";


                 return obj_db.get_viewData(sql, "CardClearnce");
    }

    public DataTable get_CourseOfferingClearnce(string Semester, string Year)
    {
        string sql = @" select CRSO.SID,S.Sname, S.sprogram,WTS.STAFF_NAME,CRSO.CLEAREDBY,  CRSO.Semester, CRSO.YEAR,CASE  WHEN CRSO.SEMESTER = 1 THEN 'Spring'  WHEN CRSO.SEMESTER = 2 THEN 'Summer'  WHEN CRSO.SEMESTER = 3  THEN 'Fall' END AS SEMESTER_Name, CASE WHEN CRSO.YEAR||CRSO.SEMESTER < '20183'  THEN null  WHEN CRSO.YEAR||CRSO.SEMESTER >= '20183' THEN CRSO.APPROVE_DUES END AS DUES,
                CRSO.CLEARED_TIME CLEARED_DATETIME, TO_DATE(CRSO.CLEARED_TIME, 'MM/DD/YYYY HH:MI:SS AM') CLEAREDDATE, TO_CHAR ((TO_DATE(CRSO.CLEARED_TIME, 'MM/DD/YYYY HH:MI:SS AM')), 'HH12:MI:SS AM') CLEARED_TIME
                from CRS_OFFERING_VALID_STU CRSO left join WEB_TEACHER_STAFF WTS on WTS.VALUE= CRSO.CLEAREDBY  left join Student S on S.SID = CRSO.SID where CRSO.YEAR='" + Year + "' and CRSO.SEMESTER='" + Semester + "' order by TO_DATE(CRSO.CLEARED_TIME, 'MM/DD/YYYY HH:MI:SS AM') asc, S.SPROGRAM asc, S.SID desc ";


        return obj_db.get_viewData(sql, "CourseOfferingClearnce");
    }
    #endregion


    public DataTable get_AdmissionInquiery(string Semester, string Year)
    {
        string sql = @"select distinct * from (select CIAD.* ,C_INSTITUTE.NAME InstituteName, TO_DATE(CIAD.COMING_DATE, 'MM/DD/YYYY HH:MI:SS AM') COMINGDATE, TO_CHAR ((TO_DATE(CIAD.COMING_DATE, 'MM/DD/YYYY HH:MI:SS AM')), 'HH12:MI:SS AM') COMINGTIME, CASE  WHEN CIAD.SEMESTER = 1 THEN 'Spring'  WHEN CIAD.SEMESTER = 2 THEN 'Summer'  WHEN CIAD.SEMESTER = 3  THEN 'Fall' END AS SEMESTER_Name,    ";
        sql += " C_DISTRICT.DISTRICT, C_INQUIRY_REFFERENCE.REFFERENCE, C_PROGINDEPT.NAME Prog_Name, Insertion.staff_NAme InsertedBy, UpdatedBy.staff_NAme UpdatedStaff,    ";
        sql += " TO_DATE(CIAD.INSERTED_DATE, 'MM/DD/YYYY HH:MI:SS AM') INSERTIONDATE,TO_CHAR ((TO_DATE(CIAD.INSERTED_DATE, 'MM/DD/YYYY HH:MI:SS AM')), 'HH12:MI:SS AM') INSERTIONTIME,TO_DATE(CIAD.UPDATED_DATE, 'MM/DD/YYYY HH:MI:SS AM') UPDATEDDATE, TO_CHAR ((TO_DATE(CIAD.UPDATED_DATE, 'MM/DD/YYYY HH:MI:SS AM')), 'HH12:MI:SS AM') UPDATEDTIME   ";
        sql += " from C_INQUIRY_ADMISSION CIAD left join C_DISTRICT on C_DISTRICT.C_DISTRICT_ID = CIAD.DISTRICT_ID left join C_INQUIRY_REFFERENCE on C_INQUIRY_REFFERENCE.C_REFFERENCE_ID = CIAD.REFERRED_BY   left join C_INSTITUTE on C_INSTITUTE.C_INSTITUTE_ID=CIAD.COLLEGE  ";
                 sql += " left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = CIAD.INTERESTED_PROGRAM left join (select C_Inquiry_ID, Inserted_By,WEB_TEACHER_STAFF.staff_NAme  from C_INQUIRY_ADMISSION    ";
                 sql += " left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.VALUE = C_INQUIRY_ADMISSION.INSERTED_BY )Insertion on Insertion.Inserted_By =CIAD.INSERTED_BY     ";
                 sql += " left join (select C_Inquiry_ID, Updated_By,WEB_TEACHER_STAFF.staff_NAme  from C_INQUIRY_ADMISSION left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.VALUE = C_INQUIRY_ADMISSION.Updated_By )  UpdatedBy on UpdatedBy.Updated_By =CIAD.Updated_By    ";
                 sql += " where CIAD.YEAR='" + Year + "' and CIAD.SEMESTER='" + Semester + "' order by TO_DATE(CIAD.INSERTED_DATE, 'MM/DD/YYYY HH:MI:SS AM') asc, Insertion.Inserted_By asc )   order by TO_DATE(INSERTED_DATE, 'MM/DD/YYYY HH:MI:SS AM') desc ";

        return obj_db.get_viewData(sql, "AdmissionInquiery");
    }

    #region WEB_COURSE_OFFERING_TEMP_DEMO

    //course apply by student
    public int InsertIntoDemo(DataSet ds, string INSERTION_TIME, string INSERTIONID)
    {
        string sql = " ";
        int count = 0;
        foreach (DataRow dr in ds.Tables["WEB_COURSE_OFFERING_TEMP"].Rows)
        {
            sql = @" Insert into WEB_COURSE_OFFERING_TEMP_DEMO (COURSEKEY, REGKEY, GGROUP, CHOURS, CTRL, TEACHER_COMMENTS,STUDENT_COMMENTS,INSERTION_TIME,INSERTIONID) ";
            sql += " values ('" + dr["COURSEKEY"] + "' ,  '" + dr["REGKEY"] + "' , '" + dr["GGROUP"] + "','" + dr["CHOURS"] + "',  ";
            sql += "  '" + dr["CTRL"] + "','" + dr["TEACHER_COMMENTS"] + "','" + dr["STUDENT_COMMENTS"] + "','" + INSERTION_TIME + "','" + INSERTIONID + "' ) ";

            if (obj_db.execute_query(sql) == "1")
            {
                count++;
            }

        }


        return count;
    }

    public int InsertOnlineAdmission(string sql)
    {
        int count = 0;
        if (obj_db.execute_query(sql) == "1")
        {
            count = 1;
        }

        return count;
    }

    //course delete by student
    public string DeletedDataIntoDemo(string courseKey, string regKey, string droptime)
    {
        string sql = " ";
        sql = @" Insert into WEB_COURSE_OFFERING_TEMP_DEMO (COURSEKEY, REGKEY, GGROUP, CHOURS, CTRL, TEACHER_COMMENTS,STUDENT_COMMENTS,DELETE_TIME,DELETEID) ";
        sql += "  (select COURSEKEY, REGKEY, GGROUP, CHOURS, CTRL, TEACHER_COMMENTS,STUDENT_COMMENTS,'" + droptime + "' as DELETE_TIME, SUBSTR(REGKEY,1,9)";
        sql += "  from WEB_COURSE_OFFERING_TEMP  where COURSEKEY = '" + courseKey + "' and REGKEY = '" + regKey + "'  ) ";


        string i = obj_db.execute_query(sql);
        return i;
    }


    //student requested course approve by teacher at advising periord
    public int ApproveIntoDemo(DataSet ds, string APPROVETIME, string APPROVEID)
    {
        string sql = " ";
        int count = 0;
        foreach (DataRow dr in ds.Tables["OFFERERINGANDGRADE"].Rows)
        {
            sql = @" Insert into WEB_COURSE_OFFERING_TEMP_DEMO (COURSEKEY, REGKEY, GGROUP, CHOURS,  APPROVETIME,APPROVEID) ";
            sql += " values ('" + dr["COURSEKEY"] + "' ,  '" + dr["REGKEY"] + "' , '" + dr["GGROUP"] + "','" + dr["CHOURS"] + "', ";
            sql += "  '" + APPROVETIME + "','" + APPROVEID + "' ) ";

            if (obj_db.execute_query(sql) == "1")
            {
                count++;
            }
        }

        // string i = obj_db.execute_query(sql);
        return count;
    }

    //student requested course delete by teacher  at advising periord
    public string DeletedDataTecIntoDemo(string courseKey, string regKey, string DROPID, string DROP_TIME)
    {
        string sql = " ";
        sql = @" Insert into WEB_COURSE_OFFERING_TEMP_DEMO (COURSEKEY, REGKEY, GGROUP, CHOURS, CTRL, TEACHER_COMMENTS,STUDENT_COMMENTS,DROP_TIME,DROPID) ";
        sql += "  (select COURSEKEY, REGKEY, GGROUP, CHOURS, CTRL, TEACHER_COMMENTS,STUDENT_COMMENTS,'" + DROP_TIME + "' as DROP_TIME, '" + DROPID + "' as DROPID";
        sql += "  from WEB_COURSE_OFFERING_TEMP   where COURSEKEY = '" + courseKey + "' and REGKEY = '" + regKey + "'  ) ";


        string i = obj_db.execute_query(sql);
        return i;
    }


    //newly course add by teacher  at advising periord
    public string NewInsertIntoDemo(DataSet ds, string INSERTIONTIME_TEC, string INSERTIONIDTEC)
    {
        string sql = " ";
        foreach (DataRow dr in ds.Tables["WEB_COURSE_OFFERING_TEMP"].Rows)
        {
            sql = @" Insert into WEB_COURSE_OFFERING_TEMP_DEMO (COURSEKEY, REGKEY, GGROUP, CHOURS, CTRL, TEACHER_COMMENTS,STUDENT_COMMENTS,INSERTIONTIME_TEC,INSERTIONIDTEC) ";
            sql += " values ('" + dr["COURSEKEY"] + "' ,  '" + dr["REGKEY"] + "' , '" + dr["GGROUP"] + "', '" + dr["CHOURS"] + "',  ";
            sql += "  '" + dr["CTRL"] + "','" + dr["TEACHER_COMMENTS"] + "','" + dr["STUDENT_COMMENTS"] + "','" + INSERTIONTIME_TEC + "','" + INSERTIONIDTEC + "' ) ";
        }

        string i = obj_db.execute_query(sql);
        return i;
    }


    //student requested course approve by teacher at readvising periord
    public int Approve_IntoDemo(DataSet ds, string NEWAPPROVETIME, string NEWAPPROVEID)
    {
        string sql = " ";
        int count = 0;
        foreach (DataRow dr in ds.Tables["OFFERERINGANDGRADE"].Rows)
        {
            sql = @" Insert into WEB_COURSE_OFFERING_TEMP_DEMO (COURSEKEY, REGKEY, GGROUP, CHOURS,  NEWAPPROVETIME,NEWAPPROVEID) ";
            sql += " values ('" + dr["COURSEKEY"] + "' ,  '" + dr["REGKEY"] + "' , '" + dr["GGROUP"] + "','" + dr["CHOURS"] + "', ";
            sql += "  '" + NEWAPPROVETIME + "','" + NEWAPPROVEID + "' ) ";

            if (obj_db.execute_query(sql) == "1")
            {
                count++;
            }
        }

        // string i = obj_db.execute_query(sql);
        return count;
    }

    //student requested course delete by teacher  at readvising periord
    public string Deleted_DataTecIntoDemo(string courseKey, string regKey, string REDROPID, string REDROPTIME)
    {
        string sql = " ";
        sql = @" Insert into WEB_COURSE_OFFERING_TEMP_DEMO (COURSEKEY, REGKEY, GGROUP, CHOURS, CTRL, TEACHER_COMMENTS,STUDENT_COMMENTS,REDROPTIME,REDROPID) ";
        sql += "  (select COURSEKEY, REGKEY, GGROUP, CHOURS, CTRL, TEACHER_COMMENTS,STUDENT_COMMENTS,'" + REDROPTIME + "' as REDROPTIME, '" + REDROPID + "' as REDROPID";
        sql += "  from WEB_COURSE_OFFERING_TEMP   where COURSEKEY = '" + courseKey + "' and REGKEY = '" + regKey + "'  ) ";


        string i = obj_db.execute_query(sql);
        return i;
    }


    //newly course add by teacher  at readvising periord
    public string New_InsertIntoDemo(DataSet ds, string NEWINSERTIONTIMETEC, string NEWINSERTIONIDTEC)
    {
        string sql = " ";
        foreach (DataRow dr in ds.Tables["WEB_COURSE_OFFERING_TEMP"].Rows)
        {
            sql = @" Insert into WEB_COURSE_OFFERING_TEMP_DEMO (COURSEKEY, REGKEY, GGROUP, CHOURS, CTRL, TEACHER_COMMENTS,STUDENT_COMMENTS,NEWINSERTIONTIMETEC,NEWINSERTIONIDTEC) ";
            sql += " values ('" + dr["COURSEKEY"] + "' ,  '" + dr["REGKEY"] + "' , '" + dr["GGROUP"] + "','" + dr["CHOURS"] + "',  ";
            sql += "  '" + dr["CTRL"] + "','" + dr["TEACHER_COMMENTS"] + "','" + dr["STUDENT_COMMENTS"] + "','" + NEWINSERTIONTIMETEC + "','" + NEWINSERTIONIDTEC + "' ) ";
        }

        string i = obj_db.execute_query(sql);
        return i;
    }





    #endregion



















    public string save_pre_offering(DataSet ds)
    {
        return obj_db.insert_general(ds, "WEB_COURSE_OFFERING_TEMP");
    }

    public bool is_register_student(string sid, string semester, string year)
    {
        DataSet ds = new DataSet();

        string sql = " Select * from REGISTATUS where REGKEY='" +sid+semester+year+ "' ";
        ds.Merge(obj_db.get_viewData(sql, "REGISTATUS"));

        if (ds.Tables["REGISTATUS"].Rows.Count > 0)
           return true;
        else return false;

    }
    #region Student_Message
    public DataTable get_student_Message(string sid)
    {

        string sql = @" Select * from WEB_Student_Message where (ReciverStudent_ID ='" + sid + "' or ( batchID like  substr('" + sid + "', 1, 2) or  batchID like  substr('" + sid + "', 1, 3)";
        sql += " or batchID like  substr('" + sid + "', 1, 4) or batchID like  substr('" + sid + "', 1, 5)  or batchID like  substr('" + sid + "', 1, 6) or batchID like  substr('" + sid + "', 1, 7) ";
        sql += " or batchID like  substr('" + sid + "', 1, 8) or batchID like  substr('" + sid + "', 1, 9) ) ";
        sql += " or STDGRPID like  '%" + sid + "%' ";
        sql += " or (ReciverStudent_ID is null and batchID is null and STDGRPID is null)) ";
        sql += " and CTRL=1 order by INPUT_DATE desc, Message_ID desc ";
        //string sql = @" Select * from WEB_Student_Message where ReciverStudent_ID ='" + sid + "' or '" + sid + "' like  BATCHID"+"'%' and CTRL=1 order by Message_ID desc ";
        return obj_db.get_viewData(sql, "WEB_Student_Message");
    }
    public DataTable get_a_message_details(string MESSAGE_ID)
    {
        string sql = " SELECT  * from WEB_STUDENT_MESSAGE where MESSAGE_ID ='" + MESSAGE_ID + "'   ";

        return obj_db.get_viewData(sql, "WEB_STUDENT_MESSAGE");
    }

    #endregion



    #region Alumni

    //-------------------------check alumni login
    public DataTable check_AlumniSID_login(string user_id, string user_pass)
    {
        string sql = @"Select * from EU_ALUMNI where ALUMNI_TRANID='" + user_id + "' and CPASSWORD='" + user_pass + "' and  ISBLOCK ='N' ";
        return obj_db.get_viewData(sql, "EU_ALUMNI");
    }

    public DataTable check_Alumni_login(string user_id, string user_pass)
    {
        string sql = @"Select EU_ALUMNI.*, ESID.SID from EU_ALUMNI left join EU_ALUMNI_SID ESID on ESID.ALUMNI_TRANID= EU_ALUMNI.ALUMNI_TRANID where SID='" + user_id + "' and CPASSWORD='" + user_pass + "' and  ISBLOCK ='N' ";
        return obj_db.get_viewData(sql, "EU_ALUMNI");
    }
    public bool check_alumni_loginAlumniTable(string user_id, string user_pass)
    {
        DataSet ds = new DataSet();
        bool status = false;

        string sql = @"Select distinct EU_ALUMNI.* from EU_ALUMNI left join EU_ALUMNI_SID ESID on ESID.ALUMNI_TRANID= EU_ALUMNI.ALUMNI_TRANID where EU_ALUMNI.ALUMNI_TRANID ='" + user_id + "' and CPASSWORD='" + user_pass + "' and  ISBLOCK ='N' ";
        ds.Merge(obj_db.get_viewData(sql, "EU_ALUMNI"));

        if (ds.Tables["EU_ALUMNI"].Rows.Count > 0)
        {
            status = true;
            //DataRow dr = ds.Tables["EU_ALUMNI"].Rows[0];
            //if (dr["SID"].ToString() != "" && dr["CPASSWORD"].ToString() != "")
            //{
            //    if (dr["SID"].ToString() == user_id && dr["CPASSWORD"].ToString() == user_pass)
            //    {
            //        status = true;
            //    }

            //}
        }

        return status;
    }


    public bool check_alumni_Registered(string user_id, string user_id1)
    {
        DataSet ds = new DataSet();
        bool status = false;

        string sql = @"Select * from EU_ALUMNI_SID where (SID='" + user_id + "' or SID='" + user_id1 + "')   ";
        //and VALID != 'N' 
        ds.Merge(obj_db.get_viewData(sql, "EU_ALUMNI_SID"));

        if (ds.Tables["EU_ALUMNI_SID"].Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    public DataTable check_alumni_SID_Registered(string user_id)
    {
        // string sql = @"Select EU_ALUMNI.*, ESID.SID from EU_ALUMNI left join EU_ALUMNI_SID ESID on ESID.ALUMNI_TRANID= EU_ALUMNI.ALUMNI_TRANID where SID='" + user_id + "' and CPASSWORD='" + user_pass + "' and  ISBLOCK ='N' ";

        string sql = @" SELECT distinct P.SID,P.SNAME,P.DOB,ST.SID SECONDSID,P.SPROGRAM,ST.SPROGRAM SECONDSPROGRAM,P.GRADUATIONDATE, ST.GRADUATIONDATE SECONDGRADUATIONDATE,
                        P.BLOODGROUP,ST.BLOODGROUP SECONDBLOODGROUP FROM (SELECT distinct S.SID,S.SNAME,S.DOB,S.SPROGRAM,S.BLOODGROUP,S.GRADUATIONDATE  FROM 
                        STUDENT S LEFT JOIN STUDMOTHERDET M ON (S.SID = M.SID) LEFT JOIN STUDFATHERDET F ON (S.SID = F.SID)
                        WHERE S.SID = '" + user_id + @"' ) P  LEFT JOIN STUDENT ST ON  (LOWER(P.SNAME)=LOWER(ST.SNAME) AND  P.DOB = ST.DOB AND  P.SID<>ST.SID AND  LOWER(P.BLOODGROUP)=LOWER(ST.BLOODGROUP)  )  
                        where (ST.SID = '" + user_id + @"' or P.SID ='" + user_id + @"') AND (ST.GRADUATIONDATE is not null or P.GRADUATIONDATE is not null)  ORDER BY P.SNAME,P.BLOODGROUP ";


        return obj_db.get_viewData(sql, "EU_ALUMNI_Registered_List");
    }


    public DataTable check_Missing_TrunID()
    {
       // string sql = @"SELECT min(alumni_tranid) ALUMNI_TRANID from VW_ALUMNI_TRANID ";
        string sql = @"SELECT min(VW_ALUMNI_TRANID.ALUMNI_TRANID) ALUMNI_TRANID from VW_ALUMNI_TRANID  left join EU_ALUMNI_SID on EU_ALUMNI_SID.ALUMNI_TRANID = VW_ALUMNI_TRANID.ALUMNI_TRANID where EU_ALUMNI_SID.ALUMNI_TRANID is null and VW_ALUMNI_TRANID.ALUMNI_TRANID > 'EUAL000084'";

        return obj_db.get_viewData(sql, "EU_ALUMNI_MissingTrunID");
    }

    public DataTable check_alumni_SID_Saved(string user_id)
    {
        string sql = @"Select EU_ALUMNI_SID.* from EU_ALUMNI_SID where SID='" + user_id + "' ";


        return obj_db.get_viewData(sql, "EU_ALUMNI_Registered_List");
    }

    public DataTable check_alumni_SID(string user_id, string user_pass)
    {
        string sql = @"Select EU_ALUMNI.*, ESID.SID from EU_ALUMNI left join EU_ALUMNI_SID ESID on ESID.ALUMNI_TRANID= EU_ALUMNI.ALUMNI_TRANID where EU_ALUMNI.ALUMNI_TRANID ='" + user_id + "' and CPASSWORD='" + user_pass + "' and  ISBLOCK ='N' ";

        return obj_db.get_viewData(sql, "EU_ALUMNI");
    }


    public bool check_alumni_loginAlumniView(string user_id, string user_pass)
    {
        DataSet ds = new DataSet();
        bool status = false;

        string sql = @"Select EU_ALUMNI.*, ESID.SID from VW_ALUMNI_PASSWORD where SID='" + user_id + "' and MATCHPASS_DOB='" + user_pass + "'   ";
        ds.Merge(obj_db.get_viewData(sql, "EU_ALUMNI_PASSMATCH"));

        if (ds.Tables["EU_ALUMNI_PASSMATCH"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["EU_ALUMNI_PASSMATCH"].Rows[0];
            if (dr["SID"].ToString() != "" && dr["MATCHPASS_DOB"].ToString() != "")
            {
                if (dr["SID"].ToString() == user_id && dr["MATCHPASS_DOB"].ToString() == user_pass)
                {
                    status = true;
                }

            }
        }

        return status;
    }


    public bool check_alumni_PrevData(string user_id, string DEPT_ID, string CGPA, string GRADUATIONSEMESTER, string GRADUATIONYEAR)
    {
        DataSet ds = new DataSet();
        bool status = false;

        string sql = @"Select * from VW_ALUMNI_PASSWORD where SID='" + user_id + "' and COLLEGECODE='" + DEPT_ID + "' and TCGPA='" + CGPA + "' and GRADUATIONYEAR='" + GRADUATIONYEAR + "' and GRADUATIONSEMESTER='" + GRADUATIONSEMESTER + "'  ";
        ds.Merge(obj_db.get_viewData(sql, "EU_ALUMNI_MATCH"));

        if (ds.Tables["EU_ALUMNI_MATCH"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["EU_ALUMNI_MATCH"].Rows[0];
            if (dr["SID"].ToString() != "" && dr["COLLEGECODE"].ToString() != "" && dr["TCGPA"].ToString() != ""
                && dr["GRADUATIONYEAR"].ToString() != "" && dr["GRADUATIONSEMESTER"].ToString() != "")
            {

                status = true;


            }
        }

        return status;
    }


    //public string get_database_formate(DateTime dt)
    //{
    //    string date = "";
    //    date = "" + dt.Month;
    //    date += "/" + (dt.Day);
    //    date += "/" + dt.Year;

    //    return date;
    //}
    public bool check_alumni_NewDataDOB(string user_id, string DEPT_ID, string CGPA, string GRADUATIONSEMESTER, string GRADUATIONYEAR, string DOB)
    {
        DataSet ds = new DataSet();
        bool status = false;

        string sql = @"Select * from VW_ALUMNI_PASSWORD where SID='" + user_id + "' and COLLEGECODE='" + DEPT_ID + "' and (TCGPA='" + CGPA + "' or CGPA='" + CGPA + "') and GRADUATIONYEAR='" + GRADUATIONYEAR + "' and GRADUATIONSEMESTER='" + GRADUATIONSEMESTER + "' and matchpass ='" + DOB + "'  ";
        ds.Merge(obj_db.get_viewData(sql, "EU_ALUMNI_MATCH"));

        if (ds.Tables["EU_ALUMNI_MATCH"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["EU_ALUMNI_MATCH"].Rows[0];
            if (dr["SID"].ToString() != "" && dr["COLLEGECODE"].ToString() != "" && dr["TCGPA"].ToString() != ""
                && dr["GRADUATIONYEAR"].ToString() != "" && dr["GRADUATIONSEMESTER"].ToString() != "")
            {
                status = true;
                //if (dr["SID"].ToString() == user_id && dr["COLLEGECODE"].ToString() == DEPT_ID && (dr["TCGPA"].ToString() == CGPA || dr["CGPA"].ToString() == CGPA)
                //    && dr["GRADUATIONYEAR"].ToString() == GRADUATIONYEAR && dr["GRADUATIONSEMESTER"].ToString() == GRADUATIONSEMESTER)
                //{
                  
                //}

            }
        }

        return status;
    }

    public bool check_alumni_PrevDataDOB(string user_id, string DEPT_ID, string CGPA, string GRADUATIONSEMESTER, string GRADUATIONYEAR, DateTime DOB)
    {
        DataSet ds = new DataSet();
        bool status = false;

        string sql = @"Select * from VW_ALUMNI_PASSWORD where SID='" + user_id + "' and COLLEGECODE='" + DEPT_ID + "' and (TCGPA='" + CGPA + "' or CGPA='" + CGPA + "') and GRADUATIONYEAR='" + GRADUATIONYEAR + "' and GRADUATIONSEMESTER='" + GRADUATIONSEMESTER + "' and DOB =TO_DATE('" + new cls_tools().get_database_formateDate(DOB) + "', 'dd/mm/yyyy hh24:mi:ss') ' ";
        ds.Merge(obj_db.get_viewData(sql, "EU_ALUMNI_MATCH"));

        if (ds.Tables["EU_ALUMNI_MATCH"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["EU_ALUMNI_MATCH"].Rows[0];
            if (dr["SID"].ToString() != "" && dr["COLLEGECODE"].ToString() != "" && dr["TCGPA"].ToString() != ""
                && dr["GRADUATIONYEAR"].ToString() != "" && dr["GRADUATIONSEMESTER"].ToString() != "")
            {
                status = true;
                //if (dr["SID"].ToString() == user_id && dr["COLLEGECODE"].ToString() == DEPT_ID && dr["TCGPA"].ToString() == CGPA
                //    && dr["GRADUATIONYEAR"].ToString() == GRADUATIONYEAR && dr["GRADUATIONSEMESTER"].ToString() == GRADUATIONSEMESTER)
                //{
                   
                //}

            }
        }

        return status;
    }


    //-------------------------end process


    #endregion



    #region Student_GuiLedger
    public bool get_Payment_dateRange(string SID)
    {
        bool status = false;
        DataSet ds = new DataSet();
        string sql = @" Select * from STUDENTDEBIT where SID ='" + SID + "'  and ( created  >= TO_DATE('" + new cls_tools().get_database_formateDate(DateTime.Today) + "', 'dd/mm/yyyy hh24:mi:ss') )  ";
        //'" + new cls_tools().get_database_formateDate(DateTime.Today) + "' = PDATE or 
        ds.Merge(obj_db.get_viewData(sql, "STUDENTDEBIT"));

        foreach (DataRow dr in ds.Tables["STUDENTDEBIT"].Rows)
        {
            if (!String.IsNullOrEmpty(dr["created"].ToString()))
            {
                status = true;

            }
        }
        return status;
    }


    public bool Chk_StdControl(string SID)
    {
        bool status = false;
        DataSet ds = new DataSet();
        string sql = @" Select * from E_STUDENT_CONTROL where SID ='" + SID + "'  and ( LEDGER_UPDATE  >= TO_DATE('" + new cls_tools().get_database_formateDate(DateTime.Today) + "', 'dd/mm/yyyy hh24:mi:ss') )  ";

        ds.Merge(obj_db.get_viewData(sql, "E_STUDENT_CONTROL"));

        foreach (DataRow dr in ds.Tables["E_STUDENT_CONTROL"].Rows)
        {
            if (!String.IsNullOrEmpty(dr["LEDGER_UPDATE"].ToString()))
            {
                status = true;

            }
        }
        return status;
    }

    #endregion












    #region Convocation

    public bool check_ConvoInfo(string user_id, string DEPT_ID, string CGPA, string RegistusSEMESTER, string RegistusYEAR)
    {
        DataSet ds = new DataSet();
        bool status = false;

        string sql = @"Select * from VW_CONVOCATION where SID='" + user_id + "' and COLLEGECODE='" + DEPT_ID + "' and (TCGPA='" + CGPA + "' or CGPA='" + CGPA + "') and ((LRY='" + RegistusYEAR + "' and LRS ='" + RegistusSEMESTER + "') or (graduationyear='" + RegistusYEAR + "' and graduationsemester ='" + RegistusSEMESTER + "'))   ";
        ds.Merge(obj_db.get_viewData(sql, "EU_ALUMNI_MATCH"));

        if (ds.Tables["EU_ALUMNI_MATCH"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["EU_ALUMNI_MATCH"].Rows[0];
            if (dr["SID"].ToString() != "" && dr["COLLEGECODE"].ToString() != "" && dr["TCGPA"].ToString() != ""
                && dr["LRY"].ToString() != "" && dr["LRS"].ToString() != "")
            {
                status = true;

            }
        }

        return status;
    }
    public bool check_ConvoInfo_prev(string user_id, string DEPT_ID, string CGPA, string RegistusSEMESTER, string RegistusYEAR)
    {
        DataSet ds = new DataSet();
        bool status = false;
        string sql = @"Select * from VW_CONVOCATION_PREV where SID='" + user_id + "' and COLLEGECODE='" + DEPT_ID + "' and (TCGPA='" + CGPA + "' or CGPA='" + CGPA + "') and ((LRY='" + RegistusYEAR + "' and LRS ='" + RegistusSEMESTER + "') or (graduationyear='" + RegistusYEAR + "' and graduationsemester ='" + RegistusSEMESTER + "'))   ";
       
       // string sql = @"Select * from VW_CONVOCATION where SID='" + user_id + "' and COLLEGECODE='" + DEPT_ID + "' and (TCGPA='" + CGPA + "' or CGPA='" + CGPA + "') and ((LRY='" + RegistusYEAR + "' and LRS ='" + RegistusSEMESTER + "') or (graduationyear='" + RegistusYEAR + "' and graduationsemester ='" + RegistusSEMESTER + "'))   ";
        ds.Merge(obj_db.get_viewData(sql, "EU_ALUMNI_MATCH"));

        if (ds.Tables["EU_ALUMNI_MATCH"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["EU_ALUMNI_MATCH"].Rows[0];
            if (dr["SID"].ToString() != "" && dr["COLLEGECODE"].ToString() != "" && dr["TCGPA"].ToString() != ""
                && dr["LRY"].ToString() != "" && dr["LRS"].ToString() != "")
            {
                status = true;

            }
        }

        return status;
    }

    public DataTable check_ConvoBlock_prev(string user_id)
    {
        string sql = @"select * from EU_CONVOCATION_BLOCK CBLK where (CBLK.PAPERMISSING is not null or CBLK.VARIFICATION is not null or CBLK.LIBRARY is not null) and SID ='" + user_id + "'    ";
        return obj_db.get_viewData(sql, "EU_CONVOBLK");
    }


    public DataTable check_ConvoBlock(string user_id)
    {
        string sql = @"select * from EU_CONVOCATION_BLOCK CBLK where (CBLK.PAPERMISSING is not null or CBLK.VARIFICATION is not null or CBLK.LIBRARY is not null) and CONVO_YEAR = 2019 and SID ='" + user_id + "'    ";
        return obj_db.get_viewData(sql, "EU_CONVOBLK");
    }

    public DataTable check_ConvoSuccess(string user_id)
    {
        string sql = @"select SID from VW_CONVOSUCCESS where SID ='" + user_id + "'    ";
        return obj_db.get_viewData(sql, "EU_CONVOSUCS");
    }

    public DataTable check_ConvoDoubleSID(string user_id)
    {
        string sql = @"select * from EU_CONVO_DOUBLESID where (SID ='" + user_id + "' or ALTER_SID='" + user_id + "') and CONVO_YEAR=2019    ";//for 2017
        return obj_db.get_viewData(sql, "EU_CONVODOUBLESID");
    }

    public DataTable get_Convocation_student_information(string student_id)
    {
        string sql = @" SELECT * FROM VW_CONVOCATION  WHERE sid='" + student_id + "' ";
        return obj_db.get_viewData(sql, "CONVOSTD");
    }

    public DataTable get_ConvocationStudent(string student_id)
    {
        DataSet ds = new DataSet();
        string sql = @" SELECT EU_CONVOCATION.*, Student.EMAIL EmailAdd, Student.PHONE FROM EU_CONVOCATION  left join student on student.SID = EU_CONVOCATION.SID WHERE EU_CONVOCATION.sid='" + student_id + "' ";
        ds.Merge(obj_db.get_viewData(sql, "EU_CONVOCHK"));
        //obj_db.execute_query(" Delete from EU_CONVOCATION where SID='" + student_id + "' ");
        if (ds.Tables["EU_CONVOCHK"].Rows.Count > 0)
        {
            return obj_db.get_viewData(sql, "EU_CONVOCHK");
           // obj_db.execute_query(" Delete from EU_CONVOCATION where SID='" + student_id + "' ");
          //  obj_db.execute_query(" update T_STUDENTDEBIT set TRANID= 'EU" + code + "' where SID='" + dr["SID"] + "'  ");
        }

      //  return status;
        return obj_db.get_viewData(sql, "EU_CONVOCHK");
    }





    public string insert_Con_stdDebit(DataSet ds, DataSet dsGst)
    {
        string sql = "", sql1 = "", sql2 = "", sqlLog = "", sqlLogStmt = "";
        int count = 0;
        string code = "";

        DataTable dt = new DataTable();
        string T_STUDENTDEBIT = "T_STUDENTDEBIT";
        code = obj_db.call_procedure("SP_GET_SEQ_VALUE", T_STUDENTDEBIT);

        code = format_code(code);


        Session["TRANID"] = "EU" + "" + code;

        try
        {

            foreach (DataRow dr in ds.Tables["EU_CONVOCATION"].Rows)
            {
                sql = @" insert into EU_CONVOCATION (TRANID,SID,YEAR,SEMESTER, GAUSTNO,GAUSTFEE, REGISTERFEE, TOTALFEE, STATUS,CREATEDBY,PDATE,EMPSTATUS,DESIGNATION,ORGNAME,ORGADDR,ORGCONT,PRESENT_ADDRESS,CONVOCATION_YEAR,REG_TYPE, CONTACT,EMAIL, PICKUP_POINT)";
                sql += " values ('EU" + code + "', '" + dr["SID"] + "','" + dr["YEAR"] + "','" + dr["SEMESTER"] + "', '" + dr["GAUSTNO"] + "','" + dr["GAUSTFEE"] + "',  '" + dr["REGISTERFEE"] + "', '" + dr["TOTALFEE"] + "', '" + dr["STATUS"] + "', ";
                sql += "'" + dr["SID"] + "',  TO_DATE('" + dr["PDATE"] + "', 'dd/mm/yyyy hh24:mi:ss') ,  '" + dr["EMPSTATUS"] + "',  '" + dr["DESIGNATION"] + "',  '" + dr["ORGNAME"] + "',  '" + dr["ORGADDR"] + "',  '" + dr["ORGCONT"] + "' ,  '" + dr["PRESENT_ADDRESS"] + "' ,  '" + dr["CONVOCATION_YEAR"] + "',  '" + dr["REG_TYPE"] + "',  '" + dr["PHONE"] + "', '" + dr["EMAIL"] + "',  '" + dr["PICKUP_POINT"] + "') ";


                sql1 = @" insert into T_STUDENTDEBIT (TRANID,HEADSN,SID,YEAR,SEMESTER, AMOUNT, STATUS,CREATEDBY)";
                sql1 += " values ('EU" + code + "', '" + dr["HEADSN"] + "','" + dr["SID"] + "','" + dr["YEAR"] + "','" + dr["SEMESTER"] + "', '" + dr["TOTALFEE"] + "', '" + dr["STATUS"] + "', ";
                sql1 += "'" + dr["SID"] + "' ) ";

                sqlLogStmt = " values (EU" + code + ", " + dr["HEADSN"] + "," + dr["SID"] + "," + dr["YEAR"] + "," + dr["SEMESTER"] + ", " + dr["TOTALFEE"] + ", " + dr["STATUS"] + ", " + dr["SID"] + ")";
                sqlLog = @"INSERT INTO ERROR_LOG VALUES ( 'EU" + code + "', '" + sqlLogStmt + "' ) ";
                obj_db.execute_query(sqlLog);

              //  sqlLog = @"INSERT INTO ERROR_LOG VALUES ( 'EU" + code + "', '" + sql1 + "' ) ";
              //  obj_db.execute_query(sqlLog);
            }

            foreach (DataRow drGst in dsGst.Tables["EU_CONVOCATION_GUEST"].Rows)
            {
                sql2 = @" insert into EU_CONVOCATION_GUEST (TRANID,SID,GNAME,RELATIONSHIP, ADDRESS,GR_PHONE)";
                sql2 += " values ('EU" + code + "','" + drGst["SID"] + "','" + drGst["GNAME"] + "','" + drGst["RELATIONSHIP"] + "','" + drGst["ADDRESS"] + "','" + drGst["GR_PHONE"] + "' ) ";
            }
            if (obj_db.execute_query(sql) != "1" || obj_db.execute_query(sql1) != "1" || obj_db.execute_query(sql2) != "1")
            {
                return "0";
            }

        }
        catch
        {
            return "0";
        }

        return Convert.ToString(Session["TRANID"]);

    }
	
	
	
	
    private string insertGst(DataSet dsGst, String TRANID)
    {
        int count = 0;
        string sql2 = "";
        foreach (DataRow drGst in dsGst.Tables["EU_CONVOCATION_GUEST"].Rows)
        {
            sql2 = @" insert into EU_CONVOCATION_GUEST (TRANID,SID,GNAME,RELATIONSHIP, ADDRESS,GR_PHONE)";
            sql2 += " values ('" + TRANID + "', '" + drGst["SID"] + "','" + drGst["GNAME"] + "','" + drGst["RELATIONSHIP"] + "','" + drGst["ADDRESS"] + "','" + drGst["GR_PHONE"] + "' ) ";

            if (obj_db.execute_query(sql2) == "1")
            {
                count++;
            }
        }
        return Convert.ToString(count);
        //   return (obj_db.execute_query(sql2));

    }


    public DataTable get_ConSTUDENT_Vourcher(string Year, string Semister, string sid, string Vourcher, string HEADSN)
    {
        string sql = " SELECT * FROM STUDENTDEBIT WHERE YEAR ='" + Year + "' and SID='" + sid + "'  and SEMESTER ='" + Semister + "' and VOUCARNO ='" + Vourcher + "' and HEADSN ='" + HEADSN + "' ";
        return obj_db.get_viewData(sql, "STUDENTDEBIT");
    }

    public string update_code(string objects, string stNo)
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

        string sql = @" update WEB_CODES set SERIAL='" + code + "' where OBJECT='" + objects + "' ";
        return obj_db.execute_query(sql);
    }



  
    

    public DataTable get_ConSTUDENT_Payment(string TRAN_ID)
    {
        string sql = "select EU_CONVOCATION.*, S.PHONE PHONE from EU_CONVOCATION left join Student S on S.SID = EU_CONVOCATION.SID where TRANID = '" + TRAN_ID + "' ";
        return obj_db.get_viewData(sql, "EU_CONVOCATION");
    }

    public string format_code(string serial)
    {
        string code = "";
        int no = Convert.ToInt32(serial);
      //  no++;
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

        return code;
    }


    public string update_STranID(DataSet ds, DataSet dsGst, string TrunIDC)
    {
        string sql = " ";
        string sql1 = " ";
        string sql2 = " ";
        int count = 0;
        string code = "";
        // string code = "" + obj_db.get_pk_no("T_STUDENTDEBIT");

        DataTable dt = new DataTable();
        string objectCode = "T_STUDENTDEBIT";
        code = obj_db.call_procedure("SP_GET_SEQ_VALUE", objectCode);

        code= format_code(code);

     
        Session["TRANID"] = "EU" + "" + code;



        foreach (DataRow dr in ds.Tables["EU_CONVOCATION"].Rows)
        {
            sql = @" update EU_CONVOCATION set TRANID= 'EUC" + code + "' where SID='" + dr["SID"] + "'  ";

            sql1 = @" insert into T_STUDENTDEBIT (TRANID,HEADSN,SID,YEAR,SEMESTER, AMOUNT, STATUS,CREATEDBY)";
            sql1 += " values ('EU" + code + "', '" + dr["HEADSN"] + "','" + dr["SID"] + "','" + dr["YEAR"] + "','" + dr["SEMESTER"] + "', '" + dr["TOTALFEE"] + "', '" + dr["STATUS"] + "', ";
            sql1 += "'" + dr["SID"] + "'  ) ";
            // sql1 = @" update T_STUDENTDEBIT set TRANID= 'EU" + code + "' where SID='" + dr["SID"] + "' and HEADSN= '" + dr["HEADSN"] + "' ";

            foreach (DataRow drGst in dsGst.Tables["EU_CONVOCATION_GUEST"].Rows)
            {
                sql2 = @" update EU_CONVOCATION_GUEST set TRANID= 'EUC" + code + "' where SID='" + drGst["SID"] + "' ";

            }
            break;


        }

        string i = obj_db.execute_query(sql);
        string a = obj_db.execute_query(sql1);
        string b = obj_db.execute_query(sql2);

        if (i == "1" && a == "1" && b == "1")
        {
            count++;
        }

        if (i != "1" && a != "1")
        {
            return "0";
        }
        else
        {
            count++;
        }
     //  update_code("T_STUDENTDEBIT", code);

        return Convert.ToString(Session["TRANID"]);


    }

    public string UpstrudentInfo(DataSet ds)
    {
        string sql = " ";
        string sql1 = " ";

        foreach (DataRow dr in ds.Tables["EU_CONVOCATION"].Rows)
        {
            sql = @" update student set PHONE= '" + dr["PHONE"] + "', EMAIL= '" + dr["EMAIL"] + "', S_PICTURE= '" + dr["S_PICTURE"] + "'  where SID='" + dr["SID"] + "'  ";
            break;
        }

        return obj_db.execute_query(sql);

    }

    public string UpEU_Convocation(string Vourcher, string sid, string TRANID)
    {
        string sql = @" update EU_CONVOCATION set VOUCARNO ='" + Vourcher + "', TRANID ='' where SID='" + sid + "' ";

        string i = obj_db.execute_query(sql);
        return Convert.ToString(i);

    }

    public string UpEU_ConvocationFail(string Vourcher, string sid, string TRANID)
    {
        string sql = @" update EU_CONVOCATION set VOUCARNO ='', TRANID ='" + TRANID + "' where SID='" + sid + "' ";

        string i = obj_db.execute_query(sql);
        return Convert.ToString(i);

    }

    public string delete_T_StudentDebit(string sid, string HEADSN)
    {
        return obj_db.execute_query(" Delete from T_STUDENTDEBIT where SID='" + sid + "' and HEADSN='" + HEADSN + "' ");
    }


    public string ClearStdPapermissing(string sid)
    {
        string sql = @"update EU_CONVOCATION_BLOCK set PAPERMISSING ='' where SID = '" + sid + "' and CONVO_YEAR='2019' ";

        string i = obj_db.execute_query(sql);
        return Convert.ToString(i);
    }

    public string ClearStdPVarif(string sid)
    {
        string sql = @"update EU_CONVOCATION_BLOCK set VARIFICATION ='' where SID = '" + sid + "' and CONVO_YEAR='2019' ";
        string i = obj_db.execute_query(sql);
        return Convert.ToString(i);
    }
    public string ClearStdLibrary(string sid)
    {
        string sql = @"update EU_CONVOCATION_BLOCK set LIBRARY ='' where SID = '" + sid + "' and CONVO_YEAR='2019' ";
        string i = obj_db.execute_query(sql);
        return Convert.ToString(i);
    }

    public string get_StudentName(string sid, string DEPTCODE)
    {
        string Sname = "";
        DataSet ds = new DataSet();

        string sql = @"select S.SID,S.SNAME, S.SPROGRAM, S.PROGRAM_ID, C_PROGINDEPT.DEPID, C_DEPARTMENTINFACULTY.COLLEGECODE
                        from student S 
                        left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID=S.PROGRAM_ID
                        left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID =C_PROGINDEPT.DEPID
                        where S.SID='" + sid + "' and C_DEPARTMENTINFACULTY.COLLEGECODE = '" + DEPTCODE + "'  ";


        ds.Merge(obj_db.get_viewData(sql, "Student"));

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables["Student"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["Student"].Rows)
                {
                    Sname = dr["SNAME"].ToString();
                }
            }
        }

        return Convert.ToString(Sname);
    }

    public string FindStdName(string sid)
    {
        string Sname = "";
        DataSet ds = new DataSet();

        string sql = @"select SID, SNAME from student where SID = '" + sid + "' ";


        ds.Merge(obj_db.get_viewData(sql, "Student"));

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables["Student"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["Student"].Rows)
                {
                    Sname = dr["SNAME"].ToString();
                }
            }
        }

        return Convert.ToString(Sname);
    }

    public DataTable get_block_student()
    {


        string sql = @" select CBLK.*, Student.SNAME from EU_CONVOCATION_BLOCK CBLK 
                    left join Student on Student.SID = CBLK.SID
                    where (CBLK.PAPERMISSING is not null or CBLK.VARIFICATION is not null 
                    or CBLK.LIBRARY is not null) and CBLK.CONVO_YEAR ='2019' order by CBLK.SID ";

        return obj_db.get_viewData(sql, "student");
    }

    public DataTable get_Convo_student( string batch)
    {
      //  string sql = @" select VWC.SID, VWC.SNAME, VWC.COLLEGENAME, VWC.LRY, VWC.LSEMN, VWC.TCGPA, VWC.CGPA, VWC.DUE, VWC.phone  from VW_CONVOCATION VWC where VWC.SID like '" + batch + "%' order by SID asc";
        string sql = @"SELECT distinct  SID,SNAME,COLLEGENAME, LRY, LSEMN, TCGPA, CGPA, DUE, phone,
                        SUM(  NVL(LOAN,0)) LOAN,
                        SUM(   FLOOR((NVL(CR,0)*NVL(CH,0) *NVL(LOAN,0) / 100)) ) LOAN_AMOUNT  
                        FROM
                        (   
                        select VWC.SID, VWC.SNAME, VWC.COLLEGENAME, VWC.LRY, VWC.LSEMN, VWC.TCGPA, VWC.CGPA, VWC.DUE, VWC.phone ,
                        (CASE WHEN S.GENDER='M' THEN CR.MALERATE ELSE CR.FEMRATE END ) CR,L.LOAN,
                        SUM(OG.CHOURS) CH
                        from VW_CONVOCATION VWC
                        left join Student S on S.SID =VWC.SID
                        LEFT JOIN LOANSANDWAIVER L ON (S.SID = L.SID)
                        LEFT JOIN STUDENTCREDIT SCR on (SCR.SID = L.SID AND SCR.year=L.YEAR and SCR.SEMESTER=L.Semester)
                        LEFT JOIN CreditRate CR ON
                        (S.SPROGRAM=CR.DEPCODE AND CR.YEAR=(2000+SUBSTR(S.SID,0,2)) AND (CR.SEMESTER=SUBSTR(S.SID,3,1)) )
                        LEFT JOIN REGISTATUS RS ON 
                        ( RS.YEAR=SCR.YEAR AND RS.SEMESTER=SCR.SEMESTER AND RS.SID =SCR.SID )
                        LEFT JOIN OFFERERINGANDGRADE OG ON (RS.REGKEY=OG.REGKEY)
                        where VWC.SID like '" + batch + "%' group by VWC.SID, VWC.SNAME, VWC.COLLEGENAME, VWC.LRY, VWC.LSEMN, VWC.TCGPA, VWC.CGPA, VWC.DUE, VWC.phone,S.GENDER,   CR.MALERATE,CR.FEMRATE,L.LOAN  order by VWC.SID asc   )    group by  SID,SNAME,COLLEGENAME, LRY, LSEMN, TCGPA, CGPA, DUE, phone  order by SID asc  ";
        
        return obj_db.get_viewData(sql, "Convo_STUDENT");
    }

    public DataTable get_RegConvo_student(string year)
    {
        string sql = @"select distinct VWCS.* , S.Sname,S.PHONE,'~/student/profile/student_images/'||VWCS.S_PICTURE PICTURE,
                    C_PROGINDEPT.NAME ,DEPT.COLLEGECODE,COLLEGE.COLLEGENAME,
                    (CASE 
                    WHEN DUAL_DEGREE=1 THEN 'YES'
                    END) Duel_status,
                    (CASE 
                    WHEN DREG_CONFIRM=1 THEN 'Clearnce'
                    END) Duel_Clearnce
                    from VW_CONVOCATION_STATUS VWCS
                    left join student S on S.SID = VWCS.SD
                    left join C_PROGINDEPT  on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID
                    left join C_DEPARTMENTINFACULTY dept on dept.DEPID = C_PROGINDEPT.DEPID 
                    left join COLLEGE on COLLEGE.COLLEGECODE=DEPT.COLLEGECODE
                    where 
                    (VWCS.STATUS='SUCCESS' or DUAL_DEGREE=1) and HEADSN  =28
                    order by  DEPT.COLLEGECODE asc , sd asc";
        //string sql = @" select VWCS.*, '~/student/profile/student_images/'||VWCS.S_PICTURE PICTURE from VW_CONVOSUCCESS VWCS where VWCS.CONVOCATION_YEAR ='" + year + "'  order by PROGRAM_ID asc, SID asc";
        return obj_db.get_viewData(sql, "EU_RegConvoStd");
    }


    public DataTable get_RegConvo_studentNew(string year)
    {
        string sql = @"select distinct VWCS.*, S.Sname ,'~/student/profile/student_images/'||VWCS.S_PICTURE PICTURE,
                    C_PROGINDEPT.NAME ,DEPT.COLLEGECODE,COLLEGE.COLLEGENAME,
                    (CASE 
                    WHEN DUAL_DEGREE=1 THEN 'YES'
                    else 'NO'
                    END) Duel_status
                    from VW_CONVOCATION_STATUS VWCS
                    left join student S on S.SID = VWCS.SD
                    left join C_PROGINDEPT  on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID
                    left join C_DEPARTMENTINFACULTY dept on dept.DEPID = C_PROGINDEPT.DEPID 
                    left join COLLEGE on COLLEGE.COLLEGECODE=DEPT.COLLEGECODE
                    left join EU_CONVOCATION on EU_CONVOCATION.SID =VWCS.SD
                    where 
                    VWCS.STATUS='SUCCESS' or EU_CONVOCATION.DREG_CONFIRM=1
                    order by  DEPT.COLLEGECODE asc , sd asc";
        //string sql = @" select VWCS.*, '~/student/profile/student_images/'||VWCS.S_PICTURE PICTURE from VW_CONVOSUCCESS VWCS where VWCS.CONVOCATION_YEAR ='" + year + "'  order by PROGRAM_ID asc, SID asc";
        return obj_db.get_viewData(sql, "EU_RegConvoStd");
    }
    public DataTable get_RegConvo_studentSearch(string degree, string degyear, string degsem, string year)
    {
        string sql = @" select VWCS.*, '~/student/profile/student_images/'||VWCS.S_PICTURE PICTURE from VW_CONVOSUCCESS VWCS where VWCS.PROGRAM_ID ='" + degree + "' and ( VWCS.GRADUATIONYEAR = '" + degyear + "' or VWCS.LRY ='" + degyear + "') and (VWCS.GRADUATIONSEMESTER ='" + degsem + "' or VWCS.LRS ='" + degsem + "') and VWCS.CONVOCATION_YEAR ='" + year + "'  order by SID asc";
        return obj_db.get_viewData(sql, "EU_RegConvoStd");
    }

    //public DataTable get_RegConvo_studentPublic()
    //{
    //    string sql = @" select VWCS.*, '~/student/profile/student_images/'||VWCS.S_PICTURE PICTURE from VW_CONVOSUCCESS VWCS order by SID asc";
    //    return obj_db.get_viewData(sql, "EU_RegConvoStd");
    //}

    public DataTable get_RegConvo_studentTotal(string year)
    {
        string sql = @"select count(SD) total from VW_CONVOCATION_STATUS  where (STATUS='SUCCESS' or DUAL_DEGREE=1) AND HEADSN  =28";
        return obj_db.get_viewData(sql, "EU_RegConvoTotal");
    }

    public DataTable get_RegConvo_studentTotalSearchwise(string degree, string degyear, string degsem, string year)
    {
        //  string sql = @"   order by SID asc";

        string sql = @" Select count(C.SID)Total from  (select VWCS.*, '~/student/profile/student_images/'||VWCS.S_PICTURE PICTURE from VW_CONVOSUCCESS VWCS where VWCS.PROGRAM_ID ='" + degree + "' and ( VWCS.GRADUATIONYEAR = '" + degyear + "' or VWCS.LRY ='" + degyear + "') and (VWCS.GRADUATIONSEMESTER ='" + degsem + "' or VWCS.LRS ='" + degsem + "') and VWCS.CONVOCATION_YEAR ='" + year + "' )C";
        return obj_db.get_viewData(sql, "EU_RegConvoTotal");
    }

   public DataTable get_all_StudentFacultyWise(string Faculty_ID, string Year)
    {
        string sql = @" select VWCS.*, '~/student/profile/student_images/'||VWCS.S_PICTURE PICTURE ,DEPT.COLLEGECODE
                        from VW_CONVOSUCCESS VWCS left join C_PROGINDEPT  on C_PROGINDEPT.C_PROGINDEPT_ID = VWCS.PROGRAM_ID
                        left join C_DEPARTMENTINFACULTY dept on dept.DEPID = C_PROGINDEPT.DEPID 
                        where DEPT.COLLEGECODE = '" + Faculty_ID + "' and VWCS.CONVOCATION_YEAR ='" + Year + "'   order by VWCS.SPROGRAM asc, VWCS.SID asc";
      
      //  string sql = " Select * from WEB_TEACHER_STAFF where JOB_CATEGORY='Teacher'and STAFF_CTRL=1 and DEPARTMENT='" + Faculty_ID + "'order by STAFF_NAME";
        return obj_db.get_viewData(sql, "EU_RegConvoStdDept");
    }


   public DataTable get_Convo_studentTotalDeptwise(string Faculty_ID, string Year)
    {
        string sql = @" Select count(DepT.SID)Total from (select C.* from  (SELECT DISTINCT sd.SID FROM studentdebit sd INNER JOIN eu_convocation ";
          sql += "   ON eu_convocation.SID = sd.SID WHERE sd.headsn = 28  AND eu_convocation.tranid IS NULL AND eu_convocation.CONVOCATION_YEAR ='" + Year + "'   UNION ";
          sql += "   SELECT tsd.SID FROM t_studentdebit tsd WHERE tsd.status = 'S' AND tsd.headsn = 28  and TSD.YEAR='" + Year + "')C    left join Student S on C.SID= S.SID left join C_PROGINDEPT  on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID  ";
          sql += "   left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID=C_PROGINDEPT.DEPID  where C_DEPARTMENTINFACULTY.COLLEGECODE = '" + Faculty_ID + "' )DepT";

        return obj_db.get_viewData(sql, "EU_RegConvoTotalDept");
    }
    //select count(VWCS.SID) Total from VW_CONVOSUCCESS VWCS
    public DataTable get_RegConvo_studentPublic()
    {
        string sql = @" Select C.SID,  S.SNAME, S.SPROGRAM, '~/student/profile/student_images/'||S.S_PICTURE Picture from 
                        (SELECT DISTINCT sd.SID FROM studentdebit sd INNER JOIN eu_convocation
                        ON eu_convocation.SID = sd.SID WHERE sd.headsn = 28 AND EU_CONVOCATION.CONVOCATION_YEAR = 2019
                        and eu_convocation.tranid IS NULL UNION
                        SELECT tsd.SID FROM t_studentdebit tsd WHERE tsd.status = 'S' AND tsd.headsn = 28 and TSD.YEAR =2019  )C
                        left join Student S on S.SID = C.SID order by S.SID asc";

        return obj_db.get_viewData(sql, "EU_RegConvoStd");
    }

    public string InsertConvoDouble(string sid)
    {
        string sql = @" update EU_CONVOCATION set DREG_CONFIRM=1 where SID='" + sid + "' ";

        string i = obj_db.execute_query(sql);
        return Convert.ToString(i);

    }

    public DataTable Check_ConvoReg(string sid)
    {
        string sql = @"select SID from EU_CONVOCATION  where sid ='" + sid + "' ";
        return obj_db.get_viewData(sql, "EU_RegConvo");
    }
    public string insert_ConStdNew(DataSet ds)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["EU_CONVO_EXTRAID"].Rows)
        {
            sql = @" insert into EU_CONVO_EXTRAID (SID, YEAR)";
            sql += " values ('" + dr["SID"] + "','" + dr["YEAR"] + "' ) ";

        }

        if (obj_db.execute_query(sql) == "1" )
        {
            count++;
        }

        return Convert.ToString(count);

    }


    public string insert_ConPermittedStdNew(DataSet ds)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["EU_COVOSTUDENTADD"].Rows)
        {
            sql = @" insert into EU_COVOSTUDENTADD (SID, YEAR,INSERTEDBY,INSERTION_TIME)";
            sql += " values ('" + dr["SID"] + "','" + dr["YEAR"] + "','" + dr["INSERTEDBY"] + "','" + dr["INSERTION_TIME"] + "' ) ";

        }

        if (obj_db.execute_query(sql) == "1")
        {
            count++;
        }

        return Convert.ToString(count);

    }
    public DataTable get_NewConvo_student(string Year)
    {
        string sql = @" select EU_CONVO_EXTRAID.*, S.SNAME  from EU_CONVO_EXTRAID left join Student S on S.SID = EU_CONVO_EXTRAID.SID where  EU_CONVO_EXTRAID.YEAR = '"+Year+ "' order by EU_CONVO_EXTRAID.SID asc";
        return obj_db.get_viewData(sql, "EU_NewConvoStd");
    }
    public DataTable get_NewConvoPermitted_student(string Year)
    {
        string sql = @" select EU_COVOSTUDENTADD.*, S.SNAME  from EU_COVOSTUDENTADD left join Student S on S.SID = EU_COVOSTUDENTADD.SID where  EU_COVOSTUDENTADD.YEAR = '" + Year + "' order by EU_COVOSTUDENTADD.SID asc";
        return obj_db.get_viewData(sql, "EU_COVOSTUDENTADD");
    }
    public DataTable get_NewConvo_studentAll()
    {
        string sql = @" select EU_CONVO_EXTRAID.*, S.SNAME  from EU_CONVO_EXTRAID left join Student S on S.SID = EU_CONVO_EXTRAID.SID  order by EU_CONVO_EXTRAID.SID asc";
        return obj_db.get_viewData(sql, "EU_NewConvoStd");
    }

    public DataTable get_Convo_DoubleConfirmation()
    {
        string sql = @"select EU_CONVOCATION.SID, SNAME, CONVOCATION_YEAR from EU_CONVOCATION
left join Student S on S.SID = EU_CONVOCATION.SID where DREG_CONFIRM=1 order by EU_CONVOCATION.SID asc ";
        return obj_db.get_viewData(sql, "EU_ConvoDobleStd");
    }

    public DataTable get_Convo_DoubleConfirmation(string year)
    {
        string sql = @"select EU_CONVOCATION.SID, SNAME, CONVOCATION_YEAR from EU_CONVOCATION left join Student S on S.SID = EU_CONVOCATION.SID where DREG_CONFIRM=1 and CONVOCATION_YEAR ='" + year + "' order by EU_CONVOCATION.SID asc ";
        return obj_db.get_viewData(sql, "EU_ConvoDobleStd");
    }
    public DataTable get_ConvoStd_VourcherChk(string Year)
    {
        string sql = @"select studentdebit.*, S.SNAME, S.PHONE from studentdebit left join Student S on S.SID =STUDENTDEBIT.SID where HEADSN = 28    AND SEMESTER = 1  and YEAR = '" + Year + "' order by created asc";
        return obj_db.get_viewData(sql, "ConvoStd_Vourcher");
    }
    public DataTable get_ConvoStd_Vourcher()
    {
        string sql = @"select studentdebit.*, S.SNAME, S.PHONE from studentdebit left join Student S on S.SID =STUDENTDEBIT.SID where HEADSN = 28  AND YEAR = 2019   AND SEMESTER = 1 order by created asc";
        return obj_db.get_viewData(sql, "ConvoStd_Vourcher");
    }
    public DataTable get_ConvoStd_VourcherNew()
    {
        string sql = @"select studentdebit.*, S.SNAME, S.PHONE from studentdebit left join Student S on S.SID =STUDENTDEBIT.SID where HEADSN = 28  AND YEAR = 2019   AND SEMESTER = 1 order by created asc";
        return obj_db.get_viewData(sql, "ConvoStd_Vourcher");
    }

    public DataTable match_STUDENTDEBIT(string Year, string Semister, string sid, string HEADSN)
    {
        string sql = " SELECT * FROM STUDENTDEBIT WHERE YEAR ='" + Year + "' and SID='" + sid + "'  and SEMESTER ='" + Semister + "' and HEADSN ='" + HEADSN + "' ";
        return obj_db.get_viewData(sql, "STUDENTDEBIT");
    }

    public DataTable match_STUDENTDEBITN(string Year, string Semister, string sid, string HEADSN)
    {
        string sql = "select STD.SID, STD.YEAR, STD.SEMESTER, STD.HEADSN, ";
        sql += " sum(STD.AMOUNT) AMOUNT, CASE WHEN s.graduationyear > 0 THEN 'Graduate' WHEN s.graduationyear <= 0 THEN 'Probable' END AS status ";
        sql += " from studentdebit STD left join Student S on S.SID = STD.SID WHERE STD.YEAR ='" + Year + "' and STD.SID='" + sid + "'  and STD.SEMESTER ='" + Semister + "' and STD.HEADSN ='" + HEADSN + "' ";
        sql += " group by STD.SID, STD.YEAR, STD.SEMESTER, STD.HEADSN,s.graduationyear ";
        return obj_db.get_viewData(sql, "STUDENTDEBIT_NEW");
    }

    public string UpStd_Convocation(string Vourcher, string sid)
    {
        string sql = @" update EU_CONVOCATION set VOUCARNO ='" + Vourcher + "', TRANID ='' where SID='" + sid + "' ";

        string i = obj_db.execute_query(sql);
        return Convert.ToString(i);

    }








    public DataTable get_Convo_studentTotalFacultyDeptwise(string Faculty_ID, String Dept)
    {
        string sql = @" Select count(DepT.SID)Total from (select C.* from
                        (SELECT DISTINCT sd.SID FROM studentdebit sd INNER JOIN eu_convocation
                        ON eu_convocation.SID = sd.SID WHERE sd.headsn = 28 
                        AND eu_convocation.tranid IS NULL UNION
                        SELECT tsd.SID FROM t_studentdebit tsd WHERE tsd.status = 'S' AND tsd.headsn = 28 )C
                        left join Student S on C.SID= S.SID
                        left join DepartmentinCollege dept on DEPT.DEPCODE =S.SPROGRAM
                        where DEPT.COLLEGECODE = '" + Faculty_ID + "' and dept.DEPCODE = '" + Dept + "' )DepT";

        return obj_db.get_viewData(sql, "EU_RegConvoTotalDept");
    }

    public DataTable get_RegFacultyDept_student(string Faculty_ID, String Dept)
    {
        string sql = @" Select C.SID,  S.SNAME, S.SPROGRAM, '~/student/profile/student_images/'||S.S_PICTURE Picture from 
                    (SELECT DISTINCT sd.SID FROM studentdebit sd INNER JOIN eu_convocation
                    ON eu_convocation.SID = sd.SID WHERE sd.headsn = 28 AND eu_convocation.tranid IS NULL UNION
                    SELECT tsd.SID FROM t_studentdebit tsd WHERE tsd.status = 'S' AND tsd.headsn = 28 )C
                    left join Student S on S.SID = C.SID left join DepartmentinCollege dept on DEPT.DEPCODE = S.SPROGRAM
                    where DEPT.COLLEGECODE= '" + Faculty_ID + "' and dept.DEPCODE = '" + Dept + "'  order by S.SID asc";

        //  string sql = " Select * from WEB_TEACHER_STAFF where JOB_CATEGORY='Teacher'and STAFF_CTRL=1 and DEPARTMENT='" + Faculty_ID + "'order by STAFF_NAME";
        return obj_db.get_viewData(sql, "EU_RegFacultyDeptStd");
    }

    public DataTable get_RegFaculty_student(string Faculty_ID)
    {
//        string sql = @" select VWCS.*, '~/student/profile/student_images/'||VWCS.S_PICTURE PICTURE ,DEPT.COLLEGECODE
//                        from VW_CONVOSUCCESS VWCS left join DepartmentinCollege dept on DEPT.DEPCODE =VWCS.SPROGRAM
//                        where DEPT.COLLEGECODE = '" + Faculty_ID + "' order by VWCS.SID asc";

      string sql = @" Select C.SID,  S.SNAME, S.SPROGRAM, '~/student/profile/student_images/'||S.S_PICTURE Picture from 
                    (SELECT DISTINCT sd.SID FROM studentdebit sd INNER JOIN eu_convocation
                    ON eu_convocation.SID = sd.SID WHERE sd.headsn = 28 AND eu_convocation.tranid IS NULL UNION
                    SELECT tsd.SID FROM t_studentdebit tsd WHERE tsd.status = 'S' AND tsd.headsn = 28 )C
                    left join Student S on S.SID = C.SID  left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID
                    left join C_DEPARTMENTINFACULTY dept on dept.DEPID =  C_PROGINDEPT.DEPID
                    where DEPT.COLLEGECODE= '" + Faculty_ID + "' order by S.SID asc ";

        return obj_db.get_viewData(sql, "EU_RegFacultyStd");
    }

    public DataTable get_all_DeptFaculty(string Faculty_ID)
    {
        string sql = " Select * from DepartmentInCollege where CollegeCode = '" + Faculty_ID + "' order by depcode ";
        return obj_db.get_viewData(sql, "DepartmentList");
    }

    #endregion






    public bool is_valid_student_Result(string sid, string year, string semester)
    {
        bool isValid = false;
        DataSet ds = new DataSet();

        string sql = " select * from REGISTATUS where sid ='" + sid + "' and year ='" + year + "' and semester ='" + semester + "'and RESULTVIEW_STATUS=1 ";
        ds.Merge(obj_db.get_viewData(sql, "CRS_OFFERING_VALID_STU"));

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables["CRS_OFFERING_VALID_STU"].Rows.Count > 0)
                isValid = true;
        }

        return isValid;
    }






    public bool is_valid_student(string sid, string year, string semester)
    {
        bool isValid = false;
        DataSet ds = new DataSet();

        string sql = " select * from CRS_OFFERING_VALID_STU where sid ='" + sid + "' and year ='" + year + "' and semester ='" + semester + "' ";
        ds.Merge(obj_db.get_viewData(sql, "CRS_OFFERING_VALID_STU"));

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables["CRS_OFFERING_VALID_STU"].Rows.Count > 0)
                isValid = true;
        }

        return isValid;
    }

    public bool is_dropout_student(string sid)
    {
        bool isDroupOut = false;
        DataSet ds = new DataSet();

        string sql = " select * from studentleave where sid ='" + sid + "' and slflag=0 ";
        ds.Merge(obj_db.get_viewData(sql, "REGISTATUS"));

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables["REGISTATUS"].Rows.Count > 0)
                isDroupOut= true;
        }
        return isDroupOut;
    }

    public DataTable get_prerequisite_done(string sid, string courseCode)
    {
        string sql = " Select * from OFFERERINGANDGRADE where COURSEKEY like '%" + courseCode + "' ";
        sql += " and REGKEY like '" + sid + "%'  and GGRADE2<>'F' and GGRADE2<>'W' ";
        return obj_db.get_viewData(sql, "preRequisit");
    }
    
    public DataTable get_prerequisite_from_preOffered(string sid, string courseCode)
    {
        string sql = " Select * from WEB_COURSE_OFFERING_TEMP where COURSEKEY like '%" + courseCode + "' ";
        sql = " and REGKEY like '" + sid + "%'  and CTRL=1 ";
        return obj_db.get_viewData(sql, "per_preRequisit");
    }

    public DataTable get_all_preOffered_courses(string sid, string sem, string year)
    {
        string sql = " Select * from WEB_COURSE_OFFERING_TEMP where REGKEY='" +sid+ sem+year+ "' ";        
        return obj_db.get_viewData(sql, "pree_offered");
    }

    public DataTable rpt_Student_Persentance()
    {
        string sql = " SELECT * from RPT_STUDENT_CLSATTENDANCE  where COURSE_TEACHER_ID like 'C_T_0000024403'  ";
        return obj_db.get_viewData(sql, "StudentPersentance");
    }

    //-----------------------------------------change items
    public DataTable get_alreadyTakenCourses(String courseKey, String sid, String sem, String year, string GGROUP)
    {
        string sql = "select * from OFFERERINGANDGRADE where COURSEKEY = '" + sem + year + courseKey + "' and REGKEY = '" + sid + sem + year + "'  and GGROUP='" + GGROUP + "'  ";

        return obj_db.get_viewData(sql, "AlreadyTakenCourses");
    }

    public DataTable get_all_preOffered_coursesFull(string sid, string sem, string year)
    {
        //string sql = " SELECT distinct t.*, SCH_CLS_1, SCH_CLS_2,SCH_CLS_3, TUT_CLS_1, TUT_CLS_2,TUT_CLS_3,STAFF_NAME ,substr(COURSEKEY, -7) as course,";
        //  sql += "case when  substr (REGKEY, -5) > 12015 then (substr (WEB_COURSE_TEACHER.SCH_CLS_1 ,1,7) || ' '|| substr (WEB_COURSE_TEACHER.SCH_CLS_1 ,-27, 23) ) else SCH_CLS_1 end as Cls_1,";
        //  sql += "case when  substr (REGKEY, -5) > 12015 then (substr (WEB_COURSE_TEACHER.SCH_CLS_2 ,1,7) || ' '|| substr (WEB_COURSE_TEACHER.SCH_CLS_2 ,-27, 23) ) else SCH_CLS_1 end as Cls_2,";
        //  sql += "case when  substr (REGKEY, -5) > 12015 then substr (WEB_COURSE_TEACHER.SCH_CLS_1 ,-4) else ' ' end as room1,";
        //  sql += "case when  substr (REGKEY, -5) > 12015 then substr (WEB_COURSE_TEACHER.SCH_CLS_2 ,-4) else ' ' end as room2,";
        //  sql += "case when  substr (COURSEKEY, -3) > 250 then substr (COURSEKEY ,-3) else ' ' end as ck";
        //  sql += " FROM WEB_COURSE_OFFERING_TEMP t,  WEB_COURSE_TEACHER,WEB_TEACHER_STAFF ";
        //  sql += " WHERE REGKEY='" + sid + sem + year + "' ";
        //  sql += " AND (t.COURSEKEY =WEB_COURSE_TEACHER.COURSE_KEY ";
        //  sql += " AND WEB_COURSE_TEACHER.TEACHER_ID= WEB_TEACHER_STAFF.STAFF_ID AND WEB_COURSE_TEACHER.SECTION=t.GGROUP)";




        string sql = "select distinct Taken.*,WVC.total_student,WVC.TOTAL_CAPACITY, offering.offered  from  ( SELECT distinct t.*, SCH_CLS_1, SCH_CLS_2,SCH_CLS_3, TUT_CLS_1, TUT_CLS_2,TUT_CLS_3,CHANGEDCOURSENAME.CNAME, STAFF_NAME ,";
        sql += "substr(t.COURSEKEY, -7) as course,case when  substr (REGKEY, -5) > 12015 ";
        sql += "then (substr (WEB_COURSE_TEACHER.SCH_CLS_1 ,1,7) || ' '|| substr (WEB_COURSE_TEACHER.SCH_CLS_1 ,-27, 23) ) ";
        sql += "else SCH_CLS_1 end as Cls_1,case when  substr (REGKEY, -5) > 12015 then (substr (WEB_COURSE_TEACHER.SCH_CLS_2 ,1,7) || ' '|| substr (WEB_COURSE_TEACHER.SCH_CLS_2 ,-27, 23) ) ";
        sql += "else SCH_CLS_1 end as Cls_2,case when  substr (REGKEY, -5) > 12015 then substr (WEB_COURSE_TEACHER.SCH_CLS_1 ,-4) ";
        sql += "else ' ' end as room1,case when  substr (REGKEY, -5) > 12015 then substr (WEB_COURSE_TEACHER.SCH_CLS_2 ,-4) else ' ' end as room2,case when  substr (t.COURSEKEY, -3) > 250 then substr (t.COURSEKEY ,-3) else ' ' end as ck ";
        sql += "FROM WEB_COURSE_OFFERING_TEMP t,  WEB_COURSE_TEACHER,WEB_TEACHER_STAFF ,CHANGEDCOURSENAME,OFFEREDCOURSE ";
        sql += "WHERE REGKEY='" + sid + sem + year + "'  AND (OFFEREDCOURSE.COURSEKEY = t.COURSEKEY and ";
        sql += "CHANGEDCOURSENAME.COURSECODE =OFFEREDCOURSE.COURSECODE and ";
        sql += "t.COURSEKEY =WEB_COURSE_TEACHER.COURSE_KEY  AND ";
        sql += "WEB_COURSE_TEACHER.TEACHER_ID= WEB_TEACHER_STAFF.STAFF_ID AND WEB_COURSE_TEACHER.SECTION=t.GGROUP)";
        sql += ") Taken left join WEB_VIEW_COURSE_TEACHER WVC on (WVC.COURSE_KEY  =Taken.COURSEKEY and Taken.GGROUP = WVC.SECTION)";
        sql += " left join (select count(regkey) offered, COURSEKEY, GGROUP  from WEB_COURSE_OFFERING_TEMP group by COURSEKEY, GGROUP)offering  on (OFFERING.COURSEKEY = Taken.COURSEKEY and Taken.GGROUP = OFFERING.GGROUP)";


        //        SELECT t.*, SCH_CLS_1, 



        //SCH_CLS_2,
        //-- substr (WEB_COURSE_TEACHER.SCH_CLS_2 ,-4) as room2 ,

        //TUT_CLS_1, TUT_CLS_2,STAFF_NAME ,
        //case substr (REGKEY, 1, 5)
        //when   '12015' 
        //then substr (WEB_COURSE_TEACHER.SCH_CLS_1 ,-4) 
        //else ' '


        //end as room1,


        //case substr (REGKEY, 1, 5)
        //when   '12015' 
        //then substr (WEB_COURSE_TEACHER.SCH_CLS_2 ,-4) 
        //else ' '


        //end as room2
        //       FROM WEB_COURSE_OFFERING_TEMP t,  WEB_COURSE_TEACHER,WEB_TEACHER_STAFF 
        //         WHERE REGKEY='09380010812014' 
        //        AND (t.COURSEKEY = WEB_COURSE_TEACHER.COURSE_KEY 
        //         AND WEB_COURSE_TEACHER.TEACHER_ID= WEB_TEACHER_STAFF.STAFF_ID AND WEB_COURSE_TEACHER.SECTION=t.GGROUP)



        return obj_db.get_viewData(sql, "pree_offered");
    }

    public DataTable get_all_FinalAdvising_approvedCourse(string sid, string sem, string year)
    {


        string sql = "select distinct  SUBSTR(Course.coursekey,6,8) as Approvedcourse from ";
        sql += " (SELECT distinct t.*, SCH_CLS_1, SCH_CLS_2,SCH_CLS_3,STAFF_NAME , ";
        sql += "CHANGEDCOURSENAME.CNAME ,CHANGEDCOURSENAME.COURSECODE as course ";
        sql += "   FROM OFFERERINGANDGRADE t,  WEB_COURSE_TEACHER,WEB_TEACHER_STAFF  ,OFFEREDCOURSE,CHANGEDCOURSENAME ";
        sql += " WHERE REGKEY='" + sid + sem + year + "' AND t.COURSEKEY LIKE'" + sem + year + "%' ";
        sql += "   AND (t.COURSEKEY =WEB_COURSE_TEACHER.COURSE_KEY  ";
        sql += "  AND OFFEREDCOURSE.COURSEKEY = t.COURSEKEY and CHANGEDCOURSENAME.COURSECODE=OFFEREDCOURSE.COURSECODE ";
        sql += " and WEB_COURSE_TEACHER.TEACHER_ID= WEB_TEACHER_STAFF.STAFF_ID AND WEB_COURSE_TEACHER.SECTION=t.GGROUP) )Course";


        return obj_db.get_viewData(sql, "approvedCourse");
    }

    public DataTable get_all_FinalAdvising_coursesFull(string sid, string sem, string year)
    {


        string sql = "    SELECT distinct t.*, SCH_CLS_1, SCH_CLS_2,SCH_CLS_3,STAFF_NAME , ";
        sql += "CHANGEDCOURSENAME.CNAME ,CHANGEDCOURSENAME.COURSECODE as course ";
        sql += "   FROM OFFERERINGANDGRADE t,  WEB_COURSE_TEACHER,WEB_TEACHER_STAFF  ,OFFEREDCOURSE,CHANGEDCOURSENAME ";
        sql += " WHERE REGKEY='" + sid + sem + year + "' AND t.COURSEKEY LIKE'" + sem + year + "%' ";
        sql += "   AND (t.COURSEKEY =WEB_COURSE_TEACHER.COURSE_KEY  ";
        sql += "  AND OFFEREDCOURSE.COURSEKEY = t.COURSEKEY and CHANGEDCOURSENAME.COURSECODE=OFFEREDCOURSE.COURSECODE ";
        sql += " and WEB_COURSE_TEACHER.TEACHER_ID= WEB_TEACHER_STAFF.STAFF_ID AND WEB_COURSE_TEACHER.SECTION=t.GGROUP) ";


        //string sql = "  SELECT distinct t.*, SCH_CLS_1, SCH_CLS_2,SCH_CLS_3, TUT_CLS_1, TUT_CLS_2,TUT_CLS_3,CHANGEDCOURSENAME.CNAME, STAFF_NAME ,";
        //sql += "substr(t.COURSEKEY, -7) as course,case when  substr (REGKEY, -5) > 12015 ";
        //sql += "then (substr (WEB_COURSE_TEACHER.SCH_CLS_1 ,1,7) || ' '|| substr (WEB_COURSE_TEACHER.SCH_CLS_1 ,-27, 23) ) ";
        //sql += "else SCH_CLS_1 end as Cls_1,case when  substr (REGKEY, -5) > 12015 then (substr (WEB_COURSE_TEACHER.SCH_CLS_2 ,1,7) || ' '|| substr (WEB_COURSE_TEACHER.SCH_CLS_2 ,-27, 23) ) ";
        //sql += "else SCH_CLS_1 end as Cls_2,case when  substr (REGKEY, -5) > 12015 then substr (WEB_COURSE_TEACHER.SCH_CLS_1 ,-4) ";
        //sql += "else ' ' end as room1,case when  substr (REGKEY, -5) > 12015 then substr (WEB_COURSE_TEACHER.SCH_CLS_2 ,-4) else ' ' end as room2,case when  substr (t.COURSEKEY, -3) > 250 then substr (t.COURSEKEY ,-3) else ' ' end as ck ";
        //sql += "FROM WEB_COURSE_OFFERING_TEMP t,  WEB_COURSE_TEACHER,WEB_TEACHER_STAFF ,CHANGEDCOURSENAME,OFFEREDCOURSE ";
        //sql += "WHERE REGKEY='" + sid + sem + year + "'  AND (OFFEREDCOURSE.COURSEKEY = t.COURSEKEY and ";
        //sql += "CHANGEDCOURSENAME.COURSECODE =OFFEREDCOURSE.COURSECODE and ";
        //sql += "t.COURSEKEY =WEB_COURSE_TEACHER.COURSE_KEY  AND ";
        //sql += "WEB_COURSE_TEACHER.TEACHER_ID= WEB_TEACHER_STAFF.STAFF_ID AND WEB_COURSE_TEACHER.SECTION=t.GGROUP)";


        return obj_db.get_viewData(sql, "pree_offered");
    }

    //-------------------------------------------------------

    //public DataTable get_available_course_forOffering(string sem, string year, string depCode)
    //{
    //    string sql = " SELECT  o.*, cd.*,CNAME FROM OFFEREDCOURSE o, COURSEINDEPARTMENT cd, CHANGEDCOURSENAME  ";
    //    sql += " WHERE CHANGEDCOURSENAME.COURSECODE=cd.COURSECODE AND o.COURSECODE=cd.COURSECODE AND COURSEKEY LIKE'" + sem + year + "%' AND DEPCODE='" + depCode + "' ORDER BY cd.COURSECODE ASC ";
    //    return obj_db.get_viewData(sql, "courses");
    //}
	
	public DataTable get_available_course_forOffering(string sem, string year, string depCode)
    {
        string sql = " SELECT  o.*, cd.*,CNAME FROM OFFEREDCOURSE o, COURSEINDEPARTMENT cd, CHANGEDCOURSENAME  ";
        sql += " WHERE CHANGEDCOURSENAME.COURSECODE=cd.COURSECODE AND o.COURSECODE=cd.COURSECODE AND o.COURSEKEY LIKE'" + sem + year 
            + "%' AND cd.DEPCODE='" + depCode + "' ORDER BY cd.COURSECODE ASC ";
        return obj_db.get_viewData(sql, "courses");
    }

    public DataTable Check_Registration_Student(string sid, string sem, string year)
    {
        string sql = @"select * from registatus   where SID = '" + sid + "' and YEAR ='" + year + "' and Semester ='" + sem + "' ";

        return obj_db.get_viewData(sql, "Registatus");
    }
    public DataTable Check_RuuningYearSemester(string sid)
    {
        string sql = @"select max(ADMINYEAR||ADMINSEMETER) running    from student
                    left join REGISTATUS on registatus.sid = STUDENT.SID
                    where student.sid ='" + sid + "' having max(ADMINYEAR||ADMINSEMETER) = max(REGISTATUS.YEAR||REGISTATUS.SEMESTER) ";

        return obj_db.get_viewData(sql, "Ruuning");
    }

    public DataTable Check_RuuningYearSemesterDepwise(string DEPCODE)
    {
        string sql = @"select max(ADMINYEAR||ADMINSEMETER) running    from student
                        left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = STUDENT.PROGRAM_ID
                        left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID=C_PROGINDEPT.DEPID
                        where C_DEPARTMENTINFACULTY.COLLEGECODE ='" + DEPCODE + "' ";

        return obj_db.get_viewData(sql, "Ruuning");
    }
    public DataTable Check_MinRegistration_Student(string sid, string sem, string year)
    {
        string sql = @" select sid, MIN(YEAR||SEMESTER)  from registatus  where SID = '" + sid + "'  group by SID   having   MIN(YEAR||SEMESTER) = '" + year + sem + "' ";

        return obj_db.get_viewData(sql, "MaxRegistatus");
    }

    public DataTable get_all_Offered_coursesFull(string sid, string sem, string year)
    {
        string sql = @"select distinct  OC.REGKEY,OC.COURSEKEY, substr(OC.COURSEKEY, -7) Course,OC.CHOURS, CNG.CNAME, OC.GGROUP, WT.SCH_CLS_1, WT.SCH_CLS_2,WT.SCH_CLS_3,
                        case when  substr (OC.REGKEY, -5) > 12015  then TRIM(substr (WT.SCH_CLS_1 ,1,10) ) else SCH_CLS_1 end as Cls_1,
                        case when  substr (OC.REGKEY, -5) > 12015 then TRIM(substr (WT.SCH_CLS_2 ,1,10)) else SCH_CLS_1 end as Cls_2,
                        case when  substr (OC.REGKEY, -5) > 12015 then substr (WT.SCH_CLS_1 ,-6) else ' ' end as room1,
                        case when  substr (OC.REGKEY, -5) > 12015 then substr (WT.SCH_CLS_2 ,-6) else ' ' end as room2,
                        case when  substr (t.COURSEKEY, -3) > 250 then substr (t.COURSEKEY ,-3) else ' ' end as ck,TAKENSTD.TAKEN TOTAL_STUDENT, WT.TOTAL_CAPACITY,
                          WST.STAFF_ID, WST.STAFF_NAME, TAKENSTD.TAKEN||'/'||WT.TOTAL_CAPACITY enrolled, OC.COURSE_INSERTIONDATE APPROVAL_TIME, WT.TUT_CLS_1, WT.TUT_CLS_2,WT.TUT_CLS_3
                            from OFFERERINGANDGRADE OC   left join    (    select count(Regkey) Taken, COURSEKEY,GGROUP  from OFFERERINGANDGRADE    group by COURSEKEY,GGROUP    )TakenStd on TAKENSTD.COURSEKEY=OC.COURSEKEY and TakenStd.GGROUP = OC.GGROUP
                              left join WEB_COURSE_TEACHER WT on WT.COURSE_KEY = OC.COURSEKEY and WT.SECTION = OC.GGROUP     left join WEB_COURSE_OFFERING_TEMP t on t.COURSEKEY = OC.COURSEKEY and T.GGROUP = OC.GGROUP
                              left join OFFEREDCOURSE on OFFEREDCOURSE.COURSEKEY=OC.COURSEKEY     left join CHANGEDCOURSENAME CNG on CNG.COURSECODE =OFFEREDCOURSE.COURSECODE
                              left join WEB_TEACHER_STAFF WST on WST.STAFF_ID=WT.TEACHER_ID     where OC.REGKEY='" + sid + sem + year + "' order by CNG.CNAME asc ";

        return obj_db.get_viewData(sql, "OfferedCourses");
    }

    public DataTable get_available_course_forOfferingByDep(string sid, string sem, string year, string depCode)
    {
        string sql = " SELECT  o.*, CNAME FROM OFFEREDCOURSE o, CHANGEDCOURSENAME  ";
        sql += " WHERE CHANGEDCOURSENAME.COURSECODE=o.COURSECODE AND o.YEAR=" + year
            + " AND o.SEMESTER=" + sem
            + " AND o.DEPCODE='" + depCode
            + "' AND o.DEP_SEMESTER <= (select count(regkey) current_sem from REGISTATUS where sid = '" + sid
                + "' and (year||semester) not in (select LEAVEYEAR||LEAVESEM from STUDENTLEAVE where sid='" + sid + "' and slflag = 0))"
            + " ORDER BY o.SEMESTER, o.COURSECODE ";
        return obj_db.get_viewData(sql, "courses");
    }

    public string delete_assignment_student(string sid, string COURSE_MAT_ID)
    {
        return obj_db.execute_query(" Delete from WEB_COURSE_MATERIALS_STUDENT where COURSE_MAT_ID='" + COURSE_MAT_ID + "' and sid='" + sid + "' ");
    }

    public DataTable get_a_student_assignmentInfo(string ass_id, string sid)
    {
        string sql = @" Select * from WEB_COURSE_MATERIALS_STUDENT where COURSE_MAT_ID='" + ass_id + "' and SID='" + sid + "' ";
        return obj_db.get_viewData(sql, "s_courseMaterial");
    }

    public DataTable get_advisor_Info( string sid)
    {
        string sql = @" SELECT sid, advisor_id, STAFF_NAME, P_ADDRESS, PHONE_NUMBER, MOBILE, E_MAIL, DEPARTMENT, JOB_DESIGNATION,  STAFF_PICTURE,COLLEGENAME ";
        sql += " FROM STUDENT, WEB_TEACHER_STAFF,COLLEGE  ";
        sql += " WHERE sid='" + sid + "' ";
        sql += " AND ((STUDENT.ADVISOR_ID=WEB_TEACHER_STAFF.STAFF_ID)AND (WEB_TEACHER_STAFF.DEPARTMENT=COLLEGE.COLLEGECODE)) ";
 
        return obj_db.get_viewData(sql, "advisor_info");
    }

    public DataTable get_filterd_students(string department, string active, string graduate, string batch)
    {
        int flag = 0;
        string sql = @" Select * FROM STUDENT ";

        if (department != "Select")
        {
            sql += " where SPROGRAM='" + department + "'";
            flag++;
        }

        if (!string.IsNullOrEmpty(graduate.Trim()))
        {
            if (graduate == "1") // graduate
            {
                if (flag == 0)
                    sql += " where GRADUATIONYEAR <>'0' ";
                else
                    sql += " and GRADUATIONYEAR <>'0' ";
            }
            else // not graduate
            {                
                if (flag == 0)
                    sql += " where GRADUATIONYEAR='0'";
                else
                    sql += " and GRADUATIONYEAR ='0'";  
            }

        }

        if (!string.IsNullOrEmpty(active.Trim()))
        {
            if (active== "1") // active
            {
                if (flag == 0)
                    sql += " where S_CTRL=1";
                else
                    sql += " and S_CTRL=1 ";
            }
            else // Inactive
            {
                if (flag == 0)
                    sql += " where S_CTRL=0 ";
                else
                    sql += " and S_CTRL=0 ";
            }

        }

        if (!string.IsNullOrEmpty(batch.Trim()))
        {
            if (flag == 0)
                sql += " where SID like '" + batch + "%'  ";
            else
                sql += " and SID like '" + batch + "%' ";
        }


        sql += " order by sid asc ";

        return obj_db.get_viewData(sql, "student");
    }



    public DataTable get_find_students(string department, string name_id)
    {
        int flag = 0;
        string sql = @" Select * FROM STUDENT ";

        if (department != "Select")
        {
            sql += " where SPROGRAM='" + department + "'";
            flag++;
        }

        if (!string.IsNullOrEmpty(name_id.Trim()))
        {
            if(flag==0)
                sql += " where (SID like '%" + name_id + "%' OR SNAME like '%" + name_id + "%') ";
            else
                sql += " and (SID like '%" + name_id + "%' OR SNAME like '%" + name_id + "%')";
        }

        return obj_db.get_viewData(sql, "student");
    }

    public string update_atudent_active_inactive(string sid, string ctrl)
    {
        string sql = @" update student set S_CTRL='" + ctrl + "' where sid='" + sid + "' ";
        return obj_db.execute_query("" + sql);
    }




    public string upload_student_picture(string picName)
    {
        string sql = @" update student set S_PICTURE='" + picName + "' where sid='" + Session["ctrlId"].ToString() + "' ";
        return obj_db.execute_query("" + sql);
    }

    public string get_student_picture()
    {
        string picName="";
        DataSet ds = new DataSet();
        string sql = @" select * from  student where sid='" + Session["ctrlId"].ToString() + "' ";
        ds.Merge( obj_db.get_viewData(sql,"student"));

        if (ds.Tables["student"].Rows.Count > 0)
        {
            if (!String.IsNullOrEmpty(ds.Tables["student"].Rows[0]["S_PICTURE"].ToString()))
                picName = ds.Tables["student"].Rows[0]["S_PICTURE"].ToString();
        }
        return picName;
    }

    public DataTable get_semester_GradeSheet(String regiKey)
    {
        string sql = @" SELECT o.*,cName,OFFEREDCOURSE.COURSECODE FROM OFFERERINGANDGRADE o,CHANGEDCOURSENAME,OFFEREDCOURSE ";
               sql += " WHERE o.COURSEKEY=OFFEREDCOURSE.COURSEKEY  AND OFFEREDCOURSE.COURSECODE=CHANGEDCOURSENAME.COURSECODE ";
               sql += " AND  REGKEY='" + regiKey + "' order by cName asc ";

       return obj_db.get_viewData(sql, "gradeSheet");       
    }
    
    public DataTable get_semester_GradeSheetNew(String regiKey)
    {
        string sql = @" SELECT o.*,cName,OFFEREDCOURSE.COURSECODE FROM OFFERERINGANDGRADE o,CHANGEDCOURSENAME,OFFEREDCOURSE ";
        sql += " WHERE o.COURSEKEY=OFFEREDCOURSE.COURSEKEY  AND OFFEREDCOURSE.COURSECODE=CHANGEDCOURSENAME.COURSECODE ";
        sql += " AND  REGKEY='" + regiKey + "' order by OFFEREDCOURSE.COURSECODE asc ";

        return obj_db.get_viewData(sql, "gradeSheet");
    }
    public DataTable get_semester_takenCourse_Evalute(String regiKey)
    {
        //string sql = @"   SELECT M.*,M.COURSENAME as cName,C.COURSE_KEY,DECODE(C.COURSE_KEY,NULL,'Not done','done') Evaluation_status  ";
        //sql += " FROM (SELECT * FROM  OFFERERINGANDGRADE O  JOIN COURSEDETAILS CD ON (SUBSTR(O.COURSEKEY,6,LENGTH(O.COURSEKEY)) = CD.COURSECODE) ";
        //sql += " WHERE O.REGKEY='" + regiKey + "') M LEFT JOIN (SELECT * FROM WEB_TEACHER_EVAL_VALUE WTV ";
        //sql += " JOIN WEB_COURSE_TEACHER WCT ON (WTV.COURSE_TEACHER = WCT.COURSE_TEACHER_ID) ";
        //sql += " WHERE WTV.REGKEY = '" + regiKey + "' ) C ON (M.REGKEY=C.REGKEY AND M.COURSEKEY=C.COURSE_KEY) ";


        string sql = @"   SELECT M.*, C.COURSE_KEY,DECODE(C.COURSE_KEY,NULL,'Not done','done') Evaluation_status    ";
        sql += " FROM  ";
        sql += " (SELECT distinct O.*,cName,OFFEREDCOURSE.COURSECODE FROM   ";
        sql += " OFFERERINGANDGRADE O   ";
        sql += "  JOIN COURSEDETAILS CD ON (SUBSTR(O.COURSEKEY,6,LENGTH(O.COURSEKEY)) = CD.COURSECODE)   ";
        sql += "  inner join OFFEREDCOURSE on O.COURSEKEY=OFFEREDCOURSE.COURSEKEY    ";
        sql += "  inner join CHANGEDCOURSENAME on OFFEREDCOURSE.COURSECODE=CHANGEDCOURSENAME.COURSECODE  ";
        sql += "  WHERE O.REGKEY='" + regiKey + "') M  ";
        sql += " LEFT JOIN  ";
        sql += " (SELECT *  ";
        sql += " FROM WEB_TEACHER_EVAL_VALUE WTV   ";
        sql += " JOIN WEB_COURSE_TEACHER WCT ON (WTV.COURSE_TEACHER = WCT.COURSE_TEACHER_ID)  ";
        sql += " WHERE WTV.REGKEY = '" + regiKey + "'  ";
        sql += " ) C  ";
        sql += " ON (M.REGKEY=C.REGKEY AND M.COURSEKEY=C.COURSE_KEY)  ";

        return obj_db.get_viewData(sql, "gradeSheet");
    } 

    public DataTable get_AdmitCard_takenCourse(String regiKey)
    {
        string sql = @"   SELECT S.SID,S.SNAME,S.S_PICTURE, S.SPROGRAM,CL.COLLEGENAME, M.*, C.COURSE_KEY,DECODE(C.COURSE_KEY,NULL,'Not done','done') Evaluation_status   ";
        sql += "  FROM (SELECT distinct O.*,cName,OFFEREDCOURSE.COURSECODE FROM OFFERERINGANDGRADE O   ";
        sql += "  JOIN COURSEDETAILS CD ON (SUBSTR(O.COURSEKEY,6,LENGTH(O.COURSEKEY)) = CD.COURSECODE)   ";
        sql += "   inner join OFFEREDCOURSE on O.COURSEKEY=OFFEREDCOURSE.COURSEKEY    ";
        sql += "   inner join CHANGEDCOURSENAME on OFFEREDCOURSE.COURSECODE=CHANGEDCOURSENAME.COURSECODE   ";
        sql += "   WHERE O.REGKEY='" + regiKey + "') M  LEFT JOIN (SELECT * FROM WEB_TEACHER_EVAL_VALUE WTV   ";
        sql += "   JOIN WEB_COURSE_TEACHER WCT ON (WTV.COURSE_TEACHER = WCT.COURSE_TEACHER_ID)  ";
        sql += "   WHERE WTV.REGKEY = '" + regiKey + "' ) C  ON (M.REGKEY=C.REGKEY AND M.COURSEKEY=C.COURSE_KEY)  ";
        sql += "   left join student S on SUBSTR(M.REGKEY,0,9)= S.SID  ";
        sql += "   left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID left join C_DEPARTMENTINFACULTY DP on DP.DEPID =  C_PROGINDEPT.DEPID  ";
        sql += "   left join College CL on CL.COLLEGECODE = DP.COLLEGECODE  order by COURSECODE asc";

        return obj_db.get_viewData(sql, "CourseList");
    }

    public bool get_AdmitCard_dateRange(string sem, string year)
    {
        bool status = false;
        DataSet ds = new DataSet();
       // string sql = @" Select * from WEB_PRE_OFFERING_DATE where SEMESTER ='" + sem + "' and YEAR='" + year + "' and ('" + new cls_tools().get_database_formateDate(DateTime.Today) + "'>= Admit_OPENING and '" + new cls_tools().get_database_formateDate(DateTime.Today) + "'<= Admit_CLOSING )  ";

        string sql = @" Select * from WEB_PRE_OFFERING_DATE where SEMESTER ='" + sem + "' and YEAR='" + year + "' and TO_DATE('" + new cls_tools().get_database_formateDate(DateTime.Today) + "', 'dd/mm/yyyy hh24:mi:ss') >= Admit_OPENING and TO_DATE('" + new cls_tools().get_database_formateDate(DateTime.Today) + "', 'dd/mm/yyyy hh24:mi:ss') <= Admit_CLOSING   ";
      
        //DateTime.Today
        
        ds.Merge(obj_db.get_viewData(sql, "WEB_PRE_OFFERING_DATE"));

        foreach (DataRow dr in ds.Tables["WEB_PRE_OFFERING_DATE"].Rows)
        {
            if (!String.IsNullOrEmpty(dr["Admit_CLOSING"].ToString()))
            {
                status = true;

            }
        }
        return status;
    }

    public bool get_MID_AdmitCard_dateRange(string sem, string year)
    {
        bool status = false;
        DataSet ds = new DataSet();

        string sql = @" Select * from WEB_PRE_OFFERING_DATE where SEMESTER ='" + sem + "' and YEAR='" + year + "' and TO_DATE('" + new cls_tools().get_database_formateDate(DateTime.Today) + "', 'dd/mm/yyyy hh24:mi:ss') >= MIDAMIT_OPENING and TO_DATE('" + new cls_tools().get_database_formateDate(DateTime.Today) + "', 'dd/mm/yyyy hh24:mi:ss') <= MIDAMIT_CLOSING   ";
      
       // string sql = @" Select * from WEB_PRE_OFFERING_DATE where SEMESTER ='" + sem + "' and YEAR='" + year + "' and ('" + new cls_tools().get_database_formateDate(DateTime.Today) + "'>= MIDAMIT_OPENING and '" + new cls_tools().get_database_formateDate(DateTime.Today) + "'<= MIDAMIT_CLOSING )  ";
        ds.Merge(obj_db.get_viewData(sql, "WEB_PRE_OFFERING_DATE"));

        foreach (DataRow dr in ds.Tables["WEB_PRE_OFFERING_DATE"].Rows)
        {
            if (!String.IsNullOrEmpty(dr["MIDAMIT_CLOSING"].ToString()))
            {
                status = true;

            }
        }
        return status;
    }


    public DataTable get_AdmitDueClearance_student()
    {
        string sql = @" select DISTINCT AOP.*, CASE    WHEN AOP.SEMESTER = 1 THEN 'Spring'
                        WHEN AOP.SEMESTER = 2 THEN 'Summer' WHEN AOP.SEMESTER = 3 THEN 'Fall' END AS SemisterName,
                        CASE WHEN AOP.EXAMTYPE = 'M' THEN 'Mid' WHEN AOP.EXAMTYPE = 'F' THEN 'Final' END AS EXAMTYPE_Name,
                        S.SNAME, S.SPROGRAM from E_STUDENTOPENING_CONTENT AOP left join Student S on S.SID = AOP.SID where AOP.EXAMTYPE is not null";

        return obj_db.get_viewData(sql, "student");
    }


    public DataTable get_AdmitDueClearance_YearSem(String Year, String Sem)
    {
        string sql = @" select DISTINCT AOP.*, CASE    WHEN AOP.SEMESTER = 1 THEN 'Spring'
                        WHEN AOP.SEMESTER = 2 THEN 'Summer' WHEN AOP.SEMESTER = 3 THEN 'Fall' END AS SemisterName,
                        CASE WHEN AOP.EXAMTYPE = 'M' THEN 'Mid' WHEN AOP.EXAMTYPE = 'F' THEN 'Final' END AS EXAMTYPE_Name,
                        S.SNAME, S.SPROGRAM from E_STUDENTOPENING_CONTENT AOP left join Student S on S.SID = AOP.SID 
                        where AOP.EXAMTYPE is not null and AOP.YEAR = '" + Year + "' and AOP.SEMESTER = '" + Sem + "' order by AOP.SID asc";

        return obj_db.get_viewData(sql, "student");
    }


    public bool is_valid_AdmitClearance(string sid, string year, string semester, string Examtype)
    {
        bool isValid = false;
        DataSet ds = new DataSet();

        string sql = " select * from E_STUDENTOPENING_CONTENT where SID ='" + sid + "' and YEAR ='" + year + "' and SEMESTER ='" + semester + "' and EXAMTYPE ='" + Examtype + "' ";
        ds.Merge(obj_db.get_viewData(sql, "E_STUDENTOPENING_CONTENT"));

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables["E_STUDENTOPENING_CONTENT"].Rows.Count > 0)
                isValid = true;
        }

        return isValid;
    }


    public string save_STUDENTOPENING_info(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["E_STUDENTOPENING_CONTENT"].Rows)
        {
            //string code = obj_db.get_pk_no("staff");
            sql = " Insert into E_STUDENTOPENING_CONTENT (SID, SEMESTER, YEAR, EXAMTYPE, INSERTIONID, INSERTION_TIME, APPROVAL_DUES)";
            sql += " values ( '" + dr["SID"] + "', '" + dr["SEMESTER"] + "', '" + dr["YEAR"] + "', '" + dr["EXAMTYPE"] + "', '" + dr["INSERTIONID"] + "', '" + dr["INSERTION_TIME"] + "', '" + dr["APPROVAL_DUES"] + "') ";
            // update_code("staff", code);
            return obj_db.execute_query(sql);
        }
        return "";
    }

    public string save_StudentName_info(DataSet ds)
    {
        String sql = "", sql1="";

        foreach (DataRow dr in ds.Tables["SNAME_CORRECTION"].Rows)
        {
            string code = obj_db.get_pk_no("SNAME");
            sql = " Insert into SNAME_CORRECTION (SNAME_CORRECTION_ID, SID, SNAME, SNAME_PREV, UPDATEDBY, UPDATETIME)";
            sql += " values ( 'SN" + code + "', '" + dr["SID"] + "', '" + dr["SNAME"] + "', '" + dr["SNAME_PREV"] + "', '" + dr["UPDATEDBY"] + "', '" + dr["UPDATETIME"] + "' ) ";

            if (obj_db.execute_query(sql) == "1")
            {
               // update student set SNAME = 'Mr. Akbor Haider' where SID='032700011';
                sql1 = "Update Student set SNAME ='" + dr["SNAME"] + "' where SID = '" + dr["SID"] + "' ";
                if (obj_db.execute_query(sql1) == "1")
                {
                    update_code("SNAME", code);
                    return "1";
                }
            }
            //  return obj_db.execute_query(sql);
        }
        return "";
    }


    public DataTable get_StudentNameChange_list()
    {
        string sql = @"select DISTINCT S.SID,S.SPROGRAM, SC.SNAME_CORRECTION_ID,SC.SNAME_PREV, SC.UPDATEDBY, SC.UPDATETIME , S.SNAME,SC.SNAME CNG_NAME from SNAME_CORRECTION SC left join Student S on S.SID = SC.SID ";

        return obj_db.get_viewData(sql, "student");
    }
    public bool get_accounts_status(String sid)
    {
        DataSet ds = new DataSet();
        ds.Merge(this.get_allRegistred_semesters_ofA_student(sid));
        ds.Merge(this.get_all_Debit_semesters_ofA_student(sid));

        ds.Tables["registration"].Columns.Add("isRegi");
        foreach (DataRow dr in ds.Tables["registration"].Rows)
        {
            for (int p = 0; p < ds.Tables["DebitSemester"].Rows.Count; p++)
            {
                if ((ds.Tables["DebitSemester"].Rows[p]["YEAR"].ToString() == dr["YEAR"].ToString()) && (ds.Tables["DebitSemester"].Rows[p]["SEMESTER"].ToString() == dr["SEMESTER"].ToString()))
                {
                    ds.Tables["DebitSemester"].Rows.RemoveAt(p--);
                }
            }
            dr["isRegi"] = "1";
        }

        foreach (DataRow dr in ds.Tables["DebitSemester"].Rows)
        {
            DataRow drN = ds.Tables["registration"].NewRow();
            drN["SID"] = dr["SID"].ToString();
            drN["SEMESTER"] = dr["SEMESTER"].ToString();
            drN["YEAR"] = dr["YEAR"].ToString();
            drN["REGKEY"] = dr["SID"].ToString() + dr["SEMESTER"].ToString() + dr["YEAR"].ToString();
            drN["isRegi"] = "0";

            ds.Tables["registration"].Rows.Add(drN);
        }
        //int l = ds.Tables["DebitSemester"].Rows.Count;

        int i = 0;
        int rowSpan = 0;
        double regFee = 0;
        double adjustment = 0;
        int loan = 0;
        int waiver = 0;
        double tuitionFee = 0;
        double totaPable = 0;
        int k = 0;
        double paid = 0;
        double totalCredit = 0;
        double total_loan = 0;
        double total_waiver = 0;
        double total_paid = 0;
        double total_payable = 0;
        double credt = 0;

        foreach (DataRow dr in ds.Tables["registration"].Rows)
        {
            rowSpan = 0;
            regFee = 0;
            adjustment = 0;
            loan = 0;
            waiver = 0;
            tuitionFee = 0;
            totaPable = 0;
            k = 0;
            credt = 0;


            //----------- Payable-------------------------------------------------------------

            if (i == 0)
            {
                ds.Merge(this.get_admission_credit(sid));
                if (ds.Tables["ADMISSIONCREDIT"].Rows.Count > 0)
                    totaPable = Convert.ToDouble("0" + ds.Tables["ADMISSIONCREDIT"].Rows[0]["ADMINFEE"].ToString());
                rowSpan = 1; // Admission  
                i++;
            }

            ds.Merge(this.get_registration_credit(dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            rowSpan += 1;// ds.Tables["ADMINREGISTRATIONRATE"].Rows.Count; //  registration            

            ds.Merge(this.get_tuition_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            rowSpan += ds.Tables["STUDENTCREDIT"].Rows.Count;
            if (ds.Tables["STUDENTCREDIT"].Rows.Count > 0)
            {
                //if (dr["SEMESTER"].ToString() == "1" && dr["YEAR"].ToString() == "2005")
                //{
                if (!String.IsNullOrEmpty(ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString()) && !String.IsNullOrEmpty(ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString()))
                {
                    adjustment = Convert.ToDouble(ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString());
                    tuitionFee = Convert.ToDouble(ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString());
                }
                else
                {
                    adjustment = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString());
                    tuitionFee = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString());
                }
                //}
                //else
                //{
                //    adjustment = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString());
                //    tuitionFee = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString());
                //}


            }
            if (tuitionFee > 0)//(dr["isRegi"].ToString() == "1")
                foreach (DataRow drRR in ds.Tables["ADMINREGISTRATIONRATE"].Rows)
                {
                    regFee = Convert.ToDouble("0" + drRR["REGRATE"].ToString());
                }
            else
                regFee = 0;

            ds.Merge(this.get_loan_waiver_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            foreach (DataRow drL in ds.Tables["LOANSANDWAIVER"].Rows)
            {
                loan = Convert.ToInt32("0" + drL["LOAN"].ToString());
                waiver = Convert.ToInt32("0" + drL["WAIVER"].ToString());
            }

            if (tuitionFee > 0)
            {
                total_loan += Convert.ToInt32((((tuitionFee - regFee) * loan) / 100));
                // td_loans.Text = "" + Convert.ToInt32((((tuitionFee - regFee) * loan) / 100));
            }
            else
            {
                total_loan += 0;
                //td_loans.Text = "0";
            }

            if (tuitionFee > 0)
            {
                total_waiver += Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100));
                // td_waivers.Text = "" + Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100)); ;
            }
            else
            {
                total_waiver += 0;
                //td_waivers.Text = "0";
            }

            //tr_registration.Controls.Add(td_waivers);
            // aa;

            if (tuitionFee > 0)
            {
                totaPable += tuitionFee - ((((tuitionFee - regFee) * loan) / 100) + (((tuitionFee - regFee) * waiver) / 100) + adjustment);
            }
            else if (tuitionFee < 0)
            {
                totaPable += tuitionFee;// -regFee;
            }
            else
            {
                totaPable += regFee;// -adjustment;
            }

            cls_tools obj_tools = new cls_tools();
            ds.Merge(this.get_semester_debit_ofA_student(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));

            paid = 0;
            foreach (DataRow drD in ds.Tables["STUDENTDEBIT"].Rows)
            {
                paid += Convert.ToDouble("0" + drD["AMOUNT"].ToString());
            }

            ds.Merge(this.get_lateFee_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            //******************************************************  late fee for unpaid taka
            DateTime dt_firstIns = new DateTime();
            DateTime dt_secondIns = new DateTime();
            DateTime dt_thirdIns = new DateTime();
            DateTime dt_registration = new DateTime();
            DateTime dt_end = DateTime.Parse("30-SEP-2010");

            //*******************late fine section
            if (int.Parse(dr["YEAR"].ToString()) >= 2008)
            {
                DataTable fdt = new DataTable();
                fdt.Merge(this.get_last_dates_of_payment(dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));

                if (fdt.Rows.Count > 0)
                {
                    dt_firstIns = Convert.ToDateTime(fdt.Rows[0]["INSONEDATE"]);
                    dt_secondIns = Convert.ToDateTime(fdt.Rows[0]["INSTWODATE"]);
                    dt_thirdIns = Convert.ToDateTime(fdt.Rows[0]["INSTHREEDATE"]);
                    dt_registration = Convert.ToDateTime(fdt.Rows[0]["REGISTRATIONDATE"]);
                }
            }


            {
                //(semCredit-adjustment-loan-waiver-semRegFee)
                double totaReceivable = tuitionFee - adjustment - Convert.ToInt32((((tuitionFee - regFee) * loan) / 100)) - Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100)) - regFee;


                if ((int.Parse(dr["YEAR"].ToString()) == 2010 && int.Parse(dr["SEMESTER"].ToString()) == 3) || int.Parse(dr["YEAR"].ToString()) > 2010)
                {
                    int fc = 0;
                    if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "2"))
                    //if (DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "2"))
                    {
                        if (totaReceivable > 0)
                        {
                            if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "8")))
                            //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "8"))
                            {
                                if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                                //if (today.compareTo(dt_firstIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "5"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (1st Inst)";
                                        tdr["HEADSN"] = "25"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                                if (DateTime.Today.CompareTo(dt_secondIns) > 0)
                                //if (today.compareTo(dt_secondIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "6"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (2nd Inst)";
                                        tdr["HEADSN"] = "26"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                                if (DateTime.Today.CompareTo(dt_thirdIns) > 0)
                                //if (today.compareTo(dt_thirdIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "7"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                                        tdr["HEADSN"] = "27"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                            }
                        }//end if( isPaidOfThisHead(idd,yeari,semi,
                    }
                    //li = li + fc;

                }//end if((yeari==2010 && semi==3) ||yeari>2010)

                else if (int.Parse(dr["YEAR"].ToString()) == 2008 && int.Parse(dr["SEMESTER"].ToString()) == 1)
                {
                    int fc = 0;
                    double tempBalanceupto = 0;//balanceUpto;


                    //double paid = new student_webService().GetCurrentSemTotalTutionFeepayment(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString());
                    paid += tempBalanceupto;
                    if (totaReceivable - (paid) > cls_tools.finePlusMinus)
                    {
                        if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                        {
                            DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                            tdr["amount"] = (int)((totaReceivable - paid) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_thirdIns));
                            tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                            tdr["HEADSN"] = "27"; fc++;
                            ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                        }
                    }
                }//  end year=2008 and semester =1
                else if (int.Parse(dr["YEAR"].ToString()) >= 2008)
                {
                    //**************************************for registration
                    int fc = 0;
                    double tempBalanceupto = 0;//balanceUpto;
                    double installmentAmout = totaReceivable / 3;
                    if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "8"))
                    {

                    }
                    else//course fee full not paid
                    {
                        //if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5"))//first inst
                        {
                            if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_firstIns));
                                    tdr["HEADNAME"] = "Late Fine (1st Inst)";
                                    tdr["HEADSN"] = "25"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }
                        // if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6"))//second inst
                        {
                            if (DateTime.Today.CompareTo(dt_secondIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_secondIns));
                                    tdr["HEADNAME"] = "Late Fine (2nd Inst)";
                                    tdr["HEADSN"] = "26"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }
                        // if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7"))//third inst
                        {
                            if (DateTime.Today.CompareTo(dt_thirdIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_thirdIns));
                                    tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                                    tdr["HEADSN"] = "27"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }

                    }//course fee full end                   
                }/// if(Integer.parseInt(syear[j])>=2008 && Integer.parseInt(ssem[j])==1)

            }


            //****************************************************** end late fee for unpaid taka
            rowSpan += ds.Tables["LATEFEECREDIT"].Rows.Count;

            foreach (DataRow drL in ds.Tables["LATEFEECREDIT"].Rows)
                totaPable += Convert.ToDouble(drL["amount"].ToString());


            //----------------------- Semester Balance ---------------------------------------------
            
            total_payable += totaPable;
            total_paid += paid;

            //------------------ clear All ---------------------------------------------------------
            ds.Tables["ADMINREGISTRATIONRATE"].Rows.Clear();
            ds.Tables["LATEFEECREDIT"].Rows.Clear();
            ds.Tables["ADMISSIONCREDIT"].Rows.Clear();
            ds.Tables["STUDENTCREDIT"].Rows.Clear();
            ds.Tables["LOANSANDWAIVER"].Rows.Clear();
            ds.Tables["STUDENTDEBIT"].Rows.Clear();
        }

        //----------------------- Semester Balance ---------------------------------------------


        if ((Convert.ToInt32(total_payable) - Convert.ToInt32(total_paid)) > 500)
            return false;

        return true;
    }

    public string get_clearence_statusNew(string PROGRAM_ID, String regiKey, string semester, string year)
    {
        /*  Status = Accounce + Library + Evaluation  +  PublishDate
         * -------------------------------------------------------
         *  true   =   1      +    1    +       1     +     1
         * -------------------------------------------------------
         *  False  =   0      +    0    +       0     +     0
         * ============================================*/

        string status = "0_0_0_0";

        DataSet ds = new DataSet();
        string sql = @" SELECT * from REGISTATUS where REGKEY='" + regiKey + "' ";
        ds.Merge(obj_db.get_viewData(sql, "REGISTATUS"));

        foreach (DataRow dr in ds.Tables["REGISTATUS"].Rows)
        {
            if (dr["ACC_STATUS"].ToString() == "1")// && 
            {
                status = "1_";
            }
            else 
            {
                status = "1_";
            }

            if (dr["LIB_STATUS"].ToString() == "1")
                status += "1_";
            else
            {
                status += "1_";
            }

           // if(get_evaluation_status(regiKey))
            if (get_evaluation_status_withoutThesis(regiKey))
                status += "1_";
            else
                status += "0_";

            DataSet ds2 = new DataSet();
            ds2.Merge(new admin_webService().get_pre_offeringDateNew(semester, year, PROGRAM_ID));
            if (ds2.Tables["WEB_PRE_OFFERING_DATE"].Rows.Count > 0)
            {
                foreach (DataRow dr2 in ds2.Tables["WEB_PRE_OFFERING_DATE"].Rows)
                {
                    if (DateTime.Today >= Convert.ToDateTime(dr2["PROG_RESULT_PUBLISH_DATE"]))
                    {
                        status += "1";
                    }
                    else
                    {
                        status += "0";
                    }
                }
            }
            else { status += "0"; }
        }


        return status;
    }

    public string get_clearence_status(String regiKey, string semester, string year)
    {
        /*  Status = Accounce + Library + Evaluation  +  PublishDate
         * -------------------------------------------------------
         *  true   =   1      +    1    +       1     +     1
         * -------------------------------------------------------
         *  False  =   0      +    0    +       0     +     0
         * ============================================*/

        string status = "0_0_0_0";

        DataSet ds = new DataSet();
        string sql = @" SELECT * from REGISTATUS where REGKEY='" + regiKey + "' ";
        ds.Merge(obj_db.get_viewData(sql, "REGISTATUS"));

        foreach (DataRow dr in ds.Tables["REGISTATUS"].Rows)
        {
            if (dr["ACC_STATUS"].ToString() == "1")// && 
            {
                status = "1_";
            }
            else //if (dr["ACC_STATUS"].ToString() == "0")
            {
                status = "0_";
            }

            if (dr["LIB_STATUS"].ToString() == "1")
                status += "1_";
            else// if (dr["LIB_STATUS"].ToString() == "1")
            {
                status += "0_";
            }

            // if(get_evaluation_status(regiKey))
            if (get_evaluation_status_withoutThesis(regiKey))
                status += "1_";
            else
                status += "0_";

            DataSet ds2 = new DataSet();
            ds2.Merge(new admin_webService().get_pre_offeringDate(semester, year));
            if (ds2.Tables["WEB_PRE_OFFERING_DATE"].Rows.Count > 0)
            {
                foreach (DataRow dr2 in ds2.Tables["WEB_PRE_OFFERING_DATE"].Rows)
                {
                    if (DateTime.Today >= Convert.ToDateTime(dr2["SEM_RESULT_PUBLISH"]))
                    {
                        status += "1";
                    }
                    else
                    {
                        status += "0";
                    }
                }
            }
            else { status += "0"; }
        }


        return status;
    }
    //need to join
    public bool get_evaluation_status_withoutThesis(String regikey)
    {
        int offer_total = 0;
        int evaluate_total = 0;

        DataSet ds = new DataSet();


        // sql = @" SELECT count(*)as total FROM OFFERERINGANDGRADE WHERE REGKEY ='" + regikey + "'";
        string sql = "SELECT count(*) as total FROM OFFERERINGANDGRADE OFC inner join COURSEDETAILS on SUBSTR(OFC.COURSEKEY ,6) = COURSEDETAILS.COURSECODE WHERE REGKEY ='" + regikey + "' and COURSEDETAILS.ISPROJECT != 1";
        ds.Merge(obj_db.get_viewData(sql, "OFFERERINGANDGRADE"));

        if (ds.Tables["OFFERERINGANDGRADE"].Rows.Count > 0)
        {
            offer_total = Convert.ToInt32("0" + ds.Tables["OFFERERINGANDGRADE"].Rows[0]["total"].ToString());
        }

        ds.Tables.Clear();

        sql = @" SELECT count(*)as total FROM WEB_TEACHER_EVAL_VALUE WHERE REGKEY ='" + regikey + "'";
        ds.Merge(obj_db.get_viewData(sql, "WEB_TEACHER_EVAL_VALUE"));

        if (ds.Tables["WEB_TEACHER_EVAL_VALUE"].Rows.Count > 0)
        {
            evaluate_total = Convert.ToInt32("0" + ds.Tables["WEB_TEACHER_EVAL_VALUE"].Rows[0]["total"].ToString());
        }

        if (evaluate_total >= offer_total)
            return true;
        else
        {
            if ((new student_webService().is_ResultView_SpeciallyOpen(regikey)) || (new student_webService().is_ResultView_AccountFee(regikey)))
            {
                return true;
            }
            else
                //check registatus table
                return false;
        }
    }

    public bool get_evaluation_status(String regikey)
    {
        int offer_total = 0;
        int evaluate_total = 0;
      
        DataSet ds = new DataSet();

        string sql = @" SELECT count(*)as total FROM OFFERERINGANDGRADE WHERE REGKEY ='" + regikey + "'";
        ds.Merge(obj_db.get_viewData(sql, "OFFERERINGANDGRADE"));

        if (ds.Tables["OFFERERINGANDGRADE"].Rows.Count > 0)
        {
            offer_total = Convert.ToInt32("0" + ds.Tables["OFFERERINGANDGRADE"].Rows[0]["total"].ToString());
        }

        ds.Tables.Clear();

        sql = @" SELECT count(*)as total FROM WEB_TEACHER_EVAL_VALUE WHERE REGKEY ='" + regikey + "'";
        ds.Merge(obj_db.get_viewData(sql, "WEB_TEACHER_EVAL_VALUE"));

        if (ds.Tables["WEB_TEACHER_EVAL_VALUE"].Rows.Count > 0)
        {
            evaluate_total = Convert.ToInt32("0" + ds.Tables["WEB_TEACHER_EVAL_VALUE"].Rows[0]["total"].ToString());
        }

        if (evaluate_total >= offer_total)
            return true;
        else
        {
            if (new student_webService().is_ResultView_SpeciallyOpen(regikey))
            {
                return true;
            }
            else
                //check registatus table
                return false;
        }
    }

    //-------------------------need to add for view result without evaluation

    public bool is_ResultView_SpeciallyOpen(string regikey)
    {
        bool status = false;
        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData("select * from REGISTATUS where REGKEY='" + regikey + "' ", "REGISTATUS"));

        foreach (DataRow dr in ds.Tables["REGISTATUS"].Rows)
        {
            if (dr["RESULTVIEW_STATUS"].ToString() == "1")
            {

                status = true;
                break;

            }

        }
        return status;

    }


    public bool is_ResultView_AccountFee(string regikey)
    {
        bool status = false;
        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData("select sum(AMOUNT) AMOUNT, HEADSN from STUDENTDEBIT group by SID,SEMESTER,YEAR,HEADSN HAVING SID||SEMESTER||YEAR ='" + regikey + "' and  HEADSN = 34 and sum(AMOUNT) >= 100 ", "STUDENTDEBIT"));
        //  
        foreach (DataRow dr in ds.Tables["STUDENTDEBIT"].Rows)
        {
            if (dr["HEADSN"].ToString() == "34" && Convert.ToDecimal(dr["AMOUNT"].ToString()) >= 100)
            {

                status = true;
                break;

            }

        }
        return status;

    }

    public string set_student_resultView_clearence(string regKey, string status)
    {
        return obj_db.execute_query(" update REGISTATUS set RESULTVIEW_STATUS='" + status + "' where REGKEY='" + regKey + "'  ");
    }

    public string get_program_ofA_student(String sid)
    {
        string DepCode = "";
        DataSet ds = new DataSet();

        string sql = @" SELECT SPROGRAM FROM STUDENT WHERE SID ='"+sid+"'";       
        ds.Merge( obj_db.get_viewData(sql, "STUDENT"));

        if (ds.Tables["STUDENT"].Rows.Count > 0)        
        {
            DepCode = ds.Tables["STUDENT"].Rows[0]["SPROGRAM"].ToString();
        }
        return DepCode;
    }

    public DataTable get_registered_student(String sem, string year, string dep, string batch)
    {


        string sql = @"   SELECT STUDENT.SID, STUDENT.SNAME, REGISTATUS.RESULTVIEW_STATUS, STD_ALL_EVALUTED.COURSE, ";
        sql += " CASE WHEN STD_ALL_EVALUTED.COURSE is not null THEN STD_ALL_EVALUTED.COURSE WHEN STD_ALL_EVALUTED.COURSE is null THEN 0  END  Course_Count, ";
        sql += " CASE WHEN AcoountEntry.Amount is not null THEN AcoountEntry.Amount WHEN AcoountEntry.Amount is null THEN 0  END  Amount ";
        sql += " FROM REGISTATUS inner join STUDENT on STUDENT.SID = REGISTATUS.SID left join STD_ALL_EVALUTED on STD_ALL_EVALUTED.REGKEY = REGISTATUS.REGKEY ";
        sql += "left join ( select SID, Sum(amount) Amount, year, semester from STUDENTDEBIT where STUDENTDEBIT.HEADSN = '34' and year = '" + year + "' and STUDENTDEBIT.SEMESTER = '" + sem + "' group by SID,  year, semester  )AcoountEntry on AcoountEntry.SID = STUDENT.SID";
        //       string sql = @" SELECT STUDENT.SID, STUDENT.SNAME, REGISTATUS.RESULTVIEW_STATUS, STD_ALL_EVALUTED.COURSE FROM REGISTATUS ";
        //sql += " inner join STUDENT on STUDENT.SID = REGISTATUS.SID ";
        //sql += " left join STD_ALL_EVALUTED on STD_ALL_EVALUTED.REGKEY = REGISTATUS.REGKEY ";
        // string sql = @" SELECT * FROM REGISTATUS, STUDENT ";
        sql += " WHERE (REGISTATUS.SEMESTER='" + sem + "' AND REGISTATUS.year='" + year + "') and sprogram='" + dep + "' ";
        if (!String.IsNullOrEmpty(batch))
            sql += "  AND REGISTATUS.regkey LIKE'" + batch + "%' order by STUDENT.SID asc";

        return obj_db.get_viewData(sql, "student");
    }

	public string get_year_and_semester_ofA_student(String sid)
    {
        string yearsem = "";

        string sql = @"SELECT (ADMINYEAR || ADMINSEMETER) AS YEARSEM FROM STUDENT WHERE sid='" + sid + "' ";        
        yearsem = obj_db.get_viewData(sql, "student").Rows[0][0].ToString();

        return yearsem;
    }


    public string get_Prevyear_and_semester_ofA_student(String sid)
    {
        string yearsem = "";

        string sql = @"SELECT (ADMINYEAR || ADMINSEMETER) AS YEARSEM FROM STUDENT WHERE sid='" + sid + "' ";
        yearsem = obj_db.get_viewData(sql, "student").Rows[0][0].ToString();

        return "20152";
    }
    
    public DataTable get_allRegistred_semesters_ofA_student(String sid)
    {
        string sql = @"SELECT * FROM REGISTATUS WHERE sid='" + sid + "' ";
        sql += "  ORDER BY year ASC, SEMESTER ASC";        
        return obj_db.get_viewData(sql, "registration");
    }

    public DataTable get_allRegistred_semesters_ofA_student_for_account(String sid)
    {
        string sql = @"SELECT * FROM ALLSID_YEAR_SEMESTER WHERE sid='" + sid + "' ";
        sql += "  ORDER BY year ASC, SEMESTER ASC";
        return obj_db.get_viewData(sql, "registration");
    }

	public DataTable get_allRegistred_semesters_ofA_student_for_account(String sid, String semester, String year)
    {
        string sql = @"SELECT * FROM ALLSID_YEAR_SEMESTER WHERE sid='" + sid + "' and YEAR||SEMESTER <= " + year + semester;
        sql += "  ORDER BY year ASC, SEMESTER ASC";
        return obj_db.get_viewData(sql, "registration");
    }
	
    public DataTable get_all_Debit_semesters_ofA_student(String sid)
    {
        string sql = @"SELECT DISTINCT sid,YEAR, SEMESTER FROM STUDENTDEBIT ";
        sql += "WHERE sid='" + sid + "' ORDER BY year ASC, SEMESTER ASC ";
        return obj_db.get_viewData(sql, "DebitSemester");
    }

	public DataTable get_all_Debit_semesters_ofA_student(String sid, String semester, String year)
    {
        string sql = @"SELECT DISTINCT sid,YEAR, SEMESTER FROM STUDENTDEBIT ";
        sql += "WHERE sid='" + sid + "' and YEAR||SEMESTER <= " + year + semester + " ORDER BY year ASC, SEMESTER ASC ";
        return obj_db.get_viewData(sql, "DebitSemester");
    }
	
    public double get_semester_creditHrs_ofA_student(String sid, string sem, string year)
    {
        double credits = 0;
        DataSet ds = new DataSet();
        string sql = @" SELECT SUM(CHOURS)as total FROM OFFERERINGANDGRADE  WHERE REGKEY='"+sid+sem+year+"' ";
        ds.Merge(obj_db.get_viewData(sql, "OFFERERINGANDGRADE"));
        if (ds.Tables["OFFERERINGANDGRADE"].Rows.Count > 0)
        {
            credits = Convert.ToDouble("0" + ds.Tables["OFFERERINGANDGRADE"].Rows[0]["total"].ToString());
        }

        return credits;
    }

    // ------------------------  Account ---------------------------------
    public DataTable get_admission_credit(string sid)
    {
        string sql = " Select * from ADMISSIONCREDIT where sid='" + sid+ "' ";
        return obj_db.get_viewData(sql, "ADMISSIONCREDIT");
    }
    public DataTable get_admission_creditNew(string sid, string Semester, string Year)
    {
        string sql = " Select * from ADMISSIONCREDIT where sid='" + sid + "' and YEAR= '" + Year + "' and SEMESTER='" + Semester + "' ";
        return obj_db.get_viewData(sql, "ADMISSIONCREDIT");
    }
	public double get_registration_credit_by_id(string id, string sem, string year)
    {
        double amount = 0.0;

        string sql = "SELECT AMOUNT FROM STUDENTDEBIT WHERE HEADSN = 2 AND SID = '" + id + "' AND SEMESTER = " + sem + " AND YEAR =" + year;
        DataTable dt = obj_db.get_viewData(sql, "STUDENTDEBIT");
        if (dt.DataSet.Tables[0].Rows.Count > 0)
            amount = Convert.ToDouble(dt.DataSet.Tables[0].Rows[0]["AMOUNT"]);

        return amount;
    }
	
    public DataTable get_registration_credit(string sem, string year)
    {
        string sql = " Select * from ADMINREGISTRATIONRATE where SEMESTER='" + sem + "' and YEAR='" + year + "' ";
        return obj_db.get_viewData(sql, "ADMINREGISTRATIONRATE");
    }

    public DataTable get_tuition_credit(string id, string sem, string year)
    {
        string sql = " Select SID, NVL(AMOUNT,0) AMOUNT, COMENTS, YEAR, SEMESTER,ADJUSTMENT, NVL(ADJUSTMENTCOMMENT,0) ADJUSTMENTCOMMENT, CREATED, CREATEDBY, UPDATED, UPDATEDBY from STUDENTCREDIT  where sid='" + id + "' and SEMESTER='" + sem + "' and YEAR='" + year + "' ";
        return obj_db.get_viewData(sql, "STUDENTCREDIT");
    }

    public DataTable get_lateFee_credit(string sid, string sem, string year)
    {
        string sql = " SELECT * FROM LATEFEECREDIT,STUDENTHEADINFO ";
               sql +=" WHERE STUDENTHEADINFO.HEADSN=LATEFEECREDIT.HEADSN ";
               sql +=" AND  sid='"+sid+"' AND SEMESTER='"+sem+"' AND year='"+year+"' ";
               return obj_db.get_viewData(sql, "LATEFEECREDIT");
    }

    public DataTable get_loan_waiver_credit(string sid, string sem, string year)
    {
        string sql = " SELECT * FROM LOANSANDWAIVER";
        sql += " WHERE  sid='" + sid + "' AND SEMESTER='" + sem + "' AND year='" + year + "' ";
        return obj_db.get_viewData(sql, "LOANSANDWAIVER");
    }

    public DataTable get_semester_debit_ofA_student(string sid, string sem, string year)
    {
        string sql = " SELECT* FROM STUDENTDEBIT, STUDENTHEADINFO  ";
        sql += " WHERE STUDENTDEBIT.HEADSN=STUDENTHEADINFO.HEADSN ";
        sql += " AND  sid='" + sid + "'  AND SEMESTER='" + sem + "' AND YEAR='" + year + "' ";
        return obj_db.get_viewData(sql, "STUDENTDEBIT");
    }
    public DataTable get_last_dates_of_payment(string sem, string year)
    {
        String qul = " select INSONEDATE, INSTWODATE, INSTHREEDATE,REGISTRATIONDATE "+
            " from ADMINREGISTRATIONRATE where YEAR='" + year + "' and  SEMESTER='" + sem + "' ";
        return obj_db.get_viewData(qul, "temp");
    }



    public string get_PreviousRegistrationYear_Semister(String sid)
    {
        string PrevSem = "";

        //String sql = "SELECT TO_NUMBER(SUBSTR(REVERSE(MAX(R.YEAR||R.SEMESTER)),0,1)) PREV_SEMESTER ";
        //sql += " FROM REGISTATUS R inner join OFFERERINGANDGRADE O on substr(O.REGKEY,1, 9)= R.SID ";
        //sql += "WHERE R.SID = '" + sid + "' AND R.YEAR||R.SEMESTER NOT IN ";
        //sql += "( SELECT MAX(R.YEAR||R.SEMESTER)  FROM REGISTATUS R  inner join OFFERERINGANDGRADE O on substr(O.REGKEY,1, 9)= R.SID WHERE R.SID = '" + sid + "'  ) and substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR";

        String sql = " SELECT  TO_NUMBER(SUBSTR(REVERSE(MAX(R.YEAR||R.SEMESTER)),0,1)) PREV_SEMESTER ";
        sql += " FROM REGISTATUS R join OFFERERINGANDGRADE O on (substr(O.REGKEY,1, 9)= R.SID and  substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR) ";
        sql += "  WHERE R.SID = '" + sid + "'   AND R.YEAR||R.SEMESTER NOT IN   (SELECT MAX(R.YEAR||R.SEMESTER) ";
        sql += "  FROM REGISTATUS R   join OFFERERINGANDGRADE O on (substr(O.REGKEY,1, 9)=   R.SID and substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR)) ";
       

        PrevSem = obj_db.get_viewData(sql, "REGISTATUS").Rows[0][0].ToString();

        return PrevSem;
    }

    public string get_PreviousRegistrationYear(String sid)
    {
        string PrevYear = "";


        //String sql = " SELECT MAX(R.YEAR) PREV_YEAR FROM REGISTATUS R  inner join OFFERERINGANDGRADE O on substr(O.REGKEY,1, 9)= R.SID ";
        //sql += " WHERE R.SID = '" + sid + "' AND R.YEAR||R.SEMESTER NOT IN ";
        //sql += " ( SELECT MAX(R.YEAR||R.SEMESTER)  FROM REGISTATUS R  inner join OFFERERINGANDGRADE O on substr(O.REGKEY,1, 9)= R.SID WHERE R.SID = '" + sid + "'  ) and substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR";


        String sql = " SELECT MAX(R.YEAR) PREV_YEAR   ";
        sql += " FROM REGISTATUS R join OFFERERINGANDGRADE O on (substr(O.REGKEY,1, 9)= R.SID and  substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR) ";
       sql += "  WHERE R.SID = '" + sid + "'   AND R.YEAR||R.SEMESTER NOT IN   (SELECT MAX(R.YEAR||R.SEMESTER) ";
       sql += "  FROM REGISTATUS R   join OFFERERINGANDGRADE O on (substr(O.REGKEY,1, 9)=   R.SID and substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR)) ";
       
        
        //String sql = "SELECT MAX(R.YEAR) PREV_YEAR FROM REGISTATUS R  ";
        //sql += "  WHERE R.SID = '" + sid + "' AND R.YEAR NOT IN ";
        //sql += "  ( SELECT MAX(R.YEAR)  FROM REGISTATUS R  WHERE R.SID = '" + sid + "')";


        PrevYear = obj_db.get_viewData(sql, "REGISTATUS").Rows[0][0].ToString();

        return PrevYear;
    }


    public DataTable get_OFFERERINGANDGRADE_Year_Semister(String sid)
    {
        String sql = " SELECT  MAX(R.YEAR) PREV_YEAR, TO_NUMBER(SUBSTR(REVERSE(MAX(R.YEAR||R.SEMESTER)),0,1)) PREV_SEMESTER  ";
        sql += "  FROM REGISTATUS R join OFFERERINGANDGRADE O on (substr(O.REGKEY,1, 9)= R.SID and  substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR)  ";
        sql += "   WHERE R.SID = '" + sid + "'  ";
       
  //  String sql =    " SELECT  MAX(R.YEAR) PREV_YEAR,  TO_NUMBER(SUBSTR(REVERSE(MAX(R.YEAR||R.SEMESTER)),0,1)) PREV_SEMESTER  ";
  //sql += " FROM REGISTATUS R    join OFFERERINGANDGRADE O on (substr(O.REGKEY,1, 9)= R.SID and  substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR)  ";
  //sql += "  WHERE R.SID = '" + sid + "'     AND R.YEAR||R.SEMESTER NOT IN (SELECT MAX(R.YEAR||R.SEMESTER) ";
  //sql += "  FROM REGISTATUS R WHERE R.SID = '" + sid + "'  ) ";

        return obj_db.get_viewData(sql, "Studentlist");
    }
    public DataTable GetSID_CourseOfferingPermission(string SID, string YEAR, String SEMESTER)
    {
        string sql = "SELECT DISTINCT CRS_OFFERING_VALID_STU.* from CRS_OFFERING_VALID_STU ";
        sql += " WHERE SID = '" + SID + "'  and YEAR ='" + YEAR + "' and  SEMESTER  ='" + SEMESTER + "'";

        return obj_db.get_viewData(sql, "PermissionList");
    }


    public string save_CourseOfferClearence(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["CRS_OFFERING_VALID_STU"].Rows)
        {
            sql = " Insert into CRS_OFFERING_VALID_STU (SID, SEMESTER,YEAR,CLEAREDBY,CLEARED_TIME, APPROVE_DUES)";
            sql += " values ('" + dr["SID"] + "', '" + dr["SEMESTER"] + "', '" + dr["YEAR"] + "', '" + dr["CLEAREDBY"] + "', '" + dr["CLEARED_TIME"] + "' , '" + dr["APPROVE_DUES"] + "') ";
            return obj_db.execute_query(sql);
        }
        return "";
    }

    public string set_student_CourseAdvising_Update(DataSet ds)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["CRS_OFFERING_VALID_STU"].Rows)
        {
            sql = " Update CRS_OFFERING_VALID_STU set VALID = 1 , APPROVE_DUES='" + dr["APPROVE_DUES"] + "' where SID ='" + dr["SID"] + "' and SEMESTER='" + dr["SEMESTER"] + "' and YEAR ='" + dr["YEAR"] + "' ";

            if (obj_db.execute_query(sql) == "1")
            {
                count = 1;
            }
        }
        // update_code("T_STUDENTDEBIT", code);

        return Convert.ToString(count);
    }

    public DataTable get_registered_Course_OfferingStdIT(String sem, string year, string batch)
    {
        string sql = @"select DISTINCT R.SID, S.SNAME, COV.VALID,COV.APPROVE_DUES from registatus R left join student S on S.SID= R.SID  ";
        sql += " inner join CRS_OFFERING_VALID_STU  COV on (R.year=COV.YEAR and R.semester=COV.semester and R.sid =COV.SID)  ";
        sql += " where R.YEAR='" + year + "' and R.SEMESTER='" + sem + "' and R.regkey LIKE '" + batch + "%'  order by R.SID asc";

        return obj_db.get_viewData(sql, "RegStudent");
    }
    public DataTable get_registered_Course_OfferingStd(String sem, string year, string dep, string batch)
    {
        string sql = @"select DISTINCT R.SID, S.SNAME, COV.VALID from registatus R left join student S on S.SID= R.SID  ";
        sql += " left join CRS_OFFERING_VALID_STU  COV on (R.year=COV.YEAR and R.semester=COV.semester and R.sid =COV.SID)  ";
        sql += " where R.YEAR='" + year + "' and R.SEMESTER='" + sem + "' and R.regkey LIKE '" + batch + "%'  and S.SPROGRAM ='" + dep + "' order by R.SID asc";

        return obj_db.get_viewData(sql, "RegStudent");
    }
    public DataTable get_OFFERERINGANDGRADE_Year_Semister_either(String sid)
    {
        String sql = " SELECT  MAX(R.YEAR) PREV_YEAR, TO_NUMBER(SUBSTR(REVERSE(MAX(R.YEAR||R.SEMESTER)),0,1)) PREV_SEMESTER  ";
        sql += " FROM REGISTATUS R join OFFERERINGANDGRADE O on (substr(O.REGKEY,1, 9)= R.SID and  substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR)  ";
        sql += " WHERE R.SID = '" + sid + "'   AND R.YEAR||R.SEMESTER NOT IN   (SELECT MAX(R.YEAR||R.SEMESTER)  ";
        sql += "  FROM REGISTATUS R   join OFFERERINGANDGRADE O on (substr(O.REGKEY,1, 9)=   R.SID and substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR))  ";

        //String sql = " SELECT  MAX(R.YEAR) PREV_YEAR,  TO_NUMBER(SUBSTR(REVERSE(MAX(R.YEAR||R.SEMESTER)),0,1)) PREV_SEMESTER  ";
        //sql += " FROM REGISTATUS R    join OFFERERINGANDGRADE O on (substr(O.REGKEY,1, 9)= R.SID and  substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR)  ";
        //sql += "  WHERE R.SID = '" + sid + "'     AND R.YEAR||R.SEMESTER NOT IN (SELECT MAX(R.YEAR||R.SEMESTER) ";
        //sql += "  FROM REGISTATUS R WHERE R.SID = '" + sid + "'  ) ";

        return obj_db.get_viewData(sql, "Studentlist");
    }


    public DataTable get_ADMINREGISTRATIONRATE_LYS()
    {
        String sql = "select year, semester from ADMINREGISTRATIONRATE where Year||semester=(select max(YEAR||Semester) from ADMINREGISTRATIONRATE)";
        return obj_db.get_viewData(sql, "Studentlist");
    }

    public DataTable checkStudentCount()
    {
        String sql = "Select count(*) as total from v$session v where lower(v.username) = 'adminuser' and program <> 'JDBC' ";
        return obj_db.get_viewData(sql, "StudentCount");
    }

    public DataTable get_Registatus_Year_Semister(String sid)
    {
        String sql = " SELECT  MAX(R.YEAR) PREV_YEAR, ";
 sql += " TO_NUMBER(SUBSTR(REVERSE(MAX(R.YEAR||R.SEMESTER)),0,1)) PREV_SEMESTER  ";
 sql += " FROM REGISTATUS R   WHERE R.SID = '" + sid + "'    ";
 //sql += " AND R.YEAR||R.SEMESTER NOT IN (SELECT MAX(R.YEAR||R.SEMESTER) FROM REGISTATUS R WHERE R.SID = '" + sid + "'  ) ";

        return obj_db.get_viewData(sql, "Registedlist");
    }

    public string save_Student_Route_info(DataSet ds)
    {
        String sql = ""; int count = 0;

        foreach (DataRow dr in ds.Tables["C_TRANSPORT_ROUTE"].Rows)
        {
            string code = obj_db.get_pk_no("TRNS");
            sql = " Insert into C_TRANSPORT_ROUTE (TRANSPORT_ROUTE_ID,SID,PREV_CONTACT, PROGRAM_ID, CONTACT, INSERTION, PICK_PLACE,   PRESENT_ADDRESS)";
            sql += " values ('" + "TRNS" + code + "', '" + dr["SID"] + "', '" + dr["PREV_CONTACT"] + "', '" + dr["PROGRAM_ID"] + "', '" + dr["CONTACT"] + "', '" + dr["INSERTION"] + "',  '" + dr["PICK_PLACE"] + "', '" + dr["PRESENT_ADDRESS"] + "' ) ";
            update_code("TRNS", code);
            if (obj_db.execute_query(sql) == "1")
            {
                return "TRNS" + code;
            }

        }
        return "";
    }

    public string Update_Student_Route_info(DataSet ds, string TransportID)
    {
        String sql = ""; int count = 0;
        foreach (DataRow dr in ds.Tables["C_TRANSPORT_ROUTE"].Rows)
        {
             sql = @" update C_TRANSPORT_ROUTE set POINT_ID ='" + dr["POINT_ID"] + "', UPDATE_TIME ='" + dr["UPDATE_TIME"] + "' where TRANSPORT_ROUTE_ID='" + TransportID + "' ";
        }
        string i = obj_db.execute_query(sql);
        return Convert.ToString(i);

    }

    public DataTable get_PROGRAM_ID(String sid)
    {
        String sql = " SELECT SID,SNAME, PROGRAM_ID, NAME PROGRAM, COLLEGECODE, PHONE from  Student  left join C_PROGINDEPT on STUDENT.PROGRAM_ID =C_PROGINDEPT.C_PROGINDEPT_ID  left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID =C_PROGINDEPT.DEPID WHERE SID = '" + sid + "'    ";

        return obj_db.get_viewData(sql, "Student");
    }

    public DataTable get_TransportInfo(String sid)
    {
      //  String sql = " SELECT * from  C_TRANSPORT_ROUTE  WHERE SID = '" + sid + "'    ";
                String sql = "    SELECT TRNS.*, TNP.POINT_NAME, C_TRNS_ROUTE.ROUTE_ID, C_TRNS_ROUTE.ROUTE_NAME ";
                sql += " from  C_TRANSPORT_ROUTE TRNS  left join C_TRNS_ROUTE_POINT TNP on TNP.POINT_ID = TRNS.POINT_ID ";
                sql += "left join C_TRNS_ROUTE on C_TRNS_ROUTE.ROUTE_ID =TNP.ROUTE_ID  WHERE TRNS.SID = '" + sid + "'  ";
                return obj_db.get_viewData(sql, "Student");
    }
    public DataTable get_min_Registatus_Year_Semister(String sid)
    {
        String sql = " SELECT  min(R.YEAR) PREV_YEAR, TO_NUMBER(SUBSTR(REVERSE(MIN(R.YEAR||R.SEMESTER)),0,1)) PREV_SEMESTER   ";
         sql += " FROM REGISTATUS R  WHERE R.SID = '" + sid + "'    ";
        //sql += " AND R.YEAR||R.SEMESTER NOT IN (SELECT MAX(R.YEAR||R.SEMESTER) FROM REGISTATUS R WHERE R.SID = '" + sid + "'  ) ";

        return obj_db.get_viewData(sql, "Registedlist");
    }

    public DataTable get_Registatus_Year_Semister_AdmitCard(String sid)
    {
        String sql = " SELECT  MAX(REGISTATUS.YEAR) PREV_YEAR, TO_NUMBER(SUBSTR(REVERSE(MAX(REGISTATUS.YEAR||REGISTATUS.SEMESTER)),0,1)) PREV_SEMESTER  ";
sql += " from REGISTATUS join OFFERERINGANDGRADE O on (substr(O.REGKEY,1, 9)= REGISTATUS.SID and  substr(O.REGKEY, -5) = REGISTATUS.SEMESTER||REGISTATUS.YEAR)  "; 
sql += " WHERE REGISTATUS.SID = '153400001'   AND REGISTATUS.YEAR||REGISTATUS.SEMESTER NOT IN  ";
sql += " (SELECT  MAX(R.YEAR)||TO_NUMBER(SUBSTR(REVERSE(MAX(R.YEAR||R.SEMESTER)),0,1))  ";
sql += " FROM REGISTATUS R join OFFERERINGANDGRADE O on (substr(O.REGKEY,1, 9)= R.SID and  substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR)   ";
sql += " WHERE R.SID = '153400001'   AND R.YEAR||R.SEMESTER NOT IN   (SELECT MAX(R.YEAR||R.SEMESTER)  ";
sql += " FROM REGISTATUS R   join OFFERERINGANDGRADE O on (substr(O.REGKEY,1, 9)=   R.SID and substr(O.REGKEY, -5) = R.SEMESTER||R.YEAR))   ";
        //sql += " AND R.YEAR||R.SEMESTER NOT IN (SELECT MAX(R.YEAR||R.SEMESTER) FROM REGISTATUS R WHERE R.SID = '" + sid + "'  ) ";

        return obj_db.get_viewData(sql, "Registedlist");
    }

    public DataTable get_Balance_Year_Semister(String sid, string sem, string year)
    {
        String sql = " select NVL((Sum(PAYABLE)- sum(PAID)),0) balance ";
        sql += " from T_STUDENTBALANCE where SID ='" + sid + "'  and  YEAR||SEMESTER <= '" + year + "'||'" + sem + "' ";

        return obj_db.get_viewData(sql, "Balance");
    }

    public DataTable get_StdBalance_Year_Semister(String sid, string sem, string year)
    {
        String sql = " select T_STUDENTBALANCE.*, case semester when 1 then 'Spring' when 2 then 'Summer' when 3 then 'Fall' end as Sem, ";
        sql += "(T_STUDENTBALANCE.PAYABLE-T_STUDENTBALANCE.PAID) as due from  T_STUDENTBALANCE where SID = '" + sid + "' and year = '" + year + "' and SEMESTER='" + sem + "' ";

        return obj_db.get_viewData(sql, "Balance");
    }


    public DataTable get_Payable_Year_Semister(String sid, string prevSem, string P_Year)
    {
        String sql = " select NVL(PAYABLE,0) PAYABLE from T_STUDENTBALANCE where SID ='" + sid + "' and  SEMESTER='" + prevSem + "' and YEAR = '" + P_Year + "'  ";
        //String sql = "select NVL((Sum(PAYABLE)- sum(PAID)),0) PAYABLE  from T_STUDENTBALANCE where SID ='" + sid + "'  and  SEMESTER||YEAR <='" + sem + "'||'" + year + "'  ";
        return obj_db.get_viewData(sql, "PAYABLE");
    }

    public DataTable get_AdmitCard_Balance(String sid, string Sem, string Year)
    {
        String sql = @" select C.*, netFirstSemPayable-totalPiad firstSemDue from (select R.*,(tutionFeePayable-waiverOnTutionFee) totalTutionFeePayable,(tutionFeePayable-waiverOnTutionFee)/3 firstSemTutionPayable,
                    totalPayable+waiverOnTutionFee-tutionFeePayable semesterRegistrationFee, ((tutionFeePayable-waiverOnTutionFee)/3+(totalPayable+waiverOnTutionFee-tutionFeePayable)) netFirstSemPayable
                    from (select SCR.SID, SCR.AMOUNT, SCR.ADJUSTMENT, STD.AMOUNT regfee,NVL(((SCR.AMOUNT-STD.AMOUNT)*LoanW.waiver)/100,0) waiverOnTutionFee,
                    SCR.AMOUNT-STD.AMOUNT tutionFeePayable, STB.PAYABLE totalPayable, (STB.PAID-Prevdues.Prev_due) totalPiad, Prevdues.Prev_due
                    from StudentCredit SCR left join Studentdebit STD on STD.SID= SCR.SID
                    left join (select SID, waiver,YEAR,SEMESTER from LOANSANDWAIVER)LoanW on (LoanW.SID= SCR.SID and LoanW.YEAR=SCR.YEAR and LoanW.SEMESTER=SCR.SEMESTER)
                    left join (select SID,PAYABLE,PAID,YEAR,SEMESTER from T_STUDENTBALANCE )STB on (STB.SID= SCR.SID and STB.YEAR=SCR.YEAR and STB.SEMESTER=SCR.SEMESTER)
                    left join (select SID, case when count(SID) > 1 then (select NVL(sum(payable)-sum(Paid),0) Prev_due from T_studentbalance where 
                    (YEAR||Semester != '" + Year + "'||'" + Sem + "' and YEAR||Semester < '" + Year + "'||'" + Sem + "') and SID = '" + sid + "' ) else 0 end Prev_due from T_studentbalance where SID = '" + sid + "' group by SID )Prevdues on Prevdues.SID = SCR.SID where STD.HEADSN = 2 and SCR.SID='" + sid + "' and STD.YEAR= '" + Year + "' and STD.SEMESTER='" + Sem + "' and SCR.YEAR = '" + Year + "' and SCR.SEMESTER='" + Sem + "')R)C  ";
                                                    //String sql = "select NVL((Sum(PAYABLE)- sum(PAID)),0) PAYABLE  from T_STUDENTBALANCE where SID ='" + sid + "'  and  SEMESTER||YEAR <='" + sem + "'||'" + year + "'  ";
        return obj_db.get_viewData(sql, "AdmitCard_Balance");
    }

    public DataTable get_AdmitCard_BalanceNew(String sid, string Sem, string Year)
    {
        String sql = @" select C.*, netFirstSemPayable-totalPiad firstSemDue from (select R.*,(tutionFeePayable-waiverOnTutionFee) totalTutionFeePayable,(tutionFeePayable-waiverOnTutionFee)/3 firstSemTutionPayable,
                        totalPayable+waiverOnTutionFee-tutionFeePayable semesterRegistrationFee, ((tutionFeePayable-waiverOnTutionFee)/3+(totalPayable+waiverOnTutionFee-tutionFeePayable)) netFirstSemPayable
                        from (select SCR.SID, SCR.AMOUNT, SCR.ADJUSTMENT, STD.AMOUNT regfee,NVL(((SCR.AMOUNT-STD.AMOUNT)*LoanW.waiver)/100,0) waiverOnTutionFee,
                        SCR.AMOUNT-STD.AMOUNT tutionFeePayable, STB.PAYABLE totalPayable, (STB.PAID-Prevdues.Prev_due) totalPiad, Prevdues.Prev_due
                        from StudentCredit SCR left join Studentdebit STD on STD.SID= SCR.SID left join (select SID, waiver,YEAR,SEMESTER from LOANSANDWAIVER)LoanW on (LoanW.SID= SCR.SID and LoanW.YEAR=SCR.YEAR and LoanW.SEMESTER=SCR.SEMESTER)
                        left join (select SID,PAYABLE,PAID,YEAR,SEMESTER from T_STUDENTBALANCE )STB on (STB.SID= SCR.SID and STB.YEAR=SCR.YEAR and STB.SEMESTER=SCR.SEMESTER)
                        left join (select SID, case when count(SID) > 1 then (select sum(payable)-sum(Paid) Prev_due from T_studentbalance where 
                        (YEAR||Semester != NVL((select max(YEAR||Semester) from T_studentbalance where SID = '" + sid + "'  ),0)) and SID = '" + sid + "'  ) else 0 end Prev_due from T_studentbalance where SID = '" + sid + "'  group by SID )Prevdues on Prevdues.SID = SCR.SID where STD.HEADSN = 2 and SCR.SID='" + sid + "'  and STD.YEAR= '" + Year + "' and STD.SEMESTER='" + Sem + "' and SCR.YEAR = '" + Year + "' and SCR.SEMESTER='" + Sem + "')R)C  ";
        //String sql = "select NVL((Sum(PAYABLE)- sum(PAID)),0) PAYABLE  from T_STUDENTBALANCE where SID ='" + sid + "'  and  SEMESTER||YEAR <='" + sem + "'||'" + year + "'  ";
        return obj_db.get_viewData(sql, "AdmitCard_BalanceN");
    }

    public DataTable get_IntallmentDate(string Sem, string Year)
    {
        String sql = @" select * from ADMINREGISTRATIONRATE 
                        where YEAR = '" + Year + "' and SEMESTER = '" + Sem + "'  ";

        return obj_db.get_viewData(sql, "Per_IntallmentDate");
    }


    public DataTable get_LeaveBalance(string EmployeeID, string Leave_ID, string tableName)
    {

        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"SELECT   LD.NAME, LD.LEAVE_ID, TS.VALUE, TS.STAFF_NAME,NVL(HB.BALANCE,LD.BALANCE) TOTAL_BALANCE,TS.JOB_DESIGNATION,
                            NVL(SUM( (TO_DATE- FROM_DATE )+1 ),0) TAKEN,
                            NVL(HB.BALANCE,LD.BALANCE) - NVL(SUM( (TO_DATE-FROM_DATE)+1 ),0) REMAINING_BALANCE     
                            FROM 
                            HR_LEAVE_MASTER LD 
                            JOIN WEB_TEACHER_STAFF TS ON 1 = 1  
                            LEFT JOIN HR_STAFF_LEAVE_INFO HLI ON (TS.VALUE = HLI.STAFF_ID AND LD.LEAVE_ID = HLI.LEAVE_ID  )
                            AND ((TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(FROM_DATE, 'YYYY') AND TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(TO_DATE, 'YYYY')) 
                            OR TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(TO_DATE, 'YYYY') )
                            LEFT JOIN HR_LEAVE_BALANCE HB ON TS.VALUE = HB.EMPLOYEE_ID AND TO_CHAR(SYSDATE,'YYYY') = HB.YEAR
                            WHERE
                            TS.VALUE =?  AND LD.LEAVE_ID =?  GROUP BY LD.NAME, LD.LEAVE_ID, TS.VALUE, TS.STAFF_NAME, LD.BALANCE, TS.JOB_DESIGNATION, HB.BALANCE";

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, EmployeeID, Leave_ID, tableName);

        return ds;


      /*  String sql = @" SELECT   LD.NAME, LD.LEAVE_ID, TS.VALUE, TS.STAFF_NAME,NVL(HB.BALANCE,LD.BALANCE) TOTAL_BALANCE,TS.JOB_DESIGNATION,
                            NVL(SUM( (TO_DATE- FROM_DATE )+1 ),0) TAKEN,
                            NVL(HB.BALANCE,LD.BALANCE) - NVL(SUM( (TO_DATE-FROM_DATE)+1 ),0) REMAINING_BALANCE     
                            FROM 
                            HR_LEAVE_MASTER LD 
                            JOIN WEB_TEACHER_STAFF TS ON 1 = 1  
                            LEFT JOIN HR_STAFF_LEAVE_INFO HLI ON (TS.VALUE = HLI.STAFF_ID AND LD.LEAVE_ID = HLI.LEAVE_ID  )
                            AND ((TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(FROM_DATE, 'YYYY') AND TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(TO_DATE, 'YYYY')) 
                            OR TO_CHAR(SYSDATE, 'YYYY') = TO_CHAR(TO_DATE, 'YYYY') )
                            LEFT JOIN HR_LEAVE_BALANCE HB ON TS.VALUE = HB.EMPLOYEE_ID AND TO_CHAR(SYSDATE,'YYYY') = HB.YEAR
                            WHERE
                            TS.VALUE ='" + EmployeeID + "'  AND LD.LEAVE_ID ='" + Leave_ID + "'  GROUP BY LD.NAME, LD.LEAVE_ID, TS.VALUE, TS.STAFF_NAME, LD.BALANCE, TS.JOB_DESIGNATION, HB.BALANCE  ";

        return obj_db.get_viewData(sql, "LeaveBalance");*/
    }

    public DataTable get_FN_GET_PER_SEM_DUE(string SID, string Year,string Sem)
    {
        String sql = @" select FN_GET_PER_SEM_DUE('" + SID + "', '" + Year + "','" + Sem + "') DUE from DUAL ";
        return obj_db.get_viewData(sql, "SEM_DUE");
    }


    public DataTable get_AdmitCardList(string YearSem, string ExamType, string College, string Program, string section, string coursekey, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct ADPRNT.SID, S.SNAME, C_PROGINDEPT.NAME Program,substr(OFFERERINGANDGRADE.COURSEKEY, 6) COURSE, OFFERERINGANDGRADE.GGROUP, ''  SIGN,
                              YEAR, CASE   WHEN SEM = 1   THEN 'Spring'   WHEN SEM = 2   THEN 'Summer'   WHEN SEM = 3   THEN 'Fall' END AS SEMESTER,CASE   WHEN EXAM_TYPE = 'M'   THEN 'Mid'   WHEN EXAM_TYPE = 'F'   THEN 'Final' END AS EXAMTYPE
                                from E_ADMITCARD_HIST ADPRNT left join Student S on ADPRNT.SID = S.SID
                                left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID
                                left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID =  C_PROGINDEPT.DEPID
                                left join OFFERERINGANDGRADE on OFFERERINGANDGRADE.REGKEY = ADPRNT.SID||SEM||YEAR
                                where SEM||YEAR = ?
                                and EXAM_TYPE = ? and  OFFERERINGANDGRADE.COURSEKEY = ? and OFFERERINGANDGRADE.GGROUP =?
                                and  C_DEPARTMENTINFACULTY.COLLEGECODE = ?
                                and  C_PROGINDEPT.C_PROGINDEPT_ID = ?  order by C_PROGINDEPT.NAME asc, ADPRNT.SID asc";

        ds = obj_db.Table_GetAll_PROGWISE(query.CommandText, YearSem, ExamType, coursekey, section, College, Program, tableName);

        return ds;
    }


    public DataTable get_AdmitCardList(string YearSem, string ExamType, string Program, string CourseKey, string section, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct ADPRNT.SID, S.SNAME, C_PROGINDEPT.NAME Program, substr(OFFERERINGANDGRADE.COURSEKEY, 6) COURSE, OFFERERINGANDGRADE.GGROUP, ''  SIGN,
          YEAR, CASE   WHEN SEM = 1   THEN 'Spring'   WHEN SEM = 2   THEN 'Summer'   WHEN SEM = 3   THEN 'Fall' END AS SEMESTER,CASE   WHEN EXAM_TYPE = 'M'   THEN 'Mid'   WHEN EXAM_TYPE = 'F'   THEN 'Final' END AS EXAMTYPE from E_ADMITCARD_HIST ADPRNT
                                left join Student S on ADPRNT.SID = S.SID
                                left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID
                                left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID =  C_PROGINDEPT.DEPID
                                left join OFFERERINGANDGRADE on OFFERERINGANDGRADE.REGKEY = ADPRNT.SID||SEM||YEAR
                                where SEM||YEAR = ?
                                and EXAM_TYPE = ? and  OFFERERINGANDGRADE.COURSEKEY = ? and OFFERERINGANDGRADE.GGROUP=?
                                and  C_PROGINDEPT.C_PROGINDEPT_ID = ?  order by C_PROGINDEPT.NAME asc, ADPRNT.SID asc";

        ds = obj_db.Table_GetAll_PROGWISENew(query.CommandText, YearSem, ExamType, CourseKey, section, Program, tableName);

        return ds;
    }

    public DataTable get_AdmitCardListProgDepwise(string YearSem, string ExamType, string CourseKey, string section, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct ADPRNT.SID, S.SNAME, C_PROGINDEPT.NAME Program, substr(OFFERERINGANDGRADE.COURSEKEY, 6) COURSE, OFFERERINGANDGRADE.GGROUP, ''  SIGN,
                              YEAR, CASE   WHEN SEM = 1   THEN 'Spring'   WHEN SEM = 2   THEN 'Summer'   WHEN SEM = 3   THEN 'Fall' END AS SEMESTER,CASE   WHEN EXAM_TYPE = 'M'   THEN 'Mid'   WHEN EXAM_TYPE = 'F'   THEN 'Final' END AS EXAMTYPE from E_ADMITCARD_HIST ADPRNT
                                left join Student S on ADPRNT.SID = S.SID
                                left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID
                                left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID =  C_PROGINDEPT.DEPID
                                left join OFFERERINGANDGRADE on OFFERERINGANDGRADE.REGKEY = ADPRNT.SID||SEM||YEAR
                                where SEM||YEAR = ? and OFFERERINGANDGRADE.COURSEKEY = ? and OFFERERINGANDGRADE.GGROUP=?
                                and EXAM_TYPE = ? order by C_PROGINDEPT.NAME asc, ADPRNT.SID asc";

        ds = obj_db.Table_GetAll_DEPWISE(query.CommandText, YearSem, CourseKey, section, ExamType, tableName);

        return ds;
    }


    public DataTable get_AdmitCardListALL(string YearSem, string ExamType, string College, string Coursekey, string Section, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct ADPRNT.SID, S.SNAME, C_PROGINDEPT.NAME Program, substr(OFFERERINGANDGRADE.COURSEKEY, 6) COURSE, OFFERERINGANDGRADE.GGROUP,''  SIGN,
 YEAR, CASE   WHEN SEM = 1   THEN 'Spring'   WHEN SEM = 2   THEN 'Summer'   WHEN SEM = 3   THEN 'Fall' END AS SEMESTER,CASE   WHEN EXAM_TYPE = 'M'   THEN 'Mid'   WHEN EXAM_TYPE = 'F'   THEN 'Final' END AS EXAMTYPE from E_ADMITCARD_HIST ADPRNT
                                left join Student S on ADPRNT.SID = S.SID
                                left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID
                                left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID =  C_PROGINDEPT.DEPID
                                left join OFFERERINGANDGRADE on OFFERERINGANDGRADE.REGKEY = ADPRNT.SID||SEM||YEAR
                                where SEM||YEAR = ?
                                and EXAM_TYPE = ? and  OFFERERINGANDGRADE.COURSEKEY  =? and OFFERERINGANDGRADE.GGROUP =?
                                and  C_DEPARTMENTINFACULTY.COLLEGECODE = ? order by C_PROGINDEPT.NAME asc, ADPRNT.SID asc";

        ds = obj_db.Table_GetAll_PROGWISENew(query.CommandText, YearSem, ExamType, Coursekey, Section, College, tableName);

        return ds;
    }

    public DataTable get_AdmitCardListGED(string YearSem, string ExamType, string Coursekey, string Section, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct ADPRNT.SID, S.SNAME, C_PROGINDEPT.NAME Program, substr(OFFERERINGANDGRADE.COURSEKEY, 6) COURSE, OFFERERINGANDGRADE.GGROUP,''  SIGN,
                                YEAR, CASE   WHEN SEM = 1   THEN 'Spring'   WHEN SEM = 2   THEN 'Summer'   WHEN SEM = 3   THEN 'Fall' END AS SEMESTER,CASE   WHEN EXAM_TYPE = 'M'   THEN 'Mid'   WHEN EXAM_TYPE = 'F'   THEN 'Final' END AS EXAMTYPE from E_ADMITCARD_HIST ADPRNT
                                left join Student S on ADPRNT.SID = S.SID
                                left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = S.PROGRAM_ID
                                left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID =  C_PROGINDEPT.DEPID
                                left join OFFERERINGANDGRADE on OFFERERINGANDGRADE.REGKEY = ADPRNT.SID||SEM||YEAR
                                where SEM||YEAR = ?
                                and EXAM_TYPE = ? and  OFFERERINGANDGRADE.COURSEKEY  =? and OFFERERINGANDGRADE.GGROUP =?
                                order by C_PROGINDEPT.NAME asc, ADPRNT.SID asc";

        ds = obj_db.Table_GetAll_DEPWISE(query.CommandText, YearSem, ExamType, Coursekey, Section, tableName);

        return ds;
    }

    public DataTable get_RegisteredStudentsProgwise(string Program, string SemesterYear, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct R.SID, Initcap(Sname) Sname, S.PHONE, R.YEAR, R.SEMESTER, S.PROGRAM_ID,
                            C_PROGINDEPT.NAME,COLLEGE.COLLEGECODE, COLLEGE.COLLEGENAME,CASE WHEN R.SEMESTER = 1 THEN 'Spring'
                            WHEN R.SEMESTER = 2  THEN 'Summer'  WHEN R.SEMESTER = 3  THEN 'Fall'  END AS SemesterName,
                            Case When S.TR_CR_ELIGIBLE  = 1 then NVL(TCGPA,0) ELSE NVL(CGPA,0) END as FINAL_CGPA,
                            NVL(VW_GET_CGPA_F.COMP_CHRS,0) COMP_CHRS,
                            CASE WHEN CNGCH.REQCREDITHRS is not null THEN cngch.reqcredithrs   
                            WHEN CNGCH.REQCREDITHRS is null THEN TO_CHAR(C_PROGINDEPT.REQCREDITHRS) 
                            END reqCH   from REGISTATUS R
                                            left join student S on S.SID =R.SID
                                            left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID=S.PROGRAM_ID
                                            left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID=C_PROGINDEPT.DEPID
                                            left join college on COLLEGE.COLLEGECODE = C_DEPARTMENTINFACULTY.COLLEGECODE
                                            left join VW_GET_CGPA_F on VW_GET_CGPA_F.SID = S.SID 
                                            left join CHANGE_REQCREDITHRS  CNGCH on  (C_PROGINDEPT.C_PROGINDEPT_ID=CNGCH.DEPID  and CNGCH.BATCH = SUBSTR(S.SID, 1,3) ) 
                            where  S.PROGRAM_ID =? and SUBSTR(REGKEY, 10) =?
                            order by COLLEGE.COLLEGECODE asc , S.PROGRAM_ID asc, R.SID asc";

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, Program, SemesterYear, tableName);

        return ds;
    }
    public DataTable get_RegisteredStudentsDeptwise(string Collegecode, string Semester, string Year, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct R.SID, Initcap(Sname) Sname, S.PHONE, R.YEAR, R.SEMESTER, S.PROGRAM_ID,
                            C_PROGINDEPT.NAME,COLLEGE.COLLEGECODE, COLLEGE.COLLEGENAME,CASE WHEN R.SEMESTER = 1 THEN 'Spring'
                            WHEN R.SEMESTER = 2  THEN 'Summer'  WHEN R.SEMESTER = 3  THEN 'Fall'  END AS SemesterName,
                            Case When S.TR_CR_ELIGIBLE  = 1 then NVL(TCGPA,0) ELSE NVL(CGPA,0) END as FINAL_CGPA,
                            NVL(VW_GET_CGPA_F.COMP_CHRS,0) COMP_CHRS,
                            CASE WHEN CNGCH.REQCREDITHRS is not null THEN cngch.reqcredithrs   
                            WHEN CNGCH.REQCREDITHRS is null THEN TO_CHAR(C_PROGINDEPT.REQCREDITHRS) 
                            END reqCH   from REGISTATUS R
                                            left join student S on S.SID =R.SID
                                            left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID=S.PROGRAM_ID
                                            left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID=C_PROGINDEPT.DEPID
                                            left join college on COLLEGE.COLLEGECODE = C_DEPARTMENTINFACULTY.COLLEGECODE
                                            left join VW_GET_CGPA_F on VW_GET_CGPA_F.SID = S.SID 
                                            left join CHANGE_REQCREDITHRS  CNGCH on  (C_PROGINDEPT.C_PROGINDEPT_ID=CNGCH.DEPID  and CNGCH.BATCH = SUBSTR(S.SID, 1,3) ) 
                            where  COLLEGECODE =? and REGKEY like '%'||?||?
                            order by COLLEGE.COLLEGECODE asc , S.PROGRAM_ID asc, R.SID asc";

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, Collegecode, Semester, Year, tableName);

        return ds;
    }

    public DataTable get_StudentInfo(string SID, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"SELECT distinct 
                      S.SID, S.SNAME, S.SPROGRAM, 
                      S.DOB, S.PLACEOFBIRTH, S.GENDER, S.MARITIALSTATUS,
                      S.NATIONALITY, S.BLOODGROUP, CASE WHEN S.GENDER = 'M' THEN 'Male' WHEN S.GENDER = 'F'  THEN 'Female'  END AS GENDERDTL,
                      S.ADMINYEAR, S.ADMINSEMETER, S.ADMISSION_DATE,
                      CASE WHEN S.ADMINSEMETER = 1 THEN 'Spring' WHEN S.ADMINSEMETER = 2  THEN 'Summer'  WHEN S.ADMINSEMETER = 3  THEN 'Fall'  END AS ADMINSEMETER_Name,
                      '~\student\profile\student_images\'||S.SID||'.'||SUBSTR(S_PICTURE,INSTR(S_PICTURE,'.',-1)+ 1)  PHOTO_LOCATION, S.PHOTO, S.S_PICTURE,  S.PHONE,
                      S.ADDRESS, S.FAX, S.EMAIL, S.COMMENTS,S.STATUS, 
                      S.GRADUATIONYEAR, S.GRADUATIONSEMESTER,  S.GRADUATIONDATE,
                      CASE WHEN S.GRADUATIONSEMESTER = 1 THEN 'Spring' WHEN S.GRADUATIONSEMESTER = 2  THEN 'Summer'  WHEN S.GRADUATIONSEMESTER = 3  THEN 'Fall'  END AS GRSEMETER_Name,
                      S.MAJOR, S.ADVISOR_ID,WEB_TEACHER_STAFF.VALUE AdvisorID, S.PROGRAM_ID, WEB_TEACHER_STAFF.STAFF_NAME ADVISOR_NAME,
                      PRG.NAME,SPR.SPADDRESS, SEMG.SEPHONE,
                      STF.SFNAME, STF.SFADD, STF.SFE_MAIL, 
                      STF.SFFAX, STF.SFOCCUPATION, STF.SFPHONE,
                      STM.SMNAME, STM.SMADD, STM.SME_MAIL, 
                      STM.SMFAX, STM.SMOCCUPATION, STM.SMPHONE,
                      HSC.COLLEGE, HSC.HBOARD,HSC.HGPA, 
                      HSC.HGROUP, HSC.HPASSYEAR, HSC.HTMARKS,
                      SSC.SCHOOL, SSC.SBOARD, SSC.SGPA, 
                      SSC.SGROUP, SSC.SPASSYEAR, SSC.STMARKS,
                      ACBK.BUNIVERSITY, ACBK.BCLASS, ACBK.BMARKS, 
                      ACBK.BYEAROFPASSING, ACBK.BDEGREE,
                      ACBK.MUNIVERSITY, ACBK.MCLASS, ACBK.MMARKS, 
                      ACBK.MYEAROFPASSING, ACBK.MDEGREE,
                      Case When S.TR_CR_ELIGIBLE  = 1 then NVL(TCGPA,0) ELSE NVL(CGPA,0) END as FINAL_CGPA,
                      NVL(VW_GET_CGPA_F.COMP_CHRS,0) COMP_CHRS,
                      CASE WHEN CNGCH.REQCREDITHRS IS NOT NULL THEN CNGCH.REQCREDITHRS  
                      WHEN CNGCH.REQCREDITHRS IS NULL THEN TO_CHAR(PRG.REQCREDITHRS) END REQCR
                        FROM STUDENT S 
                        LEFT JOIN VW_GET_CGPA_F ON VW_GET_CGPA_F.SID = S.SID
                        LEFT JOIN ACADEMICBACK_HSC HSC ON HSC.SID =S.SID
                        LEFT JOIN ACADEMICBACK_SSC SSC ON SSC.SID = S.SID
                        LEFT JOIN ACADEMICBACK_OTHERS ACBK ON ACBK.SID = S.SID
                        LEFT JOIN STUDFATHERDET STF ON STF.SID = S.SID
                        LEFT JOIN STUDMOTHERDET STM ON STM.SID =S.SID
                        LEFT JOIN STUDENTPERMANENTADD SPR ON SPR.SID = S.SID
                        LEFT JOIN C_PROGINDEPT PRG ON PRG.C_PROGINDEPT_ID = S.PROGRAM_ID
                        LEFT JOIN STUDEMERGENCYCONTACTADD SEMG ON SEMG.SID=S.SID
                        LEFT JOIN WEB_TEACHER_STAFF ON WEB_TEACHER_STAFF.STAFF_ID = S.ADVISOR_ID
                        left join CHANGE_REQCREDITHRS  CNGCH on  (PRG.C_PROGINDEPT_ID=CNGCH.DEPID  and CNGCH.BATCH = SUBSTR(S.SID, 1,3) )
                        WHERE S.SID = ? ";

        ds = obj_db.Table_GetAll_Departmental(query.CommandText, SID, tableName);

        return ds;
    }

    public DataTable get_STUDENT_Payment(string TRAN_ID, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select T_STUDENTDEBIT.*,Initcap(Student.sname) sname,CASE T_STUDENTDEBIT.semester
                        WHEN 1 THEN 'Spring' WHEN 2 THEN 'Summer'  WHEN 3 THEN 'Fall' END AS semistername,
                        STUDENTHEADINFO.HEADNAME from T_STUDENTDEBIT inner join STUDENTHEADINFO on STUDENTHEADINFO.headsn = T_STUDENTDEBIT.HEADSN 
                        left join Student on student.sid = T_STUDENTDEBIT.SID
                        where TRANID =  ?
                        order by STUDENTHEADINFO.SERIAL asc";


        ds = obj_db.Table_GetAll_Departmental(query.CommandText, TRAN_ID, tableName);

        return ds;

    }
    public DataTable get_RegisteredStudents(string Semester, string Year, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct R.SID, Initcap(Sname) Sname, S.PHONE, R.YEAR, R.SEMESTER, S.PROGRAM_ID,
                            C_PROGINDEPT.NAME,COLLEGE.COLLEGECODE, COLLEGE.COLLEGENAME,CASE WHEN R.SEMESTER = 1 THEN 'Spring'
                            WHEN R.SEMESTER = 2  THEN 'Summer'  WHEN R.SEMESTER = 3  THEN 'Fall'  END AS SemesterName,
                            Case When S.TR_CR_ELIGIBLE  = 1 then NVL(TCGPA,0) ELSE NVL(CGPA,0) END as FINAL_CGPA,
                            NVL(VW_GET_CGPA_F.COMP_CHRS,0) COMP_CHRS,
                            CASE WHEN CNGCH.REQCREDITHRS is not null THEN cngch.reqcredithrs   
                            WHEN CNGCH.REQCREDITHRS is null THEN TO_CHAR(C_PROGINDEPT.REQCREDITHRS) 
                            END reqCH   from REGISTATUS R
                                            left join student S on S.SID =R.SID
                                            left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID=S.PROGRAM_ID
                                            left join C_DEPARTMENTINFACULTY on C_DEPARTMENTINFACULTY.DEPID=C_PROGINDEPT.DEPID
                                            left join college on COLLEGE.COLLEGECODE = C_DEPARTMENTINFACULTY.COLLEGECODE
                                            left join VW_GET_CGPA_F on VW_GET_CGPA_F.SID = S.SID 
                                            left join CHANGE_REQCREDITHRS  CNGCH on  (C_PROGINDEPT.C_PROGINDEPT_ID=CNGCH.DEPID  and CNGCH.BATCH = SUBSTR(S.SID, 1,3) ) 
                                            where REGKEY like '%'||?||?
                                            order by COLLEGE.COLLEGECODE asc , S.PROGRAM_ID asc, R.SID asc";

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, Semester, Year, tableName);

        return ds;
    }
    public DataTable get_AttendanceSheet(string CourseKey, string Section, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"SELECT  SUBSTR(REGKEY,1,9) SID, Initcap(S.SNAME) SNAME,S.PHONE,Initcap(S.SNAME||' - '||S.PHONE) Name_CONTACT ,OFFERERINGANDGRADE.COURSEKEY, GGROUP,WEB_COURSE_TEACHER.COURSE_TEACHER_ID,TEACHER_ID,WEB_TEACHER_STAFF.STAFF_NAME,
                    SUBSTR(OFFERERINGANDGRADE.COURSEKEY,6) COURSENAME,CHANGEDCOURSENAME.CNAME,
                    SUBSTR(OFFERERINGANDGRADE.COURSEKEY,1,1) SemesterID,
                    CASE
                    WHEN SUBSTR(OFFERERINGANDGRADE.COURSEKEY,1,1) = 1
                    THEN 'Spring'
                    WHEN SUBSTR(OFFERERINGANDGRADE.COURSEKEY,1,1) = 2
                    THEN 'Summer'
                    WHEN SUBSTR(OFFERERINGANDGRADE.COURSEKEY,1,1) = 3
                    THEN 'Fall'
                    END AS Semester,
                    SUBSTR(OFFERERINGANDGRADE.COURSEKEY,2,4) YEAR,
                    Case When S.TR_CR_ELIGIBLE  = 1 then NVL(TCGPA,0) ELSE NVL(CGPA,0) END as FINAL_CGPA
                    FROM OFFERERINGANDGRADE 
                    left join Student S on S.SID =SUBSTR(REGKEY,1,9)
                    left join VW_GET_CGPA_F on VW_GET_CGPA_F.SID = S.SID
                    left join WEB_COURSE_TEACHER on OFFERERINGANDGRADE.COURSEKEY = WEB_COURSE_TEACHER.COURSE_KEY and WEB_COURSE_TEACHER.SECTION =OFFERERINGANDGRADE.GGROUP
                    left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID =WEB_COURSE_TEACHER.TEACHER_ID
                    left join VIEW_OFFEREDCOURSE on VIEW_OFFEREDCOURSE.COURSEKEY=OFFERERINGANDGRADE.COURSEKEY
                    left join CHANGEDCOURSENAME on CHANGEDCOURSENAME.COURSECODE =VIEW_OFFEREDCOURSE.COURSECODE
                    WHERE  OFFERERINGANDGRADE.COURSEKEY =? and GGROUP=?
                    order by S.SID asc";

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, CourseKey, Section, tableName);

        return ds;
    }
    public DataTable get_AttendanceSheet(string COURSE_TEACHER_ID, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"SELECT DISTINCT SUBSTR(REGKEY,1,9) SID, Initcap(S.SNAME) SNAME,S.PHONE,Initcap(S.SNAME||' - '||S.PHONE) Name_CONTACT ,OFFERERINGANDGRADE.COURSEKEY, GGROUP,WEB_COURSE_TEACHER.COURSE_TEACHER_ID,TEACHER_ID,WEB_TEACHER_STAFF.STAFF_NAME,
                    SUBSTR(OFFERERINGANDGRADE.COURSEKEY,6) COURSENAME,CHANGEDCOURSENAME.CNAME,
                    SUBSTR(OFFERERINGANDGRADE.COURSEKEY,1,1) SemesterID,
                    CASE
                    WHEN SUBSTR(OFFERERINGANDGRADE.COURSEKEY,1,1) = 1
                    THEN 'Spring'
                    WHEN SUBSTR(OFFERERINGANDGRADE.COURSEKEY,1,1) = 2
                    THEN 'Summer'
                    WHEN SUBSTR(OFFERERINGANDGRADE.COURSEKEY,1,1) = 3
                    THEN 'Fall'
                    END AS Semester,
                    SUBSTR(OFFERERINGANDGRADE.COURSEKEY,2,4) YEAR,
                    Case When S.TR_CR_ELIGIBLE  = 1 then NVL(TCGPA,0) ELSE NVL(CGPA,0) END as FINAL_CGPA
                    FROM OFFERERINGANDGRADE 
                    left join Student S on S.SID =SUBSTR(REGKEY,1,9)
                    left join VW_GET_CGPA_F on VW_GET_CGPA_F.SID = S.SID
                    left join WEB_COURSE_TEACHER on OFFERERINGANDGRADE.COURSEKEY = WEB_COURSE_TEACHER.COURSE_KEY and WEB_COURSE_TEACHER.SECTION =OFFERERINGANDGRADE.GGROUP
                    left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID =WEB_COURSE_TEACHER.TEACHER_ID
                    left join VIEW_OFFEREDCOURSE on VIEW_OFFEREDCOURSE.COURSEKEY=OFFERERINGANDGRADE.COURSEKEY
                    left join CHANGEDCOURSENAME on CHANGEDCOURSENAME.COURSECODE =VIEW_OFFEREDCOURSE.COURSECODE
                    WHERE  WEB_COURSE_TEACHER.COURSE_TEACHER_ID =?
                     order by 1,2";

        ds = obj_db.Table_GetAll_Departmental(query.CommandText, COURSE_TEACHER_ID, tableName);

        return ds;
    }

    public DataTable get_AttendanceSheetdtl_Teacherwise(string COURSE_TEACHER_ID, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct
                WT.COURSE_TEACHER_ID, WT.TEACHER_ID,
                WEB_TEACHER_STAFF.VALUE,
                WEB_TEACHER_STAFF.STAFF_NAME, WAT.SID||' - '|| Initcap(S.Sname) STDNAME,
                WAT.SID, Initcap(S.Sname) Sname,S.PHONE ,WAT.class_date,
                WAT.C_ROUTINE_ID,C_ROUTINE.TIME, WAT.class_date   ||' - '||C_ROUTINE.TIME  Class_Time, 
                WAT.ATTEND,
                NVL(VWSTD.tcgpa,0) tcgpa,VWSTD.TakenClass, VWSTD.ATTEND_CLASS, VWSTD.Presentance,
                CASE
                WHEN WAT.ATTEND = 1
                    THEN 'P'
                WHEN WAT.ATTEND = 0
                    THEN 'A'
                END AS ATTEND_STATUS,SUBSTR(WT.COURSE_KEY,2,4) Year,
                 CASE
                WHEN SUBSTR(WT.COURSE_KEY,1,1) = 1 
                    THEN 'Spring'
                WHEN SUBSTR(WT.COURSE_KEY,1,1) = 2
                    THEN 'Summer'
               WHEN SUBSTR(WT.COURSE_KEY,1,1) = 3
                    THEN 'Fall'   
                     END AS Semester,
                WAT.COURSEKEY,SUBSTR(WT.COURSE_KEY,6) COURSE, WAT.SECTION,CHANGEDCOURSENAME.CNAME, COLLEGE.COLLEGENAME,OFFEREDCOURSE.DEPCODE
                from OFFERERINGANDGRADE OC
                left join WEB_STUDENT_ATTENDANCE WAT on OC.COURSEKEY = WAT.COURSEKEY and OC.GGROUP=WAT.SECTION
                left join WEB_COURSE_TEACHER WT on WT.COURSE_TEACHER_ID = WAT.COURSE_TEACHER_ID
                left join C_ROUTINE on C_ROUTINE.C_ROUTINE_ID=WAT.C_ROUTINE_ID
                left join student S on S.SID = WAT.SID
                left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID =WT.TEACHER_ID
                left join COLLEGE on COLLEGE.COLLEGECODE=WEB_TEACHER_STAFF.DEPARTMENT
                left join OFFEREDCOURSE on OFFEREDCOURSE.COURSEKEY=WT.COURSE_KEY   
                left join CHANGEDCOURSENAME on OFFEREDCOURSE.COURSECODE=CHANGEDCOURSENAME.COURSECODE 
                left join WEB_STUDENT_CLSATTENDANCE VWSTD on VWSTD.REGKEY = (WAT.SID||WAT.SEMESTER||WAT.YEAR) and VWSTD.COURSEKEY =WAT.COURSEKEY
                Where WT.COURSE_TEACHER_ID =?
                order by COLLEGE.COLLEGENAME asc,  WAT.class_date asc,WAT.SID asc";

        ds = obj_db.Table_GetAll_Departmental(query.CommandText, COURSE_TEACHER_ID, tableName);

        return ds;
    }
    public DataTable get_AttendanceSheetdtl(string CourseKey, string Section, string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText = @"select distinct
                WT.COURSE_TEACHER_ID, WT.TEACHER_ID,
                WEB_TEACHER_STAFF.VALUE,
                WEB_TEACHER_STAFF.STAFF_NAME, WAT.SID||' - '|| Initcap(S.Sname) STDNAME,
                WAT.SID, Initcap(S.Sname) Sname,S.PHONE ,WAT.class_date,
                WAT.ATTEND,
                CASE
                WHEN WAT.ATTEND = 1
                    THEN 'P'
                WHEN WAT.ATTEND = 0
                    THEN 'A'
                END AS ATTEND_STATUS,
                WAT.COURSEKEY,SUBSTR(WT.COURSE_KEY,6) COURSE, WAT.SECTION,CNAME, COLLEGE.COLLEGENAME
                from OFFERERINGANDGRADE OC
                left join WEB_STUDENT_ATTENDANCE WAT on OC.COURSEKEY = WAT.COURSEKEY and OC.GGROUP=WAT.SECTION
                left join WEB_COURSE_TEACHER WT on WT.COURSE_TEACHER_ID = WAT.COURSE_TEACHER_ID
                left join student S on S.SID = WAT.SID
                left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.STAFF_ID =WT.TEACHER_ID
                left join COLLEGE on COLLEGE.COLLEGECODE=WEB_TEACHER_STAFF.DEPARTMENT
                left join OFFEREDCOURSE on OFFEREDCOURSE.COURSEKEY=WT.COURSE_KEY   
                left join CHANGEDCOURSENAME on OFFEREDCOURSE.COURSECODE=CHANGEDCOURSENAME.COURSECODE 
                Where WT.COURSE_KEY=? and WAT.SECTION=?
                order by COLLEGE.COLLEGENAME asc,  WAT.class_date asc,WAT.SID asc";

        ds = obj_db.Table_GetAll_EXMWISE(query.CommandText, CourseKey, Section, tableName);

        return ds;
    }
    public DataTable get_GET_PER_SEM_DUE(string SID, string Year, string Sem)
    {
        String sql = @" select TBL.SID, sum(TBL.PAYABLE)-sum(TBL.PAID) due from T_STUDENTBALANCE TBL where TBL.YEAR||TBL.SEMESTER <='" + Year + "'||'" + Sem + "' and TBL.SID ='" + SID + "' group by TBL.SID";
        return obj_db.get_viewData(sql, "PER_SEM_DUE");
    }

    public DataTable get_PrevDues_Balance(String sid)
    {
        String sql = @" select sum(payable)-sum(Paid) Prev_due from T_studentbalance 
                        where YEAR||Semester !=(select max(YEAR||Semester) from T_studentbalance
                        where SID = '" + sid + "') and SID = '" + sid + "'  ";
        
        return obj_db.get_viewData(sql, "PrevDues_Balance");
    }


    public DataTable get_AdmitCard_SeconIns_Balance(String sid, string Sem, string Year)
    {
        String sql = @" select C.*, totalPiad-netFirstSemPayable firstSemDue
                        from (select R.*,(tutionFeePayable-waiverOnTutionFee) totalTutionFeePayable,(tutionFeePayable-waiverOnTutionFee)/3 firstSemTutionPayable,
                        totalPayable+waiverOnTutionFee-tutionFeePayable semesterRegistrationFee, 
                        ((tutionFeePayable-waiverOnTutionFee)/3)+(totalPayable+waiverOnTutionFee-tutionFeePayable) netFirstSemPayable
                        from (select SCR.SID, SCR.AMOUNT, SCR.ADJUSTMENT, STD.AMOUNT regfee, NVL(((SCR.AMOUNT-STD.AMOUNT)*LoanW.waiver)/100,0) waiverOnTutionFee,
                        SCR.AMOUNT-STD.AMOUNT tutionFeePayable, STB.PAYABLE totalPayable, STB.PAID totalPiad
                        from StudentCredit SCR left join Studentdebit STD on STD.SID= SCR.SID
                        left join (select SID, waiver,YEAR,SEMESTER from LOANSANDWAIVER)LoanW on (LoanW.SID= SCR.SID and LoanW.YEAR=SCR.YEAR and LoanW.SEMESTER=SCR.SEMESTER)
                        left join (select SID,PAYABLE,PAID,YEAR,SEMESTER from T_STUDENTBALANCE )STB on (STB.SID= SCR.SID and STB.YEAR=SCR.YEAR and STB.SEMESTER=SCR.SEMESTER)
                        where STD.HEADSN = 2 and SCR.SID='" + sid + "' and STD.YEAR= '" + Year + "' and STD.SEMESTER='" + Sem + "' and SCR.YEAR = '" + Year + "' and SCR.SEMESTER='" + Sem + "' )R)C  ";
        //String sql = "select NVL((Sum(PAYABLE)- sum(PAID)),0) PAYABLE  from T_STUDENTBALANCE where SID ='" + sid + "'  and  SEMESTER||YEAR <='" + sem + "'||'" + year + "'  ";
        return obj_db.get_viewData(sql, "AdmitCard_Balance");
    }

    //public DataTable get_duePayable_Year_Semister(String sid, string prevSem, string P_Year)
    //{
    //    // String sql = " select NVL(PAYABLE,0) PAYABLE from T_STUDENTBALANCE where SID ='" + sid + "' and  SEMESTER='" + prevSem + "' and YEAR = '" + P_Year + "'  ";
    //    String sql = "select NVL((Sum(PAYABLE)- sum(PAID)),0) PAYABLE  from T_STUDENTBALANCE where SID ='" + sid + "'  and  SEMESTER||YEAR <='" + prevSem + "'||'" + P_Year + "'  ";
    //    return obj_db.get_viewData(sql, "PAYABLE");
    //}

    public double GetCurrentSemTotalTutionFeepayment(String sid, string sem, string year)
    {
        double totalPaid = 0;
      
        String que = "";
        DataTable dt = new DataTable();
        int pAmount = 0;        
        try
        {
            que = "Select sum(AMOUNT) as tsum from STUDENTDEBIT where SID='" + sid + "' and  YEAR='" + year + "' and SEMESTER='" + sem + "' and (headsn=5 or headsn=6 or headsn=7 or headsn=8) ";
            dt.Merge(obj_db.get_viewData(que, "temp"));
            if (dt.Rows.Count > 0)
            {
                totalPaid=double.Parse(dt.Rows[0]["tsum"].ToString());
            }
        }
        catch  { }

        return totalPaid;
    }
    public double GetCurrentSemTotalPayment(String sid, string sem, string year)
    {
        double totalPaid = 0;

        String que = "";
        DataTable dt = new DataTable();
        int pAmount = 0;
        try
        {
            que = "Select sum(AMOUNT) as tsum from STUDENTDEBIT where SID='" + sid + "' and  YEAR='" + year + "' and SEMESTER='" + sem + "'";
            dt.Merge(obj_db.get_viewData(que, "temp"));
            if (dt.Rows.Count > 0)
            {
                totalPaid = double.Parse(dt.Rows[0]["tsum"].ToString());
            }
        }
        catch { }

        return totalPaid;
    }
    public  bool isPaid(String sid, String year, String semester, String headid)
    {
        DataTable dt = new DataTable();
        String stmt = "";
        stmt = "Select MRNO From STUDENTDEBIT Where " +
        "SID='" + sid + "' and HEADSN='" + headid + "' and YEAR='" + year + "' and SEMESTER='" + semester + "'";
        try
        {
            dt.Merge(obj_db.get_viewData(stmt, "temp"));
            stmt = "";
            if (dt.Rows.Count > 0)
            {
                stmt = dt.Rows[0]["MRNO"].ToString(); ;
            }
        }
        catch (Exception exp)
        { 
            string str = exp.ToString(); 
        }
        if (stmt.Trim() == "")
        {
            return false;
        }
        else return true;
    }
    public  double GetTotalPaidOfThisHead(String id, int year, int sem, String headId)
    {
        DataTable dt = new DataTable();
        String que = "";
        double pAmount = 0;

        try
        {
            que = "Select sum(AMOUNT) as tsum from STUDENTDEBIT where SID='" + id + "' and  YEAR='" + year + "' and SEMESTER='" + sem + "' and headsn='" + headId + "' ";
            dt.Merge(obj_db.get_viewData(que, "temp"));
            if (dt.Rows.Count > 0)
            {
                pAmount = double.Parse(dt.Rows[0]["tsum"].ToString()) ;
            }           
        }
        catch{ }

        return pAmount;
    }
    public  double GetTotalPaidOfThisHead(string id, string year, string sem, string headId)
    {
        return GetTotalPaidOfThisHead(id, int.Parse(year), int.Parse(sem), headId);
    }


    public int get_Status(string sid, string sem, string year)
    {
        int Status = 0;

        String sql = "SELECT (CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END) AS STATUS FROM REGISTATUS WHERE YEAR=" + year + " and  SEMESTER=" + sem + " " + " and SID='" + sid + "'";

        DataTable dt = obj_db.get_viewData(sql, "REGISTATUS");
        if (dt.DataSet.Tables[0].Rows.Count > 0)
            Status = Convert.ToInt32(dt.DataSet.Tables[0].Rows[0]["STATUS"]);

        return Status;
    }



	public double GetAccountBalance(String sid, String semester, String year)
    {
        DataSet ds = new DataSet();
        ds.Merge(get_allRegistred_semesters_ofA_student_for_account(sid, semester, year));
        ds.Merge(get_all_Debit_semesters_ofA_student(sid, semester, year));

        ds.Tables["registration"].Columns.Add("isRegi");
        foreach (DataRow dr in ds.Tables["registration"].Rows)
        {
            for (int p = 0; p < ds.Tables["DebitSemester"].Rows.Count; p++)
            {
                if ((ds.Tables["DebitSemester"].Rows[p]["YEAR"].ToString() == dr["YEAR"].ToString()) && (ds.Tables["DebitSemester"].Rows[p]["SEMESTER"].ToString() == dr["SEMESTER"].ToString()))
                {
                    ds.Tables["DebitSemester"].Rows.RemoveAt(p--);
                }
            }
            dr["isRegi"] = "1";
        }

        foreach (DataRow dr in ds.Tables["DebitSemester"].Rows)
        {
            DataRow drN = ds.Tables["registration"].NewRow();
            drN["SID"] = dr["SID"].ToString();
            drN["SEMESTER"] = dr["SEMESTER"].ToString();
            drN["YEAR"] = dr["YEAR"].ToString();
            drN["REGKEY"] = dr["SID"].ToString() + dr["SEMESTER"].ToString() + dr["YEAR"].ToString();
            drN["isRegi"] = "0";

            ds.Tables["registration"].Rows.Add(drN);
        }

        int i = 0;
        int rowSpan = 0;
        double regFee = 0;
        double adjustment = 0;
        int loan = 0;
        int waiver = 0;
        double tuitionFee = 0;
        double totaPable = 0;
        int k = 0;
        double paid = 0;
        double totalCredit = 0;
        double total_loan = 0;
        double total_waiver = 0;
        double total_paid = 0;
        double total_payable = 0;
        double credt = 0;
        double semDevelopment = 0, semExtracurricularFee = 0, semLabFee = 0, semLibraryFee = 0;
        int index = 0;

        string adminYearSem = get_year_and_semester_ofA_student(sid);

        foreach (DataRow dr in ds.Tables["registration"].Rows)
        {
            rowSpan = 0;
            regFee = 0;
            adjustment = 0;
            loan = 0;
            waiver = 0;
            tuitionFee = 0;
            totaPable = 0;
            k = 0;
            credt = 0;
            semDevelopment = 0;
            semExtracurricularFee = 0;
            semLabFee = 0;
            semLibraryFee = 0;

            //----------- Payable-------------------------------------------------------------

            if (i == 0)
            {
                ds.Merge(get_admission_credit(sid));
                if (ds.Tables["ADMISSIONCREDIT"].Rows.Count > 0)
                    totaPable = Convert.ToDouble("0" + ds.Tables["ADMISSIONCREDIT"].Rows[0]["ADMINFEE"].ToString());
                rowSpan = 1; // Admission  
                i++;
            }

            int STATUS = get_Status(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString());

            //string year = dr["YEAR"].ToString();
            //string sem = dr["SEMESTER"].ToString();
            //String qul = "SELECT (CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END) AS STATUS FROM REGISTATUS WHERE YEAR=" + year + " and  SEMESTER=" + sem + " " + " and SID='" + sid + "'";

            

            //int STATUS = 0;
            //STATUS = rsl.getInt("STATUS");


            if (STATUS == 0)
            {
                //isStuValidForDevFee = false;
            }
            else
            {
                if (int.Parse(adminYearSem) >= 20121)
                {
                    semDevelopment = 2000;
                    rowSpan += 1;
                }
                else if (int.Parse(adminYearSem) >= 20112)
                {
                    semDevelopment = 1000;
                    rowSpan += 1;
                }

                if (int.Parse(adminYearSem) >= 20123)
                {
                    semExtracurricularFee = 1000;
                    semLabFee = 500;
                    semLibraryFee = 500;
                }
            }

            

            ds.Merge(get_registration_credit(dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            rowSpan += 1;// ds.Tables["ADMINREGISTRATIONRATE"].Rows.Count; //  registration            

            ds.Merge(get_tuition_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            rowSpan += ds.Tables["STUDENTCREDIT"].Rows.Count;
            if (ds.Tables["STUDENTCREDIT"].Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString()) && !String.IsNullOrEmpty(ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString()))
                {
                    adjustment = Convert.ToDouble(ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString());
                    tuitionFee = Convert.ToDouble(ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString());
                }
                else
                {
                    adjustment = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString());
                    tuitionFee = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString());
                }
            }

            double tmpRegFee = 0;
            foreach (DataRow drRR in ds.Tables["ADMINREGISTRATIONRATE"].Rows)
            {
                tmpRegFee = Convert.ToDouble("0" + drRR["REGRATE"].ToString());
            }

            regFee = get_registration_credit_by_id(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString());

            regFee = (regFee > 0 ? (regFee > tmpRegFee ? regFee : tmpRegFee) : 0);

            ds.Merge(get_loan_waiver_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            foreach (DataRow drL in ds.Tables["LOANSANDWAIVER"].Rows)
            {
                loan = Convert.ToInt32("0" + drL["LOAN"].ToString());
                waiver = Convert.ToInt32("0" + drL["WAIVER"].ToString());
            }

            if (tuitionFee > 0)
            {
                total_loan += Convert.ToInt32((((tuitionFee - regFee) * loan) / 100));
            }
            else
            {
                total_loan += 0;
            }

            if (tuitionFee > 0)
            {
                total_waiver += Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100));
            }
            else
            {
                total_waiver += 0;
            }

            if (tuitionFee > 0)
            {
                totaPable += tuitionFee - ((((tuitionFee - regFee) * loan) / 100) + (((tuitionFee - regFee) * waiver) / 100) + adjustment);
            }
            else if (tuitionFee < 0)
            {
                totaPable += tuitionFee;// -regFee;
            }
            else
            {
                totaPable += regFee;// -adjustment;
            }

            totaPable += (semDevelopment + semExtracurricularFee + semLabFee + semLibraryFee);

            cls_tools obj_tools = new cls_tools();
            ds.Merge(get_semester_debit_ofA_student(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));

            paid = 0;
            foreach (DataRow drD in ds.Tables["STUDENTDEBIT"].Rows)
            {
                paid += Convert.ToDouble("0" + drD["AMOUNT"].ToString());
            }

            ds.Merge(get_lateFee_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            //******************************************************  late fee for unpaid taka
            DateTime dt_firstIns = new DateTime();
            DateTime dt_secondIns = new DateTime();
            DateTime dt_thirdIns = new DateTime();
            DateTime dt_registration = new DateTime();
            DateTime dt_end = DateTime.Parse("30-SEP-2010");

            //*******************late fine section
            if (int.Parse(dr["YEAR"].ToString()) >= 2008)
            {
                DataTable fdt = new DataTable();
                fdt.Merge(get_last_dates_of_payment(dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));

                if (fdt.Rows.Count > 0)
                {
                    dt_firstIns = Convert.ToDateTime(fdt.Rows[0]["INSONEDATE"]);
                    dt_secondIns = Convert.ToDateTime(fdt.Rows[0]["INSTWODATE"]);
                    dt_thirdIns = Convert.ToDateTime(fdt.Rows[0]["INSTHREEDATE"]);
                    dt_registration = Convert.ToDateTime(fdt.Rows[0]["REGISTRATIONDATE"]);
                }
            }


            {
                //(semCredit-adjustment-loan-waiver-semRegFee)
                double totaReceivable = tuitionFee - adjustment - Convert.ToInt32((((tuitionFee - regFee) * loan) / 100)) - Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100)) - regFee;


                if ((int.Parse(dr["YEAR"].ToString()) == 2010 && int.Parse(dr["SEMESTER"].ToString()) == 3) || int.Parse(dr["YEAR"].ToString()) > 2010)
                {
                    int fc = 0;
					
					foreach (DataRow drD in ds.Tables["STUDENTDEBIT"].Rows)
                    {
                        if (drD["HEADSN"].ToString() == "32")
                        {
                            DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                            tdr["AMOUNT"] = drD["AMOUNT"];
                            tdr["HEADNAME"] = drD["HEADNAME"];
                            tdr["HEADSN"] = drD["HEADSN"];
                            ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                        }
                    }
                    if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "2"))
                    //if (DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "2"))
                    {
                        if (totaReceivable > 0)
                        {
                            if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "8")))
                            //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "8"))
                            {
                                if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                                //if (today.compareTo(dt_firstIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "5"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (1st Inst)";
                                        tdr["HEADSN"] = "25"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                                if (DateTime.Today.CompareTo(dt_secondIns) > 0)
                                //if (today.compareTo(dt_secondIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "6"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (2nd Inst)";
                                        tdr["HEADSN"] = "26"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                                if (DateTime.Today.CompareTo(dt_thirdIns) > 0)
                                //if (today.compareTo(dt_thirdIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "7"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                                        tdr["HEADSN"] = "27"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                            }
                        }//end if( isPaidOfThisHead(idd,yeari,semi,
                    }
                    //li = li + fc;

                }//end if((yeari==2010 && semi==3) ||yeari>2010)

                else if (int.Parse(dr["YEAR"].ToString()) == 2008 && int.Parse(dr["SEMESTER"].ToString()) == 1)
                {
                    int fc = 0;
                    double tempBalanceupto = 0;//balanceUpto;

                    //double paid = new student_webService().GetCurrentSemTotalTutionFeepayment(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString());
                    paid += tempBalanceupto;
                    if (totaReceivable - (paid) > cls_tools.finePlusMinus)
                    {
                        if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                        {
                            DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                            tdr["amount"] = (int)((totaReceivable - paid) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_thirdIns));
                            tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                            tdr["HEADSN"] = "27"; fc++;
                            ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                        }
                    }
                }//  end year=2008 and semester =1
                else if (int.Parse(dr["YEAR"].ToString()) >= 2008)
                {
                    foreach (DataRow drLateFreeCredit in ds.Tables["LATEFEECREDIT"].Rows)
                    {
                        int head = int.Parse(drLateFreeCredit["HEADSN"].ToString());
                        if (head >= 24 && head <= 27)
                        {
                            drLateFreeCredit["amount"] = int.Parse(drLateFreeCredit["amount"].ToString()) / 2;
                        }
                    }

                    //**************************************for registration
                    int fc = 0;
                    double tempBalanceupto = 0;//balanceUpto;
                    double installmentAmout = totaReceivable / 3;
                    if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "8"))
                    {

                    }
                    else//course fee full not paid
                    {
                        //if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5"))
                        //for first installment
                        {
                            if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_firstIns));
                                    tdr["HEADNAME"] = "Late Fine (1st Inst)";
                                    tdr["HEADSN"] = "25"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }
                        // if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6"))
                        //for second installment
                        {
                            if (DateTime.Today.CompareTo(dt_secondIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_secondIns));
                                    tdr["HEADNAME"] = "Late Fine (2nd Inst)";
                                    tdr["HEADSN"] = "26"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }
                      //  if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7"))//third inst
                        {
                            if (DateTime.Today.CompareTo(dt_thirdIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_thirdIns));
                                    tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                                    tdr["HEADSN"] = "27"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }

                    }//course fee full end                   
                }/// if(Integer.parseInt(syear[j])>=2008 && Integer.parseInt(ssem[j])==1)
            }

            //****************************************************** end late fee for unpaid taka
            rowSpan += ds.Tables["LATEFEECREDIT"].Rows.Count;

            foreach (DataRow drL in ds.Tables["LATEFEECREDIT"].Rows)
                totaPable += Convert.ToDouble(drL["amount"].ToString());


            // ---------- Registration --------------

            foreach (DataRow drTC in ds.Tables["STUDENTCREDIT"].Rows)
            {
                credt = get_semester_creditHrs_ofA_student(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString());
                totalCredit += credt;
            }

            //----------- Paid-------------------------------------------------------------

            //----------------------- Semester Balance ---------------------------------------------
            double p = totaPable;
            total_payable += totaPable;
            total_paid += paid;

            //------------------ clear All ---------------------------------------------------------
            ds.Tables["ADMINREGISTRATIONRATE"].Rows.Clear();
            ds.Tables["LATEFEECREDIT"].Rows.Clear();
            ds.Tables["ADMISSIONCREDIT"].Rows.Clear();
            ds.Tables["STUDENTCREDIT"].Rows.Clear();
            ds.Tables["LOANSANDWAIVER"].Rows.Clear();
            ds.Tables["STUDENTDEBIT"].Rows.Clear();

            index++;
        }

        //----------------------- Semester Balance ---------------------------------------------
        return total_payable - total_paid;
    }




    public double getTotalPayableAmt(String sid, String semester, String year)
    {
        DataSet ds = new DataSet();
        ds.Merge(get_allRegistred_semesters_ofA_student_for_account(sid, semester, year));
        ds.Merge(get_all_Debit_semesters_ofA_student(sid, semester, year));

        ds.Tables["registration"].Columns.Add("isRegi");
        foreach (DataRow dr in ds.Tables["registration"].Rows)
        {
            for (int p = 0; p < ds.Tables["DebitSemester"].Rows.Count; p++)
            {
                if ((ds.Tables["DebitSemester"].Rows[p]["YEAR"].ToString() == dr["YEAR"].ToString()) && (ds.Tables["DebitSemester"].Rows[p]["SEMESTER"].ToString() == dr["SEMESTER"].ToString()))
                {
                    ds.Tables["DebitSemester"].Rows.RemoveAt(p--);
                }
            }
            dr["isRegi"] = "1";
        }

        foreach (DataRow dr in ds.Tables["DebitSemester"].Rows)
        {
            DataRow drN = ds.Tables["registration"].NewRow();
            drN["SID"] = dr["SID"].ToString();
            drN["SEMESTER"] = dr["SEMESTER"].ToString();
            drN["YEAR"] = dr["YEAR"].ToString();
            drN["REGKEY"] = dr["SID"].ToString() + dr["SEMESTER"].ToString() + dr["YEAR"].ToString();
            drN["isRegi"] = "0";

            ds.Tables["registration"].Rows.Add(drN);
        }

        int i = 0;
        int rowSpan = 0;
        double regFee = 0;
        double adjustment = 0;
        int loan = 0;
        int waiver = 0;
        double tuitionFee = 0;
        double totaPable = 0;
        int k = 0;
        double paid = 0;
        double totalCredit = 0;
        double total_loan = 0;
        double total_waiver = 0;
        double total_paid = 0;
        double total_payable = 0;
        double credt = 0;
        double semDevelopment = 0, semExtracurricularFee = 0, semLabFee = 0, semLibraryFee = 0;
        int index = 0;

        string adminYearSem = get_year_and_semester_ofA_student(sid);//get_Prevyear_and_semester_ofA_student(sid);

        foreach (DataRow dr in ds.Tables["registration"].Rows)
        {
            rowSpan = 0;
            regFee = 0;
            adjustment = 0;
            loan = 0;
            waiver = 0;
            tuitionFee = 0;
            totaPable = 0;
            k = 0;
            credt = 0;
            semDevelopment = 0;
            semExtracurricularFee = 0;
            semLabFee = 0;
            semLibraryFee = 0;

            //----------- Payable-------------------------------------------------------------

            if (i == 0)
            {
                ds.Merge(get_admission_credit(sid));
                if (ds.Tables["ADMISSIONCREDIT"].Rows.Count > 0)
                    totaPable = Convert.ToDouble("0" + ds.Tables["ADMISSIONCREDIT"].Rows[0]["ADMINFEE"].ToString());
                rowSpan = 1; // Admission  
                i++;
            }

            int STATUS = get_Status(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString());

            if (STATUS == 0)
            {
                //isStuValidForDevFee = false;
            }
            else
            {
                if (int.Parse(adminYearSem) >= 20121)
                {
                    semDevelopment = 2000;
                    rowSpan += 1;
                }
                else if (int.Parse(adminYearSem) >= 20112)
                {
                    semDevelopment = 1000;
                    rowSpan += 1;
                }

                if (int.Parse(adminYearSem) >= 20123)
                {
                    semExtracurricularFee = 1000;
                    semLabFee = 500;
                    semLibraryFee = 500;
                }
            }
            ds.Merge(get_registration_credit(dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            rowSpan += 1;// ds.Tables["ADMINREGISTRATIONRATE"].Rows.Count; //  registration            

            ds.Merge(get_tuition_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            rowSpan += ds.Tables["STUDENTCREDIT"].Rows.Count;
            if (ds.Tables["STUDENTCREDIT"].Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString()) && !String.IsNullOrEmpty(ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString()))
                {
                    adjustment = Convert.ToDouble(ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString());
                    tuitionFee = Convert.ToDouble(ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString());
                }
                else
                {
                    adjustment = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString());
                    tuitionFee = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString());
                }
            }

            double tmpRegFee = 0;
            foreach (DataRow drRR in ds.Tables["ADMINREGISTRATIONRATE"].Rows)
            {
                tmpRegFee = Convert.ToDouble("0" + drRR["REGRATE"].ToString());
            }

            regFee = get_registration_credit_by_id(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString());

            regFee = (regFee > 0 ? (regFee > tmpRegFee ? regFee : tmpRegFee) : 0);

            ds.Merge(get_loan_waiver_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            foreach (DataRow drL in ds.Tables["LOANSANDWAIVER"].Rows)
            {
                loan = Convert.ToInt32("0" + drL["LOAN"].ToString());
                waiver = Convert.ToInt32("0" + drL["WAIVER"].ToString());
            }

            if (tuitionFee > 0)
            {
                total_loan += Convert.ToInt32((((tuitionFee - regFee) * loan) / 100));
            }
            else
            {
                total_loan += 0;
            }

            if (tuitionFee > 0)
            {
                total_waiver += Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100));
            }
            else
            {
                total_waiver += 0;
            }

            if (tuitionFee > 0)
            {
                totaPable += tuitionFee - ((((tuitionFee - regFee) * loan) / 100) + (((tuitionFee - regFee) * waiver) / 100) + adjustment);
            }
            else if (tuitionFee < 0)
            {
                totaPable += tuitionFee;// -regFee;
            }
            else
            {
                totaPable += regFee;// -adjustment;
            }

            totaPable += (semDevelopment + semExtracurricularFee + semLabFee + semLibraryFee);

            cls_tools obj_tools = new cls_tools();
            ds.Merge(get_semester_debit_ofA_student(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));

            paid = 0;
            foreach (DataRow drD in ds.Tables["STUDENTDEBIT"].Rows)
            {
                paid += Convert.ToDouble("0" + drD["AMOUNT"].ToString());
            }

            ds.Merge(get_lateFee_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            //******************************************************  late fee for unpaid taka
            DateTime dt_firstIns = new DateTime();
            DateTime dt_secondIns = new DateTime();
            DateTime dt_thirdIns = new DateTime();
            DateTime dt_registration = new DateTime();
            DateTime dt_end = DateTime.Parse("30-SEP-2010");

            //*******************late fine section
            if (int.Parse(dr["YEAR"].ToString()) >= 2008)
            {
                DataTable fdt = new DataTable();
                fdt.Merge(get_last_dates_of_payment(dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));

                if (fdt.Rows.Count > 0)
                {
                    dt_firstIns = Convert.ToDateTime(fdt.Rows[0]["INSONEDATE"]);
                    dt_secondIns = Convert.ToDateTime(fdt.Rows[0]["INSTWODATE"]);
                    dt_thirdIns = Convert.ToDateTime(fdt.Rows[0]["INSTHREEDATE"]);
                    dt_registration = Convert.ToDateTime(fdt.Rows[0]["REGISTRATIONDATE"]);
                }
            }


            {
                //(semCredit-adjustment-loan-waiver-semRegFee)
                double totaReceivable = tuitionFee - adjustment - Convert.ToInt32((((tuitionFee - regFee) * loan) / 100)) - Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100)) - regFee;


                if ((int.Parse(dr["YEAR"].ToString()) == 2010 && int.Parse(dr["SEMESTER"].ToString()) == 3) || int.Parse(dr["YEAR"].ToString()) > 2010)
                {
                    int fc = 0;

                    foreach (DataRow drD in ds.Tables["STUDENTDEBIT"].Rows)
                    {
                        if (drD["HEADSN"].ToString() == "32")
                        {
                            DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                            tdr["AMOUNT"] = drD["AMOUNT"];
                            tdr["HEADNAME"] = drD["HEADNAME"];
                            tdr["HEADSN"] = drD["HEADSN"];
                            ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                        }
                    }
                    if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "2"))
                    //if (DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "2"))
                    {
                        if (totaReceivable > 0)
                        {
                            if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "8")))
                            //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "8"))
                            {
                                if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                                //if (today.compareTo(dt_firstIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "5"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (1st Inst)";
                                        tdr["HEADSN"] = "25"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                                if (DateTime.Today.CompareTo(dt_secondIns) > 0)
                                //if (today.compareTo(dt_secondIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "6"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (2nd Inst)";
                                        tdr["HEADSN"] = "26"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                                if (DateTime.Today.CompareTo(dt_thirdIns) > 0)
                                //if (today.compareTo(dt_thirdIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "7"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                                        tdr["HEADSN"] = "27"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                            }
                        }//end if( isPaidOfThisHead(idd,yeari,semi,
                    }
                    //li = li + fc;

                }//end if((yeari==2010 && semi==3) ||yeari>2010)

                else if (int.Parse(dr["YEAR"].ToString()) == 2008 && int.Parse(dr["SEMESTER"].ToString()) == 1)
                {
                    int fc = 0;
                    double tempBalanceupto = 0;//balanceUpto;

                    //double paid = new student_webService().GetCurrentSemTotalTutionFeepayment(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString());
                    paid += tempBalanceupto;
                    if (totaReceivable - (paid) > cls_tools.finePlusMinus)
                    {
                        if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                        {
                            DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                            tdr["amount"] = (int)((totaReceivable - paid) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_thirdIns));
                            tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                            tdr["HEADSN"] = "27"; fc++;
                            ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                        }
                    }
                }//  end year=2008 and semester =1
                else if (int.Parse(dr["YEAR"].ToString()) >= 2008)
                {
                    foreach (DataRow drLateFreeCredit in ds.Tables["LATEFEECREDIT"].Rows)
                    {
                        int head = int.Parse(drLateFreeCredit["HEADSN"].ToString());
                        if (head >= 24 && head <= 27)
                        {
                            drLateFreeCredit["amount"] = int.Parse(drLateFreeCredit["amount"].ToString()) / 2;
                        }
                    }

                    //**************************************for registration
                    int fc = 0;
                    double tempBalanceupto = 0;//balanceUpto;
                    double installmentAmout = totaReceivable / 3;
                    if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "8"))
                    {

                    }
                    else//course fee full not paid
                    {
                        //if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5"))
                        //for first installment
                        {
                            if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_firstIns));
                                    tdr["HEADNAME"] = "Late Fine (1st Inst)";
                                    tdr["HEADSN"] = "25"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }
                        // if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6"))
                        //for second installment
                        {
                            if (DateTime.Today.CompareTo(dt_secondIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_secondIns));
                                    tdr["HEADNAME"] = "Late Fine (2nd Inst)";
                                    tdr["HEADSN"] = "26"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }
                        //  if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7"))//third inst
                        {
                            if (DateTime.Today.CompareTo(dt_thirdIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_thirdIns));
                                    tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                                    tdr["HEADSN"] = "27"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }

                    }//course fee full end                   
                }/// if(Integer.parseInt(syear[j])>=2008 && Integer.parseInt(ssem[j])==1)
            }

            //****************************************************** end late fee for unpaid taka
            rowSpan += ds.Tables["LATEFEECREDIT"].Rows.Count;

            foreach (DataRow drL in ds.Tables["LATEFEECREDIT"].Rows)
                totaPable += Convert.ToDouble(drL["amount"].ToString());


            // ---------- Registration --------------

            foreach (DataRow drTC in ds.Tables["STUDENTCREDIT"].Rows)
            {
                credt = get_semester_creditHrs_ofA_student(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString());
                totalCredit += credt;
            }

            //----------- Paid-------------------------------------------------------------

            //----------------------- Semester Balance ---------------------------------------------
            double p = totaPable;
            total_payable += totaPable;
            total_paid += paid;

            //------------------ clear All ---------------------------------------------------------
            ds.Tables["ADMINREGISTRATIONRATE"].Rows.Clear();
            ds.Tables["LATEFEECREDIT"].Rows.Clear();
            ds.Tables["ADMISSIONCREDIT"].Rows.Clear();
            ds.Tables["STUDENTCREDIT"].Rows.Clear();
            ds.Tables["LOANSANDWAIVER"].Rows.Clear();
            ds.Tables["STUDENTDEBIT"].Rows.Clear();

            index++;
        }

        //----------------------- Semester Balance ---------------------------------------------
        return totaPable;
    }


	public double GetTakenCourseAccountBalance(String sid, String semester, String year)
    {
        DataSet ds = new DataSet();
        ds.Merge(get_allRegistred_semesters_ofA_student_for_account(sid, semester, year));
        ds.Merge(get_all_Debit_semesters_ofA_student(sid, semester, year));

        ds.Tables["registration"].Columns.Add("isRegi");
        foreach (DataRow dr in ds.Tables["registration"].Rows)
        {
            for (int p = 0; p < ds.Tables["DebitSemester"].Rows.Count; p++)
            {
                if ((ds.Tables["DebitSemester"].Rows[p]["YEAR"].ToString() == dr["YEAR"].ToString()) && (ds.Tables["DebitSemester"].Rows[p]["SEMESTER"].ToString() == dr["SEMESTER"].ToString()))
                {
                    ds.Tables["DebitSemester"].Rows.RemoveAt(p--);
                }
            }
            dr["isRegi"] = "1";
        }

        foreach (DataRow dr in ds.Tables["DebitSemester"].Rows)
        {
            DataRow drN = ds.Tables["registration"].NewRow();
            drN["SID"] = dr["SID"].ToString();
            drN["SEMESTER"] = dr["SEMESTER"].ToString();
            drN["YEAR"] = dr["YEAR"].ToString();
            drN["REGKEY"] = dr["SID"].ToString() + dr["SEMESTER"].ToString() + dr["YEAR"].ToString();
            drN["isRegi"] = "0";

            ds.Tables["registration"].Rows.Add(drN);
        }

        int i = 0;
        int rowSpan = 0;
        double regFee = 0;
        double adjustment = 0;
        int loan = 0;
        int waiver = 0;
        double tuitionFee = 0;
        double totaPable = 0, curTotalPayable = 0;
        int k = 0;
        double paid = 0;
        double totalCredit = 0;
        double total_loan = 0;
        double total_waiver = 0;
        double total_paid = 0;
        double total_payable = 0;
        double credt = 0;
        double semDevelopment = 0, semExtracurricularFee = 0, semLabFee = 0, semLibraryFee = 0;
        int index = 0;

        string adminYearSem = get_year_and_semester_ofA_student(sid);

        foreach (DataRow dr in ds.Tables["registration"].Rows)
        {
            rowSpan = 0;
            regFee = 0;
            adjustment = 0;
            loan = 0;
            waiver = 0;
            tuitionFee = 0;
            totaPable = 0;
            k = 0;
            credt = 0;
            semDevelopment = 0;
            semExtracurricularFee = 0;
            semLabFee = 0;
            semLibraryFee = 0;

            //----------- Payable-------------------------------------------------------------

            if (i == 0)
            {
                ds.Merge(get_admission_credit(sid));
                if (ds.Tables["ADMISSIONCREDIT"].Rows.Count > 0)
                    totaPable = Convert.ToDouble("0" + ds.Tables["ADMISSIONCREDIT"].Rows[0]["ADMINFEE"].ToString());
                rowSpan = 1; // Admission  
                i++;
            }

            if (int.Parse(adminYearSem) >= 20121)
            {
                semDevelopment = 2000;
                rowSpan += 1;
            }
            else if (int.Parse(adminYearSem) >= 20112)
            {
                semDevelopment = 1000;
                rowSpan += 1;
            }

            if (int.Parse(adminYearSem) >= 20123)
            {
                semExtracurricularFee = 1000;
                semLabFee = 500;
                semLibraryFee = 500;
            }

            ds.Merge(get_registration_credit(dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            rowSpan += 1;// ds.Tables["ADMINREGISTRATIONRATE"].Rows.Count; //  registration            

            ds.Merge(get_tuition_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            rowSpan += ds.Tables["STUDENTCREDIT"].Rows.Count;
            if (ds.Tables["STUDENTCREDIT"].Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString()) && !String.IsNullOrEmpty(ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString()))
                {
                    adjustment = Convert.ToDouble(ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString());
                    tuitionFee = Convert.ToDouble(ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString());
                }
                else
                {
                    adjustment = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["ADJUSTMENT"].ToString());
                    tuitionFee = Convert.ToDouble("0" + ds.Tables["STUDENTCREDIT"].Rows[0]["AMOUNT"].ToString());
                }
            }

            double tmpRegFee = 0;
            foreach (DataRow drRR in ds.Tables["ADMINREGISTRATIONRATE"].Rows)
            {
                tmpRegFee = Convert.ToDouble("0" + drRR["REGRATE"].ToString());
            }

            regFee = get_registration_credit_by_id(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString());

            regFee = (regFee > 0 ? (regFee > tmpRegFee ? regFee : tmpRegFee) : 0);

            ds.Merge(get_loan_waiver_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            foreach (DataRow drL in ds.Tables["LOANSANDWAIVER"].Rows)
            {
                loan = Convert.ToInt32("0" + drL["LOAN"].ToString());
                waiver = Convert.ToInt32("0" + drL["WAIVER"].ToString());
            }

            if (tuitionFee > 0)
            {
                total_loan += Convert.ToInt32((((tuitionFee - regFee) * loan) / 100));
            }
            else
            {
                total_loan += 0;
            }

            if (tuitionFee > 0)
            {
                total_waiver += Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100));
            }
            else
            {
                total_waiver += 0;
            }

            if (tuitionFee > 0)
            {
                totaPable += tuitionFee - ((((tuitionFee - regFee) * loan) / 100) + (((tuitionFee - regFee) * waiver) / 100) + adjustment);
            }
            else if (tuitionFee < 0)
            {
                totaPable += tuitionFee;// -regFee;
            }
            else
            {
                totaPable += regFee;// -adjustment;
            }

            totaPable += (semDevelopment + semExtracurricularFee + semLabFee + semLibraryFee);

            cls_tools obj_tools = new cls_tools();
            ds.Merge(get_semester_debit_ofA_student(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));

            paid = 0;
            foreach (DataRow drD in ds.Tables["STUDENTDEBIT"].Rows)
            {
                paid += Convert.ToDouble("0" + drD["AMOUNT"].ToString());
            }

            ds.Merge(get_lateFee_credit(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));
            //******************************************************  late fee for unpaid taka
            DateTime dt_firstIns = new DateTime();
            DateTime dt_secondIns = new DateTime();
            DateTime dt_thirdIns = new DateTime();
            DateTime dt_registration = new DateTime();
            DateTime dt_end = DateTime.Parse("30-SEP-2010");

            //*******************late fine section
            if (int.Parse(dr["YEAR"].ToString()) >= 2008)
            {
                DataTable fdt = new DataTable();
                fdt.Merge(get_last_dates_of_payment(dr["SEMESTER"].ToString(), dr["YEAR"].ToString()));

                if (fdt.Rows.Count > 0)
                {
                    dt_firstIns = Convert.ToDateTime(fdt.Rows[0]["INSONEDATE"]);
                    dt_secondIns = Convert.ToDateTime(fdt.Rows[0]["INSTWODATE"]);
                    dt_thirdIns = Convert.ToDateTime(fdt.Rows[0]["INSTHREEDATE"]);
                    dt_registration = Convert.ToDateTime(fdt.Rows[0]["REGISTRATIONDATE"]);
                }
            }


            {
                //(semCredit-adjustment-loan-waiver-semRegFee)
                double totaReceivable = tuitionFee - adjustment - Convert.ToInt32((((tuitionFee - regFee) * loan) / 100)) - Convert.ToInt32((((tuitionFee - regFee) * waiver) / 100)) - regFee;


                if ((int.Parse(dr["YEAR"].ToString()) == 2010 && int.Parse(dr["SEMESTER"].ToString()) == 3) || int.Parse(dr["YEAR"].ToString()) > 2010)
                {
                    int fc = 0;
                    if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "2"))
                    //if (DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "2"))
                    {
                        if (totaReceivable > 0)
                        {
                            if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "8")))
                            //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "8"))
                            {
                                if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                                //if (today.compareTo(dt_firstIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "5"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (1st Inst)";
                                        tdr["HEADSN"] = "25"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                                if (DateTime.Today.CompareTo(dt_secondIns) > 0)
                                //if (today.compareTo(dt_secondIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "6"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (2nd Inst)";
                                        tdr["HEADSN"] = "26"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                                if (DateTime.Today.CompareTo(dt_thirdIns) > 0)
                                //if (today.compareTo(dt_thirdIns) > 0)
                                {
                                    if (!(new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7")))
                                    //if (!DatabaseTools.isPaid(sid, int.Parse(dr["YEAR"].ToString()), int.Parse(dr["SEMESTER"].ToString()), "7"))
                                    {
                                        DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                        tdr["amount"] = 500;
                                        tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                                        tdr["HEADSN"] = "27"; fc++;
                                        ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                    }
                                }
                            }
                        }//end if( isPaidOfThisHead(idd,yeari,semi,
                    }
                    //li = li + fc;

                }//end if((yeari==2010 && semi==3) ||yeari>2010)

                else if (int.Parse(dr["YEAR"].ToString()) == 2008 && int.Parse(dr["SEMESTER"].ToString()) == 1)
                {
                    int fc = 0;
                    double tempBalanceupto = 0;//balanceUpto;

                    //double paid = new student_webService().GetCurrentSemTotalTutionFeepayment(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString());
                    paid += tempBalanceupto;
                    if (totaReceivable - (paid) > cls_tools.finePlusMinus)
                    {
                        if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                        {
                            DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                            tdr["amount"] = (int)((totaReceivable - paid) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_thirdIns));
                            tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                            tdr["HEADSN"] = "27"; fc++;
                            ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                        }
                    }
                }//  end year=2008 and semester =1
                else if (int.Parse(dr["YEAR"].ToString()) >= 2008)
                {
                    foreach (DataRow drLateFreeCredit in ds.Tables["LATEFEECREDIT"].Rows)
                    {
                        int head = int.Parse(drLateFreeCredit["HEADSN"].ToString());
                        if (head >= 24 && head <= 27)
                        {
                            drLateFreeCredit["amount"] = int.Parse(drLateFreeCredit["amount"].ToString()) / 2;
                        }
                    }

                    //**************************************for registration
                    int fc = 0;
                    double tempBalanceupto = 0;//balanceUpto;
                    double installmentAmout = totaReceivable / 3;
                    if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "8"))
                    {

                    }
                    else//course fee full not paid
                    {
                        //if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5"))//first inst
                        {
                            if (DateTime.Today.CompareTo(dt_firstIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "5");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_firstIns));
                                    tdr["HEADNAME"] = "Late Fine (1st Inst)";
                                    tdr["HEADSN"] = "25"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }
                        // if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6"))//second inst
                        {
                            if (DateTime.Today.CompareTo(dt_secondIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "6");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_secondIns));
                                    tdr["HEADNAME"] = "Late Fine (2nd Inst)";
                                    tdr["HEADSN"] = "26"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }
                        // if (new student_webService().isPaid(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7"))//third inst
                        {
                            if (DateTime.Today.CompareTo(dt_thirdIns) > 0)
                            {
                                double paidAmount = new student_webService().GetTotalPaidOfThisHead(sid, dr["YEAR"].ToString(), dr["SEMESTER"].ToString(), "7");
                                tempBalanceupto += paidAmount;
                                if (installmentAmout - paidAmount > cls_tools.finePlusMinus)
                                {
                                    DataRow tdr = ds.Tables["LATEFEECREDIT"].NewRow();
                                    tdr["amount"] = (int)((installmentAmout - paidAmount) * cls_tools.fineMultiplier * cls_tools.getDiffIndays(dt_end, dt_thirdIns));
                                    tdr["HEADNAME"] = "Late Fine (3rd Inst)";
                                    tdr["HEADSN"] = "27"; fc++;
                                    ds.Tables["LATEFEECREDIT"].Rows.Add(tdr);
                                }
                            }
                        }

                    }//course fee full end                   
                }/// if(Integer.parseInt(syear[j])>=2008 && Integer.parseInt(ssem[j])==1)
            }

            //****************************************************** end late fee for unpaid taka
            rowSpan += ds.Tables["LATEFEECREDIT"].Rows.Count;

            foreach (DataRow drL in ds.Tables["LATEFEECREDIT"].Rows)
                totaPable += Convert.ToDouble(drL["amount"].ToString());


            // ---------- Registration --------------

            foreach (DataRow drTC in ds.Tables["STUDENTCREDIT"].Rows)
            {
                credt = get_semester_creditHrs_ofA_student(sid, dr["SEMESTER"].ToString(), dr["YEAR"].ToString());
                totalCredit += credt;
            }

            //----------- Paid-------------------------------------------------------------

            //----------------------- Semester Balance ---------------------------------------------
            total_payable += totaPable;
            total_paid += paid;

            //------------------ clear All ---------------------------------------------------------
            ds.Tables["ADMINREGISTRATIONRATE"].Rows.Clear();
            ds.Tables["LATEFEECREDIT"].Rows.Clear();
            ds.Tables["ADMISSIONCREDIT"].Rows.Clear();
            ds.Tables["STUDENTCREDIT"].Rows.Clear();
            ds.Tables["LOANSANDWAIVER"].Rows.Clear();
            ds.Tables["STUDENTDEBIT"].Rows.Clear();

            if (dr["SEMESTER"].ToString() == semester && dr["YEAR"].ToString() == year)
            {
                curTotalPayable = totaPable - paid;
                break;
            }

            index++;
        }

        //----------------------- Semester Balance ---------------------------------------------
        return curTotalPayable;
    }
	
	public int GetEnrolledStudentOfCourse(string courseKey, string group)
    {
        DataTable dt = new DataTable();
        String que = "";
        int actual_taken = 0, temp_total_taken = 0;

        try
        {
            que = "select count(REGKEY) as total_taken from WEB_COURSE_OFFERING_TEMP where COURSEKEY='" + courseKey + "' and GGROUP='" + group + "'";
            dt = new DataTable();
            dt.Merge(obj_db.get_viewData(que, "temp"));
            if (dt.Rows.Count > 0)
            {
                temp_total_taken = int.Parse(dt.Rows[0]["total_taken"].ToString());
            }

            que = "select count(REGKEY) as total_taken from OFFERERINGANDGRADE where COURSEKEY='" + courseKey + "' and GGROUP='" + group + "'";
            dt = new DataTable();
            dt.Merge(obj_db.get_viewData(que, "temp"));
            if (dt.Rows.Count > 0)
            {
                actual_taken = int.Parse(dt.Rows[0]["total_taken"].ToString());
            }
        }
        catch { }

        return actual_taken > temp_total_taken ? actual_taken : temp_total_taken;
    }

    public int GetEnrolledStudentOfCourseStudent(string courseKey, string group)
    {
        DataTable dt = new DataTable();
        String que = "";
        int capacity = 0, temp_total_taken = 0;

        try
        {
            que = "select count(REGKEY) as total_taken from WEB_COURSE_OFFERING_TEMP where COURSEKEY='" + courseKey + "' and GGROUP='" + group + "'";
            dt = new DataTable();
            dt.Merge(obj_db.get_viewData(que, "temp"));
            if (dt.Rows.Count > 0)
            {
                temp_total_taken = int.Parse(dt.Rows[0]["total_taken"].ToString());
            }

            que = "select Total_capacity from WEB_COURSE_TEACHER where COURSE_KEY='" + courseKey + "' and SECTION='" + group + "'";
            dt = new DataTable();
            dt.Merge(obj_db.get_viewData(que, "temp"));
            if (dt.Rows.Count > 0)
            {
                capacity = int.Parse(dt.Rows[0]["total_taken"].ToString());
            }
        }
        catch { }

        return capacity > temp_total_taken ? capacity : temp_total_taken;
    }



    public int GetEnrolledStudentOfCourse_New(string courseKey, string group)
    {
        DataTable dt = new DataTable();
        String que = "";
        int actual_taken = 0, temp_total_taken = 0;

        try
        {
            que = "select count(REGKEY) as total_taken from WEB_COURSE_OFFERING_TEMP where COURSEKEY='" + courseKey + "' and GGROUP='" + group + "'";
            dt = new DataTable();
            dt.Merge(obj_db.get_viewData(que, "temp"));
            if (dt.Rows.Count > 0)
            {
                temp_total_taken = int.Parse(dt.Rows[0]["total_taken"].ToString());
            }

            que = "select count(REGKEY) as total_taken from OFFERERINGANDGRADE where COURSEKEY='" + courseKey + "' and GGROUP='" + group + "'";
            dt = new DataTable();
            dt.Merge(obj_db.get_viewData(que, "temp"));
            if (dt.Rows.Count > 0)
            {
                actual_taken = int.Parse(dt.Rows[0]["total_taken"].ToString());
            }
        }
        catch { }

        return actual_taken;
    }
	public bool IsStudentPaidAdvisingLateFee(string sid, string sem, string year)
    {
        DataSet ds = new DataSet();

        string sql = " select sum(STUDENTDEBIT.AMOUNT) sum1 from STUDENTDEBIT where sid='" + sid + "' and year='" + year + "' and semester='" + sem + "' and headsn='32' HAVING  sum(STUDENTDEBIT.AMOUNT) > = '500' ";

        //and AMOUNT > = '750'
        ds.Merge(obj_db.get_viewData(sql, "STUDENTDEBIT"));

        if (ds.Tables["STUDENTDEBIT"].Rows.Count > 0)
            return true;
        else 
            return false;
    }
}

