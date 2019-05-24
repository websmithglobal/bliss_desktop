﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public class ClientServerDataParsing
    {
        public static bool GetJsonFrom(string stringJson)
        {
            try
            {
                string GetJson = string.Empty;
                stringJson = stringJson.Replace("object", "Object");

                // Deserialize received json string to sync general class
                ENT.SyncGeneralJson objJson = JsonConvert.DeserializeObject<ENT.SyncGeneralJson>(stringJson);

                // Send Acknowledgement Process
                if (objJson != null)
                {
                    //Receive acknowledgement for message
                    ENT.ReceiveMessageData objReceiveMessage = new ENT.ReceiveMessageData { msg_guid = new Guid(objJson.ackGuid), client_ip = objJson.ipAddress, message = stringJson, send_acknowledge_status = 0 };
                    WriteLog($"ACK SYNC CODE: {objJson.syncCode}");
                    if (objJson.syncCode == ENT.SyncCode.C_SEND_MESSAGE_ACKNOWLEDGEMENT)
                    {
                        using (DAL.SendMessageData objDAL = new DAL.SendMessageData())
                        {
                            // this function update ack status 0 to 1
                            objDAL.InsertUpdateDeleteSendMessageData(objENT: new ENT.SendMessageData { msg_guid = objReceiveMessage.msg_guid.ToString(), client_ip = objReceiveMessage.client_ip, message_acknowledge_status = 1, Mode = "STATUS_ACK" });
                            WriteLog($"Receive Ack ID {objReceiveMessage.msg_guid.ToString()} and IP {objReceiveMessage.client_ip}");

                            // this function delete all the record which ack status is 1 because it is non-use record
                            objDAL.InsertUpdateDeleteSendMessageData(objENT: new ENT.SendMessageData { Mode = "DELETE" });
                        }
                    }
                    else
                    {
                        // For acknowledgement receive, don't send ack_msg again
                        // Received data store in db and send acknowledgement AND if same data receive than send acknowledgement only don't store data in db 
                        using (DAL.ReceiveMessageData obj = new DAL.ReceiveMessageData())
                        {
                            if (obj.getDuplicateReceiveMessageData(objReceiveMessage) > 0)
                            {
                                //At this time only send message to device for Acknowledgement. Don't store data in db table.
                                SendMessageAcknowledgement(objReceiveMessage.client_ip, objReceiveMessage.msg_guid.ToString());

                                // change ReceiveMessageData table field of send_acknowledge_status 0 to 1
                                using (DAL.ReceiveMessageData objDAL = new DAL.ReceiveMessageData())
                                {
                                    objDAL.InsertUpdateDeleteReceiveMessageData(objENT: new ENT.ReceiveMessageData
                                    {
                                        msg_guid = objReceiveMessage.msg_guid,
                                        send_acknowledge_status = 1,
                                        Mode = "STATUS_ACK"
                                    });
                                }
                            }
                            else
                            {
                                //Received data store in ReceiveMessageData table 
                                using (DAL.ReceiveMessageData objDAL = new DAL.ReceiveMessageData())
                                {
                                    objDAL.InsertUpdateDeleteReceiveMessageData(objENT: objReceiveMessage);
                                }

                                //At this time only send message to device for Acknowledgement. Don't store data in db table.
                                SendMessageAcknowledgement(objReceiveMessage.client_ip, objReceiveMessage.msg_guid.ToString());

                                // change ReceiveMessageData table field of send_acknowledge_status 0 to 1
                                using (DAL.ReceiveMessageData objDAL = new DAL.ReceiveMessageData())
                                {
                                    objDAL.InsertUpdateDeleteReceiveMessageData(objENT: new ENT.ReceiveMessageData
                                    {
                                        msg_guid = objReceiveMessage.msg_guid,
                                        send_acknowledge_status = 1,
                                        Mode = "STATUS_ACK"
                                    });
                                }
                            }
                        }
                    }
                }

                // Sync Master Data Save
                if (objJson.syncMaster != null)
                {
                    using (DAL.SyncMasterSave objDAL = new DAL.SyncMasterSave())
                    {
                        // Save Data to SyncMaster
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
                        SendMessageAcknowledgement(objJson.ipAddress, objJson.ackGuid);
                        break;
                    case ENT.SyncCode.C_REMOVE_DEVICE:
                        GetJson = JsonConvert.SerializeObject(objJson);
                        ENT.REMOVE_DEVICE_602 objRemove = JsonConvert.DeserializeObject<ENT.REMOVE_DEVICE_602>(GetJson);
                        foreach (ENT.RemoveDevice RemoveItem in objRemove.Object.removeDevices)
                        {
                            using (DAL.DeviceMaster objDAL = new DAL.DeviceMaster())
                            {
                                objDAL.InsertUpdateDeleteDeviceMaster(objENT: new ENT.DeviceMaster { DeviceID = new Guid(RemoveItem.id), Mode = "DELETE" });
                            }
                        }
                        SendMessageAcknowledgement(objRemove.ipAddress, objRemove.ackGuid);
                        break;
                    case ENT.SyncCode.C_ADD_DEVICE_RESPONSE:
                        GetJson = JsonConvert.SerializeObject(objJson);
                        ENT.ADD_DEVICE_RESPONSE_603 objResponse = JsonConvert.DeserializeObject<ENT.ADD_DEVICE_RESPONSE_603>(GetJson);
                        break;
                    case ENT.SyncCode.C_ADD_DEVICE_REQUEST:
                        GetJson = JsonConvert.SerializeObject(objJson);
                        ENT.ADD_DEVICE_REQUEST_604 objRequest = JsonConvert.DeserializeObject<ENT.ADD_DEVICE_REQUEST_604>(GetJson);
                        ENT.DeviceMaster objENT604 = new ENT.DeviceMaster
                        {
                            DeviceID = Guid.NewGuid(),
                            DeviceName = "POS",
                            DeviceIP = objRequest.ipAddress.Trim(),
                            DeviceTypeID = 1,
                            DeviceStatus = 1
                        };
                        using (DAL.DeviceMaster obj = new DAL.DeviceMaster())
                        {
                            if (obj.getDuplicateDeviceByIP(objENT604) <= 0)
                            {
                                objENT604.Mode = "ADD";
                                obj.InsertUpdateDeleteDeviceMaster(objENT604);
                            }
                        }
                        SendMessageAcknowledgement(objRequest.ipAddress, objRequest.ackGuid);
                        break;
                    default:
                        break;
                }

                // delete sync master data which is non use
                DeleteSyncMaster(objJson);
            }
            catch (Exception ex)
            {
                WriteLog($"Error GetResponseJson => {ex.Message} => {stringJson}");
            }
            return true;
        }

        public static void SendJsonTo(string message, string ip, string ackGuid)
        {
            try
            {
                if (AsynchronousServer.runningServer)
                {
                    AsynchronousServer.SendToAllClient(message, ackGuid);
                }
            }
            catch (Exception ex)
            {
                WriteLog($"SendJsonTo => {ex.Message}");
            }
        }

        private static void SendMessageAcknowledgement(string ip, string ackGuid)
        {
            try
            {
                string result = string.Empty;
                ENT.SEND_MESSAGE_ACKNOWLEDGEMENT_605 objSendAck605 = new ENT.SEND_MESSAGE_ACKNOWLEDGEMENT_605
                {
                    ackGuid = Guid.NewGuid().ToString(),
                    ipAddress = GlobalVariable.getSystemIP(),
                    syncCode = ENT.SyncCode.C_SEND_MESSAGE_ACKNOWLEDGEMENT,
                    Object = new ENT.ReceiverClient { guid = ackGuid, receiverClientIp = ip }
                };
                result = JsonConvert.SerializeObject(objSendAck605);
                WriteLog($"SendMessageAcknowledgement => {result}");
                if (AsynchronousServer.runningServer)
                {
                    AsynchronousServer.SendToSpecificClient(result, ip);
                }
            }
            catch (Exception ex)
            {
                WriteLog($"SendMessageAcknowledgement => {ex.Message}");
            }
        }

        public static void AddDeviceRequest()
        {
            try
            {
                string result = string.Empty;
                ENT.ADD_DEVICE_REQUEST_604 obj604 = new ENT.ADD_DEVICE_REQUEST_604
                {
                    ackGuid = Guid.NewGuid().ToString(),
                    ipAddress = GlobalVariable.getSystemIP(),
                    syncCode = ENT.SyncCode.C_ADD_DEVICE_REQUEST
                };
                result = JsonConvert.SerializeObject(obj604);
                if (AsynchronousServer.runningServer)
                {
                    AsynchronousServer.Send(result, -1);
                }
                WriteLog($"ADD DEVICE REQUEST => {result}");
            }
            catch (Exception ex)
            {
                WriteLog($"Error AddDeviceRequest => {ex.Message}");
            }
        }

        private static void SendConnectedDeviceToClient()
        {
            try
            {
                // Get all connected deivces list from database
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
                ENT.ADD_DEVICE_RESPONSE_603 obj603 = new ENT.ADD_DEVICE_RESPONSE_603
                {
                    ackGuid = Guid.NewGuid().ToString(),
                    ipAddress = GlobalVariable.getSystemIP(),
                    syncCode = ENT.SyncCode.C_ADD_DEVICE_RESPONSE,
                    Object = objDevicesList
                };

                string newJson = JsonConvert.SerializeObject(obj603);

                // Send Add Device Json To All Connected devices
                SendJsonTo(newJson, obj603.ipAddress, obj603.ackGuid);
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

        public static void SaveSendMessageData(string message, string ip, string ackGuid)
        {
            try
            {
                using (DAL.SendMessageData objDAL = new DAL.SendMessageData())
                {
                    if (objDAL.InsertUpdateDeleteSendMessageData(objENT: new ENT.SendMessageData { msg_guid = ackGuid, client_ip = ip, message_data = message, message_send_status = 1, message_acknowledge_status = 0, Mode = "ADD" }))
                    {
                        WriteLog($"Save Successfully => {ip} => {message}");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog($"Error SaveSendMessageData => {ip} => {ex.Message}");
            }
        }

        private static void WriteLog(string content)
        {
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "SocketJsonLog.txt"), true))
                {
                    writer.WriteLine($"{content}");
                    writer.Close();
                }
            }
            catch { }
        }
    }
}
