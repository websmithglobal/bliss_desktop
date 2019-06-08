using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = Websmith.DataLayer;
using ENT = Websmith.Entity;

namespace Websmith.Bliss
{
    public class SyncMasterResponse
    {
        /// <summary>
        /// The Ip of device
        /// </summary>
        string ip;

        /// <summary>
        /// Constructor of SyncMasterResponse
        /// </summary>
        /// <param name="ip"></param>
        public SyncMasterResponse(string ip)
        {
            this.ip = ip;
        }

        /// <summary>
        /// Method for Send data/json according Sync Master Data
        /// </summary>
        public void sendSyncMaster()
        {
            List<ENT.SyncMaster> syncMasterList = new List<ENT.SyncMaster>();
            foreach (ENT.SyncMaster syncMaster in syncMasterList)
            {
                toSync(syncMaster.SyncCode, syncMaster.batchCode);
            }
        }

        /// <summary>
        /// Method for send Data to Connected Devices
        /// </summary>
        /// <param name="syncCode"></param>
        /// <param name="batchCode"></param>
        private void toSync(int syncCode, string batchCode)
        {
            // Add Device
            if (ENT.SyncCode.C_ADD_DEVICE == syncCode)
            {
                ENT.AddDeviceList objDevicesList = new ENT.AddDeviceList();
                List<ENT.AddDevice> allDevices = new List<ENT.AddDevice>();
                List<ENT.DeviceMaster> lstENT = new DAL.DeviceMaster().getDeviceMaster(new ENT.DeviceMaster { Mode = "GetByID", DeviceID = new Guid(batchCode) });

                foreach (ENT.DeviceMaster item in lstENT)
                {
                    allDevices.Add(new ENT.AddDevice
                    {
                        guId = item.DeviceID.ToString(),
                        ip = item.DeviceIP,
                        station = "POS",
                        stationname = item.DeviceName,
                        status = item.DeviceStatus,
                        type = Convert.ToInt32(item.DeviceTypeID)
                    });
                }
                objDevicesList.addDevices = allDevices;

                if (allDevices.Count > 0)
                {
                    ENT.ADD_DEVICE_601 objADDDEVICE = new ENT.ADD_DEVICE_601
                    {
                        ackGuid = Guid.NewGuid().ToString(),
                        ipAddress = GlobalVariable.getSystemIP(),
                        syncCode = ENT.SyncCode.C_ADD_DEVICE,
                        Object = objDevicesList,
                        syncMaster = null
                    };

                    if (AsynchronousServer.runningServer)
                    {
                        AsynchronousServer.Send(JsonConvert.SerializeObject(objADDDEVICE), -1);
                    }
                }
            }
        }
    }
}
