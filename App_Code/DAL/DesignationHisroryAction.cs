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

    public class DesignationHisroryAction
    {
       // DESIG_ID, DESIG_NAME, DEP_TYPE, DESIG_CTRL, CREATED_DATE, CREATED_BY

        public DataSet getPromotionableEmployee(DateTime startDate, DateTime endDate)
        {
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();

            String query = @"SELECT DESIGN_HIS_ID,WEB_TEACHER_STAFF. STAFF_ID,WEB_TEACHER_STAFF.staff_name, "+
    " DESIG_ID, ACTIVE_DATE, NEXT_DESIG_DATE, COMMENTS, DESIG_CTRL,  "+
     " DESIGNATION_HISTORY.CRETED_DATE, DESIGNATION_HISTORY.CREATED_BY "+
    " FROM DESIGNATION_HISTORY, WEB_TEACHER_STAFF "+
    " WHERE NEXT_DESIG_DATE  BETWEEN ? AND ? "+
    " AND WEB_TEACHER_STAFF.STAFF_ID=DESIGNATION_HISTORY.STAFF_ID";
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
        public int insert(DesignationHistory designationHistory)
        {
            int rowsAffected = 0;
            designationHistory.Design_his_ID = new CommonFun().get_pk_no("designation_history_id");

            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            int parameterCount = 0;
            String query = @"insert into DESIGNATION_HISTORY( ";
            if (designationHistory.Design_his_ID != null)//|| designationHistory.STAFF_ID.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@DESIGN_HIS_ID", designationHistory.Design_his_ID);
                parameterCount++;
                query += "DESIGN_HIS_ID,";
            }
            if (designationHistory.Staff_ID != null)//|| designationHistory.STAFF_NAME.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@STAFF_ID", designationHistory.Staff_ID);
                parameterCount++;
                query += " STAFF_ID,";
            }
            if (designationHistory.DESIG_ID != null)//|| designationHistory.P_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@DESIG_ID", designationHistory.DESIG_ID);
                parameterCount++;
                query += " DESIG_ID,";
            }
            if (designationHistory.Active_date != null)//|| designationHistory.PER_ADDRESS.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@ACTIVE_DATE", designationHistory.Active_date);
                parameterCount++;
                query += " ACTIVE_DATE,";
            }
            if (designationHistory.Next_desig_date != null)//|| designationHistory.PHONE_NUMBER.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@NEXT_DESIG_DATE", designationHistory.Next_desig_date);
                parameterCount++;
                query += " NEXT_DESIG_DATE,";
            }
            if (designationHistory.comments != null)//|| designationHistory.MOBILE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@COMMENTS", designationHistory.comments);
                parameterCount++;
                query += " COMMENTS,";
            }
            if (designationHistory.Desig_ctrl != null)//|| designationHistory.Gender.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@DESIG_CTRL", designationHistory.Desig_ctrl);
                parameterCount++;
                query += " DESIG_CTRL,";
            }

            if (designationHistory.Creted_date != null)//|| designationHistory.JOB_TYPE.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CRETED_DATE", designationHistory.Creted_date);
                parameterCount++;
                query += " CRETED_DATE,";
            }
            if (designationHistory.Created_by != null)//|| designationHistory.JOB_CATEGORY.Trim() != "")
            {
                objDatabaseHelper.AddParameter("@CREATED_BY", designationHistory.Created_by);
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
                new CommonFun().update_code("designation_history_id", designationHistory.Design_his_ID);

            }
            catch
            { }

            return rowsAffected;            
        }
        public int update(DesignationHistory designationHistory)
        {
            int rowsAffected = 0;
             DatabaseHelper objDatabaseHelper = new DatabaseHelper();
             String query = " update DESIGNATION_HISTORY set STAFF_ID=?," +
                " DESIG_ID=?, ACTIVE_DATE=?, NEXT_DESIG_DATE=?, COMMENTS=?, DESIG_CTRL=?, CRETED_DATE=?, CREATED_BY=? " +
                " where DESIGN_HIS_ID=?";
             
             objDatabaseHelper.AddParameter("@STAFF_ID", designationHistory.Staff_ID);
             objDatabaseHelper.AddParameter("@DESIG_ID", designationHistory.DESIG_ID);
             objDatabaseHelper.AddParameter("@ACTIVE_DATE", designationHistory.Active_date);
             objDatabaseHelper.AddParameter("@NEXT_DESIG_DATE", designationHistory.Next_desig_date);
             objDatabaseHelper.AddParameter("@COMMENTS", designationHistory.comments);
             objDatabaseHelper.AddParameter("@DESIG_CTRL", designationHistory.Desig_ctrl);
             objDatabaseHelper.AddParameter("@CRETED_DATE", designationHistory.Creted_date);
             objDatabaseHelper.AddParameter("@CREATED_BY", designationHistory.Created_by);
             objDatabaseHelper.AddParameter("@DESIGN_HIS_ID", designationHistory.Design_his_ID);
             try
             {
                 rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
             }
             catch
             { }
            return rowsAffected;
        }
        public int delete(string Design_his_ID)
        {
            int rowsAffected = 0;
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = " delete from  DESIGNATION_HISTORY where DESIGN_HIS_ID=?";

            objDatabaseHelper.AddParameter("@DESIGN_HIS_ID", Design_his_ID);
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
            }
            catch
            { }
            return rowsAffected;
        }
        public List<DesignationHistory> getAll(string staff_id)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from VIEW_HR_DESIGNATION_HISTORY where STAFF_ID=?";
            objhelper.AddParameter("@STAFF_ID", staff_id);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            List<DesignationHistory> listDesignationHistory = new List<DesignationHistory>();
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DesignationHistory tempDesignationHistory = new DesignationHistory();                
                tempDesignationHistory.Design_his_ID = dr["DESIGN_HIS_ID"].ToString();
                tempDesignationHistory.Staff_ID = dr["STAFF_ID"].ToString();
                tempDesignationHistory.DESIG_ID = dr["DESIG_ID"].ToString();
                tempDesignationHistory.DESIG_name = dr["DESIG_NAME"].ToString();
                tempDesignationHistory.Active_date = Convert.ToDateTime(dr["ACTIVE_DATE"]);
                tempDesignationHistory.Next_desig_date = Convert.ToDateTime(dr["NEXT_DESIG_DATE"]);
                tempDesignationHistory.comments = dr["COMMENTS"].ToString();
                tempDesignationHistory.Desig_ctrl = dr["DESIG_CTRL"].ToString();
                tempDesignationHistory.Creted_date = Convert.ToDateTime(dr["CRETED_DATE"]);
                tempDesignationHistory.Created_by = dr["CREATED_BY"].ToString();

                listDesignationHistory.Add(tempDesignationHistory);
            }
            return listDesignationHistory;
        }
        public DesignationHistory get(string designationHistoryId)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from VIEW_HR_DESIGNATION_HISTORY where DESIG_ID = ?";
            objhelper.AddParameter("@DESIG_ID", designationHistoryId);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            DesignationHistory listDesignationHistory = new DesignationHistory();

            if (ds.Tables[0].Rows.Count > 0)
            {
                DesignationHistory tempDesignationHistory = new DesignationHistory();
                tempDesignationHistory.Design_his_ID = ds.Tables[0].Rows[0]["DESIGN_HIS_ID"].ToString();
                tempDesignationHistory.Staff_ID = ds.Tables[0].Rows[0]["STAFF_ID"].ToString();
                tempDesignationHistory.DESIG_ID = ds.Tables[0].Rows[0]["DESIG_ID"].ToString();
                tempDesignationHistory.DESIG_name = ds.Tables[0].Rows[0]["DESIG_NAME"].ToString();
                tempDesignationHistory.Active_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["ACTIVE_DATE"]);
                tempDesignationHistory.Next_desig_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["NEXT_DESIG_DATE"]);
                tempDesignationHistory.comments = ds.Tables[0].Rows[0]["COMMENTS"].ToString();
                tempDesignationHistory.Desig_ctrl = ds.Tables[0].Rows[0]["DESIG_CTRL"].ToString();
                tempDesignationHistory.Creted_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["CRETED_DATE"]);
                tempDesignationHistory.Created_by = ds.Tables[0].Rows[0]["CREATED_BY"].ToString();
                listDesignationHistory = tempDesignationHistory;
            }
            return listDesignationHistory;
        }
    }


    

