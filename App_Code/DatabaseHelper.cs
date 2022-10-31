using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;

    public class DatabaseHelper:IDisposable
    {
        private string strConnectionString;
        private OleDbConnection objConnection;
        private OleDbCommand objCommand;
       
        private bool boolHandleErrors;
        private string strLastError;
        private bool boolLogError;
        private string strLogFile;

        public DatabaseHelper()
        {            
            
            objCommand = new OleDbCommand();
            strConnectionString = "Provider= msdaORA; Data Source=(DESCRIPTION=(ADDRESS_LIST="
          + "(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.15)(PORT=1521)))"
          + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=GLOBAL)));"
          + "User Id=adminuser;Password=user;";

            objConnection = new OleDbConnection(strConnectionString);
            //objConnection.ConnectionString = strConnectionString;
            objCommand.Connection = objConnection;
        }

       
        public bool HandleErrors
        {
            get
            {
                return boolHandleErrors;
            }
            set
            {
                boolHandleErrors = value;
            }
        }

        public string LastError
        {
            get
            {
                return strLastError;
            }
        }

        public bool LogErrors
        {
            get
            {
                return boolLogError;
            }
            set
            {
                boolLogError=value;
            }
        }

        public string LogFile
        {
            get
            {
                return strLogFile;
            }
            set
            {
                strLogFile = value;
            }
        }

        public int AddParameter(string name,object value)
        {
            OleDbParameter p = objCommand.CreateParameter();
            p.ParameterName = name;
            p.Value=value;
            return (((DbParameterCollection)objCommand.Parameters).Add(p));
        }

        public int AddParameter(DbParameter parameter)
        {
            return objCommand.Parameters.Add(parameter);
        }

        public DbCommand Command
        {
            get
            {
                return objCommand;
            }
        }

        public void BeginTransaction()
        {
            if (objConnection.State == System.Data.ConnectionState.Closed)
            {
                objConnection.Open();
            }
            objCommand.Transaction = objConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            objCommand.Transaction.Commit();
            objConnection.Close();
        }

        public void RollbackTransaction()
        {
            objCommand.Transaction.Rollback();
            objConnection.Close();
        }

        public int ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, CommandType.Text, ConnectionState.CloseOnExit);
        }

        public int ExecuteNonQuery(string query,CommandType commandtype)
        {
            return ExecuteNonQuery(query, commandtype, ConnectionState.CloseOnExit);
        }

        public int ExecuteNonQuery(string query,ConnectionState connectionstate)
        {
            return ExecuteNonQuery(query,CommandType.Text,connectionstate);
        }

        public int ExecuteNonQuery(string query,CommandType commandtype, ConnectionState connectionstate)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            int i=-1;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = objCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                objCommand.Parameters.Clear();
                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    objConnection.Close();
                }
            }

            return i;
        }

        public object ExecuteScalar(string query)
        {
            return ExecuteScalar(query, CommandType.Text, ConnectionState.CloseOnExit);
        }

        public object ExecuteScalar(string query,CommandType commandtype)
        {
            return ExecuteScalar(query, commandtype, ConnectionState.CloseOnExit);
        }

        public object ExecuteScalar(string query, ConnectionState connectionstate)
        {
            return ExecuteScalar(query, CommandType.Text, connectionstate);
        }

        public object ExecuteScalar(string query,CommandType commandtype, ConnectionState connectionstate)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            
            object o = null;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                o = objCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                objCommand.Parameters.Clear();
                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    objConnection.Close();
                }
            }

            return o;
        }

        public DbDataReader ExecuteReader(string query)
        {
            return ExecuteReader(query, CommandType.Text, ConnectionState.CloseOnExit);
        }

        public DbDataReader ExecuteReader(string query,CommandType commandtype)
        {
            return ExecuteReader(query, commandtype, ConnectionState.CloseOnExit);
        }

        public DbDataReader ExecuteReader(string query, ConnectionState connectionstate)
        {
            return ExecuteReader(query, CommandType.Text, connectionstate);
        }

        public DbDataReader ExecuteReader(string query,CommandType commandtype, ConnectionState connectionstate)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            DbDataReader reader=null;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    reader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
                }
                else
                {
                    reader = objCommand.ExecuteReader();
                }

            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                objCommand.Parameters.Clear();
            }

            return reader;
        }

        public DataSet ExecuteDataSet(string query)
        {
            return ExecuteDataSet(query, CommandType.Text, ConnectionState.CloseOnExit);
        }

        public DataSet ExecuteDataSet(string query,CommandType commandtype)
        {
            return ExecuteDataSet(query, commandtype, ConnectionState.CloseOnExit);
        }

        public DataSet ExecuteDataSet(string query,ConnectionState connectionstate)
        {
            return ExecuteDataSet(query, CommandType.Text, connectionstate);
        }

        public DataSet ExecuteDataSet(string query,CommandType commandtype, ConnectionState connectionstate)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            adapter.SelectCommand = objCommand;
            DataSet ds = new DataSet();
            try
            {
                objCommand.Connection.Open();
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                objCommand.Parameters.Clear();
                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    if (objConnection.State == System.Data.ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                }
            }
            return ds;
        }

        private void HandleExceptions(Exception ex)
        {
            if (LogErrors)
            {
                WriteToLog(ex.Message);
            }
            if (HandleErrors)
            {
            
                strLastError = ex.Message;
            }
            {
                throw ex;
            }
        }

        private void WriteToLog(string msg)
        {
            StreamWriter writer= File.AppendText(LogFile);
            writer.WriteLine(DateTime.Now.ToString() + " - " + msg);
            writer.Close();
        }
        
        public void Dispose()
        {
            objConnection.Close();
            objConnection.Dispose();
            objCommand.Dispose();
        }

        public  string getNextVoucherReference()
        {
            string voucherReference = "";
            OleDbCommand objOledbCommand = new OleDbCommand("st_get_voucher_refrence",objConnection);          
            objOledbCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter parameter = new OleDbParameter("@voucharReference", OleDbType.VarChar, 50);
            parameter.Direction = ParameterDirection.Output;
            objOledbCommand.Parameters.Add(parameter);
            
            int i = -1;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = objOledbCommand.ExecuteNonQuery();
                voucherReference = parameter.Value.ToString();
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                objCommand.Parameters.Clear();
                
                    objConnection.Close();
                
            }            

            return voucherReference;
        }

    }

   

    public enum ConnectionState
    {
        KeepOpen,CloseOnExit
    }

