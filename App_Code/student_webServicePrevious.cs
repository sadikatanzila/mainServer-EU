using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;

 
public class student_webService_Prev : System.Web.Services.WebService
{
    Dts obj_db;
    public student_webService_Prev()
    {
        obj_db = new Dts();
    }

	public DataTable getWaiveCourseList(string sid)
    {
        string sql = @"select CHANGEDCOURSENAME.COURSECODE, CHANGEDCOURSENAME.CNAME, max(COURSEWAIVER.CREDIT) CREDIT from COURSEWAIVER, CHANGEDCOURSENAME
                        where COURSEWAIVER.EUCCODE = CHANGEDCOURSENAME.COURSECODE and COURSEWAIVER.SID = '" + sid + @"'
                        group by CHANGEDCOURSENAME.COURSECODE, CHANGEDCOURSENAME.CNAME";

        return obj_db.get_viewData(sql, "WaivedCourse");
    }

    public DataTable getTransferCourseList(string sid)
    {
        string sql = @"select CHANGEDCOURSENAME.COURSECODE, CHANGEDCOURSENAME.CNAME, max(TRANSFER.EUCREDIT) CREDIT from TRANSFER, CHANGEDCOURSENAME
                        where TRANSFER.COURSECODE = CHANGEDCOURSENAME.COURSECODE and TRANSFER.SID = '" + sid + @"'
                        group by CHANGEDCOURSENAME.COURSECODE, CHANGEDCOURSENAME.CNAME";

        return obj_db.get_viewData(sql, "TransferedCourse");
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
        string sql = @" Select * from WEB_COURSE_TEACHER where COURSE_KEY='"+courseKey+"' and SECTION='" + group+ "'  ";
        return obj_db.get_viewData(sql, "course_schedule");
    }

    public DataTable get_semester_class_routine(string regkey)
    {
        string sql = @" SELECT DISTINCT od.*,  ws.* ";
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
        string sql = @" Select * from WEB_PRE_OFFERING_DATE where SEMESTER ='" + sem + "' and YEAR='" + year + "' and ('" + new cls_tools().get_database_formateDate(DateTime.Today) + "'>= EVAL_OPENING and '" + new cls_tools().get_database_formateDate(DateTime.Today) + "'<= EVAL_CLOSING )  ";
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
        string sql = "insert into WEB_TEACHER_EVAL_VALUE (";
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
        
        return obj_db.execute_query(sql);
    }



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

    public DataTable get_all_preOffered_coursesFull(string sid, string sem, string year)
    { 
        string sql = " SELECT t.*, SCH_CLS_1, SCH_CLS_2, TUT_CLS_1, TUT_CLS_2,STAFF_NAME ";
        sql += " FROM WEB_COURSE_OFFERING_TEMP t,  WEB_COURSE_TEACHER,WEB_TEACHER_STAFF ";
        sql += " WHERE REGKEY='" + sid + sem + year + "' ";
        sql += " AND (t.COURSEKEY =WEB_COURSE_TEACHER.COURSE_KEY ";
        sql += " AND WEB_COURSE_TEACHER.TEACHER_ID= WEB_TEACHER_STAFF.STAFF_ID AND WEB_COURSE_TEACHER.SECTION=t.GGROUP)";
	 
	    return obj_db.get_viewData(sql, "pree_offered");
    }



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

    public string get_clearence_status(String regiKey,string semester,string year)
    {
        /*  Status = Accounce + Library + Evaluation    PublishDate
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

            if(dr["LIB_STATUS"].ToString() == "1")
                    status += "1_"; 
            else// if (dr["LIB_STATUS"].ToString() == "1")
            {
                status += "0_"; 
            }

            if(get_evaluation_status(regiKey))
                status += "1_"; 
            else
                status += "0_";

            DataSet ds2 = new DataSet();
            ds2.Merge(new admin_webService().get_pre_offeringDate(semester, year));
            if (ds2.Tables["WEB_PRE_OFFERING_DATE"].Rows.Count > 0)
            {
                foreach (DataRow dr2 in ds2.Tables["WEB_PRE_OFFERING_DATE"].Rows)
                {
                    if (DateTime.Today
                        >= Convert.ToDateTime(dr2["SEM_RESULT_PUBLISH"]))
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
            return false;
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
         string sql = @" SELECT * FROM REGISTATUS, STUDENT ";
                sql += " WHERE REGISTATUS.SID=STUDENT.SID AND (SEMESTER='"+sem+"' AND year='"+year+"') and sprogram='"+dep+"' "; 
        if(!String.IsNullOrEmpty(batch))
            sql += "  AND regkey LIKE'" + batch + "%' ";

        return obj_db.get_viewData(sql, "student");
    }

	public string get_year_and_semester_ofA_student(String sid)
    {
        string yearsem = "";

        string sql = @"SELECT (ADMINYEAR || ADMINSEMETER) AS YEARSEM FROM STUDENT WHERE sid='" + sid + "' ";        
        yearsem = obj_db.get_viewData(sql, "student").Rows[0][0].ToString();

        return yearsem;
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
        string sql = " Select * from STUDENTCREDIT where sid='"+id+"' and SEMESTER='" + sem + "' and YEAR='" + year + "' ";
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

            index++;
        }

        //----------------------- Semester Balance ---------------------------------------------
        return total_payable - total_paid;
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
	
	public bool IsStudentPaidAdvisingLateFee(string sid, string sem, string year)
    {
        DataSet ds = new DataSet();

        string sql = " select * from STUDENTDEBIT where sid='" + sid + "' and year='" + year + "' and semester='" + sem + "' and headsn='32'";
        ds.Merge(obj_db.get_viewData(sql, "STUDENTDEBIT"));

        if (ds.Tables["STUDENTDEBIT"].Rows.Count > 0)
            return true;
        else 
            return false;
    }
}

