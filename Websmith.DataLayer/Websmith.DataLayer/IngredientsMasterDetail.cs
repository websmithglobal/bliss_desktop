using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class IngredientsMasterDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteIngredientsMasterDetail(ENT.IngredientsMasterDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteIngredientsMasterDetail";
                sqlCMD.Parameters.AddWithValue("@IngredientsID", objENT.IngredientsID);
                sqlCMD.Parameters.AddWithValue("@IngredientName", objENT.IngredientName);
                sqlCMD.Parameters.AddWithValue("@IngredientsMasterDetail_Id", objENT.IngredientsMasterDetail_Id);
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
        
        public List<ENT.IngredientsMasterDetail> getIngredientsMasterDetail(ENT.IngredientsMasterDetail objENT)
        {
            List<ENT.IngredientsMasterDetail> lstENT= new List<ENT.IngredientsMasterDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetIngredientsMasterDetail";
                sqlCMD.Parameters.AddWithValue("@IngredientsID", objENT.IngredientsID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.IngredientsMasterDetail>(sqlCMD);
                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.IngredientsMasterDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateIngredientsMasterByID(string IngredientsID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [IngredientsMasterDetail] WHERE IngredientsID = '" + IngredientsID + "'";
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
