using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.Common;


public class college : System.Web.Services.WebService
{
    Dts obj_db;

    public college()
    {
        obj_db = new Dts();
    }

    //get all department from college table
    public DataTable get_allCollege(string tableName)
    {
        OracleCommand query = new OracleCommand();
        DataTable ds = new DataTable();

        query.CommandText =  @" Select * from COLLEGE order by COLLEGENAME asc  ";

        ds = obj_db.Table_CollegeGetAll(query.CommandText,  tableName);
        return ds;
    }
}

