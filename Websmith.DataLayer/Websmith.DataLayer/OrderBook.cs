using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class OrderBook
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteOrder(ENT.OrderBook objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteOrder";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@OrderNo", objENT.OrderNo);
                sqlCMD.Parameters.AddWithValue("@OrderDate", objENT.OrderDate);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@CustomerID", objENT.CustomerID);
                sqlCMD.Parameters.AddWithValue("@DeliveryType", objENT.DeliveryType);
                sqlCMD.Parameters.AddWithValue("@DeliveryTypeName", objENT.DeliveryTypeName);
                sqlCMD.Parameters.AddWithValue("@TableID", objENT.TableID);
                sqlCMD.Parameters.AddWithValue("@SubTotal", objENT.SubTotal);
                sqlCMD.Parameters.AddWithValue("@ExtraCharge", objENT.ExtraCharge);
                sqlCMD.Parameters.AddWithValue("@TaxLabel1", objENT.TaxLabel1);
                sqlCMD.Parameters.AddWithValue("@TaxPercent1", objENT.TaxPercent1);
                sqlCMD.Parameters.AddWithValue("@SGSTAmount", objENT.SGSTAmount);
                sqlCMD.Parameters.AddWithValue("@TaxLabel2", objENT.TaxLabel2);
                sqlCMD.Parameters.AddWithValue("@TaxPercent2", objENT.TaxPercent2);
                sqlCMD.Parameters.AddWithValue("@CGSTAmount", objENT.CGSTAmount);
                sqlCMD.Parameters.AddWithValue("@TotalTax", objENT.TotalTax);
                sqlCMD.Parameters.AddWithValue("@DiscountID", objENT.DiscountID);
                sqlCMD.Parameters.AddWithValue("@DiscountPer", objENT.DiscountPer);
                sqlCMD.Parameters.AddWithValue("@Discount", objENT.Discount);
                sqlCMD.Parameters.AddWithValue("@TipGratuity", objENT.TipGratuity);
                sqlCMD.Parameters.AddWithValue("@PayableAmount", objENT.PayableAmount);
                sqlCMD.Parameters.AddWithValue("@TableStatusID", objENT.TableStatusID);
                sqlCMD.Parameters.AddWithValue("@OrderActions", objENT.OrderActions);
                sqlCMD.Parameters.AddWithValue("@OrderSpecialRequest", objENT.OrderSpecialRequest);
                sqlCMD.Parameters.AddWithValue("@DiscountType", objENT.DiscountType);
                sqlCMD.Parameters.AddWithValue("@DiscountRemark", objENT.DiscountRemark);
                sqlCMD.Parameters.AddWithValue("@DeliveryCharge", objENT.DeliveryCharge);
                sqlCMD.Parameters.AddWithValue("@RUserID", objENT.RUserID);
                sqlCMD.Parameters.AddWithValue("@RUserType", objENT.RUserType);
                sqlCMD.Parameters.AddWithValue("@StartTime", objENT.StartTime);
                sqlCMD.Parameters.AddWithValue("@OrderStatus", objENT.OrderStatus);
                sqlCMD.Parameters.AddWithValue("@IsPrint", objENT.IsPrint);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception)
            {
                throw;
            }
            return row;
        }

        public bool UpdateKOTPrintStatus(List<ENT.OrderBook> objENT)
        {
            bool row = false;
            try
            {
                for (int i = 0; i < objENT.Count; i++)
                {
                    sqlCMD = new SqlCommand();
                    sqlCMD.CommandText = "InsertUpdateDeleteOrder";
                    sqlCMD.Parameters.AddWithValue("@OrderID", objENT[i].OrderID);
                    sqlCMD.Parameters.AddWithValue("@ProductID", objENT[i].ProductID);
                    sqlCMD.Parameters.AddWithValue("@CategoryID", objENT[i].CategoryID);
                    sqlCMD.Parameters.AddWithValue("@IsSendToKitchen", objENT[i].IsSendToKitchen);
                    sqlCMD.Parameters.AddWithValue("@HeaderStatus", objENT[i].HeaderStatus);
                    sqlCMD.Parameters.AddWithValue("@Mode", objENT[i].Mode);
                    row = objCRUD.InsertUpdateDelete(sqlCMD);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return row;
        }

        public List<ENT.OrderBook> getOrder(ENT.OrderBook objENT)
        {
            List<ENT.OrderBook> lstENT = new List<ENT.OrderBook>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOrder";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@TableID", objENT.TableID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@DeliveryType", objENT.DeliveryType);
                sqlCMD.Parameters.AddWithValue("@OrderDateFrom", objENT.OrderDateFrom);
                sqlCMD.Parameters.AddWithValue("@OrderDateTo", objENT.OrderDateTo);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.OrderBook>(sqlCMD);
            }
            catch (Exception)
            {
                throw;
            }
            return lstENT;
        }

        public List<ENT.Object> getOrderForSocket(ENT.OrderBook objENT)
        {
            List<ENT.Object> lstENT = new List<ENT.Object>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOrder";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@TableID", objENT.TableID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@DeliveryType", objENT.DeliveryType);
                sqlCMD.Parameters.AddWithValue("@OrderDateFrom", objENT.OrderDateFrom);
                sqlCMD.Parameters.AddWithValue("@OrderDateTo", objENT.OrderDateTo);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.Object>(sqlCMD);
            }
            catch (Exception)
            {
                throw;
            }
            return lstENT;
        }

        public List<ENT.OrderBook> getSalesReport(ENT.OrderBook objENT)
        {
            List<ENT.OrderBook> lstENT = new List<ENT.OrderBook>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetSalesReport";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@TableID", objENT.TableID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@DeliveryType", objENT.DeliveryType);
                sqlCMD.Parameters.AddWithValue("@OrderDateFrom", objENT.OrderDateFrom);
                sqlCMD.Parameters.AddWithValue("@OrderDateTo", objENT.OrderDateTo);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.OrderBook>(sqlCMD);
            }
            catch (Exception)
            {
                throw;
            }
            return lstENT;
        }

        public List<ENT.OrderMaster> getOrderForUpStream(ENT.OrderMaster objENT)
        {
            List<ENT.OrderMaster> lstENT = new List<ENT.OrderMaster>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOrder";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.OrderMaster>(sqlCMD);
            }
            catch (Exception)
            {
                throw;
            }
            return lstENT;
        }

        public List<ENT.OrderMasterTax> getOrderForUpStreamTax(ENT.OrderMasterTax objENT)
        {
            List<ENT.OrderMasterTax> lstENT = new List<ENT.OrderMasterTax>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOrder";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.OrderMasterTax>(sqlCMD);
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

        public int getDuplicateOrderByID(string OrderID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [Order] WHERE OrderID = '" + OrderID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateOrderByOrderNo(string OrderNo)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [Order] WHERE OrderNo = '" + OrderNo + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public Int64 getTokenNoFromOrder(string date)
        {
            Int64 intTokenNo = 0;
            try
            {
                string strQuery = "SELECT TOP 1 TokenNo FROM [order] WHERE DATEADD(DAY, DATEDIFF(DAY, 0, OrderDate), 0) = '" + date + "' ORDER BY TokenNo DESC";
                sqlCMD = new SqlCommand(strQuery);
                intTokenNo = objCRUD.ExecuteScalar(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intTokenNo + 1;
        }

        public Int64 getDateWiseOrderCount(string startdate, string enddate)
        {
            Int64 count = 0;
            try
            {
                string strQuery = "SELECT COUNT(*) FROM [Order] INNER JOIN CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID LEFT OUTER JOIN TableMasterDetail ON [Order].TableID = TableMasterDetail.TableID WHERE DATEADD(DAY, DATEDIFF(DAY, 0, [Order].OrderDate), 0) BETWEEN '" + startdate + "' AND '" + enddate + "' AND [Order].OrderActions = 2 AND [Order].OrderStatus = 3";
                sqlCMD = new SqlCommand(strQuery);
                count = objCRUD.ExecuteScalar(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return count;
        }

        public DataTable GetDatatableForExportExcel(ENT.OrderBook objENT)
        {
            CRUDOperation objCRUD = new CRUDOperation();
            DataTable dt = new DataTable();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetSalesReport";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@DeliveryType", objENT.DeliveryType);
                sqlCMD.Parameters.AddWithValue("@OrderDateFrom", objENT.OrderDateFrom);
                sqlCMD.Parameters.AddWithValue("@OrderDateTo", objENT.OrderDateTo);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                dt = DBHelper.GetDatatable(sqlCMD);
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable getDateWiseStock(string Query)
        {
            DataTable dt = new DataTable();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = Query;
                dt = objCRUD.getDataTableByQuery(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

    }
}
