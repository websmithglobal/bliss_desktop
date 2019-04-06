using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Websmith.DataLayer
{
    public class TillPayOut
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteTillPayOut(ENT.TillPayOut objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteTillPayOut";
                sqlCMD.Parameters.AddWithValue("@PayOutID", objENT.PayOutID);
                sqlCMD.Parameters.AddWithValue("@TillID", objENT.TillID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@Amount", objENT.Amount);
                sqlCMD.Parameters.AddWithValue("@Reason", objENT.Reason);
                sqlCMD.Parameters.AddWithValue("@EntryDateTime", objENT.EntryDateTime);
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

        public List<ENT.TillPayOut> GetTillPayOut(ENT.TillPayOut objENT)
        {
            List<ENT.TillPayOut> lstENT = new List<ENT.TillPayOut>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetTillPayOut";
                sqlCMD.Parameters.AddWithValue("@PayOutID", objENT.PayOutID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.TillPayOut>(sqlCMD);
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

    }
}
