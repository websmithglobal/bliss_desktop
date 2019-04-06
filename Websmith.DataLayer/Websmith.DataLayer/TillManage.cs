using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Websmith.DataLayer
{
    public class TillManage
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteTillManage(ENT.TillManage objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteTillManage";
                sqlCMD.Parameters.AddWithValue("@TillID", objENT.TillID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@PayIn", objENT.PayIn);
                sqlCMD.Parameters.AddWithValue("@PayOut", objENT.PayOut);
                sqlCMD.Parameters.AddWithValue("@Cash", objENT.Cash);
                sqlCMD.Parameters.AddWithValue("@Currency5", objENT.Currency5);
                sqlCMD.Parameters.AddWithValue("@Currency10", objENT.Currency10);
                sqlCMD.Parameters.AddWithValue("@Currency20", objENT.Currency20);
                sqlCMD.Parameters.AddWithValue("@Currency50", objENT.Currency50);
                sqlCMD.Parameters.AddWithValue("@Currency100", objENT.Currency100);
                sqlCMD.Parameters.AddWithValue("@Currency200", objENT.Currency200);
                sqlCMD.Parameters.AddWithValue("@Currency500", objENT.Currency500);
                sqlCMD.Parameters.AddWithValue("@Currency1000", objENT.Currency1000);
                sqlCMD.Parameters.AddWithValue("@Currency2000", objENT.Currency2000);
                sqlCMD.Parameters.AddWithValue("@StartCash", objENT.StartCash);
                sqlCMD.Parameters.AddWithValue("@ExpectedCash", objENT.ExpectedCash);
                sqlCMD.Parameters.AddWithValue("@EndCash", objENT.EndCash);
                sqlCMD.Parameters.AddWithValue("@Difference", objENT.Difference);
                sqlCMD.Parameters.AddWithValue("@StartDateTime", objENT.StartDateTime);
                sqlCMD.Parameters.AddWithValue("@EndDateTime", objENT.EndDateTime);
                sqlCMD.Parameters.AddWithValue("@EnrtyDate", objENT.EnrtyDate);
                sqlCMD.Parameters.AddWithValue("@IsTillDone", objENT.IsTillDone);
                sqlCMD.Parameters.AddWithValue("@UpStreamStatus", objENT.UpStreamStatus);
                sqlCMD.Parameters.AddWithValue("@RUserID", objENT.RUserID);
                sqlCMD.Parameters.AddWithValue("@RuserType", objENT.RuserType);
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

        public List<ENT.TillManage> getTillManage(ENT.TillManage objENT)
        {
            List<ENT.TillManage> lstENT = new List<ENT.TillManage>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetTillManage";
                sqlCMD.Parameters.AddWithValue("@TillID", objENT.TillID);
                sqlCMD.Parameters.AddWithValue("@StartDateTime", objENT.StartDateTime);
                sqlCMD.Parameters.AddWithValue("@EndDateTime", objENT.EndDateTime);
                sqlCMD.Parameters.AddWithValue("@IsTillDone", objENT.IsTillDone);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.TillManage>(sqlCMD);
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

        public List<ENT.TillManageUpStream> getTillManageForUpStream(ENT.TillManageUpStream objENT)
        {
            List<ENT.TillManageUpStream> lstENT = new List<ENT.TillManageUpStream>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetTillManage";
                sqlCMD.Parameters.AddWithValue("@TillID", objENT.TillID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.TillManageUpStream>(sqlCMD);
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

        public int getDuplicateTillByIsTillDone()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [TillManage] WHERE IsTillDone = 0";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int IsUpStreamTrue()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "UPDATE [TillManage] SET IsUPStream = 1 WHERE IsUPStream = 0";
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
