using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class CheckOutDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteCheckOutDetail(ENT.CheckOutDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteCheckOutDetail";
                sqlCMD.Parameters.AddWithValue("@TransactionID", objENT.TransactionID);
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@CustomerID", objENT.CustomerID);
                sqlCMD.Parameters.AddWithValue("@TableID", objENT.TableID);
                sqlCMD.Parameters.AddWithValue("@CRMMethod", objENT.CRMMethod);
                sqlCMD.Parameters.AddWithValue("@PaymentMethod", objENT.PaymentMethod);
                sqlCMD.Parameters.AddWithValue("@OrderAmount", objENT.OrderAmount);
                sqlCMD.Parameters.AddWithValue("@PaidAmount", objENT.PaidAmount);
                sqlCMD.Parameters.AddWithValue("@ChangeAmount", objENT.ChangeAmount);
                sqlCMD.Parameters.AddWithValue("@ChequeNo", objENT.ChequeNo);
                sqlCMD.Parameters.AddWithValue("@ChequeDate", objENT.ChequeDate);
                sqlCMD.Parameters.AddWithValue("@CardHolderName", objENT.CardHolderName);
                sqlCMD.Parameters.AddWithValue("@CardNumber", objENT.CardNumber);
                sqlCMD.Parameters.AddWithValue("@OrderActions", objENT.OrderActions);
                sqlCMD.Parameters.AddWithValue("@OrderStatus", objENT.OrderStatus);
                sqlCMD.Parameters.AddWithValue("@TableStatusID", objENT.TableStatusID);
                sqlCMD.Parameters.AddWithValue("@EntryDateTime", objENT.EntryDateTime);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }

        public List<ENT.ComboDetail> getCheckOutDetail(ENT.CheckOutDetail objENT)
        {
            List<ENT.ComboDetail> lstENT = new List<ENT.ComboDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetCheckOutDetail";
                sqlCMD.Parameters.AddWithValue("@TransactionID", objENT.TransactionID);
                sqlCMD.Parameters.AddWithValue("@CustomerID", objENT.CustomerID);
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@TableID", objENT.TableID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.ComboDetail>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public List<ENT.OrderPayment> getCheckOutDetailForUpStream(ENT.OrderPayment objENT)
        {
            List<ENT.OrderPayment> lstENT = new List<ENT.OrderPayment>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetCheckOutDetail";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.OrderPayment>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateCheckOutDetailByOrderID(string OrderID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [CheckOutDetail] WHERE OrderID = '" + OrderID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }
    }
}
