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
    public class BasicSalaryAction
    {
        public bool insert(BasicSalary basicSalary)
        {
            int rowsAffected = 0;
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = "insert into (DESIG_ID,Basic_salary,Active_date,Creted_date,Created_by) values(?,?,?,?,?)";
            objDatabaseHelper.AddParameter("@DESIG_ID", basicSalary.DESIG_ID);
            objDatabaseHelper.AddParameter("@Basic_salary", basicSalary.Basic_salary);
            objDatabaseHelper.AddParameter("@Active_date", basicSalary.Active_date);
            objDatabaseHelper.AddParameter("@createdDate", basicSalary.Creted_date);
            objDatabaseHelper.AddParameter("@createdBy", basicSalary.Created_by);
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
                return true;
            }
            catch
            { }

            return false;
        }

//        DESIG_ID,Basic_salary,Active_date,Creted_date,Created_by
        public int update(BasicSalary basicSalary)
        {
            int rowsAffected = 0;
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = " update ga_BasicSalary set Basic_salary=?," +
                "Active_date=?," +
                "Creted_date=?, " +
                "Created_by=? " +
                "where DESIG_ID=?";

            objDatabaseHelper.AddParameter("@Basic_salary", basicSalary.Basic_salary);
            objDatabaseHelper.AddParameter("@Active_date", basicSalary.Active_date);
            objDatabaseHelper.AddParameter("@Creted_date", basicSalary.Creted_date);
            objDatabaseHelper.AddParameter("@Created_by", basicSalary.Created_by);
            objDatabaseHelper.AddParameter("@DESIG_ID", basicSalary.DESIG_ID);
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
            }
            catch
            { }
            return rowsAffected;
        }
        public int delete(BasicSalary basicSalary)
        {
            int rowsAffected = 0;
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = " delete from  Basic_Salary where DESIG_ID=?";

            objDatabaseHelper.AddParameter("@DESIG_ID", basicSalary.DESIG_ID);
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
            }
            catch
            { }
            return rowsAffected;
        }
        public List<BasicSalary> getAll()
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from view_BasicSalary";
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            List<BasicSalary> ListBasicSalary = new List<BasicSalary>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BasicSalary tempBasicSalaryList = new BasicSalary();
                tempBasicSalaryList.DESIG_ID = dr["DESIG_ID"].ToString();
                tempBasicSalaryList.Creted_date = Convert.ToDateTime(dr["Creted_date"]);
                tempBasicSalaryList.Created_by = dr["Created_by"].ToString();
                tempBasicSalaryList.Basic_salary = dr["Basic_salary"].ToString();
                tempBasicSalaryList.Active_date = Convert.ToDateTime(dr["Active_date"]);

                
                ListBasicSalary.Add(tempBasicSalaryList);
            }
            return ListBasicSalary;
        }
        public BasicSalary get(string BasicSalaryId)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from view_BasicSalary where cmp_id = ?";
            objhelper.AddParameter("@cmp_id", BasicSalaryId);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            BasicSalary ListBasicSalary = new BasicSalary();

            if( ds.Tables[0].Rows.Count>0)
            {
                BasicSalary tempBasicSalaryList = new BasicSalary();
                tempBasicSalaryList.DESIG_ID = ds.Tables[0].Rows[0]["DESIG_ID"].ToString();
                tempBasicSalaryList.Creted_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Creted_date"]);
                tempBasicSalaryList.Created_by = ds.Tables[0].Rows[0]["Created_by"].ToString();
                tempBasicSalaryList.Basic_salary = ds.Tables[0].Rows[0]["Basic_salary"].ToString();
                tempBasicSalaryList.Active_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Active_date"]);
            }
            return ListBasicSalary;
        }
    }

