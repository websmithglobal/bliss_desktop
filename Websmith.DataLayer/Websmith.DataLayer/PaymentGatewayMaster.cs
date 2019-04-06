using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class PaymentGatewayMaster
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeletePaymentGatewayMaster(ENT.PaymentGatewayMaster objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeletePaymentGatewayMaster";
                sqlCMD.Parameters.AddWithValue("@PaymentGatewayID", objENT.PaymentGatewayID);
                sqlCMD.Parameters.AddWithValue("@PaymentGatewayName", objENT.PaymentGatewayName);
                sqlCMD.Parameters.AddWithValue("@MerchantID", objENT.MerchantID);
                sqlCMD.Parameters.AddWithValue("@TokenKey", objENT.TokenKey);
                sqlCMD.Parameters.AddWithValue("@UserName", objENT.UserName);
                sqlCMD.Parameters.AddWithValue("@Password", objENT.Password);
                sqlCMD.Parameters.AddWithValue("@ResponseUrl", objENT.ResponseUrl);
                sqlCMD.Parameters.AddWithValue("@ATOMTransactionType", objENT.ATOMTransactionType);
                sqlCMD.Parameters.AddWithValue("@Productid", objENT.Productid);
                sqlCMD.Parameters.AddWithValue("@Version", objENT.Version);
                sqlCMD.Parameters.AddWithValue("@ServiceID", objENT.ServiceID);
                sqlCMD.Parameters.AddWithValue("@ApplicationProfileId", objENT.ApplicationProfileId);
                sqlCMD.Parameters.AddWithValue("@MerchantProfileId", objENT.MerchantProfileId);
                sqlCMD.Parameters.AddWithValue("@MerchantProfileName", objENT.MerchantProfileName);
                sqlCMD.Parameters.AddWithValue("@IsUPStream", objENT.IsUPStream);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }

        public List<ENT.PaymentGatewayMaster> getPaymentGatewayMaster(ENT.PaymentGatewayMaster objENT)
        {
            List<ENT.PaymentGatewayMaster> lstENT = new List<ENT.PaymentGatewayMaster>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetPaymentGatewayMaster";
                sqlCMD.Parameters.AddWithValue("@PaymentGatewayID", objENT.PaymentGatewayID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.PaymentGatewayMaster>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicatePaymentGatewayByID(string PaymentGatewayID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [PaymentGatewayMaster] WHERE PaymentGatewayID = '" + PaymentGatewayID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicatePaymentGatewayByName(string PaymentGatewayName)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [PaymentGatewayMaster] WHERE PaymentGatewayName = '" + PaymentGatewayName + "'";
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
