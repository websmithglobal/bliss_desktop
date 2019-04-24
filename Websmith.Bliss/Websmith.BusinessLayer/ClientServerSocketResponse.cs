using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.BusinessLayer
{
    public class ClientServerSocketResponse
    {
        public void GetResponseJson(string stringJson)
        {
            stringJson = stringJson.Replace("object", "Object");

            ENT.SyncResponseAdd responseAdd = JsonConvert.DeserializeObject<ENT.SyncResponseAdd>(stringJson); 

            if (responseAdd.syncCode == ENT.SyncCode.C_ADD_DEVICE)
            {
                if (responseAdd.syncMaster != null)
                {
                    ENT.SyncMasterSave objEnt = new ENT.SyncMasterSave();
                    objEnt.Mode = "ADD";
                    objEnt.SYNC_MASTER_ID = Guid.NewGuid();
                    objEnt.SYNC_MASTER_SYNC_CODE = Convert.ToString(responseAdd.syncCode);
                    objEnt.SYNC_MASTER_BATCH_CODE= new Guid(responseAdd.syncMaster.batchCode);
                    objEnt.SYNC_MASTER_DEVICE_IP = responseAdd.ipAddress;
                    using (DAL.SyncMasterSave objDAL = new DAL.SyncMasterSave())
                    {
                        objDAL.InsertUpdateDeleteSyncMaster(objEnt);
                    }
                }
            }
        }

        
    }
}
