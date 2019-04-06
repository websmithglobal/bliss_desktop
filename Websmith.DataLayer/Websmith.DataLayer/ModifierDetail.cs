using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class ModifierDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteModifierDetail(ENT.ModifierDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteModifierDetail";
                sqlCMD.Parameters.AddWithValue("@IngredientsID", objENT.IngredientsID);
                sqlCMD.Parameters.AddWithValue("@Name", objENT.Name);
                sqlCMD.Parameters.AddWithValue("@Qty", objENT.Qty);
                sqlCMD.Parameters.AddWithValue("@Price", objENT.Price);
                sqlCMD.Parameters.AddWithValue("@IsDefault", objENT.IsDefault);
                sqlCMD.Parameters.AddWithValue("@IsQty", objENT.IsQty);
                sqlCMD.Parameters.AddWithValue("@UnitTypeID", objENT.UnitTypeID);
                sqlCMD.Parameters.AddWithValue("@UnitType", objENT.UnitType);
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

        public List<ENT.ModifierDetail> getModifierDetail(ENT.ModifierDetail objENT)
        {
            List<ENT.ModifierDetail> lstENT = new List<ENT.ModifierDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetModifierDetail";
                sqlCMD.Parameters.AddWithValue("@IngredientsID", objENT.IngredientsID);
                sqlCMD.Parameters.AddWithValue("@ModifierCategoryDetail_Id", objENT.ModifierCategoryDetail_Id);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.ModifierDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.ModifierDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateModifierDetailByID(string IngredientsID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [ModifierDetail] WHERE IngredientsID = '" + IngredientsID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateModifierDetailByName(string ModifierName)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [ModifierDetail] WHERE Name = '" + ModifierName + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int DeleteModifierDetailForDownStream()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "DELETE FROM [ModifierDetail]";
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
