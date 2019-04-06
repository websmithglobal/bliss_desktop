using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;
using System.Data;
using System.Data.SqlClient;


namespace Websmith.DataLayer
{
    public class LoginDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteLoginDetail(ENT.LoginDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteLoginDetail";
                sqlCMD.Parameters.AddWithValue("@BranchID", objENT.BranchID);
                sqlCMD.Parameters.AddWithValue("@Username", objENT.Username);
                sqlCMD.Parameters.AddWithValue("@Password", objENT.Password);
                sqlCMD.Parameters.AddWithValue("@App_Version", objENT.App_Version);
                sqlCMD.Parameters.AddWithValue("@Login_Via", objENT.Login_Via);
                sqlCMD.Parameters.AddWithValue("@Device_ID", objENT.Device_ID);
                sqlCMD.Parameters.AddWithValue("@IMEI_No", objENT.IMEI_No);
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

        public List<ENT.LoginDetail> getLoginDetail(ENT.LoginDetail objENT)
        {
            List<ENT.LoginDetail> lstENT = new List<ENT.LoginDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetLoginDetail";
                sqlCMD.Parameters.AddWithValue("@BranchID", objENT.BranchID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.LoginDetail>(sqlCMD);
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

        public int getDuplicateLoginDetailByBranchID(string BranchID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [LoginDetail] WHERE BranchID = '" + BranchID + "'";
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
