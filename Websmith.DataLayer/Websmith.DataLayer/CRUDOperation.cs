using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{ 
    public class CRUDOperation
    {
        private SqlConnection sqlCON;
       
        public CRUDOperation()
        {
            sqlCON = GetConnection.GetDBConnection();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                GetConnection.CloseConnection(sqlCON);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CRUDOperation()
        {
            GetConnection.CloseConnection(sqlCON);
        }

        public Int64 ExecuteScalar(SqlCommand sqlCMD)
        {
            Int64 Rows = 0;
            try
            {
                GetConnection.OpenConnection(sqlCON);
                if (sqlCON.State == ConnectionState.Open)
                {
                    sqlCMD.CommandType = CommandType.Text;
                    sqlCMD.Connection = sqlCON;
                    Rows = Convert.ToInt64(sqlCMD.ExecuteScalar());
                    GetConnection.CloseConnection(sqlCON);
                }
                else
                {
                    throw new Exception("Server not found... database connection error");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                GetConnection.CloseConnection(sqlCON);
            }
            return Rows;
        }
        #region Insert,Update,Delete Methods

        public bool InsertUpdateDelete(SqlCommand sqlCMD)
        {
            bool blnResult = false;
            GetConnection.OpenConnection(sqlCON);
            SqlTransaction transaction = sqlCON.BeginTransaction(IsolationLevel.ReadCommitted);
            sqlCMD.Transaction = transaction;
            try
            {
                if (sqlCON.State == ConnectionState.Open)
                {
                    sqlCMD.CommandType = CommandType.StoredProcedure;
                    sqlCMD.Connection = sqlCON;
                    int row = sqlCMD.ExecuteNonQuery();
                    if (row > 0)
                    {
                        blnResult = true;
                        transaction.Commit();
                    }
                    GetConnection.CloseConnection(sqlCON);
                }
                else
                {
                    throw new Exception("Server not found...database connection error");
                }
            }
            catch (Exception)
            {
                blnResult = false;
                transaction.Rollback();
                throw;
            }
            finally
            {
                GetConnection.CloseConnection(sqlCON);
            }
            return blnResult;
        }
        
        public DataTable getDataTable(SqlCommand sqlCMD)
        {
            DataTable dt = new DataTable();
            try
            {
                GetConnection.OpenConnection(sqlCON);
                if (sqlCON.State == ConnectionState.Open)
                {
                    sqlCMD.CommandType = CommandType.StoredProcedure;
                    sqlCMD.Connection = sqlCON;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCMD);
                    da.Fill(dt);
                    GetConnection.CloseConnection(sqlCON);
                }
                else
                {
                    throw new Exception("Server not found...database connection error");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                GetConnection.CloseConnection(sqlCON);
            }
            return dt;
        }
        
        public DataTable getDataTableByQuery(SqlCommand sqlCMD)
        {
            DataTable dt = new DataTable();
            try
            {
                GetConnection.OpenConnection(sqlCON);
                if (sqlCON.State == ConnectionState.Open)
                {
                    sqlCMD.CommandType = CommandType.Text;
                    sqlCMD.Connection = sqlCON;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCMD);
                    da.Fill(dt);
                    GetConnection.CloseConnection(sqlCON);
                }
                else
                {
                    throw new Exception("Server not found... database connection error");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                GetConnection.CloseConnection(sqlCON);
            }
            return dt;
        }

        public int ExecuteQuery(SqlCommand sqlCMD)
        {
            int Rows = 0;
            try
            {
                GetConnection.OpenConnection(sqlCON);
                if (sqlCON.State == ConnectionState.Open)
                {
                    sqlCMD.CommandType = CommandType.Text;
                    sqlCMD.Connection = sqlCON;
                    Rows = sqlCMD.ExecuteNonQuery();
                    GetConnection.CloseConnection(sqlCON);
                }
                else
                {
                    throw new Exception("Server not found... database connection error");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                GetConnection.CloseConnection(sqlCON);
            }
            return Rows;
        }


        #endregion
    }
}
