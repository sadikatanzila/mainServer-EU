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

    public class LeaveTypeAction
    {
       // DESIG_ID, DESIG_NAME, DEP_TYPE, DESIG_CTRL, CREATED_DATE, CREATED_BY
        public int insert(LeaveType leaveType)
        {
            int rowsAffected = 0;
             leaveType.Type_Id= new CommonFun().get_pk_no("leave_type_id");
             
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = @"insert into LEAVE_TYPE(TYPE_ID, TYPE_NAME, TOTAL_LEAV, CRETED_DATE, CREATED_BY)" +
            " values(?,?,?,?,?)";
            objDatabaseHelper.AddParameter("@TYPE_ID", leaveType.Type_Id);
            objDatabaseHelper.AddParameter("@TYPE_NAME", leaveType.Type_name);
            objDatabaseHelper.AddParameter("@TOTAL_LEAV", leaveType.Total_leav);
            objDatabaseHelper.AddParameter("@CRETED_DATE", leaveType.Creted_date);
            objDatabaseHelper.AddParameter("@CREATED_BY", leaveType.Created_by);
           
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
                new CommonFun().update_code("leave_type_id", leaveType.Type_Id);
                
            }
            catch
            { }

            return rowsAffected;
        }
       
        public int delete(LeaveType leaveType)
        {
            int rowsAffected = 0;
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = " delete from  LEAVE_TYPE where TYPE_ID=?";

            objDatabaseHelper.AddParameter("@TYPE_ID", leaveType.Type_Id);
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
            }
            catch
            { }
            return rowsAffected;
        }
        public List<LeaveType> getAll()
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from LEAVE_TYPE";
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            List<LeaveType> ListleaveType = new List<LeaveType>();
            
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            LeaveType templeaveType = new LeaveType();
                            templeaveType.Type_Id = dr["TYPE_ID"].ToString();
                            templeaveType.Type_name = dr["TYPE_NAME"].ToString();
                            templeaveType.Total_leav = dr["TOTAL_LEAV"].ToString();
                            templeaveType.Creted_date = Convert.ToDateTime(dr["CRETED_DATE"]);
                            templeaveType.Created_by = dr["CREATED_BY"].ToString();
                           

                            ListleaveType.Add(templeaveType);
                        }
            return ListleaveType;
        }
        public LeaveType get(string leaveTypeId)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from LEAVE_TYPE where TYPE_ID = ?";
            objhelper.AddParameter("@TYPE_ID", leaveTypeId);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            LeaveType ListleaveType = new LeaveType();

            if (ds.Tables[0].Rows.Count > 0)
            {
                LeaveType templeaveType = new LeaveType();
                templeaveType.Type_Id = ds.Tables[0].Rows[0]["TYPE_ID"].ToString();
                templeaveType.Type_name = ds.Tables[0].Rows[0]["TYPE_NAME"].ToString();
                templeaveType.Total_leav = ds.Tables[0].Rows[0]["TOTAL_LEAV"].ToString();
                templeaveType.Creted_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["CRETED_DATE"]);
                templeaveType.Created_by = ds.Tables[0].Rows[0]["CREATED_BY"].ToString();
            }
            return ListleaveType;
        }
    }


    

