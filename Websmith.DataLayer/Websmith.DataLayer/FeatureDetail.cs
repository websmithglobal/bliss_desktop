using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class FeatureDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteFeatureDetail(ENT.FeatureDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteFeatureDetail";
                sqlCMD.Parameters.AddWithValue("@FeatureCode", objENT.FeatureCode);
                sqlCMD.Parameters.AddWithValue("@FeatureName", objENT.FeatureName);
                sqlCMD.Parameters.AddWithValue("@FeatureDetail_Id", objENT.FeatureDetail_Id);
                sqlCMD.Parameters.AddWithValue("@RoleMasterDetail_Id", objENT.RoleMasterDetail_Id);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }
        
        public List<ENT.FeatureDetail> getFeatureDetail(ENT.FeatureDetail objENT)
        {
            List<ENT.FeatureDetail> lstENT = new List<ENT.FeatureDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetFeatureDetail";
                sqlCMD.Parameters.AddWithValue("@FeatureDetail_Id", objENT.FeatureDetail_Id);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.FeatureDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.FeatureDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateFeatureDetailByID(string FeatureDetailId)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [FeatureDetail] WHERE FeatureDetail_Id = '" + FeatureDetailId + "'";
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
