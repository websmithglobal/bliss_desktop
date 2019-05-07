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
        public Tuple<bool, string, int> GetResponseJson(string stringJson)
        {
            string SendJson = string.Empty;
            var result = new Tuple<bool, string, int>(false, SendJson, 0);
            try
            {
                WriteLog(stringJson);
                stringJson = stringJson.Replace("object", "Object");

                ENT.SyncGeneralJson objJson = JsonConvert.DeserializeObject<ENT.SyncGeneralJson>(stringJson);

                // Sync Master Data Save
                if (objJson.syncCode == ENT.SyncCode.C_ADD_DEVICE)
                {
                    if (objJson.syncMaster != null)
                    {
                        using (DAL.SyncMasterSave objDAL = new DAL.SyncMasterSave())
                        {
                            objDAL.InsertUpdateDeleteSyncMaster(new ENT.SyncMasterSave
                            {
                                Mode = "ADD",
                                SYNC_MASTER_ID = Guid.NewGuid(),
                                SYNC_MASTER_SYNC_CODE = Convert.ToString(objJson.syncCode),
                                SYNC_MASTER_BATCH_CODE = new Guid(objJson.syncMaster.batchCode),
                                SYNC_MASTER_DEVICE_IP = objJson.ipAddress.Trim()
                            });
                        }
                    }
                }
                string GetJson = string.Empty;
                switch (objJson.syncCode)
                {
                    case ENT.SyncCode.C_ADD_DEVICE:
                        GetJson = JsonConvert.SerializeObject(objJson);
                        ENT.ADD_DEVICE_601 objAdd = JsonConvert.DeserializeObject<ENT.ADD_DEVICE_601>(GetJson);
                        foreach (ENT.AddDevice item in objAdd.Object.addDevices)
                        {
                            ENT.DeviceMaster objENT = new ENT.DeviceMaster
                            {
                                DeviceID = new Guid(item.guId),
                                DeviceName = item.stationname,
                                DeviceIP = item.ip.Trim(),
                                DeviceTypeID = item.type,
                                DeviceStatus = item.status
                            };
                            using (DAL.DeviceMaster obj = new DAL.DeviceMaster())
                            {
                                if (obj.getDuplicateDeviceByName(objENT) <= 0)
                                {
                                    objENT.Mode = "ADD";
                                    if (obj.InsertUpdateDeleteDeviceMaster(objENT))
                                    {
                                        // Now Acknowledgement to tab
                                        ENT.SendMessageAcknowledgement ackResponse = new ENT.SendMessageAcknowledgement
                                        {
                                            msg_guid = objJson.ackGuid,
                                            client_ip = objJson.ipAddress,
                                            message_data = GetJson,
                                            message_send_status = 0,
                                            message_acknowledge_status = 0,
                                            Mode = "ADD"
                                        };

                                        using (DAL.SendMessageAcknowledgement objDAL = new DAL.SendMessageAcknowledgement())
                                        {
                                            if (objDAL.InsertUpdateDeleteSendMessageData(ackResponse))
                                            {
                                                SendJson = SendConnectedDeviceToTab();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    objENT.Mode = "UPDATE";
                                    if (obj.InsertUpdateDeleteDeviceMaster(objENT))
                                    {
                                        // Now Acknowledgement to tab
                                        ENT.SendMessageAcknowledgement ackResponse = new ENT.SendMessageAcknowledgement
                                        {
                                            msg_guid = objJson.ackGuid,
                                            client_ip = objJson.ipAddress,
                                            message_data = GetJson,
                                            message_send_status = 0,
                                            message_acknowledge_status = 0,
                                            Mode = "ADD"
                                        };

                                        using (DAL.SendMessageAcknowledgement objDAL = new DAL.SendMessageAcknowledgement())
                                        {
                                            if (objDAL.InsertUpdateDeleteSendMessageData(ackResponse))
                                            {
                                                SendJson = SendConnectedDeviceToTab();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        result = new Tuple<bool, string, int>(true, SendJson, 0);
                        break;
                    case ENT.SyncCode.C_REMOVE_DEVICE:
                        GetJson = JsonConvert.SerializeObject(objJson);
                        ENT.REMOVE_DEVICE_602 objRemove = JsonConvert.DeserializeObject<ENT.REMOVE_DEVICE_602>(GetJson);
                        foreach (ENT.RemoveDevice RemoveItem in objRemove.Object.removeDevices)
                        {
                            ENT.DeviceMaster objENT = new ENT.DeviceMaster
                            {
                                DeviceID = new Guid(RemoveItem.id),
                                Mode= "DELETE"
                            };
                            using (DAL.DeviceMaster objDAL = new DAL.DeviceMaster())
                            {
                                if (objDAL.InsertUpdateDeleteDeviceMaster(objENT))
                                {
                                    // Send Acknowledgement
                                }
                            }
                        }
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
            catch (Exception)
            {
                result = new Tuple<bool, string, int>(false, SendJson, 0);
            }
            return result;
        }

        private string SendConnectedDeviceToTab()
        {
            string newJson = string.Empty;
            try
            {
                List<ENT.DeviceMaster> lstENT = new List<ENT.DeviceMaster>();
                lstENT = new DAL.DeviceMaster().getDeviceMaster(objENT: new ENT.DeviceMaster { Mode = "GetByStatus", DeviceStatus = 2, DeviceTypeID = Convert.ToInt32(GlobalVariable.DeviceType.POS) });

                List<ENT.AddDevice> lstDevice = new List<ENT.AddDevice>();
                ENT.AddDeviceList objDevicesList = new ENT.AddDeviceList();
                foreach (ENT.DeviceMaster objItem in lstENT)
                {
                    lstDevice.Add(new ENT.AddDevice
                    {
                        guId = objItem.DeviceID.ToString(),
                        ip = objItem.DeviceIP,
                        station = "POS",
                        stationname = objItem.DeviceName,
                        status = objItem.DeviceStatus,
                        type = Convert.ToInt32(objItem.DeviceTypeID)
                    });
                }

                objDevicesList.addDevices = lstDevice;
                ENT.ADD_DEVICE_RESPONSE_603 obj603 = new ENT.ADD_DEVICE_RESPONSE_603();
                obj603.ackGuid = Guid.NewGuid().ToString();
                obj603.ipAddress = GlobalVariable.getSystemIP();
                obj603.syncCode = ENT.SyncCode.C_ADD_DEVICE_RESPONSE;
                obj603.Object = objDevicesList;

                newJson = JsonConvert.SerializeObject(obj603);
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return newJson;
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
