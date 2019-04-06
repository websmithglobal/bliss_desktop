using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class ModuleMasterDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteModuleMasterDetail(ENT.ModuleMasterDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteModuleMasterDetail";
                sqlCMD.Parameters.AddWithValue("@ModuleID", objENT.ModuleID);
                sqlCMD.Parameters.AddWithValue("@ModuleName", objENT.ModuleName);
                sqlCMD.Parameters.AddWithValue("@NoOfModule", objENT.NoOfModule);
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

        public List<ENT.ModuleMasterDetail> getModuleMasterDetail(ENT.ModuleMasterDetail objENT)
        {
            List<ENT.ModuleMasterDetail> lstENT = new List<ENT.ModuleMasterDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetModuleMasterDetail";
                sqlCMD.Parameters.AddWithValue("@ModuleID", objENT.ModuleID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.ModuleMasterDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.ModuleMasterDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateModuleMasterDetailByID(string ModuleID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [ModuleMasterDetail] WHERE ModuleID = '" + ModuleID + "'";
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
