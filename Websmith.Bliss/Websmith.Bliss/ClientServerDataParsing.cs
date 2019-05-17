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
                //WriteLog(stringJson);
                stringJson = stringJson.Replace("object", "Object");

                ENT.SyncGeneralJson objJson = JsonConvert.DeserializeObject<ENT.SyncGeneralJson>(stringJson);

                // Sync Master Data Save
                if (objJson.syncMaster != null)
                {
                    using (DAL.SyncMasterSave objDAL = new DAL.SyncMasterSave())
                    {
                        objDAL.InsertUpdateDeleteSyncMaster(objENT: new ENT.SyncMasterSave
                        {
                            Mode = "ADD",
                            SYNC_MASTER_ID = Guid.NewGuid(),
                            SYNC_MASTER_SYNC_CODE = Convert.ToString(objJson.syncCode),
                            SYNC_MASTER_BATCH_CODE = new Guid(objJson.syncMaster.batchCode),
                            SYNC_MASTER_DEVICE_IP = objJson.ipAddress.Trim()
                        });
                    }
                }

                string GetJson = string.Empty;
                switch (objJson.syncCode)
                {
                    case ENT.SyncCode.C_ADD_DEVICE:
                        GetJson = JsonConvert.SerializeObject(objJson);
                        WriteLog($"ADD_DEVICE => {GetJson}");
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
                                    obj.InsertUpdateDeleteDeviceMaster(objENT);
                                }
                                else
                                {
                                    objENT.Mode = "UPDATE";
                                    obj.InsertUpdateDeleteDeviceMaster(objENT);
                                }
                            }
                        }
                        SendConnectedDeviceToClient();
                        SendJson = SendMessageAcknowledgement(GetJson, objJson.ipAddress, objJson.ackGuid);
                        result = new Tuple<bool, string, int>(true, SendJson, 0);
                        break;
                    case ENT.SyncCode.C_REMOVE_DEVICE:
                        GetJson = JsonConvert.SerializeObject(objJson);
                        WriteLog($"REMOVE_DEVICE => {GetJson}");
                        ENT.REMOVE_DEVICE_602 objRemove = JsonConvert.DeserializeObject<ENT.REMOVE_DEVICE_602>(GetJson);
                        foreach (ENT.RemoveDevice RemoveItem in objRemove.Object.removeDevices)
                        {
                            using (DAL.DeviceMaster objDAL = new DAL.DeviceMaster())
                            {
                                objDAL.InsertUpdateDeleteDeviceMaster(objENT: new ENT.DeviceMaster { DeviceID = new Guid(RemoveItem.id), Mode = "DELETE" });
                            }
                        }
                        SendJson = SendMessageAcknowledgement(GetJson, objRemove.ipAddress, objRemove.ackGuid);
                        result = new Tuple<bool, string, int>(true, SendJson, 0);
                        break;
                    case ENT.SyncCode.C_ADD_DEVICE_RESPONSE:
                        GetJson = JsonConvert.SerializeObject(objJson);
                        WriteLog($"ADD_DEVICE_RESPONSE => {GetJson}");
                        ENT.ADD_DEVICE_RESPONSE_603 objResponse = JsonConvert.DeserializeObject<ENT.ADD_DEVICE_RESPONSE_603>(GetJson);
                        break;
                    case ENT.SyncCode.C_ADD_DEVICE_REQUEST:
                        GetJson = JsonConvert.SerializeObject(objJson);
                        WriteLog($"ADD_DEVICE_REQUEST => {GetJson}");
                        ENT.ADD_DEVICE_REQUEST_604 objRequest = JsonConvert.DeserializeObject<ENT.ADD_DEVICE_REQUEST_604>(GetJson);
                        break;
                    default:
                        break;
                }

                DeleteSyncMaster(objJson);
            }
            catch (Exception ex)
            {
                WriteLog($"Error GetResponseJson => {ex.Message}");
                result = new Tuple<bool, string, int>(false, SendJson, 0);
            }
            return result;
        }

        private static string SendMessageAcknowledgement(string message, string ip, string ackGuid)
        {
            string result = string.Empty;
            try
            {
                using (DAL.SendMessageAcknowledgement objDAL = new DAL.SendMessageAcknowledgement())
                {
                    if (objDAL.InsertUpdateDeleteSendMessageData(objENT: new ENT.SendMessageAcknowledgement { msg_guid = ackGuid, client_ip = ip, message_data = message, message_send_status = 1, message_acknowledge_status = 0, Mode = "ADD" }))
                    {
                        ENT.SEND_MESSAGE_ACKNOWLEDGEMENT_605 objSendAck605 = new ENT.SEND_MESSAGE_ACKNOWLEDGEMENT_605
                        {
                            ackGuid = Guid.NewGuid().ToString(), ipAddress = GlobalVariable.getSystemIP(), syncCode = ENT.SyncCode.C_SEND_MESSAGE_ACKNOWLEDGEMENT,
                            Object = new ENT.ReceiverClient { guid = ackGuid, receiverClientIp = ip }
                        };
                        result = JsonConvert.SerializeObject(objSendAck605);
                        WriteLog($"SEND_MESSAGE_ACKNOWLEDGEMENT => {result}");
                    }
                }
            }
            catch(Exception ex)
            {
                result = string.Empty;
                WriteLog($"Error SendMessageAcknowledgement => {ex.Message}");
            }
            return result;
        }

        public static string AddDeviceRequest()
        {
            string result = string.Empty;
            try
            {
                ENT.ADD_DEVICE_REQUEST_604 obj604 = new ENT.ADD_DEVICE_REQUEST_604
                {
                    ackGuid = Guid.NewGuid().ToString(),
                    ipAddress = GlobalVariable.getSystemIP(),
                    syncCode = ENT.SyncCode.C_ADD_DEVICE_REQUEST,
                    Object = new ENT.AddDeviceList()
                };
                result = JsonConvert.SerializeObject(obj604);
                WriteLog($"ADD_DEVICE_REQUEST => {result}");
            }
            catch (Exception ex)
            {
                result = string.Empty;
                WriteLog($"Error AddDeviceRequest => {ex.Message}");
            }
            return result;
        }

        private static void SendConnectedDeviceToClient()
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
                WriteLog($"SEND_ADD_DEVICE_RESPONSE => {newJson}");
                AsynchronousServer.Send(newJson, -1);
            }
            catch (Exception ex)
            {
                WriteLog($"Error SendConnectedDeviceToClient=> {ex.Message}");
            }
        }

        private static void DeleteSyncMaster(ENT.SyncGeneralJson objJson)
        {
            try
            {
                if (objJson.syncMaster != null && objJson != null)
                {
                    using (DAL.SyncMasterSave objDAL = new DAL.SyncMasterSave())
                    {
                        objDAL.InsertUpdateDeleteSyncMaster(new ENT.SyncMasterSave
                        {
                            Mode = "DELETE",
                            SYNC_MASTER_SYNC_CODE = Convert.ToString(objJson.syncMaster.SyncCode),
                            SYNC_MASTER_BATCH_CODE = new Guid(objJson.syncMaster.batchCode),
                            SYNC_MASTER_DEVICE_IP = objJson.ipAddress.Trim()
                        });
                        WriteLog($"DELETE SYNC MASTER");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog($"Error DeleteSyncMaster => {ex.Message}");
            }
        }

        private static void WriteLog(string content)
        {
            //using (System.IO.StreamWriter writer = new System.IO.StreamWriter(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "SocketJsonLog.txt"), true))
            //{
            //    writer.WriteLine($"{content}");
            //    writer.Close();
            //}
        }
    }
}
