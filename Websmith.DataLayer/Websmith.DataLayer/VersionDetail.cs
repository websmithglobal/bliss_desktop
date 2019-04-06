using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class VersionDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();
        
        public bool InsertUpdateDeleteVersionDetail(ENT.VersionDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteVersionDetail";
                sqlCMD.Parameters.AddWithValue("@Version_Code", objENT.Version_Code);
                sqlCMD.Parameters.AddWithValue("@IsMandtory", objENT.IsMandtory);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }

        public List<ENT.VersionDetail> getVersionDetail(ENT.VersionDetail objENT)
        {
            List<ENT.VersionDetail> lstENT = new List<ENT.VersionDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetVersionDetail";
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.VersionDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.VersionDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateVersionByCode(string VersionCode)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [VersionDetail] WHERE Version_Code = '" + VersionCode + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public List<ENT.VersionDetail> getVersion(ENT.VersionDetail objENT)
        {
            List<ENT.VersionDetail> lstENTVersion = new List<ENT.VersionDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "GetCategoryMaster";
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                DataTable dt = objCRUD.getDataTable(sqlCMD);

                lstENTVersion = (from DataRow dr in dt.Rows
                                  select new ENT.VersionDetail()
                                  {
                                      Version_Code = Convert.ToString(dr["Priority"].ToString()),
                                      IsMandtory = Convert.ToBoolean(dr["IsCategory"].ToString())
                                  }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENTVersion;
        }
        
    }
}
