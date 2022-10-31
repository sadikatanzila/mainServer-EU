using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

    public class StaffAction
    {
       
        public int update(Staff staff)
        {
            int rowsAffected = 0;            
             
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            int parameterCount = 0;
            String query = @"update  WEB_TEACHER_STAFF set ";
           
            if (staff.STAFF_NAME != null)//|| staff.STAFF_NAME.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@STAFF_NAME", staff.STAFF_NAME);
                parameterCount++;
                query += " STAFF_NAME=?,";
            }
            if (staff.P_ADDRESS != null)//|| staff.P_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@P_ADDRESS", staff.P_ADDRESS);
                parameterCount++;
                query += " P_ADDRESS=?,";
            }
            if (staff.PER_ADDRESS != null)//|| staff.PER_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@PER_ADDRESS", staff.PER_ADDRESS);
                parameterCount++;
                query += " PER_ADDRESS=?,";
            }
            if (staff.PHONE_NUMBER != null)//|| staff.PHONE_NUMBER.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@PHONE_NUMBER", staff.PHONE_NUMBER);
                parameterCount++;
                query += " PHONE_NUMBER=?,";
            }
            if (staff.MOBILE != null)//|| staff.MOBILE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@MOBILE", staff.MOBILE);
                parameterCount++;
                query += " MOBILE=?,";
            }
            if (staff.Gender != null)//|| staff.Gender.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@GENDER", staff.Gender);
                parameterCount++;
                query += " GENDER=?,";
            }
            if (staff.E_MAIL != null)//|| staff.E_MAIL.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@E_MAIL", staff.E_MAIL);
                parameterCount++;
                query += " E_MAIL=?,";
            }
            if (staff.DEPARTMENT != null)//|| staff.DEPARTMENT.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@DEPARTMENT", staff.DEPARTMENT);
                parameterCount++;
                query += " DEPARTMENT=?,";
            }
            if (staff.JOB_TYPE != null)//|| staff.JOB_TYPE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@JOB_TYPE", staff.JOB_TYPE);
                parameterCount++;
                query += " JOB_TYPE=?,";
            }
            if (staff.JOB_CATEGORY != null)//|| staff.JOB_CATEGORY.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@JOB_CATEGORY", staff.JOB_CATEGORY);
                parameterCount++;
                query += " JOB_CATEGORY=?,";
            }
            if (staff.JOB_DESIGNATION != null)//|| staff.JOB_DESIGNATION.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@JOB_DESIGNATION", staff.JOB_DESIGNATION);
                parameterCount++;
                query += " JOB_DESIGNATION=?,";
            }
            if (staff.JOIN_DATE != null)//|| staff.JOIN_DATE.ToString().Trim() != "")
            {
                objDatabaseHelper.AddParameter("@JOIN_DATE", staff.JOIN_DATE);
                parameterCount++;
                query += " JOIN_DATE=?,";
            }
            if (staff.LOGIN_NAME != null)//|| staff.LOGIN_NAME.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@LOGIN_NAME", staff.LOGIN_NAME);
                parameterCount++;
                query += " LOGIN_NAME=?,";
            }
          /*  if (staff.PASSWORD != null)//|| staff.PASSWORD.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@PASSWORD", staff.PASSWORD);
                parameterCount++;
                query += " PASSWORD=?,";
            }*/
            if (staff.STAFF_PICTURE != null)//|| staff.STAFF_PICTURE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@STAFF_PICTURE", staff.STAFF_PICTURE);
                parameterCount++;
                query += " STAFF_PICTURE=?,";
            }
            if (staff.IS_ADVISOR != null)//|| staff.IS_ADVISOR.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@IS_ADVISOR", staff.IS_ADVISOR);
                parameterCount++;
                query += " IS_ADVISOR=?,";
            }
            if (staff.STAFF_CTRL != null)//|| staff.STAFF_CTRL.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@STAFF_CTRL", staff.STAFF_CTRL);
                parameterCount++;
                query += " STAFF_CTRL=?,";
            }
            if (staff.REGINED_REASON != null)// || staff.REGINED_REASON.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@REGINED_REASON", staff.REGINED_REASON);
                parameterCount++;
                query += " REGINED_REASON=?,";
            }
            if (staff.REGINED_DATE != null)//|| staff.REGINED_DATE.ToString().Trim() != "")
            {
                objDatabaseHelper.AddParameter("@REGINED_DATE", staff.REGINED_DATE);
                parameterCount++;
                query += " REGINED_DATE=?,";
            }
            if (staff.CREATED_DATE != null)// || staff.CREATED_DATE.ToString().Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CREATED_DATE", staff.CREATED_DATE);
                parameterCount++;
                query += " CREATED_DATE=?,";
            }
            if (staff.CREATED_BY != null )//|| staff.CREATED_BY.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CREATED_BY", staff.CREATED_BY);
                parameterCount++;
                query += " CREATED_BY=?,";
            }
            int indexOflastComma = query.LastIndexOf(',');
           query= query.Remove(indexOflastComma);
            

            query += " where STAFF_ID=? ";
            objDatabaseHelper.AddParameter("@STAFF_ID", staff.STAFF_ID);
            
            
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
                
            }
            catch
            { }

            return rowsAffected;
        }
        public int insert(Staff staff)
        {
            int rowsAffected = 0;
            staff.STAFF_ID = new CommonFun().get_pk_no("staff");

            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            int parameterCount = 0;
            String query = @"insert into WEB_TEACHER_STAFF( ";
            if (staff.STAFF_ID != null)//|| staff.STAFF_ID.Trim() != "")
            {
                if(staff.JOB_CATEGORY=="Teacher")
                {
                    objDatabaseHelper.AddParameter("@STAFF_ID", "HRT" + staff.STAFF_ID);
                }
                else
                {
                    objDatabaseHelper.AddParameter("@STAFF_ID", "HRO" + staff.STAFF_ID);
                }
                
                parameterCount++;
                query += "STAFF_ID,";
            }
            if (staff.STAFF_NAME != null)//|| staff.STAFF_NAME.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@STAFF_NAME", staff.STAFF_NAME);
                parameterCount++;
                query += "STAFF_NAME,";
            }
            if (staff.P_ADDRESS != null)//|| staff.P_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@P_ADDRESS", staff.P_ADDRESS);
                parameterCount++;
                query += "P_ADDRESS,";
            }
            if (staff.PER_ADDRESS != null)//|| staff.PER_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@PER_ADDRESS", staff.PER_ADDRESS);
                parameterCount++;
                query += "PER_ADDRESS,";
            }
            if (staff.PHONE_NUMBER != null)//|| staff.PHONE_NUMBER.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@PHONE_NUMBER", staff.PHONE_NUMBER);
                parameterCount++;
                query += "PHONE_NUMBER,";
            }
            if (staff.MOBILE != null)//|| staff.MOBILE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@MOBILE", staff.MOBILE);
                parameterCount++;
                query += "MOBILE,";
            }
            if (staff.Gender != null)//|| staff.Gender.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@GENDER", staff.Gender);
                parameterCount++;
                query += "GENDER,";
            }
            if (staff.E_MAIL != null)//|| staff.E_MAIL.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@E_MAIL", staff.E_MAIL);
                parameterCount++;
                query += "E_MAIL,";
            }
            if (staff.DEPARTMENT != null)//|| staff.DEPARTMENT.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@DEPARTMENT", staff.DEPARTMENT);
                parameterCount++;
                query += "DEPARTMENT,";
            }
            if (staff.JOB_TYPE != null)//|| staff.JOB_TYPE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@JOB_TYPE", staff.JOB_TYPE);
                parameterCount++;
                query += "JOB_TYPE,";
            }
            if (staff.JOB_CATEGORY != null)//|| staff.JOB_CATEGORY.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@JOB_CATEGORY", staff.JOB_CATEGORY);
                parameterCount++;
                query += "JOB_CATEGORY,";
            }
            if (staff.JOB_DESIGNATION != null)//|| staff.JOB_DESIGNATION.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@JOB_DESIGNATION", staff.JOB_DESIGNATION);
                parameterCount++;
                query += "JOB_DESIGNATION,";
            }
            if (staff.JOIN_DATE != null)//|| staff.JOIN_DATE.ToString().Trim() != "")
            {
                objDatabaseHelper.AddParameter("@JOIN_DATE", staff.JOIN_DATE);
                parameterCount++;
                query += "JOIN_DATE,";
            }
            if (staff.LOGIN_NAME != null)//|| staff.LOGIN_NAME.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@LOGIN_NAME", staff.LOGIN_NAME);
                parameterCount++;
                query += "LOGIN_NAME,";
            }
            if (staff.PASSWORD != null)//|| staff.PASSWORD.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@PASSWORD", staff.PASSWORD);
                parameterCount++;
                query += "PASSWORD,";
            }
            if (staff.STAFF_PICTURE != null)//|| staff.STAFF_PICTURE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@STAFF_PICTURE", staff.STAFF_PICTURE);
                parameterCount++;
                query += "STAFF_PICTURE,";
            }
            if (staff.IS_ADVISOR != null)//|| staff.IS_ADVISOR.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@IS_ADVISOR", staff.IS_ADVISOR);
                parameterCount++;
                query += "IS_ADVISOR,";
            }
            if (staff.STAFF_CTRL != null)//|| staff.STAFF_CTRL.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@STAFF_CTRL", staff.STAFF_CTRL);
                parameterCount++;
                query += "STAFF_CTRL,";
            }
            if (staff.REGINED_REASON != null)// || staff.REGINED_REASON.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@REGINED_REASON", staff.REGINED_REASON);
                parameterCount++;
                query += "REGINED_REASON,";
            }
            if (staff.REGINED_DATE != null)//|| staff.REGINED_DATE.ToString().Trim() != "")
            {
                objDatabaseHelper.AddParameter("@REGINED_DATE", staff.REGINED_DATE);
                parameterCount++;
                query += "REGINED_DATE,";
            }
            if (staff.CREATED_DATE != null)// || staff.CREATED_DATE.ToString().Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CREATED_DATE", staff.CREATED_DATE);
                parameterCount++;
                query += "CREATED_DATE,";
            }
            if (staff.CREATED_BY != null)//|| staff.CREATED_BY.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CREATED_BY", staff.CREATED_BY);
                parameterCount++;
                query += "CREATED_BY,";
            }
            int indexOflastComma = query.LastIndexOf(',');
            query = query.Remove(indexOflastComma);
            query += " ) ";

            query += " values( ";
            for (int i = 0; i < parameterCount; i++)
            {
                query += " ? ";
                if (i < parameterCount - 1) query += ",";
            }
            query += " ) ";


            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
                new CommonFun().update_code("staff", staff.STAFF_ID);

            }
            catch
            { }

            return rowsAffected;
        }
        public int delete(Staff staff)
        {
            int rowsAffected = 0;
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = " delete from  WEB_TEACHER_STAFF where STAFF_ID=?";

            objDatabaseHelper.AddParameter("@STAFF_ID", staff.STAFF_ID);
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
            }
            catch
            { }
            return rowsAffected;
        }
        public List<Staff> getAll()
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from WEB_TEACHER_STAFF,college where college.collegecode=WEB_TEACHER_STAFF.DEPARTMENT";
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            List<Staff> listStaff = new List<Staff>();
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Staff tempStaff = new Staff();
                tempStaff.STAFF_ID = dr["STAFF_ID"].ToString();
                tempStaff.STAFF_NAME = dr["STAFF_NAME"].ToString();
                tempStaff.P_ADDRESS = dr["P_ADDRESS"].ToString();
                tempStaff.PER_ADDRESS = dr["PER_ADDRESS"].ToString();
                tempStaff.MOBILE = dr["MOBILE"].ToString();
                tempStaff.Gender = dr["Gender"].ToString();
                tempStaff.E_MAIL = dr["E_MAIL"].ToString();
                tempStaff.DEPARTMENT = dr["COLLEGENAME"].ToString();
                tempStaff.JOB_TYPE = dr["JOB_TYPE"].ToString();
                tempStaff.JOB_CATEGORY = dr["JOB_CATEGORY"].ToString();
                tempStaff.JOB_DESIGNATION = dr["JOB_DESIGNATION"].ToString();
                if(dr["JOIN_DATE"].ToString()!="")
                tempStaff.JOIN_DATE =Convert.ToDateTime( dr["JOIN_DATE"]);
                tempStaff.LOGIN_NAME = dr["LOGIN_NAME"].ToString();
                tempStaff.PASSWORD = dr["PASSWORD"].ToString();
                if (dr["STAFF_PICTURE"].ToString() != "")
                tempStaff.STAFF_PICTURE = dr["STAFF_PICTURE"].ToString();
                tempStaff.STAFF_CTRL = dr["STAFF_CTRL"].ToString();
                tempStaff.REGINED_REASON = dr["REGINED_REASON"].ToString();
                if(dr["REGINED_DATE"].ToString()!="")
                tempStaff.REGINED_DATE =Convert.ToDateTime( dr["REGINED_DATE"]);
                if (dr["CREATED_DATE"].ToString() != "")
                tempStaff.CREATED_DATE =Convert.ToDateTime( dr["CREATED_DATE"]);
                if (dr["CREATED_BY"].ToString() != "")
                tempStaff.CREATED_BY = dr["CREATED_BY"].ToString();
                

                listStaff.Add(tempStaff);
            }
            return listStaff;
        }
        public Staff get(string StaffId)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from WEB_TEACHER_STAFF where STAFF_ID = ?";
            objhelper.AddParameter("@STAFF_ID", StaffId);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            Staff listStaff = new Staff();

            if (ds.Tables[0].Rows.Count > 0)
            {
                Staff tempStaff = new Staff();
                tempStaff.STAFF_ID = ds.Tables[0].Rows[0]["STAFF_ID"].ToString();
                tempStaff.STAFF_NAME = ds.Tables[0].Rows[0]["STAFF_NAME"].ToString();
                tempStaff.P_ADDRESS = ds.Tables[0].Rows[0]["P_ADDRESS"].ToString();
                tempStaff.PER_ADDRESS = ds.Tables[0].Rows[0]["PER_ADDRESS"].ToString();
                tempStaff.MOBILE = ds.Tables[0].Rows[0]["MOBILE"].ToString();
                tempStaff.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                tempStaff.E_MAIL = ds.Tables[0].Rows[0]["E_MAIL"].ToString();
                tempStaff.DEPARTMENT = ds.Tables[0].Rows[0]["DEPARTMENT"].ToString();
                tempStaff.JOB_TYPE = ds.Tables[0].Rows[0]["JOB_TYPE"].ToString();
                tempStaff.JOB_CATEGORY = ds.Tables[0].Rows[0]["JOB_CATEGORY"].ToString();
                tempStaff.JOB_DESIGNATION = ds.Tables[0].Rows[0]["JOB_DESIGNATION"].ToString();
                if (ds.Tables[0].Rows[0]["JOIN_DATE"].ToString() != "")
                tempStaff.JOIN_DATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["JOIN_DATE"]);
                tempStaff.LOGIN_NAME = ds.Tables[0].Rows[0]["LOGIN_NAME"].ToString();
                tempStaff.PASSWORD = ds.Tables[0].Rows[0]["PASSWORD"].ToString();
                if (ds.Tables[0].Rows[0]["STAFF_PICTURE"].ToString() != "")
                tempStaff.STAFF_PICTURE = ds.Tables[0].Rows[0]["STAFF_PICTURE"].ToString();
                tempStaff.STAFF_CTRL = ds.Tables[0].Rows[0]["STAFF_CTRL"].ToString();
                if (ds.Tables[0].Rows[0]["REGINED_DATE"].ToString() != "")
                    tempStaff.REGINED_DATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["REGINED_DATE"]);
                if (ds.Tables[0].Rows[0]["CREATED_DATE"].ToString() != "")
                    tempStaff.CREATED_DATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["CREATED_DATE"]);
                if (ds.Tables[0].Rows[0]["CREATED_BY"].ToString() != "")
                    tempStaff.CREATED_BY = ds.Tables[0].Rows[0]["CREATED_BY"].ToString();
                tempStaff.CREATED_BY = ds.Tables[0].Rows[0]["CREATED_BY"].ToString();
                listStaff = tempStaff;
            }
            return listStaff;
        }
    }


    
