using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class TaxGroupDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteTaxGroupDetail(ENT.TaxGroupDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteTaxGroupDetail";
                sqlCMD.Parameters.AddWithValue("@TaxGroupID", objENT.TaxGroupID);
                sqlCMD.Parameters.AddWithValue("@Name", objENT.Name);
                sqlCMD.Parameters.AddWithValue("@Percentage", objENT.Percentage);
                sqlCMD.Parameters.AddWithValue("@ParentID", objENT.ParentID);
                sqlCMD.Parameters.AddWithValue("@PartnerID", objENT.PartnerID);
                sqlCMD.Parameters.AddWithValue("@Action", objENT.Action);
                sqlCMD.Parameters.AddWithValue("@Sign", objENT.Sign);
                sqlCMD.Parameters.AddWithValue("@Sort", objENT.Sort);
                sqlCMD.Parameters.AddWithValue("@TaxOnProductType", objENT.TaxOnProductType);
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
        
        public List<ENT.TaxGroupDetail> getTaxGroupDetail(ENT.TaxGroupDetail objENT)
        {
            List<ENT.TaxGroupDetail> lstENT = new List<ENT.TaxGroupDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetTaxGroupDetail";
                sqlCMD.Parameters.AddWithValue("@TaxGroupID", objENT.TaxGroupID);
                sqlCMD.Parameters.AddWithValue("@ParentID", objENT.ParentID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.TaxGroupDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENTTaxGroup = DBHelper.CopyDataReaderToEntity<ENT.TaxGroupDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateTaxGroupByTaxGroupID(string TaxGroupID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [TaxGroupDetail] WHERE TaxGroupID = '" + TaxGroupID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateTaxGroupByName(string Name)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [TaxGroupDetail] WHERE Name = '" + Name + "'";
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
