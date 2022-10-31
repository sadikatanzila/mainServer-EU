using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.OleDb;
using System.Data.ProviderBase;
using System.Data.OracleClient;
using System.Data;
using System.Data.Odbc;


public class Dts : System.Web.Services.WebService {

    private OleDbConnection con;
    private OleDbDataAdapter da;
    
    public Dts () 
    {
         con = new OleDbConnection(get_con_string());
    }

    private string get_con_string()
    {
       // return "Provider=MSDAORA.1;Data Source=GLOBAL;Persist Security Info=True;User ID=adminuser;password=user ";

        //for local pc to Testing Server  192.168.0.7  server connection
        /*   return "Provider= msdaORA; Data Source=(DESCRIPTION=(ADDRESS_LIST="
                   + "(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.7)(PORT=1521)))"
                   + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=GLOBAL)));"
                    + "User Id=adminuser;Password=adminuser;";*/


        //for 192.168.0.15 server connection
        /*       return  "Provider= msdaORA; Data Source=(DESCRIPTION=(ADDRESS_LIST="
               + "(ADDRESS=(PROTOCOL=TCP)(HOST=euc)(PORT=1521)))"
               + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=GLOBAL)));"
        + "User Id=adminuser;Password=user;";
        */
       //for local pc to  192.168.0.3  server connection
            return "Provider= msdaORA; Data Source=(DESCRIPTION=(ADDRESS_LIST="
                                + "(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.3)(PORT=1521)))"
                                + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=GLOBAL)));"
                              + "User Id=adminuser;Password=user;"; 


        //for server pc to 192.168.0.3 server connection
         //   return "Provider= ORAOLEDB.ORACLE; Data Source=(DESCRIPTION=(ADDRESS_LIST="
         //              + "(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.3)(PORT=1521)))"
         //              + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=GLOBAL)));"
           //            + "User Id=adminuser;Password=user;";


    }

  
        public string execute_query(string query)
        {
            string st = "1";
            OleDbCommand cmm;

            try
            { 
                cmm = new OleDbCommand(query, con);
                con.Open();
                cmm.ExecuteNonQuery();
            }            
            catch (Exception er)
            {
                st = er.ToString();
            }
            finally
            {
                con.Close();
            }
            return st;
        
        }


        public DataTable Table_CollegeGetAll(string storeProcedure, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;

                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }

        public DataTable Table_GetAll_Departmental(string storeProcedure, string Department, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                //cmdf.Parameters.Add("@EmpValue", OleDbType.Char).Value = EmpValue;
                cmdf.Parameters.Add("Parameter1", OracleType.NVarChar).Value = Department;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }
        public DataTable Table_GetAll_EXMWISE(string storeProcedure, string YearSem, string ExamType, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                cmdf.Parameters.Add("YearSem", OracleType.NVarChar).Value = YearSem;
                cmdf.Parameters.Add("ExamType", OracleType.NVarChar).Value = ExamType;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }

        public DataTable Table_GetAll_EXMWISE(string storeProcedure,string year, string YearSem, string ExamType, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                cmdf.Parameters.Add("Year", OracleType.NVarChar).Value = YearSem;
                cmdf.Parameters.Add("YearSem", OracleType.NVarChar).Value = YearSem;
                cmdf.Parameters.Add("ExamType", OracleType.NVarChar).Value = ExamType;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }

        public DataTable Table_get_STAFF_INFO(string storeProcedure, string YearSem, string ExamType, string YearSem1, string ExamType1, string YearSem2, string ExamType2, string YearSem3, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                cmdf.Parameters.Add("YearSem", OracleType.NVarChar).Value = YearSem;
                cmdf.Parameters.Add("ExamType", OracleType.NVarChar).Value = ExamType;
                cmdf.Parameters.Add("YearSem1", OracleType.NVarChar).Value = YearSem1;
                cmdf.Parameters.Add("ExamType1", OracleType.NVarChar).Value = ExamType1;
                cmdf.Parameters.Add("YearSem2", OracleType.NVarChar).Value = YearSem2;
                cmdf.Parameters.Add("ExamType2", OracleType.NVarChar).Value = ExamType2;
                cmdf.Parameters.Add("YearSem3", OracleType.NVarChar).Value = YearSem3;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }



        public DataTable Table_GetAll_DEPWISE(string storeProcedure,string YearSem,string CourseKey,string section,string ExamType, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                cmdf.Parameters.Add("YearSem", OracleType.NVarChar).Value = YearSem;
                cmdf.Parameters.Add("CourseKey", OracleType.NVarChar).Value = CourseKey;
                cmdf.Parameters.Add("section", OracleType.Int32).Value = section;
                cmdf.Parameters.Add("ExamType", OracleType.Int32).Value = ExamType;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }


        public DataTable Table_GetAll_DEPWISE_NEW(string storeProcedure, int YearSem, int CourseKey, int section, int ExamType, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                cmdf.Parameters.Add("YearSem", OracleType.Int32).Value = YearSem;
                cmdf.Parameters.Add("CourseKey", OracleType.Int32).Value = CourseKey;

                cmdf.Parameters.Add("section", OracleType.Int32).Value = section;
                cmdf.Parameters.Add("ExamType", OracleType.Int32).Value = ExamType;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }
  
    
    public DataTable Table_GetAll_PROGWISE(string storeProcedure, string YearSem, string ExamType,string coursekey,string section, string College, string Program, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                cmdf.Parameters.Add("YearSem", OracleType.NVarChar).Value = YearSem;
                cmdf.Parameters.Add("ExamType", OracleType.NVarChar).Value = ExamType;
                cmdf.Parameters.Add("coursekey", OracleType.Int32).Value = coursekey;
                cmdf.Parameters.Add("section", OracleType.NVarChar).Value = section;
                cmdf.Parameters.Add("College", OracleType.Int32).Value = College;
                cmdf.Parameters.Add("Program", OracleType.NVarChar).Value = Program;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }


        public DataTable Table_GetAll_PROGWISENew(string storeProcedure, string YearSem, string ExamType,string coursekey, string College, string Program, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                cmdf.Parameters.Add("YearSem", OracleType.NVarChar).Value = YearSem;
                cmdf.Parameters.Add("ExamType", OracleType.NVarChar).Value = ExamType;
                cmdf.Parameters.Add("coursekey", OracleType.NVarChar).Value = coursekey;
                cmdf.Parameters.Add("ToSemester", OracleType.Int32).Value = College;
                cmdf.Parameters.Add("Program", OracleType.NVarChar).Value = Program;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }

        public DataTable Table_GetAll(string storeProcedure, Int32 Semester, Int32 Year, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                //cmdf.Parameters.Add("@EmpValue", OleDbType.Char).Value = EmpValue;
                cmdf.Parameters.Add("Parameter1", OracleType.Int32).Value = Semester;
                cmdf.Parameters.Add("Parameter2", OracleType.Int32).Value = Year;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }

        public DataTable Table_ProbableGetAll(string storeProcedure, Int32 FrmYear, Int32 FrmSemester, Int32 ToYear, Int32 ToSemester, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                //cmdf.Parameters.Add("@EmpValue", OleDbType.Char).Value = EmpValue;
                cmdf.Parameters.Add("FrmYear", OracleType.Int32).Value = FrmYear;
                cmdf.Parameters.Add("FrmSemester", OracleType.Int32).Value = FrmSemester;
                cmdf.Parameters.Add("ToYear", OracleType.Int32).Value = ToYear;
                cmdf.Parameters.Add("ToSemester", OracleType.Int32).Value = ToSemester;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }

        public DataTable Table_ProbableGetAllDeptwise(string storeProcedure, Int32 FrmYear, Int32 FrmSemester, Int32 ToYear, Int32 ToSemester,string Program, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                //cmdf.Parameters.Add("@EmpValue", OleDbType.Char).Value = EmpValue;
                cmdf.Parameters.Add("FrmYear", OracleType.Int32).Value = FrmYear;
                cmdf.Parameters.Add("FrmSemester", OracleType.Int32).Value = FrmSemester;
                cmdf.Parameters.Add("ToYear", OracleType.Int32).Value = ToYear;
                cmdf.Parameters.Add("ToSemester", OracleType.Int32).Value = ToSemester;
                cmdf.Parameters.Add("Program", OracleType.NVarChar).Value = Program;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }


        public DataTable Table_RegnonRegGetAll(string storeProcedure, Int32 RegSemester, Int32 RegYear, Int32 nonRegYear, Int32 nonRegSemester, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                //cmdf.Parameters.Add("@EmpValue", OleDbType.Char).Value = EmpValue;
                cmdf.Parameters.Add("RegSemester", OracleType.Int32).Value = RegSemester;
                cmdf.Parameters.Add("RegYear", OracleType.Int32).Value = RegYear;
                cmdf.Parameters.Add("RegSemester", OracleType.Int32).Value = RegSemester;
                cmdf.Parameters.Add("RegYear", OracleType.Int32).Value = RegYear;
                cmdf.Parameters.Add("nonRegSemester", OracleType.Int32).Value = nonRegSemester;
                cmdf.Parameters.Add("nonRegYear", OracleType.Int32).Value = nonRegYear;
                cmdf.Parameters.Add("nonRegSemester", OracleType.Int32).Value = nonRegSemester;
                cmdf.Parameters.Add("nonRegYear", OracleType.Int32).Value = nonRegYear;
                cmdf.Parameters.Add("RegSemester", OracleType.Int32).Value = RegSemester;
                cmdf.Parameters.Add("RegYear", OracleType.Int32).Value = RegYear;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }

        public DataTable get_viewData(string query, string tableName)
        {
            DataSet ds = new DataSet();
            da = new OleDbDataAdapter(query, con);
            try
            {
                con.Open();
                da.Fill(ds, "" + tableName);
            }                
            catch (Exception e) 
            {
                ds.Tables.Add("" + tableName);
            }
            finally { con.Close(); }
            return ds.Tables["" + tableName];
        }

        public string get_pk_no(String obj )
        {
            string ids = "";

            DataSet ds = new DataSet();
            da = new OleDbDataAdapter(" Select * from WEB_CODES where OBJECT='" + obj + "' ", con);
            try
            {
                con.Open();
                da.Fill(ds, "WEB_CODES");
            }
            catch (Exception e)
            { 
            }
            finally { con.Close(); }

            if(ds.Tables["WEB_CODES"].Rows.Count>0)
                ids = ds.Tables["WEB_CODES"].Rows[0]["SERIAL"].ToString();
            return ids; 
        }

        public DataTable Table_SP_GET_SEQ_VALUE(string storeProcedure, Int32 Semester, Int32 Year, string tableName)
        {
            // DataSet ds = new DataSet();
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                //cmdf.Parameters.Add("@EmpValue", OleDbType.Char).Value = EmpValue;
                cmdf.Parameters.Add("Parameter1", OracleType.Int32).Value = Semester;
                cmdf.Parameters.Add("Parameter2", OracleType.Int32).Value = Year;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;
        }

        public String call_procedure(string storeProcedure, String objectCode)
        {
            OracleDataAdapter da = new OracleDataAdapter();
            DataTable table = new DataTable();
            String serialNumber = "";
            try
            {
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.StoredProcedure;
                cmdf.Parameters.Add("P_SOURCE_KEY", OracleType.VarChar).Value = objectCode;
                cmdf.Parameters.Add("R_RETURN_VAL", OracleType.Number).Direction = ParameterDirection.Output;
                cmdf.ExecuteNonQuery();
                serialNumber = cmdf.Parameters["R_RETURN_VAL"].Value.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return serialNumber;
        }
        public DataTable call_procedure1(string storeProcedure, String code)
        {
            DataTable table = new DataTable();
            try
            {
                // DbConnection conn_st=ge;
                con.Open();
                OleDbCommand cmdf = new OleDbCommand(storeProcedure, con);
                cmdf.CommandType = CommandType.Text;
                //cmdf.Parameters.Add("@EmpValue", OleDbType.Char).Value = EmpValue;
                cmdf.Parameters.Add("Parameter1", OracleType.Int32).Value = code;
                OleDbDataAdapter daf = new OleDbDataAdapter(cmdf);
                DataSet dsf = new DataSet();
                // daf.Fill(ds);
                daf.Fill(table);
                //  con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //  OleDbDataAdapter roTable = new OleDbDataAdapter(cmdf);
            return table;

            // return web_codeserial;
        }


        //public string upload_assignment_teacher(DataSet ds, ref string ids)
        //{
        //    string st ="1";
        //    string sql = "";
        //    OleDbCommand cmm;
           
        //    try
        //    {
        //        foreach (DataRow dr in ds.Tables["assignment"].Rows)
        //        {
        //            ids = "Ass"+get_pk_no("course_mat");

        //            sql = @" insert into WEB_COURSE_MATERIALS_TEACHER (COURSE_MATERIALS_ID, COURSE_TEACHER_ID, TITLE, FILE_NAME, TYPE,UPLOAD_DATE, DUE_DATE, DESCRIPTION, CTRL)";
        //            sql += " values ('" + ids + "', '" + dr["COURSE_TEACHER_ID"] + "', '" + dr["TITLE"] + "', '" + dr["FILE_NAME"] + "', '" + dr["TYPE"] + "', "; // '" + dr["FILE_DATA"] + "',
        //            sql += " '" + dr["UPLOAD_DATE"] + "', '" + dr["DUE_DATE"] + "', '" + dr["DESCRIPTION"] + "', '" + dr["CTRL"] + "' ) ";

        //            cmm = new OleDbCommand(sql, con);
        //            con.Open();
        //            cmm.ExecuteNonQuery();

        //            break;
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        st = er.ToString();
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return st;
        //}


        //public string upload_lecture(DataSet ds, ref string ids)
        //{
        //    string st = "1";
        //    string sql = "";
        //    OleDbCommand cmm;

        //    try
        //    {
        //        foreach (DataRow dr in ds.Tables["assignment"].Rows)
        //        {
        //            ids = "Lec"+get_pk_no("course_mat");

        //            sql = @" insert into WEB_COURSE_MATERIALS_TEACHER (COURSE_MATERIALS_ID, COURSE_TEACHER_ID, TITLE, FILE_NAME, TYPE,UPLOAD_DATE, DESCRIPTION, CTRL)";
        //            sql += " values ('" + ids + "', '" + dr["COURSE_TEACHER_ID"] + "', '" + dr["TITLE"] + "', '" + dr["FILE_NAME"] + "', '" + dr["TYPE"] + "', "; 
        //            sql += " '" + dr["UPLOAD_DATE"] + "','" + dr["DESCRIPTION"] + "', '" + dr["CTRL"] + "' ) ";
                     
        //            cmm = new OleDbCommand(sql, con);
        //            con.Open();
        //            cmm.ExecuteNonQuery();

        //            break;
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        st = er.ToString();
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return st;
        //}

        //public string upload_outLine(DataSet ds, ref string ids)
        //{
        //    string st = "1";
        //    string sql = "";
        //    OleDbCommand cmm;

        //    try
        //    {
        //        foreach (DataRow dr in ds.Tables["assignment"].Rows)
        //        {
        //            ids = "Out" + get_pk_no("course_mat");

        //            sql = @" insert into WEB_COURSE_MATERIALS_TEACHER (COURSE_MATERIALS_ID, COURSE_TEACHER_ID, TITLE, FILE_NAME, TYPE,UPLOAD_DATE, DESCRIPTION, CTRL)";
        //            sql += " values ('" + ids + "', '" + dr["COURSE_TEACHER_ID"] + "', '" + dr["TITLE"] + "', '" + dr["FILE_NAME"] + "', '" + dr["TYPE"] + "', ";
        //            sql += " '" + dr["UPLOAD_DATE"] + "','" + dr["DESCRIPTION"] + "', '" + dr["CTRL"] + "' ) ";

        //            cmm = new OleDbCommand(sql, con);
        //            con.Open();
        //            cmm.ExecuteNonQuery();

        //            break;
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        st = er.ToString();
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return st;
        //}



        public string insert_general(DataSet ds, string tableName)
        {
            string st = "1";
            string sql = "";
            OleDbCommand cmm;
            int i = 0;
           
                
                foreach (DataRow dr in ds.Tables[""+tableName].Rows)
                {
                    i = 0;
                    sql = @" INSERT INTO " + tableName+" (";

                    foreach (DataColumn clName in ds.Tables[tableName].Columns)
                    {
                        if (i == 0)
                        {
                            i++;
                            sql += " "+clName.ColumnName;
                        }
                        else
                        {
                            sql += ", " + clName.ColumnName;
                        }
                    }
                    sql += ") values ( ";
                    i = 0;
                    foreach (DataColumn clName in ds.Tables[tableName].Columns)
                    {
                        if (i == 0)
                        {
                            i++;
                            sql += " '" + dr[clName.ColumnName].ToString()+"' ";
                        }
                        else
                        {
                            sql += ", '" + dr[clName.ColumnName].ToString() + "' ";
                        }
                    }
                    sql += ")";
                     
                    try
                        {
                            cmm = new OleDbCommand(sql, con);
                            con.Open();
                            cmm.ExecuteNonQuery();                     
                        }
                        catch (Exception er)
                        {
                            st += er.ToString();
                        }
                        finally
                        {
                            con.Close();
                        }
            }
           
            return st;
        }



    
}

