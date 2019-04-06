using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class DiscountMasterDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteDiscountMasterDetail(ENT.DiscountMasterDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteDiscountMasterDetail";
                sqlCMD.Parameters.AddWithValue("@DiscountID", objENT.DiscountID);
                sqlCMD.Parameters.AddWithValue("@DiscountName", objENT.DiscountName);
                sqlCMD.Parameters.AddWithValue("@DiscountType", objENT.DiscountType);
                sqlCMD.Parameters.AddWithValue("@AmountOrPercentage", objENT.AmountOrPercentage);
                sqlCMD.Parameters.AddWithValue("@QualificationType", objENT.QualificationType);
                sqlCMD.Parameters.AddWithValue("@IsTaxed", objENT.IsTaxed);
                sqlCMD.Parameters.AddWithValue("@Barcode", objENT.Barcode);
                sqlCMD.Parameters.AddWithValue("@DiscountCode", objENT.DiscountCode);
                sqlCMD.Parameters.AddWithValue("@PasswordRequired", objENT.PasswordRequired);
                sqlCMD.Parameters.AddWithValue("@DisplayOnPOS", objENT.DisplayOnPOS);
                sqlCMD.Parameters.AddWithValue("@AutoApply", objENT.AutoApply);
                sqlCMD.Parameters.AddWithValue("@DisplayToCustomer", objENT.DisplayToCustomer);
                sqlCMD.Parameters.AddWithValue("@IsTimeBase", objENT.IsTimeBase);
                sqlCMD.Parameters.AddWithValue("@IsLoyaltyRewards", objENT.IsLoyaltyRewards);
                sqlCMD.Parameters.AddWithValue("@DiscountMasterDetail_Id", objENT.DiscountMasterDetail_Id);

                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }

        public List<ENT.DiscountMasterDetail> getDiscountMasterDetail(ENT.DiscountMasterDetail objENT)
        {
            List<ENT.DiscountMasterDetail> lstENT = new List<ENT.DiscountMasterDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetDiscountMasterDetail";
                sqlCMD.Parameters.AddWithValue("@DiscountID", objENT.DiscountID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.DiscountMasterDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.DiscountMasterDetail>(sdr);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getCoutDiscountMasterByDiscountID(string DiscountID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [DiscountMasterDetail] WHERE DiscountID = '" + DiscountID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getCoutDiscountMasterByDiscountName(string DiscountName)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [DiscountMasterDetail] WHERE DiscountName = '" + DiscountName + "'";
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
