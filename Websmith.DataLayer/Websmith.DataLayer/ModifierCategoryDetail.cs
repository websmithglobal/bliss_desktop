using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class ModifierCategoryDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();
       
        public bool InsertUpdateDeleteModifierCategoryDetail(ENT.ModifierCategoryDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteModifierCategoryDetail";
                sqlCMD.Parameters.AddWithValue("@ModifierCategoryID", objENT.ModifierCategoryID);
                sqlCMD.Parameters.AddWithValue("@ModifierCategoryName", objENT.ModifierCategoryName);
                sqlCMD.Parameters.AddWithValue("@IsForced", objENT.IsForced);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@Sort", objENT.Sort);
                sqlCMD.Parameters.AddWithValue("@ModifierCategoryDetail_Id", objENT.ModifierCategoryDetail_Id);
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

        public List<ENT.ModifierCategoryDetail> getModifierCategoryDetail(ENT.ModifierCategoryDetail objENT)
        {
            List<ENT.ModifierCategoryDetail> lstENT = new List<ENT.ModifierCategoryDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetModifierCategoryDetail";
                sqlCMD.Parameters.AddWithValue("@ModifierCategoryID", objENT.ModifierCategoryID);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.ModifierCategoryDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.ModifierCategoryDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateModifierCategoryByID(string ModifierCategoryID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [ModifierCategoryDetail] WHERE ModifierCategoryID = '" + ModifierCategoryID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateModifierCategoryByName(string ModifierCategoryName)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [ModifierCategoryDetail] WHERE ModifierCategoryName = '" + ModifierCategoryName + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int DeleteModifierCategoryForDownStream()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "DELETE FROM [ModifierCategoryDetail]";
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
