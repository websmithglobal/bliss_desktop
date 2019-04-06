using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class ModuleAppIDDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteModuleAppIDDetail(ENT.ModuleAppIDDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteModuleAppIDDetail";
                sqlCMD.Parameters.AddWithValue("@AppID", objENT.AppID);
                sqlCMD.Parameters.AddWithValue("@DeviceName", objENT.DeviceName);
                sqlCMD.Parameters.AddWithValue("@ModuleMasterDetail_Id", objENT.ModuleMasterDetail_Id);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }

        public List<ENT.ModuleAppIDDetail> getModuleAppIDDetail(ENT.ModuleAppIDDetail objENT)
        {
            List<ENT.ModuleAppIDDetail> lstENT = new List<ENT.ModuleAppIDDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetModuleAppIDDetail";
                sqlCMD.Parameters.AddWithValue("@AppID", objENT.AppID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.ModuleAppIDDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.ModuleAppIDDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateModuleAppIDDetailByID(string AppID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [ModuleAppIDDetail] WHERE AppID = '" + AppID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int deleteModuleAppIDDetail()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "DELETE FROM [ModuleAppIDDetail]";
                sqlCMD.Connection = GetConnection.GetDBConnection();
                duplicateCount = sqlCMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }
    }
}
