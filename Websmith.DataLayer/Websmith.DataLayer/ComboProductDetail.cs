using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class ComboProductDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteComboProductDetail(ENT.ComboProductDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteComboProductDetail";
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@IsDefault", objENT.IsDefault);
                sqlCMD.Parameters.AddWithValue("@ProductName", objENT.ProductName);
                sqlCMD.Parameters.AddWithValue("@ComboDetail_Id", objENT.ComboDetail_Id);
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

        public List<ENT.ComboProductDetail> getComboDetail(ENT.ComboProductDetail objENT)
        {
            List<ENT.ComboProductDetail> lstENT = new List<ENT.ComboProductDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetComboProductDetail";
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.ComboProductDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.ComboProductDetail>(sdr);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public List<ENT.ComboProductDetailItem> getComboItemForSocket(ENT.ComboProductDetail objENT)
        {
            List<ENT.ComboProductDetailItem> lstENT = new List<ENT.ComboProductDetailItem>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetComboProductDetail";
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.ComboProductDetailItem>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateComboDetailBySetID(string ProductID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [ComboProductDetail] WHERE ProductID = '" + ProductID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int DeleteComboProductDetailForDownStream()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "DELETE FROM [ComboProductDetail];";
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
