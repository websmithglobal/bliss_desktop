using System;
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

                // Send Acknowledgement Process From Receive Json
                if (objJson != null)
                {
                    //Receive acknowledgement for message
                    ENT.ReceiveMessageData objReceiveMessage = new ENT.ReceiveMessageData { msg_guid = new Guid(objJson.ackGuid), client_ip = objJson.ipAddress, message = stringJson, send_acknowledge_status = 0 };
                    WriteLog($"ACK SYNC CODE: {objJson.syncCode}");
                    if (objJson.syncCode == ENT.SyncCode.C_SEND_MESSAGE_ACKNOWLEDGEMENT)
                    {
                        ENT.SEND_MESSAGE_ACKNOWLEDGEMENT_605 objGetAck = JsonConvert.DeserializeObject<ENT.SEND_MESSAGE_ACKNOWLEDGEMENT_605>(stringJson);
                        if (objGetAck != null)
                        {
                            using (DAL.SendMessageData objDAL = new DAL.SendMessageData())
                            {
                                WriteLog($"Receive Ack ID {objGetAck.Object.guid} and IP {objGetAck.Object.receiverClientIp}");

                                // this function update ack status 0 to 1
                                _ = objDAL.InsertUpdateDeleteSendMessageData(objENT: new ENT.SendMessageData { msg_guid = objGetAck.Object.guid, client_ip = objGetAck.Object.receiverClientIp, message_acknowledge_status = 1, Mode = "STATUS_ACK" });

                                // this function delete all the record which ack status is 1 because it is non-use record
                                _ = objDAL.InsertUpdateDeleteSendMessageData(objENT: new ENT.SendMessageData { Mode = "DELETE" });
                            }
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
                                    _ = objDAL.InsertUpdateDeleteReceiveMessageData(objENT: new ENT.ReceiveMessageData
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
                                    objReceiveMessage.Mode = "ADD";
                                    _ = objDAL.InsertUpdateDeleteReceiveMessageData(objENT: objReceiveMessage);
                                }

                                //At this time only send message to device for Acknowledgement. Don't store data in db table.
                                SendMessageAcknowledgement(objReceiveMessage.client_ip, objReceiveMessage.msg_guid.ToString());

                                // change ReceiveMessageData table field of send_acknowledge_status 0 to 1
                                using (DAL.ReceiveMessageData objDAL = new DAL.ReceiveMessageData())
                                {
                                    _ = objDAL.InsertUpdateDeleteReceiveMessageData(objENT: new ENT.ReceiveMessageData
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
                        _ = objDAL.InsertUpdateDeleteSyncMaster(objENT: new ENT.SyncMasterSave
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
                    // This code is used for add device when client send add device json
                    case ENT.SyncCode.C_ADD_DEVICE:
                        GetJson = JsonConvert.SerializeObject(objJson);
                        ENT.ADD_DEVICE_601 objAdd = JsonConvert.DeserializeObject<ENT.ADD_DEVICE_601>(GetJson);
                        foreach (ENT.AddDevice item in objAdd.Object.addDevices)
                        {
                            // don't save it self using self ip
                            if (item.ip.Trim() != GlobalVariable.getSystemIP())
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
                                    if (obj.getDuplicateDeviceByIP(objENT) <= 0)
                                    {
                                        objENT.Mode = "ADD";
                                        _ = obj.InsertUpdateDeleteDeviceMaster(objENT);
                                    }
                                    else
                                    {
                                        objENT.Mode = "UPDATE";
                                        _ = obj.InsertUpdateDeleteDeviceMaster(objENT);
                                    }
                                }
                            }
                        }
                        SendConnectedDeviceToClient();
                        SendMessageAcknowledgement(objJson.ipAddress, objJson.ackGuid);
                        break;
                    // This code is used for remove existing device when client send remove device json
                    case ENT.SyncCode.C_REMOVE_DEVICE:
                        GetJson = JsonConvert.SerializeObject(objJson);
                        ENT.REMOVE_DEVICE_602 objRemove = JsonConvert.DeserializeObject<ENT.REMOVE_DEVICE_602>(GetJson);
                        foreach (ENT.RemoveDevice RemoveItem in objRemove.Object.removeDevices)
                        {
                            using (DAL.DeviceMaster objDAL = new DAL.DeviceMaster())
                            {
                                _ = objDAL.InsertUpdateDeleteDeviceMaster(objENT: new ENT.DeviceMaster { DeviceID = new Guid(RemoveItem.id), Mode = "DELETE" });
                            }
                        }
                        SendMessageAcknowledgement(objRemove.ipAddress, objRemove.ackGuid);
                        break;
                    // This code is used for add requested device from tab on start up device
                    case ENT.SyncCode.C_ADD_DEVICE_REQUEST:
                        GetJson = JsonConvert.SerializeObject(objJson);
                        ENT.ADD_DEVICE_REQUEST_604 objRequest = JsonConvert.DeserializeObject<ENT.ADD_DEVICE_REQUEST_604>(GetJson);
                        ENT.DeviceMaster objENT604 = new ENT.DeviceMaster
                        {
                            DeviceID = Guid.NewGuid(),
                            DeviceName = "POS",
                            DeviceIP = objRequest.ipAddress.Trim(),
                            DeviceTypeID = 1,
                            DeviceStatus = (int)GlobalVariable.DeviceStatus.Disconneted
                        };
                        using (DAL.DeviceMaster obj = new DAL.DeviceMaster())
                        {
                            if (obj.getDuplicateDeviceByIP(objENT604) <= 0)
                            {
                                objENT604.Mode = "ADD";
                                _ = obj.InsertUpdateDeleteDeviceMaster(objENT604);
                            }
                            else
                            {
                                objENT604.Mode = "UPDATE";
                                _ = obj.InsertUpdateDeleteDeviceMaster(objENT604);
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

        public static void SendJsonTo(string message, string ackGuid)
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

        public static void SendJsonToAll(string message)
        {
            try
            {
                if (AsynchronousServer.runningServer)
                {
                    AsynchronousServer.Send(message, -1);
                }
            }
            catch (Exception ex)
            {
                WriteLog($"SendJsonTo => {ex.Message}");
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

        public static void SendConnectedDeviceToClient()
        {
            try
            {
                // Get all connected deivces list from database
                List<ENT.DeviceMaster> lstENT = new List<ENT.DeviceMaster>();
                using (DAL.DeviceMaster objDAL = new DAL.DeviceMaster())
                {
                    lstENT = objDAL.getDeviceMaster(objENT: new ENT.DeviceMaster { Mode = "GetByTypeID", DeviceTypeID = (int)GlobalVariable.DeviceType.POS });
                }

                List<ENT.AddDevice> lstDevice = new List<ENT.AddDevice>();
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

                ENT.AddDeviceList objDevicesList = new ENT.AddDeviceList { addDevices = lstDevice };
                ENT.ADD_DEVICE_RESPONSE_603 obj603 = new ENT.ADD_DEVICE_RESPONSE_603
                {
                    ackGuid = Guid.NewGuid().ToString(),
                    ipAddress = GlobalVariable.getSystemIP(),
                    syncCode = ENT.SyncCode.C_ADD_DEVICE_RESPONSE,
                    Object = objDevicesList
                };

                string newJson = JsonConvert.SerializeObject(obj603);

                // Send Add Device Json To All Connected devices
                SendJsonTo(newJson, obj603.ackGuid);
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
                        _ = objDAL.InsertUpdateDeleteSyncMaster(new ENT.SyncMasterSave
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

        public static void GetNewOrderDetailForSocket(string OrderID)
        {
            try
            {
                ENT.NEW_ORDER_101 objNEWORDER = new ENT.NEW_ORDER_101
                {
                    ackGuid = Guid.NewGuid().ToString(),
                    ipAddress = GlobalVariable.getSystemIP(),
                    syncCode = ENT.SyncCode.C_NEW_ORDER
                };

                ENT.SyncMaster objSyncMaster = new ENT.SyncMaster
                {
                    SyncCode = ENT.SyncCode.C_NEW_ORDER,
                    batchCode = OrderID.Trim(),
                    date = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"),
                    id = Guid.NewGuid().ToString()
                };
                objNEWORDER.syncMaster = objSyncMaster;

                List<ENT.OrderData> lstENTOrder = new List<ENT.OrderData>();
                lstENTOrder = new DAL.OrderBook().getOrderForSocket(objENT: new ENT.OrderBook { OrderID = new Guid(OrderID), Mode = "GetRecordByOrderIDForSocket" });
                for (int i = 0; i < lstENTOrder.Count; i++)
                {
                    ENT.Customer objCustomers = new DAL.CustomerMasterData().getCustomerForSocket(objENT: new ENT.CustomerMasterData { Mode = "GetRecordByIDForSocket", CustomerID = new Guid(lstENTOrder[i].customerId) });
                    lstENTOrder[i].customer = objCustomers;

                    List<ENT.ItemsList> lstItemList = new List<ENT.ItemsList>();
                    lstItemList = new DAL.Transaction().getItemListForSocket(objENT: new ENT.Transaction { Mode = "GetRecordForNewOrderSocket", OrderID = new Guid(lstENTOrder[i].orderId) });
                    lstENTOrder[i].itemsList = lstItemList;

                    for (int n = 0; n < lstItemList.Count; n++)
                    {
                        List<ENT.ComboProductDetailItem> lstCPDIList = new List<ENT.ComboProductDetailItem>();
                        lstCPDIList = new DAL.ComboProductDetail().getComboItemForSocket(objENT: new ENT.ComboProductDetail { Mode = "GetRecordByProductIDForSocket", ProductID = new Guid(lstItemList[n].itemId) });
                        lstItemList[n].comboProductDetailItems = lstCPDIList;
                    }

                    for (int n = 0; n < lstItemList.Count; n++)
                    {
                        List<ENT.Ingredients> lstIngredientsList = new List<ENT.Ingredients>();
                        //ENT.IngredientsMasterDetail objING = new ENT.IngredientsMasterDetail();
                        //objING.Mode = "GetRecordByProductIDForSocket";
                        //objING.IngredientsID = new Guid(lstItemList[n].itemId);
                        //lstIngredientsList = new DAL.IngredientsMasterDetail().getIngredientsMasterDetail();
                        lstItemList[n].ingredientses = lstIngredientsList;
                    }
                }
                objNEWORDER.Object = lstENTOrder;

                // This code is for send order json to all connected client as server
                //SendJsonTo(JsonConvert.SerializeObject(objNEWORDER), objNEWORDER.ackGuid);
                //SendJsonToAll(JsonConvert.SerializeObject(objNEWORDER));

                //This code is for send order json to single server as client
                if (AsynchronousClient.connected)
                {
                    AsynchronousClient.Send(JsonConvert.SerializeObject(objNEWORDER));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SaveSocketErrorLog(string location, Exception ex)
        {
            try
            {
                using (DAL.SocketErrorLog objDAL = new DAL.SocketErrorLog())
                {
                    objDAL.InsertUpdateDeleteSocketErrorLog(objENT: new ENT.SocketErrorLog {
                        log_error_location = location,
                        log_exception =ex.Message,
                        Mode ="ADD"
                    });
                }
            }
            catch { }
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
