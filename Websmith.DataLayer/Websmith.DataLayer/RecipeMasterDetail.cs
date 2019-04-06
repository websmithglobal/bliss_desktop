using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class RecipeMasterDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteRecipeMasterDetail(ENT.RecipeMasterDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteRecipeMasterDetail";
                sqlCMD.Parameters.AddWithValue("@IngredientsID", objENT.IngredientsID);
                sqlCMD.Parameters.AddWithValue("@Name", objENT.Name);
                sqlCMD.Parameters.AddWithValue("@Qty", objENT.Qty);
                sqlCMD.Parameters.AddWithValue("@Price", objENT.Price);
                sqlCMD.Parameters.AddWithValue("@IsDefault", objENT.IsDefault);
                sqlCMD.Parameters.AddWithValue("@IsQty", objENT.IsQty);
                sqlCMD.Parameters.AddWithValue("@UnitTypeID", objENT.UnitTypeID);
                sqlCMD.Parameters.AddWithValue("@UnitType", objENT.UnitType);
                sqlCMD.Parameters.AddWithValue("@RecipeMasterData_Id", objENT.RecipeMasterData_Id);
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

        public List<ENT.RecipeMasterDetail> getRecipeMasterDetail(ENT.RecipeMasterDetail objENT)
        {
            List<ENT.RecipeMasterDetail> lstENT = new List<ENT.RecipeMasterDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetRecipeMasterDetail";
                sqlCMD.Parameters.AddWithValue("@IngredientsID", objENT.IngredientsID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.RecipeMasterDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.RecipeMasterDetail>(sdr);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateRecipeMasterDetailByID(string IngredientsID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [RecipeMasterDetail] WHERE IngredientsID = '" + IngredientsID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateRecipeMasterDetailByName(string ModifierName)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [RecipeMasterDetail] WHERE Name = '" + ModifierName + "'";
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
