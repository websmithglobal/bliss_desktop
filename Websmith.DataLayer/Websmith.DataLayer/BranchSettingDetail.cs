using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class BranchSettingDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();
        ENT.BranchSettingDetail objENTBranchSetting = new ENT.BranchSettingDetail();

        public bool InsertUpdateDeleteVersionDetail(ENT.BranchSettingDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteBranchSettingDetail";
                sqlCMD.Parameters.AddWithValue("@IsFranchise", objENT.IsFranchise);
                sqlCMD.Parameters.AddWithValue("@IsReservationOn", objENT.IsReservationOn);
                sqlCMD.Parameters.AddWithValue("@IsOrderBookingOn", objENT.IsOrderBookingOn);
                sqlCMD.Parameters.AddWithValue("@IsAutoAcceptOrderOn", objENT.IsAutoAcceptOrderOn);
                sqlCMD.Parameters.AddWithValue("@IsAutoRoundOffTotalOn", objENT.IsAutoRoundOffTotalOn);
                sqlCMD.Parameters.AddWithValue("@TaxGroupId", objENT.TaxGroupId);
                sqlCMD.Parameters.AddWithValue("@IsDemoVersion", objENT.IsDemoVersion);
                sqlCMD.Parameters.AddWithValue("@ExpireDate", objENT.ExpireDate);
                sqlCMD.Parameters.AddWithValue("@DemoCode", objENT.DemoCode);
                sqlCMD.Parameters.AddWithValue("@BranchID", objENT.BranchID);
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

        public List<ENT.BranchSettingDetail> getVersionDetail(ENT.BranchSettingDetail objENT)
        {
            List<ENT.BranchSettingDetail> lstENT = new List<ENT.BranchSettingDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetBranchSettingDetail";
                sqlCMD.Parameters.AddWithValue("@BranchID", objENT.BranchID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.BranchSettingDetail>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }
        
        public int getCoutBranchSettingByBranchID(string BranchID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [BranchSettingDetail] WHERE BranchID = '" + BranchID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int DelateAllBranchSetting()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "DELETE FROM [BranchSettingDetail];";
                duplicateCount = objCRUD.ExecuteQuery(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public static bool IsFranchise(string BranchID)
        {
            bool boolResult = false;
            try
            {
                CRUDOperation obj = new CRUDOperation();
                SqlCommand sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  IsFranchise FROM [BranchSettingDetail] WHERE BranchID = '" + BranchID + "'";
                DataTable dt = obj.getDataTableByQuery(sqlCMD);
                if (dt.Rows.Count > 0)
                    boolResult = Convert.ToBoolean(dt.Rows[0]["IsFranchise"]);
                else
                    boolResult = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return boolResult;
        }

        public List<ENT.BranchSettingDetail> getBranchSettingDetail(ENT.BranchSettingDetail objENT)
        {
            List<ENT.BranchSettingDetail> lstENT = new List<ENT.BranchSettingDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetBranchSettingDetail";
                sqlCMD.Parameters.AddWithValue("@BranchID", objENT.BranchID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.BranchSettingDetail>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }
    }
}
