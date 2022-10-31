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

    public class DepartmentAction
    {
       // DESIG_ID, DESIG_NAME, DEP_TYPE, DESIG_CTRL, CREATED_DATE, CREATED_BY
        public int insert(Department department)
        {
            int rowsAffected = 0;
            department.Id = new CommonFun().get_pk_no("hd_ID");
             
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = @"insert into hr_department(hd_ID , hd_NAME, hd_description, CRETED_DATE, CREATED_BY)" +
            " values(?,?,?,?,?)";
            objDatabaseHelper.AddParameter("@ID", department.Id);
            objDatabaseHelper.AddParameter("@NAME", department.Name);
            objDatabaseHelper.AddParameter("@Description", department.Description);
            objDatabaseHelper.AddParameter("@CRETED_DATE", department.Creted_date);
            objDatabaseHelper.AddParameter("@CREATED_BY", department.Created_by);
           
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
                new CommonFun().update_code("hd_ID", department.Id);
                
            }
            catch
            { }

            return rowsAffected;
        }
       
        public int delete(Department department)
        {
            int rowsAffected = 0;
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = " delete from  hr_department where hd_ID =?";

            objDatabaseHelper.AddParameter("@hd_ID ", department.Id);
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
            }
            catch
            { }
            return rowsAffected;
        }
        public List<Department> getAll()
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select HD_ID, HD_NAME, HD_DESCRIPTION, CRETED_DATE, CREATED_BY from hr_department";
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            List<Department> ListDepartment = new List<Department>();
            
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Department tempDepartment = new Department();
                            tempDepartment.Id = dr["HD_ID"].ToString();
                            tempDepartment.Name = dr["HD_NAME"].ToString();
                            tempDepartment.Description = dr["HD_DESCRIPTION"].ToString();
                            tempDepartment.Creted_date = Convert.ToDateTime(dr["CRETED_DATE"]);
                            tempDepartment.Created_by = dr["CREATED_BY"].ToString();


                            ListDepartment.Add(tempDepartment);
                        }
                        return ListDepartment;
        }

        public List<Department> getAllTeacherDepartment()
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select COLLEGECODE, COLLEGENAME from COLLEGE";
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            List<Department> ListDepartment = new List<Department>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Department tempDepartment = new Department();
                tempDepartment.Id = dr["COLLEGECODE"].ToString();
                tempDepartment.Name = dr["COLLEGENAME"].ToString();
                


                ListDepartment.Add(tempDepartment);
            }
            return ListDepartment;
        }

        public Department get(string leaveTypeId)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select HD_ID, HD_NAME, HD_DESCRIPTION, CRETED_DATE, CREATED_BY  from hr_department where hd_ID  = ?";
            objhelper.AddParameter("@hd_ID ", leaveTypeId);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            Department ListDepartment = new Department();

            if (ds.Tables[0].Rows.Count > 0)
            {
                Department tempDepartment = new Department();
                tempDepartment.Id = ds.Tables[0].Rows[0]["HD_ID"].ToString();
                tempDepartment.Name = ds.Tables[0].Rows[0]["HD_NAME"].ToString();
                tempDepartment.Description = ds.Tables[0].Rows[0]["HD_DESCRIPTION"].ToString();
                tempDepartment.Creted_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["CRETED_DATE"]);
                tempDepartment.Created_by = ds.Tables[0].Rows[0]["CREATED_BY"].ToString();
                ListDepartment = tempDepartment;
            }
            return ListDepartment;
        }
    }


    


