using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class MergeTable
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteMergeTable(ENT.MergeTable objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteMergeTable";
                sqlCMD.Parameters.AddWithValue("@ID", objENT.ID);
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@TableID", objENT.TableID);
                sqlCMD.Parameters.AddWithValue("@OldTableID", objENT.OldTableID);
                sqlCMD.Parameters.AddWithValue("@TableStatusID", objENT.TableStatusID);
                sqlCMD.Parameters.AddWithValue("@IsVacant", objENT.IsVacant);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }

        public List<ENT.MergeTable> getMergeTable(ENT.MergeTable objENT)
        {
            List<ENT.MergeTable> lstENT = new List<ENT.MergeTable>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetMergeTable";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@TableID", objENT.TableID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.MergeTable>(sqlCMD);
            }
            catch (Exception)
            {
                throw;
            }
            return lstENT;
        }

        public int GetTableCountInOrder(ENT.MergeTable objENT)
        {
            int intReturn = 0;
            try
            {
                List<ENT.MergeTable> lstENT = new List<ENT.MergeTable>();
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetMergeTable";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@TableID", objENT.TableID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.MergeTable>(sqlCMD);
                intReturn = lstENT.Count;
            }
            catch (Exception)
            {
                throw;
            }
            return intReturn;
        }

    }
}
