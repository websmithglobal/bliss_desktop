using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class VendorMasterData
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();
        ENT.VendorMasterData objENTVendor = new ENT.VendorMasterData();

        public bool InsertUpdateDeleteVendorMaster(ENT.VendorMasterData objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteVendorMaster";
                sqlCMD.Parameters.AddWithValue("@VendorID", objENT.VendorID);
                sqlCMD.Parameters.AddWithValue("@VendorName", objENT.VendorName);
                sqlCMD.Parameters.AddWithValue("@VendorAddress", objENT.VendorAddress);
                sqlCMD.Parameters.AddWithValue("@MobileNo", objENT.MobileNo);
                sqlCMD.Parameters.AddWithValue("@EmailID", objENT.EmailID);
                sqlCMD.Parameters.AddWithValue("@CompanyName", objENT.CompanyName);
                sqlCMD.Parameters.AddWithValue("@RUserID", objENT.RUserID);
                sqlCMD.Parameters.AddWithValue("@RUserType", objENT.RUserType);
                sqlCMD.Parameters.AddWithValue("@IsUPStream", objENT.IsUPStream);
                sqlCMD.Parameters.AddWithValue("@Status", objENT.Status);
                sqlCMD.Parameters.AddWithValue("@ContactPersonName", objENT.CompanyName);
                sqlCMD.Parameters.AddWithValue("@PinCode", objENT.PinCode);
                sqlCMD.Parameters.AddWithValue("@Fax", objENT.Fax);
                sqlCMD.Parameters.AddWithValue("@IsSendPOInMail", objENT.IsSendPOInMail);
                sqlCMD.Parameters.AddWithValue("@IsSendPOInSMS", objENT.IsSendPOInSMS);
                sqlCMD.Parameters.AddWithValue("@MinOrderAmt", objENT.MinOrderAmt);
                sqlCMD.Parameters.AddWithValue("@ShippingCharges", objENT.ShippingCharges);
                sqlCMD.Parameters.AddWithValue("@PaymentTermsID", objENT.PaymentTermsID);
                sqlCMD.Parameters.AddWithValue("@Remarks", objENT.Remarks);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }

        public List<ENT.VendorMasterData> getVendorMaster(ENT.VendorMasterData objENT)
        {
            List<ENT.VendorMasterData> lstENT = new List<ENT.VendorMasterData>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetVendorMaster";
                sqlCMD.Parameters.AddWithValue("@VendorID", objENT.VendorID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.VendorMasterData>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.VendorMasterData>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public List<ENT.VendorMasterView> getVendorMasterView(ENT.VendorMasterData objENT)
        {
            List<ENT.VendorMasterView> lstENT = new List<ENT.VendorMasterView>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetVendorMaster";
                sqlCMD.Parameters.AddWithValue("@VendorID", objENT.VendorID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.VendorMasterView>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.VendorMasterData>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateVendorByID(string VendorID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [VendorMasterData] WHERE VendorID = '" + VendorID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateVendorByName(string VendorName)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [VendorMasterData] WHERE VendorName = '" + VendorName + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateVendorByName(string VendorName, string VendorID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [VendorMasterData] WHERE VendorID <> '" + VendorID + "' AND VendorName = '" + VendorName + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int IsUpStreamTrue()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "UPDATE [VendorMasterData] SET IsUPStream = 1 WHERE IsUPStream = 0";
                duplicateCount = objCRUD.ExecuteQuery(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }
    }
}
