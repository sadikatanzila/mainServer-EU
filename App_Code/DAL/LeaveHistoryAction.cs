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
    public class LeaveHistoryAction
    {
         public int update(LeaveHistory leaveHistory)
        {
            int rowsAffected = 0;            
             
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            int parameterCount = 0;
            String query = @"update  LEAVE_HISTORY set ";

            if (leaveHistory.Staff_id != null)//|| leaveHistory.STAFF_NAME.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@STAFF_ID", leaveHistory.Staff_id);
                parameterCount++;
                query += " STAFF_ID=?,";
            }
            if (leaveHistory.Leave_type != null)//|| leaveHistory.P_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@LEAVE_TYPE", leaveHistory.Leave_type);
                parameterCount++;
                query += " LEAVE_TYPE=?,";
            }
            if (leaveHistory.From_date != null)//|| leaveHistory.PER_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@FROM_DATE", leaveHistory.From_date);
                parameterCount++;
                query += " FROM_DATE=?,";
            }
            if (leaveHistory.To_date != null)//|| leaveHistory.PHONE_NUMBER.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@TO_DATE", leaveHistory.To_date);
                parameterCount++;
                query += " TO_DATE=?,";
            }            
            if (leaveHistory.application_date != null)//|| leaveHistory.Gender.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@APPLICATION_DATE", leaveHistory.application_date);
                parameterCount++;
                query += " APPLICATION_DATE=?,";
            }
            if (leaveHistory.Approv_date != null)//|| leaveHistory.E_MAIL.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@APPROV_DATE", leaveHistory.Approv_date);
                parameterCount++;
                query += " APPROV_DATE=?,";
            }
            if (leaveHistory.Reasons != null)//|| leaveHistory.DEPARTMENT.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@REASONS", leaveHistory.Reasons);
                parameterCount++;
                query += " REASONS=?,";
            }
            if (leaveHistory.Creted_date != null)//|| leaveHistory.JOB_TYPE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CRETED_DATE", leaveHistory.Creted_date);
                parameterCount++;
                query += " CRETED_DATE=?,";
            }
            if (leaveHistory.Created_by != null)//|| leaveHistory.JOB_CATEGORY.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CREATED_BY", leaveHistory.Created_by);
                parameterCount++;
                query += " CREATED_BY=?,";
            }
            
            int indexOflastComma = query.LastIndexOf(',');
           query= query.Remove(indexOflastComma);
            

            query += " where LEAVE_HIS_ID=? ";
            objDatabaseHelper.AddParameter("@LEAVE_HIS_ID", leaveHistory.LEAVE_HIS_ID);
            
            
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
                
            }
            catch
            { }

            return rowsAffected;
        }
        public int insert(LeaveHistory leaveHistory)
        {
            int rowsAffected = 0;
            leaveHistory.LEAVE_HIS_ID = new CommonFun().get_pk_no("LEAVE_HIS_ID");

            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            int parameterCount = 0;
            String query = @"insert into LEAVE_HISTORY( ";
            if (leaveHistory.LEAVE_HIS_ID != null)//|| leaveHistory.STAFF_ID.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@LEAVE_HIS_ID",   leaveHistory.LEAVE_HIS_ID);
                parameterCount++;
                query += "LEAVE_HIS_ID,";
            }
            if (leaveHistory.Staff_id != null)//|| leaveHistory.STAFF_NAME.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@STAFF_ID", leaveHistory.Staff_id);
                parameterCount++;
                query += " STAFF_ID,";
            }
            if (leaveHistory.Leave_type != null)//|| leaveHistory.P_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@LEAVE_TYPE", leaveHistory.Leave_type);
                parameterCount++;
                query += " LEAVE_TYPE,";
            }
            if (leaveHistory.From_date != null)//|| leaveHistory.PER_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@FROM_DATE", leaveHistory.From_date);
                parameterCount++;
                query += " FROM_DATE,";
            }
            if (leaveHistory.To_date != null)//|| leaveHistory.PHONE_NUMBER.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@TO_DATE", leaveHistory.To_date);
                parameterCount++;
                query += " TO_DATE,";
            }
            
            if (leaveHistory.application_date != null)//|| leaveHistory.Gender.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@APPLICATION_DATE", leaveHistory.application_date);
                parameterCount++;
                query += " APPLICATION_DATE,";
            }
            if (leaveHistory.Approv_date != null)//|| leaveHistory.E_MAIL.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@APPROV_DATE", leaveHistory.Approv_date);
                parameterCount++;
                query += " APPROV_DATE,";
            }
            if (leaveHistory.Reasons != null)//|| leaveHistory.DEPARTMENT.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@REASONS", leaveHistory.Reasons);
                parameterCount++;
                query += " REASONS,";
            }
            if (leaveHistory.Creted_date != null)//|| leaveHistory.JOB_TYPE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CRETED_DATE", leaveHistory.Creted_date);
                parameterCount++;
                query += " CRETED_DATE,";
            }
            if (leaveHistory.Created_by != null)//|| leaveHistory.JOB_CATEGORY.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CREATED_BY", leaveHistory.Created_by);
                parameterCount++;
                query += " CREATED_BY,";
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
                new CommonFun().update_code("LEAVE_HIS_ID", leaveHistory.LEAVE_HIS_ID);

            }
            catch
            { }

            return rowsAffected;
        }
        public int delete(string leaveHistoryID)
        {
            int rowsAffected = 0;
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = " delete from  LEAVE_HISTORY where LEAVE_HIS_ID=?";

            objDatabaseHelper.AddParameter("@LEAVE_HIS_ID", leaveHistoryID);
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
            }
            catch
            { }
            return rowsAffected;
        }
        public List<LeaveHistory> getAll(string staff_id)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from VIEW_HR_LEAVE_HISTORY where STAFF_ID=?";
            objhelper.AddParameter("@STAFF_ID",staff_id);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            List<LeaveHistory> listLeaveHistory = new List<LeaveHistory>();
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                LeaveHistory tempLeaveHistory = new LeaveHistory();
               
                tempLeaveHistory.LEAVE_HIS_ID = dr["LEAVE_HIS_ID"].ToString();
                tempLeaveHistory.Staff_id = dr["STAFF_ID"].ToString();
                tempLeaveHistory.Leave_type = dr["LEAVE_TYPE"].ToString();
                tempLeaveHistory.Leave_type_name = dr["TYPE_NAME"].ToString();
                tempLeaveHistory.From_date = Convert.ToDateTime(dr["FROM_DATE"]);
                tempLeaveHistory.To_date = Convert.ToDateTime(dr["TO_DATE"]);                
                tempLeaveHistory.application_date = Convert.ToDateTime(dr["APPLICATION_DATE"]);
                tempLeaveHistory.Approv_date = Convert.ToDateTime(dr["APPROV_DATE"]);
                tempLeaveHistory.Reasons = dr["REASONS"].ToString();
                
                if (dr["CRETED_DATE"].ToString() != "")
                tempLeaveHistory.Creted_date =Convert.ToDateTime( dr["CRETED_DATE"]);
                if (dr["CREATED_BY"].ToString() != "")
                tempLeaveHistory.Created_by = dr["CREATED_BY"].ToString();
               

                listLeaveHistory.Add(tempLeaveHistory);
            }
            return listLeaveHistory;
        }
        public List<LeaveHistory> getAll(string staff_id,DateTime startDate,DateTime endDate)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from VIEW_HR_LEAVE_HISTORY where STAFF_ID=? and FROM_DATE>= ? and FROM_DATE<= ? ";
            objhelper.AddParameter("@STAFF_ID",staff_id);
            objhelper.AddParameter("@startDate",startDate);
            objhelper.AddParameter("@endDate",endDate);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            List<LeaveHistory> listLeaveHistory = new List<LeaveHistory>();
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                LeaveHistory tempLeaveHistory = new LeaveHistory();
               
                tempLeaveHistory.LEAVE_HIS_ID = dr["LEAVE_HIS_ID"].ToString();
                tempLeaveHistory.Staff_id = dr["STAFF_ID"].ToString();
                tempLeaveHistory.Leave_type = dr["LEAVE_TYPE"].ToString();
                tempLeaveHistory.Leave_type_name = dr["TYPE_NAME"].ToString();
                tempLeaveHistory.From_date =Convert.ToDateTime( dr["FROM_DATE"]);
                tempLeaveHistory.To_date = Convert.ToDateTime(dr["TO_DATE"]);                
                tempLeaveHistory.application_date = Convert.ToDateTime(dr["APPLICATION_DATE"]);
                tempLeaveHistory.Approv_date = Convert.ToDateTime(dr["APPROV_DATE"]);
                tempLeaveHistory.Reasons = dr["REASONS"].ToString();

                if (dr["CRETED_DATE"].ToString() != "")
                    tempLeaveHistory.Creted_date = Convert.ToDateTime(dr["CRETED_DATE"]);
                if (dr["CREATED_BY"].ToString() != "")
                tempLeaveHistory.Created_by = dr["CREATED_BY"].ToString();
               

                listLeaveHistory.Add(tempLeaveHistory);
            }
            return listLeaveHistory;
        }
        public LeaveHistory get(string StaffId)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from WEB_TEACHER_STAFF where STAFF_ID = ?";
            objhelper.AddParameter("@STAFF_ID", StaffId);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            LeaveHistory listLeaveHistory = new LeaveHistory();

            if (ds.Tables[0].Rows.Count > 0)
            {
                LeaveHistory tempLeaveHistory = new LeaveHistory();
                tempLeaveHistory.LEAVE_HIS_ID = ds.Tables[0].Rows[0]["LEAVE_HIS_ID"].ToString();
                tempLeaveHistory.Staff_id = ds.Tables[0].Rows[0]["STAFF_ID"].ToString();
                tempLeaveHistory.Leave_type = ds.Tables[0].Rows[0]["LEAVE_TYPE"].ToString();
                tempLeaveHistory.From_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["FROM_DATE"]);
                tempLeaveHistory.To_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["TO_DATE"]);                
                tempLeaveHistory.application_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["APPLICATION_DATE"]);
                tempLeaveHistory.Approv_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["APPROV_DATE"]);
                tempLeaveHistory.Reasons = ds.Tables[0].Rows[0]["REASONS"].ToString();

                if (ds.Tables[0].Rows[0]["CREATED_DATE"].ToString() != "")
                tempLeaveHistory.Creted_date =Convert.ToDateTime( ds.Tables[0].Rows[0]["CREATED_DATE"]);
                if (ds.Tables[0].Rows[0]["CREATED_BY"].ToString() != "")
                tempLeaveHistory.Created_by = ds.Tables[0].Rows[0]["CREATED_BY"].ToString();
                
                listLeaveHistory = tempLeaveHistory;
            }
            return listLeaveHistory;
        }
    }


    

