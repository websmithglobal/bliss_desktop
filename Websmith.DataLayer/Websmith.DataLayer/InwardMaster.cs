using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Websmith.DataLayer
{
    public class InwardMaster
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteInwardMaster(ENT.InwardMaster objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteInwardMaster";
                sqlCMD.Parameters.AddWithValue("@InwardID", objENT.InwardID);
                sqlCMD.Parameters.AddWithValue("@InvoiceNo", objENT.InvoiceNo);
                sqlCMD.Parameters.AddWithValue("@InvoiceDate", objENT.InvoiceDate);
                sqlCMD.Parameters.AddWithValue("@VedorID", objENT.VedorID);
                sqlCMD.Parameters.AddWithValue("@PONo", objENT.PONo);
                sqlCMD.Parameters.AddWithValue("@OtherChargeDetail", objENT.OtherChargeDetail);
                sqlCMD.Parameters.AddWithValue("@OtherCharge", objENT.OtherCharge);
                sqlCMD.Parameters.AddWithValue("@DiscountAmount", objENT.DiscountAmount);
                sqlCMD.Parameters.AddWithValue("@TaxAmount", objENT.TaxAmount);
                sqlCMD.Parameters.AddWithValue("@RoundOffAmount", objENT.RoundOffAmount);
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

        public List<ENT.InwardMaster> GetInwardMaster(ENT.InwardMaster objENT)
        {
            List<ENT.InwardMaster> lstENT = new List<ENT.InwardMaster>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetInwardMaster";
                sqlCMD.Parameters.AddWithValue("@InwardID", objENT.InwardID);
                sqlCMD.Parameters.AddWithValue("@OrderDateFrom", objENT.DateFrom);
                sqlCMD.Parameters.AddWithValue("@OrderDateTo", objENT.DateTo);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.InwardMaster>(sqlCMD);
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

        public int getDuplicateInvoiceByInvoiceNo(string InwardID, string InvoiceNo)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [InwardMaster] WHERE InwardID <> '" + InwardID + "' AND InvoiceNo = '" + InvoiceNo + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public DataTable GetDatatableForExportExcel(ENT.InwardMaster objENT)
        {
            CRUDOperation objCRUD = new CRUDOperation();
            DataTable dt = new DataTable();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetInwardMaster";
                sqlCMD.Parameters.AddWithValue("@InwardID", objENT.InwardID);
                sqlCMD.Parameters.AddWithValue("@OrderDateFrom", objENT.DateFrom);
                sqlCMD.Parameters.AddWithValue("@OrderDateTo", objENT.DateTo);
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
