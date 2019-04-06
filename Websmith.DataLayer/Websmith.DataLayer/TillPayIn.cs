using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Websmith.DataLayer
{
    public class TillPayIn
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteTillPayIn(ENT.TillPayIn objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteTillPayIn";
                sqlCMD.Parameters.AddWithValue("@PayInID", objENT.PayInID);
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

        public List<ENT.TillPayIn> GetTillPayIn(ENT.TillPayIn objENT)
        {
            List<ENT.TillPayIn> lstENT = new List<ENT.TillPayIn>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetTillPayIn";
                sqlCMD.Parameters.AddWithValue("@PayInID", objENT.PayInID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.TillPayIn>(sqlCMD);
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
