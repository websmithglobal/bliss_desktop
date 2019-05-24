using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Websmith.DataLayer
{
    public class DeviceMaster: IDisposable
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteDeviceMaster(ENT.DeviceMaster objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteDeviceMaster";
                sqlCMD.Parameters.AddWithValue("@DeviceID", objENT.DeviceID);
                sqlCMD.Parameters.AddWithValue("@DeviceName", objENT.DeviceName);
                sqlCMD.Parameters.AddWithValue("@DeviceIP", objENT.DeviceIP);
                sqlCMD.Parameters.AddWithValue("@DeviceTypeID", objENT.DeviceTypeID);
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

        public List<ENT.DeviceMaster> getDeviceMaster(ENT.DeviceMaster objENT)
        {
            List<ENT.DeviceMaster> lstENT = new List<ENT.DeviceMaster>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetDeviceMaster";
                sqlCMD.Parameters.AddWithValue("@DeviceID", objENT.DeviceID);
                sqlCMD.Parameters.AddWithValue("@DeviceTypeID", objENT.DeviceTypeID);
                sqlCMD.Parameters.AddWithValue("@DeviceStatus", objENT.DeviceStatus);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.DeviceMaster>(sqlCMD);
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

        public int getDuplicateDeviceByName(ENT.DeviceMaster objENT)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [DeviceMaster] WHERE (DeviceID<>'" + objENT.DeviceID + "' OR DeviceID='" + objENT.DeviceID + "') AND DeviceTypeID = " + objENT.DeviceTypeID + " AND DeviceName = '" + objENT.DeviceName + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateDeviceByIP(ENT.DeviceMaster objENT)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [DeviceMaster] WHERE DeviceTypeID = " + objENT.DeviceTypeID + " AND DeviceIP = '" + objENT.DeviceIP + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DeviceMaster()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
