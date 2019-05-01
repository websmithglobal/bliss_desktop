using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;
using System.Net;

namespace Websmith.Bliss
{
    public class ClientServerDataParsing
    {
        public void GetResponseJson(string stringJson)
        {
            try
            {
                WriteLog(stringJson);
                stringJson = stringJson.Replace("object", "Object");

                ENT.SyncGeneralJson objJson = JsonConvert.DeserializeObject<ENT.SyncGeneralJson>(stringJson);

                if (objJson.syncCode == ENT.SyncCode.C_ADD_DEVICE)
                {
                    if (objJson.syncMaster != null)
                    {
                        ENT.SyncMasterSave objEnt = new ENT.SyncMasterSave();
                        objEnt.Mode = "ADD";
                        objEnt.SYNC_MASTER_ID = Guid.NewGuid();
                        objEnt.SYNC_MASTER_SYNC_CODE = Convert.ToString(objJson.syncCode);
                        objEnt.SYNC_MASTER_BATCH_CODE = new Guid(objJson.syncMaster.batchCode);
                        objEnt.SYNC_MASTER_DEVICE_IP = objJson.ipAddress;
                        using (DAL.SyncMasterSave objDAL = new DAL.SyncMasterSave())
                        {
                            objDAL.InsertUpdateDeleteSyncMaster(objEnt);
                        }
                    }
                }

                string json = JsonConvert.SerializeObject(objJson);
                switch (objJson.syncCode)
                {
                    case ENT.SyncCode.C_ADD_DEVICE:
                        ENT.ADD_DEVICE_601 objAdd = JsonConvert.DeserializeObject<ENT.ADD_DEVICE_601>(json);
                        foreach (ENT.AddDevice item in objAdd.Object.addDevices)
                        {
                            ENT.DeviceMaster objENT = new ENT.DeviceMaster();
                            objENT.DeviceID = new Guid(item.guId);
                            objENT.DeviceName = item.stationname;
                            objENT.DeviceIP = item.ip;
                            objENT.DeviceTypeID = item.type;
                            objENT.DeviceStatus = item.status;

                            using (DAL.DeviceMaster obj = new DAL.DeviceMaster())
                            {
                                if (obj.getDuplicateDeviceByName(objENT) <= 0)
                                {
                                    objENT.Mode = "ADD";
                                    if (obj.InsertUpdateDeleteDeviceMaster(objENT))
                                    {
                                        // Now Acknowledgement to tab
                                    }
                                }
                                else
                                {
                                    objENT.Mode = "UPDATE";
                                    if (obj.InsertUpdateDeleteDeviceMaster(objENT))
                                    {
                                        // Now Acknowledgement to tab
                                    }
                                }
                            }
                        }

                        break;
                    case ENT.SyncCode.C_REMOVE_DEVICE:
                        ENT.REMOVE_DEVICE_602 objRemove = JsonConvert.DeserializeObject<ENT.REMOVE_DEVICE_602>(objJson.Object.ToString());
                        break;
                    case ENT.SyncCode.C_ADD_DEVICE_RESPONSE:
                        ENT.ADD_DEVICE_RESPONSE_603 objResponse = JsonConvert.DeserializeObject<ENT.ADD_DEVICE_RESPONSE_603>(objJson.Object.ToString());
                        break;
                    case ENT.SyncCode.C_ADD_DEVICE_REQUEST:
                        ENT.ADD_DEVICE_REQUEST_604 objRequest = JsonConvert.DeserializeObject<ENT.ADD_DEVICE_REQUEST_604>(objJson.Object.ToString());
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SendDeviceToTab()
        {
            try
            {
                List<ENT.DeviceMaster> lstENT = new List<ENT.DeviceMaster>();
                lstENT = new DAL.DeviceMaster().getDeviceMaster(new ENT.DeviceMaster { Mode = "GetByTypeID", DeviceTypeID = (int)GlobalVariable.DeviceType.POS });

                List<ENT.AddDevice> lstDevice = new List<ENT.AddDevice>();
                //ENT.Object objDevicesList = new ENT.Object();
                foreach (ENT.DeviceMaster item in lstENT)
                {
                    lstDevice.Add(new ENT.AddDevice
                    {
                        guId = item.DeviceID.ToString(),
                        ip = item.DeviceIP,
                        station = "POS",
                        stationname = item.DeviceName,
                        status = item.DeviceStatus,
                        type = Convert.ToInt32(item.DeviceTypeID)
                    });
                }

                //objDevicesList.addDevices = lstDevice;
                ENT.ADD_DEVICE_601 objADDDEVICE = new ENT.ADD_DEVICE_601();
                objADDDEVICE.ackGuid = Guid.NewGuid().ToString();
                objADDDEVICE.ipAddress = GlobalVariable.getSystemIP();
                objADDDEVICE.syncCode = ENT.SyncCode.C_ADD_DEVICE;
                //objADDDEVICE.Object = objDevicesList;

                ENT.SyncMaster objSyncMaster = new ENT.SyncMaster();
                objSyncMaster.SyncCode = ENT.SyncCode.C_ADD_DEVICE;
                objSyncMaster.batchCode = objADDDEVICE.ackGuid;
                objSyncMaster.date = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                objSyncMaster.id = Guid.NewGuid().ToString();
                objADDDEVICE.syncMaster = objSyncMaster;

                if (AsynchronousClient.connected)
                {
                    AsynchronousClient.Send(Newtonsoft.Json.JsonConvert.SerializeObject(objADDDEVICE));
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void WriteLog(string content)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "SocketJsonLog.txt"), true))
            {
                writer.WriteLine($"Server: {content}");
                writer.Close();
            }
        }
    }
}
