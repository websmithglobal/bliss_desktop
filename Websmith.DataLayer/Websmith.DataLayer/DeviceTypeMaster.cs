using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Websmith.DataLayer
{
    public class DeviceTypeMaster
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteDeviceTypeMaster(ENT.DeviceTypeMaster objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteDeviceTypeMaster";
                sqlCMD.Parameters.AddWithValue("@DeviceTypeID", objENT.DeviceTypeID);
                sqlCMD.Parameters.AddWithValue("@DeviceType", objENT.DeviceType);
                sqlCMD.Parameters.AddWithValue("@DeviceStatus", objENT.DeviceStatus);
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

        public List<ENT.DeviceTypeMaster> getDeviceTypeMaster(ENT.DeviceTypeMaster objENT)
        {
            List<ENT.DeviceTypeMaster> lstENT = new List<ENT.DeviceTypeMaster>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetDeviceTypeMaster";
                sqlCMD.Parameters.AddWithValue("@DeviceTypeID", objENT.DeviceTypeID);
                sqlCMD.Parameters.AddWithValue("@DeviceStatus", objENT.DeviceStatus);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                
                lstENT = DBHelper.GetEntityList<ENT.DeviceTypeMaster>(sqlCMD);
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

        public int getDuplicateDeviceTypeByName(ENT.DeviceTypeMaster objENT)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [DeviceTypeMaster] WHERE DeviceTypeID<>'" + objENT.DeviceTypeID + "' AND DeviceType = '" + objENT.DeviceType + "'";
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
