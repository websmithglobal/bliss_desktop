using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class RightDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteRightDetail(ENT.RightDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteRightDetail";
                sqlCMD.Parameters.AddWithValue("@RightCode", objENT.RightCode);
                sqlCMD.Parameters.AddWithValue("@RightName", objENT.RightName);
                sqlCMD.Parameters.AddWithValue("@SubFeatureDetail_Id", objENT.SubFeatureDetail_Id);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }

        public List<ENT.RightDetail> getRightDetail(ENT.RightDetail objENT)
        {
            List<ENT.RightDetail> lstENT = new List<ENT.RightDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetRightDetail";
                sqlCMD.Parameters.AddWithValue("@SubFeatureDetail_Id", objENT.SubFeatureDetail_Id);
                sqlCMD.Parameters.AddWithValue("@RightCode", objENT.RightCode);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.RightDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.RightDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateRightDetailByID(string SubFeatureDetailId, string RightCode)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [RightDetail] WHERE SubFeatureDetail_Id = '" + SubFeatureDetailId + "' AND RightCode = " + RightCode + "";
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
