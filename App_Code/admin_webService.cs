using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Data.Common;
using System.Data;


public class admin_webService : System.Web.Services.WebService
{
    Dts obj_db;

    public admin_webService()
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
        string sql = "Select * from STUDENT where SPROGRAM='" + prog + "' and SID like '" + batch + "%'  ";
        return obj_db.get_viewData(sql, "studentList");
    }
    public DataTable get_MessagingStudent()
    {

        string sql = " Select distinct * from C_MESSAGE_STUDENT ";
        return obj_db.get_viewData(sql, "C_MESSAGE_STUDENT");
    }
    public string save_MessagingStudent(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["C_MESSAGE_STUDENT"].Rows)
        {
            sql = " Insert into C_MESSAGE_STUDENT (SID, CONTACT)";
            sql += " values ('" + dr["SID"] + "', '" + dr["CONTACT"] + "') ";
            return obj_db.execute_query(sql);
        }
        return "";
    }

   
    /*
    public DataTable get_allStudent_ofA_aBatch_transperred(string prog, string batch)
    {
        string sql = "select Student.SID,Student.SNAME,Student.ADVISOR_ID, count(transfer.SID) as transferred from student left join transfer on  student.SID  = transfer.SID  where SPROGRAM='" + prog + "' and Student.SID like '" + batch + "%' group by Student.SID,Student.SNAME,Student.ADVISOR_ID ";
        return obj_db.get_viewData(sql, "studentList");
    }

    public DataTable get_allStudent_ofA_aBatch_transperred_Only(string prog, string batch)
    {
        string sql = "select Student.SID,Student.SNAME,Student.ADVISOR_ID, count(transfer.SID) as transferred from student inner join transfer on  student.SID  = transfer.SID  where SPROGRAM='" + prog + "' and Student.SID like '" + batch + "%' group by Student.SID,Student.SNAME,Student.ADVISOR_ID ";
        return obj_db.get_viewData(sql, "studentList");
    }

    public DataTable get_allStudent_ofA_aBatch_Not_transperred(string prog, string batch)
    {
        string sql = "select Student.SID,Student.SNAME,Student.ADVISOR_ID, count(transfer.SID) as transferred from student left join transfer on  student.SID  = transfer.SID  where SPROGRAM='" + prog + "' and Student.SID like '" + batch + "%' and transfer.SID is NULL group by Student.SID,Student.SNAME,Student.ADVISOR_ID ";
        return obj_db.get_viewData(sql, "studentList");
    }
    */
    public string change_staff_passwordReset(string EMPLOYEE_ID, string new_pass)
    {
        string sql = @" update WEB_TEACHER_STAFF set PASSWORD='" + new_pass + "' where VALUE='" + EMPLOYEE_ID + "'   ";
        return obj_db.execute_query("" + sql);
    }
    #region User_Authentication


    public string change_staff_password(string pre_pass, string VALUE, string new_pass)
    {
        int count = 0;
        string sql = @" update WEB_TEACHER_STAFF set PASSWORD='" + new_pass + "' where VALUE='" + VALUE + "' and  PASSWORD='" + pre_pass + "' ";

        if (obj_db.execute_query(sql) == "1")
        {
            count = 1;
        }

        return Convert.ToString(count);

        //  return obj_db.execute_query("" + sql);
    }


    public DataTable get_allEmployeeID()
    {
        string sql = " select Value, STAFF_NAME from WEB_TEACHER_STAFF where value is not null and STAFF_CTRL=1 order by value asc";
        return obj_db.get_viewData(sql, "EMPLOYEE");
    }

    public DataTable get_allRole()
    {
        string sql = " Select * from AD_ROLE order by ROLE_NAME asc  ";
        return obj_db.get_viewData(sql, "ROLE");
    }


    public string MenuPermission_Add(DataSet ds)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["AD_MENUPERMISSION"].Rows)
        {
            sql = " insert into AD_MENUPERMISSION (AD_ROLE_ID,AD_MENUPAGE_ID,ISACTIVE)";
            sql += " values ('" + dr["AD_ROLE_ID"] + "', '" + dr["AD_MENUPAGE_ID"] + "' , '" + dr["ISACTIVE"] + "' ) ";

            if (obj_db.execute_query(sql) == "1")
            {
                count = 1;
            }
        }
        // update_code("T_STUDENTDEBIT", code);

        return Convert.ToString(count);
    }


    public string LeaveBalance_Add(DataSet ds)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["HR_LEAVE_BALANCE"].Rows)
        {
            //check insert or update
            DataTable Leave_bl = new DataTable();
            Leave_bl = GetPermission_Leave_bl_Update(Convert.ToString(dr["EMPLOYEE_ID"]), Convert.ToString(dr["LEAVE_ID"]));
            if (Leave_bl.Rows.Count <= 0)
            {
                sql = " insert into HR_LEAVE_BALANCE (EMPLOYEE_ID,YEAR,BALANCE,LEAVE_ID,CREATED,CREATEDBY)";
                sql += " values ('" + dr["EMPLOYEE_ID"] + "', '" + dr["YEAR"] + "' , '" + dr["BALANCE"] + "','" + dr["LEAVE_ID"] + "', TO_DATE('" + dr["CREATED"] + "', 'dd/mm/yyyy hh24:mi:ss')  , '" + dr["CREATEDBY"] + "' ) ";

                if (obj_db.execute_query(sql) == "1")
                {
                    count ++;
                }
            }
            else
            {
                sql = "Update HR_LEAVE_BALANCE set BALANCE='" + dr["BALANCE"] + "',UPDATED= TO_DATE('" + dr["CREATED"] + "', 'dd/mm/yyyy hh24:mi:ss'), UPDATEDBY='" + dr["CREATEDBY"] + "'   where EMPLOYEE_ID ='" + dr["EMPLOYEE_ID"] + "' and LEAVE_ID='" + dr["LEAVE_ID"] + "' ";

                if (obj_db.execute_query(sql) == "1")
                {
                    count++;
                }
            }

           
        }
        // update_code("T_STUDENTDEBIT", code);

        return Convert.ToString(count);
    }


    public DataTable GetPermission_Leave_bl_Update(string EMPLOYEE_ID, string LEAVE_ID)
    {
        string sql = "select * from HR_LEAVE_BALANCE  WHERE EMPLOYEE_ID = '" + EMPLOYEE_ID + "' and LEAVE_ID ='" + LEAVE_ID + "'  ";

        return obj_db.get_viewData(sql, "leavelist");
    }
    public string MenuPermission_Update(DataSet ds)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["AD_MENUPERMISSION"].Rows)
        {
            sql = " Update AD_MENUPERMISSION set ISACTIVE='" + dr["ISACTIVE"] + "' where AD_ROLE_ID ='" + dr["AD_ROLE_ID"] + "' and AD_MENUPAGE_ID='" + dr["AD_MENUPAGE_ID"] + "'";

            if (obj_db.execute_query(sql) == "1")
            {
                count = 1;
            }
        }
        // update_code("T_STUDENTDEBIT", code);

        return Convert.ToString(count);
    }
    public string insert_PageAuthentication(DataSet ds)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["AD_MENUPERMISSION"].Rows)
        {
            sql = " insert into AD_MENUPERMISSION (AD_ROLE_ID,AD_MENUPAGE_ID,ISACTIVE)";
            sql += " values ('" + dr["AD_ROLE_ID"] + "', '" + dr["AD_MENUPAGE_ID"] + "' , '" + dr["ISACTIVE"] + "' ) ";

            if (obj_db.execute_query(sql) == "1")
            {
                count = 1;
            }
        }
        // update_code("T_STUDENTDEBIT", code);

        return Convert.ToString(count);

    }
    public string update_PageAuthentication(DataSet ds)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["AD_MENUPERMISSION"].Rows)
        {
            sql = " Update AD_MENUPERMISSION set ISACTIVE='" + dr["ISACTIVE"] + "' where AD_ROLE_ID ='" + dr["AD_ROLE_ID"] + "' and AD_MENUPAGE_ID='" + dr["AD_MENUPAGE_ID"] + "'";

            if (obj_db.execute_query(sql) == "1")
            {
                count = 1;
            }
        }
        return Convert.ToString(count);

    }

    public string update_PageAuthentication_pagewise(DataSet ds)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["AD_MENUPERMISSION"].Rows)
        {
            sql = " Update AD_MENUPERMISSION set ISACTIVE='" + dr["ISACTIVE"] + "' where AD_ROLE_ID ='" + dr["AD_ROLE_ID"] + "' and AD_MENUPAGE_ID='" + dr["AD_MENUPAGE_ID"] + "'";

            if (obj_db.execute_query(sql) == "1")
            {
                count = 1;
            }
        }
        return Convert.ToString(count);

    }
    public DataTable CreatePagelist()
    {
        string sql = " select AD_MENUPAGE.*, AD_MENUHEAD.HEADNAME from AD_MENUPAGE left join AD_MENUHEAD on AD_MENUHEAD.AD_MENUHEAD_ID =AD_MENUPAGE.AD_MENUHEAD_ID ";

        return obj_db.get_viewData(sql, "AllPageList");
    }

    public DataTable CreateRolelist()
    {
        string sql = "select ad_role.* from ad_role order by ROLE_NAME asc ";

        return obj_db.get_viewData(sql, "AllRoleList");
    }

    public DataTable BindRole()
    {
        string sql = "select AD_ROLE.* from AD_ROLE order by ROLE_NAME asc";

        return obj_db.get_viewData(sql, "AllRoleList");

    }
    public DataTable GetRole_Permission_Update(Int32 ROLE_ID, string EMPLOYEE_ID)
    {
        string sql = "SELECT DISTINCT AD_ROLE.*,  AD_EMPLOYEE_ROLE.ROLE_ID, WEB_TEACHER_STAFF.STAFF_ID  FROM AD_ROLE  left join AD_EMPLOYEE_ROLE on  AD_EMPLOYEE_ROLE.ROLE_ID = AD_ROLE.ID  ";
        sql += " left join WEB_TEACHER_STAFF on WEB_TEACHER_STAFF.VALUE=AD_EMPLOYEE_ROLE.EMPLOYEE_ID WHERE  AD_EMPLOYEE_ROLE.ROLE_ID = '" + ROLE_ID + "'  and WEB_TEACHER_STAFF.VALUE='" + EMPLOYEE_ID + "'   ";

        return obj_db.get_viewData(sql, "RoleList");
    }

    public string RolePermission_Add(DataSet ds)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["AD_EMPLOYEE_ROLE"].Rows)
        {
            sql = " insert into AD_EMPLOYEE_ROLE (EMPLOYEE_ID,ROLE_ID,IS_ACTIVE)";
            sql += " values ('" + dr["EMPLOYEE_ID"] + "', '" + dr["ROLE_ID"] + "' , '" + dr["IS_ACTIVE"] + "' ) ";

            if (obj_db.execute_query(sql) == "1")
            {
                count = 1;
            }
        }
        // update_code("T_STUDENTDEBIT", code);

        return Convert.ToString(count);
    }

    public string RolePermission_Update(DataSet ds)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["AD_EMPLOYEE_ROLE"].Rows)
        {
            sql = " Update AD_EMPLOYEE_ROLE set IS_ACTIVE='" + dr["IS_ACTIVE"] + "' where ROLE_ID ='" + dr["ROLE_ID"] + "' and EMPLOYEE_ID='" + dr["EMPLOYEE_ID"] + "'";

            if (obj_db.execute_query(sql) == "1")
            {
                count = 1;
            }
        }
        // update_code("T_STUDENTDEBIT", code);

        return Convert.ToString(count);
    }

    public DataTable GetPermission_Controls_DataTable_Update(Int32 AD_MENUPAGE_ID, string userRole)
    {
        string sql = " SELECT  AD_MENUPAGE.*, AD_ROLE.ID FROM AD_MENUPAGE left join AD_MENUPERMISSION on AD_MENUPERMISSION.AD_MENUPAGE_ID  = AD_MENUPAGE.AD_MENUPAGE_ID ";
        sql += " left join AD_MENUHEAD on AD_MENUPAGE.AD_MENUHEAD_ID = AD_MENUHEAD.AD_MENUHEAD_ID left join AD_ROLE on AD_MENUPERMISSION.AD_ROLE_ID = AD_ROLE.ID ";
        sql += "  WHERE   AD_MENUPERMISSION.AD_ROLE_ID = '" + userRole + "' and AD_MENUPAGE.AD_MENUPAGE_ID ='" + AD_MENUPAGE_ID + "'  ";

        return obj_db.get_viewData(sql, "AllPageList");
    }
    public DataTable GetPermission_Controls_DataTable(Int32 AD_MENUPAGE_ID, string userRole)
    {
        string sql = " SELECT  AD_MENUPAGE.*, AD_ROLE.ID FROM AD_MENUPAGE left join AD_MENUPERMISSION on AD_MENUPERMISSION.AD_MENUPAGE_ID  = AD_MENUPAGE.AD_MENUPAGE_ID ";
        sql += " left join AD_MENUHEAD on AD_MENUPAGE.AD_MENUHEAD_ID = AD_MENUHEAD.AD_MENUHEAD_ID left join AD_ROLE on AD_MENUPERMISSION.AD_ROLE_ID = AD_ROLE.ID ";
        sql += "  WHERE   AD_MENUPERMISSION.AD_ROLE_ID = '" + userRole + "' and AD_MENUPAGE.AD_MENUPAGE_ID ='" + AD_MENUPAGE_ID + "' and AD_MENUPERMISSION.ISACTIVE ='Y'  ";

        return obj_db.get_viewData(sql, "AllPageList");
    }

    public DataTable GetRole_Permission_Controls(Int32 ROLE_ID, string userRole)
    {
        string sql = " SELECT AD_ROLE.* FROM AD_ROLE left join AD_EMPLOYEE_ROLE on AD_EMPLOYEE_ROLE.ROLE_ID  = AD_ROLE.ID ";
        sql += "  WHERE   AD_ROLE.ID = '" + ROLE_ID + "' and AD_EMPLOYEE_ROLE.EMPLOYEE_ID ='" + userRole + "' and AD_EMPLOYEE_ROLE.IS_ACTIVE ='Y'  ";

        return obj_db.get_viewData(sql, "AllPageList");
    }
    public DataTable GetPage_asUser(string userRole, string URL)
    {
        string sql = " select AD_MENUPERMISSION.AD_ROLE_ID ,AD_MENUPAGE.URL, AD_MENUPAGE.FOLDERNAME||'/'||AD_MENUPAGE.URL PathURL from AD_MENUPERMISSION ";
        sql += "inner join AD_MENUPAGE on  AD_MENUPAGE.AD_MENUPAGE_ID = AD_MENUPERMISSION.AD_MENUPAGE_ID ";
        sql += "where AD_MENUPERMISSION.AD_ROLE_ID ='" + userRole + "'  and  AD_MENUPAGE.URL = '" + URL + "' ";

        return obj_db.get_viewData(sql, "PageList");
    }


    public DataTable GetDataTableMenuHead_byUserID(string userRole)
    {
        string sql = " SELECT 	distinct AD_MENUHEAD.*, AD_ROLE.ID FROM AD_MENUHEAD left join AD_MENUPAGE on AD_MENUPAGE.AD_MENUHEAD_ID = AD_MENUHEAD.AD_MENUHEAD_ID ";
        sql += " left join AD_MENUPERMISSION on AD_MENUPERMISSION.AD_MENUPAGE_ID  = AD_MENUPAGE.AD_MENUPAGE_ID ";
        sql += " left join AD_ROLE on AD_MENUPERMISSION.AD_ROLE_ID = AD_ROLE.ID where AD_ROLE.ID = '" + userRole + "' order by AD_MENUHEAD.AD_MENUHEAD_ID asc ";

        return obj_db.get_viewData(sql, "MenuHeadList");
    }


    public DataTable GetDataTableHR_MenuPageWeb(Int32 MenuHeadID)
    {
        string sql = " SELECT AD_MENUPAGE.*,  AD_MENUHEAD.HEADNAME , AD_MENUPAGE.FOLDERNAME||'/'||AD_MENUPAGE.URL PathURL FROM  AD_MENUPAGE left join AD_MENUHEAD on AD_MENUHEAD.AD_MENUHEAD_ID =AD_MENUPAGE.AD_MENUHEAD_ID ";
        sql += " WHERE (AD_MENUHEAD.AD_MENUHEAD_ID =  '" + MenuHeadID + "') AND (AD_MENUPAGE.isactive = 'Y') ";

        return obj_db.get_viewData(sql, "MenuPageList");
    }

    public DataTable GetDataTableHR_MenuPage_Web(Int32 AD_MENUPAGE_ID)
    {
        string sql = " SELECT AD_MENUPAGE.*,  AD_MENUHEAD.HEADNAME,AD_MENUHEAD.AD_MENUHEAD_ID,  AD_MENUPAGE.FOLDERNAME||'/'||AD_MENUPAGE.URL PathURL FROM  AD_MENUPAGE left join AD_MENUHEAD on AD_MENUHEAD.AD_MENUHEAD_ID =AD_MENUPAGE.AD_MENUHEAD_ID ";
        sql += " WHERE (AD_MENUPAGE.AD_MENUPAGE_ID =  '" + AD_MENUPAGE_ID + "') AND (AD_MENUPAGE.isactive = 'Y') ";

        return obj_db.get_viewData(sql, "MenuPageList");
    }

    public DataTable GetDataTableMenuSubMenuPermission(Int32 userRole, Int32 MenuHeadID, Int32 AD_MENUPAGE_ID)
    {
        string sql = " select AD_MENUPERMISSION.*, AD_MENUHEAD.AD_MENUHEAD_ID, AD_MENUPAGE.URL, AD_MENUPAGE.FOLDERNAME||'/'||AD_MENUPAGE.URL PathURL, AD_MENUPERMISSION.ISACTIVE from AD_MENUPERMISSION ";
        sql += " left join AD_MENUPAGE on AD_MENUPERMISSION.AD_MENUPAGE_ID  = AD_MENUPAGE.AD_MENUPAGE_ID left join AD_MENUHEAD on AD_MENUPAGE.AD_MENUHEAD_ID = AD_MENUHEAD.AD_MENUHEAD_ID ";
        sql += " left join AD_ROLE on AD_MENUPERMISSION.AD_ROLE_ID = AD_ROLE.ID where AD_ROLE.ID='" + userRole + "' AND AD_MENUHEAD.AD_MENUHEAD_ID = '" + MenuHeadID + "' and AD_MENUPAGE.AD_MENUPAGE_ID='" + AD_MENUPAGE_ID + "' AND AD_MENUPERMISSION.ISACTIVE = 'Y' order by AD_MENUHEAD.PRIORITY asc, AD_MENUPAGE.AD_MENUPAGE_ID asc";

        return obj_db.get_viewData(sql, "MenuPermission");
    }

    public DataTable GetDataTableMenuSubMenuPermission(Int32 userRole, Int32 MenuHeadID)
    {
        string sql = " select distinct AD_MENUPERMISSION.*, AD_MENUHEAD.AD_MENUHEAD_ID, AD_MENUPAGE.URL, AD_MENUPAGE.FOLDERNAME||'/'||AD_MENUPAGE.URL PathURL, AD_MENUPERMISSION.ISACTIVE from AD_MENUPERMISSION ";
        sql += " left join AD_MENUPAGE on AD_MENUPERMISSION.AD_MENUPAGE_ID  = AD_MENUPAGE.AD_MENUPAGE_ID left join AD_MENUHEAD on AD_MENUPAGE.AD_MENUHEAD_ID = AD_MENUHEAD.AD_MENUHEAD_ID ";
        sql += " left join AD_ROLE on AD_MENUPERMISSION.AD_ROLE_ID = AD_ROLE.ID where AD_ROLE.ID='" + userRole + "' AND AD_MENUHEAD.AD_MENUHEAD_ID = '" + MenuHeadID + "'  AND AD_MENUPERMISSION.ISACTIVE = 'Y' order by AD_MENUHEAD.PRIORITY asc, AD_MENUPAGE.AD_MENUPAGE_ID asc";

        return obj_db.get_viewData(sql, "MenuPermission");
    }

    public DataTable HR_MenuPermissionWeb_GetByPageId(Int32 AD_MENUPAGE_ID)
    {
        string sql = " select AD_MENUPERMISSION.*, AD_MENUHEAD.AD_MENUHEAD_ID, AD_MENUPAGE.URL, AD_MENUPAGE.FOLDERNAME||'/'||AD_MENUPAGE.URL PathURL from AD_MENUPERMISSION ";
        sql += " left join AD_MENUPAGE on AD_MENUPERMISSION.AD_MENUPAGE_ID  = AD_MENUPAGE.AD_MENUPAGE_ID left join AD_MENUHEAD on AD_MENUPAGE.AD_MENUHEAD_ID = AD_MENUHEAD.AD_MENUHEAD_ID ";
        sql += " left join AD_ROLE on AD_MENUPERMISSION.AD_ROLE_ID = AD_ROLE.ID where AD_MENUPERMISSION.AD_MENUPAGE_ID='" + AD_MENUPAGE_ID + "' AND AD_MENUPERMISSION.ISACTIVE = 'Y'  order by AD_MENUHEAD.PRIORITY asc, AD_MENUPAGE.AD_MENUPAGE_ID asc ";

        return obj_db.get_viewData(sql, "MenuPermission");
    }

    public DataTable GetDataTableHR_MenuPageWeb_p(string userRole)
    {
        string sql = " SELECT DISTINCT AD_MENUHEAD.AD_MENUHEAD_ID, AD_MENUHEAD.HEADNAME, AD_MENUHEAD.PRIORITY , AD_MENUPERMISSION.AD_ROLE_ID , AD_MENUPERMISSION.ISACTIVE ";
        sql += "  FROM AD_MENUHEAD left join AD_MENUPAGE on AD_MENUPAGE.AD_MENUHEAD_ID = AD_MENUHEAD.AD_MENUHEAD_ID ";
        sql += "  INNER JOIN AD_MENUPERMISSION ON AD_MENUPAGE.AD_MENUPAGE_ID = AD_MENUPERMISSION.AD_MENUPAGE_ID  ";
        sql += "  WHERE   AD_MENUPERMISSION.AD_ROLE_ID = '" + userRole + "' ";

        return obj_db.get_viewData(sql, "MenuHeadList");
    }


    public DataTable GetDataTableHR_MenuPage(Int32 MenuPageID)
    {
        string sql = " SELECT AD_MENUPAGE.*, AD_MENUPAGE.FOLDERNAME||'/'||AD_MENUPAGE.URL PathURL, AD_MENUHEAD.HEADNAME FROM  AD_MENUPAGE left join AD_MENUHEAD on AD_MENUHEAD.AD_MENUHEAD_ID =AD_MENUPAGE.AD_MENUHEAD_ID ";
        sql += " WHERE (AD_MENUPAGE.AD_MENUPAGE_ID =  '" + MenuPageID + "') AND (AD_MENUPAGE.isactive = 'Y') ";

        return obj_db.get_viewData(sql, "MenuPageList");
    }


    #endregion
    public string delete_staff_Lvinfo(string user, string Serial, string datetime)
    {
        String sql = "";


        sql = " update HR_STAFF_LEAVE_INFO set IS_ACTIVE= 0, INACTIVE_BY= '" + user + "', INACTIVE_TIME= '" + datetime + "' where LEAVE_INFO_ID='" + Serial + "'  ";


            if (obj_db.execute_query(sql) == "1")
            {
                return obj_db.execute_query(sql);
            }
            else
                return "";
    }


    public string update_staff_info(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
        {
            sql = " update WEB_TEACHER_STAFF set STAFF_NAME= '" + dr["STAFF_NAME"] + "', VALUE= '" + dr["VALUE"] + "', P_ADDRESS='" + dr["P_ADDRESS"] + "', PER_ADDRESS='" + dr["PER_ADDRESS"] + "' , ";
            sql += " PHONE_NUMBER='" + dr["PHONE_NUMBER"] + "' , MOBILE='" + dr["MOBILE"] + "', E_MAIL='" + dr["E_MAIL"] + "', ";
            sql += " DEPARTMENT='" + dr["DEPARTMENT"] + "', JOB_TYPE='" + dr["JOB_TYPE"] + "', JOB_CATEGORY='" + dr["JOB_CATEGORY"] + "', ";
            sql += " JOB_DESIGNATION='" + dr["JOB_DESIGNATION"] + "', JOIN_DATE= TO_DATE('" + dr["JOIN_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss') , CONFIRMATION_DATE = TO_DATE('" + dr["CONFIRMATION_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss')  ";
            sql += "  where STAFF_ID='" + dr["STAFF_ID"] + "'  ";

            return obj_db.execute_query(sql);
        }
        return "";
    }
    public DataTable get_all_Staff()
    {
        string sql = " Select STAFF_ID, STAFF_NAME, VALUE, VALUE||' - '||STAFF_NAME Name from WEB_TEACHER_STAFF where  STAFF_CTRL=1  and value not like 'HRT%'  order by VALUE asc";
        return obj_db.get_viewData(sql, "stafflist");
    }
    public DataTable get_all_LeaveType()
    {
        string sql = " Select * from HR_LEAVE_MASTER where ISACTIVE ='Y' order by NAME asc ";
        return obj_db.get_viewData(sql, "leavelist");
    }

    public string save_Staff_leave_info(DataSet ds)
    {
        String sql = ""; int count = 0;

        foreach (DataRow dr in ds.Tables["HR_STAFF_LEAVE_INFO"].Rows)
        {
            string code = obj_db.get_pk_no("LV");
            sql = " Insert into HR_STAFF_LEAVE_INFO (LEAVE_INFO_ID,STAFF_ID,LEAVE_ID, LEAVE_REASON, CONTACT_ADDRESS, FROM_DATE, TO_DATE,  LEAVE_CONTACT, HANDLED_ON,CREATED,CREATED_BY)";
            sql += " values ('" + "LV" + code + "', '" + dr["STAFF_ID"] + "', '" + dr["LEAVE_ID"] + "', '" + dr["LEAVE_REASON"] + "', '" + dr["CONTACT_ADDRESS"] + "',  TO_DATE('" + dr["FROM_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["TO_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'),   '" + dr["LEAVE_CONTACT"] + "', '" + dr["HANDLED_ON"] + "','" + dr["CREATED"] + "' ,'" + dr["CREATED_BY"] + "' ) ";
            update_code("LV", code);
            if (obj_db.execute_query(sql) == "1")
            {
                return "LV" + code;
            }
           
        }
        return "";
    }
    public string save_Staff_info(DataSet ds)
    {
        String sql = ""; int count = 0;

        foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
        {
            string code = obj_db.get_pk_no("employee");
            sql = " Insert into WEB_TEACHER_STAFF (STAFF_ID,VALUE, STAFF_NAME, P_ADDRESS, PER_ADDRESS, PHONE_NUMBER, MOBILE, E_MAIL, DEPARTMENT, JOB_TYPE, JOB_CATEGORY, JOB_DESIGNATION, JOIN_DATE, LOGIN_NAME, PASSWORD, STAFF_CTRL, CONFIRMATION_DATE)";
            sql += " values ('" + "HRS" + code + "','" + dr["VALUE"] + "', '" + dr["STAFF_NAME"] + "', '" + dr["P_ADDRESS"] + "', '" + dr["PER_ADDRESS"] + "', '" + dr["PHONE_NUMBER"] + "', '" + dr["MOBILE"] + "', '" + dr["E_MAIL"] + "', '" + dr["DEPARTMENT"] + "', '" + dr["JOB_TYPE"] + "', '" + dr["JOB_CATEGORY"] + "', '" + dr["JOB_DESIGNATION"] + "', TO_DATE('" + dr["JOIN_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), '" + dr["LOGIN_NAME"] + "', '" + dr["PASSWORD"] + "', '" + dr["STAFF_CTRL"] + "' , TO_DATE('" + dr["CONFIRMATION_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss')) ";
            update_code("employee", code);

           // 
            if (obj_db.execute_query(sql) == "1")
            {
                save_Staff_Role(ds);
                count = 1;
            }
        }
        return Convert.ToString(count);
    }
    public string save_Staff_Role(DataSet ds)
    {
        String sql = ""; int count = 0;

        foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
        {
            sql = " Insert into AD_EMPLOYEE_ROLE (EMPLOYEE_ID,ROLE_ID) values ('" + dr["VALUE"] + "', '7') ";

            if (obj_db.execute_query(sql) == "1")
            {
                count = 1;
            }
        }
        return "";
    }

    public string save_teacher_Role(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
        {
            sql = " Insert into AD_EMPLOYEE_ROLE (EMPLOYEE_ID,ROLE_ID) values ('" + dr["VALUE"] + "', '8') ";

            return obj_db.execute_query(sql);
        }
        return "";
    }
    public DataTable get_TeachersCourseList_Deptwise(string Year, string Semester, string DEPTCODE, string Teacher_ID)
    {
        string sql = "Select distinct WEB_VIEW_COURSE_TEACHER.* from WEB_VIEW_COURSE_TEACHER   where COURSE_KEY like '" + Semester + "'||'" + Year + "'||'%'  and DEPARTMENT_ID ='" + DEPTCODE + "' and  ('" + Teacher_ID + "' = '0' or VALUE ='" + Teacher_ID + "' )  and SECTION != 'Waiting Group'  order by STAFF_NAME asc, CNAME asc, SECTION asc";
        return obj_db.get_viewData(sql, "Final_EVALUATION");
    }

    public DataTable get_TeachersCourseList(string Semester, string Year, string Teacher_ID)
    {
     //   string sql = "select * from WEB_VIEW_Final_EVALUATION where Year='" + Year + "' and Semester ='" + semester + "' and ('" + DEPARTMENT + "' = '0' or department  ='" + DEPARTMENT + "')  and ('" + Teacher_ID + "' = '0' or Teacher_ID  ='" + Teacher_ID + "') order by STAFF_NAME asc";

        string sql = "Select distinct WEB_VIEW_COURSE_TEACHER.* from WEB_VIEW_COURSE_TEACHER   where COURSE_KEY like '" + Semester + "'||'" + Year + "'||'%' and  ('" + Teacher_ID + "' = '0' or VALUE ='" + Teacher_ID + "' ) and SECTION != 'Waiting Group' order by STAFF_NAME asc, CNAME asc, SECTION asc";
        return obj_db.get_viewData(sql, "Final_EVALUATION");
    }
    //-------------------------Teachers' Final Evaluation 

    public DataTable get_teacher_FinalEve(string Year, String semester, string DEPARTMENT, string Teacher_ID)
    {
        string sql = "select * from WEB_VIEW_Final_EVALUATION where Year='" + Year + "' and Semester ='" + semester + "' and ('" + DEPARTMENT + "' = '0' or department  ='" + DEPARTMENT + "')  and ('" + Teacher_ID + "' = '0' or Teacher_ID  ='" + Teacher_ID + "') order by STAFF_NAME asc";
      //  string sql = "Select distinct WEB_VIEW_COURSE_TEACHER.* from WEB_VIEW_COURSE_TEACHER   where COURSE_KEY like '" + Semester + "'||'" + Year + "'||'%' and  ('" + Teacher_ID + "' = '0' or Teacher_ID ='" + Teacher_ID + "' )  order by STAFF_NAME asc, CNAME asc, SECTION asc";
       
        
        return obj_db.get_viewData(sql, "Final_EVALUATION");
    }

    public DataTable get_TeachersCourseList(string TEACHER_ID, string Year, String semester, string DEPARTMENT)
    {
        string sql = "Select distinct WEB_VIEW_COURSE_TEACHER.* from WEB_VIEW_COURSE_TEACHER   where DEPARTMENT_ID ='" + DEPARTMENT + "' and COURSE_KEY like '" + semester + "'||'" + Year + "'||'%' and  ('" + TEACHER_ID + "' = '0' or Teacher_ID ='" + TEACHER_ID + "' )  order by STAFF_NAME asc, CNAME asc, SECTION asc";
        return obj_db.get_viewData(sql, "TEACHER_COURSELIST");
    }

    public DataTable get_TeachersCourseListAll(string TEACHER_ID, string Year, String semester, string DEPARTMENT)
    {
        string sql = "select distinct WEB_VIEW_COURSE_TEACHER.* from WEB_VIEW_COURSE_TEACHER where TEACHER_ID='" + TEACHER_ID + "' and Year='" + Year + "' and Semester ='" + semester + "' and ('" + DEPARTMENT + "' = '0' or department  ='" + DEPARTMENT + "')  ";
      
        return obj_db.get_viewData(sql, "TEACHER_EVALUATION");
    }

    public DataTable get_teacher_Evaluation(string TEACHER_ID, string Year, String semester, string DEPARTMENT)
    {
        string sql = "select * from WEB_VIEW_EVALUATION_MARK where TEACHER_ID='" + TEACHER_ID + "' and Year='" + Year + "' and Semester ='" + semester + "' and ('" + DEPARTMENT + "' = '0' or department  ='" + DEPARTMENT + "')  ";
        return obj_db.get_viewData(sql, "TEACHER_EVALUATION");
    }

   

    //-----------------------------------------------


    #region Alumni

    //------------------------- alumni Info

    public DataTable get_all_degree()
    {
        string sql = " Select * from DEPARTMENTINCOLLEGE order by DEGREE asc ";
        return obj_db.get_viewData(sql, "degreelist");
    }

    public DataTable get_ALUMNI_List()
    {
        string sql = @" select distinct EU_ALUMNI.*,EU_ALUMNI_SID.SID,college.COLLEGENAME,
                    STUDENT.SNAME,SUBSTR(Student.SID,1,3) batch,STUDENT_BATCH.BATCHNO
                    from EU_ALUMNI
                    left join EU_ALUMNI_SID on EU_ALUMNI_SID.ALUMNI_TRANID=EU_ALUMNI.ALUMNI_TRANID
                    left join Student on Student.SID = EU_ALUMNI_SID.SID
                    left join STUDENT_BATCH on substr(EU_ALUMNI_SID.SID,1,3) = STUDENT_BATCH.BATCH 
                    left join C_PROGINDEPT on C_PROGINDEPT.C_PROGINDEPT_ID = Student.PROGRAM_ID   
                    left join C_DEPARTMENTINFACULTY DP on DP.DEPID =  C_PROGINDEPT.DEPID
                    left join College  on College.COLLEGECODE = DP.COLLEGECODE
                    order by EU_ALUMNI_SID.SID asc";

        return obj_db.get_viewData(sql, "EU_ALUMNI_LIST");
    }







    public DataTable get_ALUMNI_SeachInfo(string Year, String semester, string Degree)
    {
        string sql = "select * from VW_Alumni_Summery where ((FGRYEAR='" + Year + "' and FGRSEM ='" + semester + "' and FDEG ='" + Degree + "') or (SGRYEAR='" + Year + "' and SGRSEM ='" + semester + "' and SDEG ='" + Degree + "')) and S_PICTURE is not null order by FGRYEAR asc,FGRSEM asc, FDEG asc,SGRYEAR asc,SGRSEM asc, SDEG asc";
        return obj_db.get_viewData(sql, "EU_ALUMNI_SearchLIST");
    }

    public DataTable get_ALUMNI_SeachDegreeWise(string Degree)
    {
        string sql = "select * from VW_Alumni_Summery where ((FDEG ='" + Degree + "') or (SDEG ='" + Degree + "')) and S_PICTURE is not null order by FGRYEAR asc,FGRSEM asc, FDEG asc,SGRYEAR asc,SGRSEM asc, SDEG asc";
        return obj_db.get_viewData(sql, "EU_ALUMNI_SearchLIST");
    }

    public DataTable get_ALUMNI_SeachYearSemWise(string Year, String semester)
    {
        string sql = "select * from VW_Alumni_Summery where ((FGRYEAR='" + Year + "' and FGRSEM ='" + semester + "') or (SGRYEAR='" + Year + "' and SGRSEM ='" + semester + "' ))  and S_PICTURE is not null order by FGRYEAR asc,FGRSEM asc, FDEG asc,SGRYEAR asc,SGRSEM asc, SDEG asc";
        return obj_db.get_viewData(sql, "EU_ALUMNI_SearchLIST");
    }

    public DataTable get_ALUMNI_Info()
    {
        string sql = @" select * from VW_Alumni_Summery where S_PICTURE is not null order by  FGRYEAR asc,FGRSEM asc, FDEG asc,SGRYEAR asc,SGRSEM asc, SDEG asc";
        return obj_db.get_viewData(sql, "EU_ALUMNI_LIST");
    }

    public DataTable get_ALUMNI_Profile(string alumni_id)
    {
        string sql = @" select * from VW_Alumni_Summery where ALUMNI_TRANID='" + alumni_id + "' order by  FGRYEAR asc,FGRSEM asc, FDEG asc,SGRYEAR asc,SGRSEM asc, SDEG asc";
        return obj_db.get_viewData(sql, "EU_ALUMNI_LIST");
    }

    //get Registered Alumni Count

    public DataTable get_ALUMNI_Total()
    {
        string sql = @" Select count(ALUMNI_TRANID)Total from(select * from VW_Alumni_Summery where S_PICTURE is not null)";
        return obj_db.get_viewData(sql, "EU_RegAlumniTotal");
    }

    public DataTable get_ALUMNI_YearSemWise(string Year, String semester)
    {
        string sql = "Select count(ALUMNI_TRANID)Total from (select * from VW_Alumni_Summery where ((FGRYEAR='" + Year + "' and FGRSEM ='" + semester + "') or (SGRYEAR='" + Year + "' and SGRSEM ='" + semester + "' ))  and S_PICTURE is not null)";
        return obj_db.get_viewData(sql, "EU_RegAlumni_YearSemWise");
    }

    public DataTable get_ALUMNI_DegreeWise(string Degree)
    {
        string sql = "Select count(ALUMNI_TRANID)Total from (select * from VW_Alumni_Summery where ((FDEG ='" + Degree + "') or (SDEG ='" + Degree + "')) and S_PICTURE is not null )";
        return obj_db.get_viewData(sql, "EU_RegAlumni_DegreeWise");
    }

    public DataTable get_ALUMNI_Seach(string Year, String semester, string Degree)
    {
        string sql = "Select count(ALUMNI_TRANID)Total from (select * from VW_Alumni_Summery where ((FGRYEAR='" + Year + "' and FGRSEM ='" + semester + "' and FDEG ='" + Degree + "') or (SGRYEAR='" + Year + "' and SGRSEM ='" + semester + "' and SDEG ='" + Degree + "')) and S_PICTURE is not null )";
        return obj_db.get_viewData(sql, "EU_ALUMNI_Search");
    }













    public string change_Alumni_password(string ALUMNI_TRANID, string pre_pass, string new_pass)
    {
        string sql = @" update EU_ALUMNI set CPASSWORD ='" + new_pass + "' where ALUMNI_TRANID='" + ALUMNI_TRANID + "'   ";
        //and  PASSWORD='" + pre_pass + "'
        return obj_db.execute_query("" + sql);
    }

    public int UpdateInfoAlumni(DataSet ds, string ALUMNI_TRANID)
    {
        int count = 0;
        string sql = "";
        foreach (DataRow dr in ds.Tables["EU_ALUMNI"].Rows)
        {
            sql = @"update EU_ALUMNI set PRESENT_ADD='" + dr["PRESENT_ADD"].ToString() + "' ,CONTACTNO='" + dr["CONTACTNO"].ToString() + "', EMAIL='" + dr["EMAIL"].ToString() + "' ";
            sql += " , FBLINK='" + dr["FBLINK"].ToString() + "' ,BLOODGROUP='" + dr["BLOODGROUP"].ToString() + "', S_PICTURE='" + dr["S_PICTURE"].ToString() + "',CRNT_JOB_DSG='" + dr["CRNT_JOB_DSG"].ToString() + "'  ";
            sql += " , ORG_NAME='" + dr["ORG_NAME"].ToString() + "' ,ORG_ADDRESS='" + dr["ORG_ADDRESS"].ToString() + "', JOB_DEPT='" + dr["JOB_DEPT"].ToString() + "',CONTACT='" + dr["CONTACT"].ToString() + "'  ";
            sql += " where  ALUMNI_TRANID='" + ALUMNI_TRANID + "' ";
            // return obj_db.execute_query(sql);
            if (obj_db.execute_query(sql) == "1")
            {
                count++;
            }
        }
        update_EU_ALUMNI_SID(ALUMNI_TRANID);
        return count;
    }

    public int insert_Alumni_student(DataSet ds, string TRANID)
    {
        string sql = "";
        int count = 0;
        //  string TRANID = "";

        foreach (DataRow dr in ds.Tables["EU_ALUMNI"].Rows)
        {
            // TRANID = @" ' + dr["ALUMNI_TRANID"] + ' ";
            sql = @" insert into EU_ALUMNI (ALUMNI_TRANID, PASSWORD, PRESENT_ADD, CONTACTNO, EMAIL, FBLINK,BLOODGROUP,S_PICTURE,CRNT_JOB_DSG,ORG_NAME,ORG_ADDRESS,JOB_DEPT,CONTACT, CPASSWORD,FLOGIN)";
            sql += " values ('" + dr["ALUMNI_TRANID"] + "', '" + dr["PASSWORD"] + "', '" + dr["PRESENT_ADD"] + "', '" + dr["CONTACTNO"] + "', '" + dr["EMAIL"] + "', ";
            sql += "'" + dr["FBLINK"] + "',  '" + dr["BLOODGROUP"] + "' , '" + dr["S_PICTURE"] + "',   ";
            sql += "'" + dr["CRNT_JOB_DSG"] + "',  '" + dr["ORG_NAME"] + "', '" + dr["ORG_ADDRESS"] + "', '" + dr["JOB_DEPT"] + "', '" + dr["CONTACT"] + "' , '" + dr["CPASSWORD"] + "', '" + dr["FLOGIN"] + "'   ) ";

            if (obj_db.execute_query(sql) == "1")
            {
                count++;
            }
        }

        update_EU_ALUMNI_SID(TRANID);
        return count;

    }

    //public string delete_Alumni_student(string tranID, string status)
    //{
    //    return obj_db.execute_query(" delete from EU_ALUMNI_SID where ALUMNI_TRANID='" + tranID + "'  ");
    //}

    public string delete_Alumni_student(string tranID, string status)
    {
        return obj_db.execute_query(" delete from EU_ALUMNI_SID where ALUMNI_TRANID='" + tranID + "'  ");
    }

    

    public string update_EU_ALUMNI_SID(string TRANID)
    {
        string sql = @" update EU_ALUMNI_SID set VALID='Y' where ALUMNI_TRANID='" + TRANID + "' ";
        return obj_db.execute_query(sql);
    }


    public int insert_Alumni_SIDN(DataSet ds)
    {
        string sql = "", sql1 = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["EU_ALUMNI_SID"].Rows)
        {
            sql = @" insert into EU_ALUMNI_SID (SID,ALUMNI_TRANID, DEPID, COLLEGECODE, GRADUATIONSEMESTER, GRADUATIONYEAR, CGPA,DEGREENAME,INSERTION)";
            sql += " values ( '" + dr["SID"] + "', '" + dr["ALUMNI_TRANID"] + "', '" + dr["DEPID"] + "', '" + dr["COLLEGECODE"] + "', '" + dr["GRADUATIONSEMESTER"] + "', ";
            sql += "'" + dr["GRADUATIONYEAR"] + "',  '" + dr["CGPA"] + "' , '" + dr["DEGREENAME"] + "' , '" + dr["INSERTION"] + "'  )   ";

            sql1 = @"INSERT into EU_ALUMNI(ALUMNI_TRANID,PASSWORD,CPASSWORD,FLOGIN) Values ( '" + dr["ALUMNI_TRANID"] + "', '" + dr["PASSWORD"] + "', '" + dr["CPASSWORD"] + "', '" + dr["FLOGIN"] + "')";
            if (obj_db.execute_query(sql) == "1" && obj_db.execute_query(sql1) == "1")
            {
                count++;
            }
        }
        return count;

    }

    public int insert_Alumni_SID(DataSet ds)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["EU_ALUMNI_SID"].Rows)
        {
            sql = @" insert into EU_ALUMNI_SID (SID,ALUMNI_TRANID, DEPID, COLLEGECODE, GRADUATIONSEMESTER, GRADUATIONYEAR, CGPA,DEGREENAME,INSERTION)";
            sql += " values ( '" + dr["SID"] + "', '" + dr["ALUMNI_TRANID"] + "', '" + dr["DEPID"] + "', '" + dr["COLLEGECODE"] + "', '" + dr["GRADUATIONSEMESTER"] + "', ";
            sql += "'" + dr["GRADUATIONYEAR"] + "',  '" + dr["CGPA"] + "' , '" + dr["DEGREENAME"] + "' , '" + dr["INSERTION"] + "'  )   ";

            if (obj_db.execute_query(sql) == "1")
            {
                count++;
            }
        }
        return count;

    }



    public int insert_Alumni_SID_New(DataSet ds, string TRANID)
    {
        string sql = "";
        int count = 0;

        foreach (DataRow dr in ds.Tables["EU_ALUMNI_SID"].Rows)
        {
            sql = @" insert into EU_ALUMNI_SID (SID,ALUMNI_TRANID, DEPID, COLLEGECODE, GRADUATIONSEMESTER, GRADUATIONYEAR, CGPA,DEGREENAME,INSERTION)";
            sql += " values ( '" + dr["SID"] + "', '" + dr["ALUMNI_TRANID"] + "', '" + dr["DEPID"] + "', '" + dr["COLLEGECODE"] + "', '" + dr["GRADUATIONSEMESTER"] + "', ";
            sql += "'" + dr["GRADUATIONYEAR"] + "',  '" + dr["CGPA"] + "' , '" + dr["DEGREENAME"] + "' , '" + dr["INSERTION"] + "' )   ";

            if (obj_db.execute_query(sql) == "1")
            {
                count++;
            }
        }

        update_EU_ALUMNI_SID(TRANID);
        return count;

    }​

    public string update_code_Alumni(string objects, string stNo)
    {
        string code = "";

        int no = Convert.ToInt32(stNo);
        no++;

        if (no < 10)
            code = "00000" + no;
        else if (no < 100)
            code = "0000" + no;
        else if (no < 1000)
            code = "000" + no;
        else if (no < 10000)
            code = "00" + no;
        //else if (no < 100000)
        //    code = "0000" + no;
        //else if (no < 1000000)
        //    code = "000" + no;
        //else if (no < 10000000)
        //    code = "00" + no;

        string sql = @" update WEB_CODES set SERIAL='" + code + "' where OBJECT='" + objects + "' ";
        return obj_db.execute_query(sql);
    }
    //----------------------end process
    #endregion
    //-------------------------Teachers' Evaluation Summery

    public DataTable get_teacher_EveSummery(string DEPARTMENT, string Teacher_ID)
    {
        string sql = "select WEB_VIEW_AVG_YEARSEM.*, Round(AVGEVE, 6) EvalSum from WEB_VIEW_AVG_YEARSEM where ('" + DEPARTMENT + "' = '0' or departmentID  ='" + DEPARTMENT + "')  and ('" + Teacher_ID + "' = '0' or Teacher_ID  ='" + Teacher_ID + "') order by STAFF_NAME asc";
        return obj_db.get_viewData(sql, "Summery_EVALUATION");
    }
    public DataTable get_teacher_EveSummerySearch(string Year, string Semester, string Mark, string GrterSmaller)
    {
        string sql = "Select WEB_VIEW_FINAL_EVALUATION.*, Round(WEB_VIEW_AVG_YEARSEM.AVGEVE,2) AVGEVE, WEB_VIEW_AVG_YEARSEM.DEPARTMENT DEPARTMENTNAME from WEB_VIEW_FINAL_EVALUATION left join WEB_VIEW_AVG_YEARSEM on WEB_VIEW_AVG_YEARSEM.teacher_id =WEB_VIEW_FINAL_EVALUATION.TEACHER_ID where WEB_VIEW_FINAL_EVALUATION.YEAR = '" + Year + "' and WEB_VIEW_FINAL_EVALUATION.semester ='" + Semester + "' and WEB_VIEW_FINAL_EVALUATION.finalevaluation " + GrterSmaller + " '" + Mark + "'  order by WEB_VIEW_AVG_YEARSEM.DEPARTMENT asc, WEB_VIEW_FINAL_EVALUATION.FINALEVALUATION asc ";
        return obj_db.get_viewData(sql, "Summery_EVALUATION");
    }

    public DataTable get_teacher_Evaluation_Summery(string TEACHER_ID, string DEPARTMENT)
    {
        string sql = "select * from WEB_VIEW_FINAL_EVALUATION where TEACHER_ID='" + TEACHER_ID + "'  and ('" + DEPARTMENT + "' = '0' or department  ='" + DEPARTMENT + "') order by year asc, semester asc ";
        return obj_db.get_viewData(sql, "TEACHER_EVALUATION");
    }

    public DataTable get_teacher_CommentsSummery(string DEPARTMENT, string Teacher_ID, string Year, string Semester)
    {
        string sql = " select distinct TEACHER_ID, TEACHER_NAME, JOB_DESIGNATION, DEPARTMENT, DEPARTMENT_NAME  from WEB_VIEW_EVALUATION_COMMENTS where TEACHER_ID='" + Teacher_ID + "'  and ('" + DEPARTMENT + "' = '0' or department  ='" + DEPARTMENT + "') and ('" + Year + "' = '0' or year  ='" + Year + "') and ('" + Semester + "' = '0' or SEMESTER_ID  ='" + Semester + "') order by  DEPARTMENT asc, TEACHER_NAME asc ";
        return obj_db.get_viewData(sql, "Summery_EVALUATION");
    }
    public DataTable get_teacher_Evaluation_Comments(string TEACHER_ID, string DEPARTMENT, string Year, string Semester)
    {
        string sql = "select distinct TEACHER_ID, TEACHER_NAME, JOB_DESIGNATION, YEAR, SEMESTER_ID, SEM, DEPARTMENT, DEPARTMENT_NAME, COURSENAME, SECTION,COURSE_TEACHER_ID   from WEB_VIEW_EVALUATION_COMMENTS where TEACHER_ID='" + TEACHER_ID + "'  and ('" + DEPARTMENT + "' = '0' or department  ='" + DEPARTMENT + "') and ('" + Year + "' = '0' or year  ='" + Year + "') and ('" + Semester + "' = '0' or SEMESTER_ID  ='" + Semester + "')  order by year||SEMESTER_ID desc, TEACHER_NAME asc, COURSENAME asc, SECTION asc ";
        return obj_db.get_viewData(sql, "TEACHER_EVALUATION_COMMENTS");
    }

    public DataTable get_teacher_Evaluation_Commentsdtl(string COURSE_TEACHER_ID)
    {
        string sql = "select *  from WEB_VIEW_EVALUATION_COMMENTS where COURSE_TEACHER_ID='" + COURSE_TEACHER_ID + "' order by SID asc";
        return obj_db.get_viewData(sql, "TEACHER_EVALUATION_COMMENTS_DTL");
    }
    //-----------------------------------------------


    public DataTable get_STUDENT_HEADINFO()
    {
        string sql = "select STUDENTHEADINFO.*,CASE headsn ";
        sql += " WHEN 1  THEN '0'  else '0' END  as Amount from STUDENTHEADINFO ";
        return obj_db.get_viewData(sql, "STUDENTHEADINFO");
    }

    public DataTable get_STUDENT_REGINFO(string SID_chk, string HEADSN_Chk, string YEAR, string SEM)
    {
        string sql = "SELECT COUNT(*) REG_COUNT FROM STUDENTDEBIT SD WHERE SD.SID = '" + SID_chk + "' AND SD.YEAR = '" + YEAR + "' AND SD.SEMESTER = '" + SEM + "' AND SD.HEADSN= " + HEADSN_Chk + " ";
        return obj_db.get_viewData(sql, "STUDENTREGINFO");
    }

    public DataTable get_STUDENT_Payment(string TRAN_ID)
    {
        string sql = @"select T_STUDENTDEBIT.*,CASE T_STUDENTDEBIT.semester
                        WHEN 1 THEN 'Spring' WHEN 2 THEN 'Summer'  WHEN 3 THEN 'Fall' END AS semistername,
                        STUDENTHEADINFO.HEADNAME from T_STUDENTDEBIT inner join STUDENTHEADINFO on STUDENTHEADINFO.headsn = T_STUDENTDEBIT.HEADSN 
                        where TRANID =  '" + TRAN_ID + "' order by STUDENTHEADINFO.SERIAL asc";

        return obj_db.get_viewData(sql, "T_STUDENTDEBIT");
    }

    public DataTable get_STUDENT_HEADINFO_NEW(string Year, string semsester)
    {
        string sql = "select STUDENTHEADINFO.*,CASE headsn ";
        sql += " WHEN 1  THEN '0'  else '0' END  as Amount,'" + Year + "' Year, '" + semsester + "' semester from STUDENTHEADINFO  where ISACTIVE = 'Y'  order by serial asc ";
        return obj_db.get_viewData(sql, "STUDENTHEADINFO");
    }
    public DataTable get_STAFF_INFO(string year)
    {

        string sql = @"SELECT  TS.STAFF_ID, TS.STAFF_NAME, TS.VALUE, TS.VALUE||' - '||TS.STAFF_NAME Name, TS.JOIN_DATE, TS.CONFIRMATION_DATE,                
                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 2), 7) Medical,
                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 7), 0) Earned,
                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 3), 0) Leave_without_Pay,
                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 5), 0) Semester_Brk,
                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 6), 0) Duty_Lv,
                    NVL((SELECT LD.BALANCE FROM HR_LEAVE_BALANCE LD WHERE EMPLOYEE_ID=TS.VALUE AND YEAR=? AND LD.LEAVE_ID = 8), 90) Maternity_Lv,
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

        return obj_db.get_viewData(sql, "STAFFINFO");
    }
    public DataTable get_StdPaymentNew_TranID(string TRAN_ID)
    {
        string sql = "select  sum(T_studentDebit.AMOUNT) Total_Amount from T_studentDebit where T_studentDebit.TRANID ='" + TRAN_ID + "' ";

        return obj_db.get_viewData(sql, "T_studentDebit_Amount");
    }​

    //public DataTable get_STUDENT_Payment(string TRAN_ID)
    //{
    //    string sql = "select T_STUDENTDEBIT.*, STUDENTHEADINFO.HEADNAME from T_STUDENTDEBIT inner join STUDENTHEADINFO on STUDENTHEADINFO.headsn = T_STUDENTDEBIT.HEADSN where TRANID = '" + TRAN_ID + "' ";
    //    return obj_db.get_viewData(sql, "T_STUDENTDEBIT");
    //}

    public DataTable get_StdPayment_TranID(string TRAN_ID)
    {
        string sql = "select  T_studentDebit.YEAR, T_studentDebit.SEMESTER, case T_studentDebit.SEMESTER when 1 then 'Spring' when 2 then 'Summer' when 3 then 'Fall' End as Sem, ";
        sql += "sum(T_studentDebit.AMOUNT) Total_Amount from T_studentDebit where T_studentDebit.TRANID='" + TRAN_ID + "' group by T_studentDebit.YEAR, T_studentDebit.SEMESTER";

        return obj_db.get_viewData(sql, "T_studentDebit_Amount");
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

    //----------------------------offline payment

    public string insert_final_stdDebitOffline(DataSet ds)
    {
        string sql = "";
        int count = 0;
        string code = "";

        string objectCode = "T_STUDENTDEBIT_OFF";
        code = obj_db.call_procedure("SP_GET_SEQ_VALUE", objectCode);

        code = format_code(code);


        Session["TRANID"] = "EOF" + "" + code;
        //  Session["TRANID"] = "EU"+""+code;

        foreach (DataRow dr in ds.Tables["T_STUDENTDEBIT"].Rows)
        {
            sql = @" insert into T_STUDENTDEBIT (TRANID,SID, YEAR, SEMESTER, HEADSN, AMOUNT, STATUS,CREATEDBY)";
            sql += " values ('EOF" + code + "', '" + dr["SID"] + "', '" + dr["YEAR"] + "', '" + dr["SEMESTER"] + "', '" + dr["HEADSN"] + "', '" + dr["AMOUNT"] + "', ";
            sql += "'" + dr["STATUS"] + "',  '" + dr["SID"] + "' ) ";

            if (obj_db.execute_query(sql) == "1")
            {
                count++;
            }
        }
        //  update_code("T_STUDENTDEBIT", code);

        return Convert.ToString(Session["TRANID"]);

    }



    public string insert_final_stdDebit(DataSet ds)
    {
        string sql = "";
        int count = 0;
        string code = "";

        string objectCode = "T_STUDENTDEBIT";
        code = obj_db.call_procedure("SP_GET_SEQ_VALUE", objectCode);

        code = format_code(code);


        Session["TRANID"] = "EU" + "" + code;
      //  Session["TRANID"] = "EU"+""+code;

        foreach (DataRow dr in ds.Tables["T_STUDENTDEBIT"].Rows)
        {
            sql = @" insert into T_STUDENTDEBIT (TRANID,SID, YEAR, SEMESTER, HEADSN, AMOUNT, STATUS,CREATEDBY)";
            sql += " values ('EU" + code + "', '" + dr["SID"] + "', '" + dr["YEAR"] + "', '" + dr["SEMESTER"] + "', '" + dr["HEADSN"] + "', '" + dr["AMOUNT"] + "', ";
            sql += "'" + dr["STATUS"] + "',  '" + dr["SID"] + "' ) ";

            if (obj_db.execute_query(sql) == "1")
            {
                count++;
            }
        }
      //  update_code("T_STUDENTDEBIT", code);

        return Convert.ToString(Session["TRANID"]);

    }


     //--------------------transferred student type------------------after get_allStudent_ofA_aBatch
    public DataTable get_allStudent_ofA_aBatch_transperred(string prog, string batch)
    {
        string sql = "select Student.* from student  where SPROGRAM='" + prog + "' and SID like '" + batch + "%'";
        return obj_db.get_viewData(sql, "studentList");
    }

    public DataTable get_allStudent_ofA_aBatch_transperred_Only(string prog, string batch)
    {
        string sql = "select Student.* from student  where status like  'Transfer' and SPROGRAM='" + prog + "' and SID like '" + batch + "%'";
        return obj_db.get_viewData(sql, "studentList");
    }

    public DataTable get_allStudent_ofA_aBatch_Not_transperred(string prog, string batch)
    {
        string sql = "select Student.* from student  where status not like  'Transfer' and SPROGRAM='" + prog + "' and SID like '" + batch + "%'";
        return obj_db.get_viewData(sql, "studentList");
    }
    //--------------------------------------------------------

    public DataTable get_allDepartment()
    {
        string sql = " Select * from DEPARTMENTINCOLLEGE order by DEPCODE asc  ";
        return obj_db.get_viewData(sql, "departmentList");
    }


    public DataTable get_allDepartment_New()
    {
        string sql = " Select * from C_PROGINDEPT order by NAME asc  ";
        return obj_db.get_viewData(sql, "departmentList");
    }

    public DataTable get_allCollege()
    {
        string sql = " Select * from COLLEGE order by COLLEGENAME asc  ";
        return obj_db.get_viewData(sql, "COLLEGE");
    }

    public string get_departmentName(string depCode)
    {
        string depName = "";

        DataSet ds = new DataSet();
        string sql = " Select * from DEPARTMENTINCOLLEGE where DEPCODE = '" + depCode + "'";
        ds.Merge(obj_db.get_viewData(sql, "DEPARTMENTINCOLLEGE"));
        foreach (DataRow dr in ds.Tables["DEPARTMENTINCOLLEGE"].Rows)
        {
            depName = dr["DEPNAME"].ToString();
        }
        return depName;
    }

    public DataTable getall_offeredCourses_yearSem(string course_teacherId)
    {
        string sql = @" SELECT distinct substr(course_key, 2,4) as Year, substr(course_key, 1,1) as Semester From WEB_COURSE_TEACHER where COURSE_TEACHER_ID='" + course_teacherId + "' ";
        return obj_db.get_viewData("" + sql, "WEB_COURSE_TEACHER");
    }

    public DataTable get_all_offeredCourses(string year, string sem,string DEPTCODE)
    {
        string sql = " SELECT DISTINCT o.COURSEKEY, c.* FROM OFFEREDCOURSE o, CHANGEDCOURSENAME c , VW_CourseCode WHERE o.COURSECODE=c.COURSECODE  and VW_CourseCode.CourseKey=o.CourseKey";
        sql += " AND o.COURSEKEY like'" + sem + year + "%' and VW_CourseCode.Up_DeptCode ='" + DEPTCODE + "'  order by c.COURSECODE asc ";
        return obj_db.get_viewData(sql, "CourseList");
    }

   
    public DataTable get_all_offeredCourses(string year, string sem)
    {
        string sql = " SELECT DISTINCT o.COURSEKEY, c.* FROM OFFEREDCOURSE o, CHANGEDCOURSENAME c ";
        sql += " WHERE o.COURSECODE=c.COURSECODE AND o.COURSEKEY like'" + sem + year + "%'  order by c.COURSECODE asc ";
        return obj_db.get_viewData(sql, "CourseList");
    }

    public DataTable get_LatestCalenderSearch()
    {
        string sql = " select distinct Semester, Year from WEB_ACADEMIC_CALENDER where Year||Semester =(select max(YEAR||Semester) from WEB_ACADEMIC_CALENDER)";
        return obj_db.get_viewData(sql, "Calender");
    }

    //public DataTable get_CalenderSearch(string year, string sem)
    //{
    //    string sql = " select AC.*, case when AC.FROM_DATE=AC.TO_DATE then ''|| AC.FROM_DATE when AC.FROM_DATE != AC.TO_DATE then ''|| AC.FROM_DATE ";
    //    sql += " end as EventFDate, case  when AC.FROM_DATE=AC.TO_DATE then null when AC.FROM_DATE != AC.TO_DATE then ' to '||AC.TO_DATE ";
    //    sql += " end as EventLDate  from WEB_ACADEMIC_CALENDER AC where AC.YEAR='" + year + "' and AC.SEMESTER ='" + sem + "' order by AC.FROM_DATE asc";
    //    return obj_db.get_viewData(sql, "CalenderList");
    //}

    public DataTable get_CalenderSearch(string year, string sem)
    {
        string sql = "select AC.*,case when AC.FROM_DATE=AC.TO_DATE then ''|| AC.FROM_DATE when AC.FROM_DATE != AC.TO_DATE then ''|| AC.FROM_DATE end as EventFDate,  ";
        sql += "case when AC.FROM_DATE=AC.TO_DATE then null when AC.FROM_DATE != AC.TO_DATE then ' to '||TO_CHAR (AC.TO_DATE, 'dd-Mon-yyyy')  ||','||to_char(AC.TO_DATE,'Day')  ";
        sql += "end as EventLDate, case when AC.FROM_DATE=AC.TO_DATE then ''||  TO_CHAR (AC.FROM_DATE, 'dd-Mon-yyyy') ||','||to_char(AC.FROM_DATE, 'Day')  ";
        sql += " when AC.FROM_DATE != AC.TO_DATE then ''||  TO_CHAR (AC.FROM_DATE, 'dd-Mon-yyyy')  ||','||to_char(AC.FROM_DATE, 'Day') end as EventFDate1  ";
        sql += " from WEB_ACADEMIC_CALENDER AC where AC.YEAR='" + year + "' and AC.SEMESTER ='" + sem + "' order by AC.FROM_DATE asc ";


        return obj_db.get_viewData(sql, "CalenderList");
    }

    public DataTable get_CalenderHolidaySearch(string year, string sem)
    {
        string sql = "  select AC.*, case  when AC.FROM_DATE=AC.TO_DATE then ''||  TO_CHAR (AC.FROM_DATE, 'dd-Mon-yyyy')||','||to_char(AC.FROM_DATE, 'Day')   ";
        sql += " when AC.FROM_DATE != AC.TO_DATE then ''|| TO_CHAR (AC.FROM_DATE, 'dd-Mon-yyyy')||','||to_char(AC.FROM_DATE, 'Day')  ";
        sql += " end as EventFDate, case  when AC.FROM_DATE=AC.TO_DATE then null when AC.FROM_DATE != AC.TO_DATE then ' to '||TO_CHAR (AC.TO_DATE, 'dd-Mon-yyyy')||','||to_char(AC.TO_DATE, 'Day')   ";
        sql += " end as EventLDate  from WEB_ACADEMIC_HOLIDAYS  AC where AC.YEAR='" + year + "' and AC.SEMESTER ='" + sem + "' order by AC.FROM_DATE asc ";

        return obj_db.get_viewData(sql, "CalenderHolidayList");
    }
    //public DataTable get_CalenderHolidaySearch(string year, string sem)
    //{
    //    string sql = " select AC.*, case when AC.FROM_DATE=AC.TO_DATE then ''|| AC.FROM_DATE when AC.FROM_DATE != AC.TO_DATE then ''|| AC.FROM_DATE ";
    //    sql += " end as EventFDate, case  when AC.FROM_DATE=AC.TO_DATE then null when AC.FROM_DATE != AC.TO_DATE then ' to '||AC.TO_DATE ";
    //    sql += " end as EventLDate  from WEB_ACADEMIC_HOLIDAYS  AC where AC.YEAR='" + year + "' and AC.SEMESTER ='" + sem + "' order by AC.FROM_DATE asc";
    //    return obj_db.get_viewData(sql, "CalenderHolidayList");
    //}

   
    public DataTable get_all_offeredCourses(string courseKey)
    {
        string sql = " SELECT * FROM OFFEREDCOURSE, CHANGEDCOURSENAME,COURSEINDEPARTMENT ";
        sql += " WHERE COURSEKEY LIKE'" + courseKey + "%' AND (CHANGEDCOURSENAME.COURSECODE=OFFEREDCOURSE.COURSECODE AND OFFEREDCOURSE.COURSECODE=COURSEINDEPARTMENT.COURSECODE) ";
        sql += " ORDER BY DEPCODE ASC, OFFEREDCOURSE.COURSECODE ASC ";
        return obj_db.get_viewData(sql, "CourseList");
    }

    public string get_latest_creditHours_ofA_course(string courseCode)
    {
        string credits = "";
        DataSet ds = new DataSet();
        string sql = " SELECT * from CHANGEDCREDIT where COURSECODE='" + courseCode + "' order by CHANGEYEAR desc, CHANGESEMESTER desc ";
        ds.Merge(obj_db.get_viewData(sql, "CHANGEDCREDIT"));
        if (ds.Tables["CHANGEDCREDIT"].Rows.Count > 0)
        {
            credits = ds.Tables["CHANGEDCREDIT"].Rows[0]["CREDITHRS"].ToString();
        }

        return credits;
    }



    public DataTable get_prerequisite(string coureCode, string depCode)
    {
        string sql = " SELECT  * FROM COURSEPREREQUISITE where COURSECODE='" + coureCode + "' and DEPCODE='" + depCode + "'  ";

        return obj_db.get_viewData(sql, "prerequisiteList");
    }

    public DataTable get_max_YearSem()
    {
        string sql = "select *  from WEB_PRE_OFFERING_DATE   where year||semester = (select max(year||semester) from WEB_PRE_OFFERING_DATE) ";
        return obj_db.get_viewData(sql, "WEB_PRE_OFFERING_DATE");
    }

    public DataTable get_pre_offerigDate(string year, string sem)
    {
        string sql = " SELECT *from WEB_PRE_OFFERING_DATE WHERE SEMESTER='" + sem + "'AND YEAR='" + year + "' ";
        return obj_db.get_viewData(sql, "WEB_PRE_OFFERING_DATE");
    }

    public DataTable get_pre_offeringDate(string sem, string year)
    {
        string sql = " Select * from WEB_PRE_OFFERING_DATE where SEMESTER='" + sem + "'and YEAR='" + year + "' ";
        return obj_db.get_viewData(sql, "WEB_PRE_OFFERING_DATE");
    }


    public DataTable get_pre_offeringDateNew(string sem, string year, string PROGRAM_ID)
    {
        string sql = @"Select WEB_PRE_OFFERING_DATE.*, C_RESULT_PUBLICATION_DATE.SEM_RESULT_PUBLISH PROG_RESULT_PUBLISH_DATE, 
                        C_RESULT_PUBLICATION_DATE.PROGRAM_ID from WEB_PRE_OFFERING_DATE
                        left join C_RESULT_PUBLICATION_DATE on 
                        C_RESULT_PUBLICATION_DATE.YEAR= WEB_PRE_OFFERING_DATE.YEAR 
                        and WEB_PRE_OFFERING_DATE.SEMESTER = C_RESULT_PUBLICATION_DATE.SEMESTER
                        where WEB_PRE_OFFERING_DATE.SEMESTER='" + sem + "' and WEB_PRE_OFFERING_DATE.YEAR='" + year + "' and C_RESULT_PUBLICATION_DATE.PROGRAM_ID ='" + PROGRAM_ID + "' ";


      //  string sql = " Select * from WEB_PRE_OFFERING_DATE where SEMESTER='" + sem + "'and YEAR='" + year + "' ";
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

    
    public DataTable get_allAdvisor_Fulltime(string depId)
    {
        string sql = " Select * from WEB_TEACHER_STAFF where JOB_CATEGORY='Teacher' and STAFF_CTRL=1 and DEPARTMENT='" + depId + "' and JOB_TYPE='Full' and VALUE is not null  order by STAFF_NAME";
        return obj_db.get_viewData(sql, "advisorList");
    }

    public DataTable get_allEmployee_Fulltime(string VALUE)
    {
        string sql = " Select * from WEB_TEACHER_STAFF where  STAFF_CTRL=1 and VALUE='" + VALUE + "'  order by STAFF_NAME";
        return obj_db.get_viewData(sql, "EmployeeList");
    }

    public DataTable get_Employee_Role(string VALUE)
    {
        string sql = "select AD_EMPLOYEE_ROLE.*, ad_role.ROLE_NAME, ad_role.DESCRIPTION from AD_EMPLOYEE_ROLE left join ad_role on ad_role.ID= AD_EMPLOYEE_ROLE.ROLE_ID where EMPLOYEE_ID = '" + VALUE + "'  order by ad_role.ROLE_NAME asc";
        return obj_db.get_viewData(sql, "RoleList");
    }

    public DataTable get_all_teacher()
    {
        string sql = " Select * from WEB_TEACHER_STAFF where JOB_CATEGORY='Teacher'and STAFF_CTRL=1 ";
        return obj_db.get_viewData(sql, "advisorList");
    }


    public DataTable get_all_teacherFaculty(string Faculty_ID)
    {
        string sql = " Select * from WEB_TEACHER_STAFF where JOB_CATEGORY='Teacher'and STAFF_CTRL=1 and DEPARTMENT='" + Faculty_ID + "'order by STAFF_NAME";
        return obj_db.get_viewData(sql, "advisorList");
    }


    public DataTable get_all_roomno()
    {
        //string sql = " Select * from DEPARTMENTINCOLLEGE ";
        string sql = " Select C_ROOM_ID, ROOM_NAME from C_ROOM order by ROOM_NAME asc ";
        return obj_db.get_viewData(sql, "roomlist");
    }


    public DataTable get_all_department()
    {
        string sql = " Select * from COLLEGE order by COLLEGECODE ";
        return obj_db.get_viewData(sql, "deptlist");
    }


    public DataTable get_all_degreeName()
    {
        string sql = " Select * from C_PROGINDEPT order by Name asc ";
        return obj_db.get_viewData(sql, "degreetlist");
    }

    public DataTable get_all_Route()
    {
        string sql = " Select * from C_TRNS_ROUTE order by ROUTE_NAME asc ";
        return obj_db.get_viewData(sql, "ROUTELIST");
    }
    public DataTable get_Route_Point(string RouteID)
    {
        string sql = " select  POINT_ID, POINT_NAME from C_TRNS_ROUTE_POINT where ROUTE_ID = '" + RouteID + "' order by POINT_NAME asc; ";
        return obj_db.get_viewData(sql, "PointList");
    }


    public DataTable match_C_Routine(string year, string semester, string day, string time, string room)
    {
        string sql = " Select * from C_ROUTINE  where year ='" + year + "' and day ='" + day + "' and semester ='" + semester + "' and time ='" + time + "' and room_ID ='" + room + "' and Course_teacher_ID is null";
        return obj_db.get_viewData(sql, "room");
    }

    public DataTable match_C_Routine1(string year, string semester, string day, string time, string room)
    {
        string sql = " Select * from C_ROUTINE  where year ='" + year + "' and day ='" + day + "' and semester ='" + semester + "' and time ='" + time + "' and room_ID ='" + room + "' and Course_teacher_ID is null";
        return obj_db.get_viewData(sql, "room1");
    }

    public DataTable match_RoutineList(string year, string semester, string day1, string time1, string room1)
    {
        string sql = @" select * from C_ROUTINE_VIEW where year ='" + year + "' and day ='" + day1 + "' and semester ='" + semester + "' and time ='" + time1 + "' and room_ID ='" + room1 + "' and Course_teacher_ID IS NOT NULL ";

        return obj_db.get_viewData(sql, "RtnList1");
    }

    public DataTable match_RoutineList(string C_ROUTINE_ID)
    {
        string sql = @" select * from C_ROUTINE_VIEW where C_ROUTINE_ID ='" + C_ROUTINE_ID + "'  and Course_teacher_ID IS NOT NULL ";

        return obj_db.get_viewData(sql, "RtnList1");
    }

    public DataTable match_RoutineListSave(string Class1, string Class2, string Class3)
    {
        string sql = @" select * from C_ROUTINE_VIEW where (C_ROUTINE_ID ='" + Class1 + "' or C_ROUTINE_ID ='" + Class2 + "' or C_ROUTINE_ID ='" + Class3 + "')  and Course_teacher_ID IS NOT NULL ";

        return obj_db.get_viewData(sql, "RoutineListSave");
    }

    public DataTable match_RoutineList(string C_ROUTINE_ID, string ID)
    {
        string sql = @" select * from C_ROUTINE_VIEW where C_ROUTINE_ID ='" + C_ROUTINE_ID + "'  and COURSE_TEACHER_ID != '" + ID + "' ";

        return obj_db.get_viewData(sql, "RtnList1");
    }

    public DataTable match_RoutineListSave(string C_ROUTINE_ID, string ID)
    {
        string sql = @" select * from C_ROUTINE_VIEW where C_ROUTINE_ID ='" + C_ROUTINE_ID + "'  and COURSE_TEACHER_ID != '" + ID + "' ";

        return obj_db.get_viewData(sql, "RtnList1");
    }
    public DataTable match_TeacherSlot(string time1, string day1, string time2, string day2, string time3, string day3, string Tearcher_ID, string year, string semester)
    {
        string sql = @" select * from C_ROUTINE_VIEW where TEACHER_ID ='" + Tearcher_ID + "'  and YEAR = '" + year + "' and SEMESTER ='" + semester + "'  and ((day = '" + day1 + "' and time = '" + time1 + "') or (day = '" + day2 + "' and time = '" + time2 + "') or (day = '" + day3 + "' and time = '" + time3 + "'))   ";

        return obj_db.get_viewData(sql, "RtnList1");
    }


    public DataTable match_RoutineList(string year, string semester, string day1, string time1, string room1, string ID)
    {
        string sql = @" select * from C_ROUTINE_VIEW where year ='" + year + "' and day ='" + day1 + "' and semester ='" + semester + "' and time ='" + time1 + "' and room_ID ='" + room1 + "' and COURSE_TEACHER_ID != '" + ID + "' ";

        return obj_db.get_viewData(sql, "RtnList1");
    }


    public DataTable match_C_Routine(string year, string semester, string day, string time, string room, string ID)
    {
        string sql = " Select * from C_ROUTINE_VIEW  where year ='" + year + "' and day ='" + day + "' and semester ='" + semester + "' and time ='" + time + "' and room_ID ='" + room + "'and COURSE_TEACHER_ID ='" + ID + "' ";
        return obj_db.get_viewData(sql, "room");
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
        int count = 0;
        string sql = "";
        foreach (DataRow dr in ds.Tables["STUDENT"].Rows)
        {
            sql = @" update STUDENT set SPASSWORD='" + dr["SPASSWORD"].ToString() + "' where SID='" + dr["sid"].ToString() + "' ";
            if (obj_db.execute_query(sql) == "1")
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
        string sem = "";
        if (DateTime.Today.Month >= 1 && DateTime.Today.Month <= 4)
            sem = "1";
        else if (DateTime.Today.Month >= 5 && DateTime.Today.Month <= 8)
            sem = "2";
        else if (DateTime.Today.Month >= 9 && DateTime.Today.Month <= 12)
            sem = "3";

        return sem;

    }


    public DataTable get_course_teacher_list(string collegeCode, string sem, string year)
    {
        string sql = @" SELECT distinct vct.*, wt.DEPARTMENT,(SELECT COUNT(*) FROM WEB_TEACHER_EVAL_ARGUMENT)AS arg_qty ";
        sql += "  FROM WEB_VIEW_COURSE_TEACHER vct, WEB_TEACHER_STAFF wt ";
        sql += "  WHERE (vct.TEACHER_ID= wt.STAFF_ID AND wt.DEPARTMENT='" + collegeCode + "') AND vct.COURSE_KEY LIKE'" + sem + year + "%' ";
        sql += "  ORDER BY teacher_id,coursecode ASC ";

        return obj_db.get_viewData(sql, "course_teacher");
    }

    public DataTable get_course_teacher_list(string collegeCode, string staffId, string sem, string year)
    {
        string sql = @" SELECT distinct vct.*, wt.DEPARTMENT,(SELECT COUNT(*) FROM WEB_TEACHER_EVAL_ARGUMENT)AS arg_qty ";
        sql += "  FROM WEB_VIEW_COURSE_TEACHER vct, WEB_TEACHER_STAFF wt ";
        sql += "  WHERE (vct.TEACHER_ID= wt.STAFF_ID and wt.STAFF_ID='" + staffId + "' AND wt.DEPARTMENT='" + collegeCode + "') AND vct.COURSE_KEY LIKE'" + sem + year + "%' ";
        sql += "  ORDER BY teacher_id,coursecode ASC ";

        return obj_db.get_viewData(sql, "course_teacher");
    }

    public DataTable get_course_teacher_Eval_list(string sem, string year)
    {
        string sql = @"  SELECT distinct evs.* ";
        sql += " FROM WEB_VIEW_EVALUATION_SUMMERY evs, WEB_VIEW_COURSE_TEACHER ct ";
        sql += " WHERE evs.COURSE_TEACHER=ct.COURSE_TEACHER_ID AND COURSE_KEY LIKE '" + sem + year + "%'";

        return obj_db.get_viewData(sql, "course_teacher_eval");
    }

    public DataTable get_course_teacher_Eval_Details(string course_teacherID)
    {
        string sql = @" SELECT distinct * FROM WEB_TEACHER_EVAL_VALUE,WEB_VIEW_COURSE_TEACHER ";
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
        string sql = " Select * from WEB_NOTICE_BOARD where PUBLISH_DATE >= TO_DATE('" + new cls_tools().get_database_formateDate(fromDate) + "', 'dd/mm/yyyy hh24:mi:ss') and ";
        sql += " PUBLISH_DATE <= TO_DATE('" + new cls_tools().get_database_formateDate(toDate) + "', 'dd/mm/yyyy hh24:mi:ss') and CTRL='" + type + "' order by NOTICE_ID desc ";
        return obj_db.get_viewData(sql, "WEB_NOTICE_BOARD");
    }


    public DataTable get_filtered_news_event(DateTime fromDate, DateTime toDate)
    {
        toDate = toDate.AddHours(23);
        string sql = " Select * from WEB_NEWS_EVENTS where FROM_DATE >=TO_DATE('" + new cls_tools().get_database_formateDate(fromDate) + "', 'dd/mm/yyyy hh24:mi:ss') and ";
        sql += " FROM_DATE <= TO_DATE('" + new cls_tools().get_database_formateDate(toDate) + "', 'dd/mm/yyyy hh24:mi:ss') order by NEWS_EVENT_ID desc ";
        return obj_db.get_viewData(sql, "WEB_NEWS_EVENTS");
    }


    public DataTable get_allocated_room()
    {
        string sql = " Select distinct * from WEB_VIEW_ROOM order by DEPT_ID asc ";
        return obj_db.get_viewData(sql, "WEB_VIEW_ROOM");
    }

    public DataTable get_allocated_slotdtl()
    {
        string sql = " Select distinct * from WEB_VIEW_SLOT order by CATEGORY_ID asc ";
        return obj_db.get_viewData(sql, "WEB_VIEW_SLOT");
    }


    public DataTable get_allocated_slot()
    {
        string sql = " Select distinct * from SLOT_CAT order by SLOTCAT_ID asc ";
        return obj_db.get_viewData(sql, "SLOTCAT_VIEW");
    }


    public DataTable get_slot_Time(string ID)
    {
        string sql = @" SELECT * FROM SLOT inner join  SLOT_CAT on SLOT_CAT.SLOTCAT_ID = SLOT.SLOTCAT_ID where SLOT_CAT.VALUE=1 and SLOT_SL = '" + ID + "' ";

        return obj_db.get_viewData(sql, "slotdtl_list");
    }



    public DataTable get_allocated_slots()
    {
        string sql = "SELECT SLOT.SLOT_SL ,SLOT.SLOT_ID , SLOT.SLOT_NAME ,SLOT.SLOTCAT_ID ,SLOT.TIME_FROM, SLOT.TIME_TO , SLOT.SLOT_NAME || ' ('||  SLOT.TIME_FROM || ' to ' || SLOT.TIME_TO || ')' as TIME_DURATION FROM SLOT inner join  SLOT_CAT on SLOT_CAT.SLOTCAT_ID = SLOT.SLOTCAT_ID where SLOT_CAT.VALUE=1 order by SLOT_ID asc";
        return obj_db.get_viewData(sql, "SLOT_DTL_VIEW");
    }

    public DataTable get_slots(string yearID, string semesterID)
    {
        // string sql = "Select C_ROUTINE.YEAR ||'-'|| SEMESTER.SEMESTER ||'-'||  C_ROUTINE.DAY ||'-'||C_ROUTINE.TIME ||'-'|| C_room.ROOM_NAME as dept_room_name, COURSE_TEACHER_ID from C_ROUTINE  inner join C_room on C_room.C_ROOM_ID = C_ROUTINE.ROOM_ID inner join SEMESTER on SEMESTER.SEMCODE = C_ROUTINE.SEMESTER where C_room.FACULTY_ID = '" + Faculty_ID + "' ";
        string sql = @"select distinct time from C_Routine where YEAR='" + yearID + "' AND SEMESTER='" + semesterID + "'   ";


        return obj_db.get_viewData(sql, "SLOT_VIEW");
    }


    public DataTable get_slots(string yearID, string semesterID, string dateID, string roomID)
    {
        string sql = "SELECT C.C_ROUTINE_ID,C.TIME TIME FROM ";
        sql += "C_ROUTINE C JOIN SEMESTER S ON (C.SEMESTER=S.SEMCODE) JOIN C_ROOM CR ON (C.ROOM_ID=CR.C_ROOM_ID) ";
        sql += "JOIN COLLEGE FC ON (CR.FACULTY_ID=FC.COLLEGECODE) WHERE C.YEAR='" + yearID + "' AND C.SEMESTER='" + semesterID + "' ";
        sql += "AND C. DAY='" + dateID + "' AND C.ROOM_ID = '" + roomID + "' ORDER BY CR.C_ROOM_ID,C.C_ROUTINE_ID ";

        return obj_db.get_viewData(sql, "SLOT_VIEW");
    }


    public DataTable get_Dept_slots(string yearID, string semesterID)
    {
        // string sql = "Select C_ROUTINE.YEAR ||'-'|| SEMESTER.SEMESTER ||'-'||  C_ROUTINE.DAY ||'-'||C_ROUTINE.TIME ||'-'|| C_room.ROOM_NAME as dept_room_name, COURSE_TEACHER_ID from C_ROUTINE  inner join C_room on C_room.C_ROOM_ID = C_ROUTINE.ROOM_ID inner join SEMESTER on SEMESTER.SEMCODE = C_ROUTINE.SEMESTER where C_room.FACULTY_ID = '" + Faculty_ID + "' ";
        string sql = @"SELECT C.C_ROUTINE_ID, C.YEAR||'-'||S.SEMESTER||'-'||C.DAY||'-'||C.TIME||'-'||CR.ROOM_NAME TIME ";
        sql += " FROM C_ROUTINE C JOIN SEMESTER S ON (C.SEMESTER=S.SEMCODE) JOIN C_ROOM CR ON (C.ROOM_ID=CR.C_ROOM_ID)";
        sql += "JOIN COLLEGE FC ON (CR.FACULTY_ID=FC.COLLEGECODE) WHERE C.YEAR='" + yearID + "' AND C.SEMESTER='" + semesterID + "' and C.course_teacher_id is null  ";
        sql += " ORDER BY CR.C_ROOM_ID,C.C_ROUTINE_ID ";


        return obj_db.get_viewData(sql, "SLOT_VIEW");
    }


    public DataTable get_Dept_slots(string Faculty_ID, string yearID, string semesterID)
    {
        // string sql = "Select C_ROUTINE.YEAR ||'-'|| SEMESTER.SEMESTER ||'-'||  C_ROUTINE.DAY ||'-'||C_ROUTINE.TIME ||'-'|| C_room.ROOM_NAME as dept_room_name, COURSE_TEACHER_ID from C_ROUTINE  inner join C_room on C_room.C_ROOM_ID = C_ROUTINE.ROOM_ID inner join SEMESTER on SEMESTER.SEMCODE = C_ROUTINE.SEMESTER where C_room.FACULTY_ID = '" + Faculty_ID + "' ";
        string sql = @"SELECT C.C_ROUTINE_ID, C.YEAR||'-'||S.SEMESTER||'-'||C.DAY||'-'||C.TIME||'-'||CR.ROOM_NAME TIME ";
        sql += " FROM C_ROUTINE C JOIN SEMESTER S ON (C.SEMESTER=S.SEMCODE) JOIN C_ROOM CR ON (C.ROOM_ID=CR.C_ROOM_ID)";
        sql += "JOIN COLLEGE FC ON (CR.FACULTY_ID=FC.COLLEGECODE) WHERE C.YEAR='" + yearID + "'  AND C.SEMESTER='" + semesterID + "'  and C.course_teacher_id is null  ";
        sql += "AND FC.COLLEGECODE='" + Faculty_ID + "' ORDER BY CR.C_ROOM_ID,C.C_ROUTINE_ID ";


        return obj_db.get_viewData(sql, "SLOT_VIEW");
    }

    public DataTable get_fixedSemister_Roomdtl(string COURSE_SEM_YEAR)
    {
        string sql = " Select distinct * from WEB_VIEW_ROOM_DISTRIBUTION where COURSE_KEY like '" + COURSE_SEM_YEAR + "%' order by ROOM_ID asc ";
        return obj_db.get_viewData(sql, "WEB_VIEW_ROOM_DISTRIBUTION");
    }

    public DataTable get_allcated_teacher_course(string courseKey)
    {

        string sql = " Select distinct * from  WEB_VIEW_COURSE_TEACHER_PREV where COURSE_KEY='" + courseKey + "' order by STAFF_NAME asc ";
        return obj_db.get_viewData(sql, "WEB_VIEW_COURSE_TEACHER");
    }

    public DataTable get_allcated_teacher_ofa_course(string courseKey)
    {

        string sql = " Select distinct * from WEB_VIEW_COURSE_TEACHER where COURSE_KEY='" + courseKey + "' order by SECTION asc, STAFF_NAME asc ";
        return obj_db.get_viewData(sql, "WEB_VIEW_COURSE_TEACHER");
    }

    /*public string save_pre_course_offeringDate(DataSet ds, string year, string sem)
    {
        obj_db.execute_query(" Delete from WEB_PRE_OFFERING_DATE where YEAR='" + year + "' and SEMESTER='" + sem + "'  ");
        return obj_db.insert_general(ds, "WEB_PRE_OFFERING_DATE");
    }*/

    public string save_pre_course_offeringDate(DataSet ds, string year, string sem)
    {
        String sql = "", sql1 = "";
        if (obj_db.execute_query(" Delete from C_RESULT_PUBLICATION_DATE where YEAR='" + year + "' and SEMESTER='" + sem + "' ") == "1")
            obj_db.execute_query(" Delete from WEB_PRE_OFFERING_DATE where YEAR='" + year + "' and SEMESTER='" + sem + "'  ");

        //   obj_db.execute_query(" Delete from WEB_PRE_OFFERING_DATE where YEAR='" + year + "' and SEMESTER='" + sem + "'  ");

        foreach (DataRow dr in ds.Tables["WEB_PRE_OFFERING_DATE"].Rows)
        {

            sql = " Insert into WEB_PRE_OFFERING_DATE ( SEMESTER, YEAR, OPENING_DATE, CLOSING_DATE, CTRL, TEACHER_OPENINGDATE, TEACHER_CLOSINGDATE, TEACHER_RE_OPENINGDATE, TEACHER_RE_CLOSINGDATE, EVAL_OPENING, EVAL_CLOSING, SEM_RESULT_PUBLISH, ADMIT_OPENING, ADMIT_CLOSING, MIDAMIT_OPENING, MIDAMIT_CLOSING, READD_OPENING_DATE,READD_CLOSING_DATE, LATEADD_OPENING_DATE,LATEADD_CLOSING_DATE)";
            sql += " values ('" + dr["SEMESTER"] + "', '" + dr["YEAR"] + "', TO_DATE('" + dr["OPENING_DATE"] + "','dd/mm/yyyy hh24:mi:ss'), ";
            sql += " TO_DATE('" + dr["CLOSING_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), '" + dr["CTRL"] + "', TO_DATE('" + dr["TEACHER_OPENINGDATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["TEACHER_CLOSINGDATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["TEACHER_RE_OPENINGDATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), ";
            sql += " TO_DATE('" + dr["TEACHER_RE_CLOSINGDATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["EVAL_OPENING"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["EVAL_CLOSING"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["SEM_RESULT_PUBLISH"] + "', 'dd/mm/yyyy hh24:mi:ss'), ";
            sql += " TO_DATE('" + dr["ADMIT_OPENING"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["ADMIT_CLOSING"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["MIDAMIT_OPENING"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["MIDAMIT_CLOSING"] + "', 'dd/mm/yyyy hh24:mi:ss'),  ";
            sql += " TO_DATE('" + dr["READD_OPENING_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["READD_CLOSING_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss') , TO_DATE('" + dr["LATEADD_OPENING_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["LATEADD_CLOSING_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss')  )";


            sql1 = @"INSERT into C_RESULT_PUBLICATION_DATE select  '" + dr["SEMESTER"] + "','" + dr["YEAR"] + "',  C_PROGINDEPT.C_PROGINDEPT_ID, TO_DATE('" + dr["SEM_RESULT_PUBLISH"] + "', 'dd/mm/yyyy hh24:mi:ss') from C_PROGINDEPT where is_active = 1";

            //  return obj_db.execute_query(sql);
        }

        string i = obj_db.execute_query(sql);
        string j = obj_db.execute_query(sql1);

        if (i == "1" && j == "1")
            return "1";
        else
            return "";
    }

    public string Update_pre_course_offeringDate(DataSet ds, string year, string sem)
    {
        String sql = "", sql1="";
        if (obj_db.execute_query(" Delete from C_RESULT_PUBLICATION_DATE where YEAR='" + year + "' and SEMESTER='" + sem + "' ") == "1")
        {
            foreach (DataRow dr in ds.Tables["WEB_PRE_OFFERING_DATE"].Rows)
            {
                sql1 = @"INSERT into C_RESULT_PUBLICATION_DATE select  '" + dr["SEMESTER"] + "','" + dr["YEAR"] + "',  C_PROGINDEPT.C_PROGINDEPT_ID, TO_DATE('" + dr["SEM_RESULT_PUBLISH"] + "', 'dd/mm/yyyy hh24:mi:ss') from C_PROGINDEPT where is_active = 1";
            }
        }


        foreach (DataRow dr in ds.Tables["WEB_PRE_OFFERING_DATE"].Rows)
        {
            sql = " update WEB_PRE_OFFERING_DATE set OPENING_DATE= TO_DATE('" + dr["OPENING_DATE"] + "','dd/mm/yyyy hh24:mi:ss'), CLOSING_DATE= TO_DATE('" + dr["CLOSING_DATE"] + "','dd/mm/yyyy hh24:mi:ss'),  LATEADD_OPENING_DATE= TO_DATE('" + dr["LATEADD_OPENING_DATE"] + "','dd/mm/yyyy hh24:mi:ss') ,  LATEADD_CLOSING_DATE= TO_DATE('" + dr["LATEADD_CLOSING_DATE"] + "','dd/mm/yyyy hh24:mi:ss') ,  READD_OPENING_DATE= TO_DATE('" + dr["READD_OPENING_DATE"] + "','dd/mm/yyyy hh24:mi:ss') , ";
            sql += " READD_CLOSING_DATE= TO_DATE('" + dr["READD_CLOSING_DATE"] + "','dd/mm/yyyy hh24:mi:ss') , TEACHER_OPENINGDATE= TO_DATE('" + dr["TEACHER_OPENINGDATE"] + "','dd/mm/yyyy hh24:mi:ss') , ";
            sql += " TEACHER_CLOSINGDATE= TO_DATE('" + dr["TEACHER_CLOSINGDATE"] + "','dd/mm/yyyy hh24:mi:ss') , TEACHER_RE_OPENINGDATE= TO_DATE('" + dr["TEACHER_RE_OPENINGDATE"] + "','dd/mm/yyyy hh24:mi:ss') , TEACHER_RE_CLOSINGDATE= TO_DATE('" + dr["TEACHER_RE_CLOSINGDATE"] + "','dd/mm/yyyy hh24:mi:ss') , SEM_RESULT_PUBLISH=TO_DATE('" + dr["SEM_RESULT_PUBLISH"] + "','dd/mm/yyyy hh24:mi:ss') ,";
            sql += " EVAL_OPENING= TO_DATE('" + dr["EVAL_OPENING"] + "','dd/mm/yyyy hh24:mi:ss') ,EVAL_CLOSING= TO_DATE('" + dr["EVAL_CLOSING"] + "','dd/mm/yyyy hh24:mi:ss') ,  ADMIT_OPENING=  TO_DATE('" + dr["ADMIT_OPENING"] + "','dd/mm/yyyy hh24:mi:ss') , ADMIT_CLOSING=  TO_DATE('" + dr["ADMIT_CLOSING"] + "','dd/mm/yyyy hh24:mi:ss') , MIDAMIT_OPENING=  TO_DATE('" + dr["MIDAMIT_OPENING"] + "','dd/mm/yyyy hh24:mi:ss')  , MIDAMIT_CLOSING = TO_DATE('" + dr["MIDAMIT_CLOSING"] + "','dd/mm/yyyy hh24:mi:ss')  ";
            sql += " where YEAR='" + dr["YEAR"] + "' and SEMESTER='" + dr["SEMESTER"] + "'";

         //   return obj_db.execute_query(sql);
        }

        string i = obj_db.execute_query(sql);
        string j = obj_db.execute_query(sql1);

        if (i == "1" && j == "1")
            return "1";
        else
            return "";
       
    }
    public string save_Employee_Role(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["AD_EMPLOYEE_ROLE"].Rows)
        {
            sql = " Insert into AD_EMPLOYEE_ROLE (EMPLOYEE_ID, ROLE_ID)";
            sql += " values ('" + dr["EMPLOYEE_ID"] + "', '" + dr["ROLE_ID"] + "') ";
            return obj_db.execute_query(sql); 
        }
        return "";
    }
    public string save_newTeacher_infoPart(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
        {
            string code = obj_db.get_pk_no("staff");
            sql = " Insert into WEB_TEACHER_STAFF (STAFF_ID, STAFF_NAME, P_ADDRESS, PER_ADDRESS, PHONE_NUMBER, MOBILE, E_MAIL, DEPARTMENT, JOB_TYPE, JOB_CATEGORY, JOB_DESIGNATION, JOIN_DATE, LOGIN_NAME, PASSWORD, STAFF_CTRL, VALUE)";
            sql += " values ('" + "HRT" + code + "', '" + dr["STAFF_NAME"] + "', '" + dr["P_ADDRESS"] + "', '" + dr["PER_ADDRESS"] + "', '" + dr["PHONE_NUMBER"] + "', '" + dr["MOBILE"] + "', '" + dr["E_MAIL"] + "', '" + dr["DEPARTMENT"] + "', '" + dr["JOB_TYPE"] + "', '" + dr["JOB_CATEGORY"] + "', '" + dr["JOB_DESIGNATION"] + "', TO_DATE('" + dr["JOIN_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), '" + dr["LOGIN_NAME"] + "', '" + dr["PASSWORD"] + "', '" + dr["STAFF_CTRL"] + "', '" + "HRT" + code + "') ";
            update_code("staff", code);
            return obj_db.execute_query(sql);

            /*if (obj_db.execute_query(sql) == "1")
            {
                save_teacher_Role(ds);
                return "1";
            }*/
        }
        return obj_db.execute_query(sql); // return "";
    }
    public string save_newTeacher_info(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
        {
            string code = obj_db.get_pk_no("staff");
            sql = " Insert into WEB_TEACHER_STAFF (STAFF_ID, STAFF_NAME, P_ADDRESS, PER_ADDRESS, PHONE_NUMBER, MOBILE, E_MAIL, DEPARTMENT, JOB_TYPE, JOB_CATEGORY, JOB_DESIGNATION, JOIN_DATE, LOGIN_NAME, PASSWORD, STAFF_CTRL, VALUE)";
            sql += " values ('" + "HRT" + code + "', '" + dr["STAFF_NAME"] + "', '" + dr["P_ADDRESS"] + "', '" + dr["PER_ADDRESS"] + "', '" + dr["PHONE_NUMBER"] + "', '" + dr["MOBILE"] + "', '" + dr["E_MAIL"] + "', '" + dr["DEPARTMENT"] + "', '" + dr["JOB_TYPE"] + "', '" + dr["JOB_CATEGORY"] + "', '" + dr["JOB_DESIGNATION"] + "', TO_DATE('" + dr["JOIN_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss') , '" + dr["LOGIN_NAME"] + "', '" + dr["PASSWORD"] + "', '" + dr["STAFF_CTRL"] + "', '" + dr["VALUE"] + "') ";
            update_code("staff", code);
           // return obj_db.execute_query(sql);

            if (obj_db.execute_query(sql) == "1")
            {
                save_teacher_Role(ds);
                return "1";
            }
        }
        return obj_db.execute_query(sql); // return "";
    }


    public string save_newTeacher_infoConfirmation(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
        {
            string code = obj_db.get_pk_no("staff");
            sql = " Insert into WEB_TEACHER_STAFF (STAFF_ID, STAFF_NAME, P_ADDRESS, PER_ADDRESS, PHONE_NUMBER, MOBILE, E_MAIL, DEPARTMENT, JOB_TYPE, JOB_CATEGORY, JOB_DESIGNATION, JOIN_DATE,CONFIRMATION_DATE, LOGIN_NAME, PASSWORD, STAFF_CTRL, VALUE)";
            sql += " values ('" + "HRT" + code + "', '" + dr["STAFF_NAME"] + "', '" + dr["P_ADDRESS"] + "', '" + dr["PER_ADDRESS"] + "', '" + dr["PHONE_NUMBER"] + "', '" + dr["MOBILE"] + "', '" + dr["E_MAIL"] + "', '" + dr["DEPARTMENT"] + "', '" + dr["JOB_TYPE"] + "', '" + dr["JOB_CATEGORY"] + "', '" + dr["JOB_DESIGNATION"] + "', TO_DATE('" + dr["JOIN_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss') , TO_DATE('" + dr["CONFIRMATION_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss') , '" + dr["LOGIN_NAME"] + "', '" + dr["PASSWORD"] + "', '" + dr["STAFF_CTRL"] + "', '" + dr["VALUE"] + "') ";
            update_code("staff", code);
            // return obj_db.execute_query(sql);

            if (obj_db.execute_query(sql) == "1")
            {
                save_teacher_Role(ds);
                return "1";
            }
        }
        return obj_db.execute_query(sql); // return "";
    }

    public DataTable get_allInstitutions()
    {
        string sql = @" select  DISTRICT || ', '|| NAME INSTITUTE_NAME, C_INSTITUTE_ID  from C_institute where LEVELOF ! = 'S.S.C(VOCATIONAL INDEPENDENT)' and LEVELOF != 'SECONDARY' order by DISTRICT asc, NAME asc ";
        return obj_db.get_viewData(sql, "Institutions_list");
    }
    public DataTable get_allDistrict()
    {
        string sql = @" Select * from C_DISTRICT order by DISTRICT asc ";
        return obj_db.get_viewData(sql, "District_list");
    }
    public DataTable get_allRef()
    {
        string sql = @" Select * from C_INQUIRY_REFFERENCE order by C_REFFERENCE_ID asc ";
        return obj_db.get_viewData(sql, "REFFERENCE_list");
    }

    public string update_InquiryStudent_info(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["C_INQUIRY_ADMISSION"].Rows)
        {
            sql = " update C_INQUIRY_ADMISSION set OTHER_INSTITUTION= '" + dr["OTHER_INSTITUTION"] + "', STUDENT_NAME= '" + dr["STUDENT_NAME"] + "', YEAR= '" + dr["YEAR"] + "' ,  SEMESTER= '" + dr["SEMESTER"] + "' ,  BIRTH_DATE= '" + dr["BIRTH_DATE"] + "' ,  BIRTH_PLACE='" + dr["BIRTH_PLACE"] + "' , ";
            sql += " INTERESTED_PROGRAM='" + dr["INTERESTED_PROGRAM"] + "', LIVING_AREA='" + dr["LIVING_AREA"] + "', ";
            sql += " DISTRICT_ID='" + dr["DISTRICT_ID"] + "', COLLEGE='" + dr["COLLEGE"] + "', UNIVERSITY='" + dr["UNIVERSITY"] + "', ";
            sql += " REFERRED_BY='" + dr["REFERRED_BY"] + "',REFERRED_NAME='" + dr["REFERRED_NAME"] + "',REFERRED_CONTACT='" + dr["REFERRED_CONTACT"] + "', NOTE= '" + dr["NOTE"] + "', UPDATED_BY= '" + dr["UPDATED_BY"] + "', UPDATED_DATE= '" + dr["UPDATED_DATE"] + "'  ";
            sql += " where CONTACT='" + dr["CONTACT"] + "' ";

            return obj_db.execute_query(sql);
        }
        return "";
    }

    public DataTable get_InquiryStudent_info(string CONTACT)
    {
        string sql = @" Select * from C_INQUIRY_ADMISSION where CONTACT='" + CONTACT + "'";
        return obj_db.get_viewData(sql, "C_INQUIRY_ADMISSION");
    }

    public string save_InquiryStudent_info(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["C_INQUIRY_ADMISSION"].Rows)
        {
            // string code = obj_db.get_pk_no("employee");
            sql = " Insert into C_INQUIRY_ADMISSION (CONTACT,OTHER_INSTITUTION,STUDENT_NAME,YEAR, SEMESTER, BIRTH_DATE, BIRTH_PLACE, COMING_DATE, INTERESTED_PROGRAM, LIVING_AREA, DISTRICT_ID, COLLEGE, UNIVERSITY, REFERRED_BY,REFERRED_NAME,REFERRED_CONTACT, NOTE, INSERTED_BY, INSERTED_DATE)";
            sql += " values ('" + dr["CONTACT"] + "','" + dr["OTHER_INSTITUTION"] + "','" + dr["STUDENT_NAME"] + "', '" + dr["YEAR"] + "', '" + dr["SEMESTER"] + "' ,'" + dr["BIRTH_DATE"] + "' ,'" + dr["BIRTH_PLACE"] + "' , '" + dr["COMING_DATE"] + "', '" + dr["INTERESTED_PROGRAM"] + "', '" + dr["LIVING_AREA"] + "', '" + dr["DISTRICT_ID"] + "', '" + dr["COLLEGE"] + "', '" + dr["UNIVERSITY"] + "', '" + dr["REFERRED_BY"] + "','" + dr["REFERRED_NAME"] + "', '" + dr["REFERRED_CONTACT"] + "', '" + dr["NOTE"] + "', '" + dr["INSERTED_BY"] + "', '" + dr["INSERTED_DATE"] + "'  ) ";
            // update_code("employee", code);

            return obj_db.execute_query(sql);
        }
        return "";
    }


    public string update_teacher_info(DataSet ds)
    {
        String sql = "";

        foreach (DataRow dr in ds.Tables["WEB_TEACHER_STAFF"].Rows)
        {
            sql = " update WEB_TEACHER_STAFF set STAFF_NAME= '" + dr["STAFF_NAME"] + "', VALUE= '" + dr["VALUE"] + "',  P_ADDRESS='" + dr["P_ADDRESS"] + "', PER_ADDRESS='" + dr["PER_ADDRESS"] + "' , ";
            sql += " PHONE_NUMBER='" + dr["PHONE_NUMBER"] + "' , MOBILE='" + dr["MOBILE"] + "', E_MAIL='" + dr["E_MAIL"] + "', ";
            sql += " DEPARTMENT='" + dr["DEPARTMENT"] + "', JOB_TYPE='" + dr["JOB_TYPE"] + "', JOB_CATEGORY='" + dr["JOB_CATEGORY"] + "', ";
            sql += " JOB_DESIGNATION='" + dr["JOB_DESIGNATION"] + "', JOIN_DATE= TO_DATE('" + dr["JOIN_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss')  , ";
            sql += "CONFIRMATION_DATE= TO_DATE('" + dr["CONFIRMATION_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss')  where STAFF_ID='" + dr["STAFF_ID"] + "' ";

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
            sql += " values ('NTC_" + code + "' , '" + dr["TITLE"] + "', '" + dr["DESCRIPTION"] + "', TO_DATE('" + dr["PUBLISH_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["INPUT_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), '" + dr["FOR_TEACHER"] + "', '" + dr["FOR_STUDENT"] + "', '" + dr["FOR_GENERAL"] + "', '" + dr["CTRL"] + "') ";//, '" + dr["INPUT_BY"] + "'

            update_code("notice", code);
            return obj_db.execute_query(sql);
        }
        return "";
    }

    public string update_notice(DataSet ds)
    {

        foreach (DataRow dr in ds.Tables["noticeBoard"].Rows)
        {
            string sql = @" update WEB_NOTICE_BOARD set  TITLE='" + dr["TITLE"] + "', DESCRIPTION='" + dr["DESCRIPTION"] + "',   PUBLISH_DATE=TO_DATE('" + dr["PUBLISH_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), ";
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
            sql = @" update WEB_NEWS_EVENTS set TITLE='" + dr["TITLE"] + "', DESCRIPTION='" + dr["DESCRIPTION"] + "', ";
            sql += "  FROM_DATE='" + dr["FROM_DATE"] + "', TO_DATE='" + dr["TO_DATE"] + "', INPUT_DATE='" + dr["INPUT_DATE"] + "', ";
            sql += "  EVENT_IMAGE='" + dr["EVENT_IMAGE"] + "', TYPE='" + dr["TYPE"] + "', EVENT_CTRL='" + dr["EVENT_CTRL"] + "' ";
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
            if (obj_db.execute_query(sql) != "1")
                status += "1";
        }


        return status;
    }
    public string studentCapacity_update(DataSet ds, string course_teacherId)
    {
        string sql = " ";
        string sql1 = " ";
        string sql2 = " ";
        foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
        {

            sql = @" UPDATE WEB_COURSE_TEACHER set TOTAL_CAPACITY='" + dr["TOTAL_CAPACITY"] + "' where COURSE_TEACHER_ID='" + course_teacherId + "'  ";

            break;


        }

        return obj_db.execute_query(sql);



    }

    public string allocate_teacher_room(DataSet ds)
    {
        string sql = " ";
        string sql1 = " ";
        string sql2 = " ";
        string sql3 = " ";

        foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
        {

            string code = obj_db.get_pk_no("course_teacher");

            sql = @" INSERT into WEB_COURSE_TEACHER (COURSE_TEACHER_ID, COURSE_KEY, TEACHER_ID, SECTION, ACC_CTRL, TUT_CLS_1, TUT_CLS_2,TUT_CLS_3,SCH_CLS_1, SCH_CLS_2,SCH_CLS_3,TOTAL_CAPACITY)";
            sql += " values ('C_T_0" + code + "','" + dr["COURSE_KEY"] + "', '" + dr["TEACHER_ID"] + "', '" + dr["SECTION"] + "', '" + dr["ACC_CTRL"] + "', '" + dr["C_ROUTINE_ID1"] + "', '" + dr["C_ROUTINE_ID2"] + "','" + dr["C_ROUTINE_ID3"] + "', '" + dr["SCH_CLS_1"] + "', '" + dr["SCH_CLS_2"] + "','" + dr["SCH_CLS_3"] + "', '" + dr["TOTAL_CAPACITY"].ToString() + "'  )";


            sql1 = @" UPDATE C_ROUTINE C SET C.COURSE_TEACHER_ID='C_T_0" + code + "'  WHERE   C.C_ROUTINE_ID ='" + dr["C_ROUTINE_ID1"] + "'  ";

            sql2 = @" UPDATE C_ROUTINE C SET C.COURSE_TEACHER_ID='C_T_0" + code + "'  WHERE   C.C_ROUTINE_ID ='" + dr["C_ROUTINE_ID2"] + "'  ";

            sql3 = @" UPDATE C_ROUTINE C SET C.COURSE_TEACHER_ID='C_T_0" + code + "'  WHERE   C.C_ROUTINE_ID ='" + dr["C_ROUTINE_ID3"] + "'  ";


            update_code("course_teacher", code);
            break;
        }

        string i = obj_db.execute_query(sql);
        string j = obj_db.execute_query(sql1);
        string k = obj_db.execute_query(sql2);
        string m = obj_db.execute_query(sql3);

        if (j == "1" && k == "1" && m == "1")
            return m;
        else
            return i;

    }


    public string update_teacher_room(DataSet ds, string course_teacherId, string SL1, string SL2, string SL3)
    {
        string sql = " ";
        string sql1 = " ";
        string sql2 = " ";
        string sql3 = " ";

        Clear_C_ROUTINE(ds, course_teacherId);
        foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
        {


            sql = @" update WEB_COURSE_TEACHER set SECTION='" + dr["SECTION"] + "',  TUT_CLS_1='" + SL1 + "', TUT_CLS_2 ='" + SL2 + "', TUT_CLS_3 ='" + SL3 + "', TOTAL_CAPACITY=  '" + dr["TOTAL_CAPACITY"].ToString() + "', ";
            sql += " TEACHER_ID='" + dr["TEACHER_ID"] + "', SCH_CLS_1 ='" + dr["SCH_CLS_1"] + "', SCH_CLS_2 ='" + dr["SCH_CLS_2"] + "' , SCH_CLS_3 ='" + dr["SCH_CLS_3"] + "' where COURSE_TEACHER_ID='" + course_teacherId + "'  ";

            sql1 = @" UPDATE C_ROUTINE C SET C.COURSE_TEACHER_ID='" + course_teacherId + "'  WHERE   C.C_ROUTINE_ID ='" + SL1 + "'  ";

            sql2 = @" UPDATE C_ROUTINE C SET C.COURSE_TEACHER_ID='" + course_teacherId + "'  WHERE   C.C_ROUTINE_ID ='" + SL2 + "'  ";

            sql2 = @" UPDATE C_ROUTINE C SET C.COURSE_TEACHER_ID='" + course_teacherId + "'  WHERE   C.C_ROUTINE_ID ='" + SL3 + "'  ";


            break;


        }

        string i = obj_db.execute_query(sql);
        string a = obj_db.execute_query(sql1);
        string b = obj_db.execute_query(sql2);
        string c = obj_db.execute_query(sql3);


        if (a == "1" && b == "1" && c == "1")
            return c;
        else
            return i;
    }


    public string Clear_C_ROUTINE(DataSet ds, string course_teacherId)
    {
        string sql = " ";
        foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
        {
            sql = @"UPDATE C_ROUTINE C SET C.COURSE_TEACHER_ID ='' where C.COURSE_TEACHER_ID='" + course_teacherId + "'";
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

    public string allocate_teacher_update(DataSet ds, string course_teacherId, string SL1, string SL2)
    {
        string sql = " ";
        string sql1 = " ";
        string sql2 = " ";
        foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
        {
            sql = @" update WEB_COURSE_TEACHER set SECTION='" + dr["SECTION"] + "',  SCH_CLS_1='" + dr["SCH_CLS_1"] + "', SCH_CLS_2='" + dr["SCH_CLS_2"] + "', TOTAL_CAPACITY=  '" + dr["TOTAL_CAPACITY"].ToString() + "', ";
            sql += " TEACHER_ID='" + dr["TEACHER_ID"] + "', TUT_CLS_1='" + dr["TUT_CLS_1"] + "',TUT_CLS_2='" + dr["TUT_CLS_2"] + "' where COURSE_TEACHER_ID='" + course_teacherId + "'  ";

            sql1 = @" UPDATE C_ROUTINE set  DAY_WEEK='" + dr["CLS_DAY1"] + "', SLOT_SL='" + dr["SLOT_SL1"] + "', ROOM_ID='" + dr["ROOM_ID1"] + "' ,YEAR_NAME='" + dr["YEAR"] + "' , SEMESTER_NO='" + dr["SEMESTER"] + "' where COURSE_TEACHER_ID='" + course_teacherId + "'  and  ROUTINE_SL ='" + SL1 + "' ";


            sql2 = @" UPDATE C_ROUTINE set  DAY_WEEK='" + dr["CLS_DAY2"] + "', SLOT_SL='" + dr["SLOT_SL2"] + "', ROOM_ID='" + dr["ROOM_ID2"] + "' ,YEAR_NAME='" + dr["YEAR"] + "' , SEMESTER_NO='" + dr["SEMESTER"] + "' where COURSE_TEACHER_ID='" + course_teacherId + "'  and  ROUTINE_SL ='" + SL2 + "' ";


            break;


        }

        string i = obj_db.execute_query(sql);
        string a = obj_db.execute_query(sql1);
        string b = obj_db.execute_query(sql2);


        if (a == "1" && b == "1")
            return b;
        else
            return i;


    }

    public string allocate_teacher(DataSet ds)
    {
        string sql = " ";
        string sql1 = " ";
        string sql2 = " ";
        string sql3 = " ";
        string sql4 = " ";

        foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
        {
            string code = obj_db.get_pk_no("course_teacher");

            sql = @" INSERT into WEB_COURSE_TEACHER (COURSE_TEACHER_ID, COURSE_KEY, TEACHER_ID, SECTION, ACC_CTRL, SCH_CLS_1, SCH_CLS_2, TUT_CLS_1, TUT_CLS_2, TOTAL_CAPACITY)";
            sql += " values ('C_T_0" + code + "','" + dr["COURSE_KEY"] + "', '" + dr["TEACHER_ID"] + "', '" + dr["SECTION"] + "', '" + dr["ACC_CTRL"] + "', '" + dr["SCH_CLS_1"] + "', '" + dr["SCH_CLS_2"] + "', '" + dr["TUT_CLS_1"] + "', '" + dr["TUT_CLS_2"] + "', '" + dr["TOTAL_CAPACITY"].ToString() + "'  )";




            sql1 = @" INSERT into C_ROUTINE (COURSE_TEACHER_ID, YEAR_NAME, SEMESTER_NO, DAY_WEEK, SLOT_SL, ROOM_ID) values ('C_T_0" + code + "','" + dr["YEAR"] + "', '" + dr["SEMESTER"] + "', '" + dr["CLS_DAY1"] + "', '" + dr["SLOT_SL1"] + "', '" + dr["ROOM_ID1"] + "')";


            sql2 = @" INSERT into C_ROUTINE (COURSE_TEACHER_ID, YEAR_NAME, SEMESTER_NO, DAY_WEEK, SLOT_SL, ROOM_ID) values ('C_T_0" + code + "','" + dr["YEAR"] + "', '" + dr["SEMESTER"] + "', '" + dr["CLS_DAY2"] + "', '" + dr["SLOT_SL2"] + "', '" + dr["ROOM_ID2"] + "')";


            sql3 = @" INSERT into C_ROUTINE (COURSE_TEACHER_ID, YEAR_NAME, SEMESTER_NO, DAY_WEEK, SLOT_SL, ROOM_ID) values ('C_T_0" + code + "','" + dr["YEAR"] + "', '" + dr["SEMESTER"] + "', '" + dr["CLS_DAY3"] + "', '" + dr["SLOT_SL3"] + "', '" + dr["ROOM_ID3"] + "')";


            sql4 = @" INSERT into C_ROUTINE (COURSE_TEACHER_ID, YEAR_NAME, SEMESTER_NO, DAY_WEEK, SLOT_SL, ROOM_ID) values ('C_T_0" + code + "','" + dr["YEAR"] + "', '" + dr["SEMESTER"] + "', '" + dr["CLS_DAY4"] + "', '" + dr["SLOT_SL4"] + "', '" + dr["ROOM_ID4"] + "')";



            update_code("course_teacher", code);
            break;
        }

        string i = obj_db.execute_query(sql);
        string a = obj_db.execute_query(sql1);
        string b = obj_db.execute_query(sql2);
        string c = obj_db.execute_query(sql3);
        string d = obj_db.execute_query(sql4);

        if (b == "1" && c == "1" && d == "1" && a == "1")
            return d;
        else
            return i;

    }

    public string allocate_teacherPrev(DataSet ds)
    {
        string sql = " ";

        foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
        {
            string code = obj_db.get_pk_no("course_teacher");

            sql = @" INSERT into WEB_COURSE_TEACHER (COURSE_TEACHER_ID, COURSE_KEY, TEACHER_ID, SECTION, ACC_CTRL, SCH_CLS_1, SCH_CLS_2, TUT_CLS_1, TUT_CLS_2, TOTAL_CAPACITY)";
            sql += " values ('C_T_0" + code + "','" + dr["COURSE_KEY"] + "', '" + dr["TEACHER_ID"] + "', '" + dr["SECTION"] + "', '" + dr["ACC_CTRL"] + "', '" + dr["SCH_CLS_1"] + "', '" + dr["SCH_CLS_2"] + "', '" + dr["TUT_CLS_1"] + "', '" + dr["TUT_CLS_2"] + "', '" + dr["TOTAL_CAPACITY"].ToString() + "'  )";


            update_code("course_teacher", code);
            break;
        }

        return obj_db.execute_query(sql);

    }

    //public string allocate_dtl(DataSet ds)
    //{
    //    string sql1 = " ";
    //    string sql2 = " ";
    //    string sql3 = " ";
    //    string sql4 = " ";

    //    foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
    //    {
    //        string code = obj_db.get_pk_no("routine");

    //        sql1 = @" INSERT into WEB_COURSE_TEACHER (COURSE_TEACHER_ID, COURSE_KEY, TEACHER_ID, SECTION, ACC_CTRL, SCH_CLS_1, SCH_CLS_2, TUT_CLS_1, TUT_CLS_2, TOTAL_CAPACITY, ROOM_ID)";
    //        sql1 += " values ('C_T_0" + code + "','" + dr["COURSE_KEY"] + "', '" + dr["TEACHER_ID"] + "', '" + dr["SECTION"] + "', '" + dr["ACC_CTRL"] + "', '" + dr["SCH_CLS_1"] + "', '" + dr["SCH_CLS_2"] + "', '" + dr["TUT_CLS_1"] + "', '" + dr["TUT_CLS_2"] + "', '" + dr["TOTAL_CAPACITY"].ToString() + "' , '" + dr["ROOM_ID"].ToString() + "' )";



    //        update_code("routine", code);
    //        break;
    //    }

    //    return obj_db.execute_query(sql);
    //}


    public string allocate_room(DataSet ds)
    {
        string sql = " ";

        foreach (DataRow dr in ds.Tables["ROOM"].Rows)
        {
            string code = obj_db.get_pk_no("room");

            sql = @" INSERT into ROOM (ROOM_NAME, ROOM_LOC, DEPT_ID, SPROGRAM)";
            sql += " values ('" + dr["ROOM_NAME"] + "', '" + dr["ROOM_LOC"] + "', '" + dr["DEPT_ID"] + "', '" + dr["SPROGRAM"] + "' )";
            update_code("room", code);
            break;
        }

        return obj_db.execute_query(sql);
    }


    public string allocate_slot(DataSet ds)
    {
        string sql = " ";

        foreach (DataRow dr in ds.Tables["SLOT"].Rows)
        {
            string code = obj_db.get_pk_no("slotdtl");

            sql = @" INSERT into SLOT (SLOTCAT_ID, SLOT_ID, SLOT_NAME, TIME_FROM, TIME_TO)";
            sql += " values ('" + dr["SLOTCAT_ID"] + "','" + dr["SLOT_ID"] + "', '" + dr["SLOT_NAME"] + "', '" + dr["TIME_FROM"] + "', '" + dr["TIME_TO"] + "' )";
            update_code("slotdtl", code);
            break;
        }

        return obj_db.execute_query(sql);
    }


    public string allocate_slots(DataSet ds)
    {
        string sql = " ";

        foreach (DataRow dr in ds.Tables["SLOT_CAT"].Rows)
        {
            string code = obj_db.get_pk_no("slotcat");

            sql = @" INSERT into SLOT_CAT (SLOT_CATEGORY, VALUE)";
            sql += " values ('" + dr["SLOT_CATEGORY"] + "', '" + dr["VALUE"] + "' )";
            update_code("slotcat", code);
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
            sql += " values ('AC" + code + "','" + dr["YEAR"] + "', '" + dr["SEMESTER"] + "', '" + dr["EVENT"] + "', TO_DATE('" + dr["FROM_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'),  TO_DATE('" + dr["TO_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), '" + dr["COMMENTS"] + "', '" + dr["CTRL"] + "' )";
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
            sql += " values ('ACH" + code + "','" + dr["YEAR"] + "', '" + dr["SEMESTER"] + "', '" + dr["DAY_TITLE"] + "', TO_DATE('" + dr["FROM_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["TO_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), '" + dr["COMMENTS"] + "', '" + dr["CTRL"] + "' )";
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
            sql += "  FROM_DATE=TO_DATE('" + dr["FROM_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE=TO_DATE('" + dr["TO_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), COMMENTS='" + dr["COMMENTS"] + "', CTRL='" + dr["CTRL"] + "' where ID='" + dr["ID"] + "' ";
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
            sql += "  FROM_DATE=TO_DATE('" + dr["FROM_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE=TO_DATE('" + dr["TO_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), COMMENTS='" + dr["COMMENTS"] + "', CTRL='" + dr["CTRL"] + "' where ID='" + dr["ID"] + "' ";
            break;
        }
        return obj_db.execute_query(sql);

    }








    public string allocate_room_update(DataSet ds, string room_Id)
    {
        string sql = " ";
        foreach (DataRow dr in ds.Tables["ROOM"].Rows)
        {
            sql = @" update ROOM set ROOM_NAME='" + dr["ROOM_NAME"] + "',  ROOM_LOC='" + dr["ROOM_LOC"] + "', DEPT_ID='" + dr["DEPT_ID"] + "', SPROGRAM=  '" + dr["SPROGRAM"] + "'  where ROOM_ID='" + room_Id + "' ";
            break;


        }

        return obj_db.execute_query(sql);
    }



    public string allocate_slot_update(DataSet ds, string ID)
    {
        string sql = " ";
        foreach (DataRow dr in ds.Tables["SLOT"].Rows)
        {
            sql = @" update SLOT set SLOTCAT_ID='" + dr["SLOTCAT_ID"] + "', SLOT_ID='" + dr["SLOT_ID"] + "',  SLOT_NAME='" + dr["SLOT_NAME"] + "', TIME_FROM='" + dr["TIME_FROM"] + "', TIME_TO=  '" + dr["TIME_TO"] + "'  where SLOT_SL='" + ID + "' ";
            break;
        }
        return obj_db.execute_query(sql);
    }

    public string allocate_slots_update(DataSet ds, string slot_Id)
    {
        string sql = " ";
        foreach (DataRow dr in ds.Tables["SLOT_CAT"].Rows)
        {
            sql = @" update SLOT_CAT set SLOT_CATEGORY='" + dr["SLOT_CATEGORY"] + "',  VALUE='" + dr["VALUE"] + "' where SLOTCAT_ID ='" + slot_Id + "' ";
            break;


        }

        return obj_db.execute_query(sql);
    }


    public string reset_Value(string Slot_Cat)
    {
        string sql = " ";

        sql = @" update SLOT_CAT set VALUE ='0' where SLOT_CATEGORY != '" + Slot_Cat + "'  ";

        return obj_db.execute_query(sql);

    }


    public string make_notice_active_inactive(string noticeId, string status)
    {
        return obj_db.execute_query(" update WEB_NOTICE_BOARD set CTRL='" + status + "' where NOTICE_ID='" + noticeId + "'  ");
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


    public DataTable Check_Dean(string teacher_id)
    {
        string sql = " SELECT DISTINCT WEB_TEACHER_STAFF.* FROM WEB_TEACHER_STAFF where WEB_TEACHER_STAFF.STAFF_ID = '" + teacher_id + "' ";
        return obj_db.get_viewData(sql, "WEB_TEACHER_STAFF");
    }
    public DataTable Check_Department(string employee_id)
    {
        string sql = " SELECT DISTINCT WEB_TEACHER_STAFF.* FROM WEB_TEACHER_STAFF where WEB_TEACHER_STAFF.VALUE = '" + employee_id + "' ";
        return obj_db.get_viewData(sql, "WEB_TEACHER_STAFF");
    }
    public DataTable Check_Staff(string Staff_id)
    {
        string sql = " SELECT DISTINCT WEB_TEACHER_STAFF.* FROM WEB_TEACHER_STAFF where WEB_TEACHER_STAFF.VALUE = '" + Staff_id + "' ";
        return obj_db.get_viewData(sql, "WEB_TEACHER_STAFF");
    }


    public bool is_allocate_teacher_exists(string courseKey, string section)
    {
        string sql = @" SELECT * From WEB_COURSE_TEACHER where COURSE_KEY='" + courseKey + "'and SECTION='" + section + "' ";
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
        string sql = @" SELECT distinct * From WEB_VIEW_COURSE_TEACHER where COURSE_KEY='" + courseKey + "' ";
        return obj_db.get_viewData("" + sql, "WEB_VIEW_COURSE_TEACHER");
    }


    public string delete_allocated_teacher(String techer_course_Id)
    {


        string status = "1";

        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData(@" SELECT count(*)as total From WEB_STUDENT_ATTENDANCE where COURSE_TEACHER_ID='" + techer_course_Id + "' ", "WEB_STUDENT_ATTENDANCE"));
        if (ds.Tables["WEB_STUDENT_ATTENDANCE"].Rows.Count > 0)
        {
            //if (ds.Tables["WEB_STUDENT_ATTENDANCE"].Rows[0]["total"].ToString() != "0")
            //    return "2";
            //else
            //{
            //  delete_teacher_Class(techer_course_Id);
            if (obj_db.execute_query(" Delete from WEB_COURSE_TEACHER where COURSE_TEACHER_ID='" + techer_course_Id + "'  ") == "1")
                status = "1";
            //}
        }
        return status;
    }

    public string delete_dtl_Clsroom(String techer_course_Id)
    {
        DataSet ds = new DataSet();

        string sql1 = " ";
        string sql2 = " ";

        //ds.Merge(obj_db.get_viewData(@" SELECT count(*)as total From C_ROUTINE where C_ROUTINE_ID ='" + C_routine_ID1 + "' and C_ROUTINE_ID ='" + C_routine_ID2 + "' ", "ROOM_LIST"));
        // if (ds.Tables["ROOM_LIST"].Rows.Count > 0)
        // {
        //     foreach (DataRow dr in ds.Tables["C_ROUTINE"].Rows)
        //     {
        sql1 = @" UPDATE C_ROUTINE C SET C.COURSE_TEACHER_ID=''  WHERE   C.COURSE_TEACHER_ID ='" + techer_course_Id + "'  ";

        //  sql2 = @" UPDATE C_ROUTINE C SET C.COURSE_TEACHER_ID=''  WHERE   C.C_ROUTINE_ID ='" + C_routine_ID2 + "'  ";


        //    }
        //}

        string j = obj_db.execute_query(sql1);
        // string k = obj_db.execute_query(sql2);

        //if (j == "1" && k == "1")
        //    return k;
        //else
        return j;




    }
    public string delete_Teachers_room(String techer_course_Id, int DELETIONID, string DELETE_TIME)
    {
        string status = "1";

        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData(@" SELECT count(*)as total From WEB_STUDENT_ATTENDANCE where COURSE_TEACHER_ID='" + techer_course_Id + "' ", "WEB_STUDENT_ATTENDANCE"));
        if (ds.Tables["WEB_STUDENT_ATTENDANCE"].Rows.Count > 0)
        {
            if (ds.Tables["WEB_STUDENT_ATTENDANCE"].Rows[0]["total"].ToString() != "0")
                return "2";
            else
            {
                if (insertIntoDemo(techer_course_Id, DELETIONID, DELETE_TIME) == "1")
                {

                    if (obj_db.execute_query(" Delete from C_ROUTINE_WEB_COURSE_TEACHER where COURSE_TEACHER_ID='" + techer_course_Id + "'  ") == "1")

                        if (obj_db.execute_query(" Delete from WEB_COURSE_TEACHER where COURSE_TEACHER_ID='" + techer_course_Id + "'  ") == "1")
                            status = "1";
                }
            }
        }
        return status;
    }
    public string insertIntoDemo(String techer_course_Id, int DELETIONID, string DELETE_TIME)
    {
        string sql = " ";
        sql = @" Insert into WEB_COURSE_TEACHER_DEMO (COURSE_TEACHER_ID ,  COURSE_KEY ,  TEACHER_ID ,  SECTION  ,  ACC_CTRL  ,  SCH_CLS_1 , ";
        sql += " SCH_CLS_2,  TUT_CLS_1 ,  TUT_CLS_2  ,  TOTAL_CAPACITY  ,  FACULTY_ID ,  SCH_CLS_3 , ";
        sql += "  TUT_CLS_3  ,  INSERTION_TIME  ,   INSERTIONID , DELETIONID  ,  DELETE_TIME, UPDATEID  , UPDATE_TIME ) ";
        sql += "  (select CT.COURSE_TEACHER_ID ,  CT.COURSE_KEY , CT.TEACHER_ID ,  CT.SECTION  , CT.ACC_CTRL  ,CT.SCH_CLS_1 , ";
        sql += "  CT.SCH_CLS_2,  CT.TUT_CLS_1 ,  CT.TUT_CLS_2  ,  CT.TOTAL_CAPACITY  ,  CT.FACULTY_ID ,  CT.SCH_CLS_3 , ";
        sql += "  CT.TUT_CLS_3  ,   CT.INSERTION_TIME  ,   CT.INSERTIONID , ";
        sql += "     '" + DELETIONID + "' as DELETIONID , '" + DELETE_TIME + "' as DELETE_TIME,'', '' from WEB_COURSE_TEACHER CT where CT.COURSE_TEACHER_ID = '" + techer_course_Id + "' ) ";


       // sql = @"Insert into WEB_COURSE_TEACHER_DEMO select WEB_COURSE_TEACHER.* , '" + DELETIONID + "' as DELETIONID, '" + DELETE_TIME + "' as DELETE_TIME from WEB_COURSE_TEACHER where WEB_COURSE_TEACHER.COURSE_TEACHER_ID = '" + techer_course_Id + "' ";
        string i = obj_db.execute_query(sql);
        return i;
    }


    public string UpdateIntoDemo(String techer_course_Id, int UPDATEID, string UPDATE_TIME)
    {
        string sql = " ";
        sql = @" Insert into WEB_COURSE_TEACHER_DEMO (COURSE_TEACHER_ID ,  COURSE_KEY ,  TEACHER_ID ,  SECTION  ,  ACC_CTRL  ,  SCH_CLS_1 , ";
        sql += " SCH_CLS_2,  TUT_CLS_1 ,  TUT_CLS_2  ,  TOTAL_CAPACITY  ,  FACULTY_ID ,  SCH_CLS_3 , ";
        sql += "  TUT_CLS_3  ,  INSERTION_TIME  ,   INSERTIONID , DELETIONID  ,  DELETE_TIME, UPDATEID  , UPDATE_TIME ) ";
        sql += "  (select CT.COURSE_TEACHER_ID ,  CT.COURSE_KEY , CT.TEACHER_ID ,  CT.SECTION  , CT.ACC_CTRL  ,CT.SCH_CLS_1 , ";
        sql += "  CT.SCH_CLS_2,  CT.TUT_CLS_1 ,  CT.TUT_CLS_2  ,  CT.TOTAL_CAPACITY  ,  CT.FACULTY_ID ,  CT.SCH_CLS_3 , ";
        sql += "  CT.TUT_CLS_3  ,   CT.INSERTION_TIME  ,   CT.INSERTIONID , ";
        sql += "    '', '', '" + UPDATEID + "' as UPDATEID , '" + UPDATE_TIME + "' as UPDATE_TIME from WEB_COURSE_TEACHER CT where CT.COURSE_TEACHER_ID = '" + techer_course_Id + "' ) ";


        string i = obj_db.execute_query(sql);
        return i;
    }

    /*
     * COURSE_TEACHER_ID  VARCHAR2(15 BYTE)          NOT NULL,
  COURSE_KEY         VARCHAR2(15 BYTE),
  TEACHER_ID         VARCHAR2(15 BYTE),
  SECTION            VARCHAR2(20 BYTE),
  ACC_CTRL           INTEGER,
  SCH_CLS_1          VARCHAR2(100 BYTE),
  SCH_CLS_2          VARCHAR2(100 BYTE),
  TUT_CLS_1          VARCHAR2(100 BYTE),
  TUT_CLS_2          VARCHAR2(100 BYTE),
  TOTAL_CAPACITY     INTEGER                    DEFAULT 0,
  FACULTY_ID         VARCHAR2(10 BYTE),
  SCH_CLS_3          VARCHAR2(100 BYTE),
  TUT_CLS_3          VARCHAR2(100 BYTE),
  INITIAL_CAPACITY   INTEGER                    DEFAULT 0,
  INSERTION_TIME     VARCHAR2(100 BYTE),
  UPDATE_TIME        VARCHAR2(100 BYTE),
  INSERTIONID        INTEGER,
  UPDATEID           INTEGER,
  DELETIONID         INTEGER,
  DELETE_TIME 
     * */


    public string delete_teacher_Class(String techer_course_Id)
    {
        string status = "1";

        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData(@" SELECT count(*)as total From C_ROUTINE where COURSE_TEACHER_ID='" + techer_course_Id + "' ", "C_ROUTINE_CLASS"));
        if (ds.Tables["C_ROUTINE_CLASS"].Rows.Count > 0)
        {

            if (obj_db.execute_query(" Delete from C_ROUTINE where COURSE_TEACHER_ID='" + techer_course_Id + "'  ") == "1")
                status = "1";

        }
        return status;
    }

    public bool match_roomno(string COURSE_KEY, string ROOM_ID, string SCH_CLS_1, string SCH_CLS_2, string TUT_CLS_1, string TUT_CLS_2)
    {
        string sql = @" SELECT * From WEB_COURSE_TEACHER where COURSE_KEY='" + COURSE_KEY + "'and ROOM_ID='" + ROOM_ID + "'and ((SCH_CLS_1='" + SCH_CLS_1 + "'or SCH_CLS_1='" + SCH_CLS_2 + "'or SCH_CLS_1='" + TUT_CLS_1 + "'or SCH_CLS_1='" + TUT_CLS_2 + "') or (SCH_CLS_2='" + SCH_CLS_1 + "'or SCH_CLS_2='" + SCH_CLS_2 + "'or SCH_CLS_2='" + TUT_CLS_1 + "'or SCH_CLS_2='" + TUT_CLS_2 + "') or (TUT_CLS_1='" + SCH_CLS_1 + "'or TUT_CLS_1='" + SCH_CLS_2 + "'or TUT_CLS_1='" + TUT_CLS_1 + "'or TUT_CLS_1='" + TUT_CLS_2 + "') or (TUT_CLS_2='" + SCH_CLS_1 + "'or TUT_CLS_2='" + SCH_CLS_2 + "'or TUT_CLS_2='" + TUT_CLS_1 + "'or TUT_CLS_2='" + TUT_CLS_2 + "')) ";

        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData("" + sql, "WEB_COURSE_TEACHER"));
        if (ds.Tables["WEB_COURSE_TEACHER"].Rows.Count > 0)

            return true;
        else return false;

    }


    public string delete_allocated_room(String lblRoomID)
    {
        string status = "1";

        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData(@" SELECT count(*)as total From ROOM where ROOM_ID='" + lblRoomID + "' ", "ROOM_LIST"));
        if (ds.Tables["ROOM_LIST"].Rows.Count > 0)
        {
            //if (ds.Tables["ROOM_LIST"].Rows[0]["total"].ToString() != "0")
            //    return "2";
            //else
            //{
            if (obj_db.execute_query(" Delete from ROOM where ROOM_ID='" + lblRoomID + "'  ") == "1")
                status = "1";
            //}
        }
        return status;
    }



    public string delete_allocated_Slotdtl(String lblSlotID)
    {
        string status = "1";

        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData(@" SELECT count(*)as total From SLOT where SLOT_SL='" + lblSlotID + "' ", "SLOT_LIST"));
        if (ds.Tables["SLOT_LIST"].Rows.Count > 0)
        {
            //if (ds.Tables["ROOM_LIST"].Rows[0]["total"].ToString() != "0")
            //    return "2";
            //else
            //{
            if (obj_db.execute_query(" Delete from SLOT where SLOT_SL='" + lblSlotID + "'  ") == "1")
                status = "1";
            //}
        }
        return status;
    }


    public string delete_allocated_slot(String lblSlotID)
    {
        string status = "1";

        DataSet ds = new DataSet();
        ds.Merge(obj_db.get_viewData(@" SELECT count(*)as total From SLOT_CAT where SLOTCAT_ID='" + lblSlotID + "' ", "SLOT_LIST"));
        if (ds.Tables["SLOT_LIST"].Rows.Count > 0)
        {
            //if (ds.Tables["ROOM_LIST"].Rows[0]["total"].ToString() != "0")
            //    return "2";
            //else
            //{
            if (obj_db.execute_query(" Delete from SLOT_CAT where SLOTCAT_ID='" + lblSlotID + "'  ") == "1")
                status = "1";
            //}
        }
        return status;
    }


    public DataTable match_STAFF_CTRL(string STAFF_ID)
    {
        string sql = @"select * from WEB_TEACHER_STAFF where STAFF_ID = '" + STAFF_ID + "'  ";

        return obj_db.get_viewData(sql, "StaffList");
    }

    public DataTable match_dataRoutine(string C_ROUTINE_ID1, string C_ROUTINE_ID2, string C_ROUTINE_ID3)
    {
        string sql = @" select * from C_ROUTINE where C_ROUTINE_ID = '" + C_ROUTINE_ID1 + "' or C_ROUTINE_ID = '" + C_ROUTINE_ID2 + "' or C_ROUTINE_ID = '" + C_ROUTINE_ID3 + "'  ";

        return obj_db.get_viewData(sql, "RoutineList");
    }

    public DataTable match_dataRoutine(string year, string semesterID, string Day1, string Day2, string slotSl1, string slotSl2, string roomID1, string roomID2)
    {
        string sql = @" select * from C_ROUTINE_ROOM_DISTRIBUTION where YEAR_NAME = '" + year + "' and SEMESTER_NO = '" + semesterID + "' and (DAY_WEEK = '" + Day1 + "' or DAY_WEEK = '" + Day2 + "') and (SLOT_SL = '" + slotSl1 + "' or SLOT_SL = '" + slotSl2 + "' ) and (ROOM_ID = '" + roomID1 + "' or ROOM_ID = '" + roomID2 + "' ) ";

        return obj_db.get_viewData(sql, "RoutineList");
    }

    public DataTable match_dataRoutine(string year, string semesterID, string Day1, string Day2, string slotSl1, string slotSl2, string roomID1, string roomID2, string ID)
    {
        string sql = @" select * from C_ROUTINE_ROOM_DISTRIBUTION where COURSE_TEACHER_ID != '" + ID + "' and YEAR_NAME = '" + year + "' and SEMESTER_NO = '" + semesterID + "' and (DAY_WEEK = '" + Day1 + "' or DAY_WEEK = '" + Day2 + "') and (SLOT_SL = '" + slotSl1 + "' or SLOT_SL = '" + slotSl2 + "' ) and (ROOM_ID = '" + roomID1 + "' or ROOM_ID = '" + roomID2 + "' ) ";

        return obj_db.get_viewData(sql, "RoutineList");
    }

    public DataTable match_room(string COURSE_SEM_YEAR, string ROOM_ID, string Class_1, string Class_2, string Tutorial_1, string Tutorial_2)
    {
        string sql = @" SELECT * From WEB_VIEW_ROOM_Distribution where COURSE_KEY like '" + COURSE_SEM_YEAR + "%' and ROOM_ID='" + ROOM_ID + "'and ((Class_1='" + Class_1 + "'or Class_1='" + Class_2 + "'or Class_1='" + Tutorial_1 + "'or Class_1='" + Tutorial_2 + "') or (Class_2='" + Class_1 + "'or Class_2='" + Class_2 + "'or Class_2='" + Tutorial_1 + "'or Class_2='" + Tutorial_2 + "') or (Tutorial_1='" + Class_1 + "'or Tutorial_1='" + Class_2 + "'or Tutorial_1='" + Tutorial_1 + "'or Tutorial_1='" + Tutorial_2 + "') or (Tutorial_2='" + Class_1 + "'or Tutorial_2='" + Class_2 + "'or Tutorial_2='" + Tutorial_1 + "'or Tutorial_2='" + Tutorial_2 + "')) ";

        return obj_db.get_viewData(sql, "RoomList");
    }


    public DataTable match_room(string COURSE_SEM_YEAR, string ROOM_ID, string Class_1, string Class_2, string Tutorial_1, string Tutorial_2, string ID)
    {
        string sql = @" SELECT * From WEB_VIEW_ROOM_Distribution where COURSE_TEACHER_ID != '" + ID + "' and COURSE_KEY like '" + COURSE_SEM_YEAR + "%' and ROOM_ID='" + ROOM_ID + "'and ((Class_1='" + Class_1 + "'or Class_1='" + Class_2 + "'or Class_1='" + Tutorial_1 + "'or Class_1='" + Tutorial_2 + "') or (Class_2='" + Class_1 + "'or Class_2='" + Class_2 + "'or Class_2='" + Tutorial_1 + "'or Class_2='" + Tutorial_2 + "') or (Tutorial_1='" + Class_1 + "'or Tutorial_1='" + Class_2 + "'or Tutorial_1='" + Tutorial_1 + "'or Tutorial_1='" + Tutorial_2 + "') or (Tutorial_2='" + Class_1 + "'or Tutorial_2='" + Class_2 + "'or Tutorial_2='" + Tutorial_1 + "'or Tutorial_2='" + Tutorial_2 + "')) ";

        return obj_db.get_viewData(sql, "RoomList");
    }




    public DataTable match_RoutineList(string SlotID1, string SlotID2, string ID)
    {
        string sql = @" select * from C_ROUTINE_VIEW where (C_ROUTINE_ID = '" + SlotID1 + "' or  C_ROUTINE_ID = '" + SlotID2 + "') and COURSE_TEACHER_ID != '" + ID + "' ";

        return obj_db.get_viewData(sql, "RtnList1");
    }



    public DataTable CHECK_SUBJECT(string COURSECODE, string semester, string year)
    {
        string sql = @" select * from OFFEREDCOURSE where COURSECODE ='" + COURSECODE + "' and YEAR = '" + year + "' and SEMESTER = '" + semester + "' ";

        return obj_db.get_viewData(sql, "COURSE_LIST");
    }







    #region studentMessage
    //-----------------------------common form 
    public DataTable get_a_message(string MESSAGE_ID)
    {
        string sql = " Select * from WEB_STUDENT_MESSAGE where MESSAGE_ID='" + MESSAGE_ID + "' ";
        return obj_db.get_viewData(sql, "WEB_STUDENT_MESSAGE");
    }



    //-------------------------Specific Student message side
    public string save_message(DataSet ds)
    {
        string code = "" + obj_db.get_pk_no("message_SpeStd");

        foreach (DataRow dr in ds.Tables["messagelist"].Rows)
        {
            string sql = @" INSERT INTO WEB_STUDENT_MESSAGE (MESSAGE_ID, TITLE, DESCRIPTION, PUBLISH_DATE, INPUT_DATE, RECIVERSTUDENT_ID,  CTRL) ";
            sql += " values ('MSGS_" + code + "' , '" + dr["TITLE"] + "', '" + dr["DESCRIPTION"] + "',TO_DATE('" + dr["PUBLISH_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'),TO_DATE('" + dr["INPUT_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), '" + dr["RECIVERSTUDENT_ID"] + "',  '" + dr["CTRL"] + "') ";//, '" + dr["INPUT_BY"] + "'

            update_code("message_SpeStd", code);
            return obj_db.execute_query(sql);
        }
        return "";
    }



    public string update_message(DataSet ds)
    {

        foreach (DataRow dr in ds.Tables["messagelist"].Rows)
        {
            string sql = @" update WEB_STUDENT_MESSAGE set  TITLE='" + dr["TITLE"] + "', DESCRIPTION='" + dr["DESCRIPTION"] + "',   PUBLISH_DATE=TO_DATE('" + dr["PUBLISH_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), ";
            sql += " RECIVERSTUDENT_ID ='" + dr["RECIVERSTUDENT_ID"] + "',  CTRL='" + dr["CTRL"] + "' where MESSAGE_ID='" + dr["MESSAGE_ID"] + "' ";

            return obj_db.execute_query(sql);
        }
        return "";
    }

    public DataTable get_filtered_message(DateTime fromDate, DateTime toDate, string type)
    {
        toDate = toDate.AddHours(23);
        string sql = " Select * from WEB_STUDENT_MESSAGE where PUBLISH_DATE>= TO_DATE('" + new cls_tools().get_database_formateDate(fromDate) + "', 'dd/mm/yyyy hh24:mi:ss')   and PUBLISH_DATE<=TO_DATE('" + new cls_tools().get_database_formateDate(toDate) + "', 'dd/mm/yyyy hh24:mi:ss') and CTRL='" + type + "' order by MESSAGE_ID desc ";
        return obj_db.get_viewData(sql, "WEB_STUDENT_MESSAGE");
    }


    public string make_message_active_inactive(string MESSAGE_ID, string status)
    {
        return obj_db.execute_query(" update WEB_STUDENT_MESSAGE set CTRL='" + status + "' where MESSAGE_ID='" + MESSAGE_ID + "'  ");
    }






    //-------------------------Specific Batch message side

    public string save_Bmessage(DataSet ds)
    {
        string code = "" + obj_db.get_pk_no("message_SpeBatch");

        foreach (DataRow dr in ds.Tables["messageBlist"].Rows)
        {
            string sql = @" INSERT INTO WEB_STUDENT_MESSAGE (MESSAGE_ID, TITLE, DESCRIPTION, PUBLISH_DATE, INPUT_DATE, BATCHID,  CTRL) ";
            sql += " values ('MSGB_" + code + "' , '" + dr["TITLE"] + "', '" + dr["DESCRIPTION"] + "',TO_DATE('" + dr["PUBLISH_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["INPUT_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), '" + dr["BATCHID"] + "',  '" + dr["CTRL"] + "') ";//, '" + dr["INPUT_BY"] + "'

            update_code("message_SpeBatch", code);
            return obj_db.execute_query(sql);
        }
        return "";
    }

    public string update_Bmessage(DataSet ds)
    {

        foreach (DataRow dr in ds.Tables["messageBlist"].Rows)
        {
            string sql = @" update WEB_STUDENT_MESSAGE set  TITLE='" + dr["TITLE"] + "', DESCRIPTION='" + dr["DESCRIPTION"] + "',   PUBLISH_DATE=TO_DATE('" + dr["PUBLISH_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), ";
            sql += " BATCHID ='" + dr["BATCHID"] + "',  CTRL='" + dr["CTRL"] + "' where MESSAGE_ID='" + dr["MESSAGE_ID"] + "' ";

            return obj_db.execute_query(sql);
        }
        return "";
    }






    //-------------------------Specific Group message side
    public string save_Gmessage(DataSet ds)
    {
        string code = "" + obj_db.get_pk_no("message_SpeGrp");

        foreach (DataRow dr in ds.Tables["messageGlist"].Rows)
        {
            string sql = @" INSERT INTO WEB_STUDENT_MESSAGE (MESSAGE_ID, TITLE, DESCRIPTION, PUBLISH_DATE, INPUT_DATE, STDGRPID,  CTRL) ";
            sql += " values ('MSGG_" + code + "' , '" + dr["TITLE"] + "', '" + dr["DESCRIPTION"] + "', TO_DATE('" + dr["PUBLISH_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'),TO_DATE('" + dr["INPUT_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss') , '" + dr["STDGRPID"] + "',  '" + dr["CTRL"] + "') ";//, '" + dr["INPUT_BY"] + "'

            update_code("message_SpeGrp", code);
            return obj_db.execute_query(sql);
        }
        return "";
    }

    public string update_Gmessage(DataSet ds)
    {

        foreach (DataRow dr in ds.Tables["messageGlist"].Rows)
        {
            string sql = @" update WEB_STUDENT_MESSAGE set  TITLE='" + dr["TITLE"] + "', DESCRIPTION='" + dr["DESCRIPTION"] + "',   PUBLISH_DATE=TO_DATE('" + dr["PUBLISH_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), ";
            sql += " STDGRPID ='" + dr["STDGRPID"] + "',  CTRL='" + dr["CTRL"] + "' where MESSAGE_ID='" + dr["MESSAGE_ID"] + "' ";

            return obj_db.execute_query(sql);
        }
        return "";
    }





    //-------------------------message for all side
    public string save_Amessage(DataSet ds)
    {
        string code = "" + obj_db.get_pk_no("message_All");

        foreach (DataRow dr in ds.Tables["messageAlist"].Rows)
        {
            string sql = @" INSERT INTO WEB_STUDENT_MESSAGE (MESSAGE_ID, TITLE, DESCRIPTION, PUBLISH_DATE, INPUT_DATE, CTRL) ";
            sql += " values ('MSGA_" + code + "' , '" + dr["TITLE"] + "', '" + dr["DESCRIPTION"] + "',TO_DATE('" + dr["PUBLISH_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), TO_DATE('" + dr["INPUT_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), '" + dr["CTRL"] + "') ";//, '" + dr["INPUT_BY"] + "'

            update_code("message_All", code);
            return obj_db.execute_query(sql);
        }
        return "";
    }

    public string update_Amessage(DataSet ds)
    {

        foreach (DataRow dr in ds.Tables["messageAlist"].Rows)
        {
            string sql = @" update WEB_STUDENT_MESSAGE set  TITLE='" + dr["TITLE"] + "', DESCRIPTION='" + dr["DESCRIPTION"] + "',   PUBLISH_DATE=TO_DATE('" + dr["PUBLISH_DATE"] + "', 'dd/mm/yyyy hh24:mi:ss'), ";
            sql += "CTRL='" + dr["CTRL"] + "' where MESSAGE_ID='" + dr["MESSAGE_ID"] + "' ";

            return obj_db.execute_query(sql);
        }
        return "";
    }
    //-------------------------message side
    #endregion









    public string allocate_Class_room(DataSet ds)
    {
        string sql = " ";
        string sql1 = " ";
        string sql2 = " ";
        string sql3 = " ";

        foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
        {

            string code = obj_db.get_pk_no("course_teacher");

            sql = @" INSERT into WEB_COURSE_TEACHER (COURSE_TEACHER_ID, COURSE_KEY, TEACHER_ID, SECTION, ACC_CTRL, TUT_CLS_1, TUT_CLS_2,TUT_CLS_3,SCH_CLS_1, SCH_CLS_2,SCH_CLS_3,TOTAL_CAPACITY,INSERTIONID,INSERTION_TIME)";
            sql += " values ('C_T_0" + code + "','" + dr["COURSE_KEY"] + "', '" + dr["TEACHER_ID"] + "', '" + dr["SECTION"] + "', '" + dr["ACC_CTRL"] + "', '" + dr["C_ROUTINE_ID1"] + "', '" + dr["C_ROUTINE_ID2"] + "','" + dr["C_ROUTINE_ID3"] + "', '" + dr["SCH_CLS_1"] + "', '" + dr["SCH_CLS_2"] + "','" + dr["SCH_CLS_3"] + "', '" + dr["TOTAL_CAPACITY"].ToString() + "', '" + dr["INSERTIONID"] + "','" + dr["INSERTION_TIME"] + "'  )";


            sql1 = @" INSERT into C_ROUTINE_WEB_COURSE_TEACHER (COURSE_TEACHER_ID, C_ROUTINE_ID) values ('C_T_0" + code + "','" + dr["C_ROUTINE_ID1"] + "' )  ";

            sql2 = @" INSERT into C_ROUTINE_WEB_COURSE_TEACHER (COURSE_TEACHER_ID, C_ROUTINE_ID) values ('C_T_0" + code + "','" + dr["C_ROUTINE_ID2"] + "' )   ";

            sql3 = @" INSERT into C_ROUTINE_WEB_COURSE_TEACHER (COURSE_TEACHER_ID, C_ROUTINE_ID) values ('C_T_0" + code + "','" + dr["C_ROUTINE_ID3"] + "' )  ";


            update_code("course_teacher", code);
            break;
        }

        string i = obj_db.execute_query(sql);
        string j = obj_db.execute_query(sql1);
        string k = obj_db.execute_query(sql2);
        string m = obj_db.execute_query(sql3);

        if (j == "1" && k == "1" && m == "1")
            return m;
        else
            return i;

    }


    public string delete_Previous_Report(DataSet ds, string course_teacherId)
    {
        string status = "1";
        if (course_teacherId != "")
            if (obj_db.execute_query(" Delete from C_ROUTINE_WEB_COURSE_TEACHER where COURSE_TEACHER_ID ='" + course_teacherId + "'  ") == "1")
                status = "1";



        return status;
    }


    public string update_Class_room(DataSet ds, string course_teacherId, string SL1, string SL2, string SL3)
    {
        string sql = " ";
        string sql1 = " ";
        string sql2 = " ";
        string sql3 = " ";

        delete_Previous_Report(ds, course_teacherId);

        foreach (DataRow dr in ds.Tables["WEB_COURSE_TEACHER"].Rows)
        {
            sql = @" update WEB_COURSE_TEACHER set SECTION='" + dr["SECTION"] + "',  TUT_CLS_1='" + SL1 + "', TUT_CLS_2 ='" + SL2 + "', TUT_CLS_3 ='" + SL3 + "', TOTAL_CAPACITY=  '" + dr["TOTAL_CAPACITY"].ToString() + "', ";
            sql += " TEACHER_ID='" + dr["TEACHER_ID"] + "', SCH_CLS_1 ='" + dr["SCH_CLS_1"] + "', SCH_CLS_2 ='" + dr["SCH_CLS_2"] + "' , SCH_CLS_3 ='" + dr["SCH_CLS_3"] + "' , ";
            sql += "  UPDATEID ='" + dr["UPDATEID"] + "' , UPDATE_TIME ='" + dr["UPDATE_TIME"] + "'  where COURSE_TEACHER_ID='" + course_teacherId + "'  ";


            sql1 = @" INSERT into C_ROUTINE_WEB_COURSE_TEACHER (COURSE_TEACHER_ID, C_ROUTINE_ID) values ('" + course_teacherId + "','" + SL1 + "' )  ";

            sql2 = @" INSERT into C_ROUTINE_WEB_COURSE_TEACHER (COURSE_TEACHER_ID, C_ROUTINE_ID) values ('" + course_teacherId + "','" + SL2 + "' )   ";

            sql3 = @" INSERT into C_ROUTINE_WEB_COURSE_TEACHER (COURSE_TEACHER_ID, C_ROUTINE_ID) values ('" + course_teacherId + "','" + SL3 + "' )  ";


            break;


        }

        string i = obj_db.execute_query(sql);
        string a = obj_db.execute_query(sql1);
        string b = obj_db.execute_query(sql2);
        string c = obj_db.execute_query(sql3);


        if (a == "1" && b == "1" && c == "1")
            return c;
        else
            return i;


    }




    public DataTable match_course_Name(string COURSECAT, string Course_Code)
    {
        string sql = @" select * from COURSEDETAILS where COURSECODE ! ='" + Course_Code + "'  and COURSECAT ='" + COURSECAT + "' ";

        return obj_db.get_viewData(sql, "Courselist");
    }

    public DataTable match_CourseCAT(string Course_Code)
    {
        string sql = @" select * from COURSEDETAILS where COURSECODE  ='" + Course_Code + "'  ";

        return obj_db.get_viewData(sql, "CourseCATList");
    }



    public DataTable match_UserID(string USERID, string PASSWORD)
    {
        string sql = @" select * from WEB_ADMINS where USERID  ='" + USERID + "'  and PASSWORD ='" + PASSWORD + "' ";

        return obj_db.get_viewData(sql, "UserList");
    }
}

