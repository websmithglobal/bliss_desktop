using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Websmith.DataLayer
{
    public class OutwardMaster
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteOutwardMaster(ENT.OutwardMaster objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteOutwardMaster";
                sqlCMD.Parameters.AddWithValue("@OutwardID", objENT.OutwardID);
                sqlCMD.Parameters.AddWithValue("@InvoiceNo", objENT.InvoiceNo);
                sqlCMD.Parameters.AddWithValue("@InvoiceDate", objENT.InvoiceDate);
                sqlCMD.Parameters.AddWithValue("@EmpID", objENT.EmpID);
                sqlCMD.Parameters.AddWithValue("@FinalTotal", objENT.FinalTotal);
                sqlCMD.Parameters.AddWithValue("@Remark", objENT.Remark);
                sqlCMD.Parameters.AddWithValue("@IsUpStream", objENT.IsUpStream);
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

        public List<ENT.OutwardMaster> GetOutwardMaster(ENT.OutwardMaster objENT)
        {
            List<ENT.OutwardMaster> lstENT = new List<ENT.OutwardMaster>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOutwardMaster";
                sqlCMD.Parameters.AddWithValue("@OutwardID", objENT.OutwardID);
                sqlCMD.Parameters.AddWithValue("@DateFrom", objENT.DateFrom);
                sqlCMD.Parameters.AddWithValue("@DateTo", objENT.DateTo);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.OutwardMaster>(sqlCMD);
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

        public int getDuplicateInvoiceByInvoiceNo(string OutwardID, string InvoiceNo)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [OutwardMaster] WHERE OutwardID <> '" + OutwardID + "' AND InvoiceNo = '" + InvoiceNo + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }
        
        public int getNewInvoiceNo()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT TOP 1 InvoiceNo FROM [OutwardMaster] ORDER BY InvoiceNo DESC";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                if(dt.Rows.Count>0)
                    duplicateCount = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
                else
                    duplicateCount = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public decimal getProductWiseStock(string ProductID)
        {
            decimal duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT SUM(Qty) AS Qty FROM ViewStockMaster WHERE ProductID='"+ ProductID + "' GROUP BY ProductID";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                if (dt.Rows.Count > 0)
                    duplicateCount = Convert.ToDecimal(dt.Rows[0][0].ToString());
                else
                    duplicateCount = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public DataTable GetDatatableForExportExcel(ENT.OutwardMaster objENT)
        {
            CRUDOperation objCRUD = new CRUDOperation();
            DataTable dt = new DataTable();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOutwardMaster";
                sqlCMD.Parameters.AddWithValue("@OutwardID", objENT.OutwardID);
                sqlCMD.Parameters.AddWithValue("@DateFrom", objENT.DateFrom);
                sqlCMD.Parameters.AddWithValue("@DateTo", objENT.DateTo);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                dt = DBHelper.GetDatatable(sqlCMD);
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

    }
}
