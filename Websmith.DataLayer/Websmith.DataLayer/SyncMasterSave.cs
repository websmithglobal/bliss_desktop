using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class SyncMasterSave: IDisposable
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteSyncMaster(ENT.SyncMasterSave objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteSyncMaster";
                sqlCMD.Parameters.AddWithValue("@SYNC_MASTER_ID", objENT.SYNC_MASTER_ID);
                sqlCMD.Parameters.AddWithValue("@SYNC_MASTER_SYNC_CODE", objENT.SYNC_MASTER_SYNC_CODE);
                sqlCMD.Parameters.AddWithValue("@SYNC_MASTER_BATCH_CODE", objENT.SYNC_MASTER_BATCH_CODE);
                sqlCMD.Parameters.AddWithValue("@SYNC_MASTER_DEVICE_IP", objENT.SYNC_MASTER_DEVICE_IP);
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
        // ~SyncMasterSave()
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
