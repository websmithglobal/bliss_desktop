using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class TableMasterDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteTableMasterDetail(ENT.TableMasterDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteTableMasterDetail";
                sqlCMD.Parameters.AddWithValue("@TableID", objENT.TableID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@TableName", objENT.TableName);
                sqlCMD.Parameters.AddWithValue("@NoOfSeats", objENT.NoOfSeats);
                sqlCMD.Parameters.AddWithValue("@Location", objENT.Location);
                sqlCMD.Parameters.AddWithValue("@ClassID", objENT.ClassID);
                sqlCMD.Parameters.AddWithValue("@Sort", objENT.Sort);
                sqlCMD.Parameters.AddWithValue("@StatusID", objENT.StatusID);
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

        public List<ENT.TableMasterDetail> getTableMasterDetail(ENT.TableMasterDetail objENT)
        {
            List<ENT.TableMasterDetail> lstENT = new List<ENT.TableMasterDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetTableMasterDetail";
                sqlCMD.Parameters.AddWithValue("@TableID", objENT.TableID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@ClassID", objENT.ClassID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.TableMasterDetail>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateTableMasterDetailByID(string TableID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [TableMasterDetail] WHERE TableID = '" + TableID + "'";
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
