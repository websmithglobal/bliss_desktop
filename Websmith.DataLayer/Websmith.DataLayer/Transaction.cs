using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class Transaction
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteOrderTransaction(ENT.Transaction objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteOrderTransaction";
                sqlCMD.Parameters.AddWithValue("@TransactionID", objENT.TransactionID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@CategoryID", objENT.CategoryID);
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@Quantity", objENT.Quantity);
                sqlCMD.Parameters.AddWithValue("@Rate", objENT.Rate);
                sqlCMD.Parameters.AddWithValue("@TotalAmount", objENT.TotalAmount);
                sqlCMD.Parameters.AddWithValue("@Sort", objENT.Sort);
                sqlCMD.Parameters.AddWithValue("@SpecialRequest", objENT.SpecialRequest);
                sqlCMD.Parameters.AddWithValue("@RUserID", objENT.RUserID);
                sqlCMD.Parameters.AddWithValue("@RUserType", objENT.RUserType);
                sqlCMD.Parameters.AddWithValue("@StartDate", objENT.StartDate);
                sqlCMD.Parameters.AddWithValue("@EndDate", objENT.EndDate);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCMD.Connection.Close();
            }
            return row;
        }

        public List<ENT.Transaction> getOrderTransaction(ENT.Transaction objENT)
        {
            List<ENT.Transaction> lstENT = new List<ENT.Transaction>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOrderTransaction";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@CategoryID", objENT.CategoryID);
                sqlCMD.Parameters.AddWithValue("@OrderDateFrom", objENT.OrderDateFrom);
                sqlCMD.Parameters.AddWithValue("@OrderDateTo", objENT.OrderDateTo);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.Transaction>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCMD.Connection.Close();
            }
            return lstENT;
        }

        public List<ENT.OrderTransaction> getOrderTransactionForUpStream(ENT.OrderTransaction objENT)
        {
            List<ENT.OrderTransaction> lstENT = new List<ENT.OrderTransaction>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOrderTransaction";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.OrderTransaction>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCMD.Connection.Close();
            }
            return lstENT;
        }

        public int getDuplicateTransactionByID(string TransactionID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT * FROM [Transaction] WHERE TransactionID = '" + TransactionID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public List<ENT.ItemsList> getItemListForSocket(ENT.Transaction objENT)
        {
            List<ENT.ItemsList> lstENT = new List<ENT.ItemsList>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOrderTransaction";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@CategoryID", objENT.CategoryID);
                sqlCMD.Parameters.AddWithValue("@OrderDateFrom", objENT.OrderDateFrom);
                sqlCMD.Parameters.AddWithValue("@OrderDateTo", objENT.OrderDateTo);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.ItemsList>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCMD.Connection.Close();
            }
            return lstENT;
        }
    }
}
