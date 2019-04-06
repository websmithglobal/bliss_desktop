using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class ItemChefMapping
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteItemChefMapping(ENT.ItemChefMapping objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteItemChefMapping";
                sqlCMD.Parameters.AddWithValue("@ItemChefMappingID", objENT.ItemChefMappingID);
                sqlCMD.Parameters.AddWithValue("@CategoryID", objENT.CategoryID);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
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

        public List<ENT.ItemChefMapping> GetItemChefMapping(ENT.ItemChefMapping objENT)
        {
            List<ENT.ItemChefMapping> lstENT = new List<ENT.ItemChefMapping>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetItemChefMapping";
                sqlCMD.Parameters.AddWithValue("@ItemChefMappingID", objENT.ItemChefMappingID);
                sqlCMD.Parameters.AddWithValue("@CategoryID", objENT.CategoryID);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.ItemChefMapping>(sqlCMD);
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

        public int getDuplicateItemChefMapping(ENT.ItemChefMapping objENT)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT * FROM [ItemChefMapping] WHERE CategoryID='" + objENT.CategoryID + "' AND ProductID = '" + objENT.ProductID + "'";
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
