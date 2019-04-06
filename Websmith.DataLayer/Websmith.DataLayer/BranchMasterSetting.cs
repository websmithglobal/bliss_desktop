using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class BranchMasterSetting
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();
        ENT.BranchMasterSetting objENTBMS = new ENT.BranchMasterSetting();

        public bool InsertUpdateDeleteBranchMasterSetting(ENT.BranchMasterSetting objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteBranchMasterSetting";
                sqlCMD.Parameters.AddWithValue("@BranchID", objENT.BranchID);
                sqlCMD.Parameters.AddWithValue("@RestaurantID", objENT.RestaurantID);
                sqlCMD.Parameters.AddWithValue("@ContactNoForService", objENT.ContactNoForService);
                sqlCMD.Parameters.AddWithValue("@DeliveryCharges", objENT.DeliveryCharges);
                sqlCMD.Parameters.AddWithValue("@DeliveryTime", objENT.DeliveryTime);
                sqlCMD.Parameters.AddWithValue("@PickupTime", objENT.PickupTime);
                sqlCMD.Parameters.AddWithValue("@CurrencyName", objENT.CurrencyName);
                sqlCMD.Parameters.AddWithValue("@CurrencySymbol", objENT.CurrencySymbol);
                sqlCMD.Parameters.AddWithValue("@WorkingDays", objENT.WorkingDays);
                sqlCMD.Parameters.AddWithValue("@TagLine", objENT.TagLine);
                sqlCMD.Parameters.AddWithValue("@Footer", objENT.Footer);
                sqlCMD.Parameters.AddWithValue("@DeliveryAreaRedius", objENT.DeliveryAreaRedius);
                sqlCMD.Parameters.AddWithValue("@DeliveryAreaTitle", objENT.DeliveryAreaTitle);
                sqlCMD.Parameters.AddWithValue("@DistanceType", objENT.DistanceType);
                sqlCMD.Parameters.AddWithValue("@DistanceName", objENT.DistanceName);
                sqlCMD.Parameters.AddWithValue("@FreeDeliveryUpto", objENT.FreeDeliveryUpto);
                sqlCMD.Parameters.AddWithValue("@BranchName", objENT.BranchName);
                sqlCMD.Parameters.AddWithValue("@BranchEmailID", objENT.BranchEmailID);
                sqlCMD.Parameters.AddWithValue("@MobileNo", objENT.MobileNo);
                sqlCMD.Parameters.AddWithValue("@LastSyncDate", objENT.LastSyncDate);
                sqlCMD.Parameters.AddWithValue("@VatNo", objENT.VatNo);
                sqlCMD.Parameters.AddWithValue("@CSTNo", objENT.CSTNo);
                sqlCMD.Parameters.AddWithValue("@ServiceTaxNo", objENT.ServiceTaxNo);
                sqlCMD.Parameters.AddWithValue("@TinGSTNo", objENT.TinGSTNo);
                sqlCMD.Parameters.AddWithValue("@Address", objENT.Address);
                sqlCMD.Parameters.AddWithValue("@SubAreaStreet", objENT.SubAreaStreet);
                sqlCMD.Parameters.AddWithValue("@PinCode", objENT.PinCode);
                sqlCMD.Parameters.AddWithValue("@VersionCode", objENT.VersionCode);
                sqlCMD.Parameters.AddWithValue("@BranchMasterSetting_Id", objENT.BranchMasterSetting_Id);
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

        public List<ENT.BranchMasterSetting> getBranchMasterSetting(ENT.BranchMasterSetting objENT)
        {
            List<ENT.BranchMasterSetting> lstENT = new List<ENT.BranchMasterSetting>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetBranchMasterSetting";
                sqlCMD.Parameters.AddWithValue("@BranchID", objENT.BranchID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.BranchMasterSetting>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.BranchMasterSetting>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateBranchMasterSettingByID(string BranchID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [BranchMasterSetting] WHERE BranchID = '" + BranchID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateBranchMasterSettingByName(string BranchName)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [BranchMasterSetting] WHERE BranchName = '" + BranchName + "'";
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
