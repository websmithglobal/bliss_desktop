using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class ShiftMasterDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteShiftMasterDetail(ENT.ShiftMasterDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteShiftMasterDetail";
                sqlCMD.Parameters.AddWithValue("@ShiftDetailsID", objENT.ShiftDetailsID);
                sqlCMD.Parameters.AddWithValue("@ShiftFromTime", objENT.ShiftFromTime);
                sqlCMD.Parameters.AddWithValue("@ShiftToTime", objENT.ShiftToTime);
                sqlCMD.Parameters.AddWithValue("@ShiftDay", objENT.ShiftDay);
                sqlCMD.Parameters.AddWithValue("@FirstSlot", objENT.FirstSlot);
                sqlCMD.Parameters.AddWithValue("@SecondSlot", objENT.SecondSlot);
                sqlCMD.Parameters.AddWithValue("@FinalSlot", objENT.FinalSlot);
                sqlCMD.Parameters.AddWithValue("@ShiftDiff", objENT.ShiftDiff);
                sqlCMD.Parameters.AddWithValue("@ShiftMaster_ID", objENT.ShiftMaster_ID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }

        public List<ENT.ShiftMasterDetail> getShiftMasterDetail(ENT.ShiftMasterDetail objENT)
        {
            List<ENT.ShiftMasterDetail> lstENT = new List<ENT.ShiftMasterDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetShiftMasterDetail";
                sqlCMD.Parameters.AddWithValue("@ShiftDetailsID", objENT.ShiftDetailsID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.ShiftMasterDetail>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.ShiftMasterDetail>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateShiftMasterDetailByID(string ShiftDetailsID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [ShiftMasterDetail] WHERE ShiftDetailsID = '" + ShiftDetailsID + "'";
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
