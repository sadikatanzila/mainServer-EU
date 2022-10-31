using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Data;


public class admin_webService_prev : System.Web.Services.WebService
{
    Dts obj_db;

    public admin_webService_prev()
    {
        obj_db = new Dts();
    }

    public void Publish_Result(string semester, string year)
    {
        string sql = "UPDATE OFFERERINGANDGRADE SET APPROVE_CTRL = 1 WHERE COURSEKEY LIKE '" + semester + year + "%'"
                        + " AND APPROVE_CTRL IS NULL";
        obj_db.execute_query(sql);
    }

    public DataTable get_allStudent_ofA_aBatch(string prog, string batch)
    {
        string sql = "Select * from STUDENT where SPROGRAM='" + prog + "' and SID like '"+batch+"%'  ";        
        return obj_db.get_viewData(sql, "studentList");
    }


    public DataTable get_allDepartment()
    {
        string sql = " Select * from DEPARTMENTINCOLLEGE order by DEPCODE asc  ";
        return obj_db.get_viewData(sql, "departmentList");
    }

    public DataTable get_allCollege()
    {
        string sql = " Select * from COLLEGE order by COLLEGENAME asc  ";
        return obj_db.get_viewData(sql, "COLLEGE");
    }

    public string  get_departmentName(string depCode)
    {
        string depName="";

        DataSet ds = new DataSet();
        string sql = " Select * from DEPARTMENTINCOLLEGE where DEPCODE = '" + depCode + "'";
        ds.Merge(obj_db.get_viewData(sql, "DEPARTMENTINCOLLEGE"));
        foreach (DataRow dr in ds.Tables["DEPARTMENTINCOLLEGE"].Rows)
        {
            depName = dr["DEPNAME"].ToString();
        }
            return depName;
    }



    public DataTable get_all_offeredCourses(string year, string sem)
    {
        string sql = " SELECT DISTINCT o.COURSEKEY, c.* FROM OFFEREDCOURSE o, CHANGEDCOURSENAME c ";
        sql += " WHERE o.COURSECODE=c.COURSECODE AND o.COURSEKEY like'" + sem + year + "%'  order by c.COURSECODE asc ";
        return obj_db.get_viewData(sql, "CourseList");
    }

    public DataTable get_all_offeredCourses( string courseKey)
    {
        string sql = " SELECT * FROM OFFEREDCOURSE, CHANGEDCOURSENAME,COURSEINDEPARTMENT ";
        sql += " WHERE COURSEKEY LIKE'" + courseKey + "%' AND (CHANGEDCOURSENAME.COURSECODE=OFFEREDCOURSE.COURSECODE AND OFFEREDCOURSE.COURSECODE=COURSEINDEPARTMENT.COURSECODE) ";
               sql += " ORDER BY DEPCODE ASC, OFFEREDCOURSE.COURSECODE ASC ";
        return obj_db.get_viewData(sql, "CourseList");
    }

    public string  get_latest_creditHours_ofA_course(string courseCode)
    {
        string credits = "";
        DataSet ds = new DataSet();
        string sql = " SELECT * from CHANGEDCREDIT where COURSECODE='" + courseCode + "' order by CHANGEYEAR desc, CHANGESEMESTER desc ";
        ds.Merge(obj_db.get_viewData(sql, "CHANGEDCREDIT"));
        if (ds.Tables["CHANGEDCREDIT"].Rows.Count > 0)
        {
            credits=ds.Tables["CHANGEDCREDIT"].Rows[0]["CREDITHRS"].ToString();
        }

        return credits; 
    }
    


    public DataTable get_prerequisite(string coureCode, string depCode)
    {
        string sql = " SELECT  * FROM COURSEPREREQUISITE where COURSECODE='" + coureCode + "' and DEPCODE='"+depCode+"'  ";
        
        return obj_db.get_viewData(sql, "prerequisiteList");
    }


    public DataTable get_pre_offerigDate(string year, string sem)
    {
        string sql = " SELECT *from WEB_PRE_OFFERING_DATE WHERE SEMESTER='" + sem +"'AND YEAR='" +year + "' ";
        return obj_db.get_viewData(sql, "WEB_PRE_OFFERING_DATE");
    }

    public DataTable get_pre_offeringDate(string sem, string year)
    {
        string sql = " Select * from WEB_PRE_OFFERING_DATE where SEMESTER='" + sem + "'and YEAR='"+year+"' ";
        return obj_db.get_viewData(sql, "WEB_PRE_OFFERING_DATE");
    }


    public bool check_login(string user_id, string user_pass)
    {

        DataSet ds = new DataSet();
        bool status = false;

        string sql = @"Select * from WEB_ADMINS where USERID='" + user_id + "' and PASSWORD='" + user_pass + "' and  CTRL=1  ";
        ds.Merge(obj_db.get_viewData(sql, "WEB_ADMINS"));

        if (ds.Tables["WEB_ADMINS"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["WEB_ADMINS"].Rows[0];
            if (dr["USERID"].ToString() != "" && dr["PASSWORD"].ToString() != "")
            {
                if (dr["USERID"].ToString() == user_id && dr["PASSWORD"].ToString() == user_pass)
                {
                    status = true;
                }

            }
        }

        return status;
    }



    public DataTable get_allAdvisor()
    {
        string sql = " Select * from WEB_TEACHER_STAFF where JOB_CATEGORY='Teacher'and STAFF_CTRL=1 order by STAFF_NAME";
        return obj_db.get_viewData(sql, "advisorList");
    }

    public DataTable get_allAdvisor(string depId)
    {
        string sql = " Select * from WEB_TEACHER_STAFF where JOB_CATEGORY='Teacher' and STAFF_CTRL=1 and DEPARTMENT='" + depId + "' order by STAFF_NAME";
        return obj_db.get_viewData(sql, "advisorList");
    }

    public DataTable get_all_teacher()
    {
        string sql = " Select * from WEB_TEACHER_STAFF where JOB_CATEGORY='Teacher'and STAFF_CTRL=1 ";
        return obj_db.get_viewData(sql, "advisorList");
    }


    public DataTable get_general_notice()
    {
        string sql = @" Select * from WEB_NOTICE_BOARD where FOR_GENERAL=1 and CTRL=1 order by NOTICE_ID desc ";
        return obj_db.get_viewData(sql, "WEB_NOTICE_BOARD");
    }

    public DataTable get_all_events()
    {
        string sql = @" Select * from WEB_NEWS_EVENTS where TYPE=2 and EVENT_CTRL=1 order by NEWS_EVENT_ID desc ";
        return obj_db.get_viewData(sql, "WEB_NEWS_EVENTS");
    }

    public DataTable get_all_academic_calender_forA_semester(string sem, string year)
    {
        string sql = @" Select * from WEB_ACADEMIC_CALENDER where YEAR='" + year + "' and SEMESTER='" + sem + "' order by FROM_DATE asc ";
        return obj_db.get_viewData(sql, "WEB_ACADEMIC_CALENDER");
    }


    public DataTable get_custom_academic_calender_forA_semester(string sem, string year)
    {
        string sql = @" Select * from WEB_ACADEMIC_CALENDER where YEAR='" + year + "' and SEMESTER='" + sem + "' and (EVENT like'%Orientation Program%' or EVENT like'%Classes start%') order by FROM_DATE asc ";
        return obj_db.get_viewData(sql, "WEB_ACADEMIC_CALENDER");
    }


    public DataTable get_all_academic_holidays_forA_semester(string sem, string year)
    {
        string sql = @" Select * from WEB_ACADEMIC_HOLIDAYS where YEAR='" + year + "' and SEMESTER='" + sem + "' order by FROM_DATE asc ";
        return obj_db.get_viewData(sql, "WEB_ACADEMIC_HOLIDAYS");
    }


    public DataTable get_all_news()
    {
        string sql = @" Select * from WEB_NEWS_EVENTS where TYPE=1 order by NEWS_EVENT_ID desc ";
        return obj_db.get_viewData(sql, "WEB_NEWS_EVENTS");
    }

    public DataTable get_a_news_event(string news_event_id)
    {
        string sql = @" Select * from WEB_NEWS_EVENTS where NEWS_EVENT_ID='" + news_event_id + "' ";
        return obj_db.get_viewData(sql, "WEB_NEWS_EVENTS");
    }

    public int set_genarated_pass(DataSet ds)
    {
        int count=0;
        string sql = "";
        foreach (DataRow dr in ds.Tables["STUDENT"].Rows)
        {
            sql = @" update STUDENT set SPASSWORD='" + dr["SPASSWORD"].ToString() + "' where SID='" + dr["sid"].ToString() + "' ";
            if (obj_db.execute_query(sql)=="1")
            {
                count++;            
            }
        }
        return count;
    }    


    public DataTable get_all_evaluation_statement()
    {
        string sql = @" Select * from WEB_TEACHER_EVAL_ARGUMENT order by ID asc  ";
        return obj_db.get_viewData(sql, "WEB_TEACHER_EVAL_ARGUMENT");
    }

    public DataTable get_semester_schedule()
    {
        string sql = @" Select * from SEMESTER  ";
        return obj_db.get_viewData(sql, "SEMESTER");
    }

    public DataTable get_a_academicCalender_details(string id)
    {
        string sql = @" Select * from WEB_ACADEMIC_CALENDER where ID='" + id + "' ";
        return obj_db.get_viewData(sql, "WEB_ACADEMIC_CALENDER");
    }

    public DataTable get_a_academic_holiday_details(string id)
    {
        string sql = @" Select * from WEB_ACADEMIC_HOLIDAYS where ID='" + id + "' ";
        return obj_db.get_viewData(sql, "WEB_ACADEMIC_HOLIDAYS");
    }

    public string delete_a_academicCalender(string id)
    {
        string sql = @" delete from WEB_ACADEMIC_CALENDER where ID='" + id + "' ";
        return obj_db.execute_query(sql);
    }

    public string delete_a_academic_holiday(string id)
    {
        string sql = @" delete from WEB_ACADEMIC_HOLIDAYS where ID='" + id + "' ";
        return obj_db.execute_query(sql);
    }

    public string get_currentSem_id()
    {
        string sem="";
        if(DateTime.Today.Month>=1 && DateTime.Today.Month<=4)
            sem = "1";
        else if(DateTime.Today.Month>=5 && DateTime.Today.Month<=8)
             sem = "2";
        else if(DateTime.Today.Month>=9&& DateTime.Today.Month<=12)
             sem = "3";

         return sem;

    }


    public DataTable get_course_teacher_list(string collegeCode, string sem, string year)
    {
        string sql = @" SELECT vct.*, wt.DEPARTMENT,(SELECT COUNT(*) FROM WEB_TEACHER_EVAL_ARGUMENT)AS arg_qty ";
               sql +="  FROM WEB_VIEW_COURSE_TEACHER vct, WEB_TEACHER_STAFF wt ";
               sql += "  WHERE (vct.TEACHER_ID= wt.STAFF_ID AND wt.DEPARTMENT='" + collegeCode + "') AND vct.COURSE_KEY LIKE'"+sem+year+"%' ";
               sql += "  ORDER BY teacher_id,coursecode ASC ";
        
        return obj_db.get_viewData(sql, "course_teacher");
    }
	
	public DataTable get_course_teacher_list(string collegeCode, string staffId, string sem, string year)
    {
        string sql = @" SELECT vct.*, wt.DEPARTMENT,(SELECT COUNT(*) FROM WEB_TEACHER_EVAL_ARGUMENT)AS arg_qty ";
        sql += "  FROM WEB_VIEW_COURSE_TEACHER vct, WEB_TEACHER_STAFF wt ";
        sql += "  WHERE (vct.TEACHER_ID= wt.STAFF_ID and wt.STAFF_ID='" + staffId + "' AND wt.DEPARTMENT='" + collegeCode + "') AND vct.COURSE_KEY LIKE'" + sem + year + "%' ";
        sql += "  ORDER BY teacher_id,coursecode ASC ";

        return obj_db.get_viewData(sql, "course_teacher");
    }
	
    public DataTable get_course_teacher_Eval_list( string sem, string year)
    {
        string sql = @"  SELECT evs.* ";
               sql += " FROM WEB_VIEW_EVALUATION_SUMMERY evs, WEB_VIEW_COURSE_TEACHER ct ";
               sql += " WHERE evs.COURSE_TEACHER=ct.COURSE_TEACHER_ID AND COURSE_KEY LIKE '"+sem+year+"%'";

        return obj_db.get_viewData(sql, "course_teacher_eval");
    }

    public DataTable get_course_teacher_Eval_Details(string course_teacherID)
    {
        string sql = @" SELECT * FROM WEB_TEACHER_EVAL_VALUE,WEB_VIEW_COURSE_TEACHER ";
        sql += " WHERE COURSE_TEACHER='" + course_teacherID + "' ";
        sql += " AND WEB_TEACHER_EVAL_VALUE.COURSE_TEACHER= WEB_VIEW_COURSE_TEACHER.COURSE_TEACHER_ID ";

        return obj_db.get_viewData(sql, "course_teacher_eval_details");
    }



    public DataTable get_a_notice(string notice_id)
    {
        string sql = " Select * from WEB_NOTICE_BOARD where NOTICE_ID='" + notice_id + "' ";
        return obj_db.get_viewData(sql, "WEB_NOTICE_BOARD");
    }

    public DataTable get_filtered_notice(DateTime fromDate, DateTime toDate, string type)
    {
        toDate = toDate.AddHours(23);
        string sql = " Select * from WEB_NOTICE_BOARD where PUBLISH_DATE>='" + new cls_tools().get_database_formateDate(fromDate) + "'and PUBLISH_DATE<='" + new cls_tools().get_database_formateDate(toDate) + "'and CTRL='" + type + "' order by NOTICE_ID desc ";
        return obj_db.get_viewData(sql, "WEB_NOTICE_BOARD");
    }


    public DataTable get_filtered_news_event(DateTime fromDate, DateTime toDate)
    {
        toDate = toDate.AddHours(23);
        string sql = " Select * from WEB_NEWS_EVENTS where FROM_DATE>='" + new cls_tools().get_database_formateDate(fromDate) + "'and FROM_DATE<='" + new cls_tools().get_database_formateDate(toDate) + "' order by NEWS_EVENT_ID desc ";
        return obj_db.get_viewData(sql, "WEB_NEWS_EVENTS");
    }



    public DataTable get_allcated_teacher_ofa_course(string courseKey)
    {
        string sql = " Select distinct* from WEB_VIEW_COURSE_TEACHER where COURSE_KEY='" + courseKey + "' order by STAFF_NAME asc ";
        return obj_db.get_viewData(sql, "WEB_VIEW_COURSE_TEACHER");
    } 

    public string save_pre_course_offeringDate(DataSet ds, string year, string sem)
    {
        obj_db.execute_query(" Delete from WEB_PRE_OFFERING_DATE where YEAR='"+year+"' and SEMESTER='"+sem+"'  ");
        return obj_db.insert_general(ds, "WEB_PRE_OFFERING_DATE");
    }


    public string save_newTeacher_info(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
        {
            string code = obj_db.get_pk_no("staff");
            sql = " Insert into WEB_TEACHER_STAFF (STAFF_ID, STAFF_NAME, P_ADDRESS, PER_ADDRESS, PHONE_NUMBER, MOBILE, E_MAIL, DEPARTMENT, JOB_TYPE, JOB_CATEGORY, JOB_DESIGNATION, JOIN_DATE, LOGIN_NAME, PASSWORD, STAFF_CTRL)";
            sql += " values ('" +"HRT"+code + "', '" + dr["STAFF_NAME"] + "', '" + dr["P_ADDRESS"] + "', '" + dr["PER_ADDRESS"] + "', '" + dr["PHONE_NUMBER"] + "', '" + dr["MOBILE"] + "', '" + dr["E_MAIL"] + "', '" + dr["DEPARTMENT"] + "', '" + dr["JOB_TYPE"] + "', '" + dr["JOB_CATEGORY"] + "', '" + dr["JOB_DESIGNATION"] + "', '" + dr["JOIN_DATE"] + "', '" + dr["LOGIN_NAME"] + "', '" + dr["PASSWORD"] + "', '" + dr["STAFF_CTRL"] + "') ";
            update_code("staff", code);
            return obj_db.execute_query(sql);
        }
        return "";
    }


    public string update_teacher_info(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
        {
            sql = " update WEB_TEACHER_STAFF set STAFF_NAME= '" + dr["STAFF_NAME"] + "',  P_ADDRESS='" + dr["P_ADDRESS"] + "', PER_ADDRESS='" + dr["PER_ADDRESS"] + "' , ";
            sql += " PHONE_NUMBER='" + dr["PHONE_NUMBER"] + "' , MOBILE='" + dr["MOBILE"] + "', E_MAIL='" + dr["E_MAIL"] + "', ";
            sql += " DEPARTMENT='" + dr["DEPARTMENT"] + "', JOB_TYPE='" + dr["JOB_TYPE"] + "', JOB_CATEGORY='" + dr["JOB_CATEGORY"] + "', ";
            sql += " JOB_DESIGNATION='" + dr["JOB_DESIGNATION"] + "', JOIN_DATE= '" + dr["JOIN_DATE"] + "'  , ";
            sql += " PASSWORD= '" + dr["PASSWORD"] + "' where STAFF_ID='" + dr["STAFF_ID"] + "' ";           
       
            return obj_db.execute_query(sql);
        }
        return "";
    }

    public string save_notice(DataSet ds)
    {
        string code = "" + obj_db.get_pk_no("notice");
    
        foreach (DataRow dr in ds.Tables["noticeBoard"].Rows)
        {
            string sql = @" INSERT INTO WEB_NOTICE_BOARD (NOTICE_ID, TITLE, DESCRIPTION, PUBLISH_DATE, INPUT_DATE, FOR_TEACHER, FOR_STUDENT, FOR_GENERAL,  CTRL) ";
            sql += " values ('NTC_" + code + "' , '" + dr["TITLE"] + "', '" + dr["DESCRIPTION"] + "', '" + dr["PUBLISH_DATE"] + "', '" + dr["INPUT_DATE"] + "', '" + dr["FOR_TEACHER"] + "', '" + dr["FOR_STUDENT"] + "', '" + dr["FOR_GENERAL"] + "', '" + dr["CTRL"] + "') ";//, '" + dr["INPUT_BY"] + "'

            update_code("notice", code);
            return obj_db.execute_query(sql);
        }
        return "";
    }

    public string update_notice(DataSet ds)
    { 

        foreach (DataRow dr in ds.Tables["noticeBoard"].Rows)
        {
            string sql = @" update WEB_NOTICE_BOARD set  TITLE='" + dr["TITLE"] + "', DESCRIPTION='" + dr["DESCRIPTION"] + "',   PUBLISH_DATE='" + dr["PUBLISH_DATE"] + "', ";
            sql += " FOR_STUDENT ='" + dr["FOR_STUDENT"] + "', FOR_GENERAL='" + dr["FOR_GENERAL"] + "', FOR_TEACHER='" + dr["FOR_TEACHER"] + "',  CTRL='" + dr["CTRL"] + "' where NOTICE_ID='" + dr["NOTICE_ID"] + "' ";
           
            return obj_db.execute_query(sql);
        }
        return "";
    }
     


    public string save_news_Events(DataSet ds, ref string ids)
    {
        string code = "" + obj_db.get_pk_no("news_event");
        ids = "ENS_" + code;
        foreach (DataRow dr in ds.Tables["WEB_NEWS_EVENTS"].Rows)
        {
            string sql = "";

            if (dr["TYPE"].ToString() == "1")
            {
                sql = @" INSERT INTO WEB_NEWS_EVENTS (NEWS_EVENT_ID, TITLE, DESCRIPTION, FROM_DATE, TO_DATE, INPUT_DATE, EVENT_IMAGE, INPUT_BY, TYPE, EVENT_CTRL) ";
                sql += " values ('" + ids + "' , '" + dr["TITLE"] + "', '" + dr["DESCRIPTION"] + "', '" + dr["FROM_DATE"] + "', '" + dr["TO_DATE"] + "', '" + dr["INPUT_DATE"] + "', '" + dr["EVENT_IMAGE"] + "', '" + dr["INPUT_BY"] + "', '" + dr["TYPE"] + "', '" + dr["EVENT_CTRL"] + "') ";//, '" + dr["INPUT_BY"] + "'
            }
            else
            {
                sql = @" INSERT INTO WEB_NEWS_EVENTS (NEWS_EVENT_ID, TITLE, DESCRIPTION, FROM_DATE, TO_DATE, INPUT_DATE, INPUT_BY, TYPE, EVENT_CTRL) ";
                sql += " values ('" + ids + "' , '" + dr["TITLE"] + "', '" + dr["DESCRIPTION"] + "', '" + dr["FROM_DATE"] + "', '" + dr["TO_DATE"] + "', '" + dr["INPUT_DATE"] + "', '" + dr["INPUT_BY"] + "', '" + dr["TYPE"] + "', '" + dr["EVENT_CTRL"] + "') ";//, '" + dr["INPUT_BY"] + "'
            }


            update_code("news_event", code);
            return obj_db.execute_query(sql);
        }
        return "";
    }


    public string update_news_Events(DataSet ds)
    {
        string sql = "";    
        foreach (DataRow dr in ds.Tables["WEB_NEWS_EVENTS"].Rows)
        {  
            sql = @" update WEB_NEWS_EVENTS set TITLE='"+dr["TITLE"]+"', DESCRIPTION='"+dr["DESCRIPTION"]+"', ";
            sql += "  FROM_DATE='"+dr["FROM_DATE"]+"', TO_DATE='"+dr["TO_DATE"]+"', INPUT_DATE='"+dr["INPUT_DATE"]+"', ";
            sql += "  EVENT_IMAGE='"+dr["EVENT_IMAGE"]+"', TYPE='"+dr["TYPE"]+"', EVENT_CTRL='"+dr["EVENT_CTRL"]+"' ";
            sql += "  where NEWS_EVENT_ID='" + dr["NEWS_EVENT_ID"] + "' ";
           
            return obj_db.execute_query(sql);
        }
        return "";
    }



    public string save_advisor(DataSet ds)
    {
        string status = "1";
        string sql = "1";
        foreach (DataRow dr in ds.Tables["STUDENT"].Rows)
        {
            sql = @" update STUDENT set ADVISOR_ID='" + dr["ADVISOR_ID"] + "' where SID='" + dr["sid"] + "' ";
            if( obj_db.execute_query(sql)!="1")
            status += "1";
        }


        return status;
    }


    public string allocate_teacher(DataSet ds)
    {
        string sql = " ";

        foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
        {
            string code=obj_db.get_pk_no("course_teacher") ;

            sql = @" INSERT into WEB_COURSE_TEACHER (COURSE_TEACHER_ID, COURSE_KEY, TEACHER_ID, SECTION, ACC_CTRL, SCH_CLS_1, SCH_CLS_2, TUT_CLS_1, TUT_CLS_2, TOTAL_CAPACITY)";
            sql += " values ('C_T_0" + code + "','" + dr["COURSE_KEY"] + "', '" + dr["TEACHER_ID"] + "', '" + dr["SECTION"] + "', '" + dr["ACC_CTRL"] + "', '" + dr["SCH_CLS_1"] + "', '" + dr["SCH_CLS_2"] + "', '" + dr["TUT_CLS_1"] + "', '" + dr["TUT_CLS_2"] + "', '" + dr["TOTAL_CAPACITY"].ToString() + "' )";
            update_code("course_teacher", code);
            break;
        }

        return obj_db.execute_query(sql);
    }


    public string add_academic_calender(DataSet ds)
    {
        string sql = " ";

        foreach (DataRow dr in ds.Tables["AC_calender"].Rows)
        {
            string code = obj_db.get_pk_no("ac");

            sql = @" INSERT into WEB_ACADEMIC_CALENDER ( ID, YEAR, SEMESTER, EVENT, FROM_DATE, TO_DATE, COMMENTS, CTRL )";
            sql += " values ('AC" + code + "','" + dr["YEAR"] + "', '" + dr["SEMESTER"] + "', '" + dr["EVENT"] + "', '" + dr["FROM_DATE"] + "', '" + dr["TO_DATE"] + "', '" + dr["COMMENTS"] + "', '" + dr["CTRL"] + "' )";
            update_code("ac", code);
            break;
        }
        return obj_db.execute_query(sql);
    }

    public string add_academic_holidays(DataSet ds)
    {
        string sql = "";
        foreach (DataRow dr in ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Rows)
        {
            string code = obj_db.get_pk_no("ac_h");

            sql = @" INSERT into WEB_ACADEMIC_HOLIDAYS ( ID, YEAR, SEMESTER, DAY_TITLE, FROM_DATE, TO_DATE, COMMENTS, CTRL )";
            sql += " values ('ACH" + code + "','" + dr["YEAR"] + "', '" + dr["SEMESTER"] + "', '" + dr["DAY_TITLE"] + "', '" + dr["FROM_DATE"] + "', '" + dr["TO_DATE"] + "', '" + dr["COMMENTS"] + "', '" + dr["CTRL"] + "' )";
            update_code("ac_h", code);
            break;
        }
        return obj_db.execute_query(sql);
    }



    public string update_academic_calender(DataSet ds)
    {
        string sql = " ";
        foreach (DataRow dr in ds.Tables["AC_calender"].Rows)
        {
            sql = @" Update WEB_ACADEMIC_CALENDER set  YEAR='" + dr["YEAR"] + "', SEMESTER='" + dr["SEMESTER"] + "', EVENT='" + dr["EVENT"] + "', ";
            sql += "  FROM_DATE='" + dr["FROM_DATE"] + "', TO_DATE='" + dr["TO_DATE"] + "', COMMENTS='" + dr["COMMENTS"] + "', CTRL='" + dr["CTRL"] + "' where ID='" + dr["ID"] + "' ";
            break;
        }
        return obj_db.execute_query(sql);
    }

    public string update_academic_holidays(DataSet ds)
    {
        string sql = " ";
        foreach (DataRow dr in ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Rows)
        {
            sql = @" Update WEB_ACADEMIC_HOLIDAYS set  YEAR='" + dr["YEAR"] + "', SEMESTER='" + dr["SEMESTER"] + "', DAY_TITLE='" + dr["DAY_TITLE"] + "', ";
            sql += "  FROM_DATE='" + dr["FROM_DATE"] + "', TO_DATE='" + dr["TO_DATE"] + "', COMMENTS='" + dr["COMMENTS"] + "', CTRL='" + dr["CTRL"] + "' where ID='" + dr["ID"] + "' ";
            break;
        }
        return obj_db.execute_query(sql);

    }



    public string allocate_teacher_update(DataSet ds, string course_teacherId)
    {
        string sql = " ";
        foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
        {
            sql = @" update WEB_COURSE_TEACHER set SECTION='" + dr["SECTION"] + "',  SCH_CLS_1='" + dr["SCH_CLS_1"] + "', SCH_CLS_2='" + dr["SCH_CLS_2"] + "', TOTAL_CAPACITY=  '" + dr["TOTAL_CAPACITY"].ToString() + "', ";
            sql += " TUT_CLS_1='" + dr["TUT_CLS_1"] + "', TUT_CLS_2='" + dr["TUT_CLS_2"] + "' where COURSE_TEACHER_ID='" + course_teacherId + "'  ";
            break;
        }

        return obj_db.execute_query(sql);
    }



    public string make_notice_active_inactive(string noticeId, string status)
    {
        return obj_db.execute_query(" update WEB_NOTICE_BOARD set CTRL='"+status+"' where NOTICE_ID='" + noticeId + "'  ");
    }






    public string update_code(string objects, string stNo)
    {
        string code="";

        int no=Convert.ToInt32(stNo);
        no++;
        if(no<10)
            code="00000000"+no;
        else if(no<100)
            code="0000000"+no;
        else if(no<1000)
            code="000000"+no;
        else if(no<10000)
            code="00000"+no;
        else if(no<100000)
            code="0000"+no;
        else if(no<1000000)
            code="000"+no;
        else if(no<10000000)
            code="00"+no;
        
       string sql = @" update WEB_CODES set SERIAL='" + code + "' where OBJECT='"+objects+"' ";       
        return obj_db.execute_query(sql);
    }


    public bool is_allocate_teacher_exists( string courseKey, string section)
    {
        string sql = @" SELECT * From WEB_COURSE_TEACHER where COURSE_KEY='"+courseKey+"'and SECTION='"+section+"' ";
        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData("" + sql, "WEB_COURSE_TEACHER"));
        if (ds.Tables["WEB_COURSE_TEACHER"].Rows.Count > 0)
            return true;
        else return false;
    }

    public bool is_allocate_teacher_exists(string teacher_id, string courseKey, string section)
    {
        string sql = @" SELECT * From WEB_COURSE_TEACHER where COURSE_KEY='" + courseKey + "'and TEACHER_ID='" + teacher_id + "'  and SECTION='" + section + "' ";
        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData("" + sql, "WEB_COURSE_TEACHER"));
        if (ds.Tables["WEB_COURSE_TEACHER"].Rows.Count > 0)
            return true;
        else return false;
    }


    public DataTable get_allocated_teacher(string courseKey)
    {
        string sql = @" SELECT * From WEB_VIEW_COURSE_TEACHER where COURSE_KEY='" + courseKey + "' ";
        return obj_db.get_viewData("" + sql, "WEB_VIEW_COURSE_TEACHER");        
    }


    public string delete_allocated_teacher(String techer_course_Id)
    {
        string status="1";

        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData(@" SELECT count(*)as total From WEB_STUDENT_ATTENDANCE where COURSE_TEACHER_ID='" + techer_course_Id + "' ", "WEB_STUDENT_ATTENDANCE"));
        if (ds.Tables["WEB_STUDENT_ATTENDANCE"].Rows.Count > 0)
        {
            if (ds.Tables["WEB_STUDENT_ATTENDANCE"].Rows[0]["total"].ToString() != "0")
                return "2";
            else
            {
                if (obj_db.execute_query(" Delete from WEB_COURSE_TEACHER where COURSE_TEACHER_ID='" + techer_course_Id + "'  ") == "1")
                    status = "1";
            }
        }
        return status;
    }




}

