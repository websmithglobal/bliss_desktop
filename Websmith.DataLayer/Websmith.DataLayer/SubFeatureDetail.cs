using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class SubFeatureDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteSubFeatureDetail(ENT.SubFeatureDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteSubFeatureDetail";
                sqlCMD.Parameters.AddWithValue("@SubFeatureCode", objENT.SubFeatureCode);
                sqlCMD.Parameters.AddWithValue("@SubFeatureName", objENT.SubFeatureName);
                sqlCMD.Parameters.AddWithValue("@SubFeatureDetail_Id", objENT.SubFeatureDetail_Id);
                sqlCMD.Parameters.AddWithValue("@FeatureDetail_Id", objENT.FeatureDetail_Id);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }

        public List<ENT.SubFeatureDetail> getFeatureDetail(ENT.SubFeatureDetail objENT)
        {
            List<ENT.SubFeatureDetail> lstENT = new List<ENT.SubFeatureDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetSubFeatureDetail";
                sqlCMD.Parameters.AddWithValue("@SubFeatureDetail_Id", objENT.SubFeatureDetail_Id);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.SubFeatureDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.SubFeatureDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateSubFeatureDetailByID(string SubFeatureDetailId)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [SubFeatureDetail] WHERE SubFeatureDetail_Id = '" + SubFeatureDetailId + "'";
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
