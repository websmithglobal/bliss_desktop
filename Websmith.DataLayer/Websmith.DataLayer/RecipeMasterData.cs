using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class RecipeMasterData
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteRecipeMasterData(ENT.RecipeMasterData objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteRecipeMasterData";
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@ProductName", objENT.ProductName);
                sqlCMD.Parameters.AddWithValue("@RecipeID", objENT.RecipeID);
                sqlCMD.Parameters.AddWithValue("@RecipeText", objENT.RecipeText);
                sqlCMD.Parameters.AddWithValue("@RecipeMasterData_Id", objENT.RecipeMasterData_Id);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }

        public List<ENT.RecipeMasterData> getRecipeMasterData(ENT.RecipeMasterData objENT)
        {
            List<ENT.RecipeMasterData> lstENT = new List<ENT.RecipeMasterData>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetRecipeMasterData";
                sqlCMD.Parameters.AddWithValue("@RecipeID", objENT.RecipeID);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.RecipeMasterData>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.RecipeMasterData>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateRecipeMasterDataByID(string RecipeID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [RecipeMasterData] WHERE RecipeID = '" + RecipeID + "'";
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
