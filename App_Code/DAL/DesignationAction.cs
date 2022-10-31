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

    public class DesignationAction
    {
       // DESIG_ID, DESIG_NAME, DEP_TYPE, DESIG_CTRL, CREATED_DATE, CREATED_BY
        public int insert(Designation designation)
        {
            int rowsAffected = 0;
             designation.DESIG_ID= new CommonFun().get_pk_no("designation_id");
             
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = @"insert into Designation(DESIG_ID,DESIG_NAME,DEP_TYPE,DESIG_CTRL,CREATED_DATE,Created_by)" +
            " values(?,?,?,?,?,?)";
            objDatabaseHelper.AddParameter("@DESIG_ID", designation.DESIG_ID);
            objDatabaseHelper.AddParameter("@DESIG_NAME", designation.DESIG_NAME);
            objDatabaseHelper.AddParameter("@DEP_TYPE", designation.DEP_TYPE);
            objDatabaseHelper.AddParameter("@DESIG_CTRL", designation.DESIG_CTRL);
            objDatabaseHelper.AddParameter("@createdDate", designation.Creted_date);
            objDatabaseHelper.AddParameter("@Created_by", designation.Created_by);
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
                new CommonFun().update_code("designation_id", designation.DESIG_ID);
                
            }
            catch
            { }

            return rowsAffected;
        }
        public int update(Designation designation)
        {
            int rowsAffected = 0;
             DatabaseHelper objDatabaseHelper = new DatabaseHelper();
             String query = " update Designation set DESIG_NAME=?," +
                 "DESIG_DEPARTMENT=?," +
                 "DESIG_CTRL=?, " +
                 "Creted_date=?," +
                 "Created_by=?" +                 
                 "where DESIG_ID=?";
             objDatabaseHelper.AddParameter("@DESIG_NAME", designation.DESIG_NAME);
             objDatabaseHelper.AddParameter("@DESIG_DEPARTMENT", designation.DEP_TYPE);
             objDatabaseHelper.AddParameter("@DESIG_CTRL", designation.DESIG_CTRL);
             objDatabaseHelper.AddParameter("@Creted_date", designation.Creted_date);
             objDatabaseHelper.AddParameter("@Created_by", designation.Created_by);
             objDatabaseHelper.AddParameter("@DESIG_ID", designation.DESIG_ID);
             try
             {
                 rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
             }
             catch
             { }
            return rowsAffected;
        }
        public int delete(Designation designation)
        {
            int rowsAffected = 0;
            DatabaseHelper objDatabaseHelper = new DatabaseHelper();
            String query = " delete from  Designation where DESIG_ID=?";

            objDatabaseHelper.AddParameter("@DESIG_ID", designation.DESIG_ID);
            try
            {
                rowsAffected = objDatabaseHelper.ExecuteNonQuery(query);
            }
            catch
            { }
            return rowsAffected;
        }
        public List<Designation> getAll()
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from Designation";
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            List<Designation> ListBasicSalary = new List<Designation>();
            
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Designation tempDesignation = new Designation();
                            tempDesignation.DESIG_ID = dr["DESIG_ID"].ToString();
                            tempDesignation.DESIG_NAME = dr["DESIG_NAME"].ToString();
                            tempDesignation.DEP_TYPE = dr["DEP_TYPE"].ToString();
                            tempDesignation.DESIG_CTRL = int.Parse(dr["DESIG_CTRL"].ToString());
                            tempDesignation.Creted_date = Convert.ToDateTime(dr["Created_date"]);
                            tempDesignation.Created_by = dr["Created_by"].ToString();

                            ListBasicSalary.Add(tempDesignation);
                        }
            return ListBasicSalary;
        }

        public List<Designation> getAllTeacherDesignation()
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from Designation where DEP_TYPE=?";
            objhelper.AddParameter("@DEP_TYPE", "Academic");
            DataSet ds = new DataSet();

            ds.Merge(objhelper.ExecuteDataSet(query));
            List<Designation> ListBasicSalary = new List<Designation>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Designation tempDesignation = new Designation();
                tempDesignation.DESIG_ID = dr["DESIG_ID"].ToString();
                tempDesignation.DESIG_NAME = dr["DESIG_NAME"].ToString();
                tempDesignation.DEP_TYPE = dr["DEP_TYPE"].ToString();
                tempDesignation.DESIG_CTRL = int.Parse(dr["DESIG_CTRL"].ToString());
                tempDesignation.Creted_date = Convert.ToDateTime(dr["Created_date"]);
                tempDesignation.Created_by = dr["Created_by"].ToString();

                ListBasicSalary.Add(tempDesignation);
            }
            return ListBasicSalary;
        }

        public List<Designation> getAllStaffDesignation()
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from Designation where DEP_TYPE=?";
            objhelper.AddParameter("@DEP_TYPE", "General");
            DataSet ds = new DataSet();

            ds.Merge(objhelper.ExecuteDataSet(query));
            List<Designation> ListBasicSalary = new List<Designation>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Designation tempDesignation = new Designation();
                tempDesignation.DESIG_ID = dr["DESIG_ID"].ToString();
                tempDesignation.DESIG_NAME = dr["DESIG_NAME"].ToString();
                tempDesignation.DEP_TYPE = dr["DEP_TYPE"].ToString();
                tempDesignation.DESIG_CTRL = int.Parse(dr["DESIG_CTRL"].ToString());
                tempDesignation.Creted_date = Convert.ToDateTime(dr["Created_date"]);
                tempDesignation.Created_by = dr["Created_by"].ToString();

                ListBasicSalary.Add(tempDesignation);
            }
            return ListBasicSalary;
        }
        public Designation get(string designationId)
        {
            DatabaseHelper objhelper = new DatabaseHelper();
            string query = "select * from Designation where DESIG_ID = ?";
            objhelper.AddParameter("@DESIG_ID", designationId);
            DataSet ds = new DataSet();
            ds.Merge(objhelper.ExecuteDataSet(query));
            Designation ListBasicSalary = new Designation();

            if (ds.Tables[0].Rows.Count > 0)
            {
                Designation tempDesignation = new Designation();
                tempDesignation.DESIG_ID = ds.Tables[0].Rows[0]["DESIG_ID"].ToString();
                tempDesignation.DESIG_NAME = ds.Tables[0].Rows[0]["DESIG_NAME"].ToString();
                tempDesignation.DEP_TYPE = ds.Tables[0].Rows[0]["DESIG_DEPARTMENT"].ToString();
                tempDesignation.DESIG_CTRL = int.Parse(ds.Tables[0].Rows[0]["DESIG_CTRL"].ToString());
                tempDesignation.Creted_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Creted_date"]);
                tempDesignation.Created_by = ds.Tables[0].Rows[0]["Created_by"].ToString();
                ListBasicSalary = tempDesignation;
            }
            return ListBasicSalary;
        }
    }


    