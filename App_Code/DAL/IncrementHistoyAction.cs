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
    public class IncrementHistoyAction
    {

        public DataSet getIncrementableEmployee(DateTime startDate, DateTime endDate)
        {
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();

            String query = @"SELECT INCREMENT_HIS_ID,WEB_TEACHER_STAFF. STAFF_ID,WEB_TEACHER_STAFF.staff_name,  "+
            " ACTIVE_DATE, NEXT_INCREMENT_DATE, COMMENTS, INCREMENT_CTRL,  "+
            "     INCREMENT_HISTORY.CRETED_DATE, INCREMENT_HISTORY.CREATED_BY  "+
            " 	  FROM INCREMENT_HISTORY, WEB_TEACHER_STAFF    "+
            " 	  WHERE NEXT_INCREMENT_DATE  BETWEEN ? AND ?   "+      
            " 	  AND WEB_TEACHER_STAFF.STAFF_ID=INCREMENT_HISTORY.STAFF_ID";  
            objDatabaseHelper.AddParameter("@startDate", startDate);  
            objDatabaseHelper.AddParameter("@endDate", endDate);
            DataSet ds = new DataSet();
            try
            {
                ds.Merge(objDatabaseHelper.ExecuteDataSet(query));
            }
            catch { }
            return ds;


        }
        public int update(IncrementHistory incrementHistory)
        {
            int rowsAffected = 0;            
             
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            int parameterCount = 0;
            String query = @"update  INCREMENT_HISTORY set ";

            if (incrementHistory.Staff_ID != null)//|| incrementHistory.STAFF_NAME.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@STAFF_ID", incrementHistory.Staff_ID);
                parameterCount++;
                query += " STAFF_ID=?,";
            }
            if (incrementHistory.Increment_amount != null)//|| incrementHistory.P_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@Increment_amount", incrementHistory.Increment_amount);
                parameterCount++;
                query += " INCREMENT_AMOUNT=?,";
            }
            if (incrementHistory.Active_date != null)//|| incrementHistory.PER_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@Active_date", incrementHistory.Active_date);
                parameterCount++;
                query += " ACTIVE_DATE=?,";
            }
            if (incrementHistory.Next_increment_date != null)//|| incrementHistory.PHONE_NUMBER.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@Next_increment_date", incrementHistory.Next_increment_date);
                parameterCount++;
                query += " NEXT_INCREMENT_DATE=?,";
            }
            if (incrementHistory.comments != null)//|| incrementHistory.MOBILE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@comments", incrementHistory.comments);
                parameterCount++;
                query += " COMMENTS=?,";
            }
            if (incrementHistory.Increment_ctrl != null)//|| incrementHistory.Gender.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@Increment_ctrl", incrementHistory.Increment_ctrl);
                parameterCount++;
                query += " INCREMENT_CTRL=?,";
            }
           
            if (incrementHistory.Creted_date != null)//|| incrementHistory.JOB_TYPE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CRETED_DATE", incrementHistory.Creted_date);
                parameterCount++;
                query += " CRETED_DATE=?,";
            }
            if (incrementHistory.Created_by != null)//|| incrementHistory.JOB_CATEGORY.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CREATED_BY", incrementHistory.Created_by);
                parameterCount++;
                query += " CREATED_BY=?,";
            }
            
            int indexOflastComma = query.LastIndexOf(',');
           query= query.Remove(indexOflastComma);


           query += " where INCREMENT_HIS_ID=? ";
           objDatabaseHelper.AddParameter("@INCREMENT_HIS_ID", incrementHistory.Increment_his_ID);
            
            
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
                
            }
            catch
            { }

            return rowsAffected;
        }
        public int insert(IncrementHistory incrementHistory)
        {
            int rowsAffected = 0;
            incrementHistory.Increment_his_ID = new CommonFun().get_pk_no("INCREMENT_HIS_ID");

            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            int parameterCount = 0;
            String query = @"insert into INCREMENT_HISTORY( ";
            if (incrementHistory.Increment_his_ID != null)//|| incrementHistory.STAFF_ID.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@INCREMENT_HIS_ID", incrementHistory.Increment_his_ID);
                parameterCount++;
                query += "INCREMENT_HIS_ID,";
            }
            if (incrementHistory.Staff_ID != null)//|| incrementHistory.STAFF_NAME.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@STAFF_ID", incrementHistory.Staff_ID);
                parameterCount++;
                query += " STAFF_ID,";
            }
            if (incrementHistory.Increment_amount != null)//|| incrementHistory.P_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@INCREMENT_AMOUNT", incrementHistory.Increment_amount);
                parameterCount++;
                query += " INCREMENT_AMOUNT,";
            }
            if (incrementHistory.Active_date != null)//|| incrementHistory.PER_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@ACTIVE_DATE", incrementHistory.Active_date);
                parameterCount++;
                query += " ACTIVE_DATE,";
            }
            if (incrementHistory.Next_increment_date != null)//|| incrementHistory.PHONE_NUMBER.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@NEXT_INCREMENT_DATE", incrementHistory.Next_increment_date);
                parameterCount++;
                query += " NEXT_INCREMENT_DATE,";
            }
            if (incrementHistory.comments != null)//|| incrementHistory.MOBILE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@COMMENTS", incrementHistory.comments);
                parameterCount++;
                query += " COMMENTS,";
            }
            if (incrementHistory.Increment_ctrl != null)//|| incrementHistory.Gender.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@INCREMENT_CTRL", incrementHistory.Increment_ctrl);
                parameterCount++;
                query += " INCREMENT_CTRL,";
            }
            
            if (incrementHistory.Creted_date != null)//|| incrementHistory.JOB_TYPE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CRETED_DATE", incrementHistory.Creted_date);
                parameterCount++;
                query += " CRETED_DATE,";
            }
            if (incrementHistory.Created_by != null)//|| incrementHistory.JOB_CATEGORY.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CREATED_BY", incrementHistory.Created_by);
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
                new CommonFun().update_code("INCREMENT_HIS_ID", incrementHistory.Increment_his_ID);

            }
            catch
            { }

            return rowsAffected;
        }
        public int delete(string INCREMENT_HIS_ID)
        {
            int rowsAffected = 0;
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = " delete from  INCREMENT_HISTORY where INCREMENT_HIS_ID=?";

            objDatabaseHelper.AddParameter("@INCREMENT_HIS_ID", INCREMENT_HIS_ID);
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
            }
            catch
            { }
            return rowsAffected;
        }
        public List<IncrementHistory> getAll(string staff_id)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from INCREMENT_HISTORY where STAFF_ID=?";
            objhelper.AddParameter("@STAFF_ID",staff_id);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            List<IncrementHistory> listIncrementHistory = new List<IncrementHistory>();
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                IncrementHistory tempIncrementHistory = new IncrementHistory();
               
                tempIncrementHistory.Increment_his_ID = dr["INCREMENT_HIS_ID"].ToString();
                tempIncrementHistory.Staff_ID = dr["STAFF_ID"].ToString();
                tempIncrementHistory.Active_date =Convert.ToDateTime( dr["ACTIVE_DATE"]);
                tempIncrementHistory.comments = dr["COMMENTS"].ToString();
                tempIncrementHistory.Increment_amount = dr["INCREMENT_AMOUNT"].ToString();
                tempIncrementHistory.Increment_ctrl = dr["INCREMENT_CTRL"].ToString();
                tempIncrementHistory.Next_increment_date = Convert.ToDateTime( dr["NEXT_INCREMENT_DATE"]);
                
                
                if (dr["CRETED_DATE"].ToString() != "")
                    tempIncrementHistory.Creted_date = Convert.ToDateTime(dr["CRETED_DATE"]);
                if (dr["CREATED_BY"].ToString() != "")
                    tempIncrementHistory.Created_by = dr["CREATED_BY"].ToString();
               

                listIncrementHistory.Add(tempIncrementHistory);
            }
            return listIncrementHistory;
        }
        public List<IncrementHistory> getAll(string staff_id,DateTime startDate,DateTime endDate)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from INCREMENT_HISTORY where STAFF_ID=? and FROM_DATE>= ? and FROM_DATE<= ? ";
            objhelper.AddParameter("@STAFF_ID",staff_id);
            objhelper.AddParameter("@startDate",startDate);
            objhelper.AddParameter("@endDate",endDate);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            List<IncrementHistory> listIncrementHistory = new List<IncrementHistory>();
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                IncrementHistory tempIncrementHistory = new IncrementHistory();

                tempIncrementHistory.Increment_his_ID = dr["INCREMENT_HIS_ID"].ToString();
                tempIncrementHistory.Staff_ID = dr["STAFF_ID"].ToString();
                tempIncrementHistory.Active_date = Convert.ToDateTime(dr["ACTIVE_DATE"]);
                tempIncrementHistory.comments = dr["COMMENTS"].ToString();
                tempIncrementHistory.Increment_amount = dr["INCREMENT_AMOUNT"].ToString();
                tempIncrementHistory.Increment_ctrl = dr["INCREMENT_CTRL"].ToString();
                tempIncrementHistory.Next_increment_date = Convert.ToDateTime(dr["NEXT_INCREMENT_DATE"]);


                if (dr["CREATED_DATE"].ToString() != "")
                    tempIncrementHistory.Creted_date = Convert.ToDateTime(dr["CRETED_DATE"]);
                if (dr["CREATED_BY"].ToString() != "")
                    tempIncrementHistory.Created_by = dr["CREATED_BY"].ToString();
               

                listIncrementHistory.Add(tempIncrementHistory);
            }
            return listIncrementHistory;
        }
        public IncrementHistory get(string StaffId)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from WEB_TEACHER_STAFF where STAFF_ID = ?";
            objhelper.AddParameter("@STAFF_ID", StaffId);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            IncrementHistory listIncrementHistory = new IncrementHistory();

            if (ds.Tables[0].Rows.Count > 0)
            {
                IncrementHistory tempIncrementHistory = new IncrementHistory();
                tempIncrementHistory.Increment_his_ID = ds.Tables[0].Rows[0]["INCREMENT_HIS_ID"].ToString();
                tempIncrementHistory.Staff_ID = ds.Tables[0].Rows[0]["STAFF_ID"].ToString();
                tempIncrementHistory.Active_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["ACTIVE_DATE"]);
                tempIncrementHistory.comments = ds.Tables[0].Rows[0]["COMMENTS"].ToString();
                tempIncrementHistory.Increment_amount = ds.Tables[0].Rows[0]["INCREMENT_AMOUNT"].ToString();
                tempIncrementHistory.Increment_ctrl = ds.Tables[0].Rows[0]["INCREMENT_CTRL"].ToString();
                tempIncrementHistory.Next_increment_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["NEXT_INCREMENT_DATE"]);


                if (ds.Tables[0].Rows[0]["CREATED_DATE"].ToString() != "")
                    tempIncrementHistory.Creted_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["CRETED_DATE"]);
                if (ds.Tables[0].Rows[0]["CREATED_BY"].ToString() != "")
                    tempIncrementHistory.Created_by = ds.Tables[0].Rows[0]["CREATED_BY"].ToString();
                
                listIncrementHistory = tempIncrementHistory;
            }
            return listIncrementHistory;
        }

        
    }


    


    

