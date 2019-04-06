using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class OrderWiseModifier
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteOrderWiseModifier(ENT.OrderWiseModifier objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteOrderWiseModifier";
                sqlCMD.Parameters.AddWithValue("@ModifierID", objENT.ModifierID);
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@TransactionID", objENT.TransactionID);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@IngredientsID", objENT.IngredientsID);
                sqlCMD.Parameters.AddWithValue("@Quantity", objENT.Quantity);
                sqlCMD.Parameters.AddWithValue("@Price", objENT.Price);
                sqlCMD.Parameters.AddWithValue("@Total", objENT.Total);
                sqlCMD.Parameters.AddWithValue("@ModifierOption", objENT.ModifierOption);
                sqlCMD.Parameters.AddWithValue("@RUserID", objENT.RUserID);
                sqlCMD.Parameters.AddWithValue("@RUserType", objENT.RUserType);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCMD.Connection.Close();
            }
            return row;
        }

        public List<ENT.OrderWiseModifier> GetOrderWiseModifier(ENT.OrderWiseModifier objENT)
        {
            List<ENT.OrderWiseModifier> lstENT = new List<ENT.OrderWiseModifier>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOrderWiseModifier";
                sqlCMD.Parameters.AddWithValue("@ModifierID", objENT.ModifierID);
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@TransactionID", objENT.TransactionID);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@IngredientsID", objENT.IngredientsID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.OrderWiseModifier>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCMD.Connection.Close();
            }
            return lstENT;
        }

        public List<ENT.OrderIngredient> GetOrderWiseModifierForUpStream(ENT.OrderIngredient objENT)
        {
            List<ENT.OrderIngredient> lstENT = new List<ENT.OrderIngredient>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOrderWiseModifier";
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.OrderIngredient>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCMD.Connection.Close();
            }
            return lstENT;
        }

        public int getDuplicateOrderWiseModifierByID(string TransactionID, string IngredientsID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT * FROM [OrderWiseModifier] WHERE TransactionID = '" + TransactionID + "' AND IngredientsID='" + IngredientsID + "'";
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
