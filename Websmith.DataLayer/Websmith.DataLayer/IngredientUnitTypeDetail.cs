using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class IngredientUnitTypeDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteIngredientUnitTypeDetail(ENT.IngredientUnitTypeDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteIngredientUnitTypeDetail";
                sqlCMD.Parameters.AddWithValue("@UnitTypeID", objENT.UnitTypeID);
                sqlCMD.Parameters.AddWithValue("@UnitType", objENT.UnitType);
                sqlCMD.Parameters.AddWithValue("@Qty", objENT.Qty);
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
        
        public List<ENT.IngredientUnitTypeDetail> getIngredientUnitTypeDetail(ENT.IngredientUnitTypeDetail objENT)
        {
            List<ENT.IngredientUnitTypeDetail> lstENT = new List<ENT.IngredientUnitTypeDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetIngredientUnitTypeDetail";
                sqlCMD.Parameters.AddWithValue("@UnitTypeID", objENT.UnitTypeID);
                sqlCMD.Parameters.AddWithValue("@IngredientsMasterDetail_Id", objENT.IngredientsMasterDetail_Id);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.IngredientUnitTypeDetail>(sqlCMD);
                
                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.IngredientUnitTypeDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateIngredientUnitTypeByID(string UnitTypeID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [IngredientUnitTypeDetail] WHERE UnitTypeID = '" + UnitTypeID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateIngredientUnitTypeByName(string UnitType)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT * FROM [IngredientUnitTypeDetail] WHERE UnitType = '" + UnitType + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int deleteIngredientUnitTypeDetail()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "DELETE FROM [IngredientUnitTypeDetail]";
                sqlCMD.Connection = GetConnection.GetDBConnection();
                duplicateCount = sqlCMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }
    }
}
