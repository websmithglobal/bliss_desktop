using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class RoleMasterDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteRoleMasterDetail(ENT.RoleMasterDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteRoleMasterDetail";
                sqlCMD.Parameters.AddWithValue("@RoleID", objENT.RoleID);
                sqlCMD.Parameters.AddWithValue("@RoleName", objENT.RoleName);
                sqlCMD.Parameters.AddWithValue("@RoleMasterDetail_Id", objENT.RoleMasterDetail_Id);
                sqlCMD.Parameters.AddWithValue("@ModuleMasterDetail_Id", objENT.ModuleMasterDetail_Id);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }
        
        public List<ENT.RoleMasterDetail> getRoleMasterDetail(ENT.RoleMasterDetail objENT)
        {
            List<ENT.RoleMasterDetail> lstENT = new List<ENT.RoleMasterDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetRoleMasterDetail";
                sqlCMD.Parameters.AddWithValue("@RoleID", objENT.RoleID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.RoleMasterDetail>(sqlCMD);

                //DataTable dt = objCRUD.getDataTable(sqlCMD);
                //lstENT = (from DataRow dr in dt.Rows
                //          select new ENT.RoleMasterDetail()
                //          {
                //              RoleID = new Guid(dr["RoleID"].ToString()),
                //              RoleName = Convert.ToString(dr["RoleName"]),
                //              RoleMasterDetail_Id = dr["RoleMasterDetail_Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["RoleMasterDetail_Id"]),
                //              ModuleMasterDetail_Id = dr["ModuleMasterDetail_Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModuleMasterDetail_Id"])
                //          }).ToList();
                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.RoleMasterDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateRoleMasterDetailByID(string RoleID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [RoleMasterDetail] WHERE RoleID = '" + RoleID + "'";
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
