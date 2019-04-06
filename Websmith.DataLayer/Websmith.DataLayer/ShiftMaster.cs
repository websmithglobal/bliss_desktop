using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class ShiftMaster
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteShiftMaster(ENT.ShiftMaster objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteShiftMaster";
                sqlCMD.Parameters.AddWithValue("@ShiftID", objENT.ShiftID);
                sqlCMD.Parameters.AddWithValue("@ShiftName", objENT.ShiftName);
                sqlCMD.Parameters.AddWithValue("@ShiftMaster_Id", objENT.ShiftMaster_Id);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }
        
        public List<ENT.ShiftMaster> getShiftMaster(ENT.ShiftMaster objENT)
        {
            List<ENT.ShiftMaster> lstENT = new List<ENT.ShiftMaster>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetShiftMaster";
                sqlCMD.Parameters.AddWithValue("@ShiftID", objENT.ShiftID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.ShiftMaster>(sqlCMD);

                //DataTable dt = objCRUD.getDataTable(sqlCMD);
                //lstENT = (from DataRow dr in dt.Rows
                //          select new ENT.ShiftMaster()
                //          {
                //              ShiftID = new Guid(dr["ShiftID"].ToString()),
                //              ShiftName = Convert.ToString(dr["ShiftName"]),
                //              ShiftMaster_Id = dr["ShiftMaster_Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ShiftMaster_Id"])
                //          }).ToList();
                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.ShiftMaster>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateShiftByID(string ShiftID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [ShiftMaster] WHERE ShiftID = '" + ShiftID + "'";
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
