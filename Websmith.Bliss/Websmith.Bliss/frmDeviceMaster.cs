using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL = Websmith.DataLayer;
using ENT = Websmith.Entity;
using Newtonsoft.Json;

namespace Websmith.Bliss
{
    public partial class frmDeviceMaster : Form
    {
        DAL.DeviceMaster objDAL = new DAL.DeviceMaster();
        ENT.DeviceMaster objENT = new ENT.DeviceMaster();

        string strMode;

        public frmDeviceMaster()
        {
            InitializeComponent();
        }

        public void getDataGrid()
        {
            try
            {
                List<ENT.DeviceMaster> lstENT = new List<ENT.DeviceMaster>();
                objENT.Mode = "GetAll";
                lstENT = objDAL.getDeviceMaster(objENT);

                dgvItem.Rows.Clear();
                for (int i = 0; i < lstENT.Count; i++)
                {
                    dgvItem.Rows.Add();
                    dgvItem.Rows[i].Cells["DeviceID"].Value = lstENT[i].DeviceID.ToString();
                    dgvItem.Rows[i].Cells["DeviceName"].Value = lstENT[i].DeviceName.ToString();
                    dgvItem.Rows[i].Cells["DeviceIP"].Value = lstENT[i].DeviceIP.ToString();
                    dgvItem.Rows[i].Cells["DeviceType"].Value = lstENT[i].DeviceType.ToString();
                    dgvItem.Rows[i].Cells["DeviceTypeID"].Value = lstENT[i].DeviceTypeID.ToString();
                    dgvItem.Rows[i].Cells["DeviceStatus"].Value = lstENT[i].DeviceStatusText.ToString();
                    if(lstENT[i].DeviceStatusText.Contains("Connected"))
                        dgvItem.Rows[i].Cells["DeviceStatus"].Style.ForeColor = Color.Green;
                    else
                        dgvItem.Rows[i].Cells["DeviceStatus"].Style.ForeColor = Color.Red;

                    dgvItem.Rows[i].Cells["DeviceLastSync"].Value = lstENT[i].DeviceLastSync;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillCombo()
        {
            try
            {
                List<KeyValuePair<string, string>> lstDeviceType = new List<KeyValuePair<string, string>>();
                Array DeviceType = Enum.GetValues(typeof(ENT.DeviceMaster.DeviceTypeName));
                foreach (var dType in DeviceType)
                {
                    lstDeviceType.Add(new KeyValuePair<string, string>(dType.ToString(), ((int)dType).ToString()));
                }
                cmbDeviceType.DataSource = lstDeviceType;
                cmbDeviceType.DisplayMember = "Key";
                cmbDeviceType.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearData()
        {
            try
            {
                strMode = "ADD";
                txtDeviceName.Text = "";
                txtDeviceIP.Text = "";
                txtDeviceId.Text = "";
                FillCombo();
                getDataGrid();
                cmbDeviceType.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendDeviceToTab()
        {
            try
            {
                List<ENT.DeviceMaster> lstENT = new List<ENT.DeviceMaster>();
                lstENT = new DAL.DeviceMaster().getDeviceMaster(new ENT.DeviceMaster { Mode= "GetByTypeID", DeviceTypeID = (int)GlobalVariable.DeviceType.POS });

                List<ENT.AddDevice> lstDevice = new List<ENT.AddDevice>();
                ENT.AddDeviceList objDevicesList = new ENT.AddDeviceList();
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

                objDevicesList.addDevices = lstDevice;
                ENT.ADD_DEVICE_601 objADDDEVICE = new ENT.ADD_DEVICE_601
                {
                    ackGuid = Guid.NewGuid().ToString(),
                    ipAddress = GlobalVariable.getSystemIP(),
                    syncCode = ENT.SyncCode.C_ADD_DEVICE,
                    Object = objDevicesList
                };
                ENT.SyncMaster objSyncMaster = new ENT.SyncMaster
                {
                    SyncCode = ENT.SyncCode.C_ADD_DEVICE,
                    batchCode = objADDDEVICE.ackGuid,
                    date = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"),
                    id = Guid.NewGuid().ToString()
                };
                objADDDEVICE.syncMaster = objSyncMaster;

                // Send Add Device Json To All Connected devices
                string sendJson = JsonConvert.SerializeObject(objADDDEVICE);
                ClientServerDataParsing.SendJsonTo(sendJson, objADDDEVICE.ipAddress, objADDDEVICE.ackGuid);

                //if (objADDDEVICE != null)
                //{
                //    ENT.ReceiveMessageData objENTRMD = new ENT.ReceiveMessageData
                //    {
                //        msg_guid = new Guid(objADDDEVICE.ackGuid),
                //        client_ip = objADDDEVICE.ipAddress,
                //        message = JsonConvert.SerializeObject(objADDDEVICE),
                //        send_acknowledge_status = 0,
                //        Mode = "ADD"
                //    };
                //    using (DAL.ReceiveMessageData objDAL = new DAL.ReceiveMessageData())
                //    {
                //        _ = objDAL.InsertUpdateDeleteReceiveMessageData(objENTRMD);
                //    }
                //}

                //new frmOrderBook().StartSocketServerClient();

                //if (AsynchronousServer.runningServer)
                //{
                //    AsynchronousServer.Send(JsonConvert.SerializeObject(objADDDEVICE), -1);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDeviceMaster_Load(object sender, EventArgs e)
        {
            //Timer timer1 = new Timer
            //{
            //    Interval = 2000
            //};
            //timer1.Enabled = true;
            //timer1.Tick += new System.EventHandler(OnTimerEvent);
            ClearData();
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            getDataGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDeviceType.SelectedValue == null)
                {
                    MessageBox.Show("Select valid device type.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbDeviceType.Focus();
                    return;
                }
                if (txtDeviceName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Device name should not be empty.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDeviceName.Focus();
                    return;
                }
                if (txtDeviceIP.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Device IP should not be empty.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDeviceIP.Focus();
                    return;
                }

                if (strMode == "ADD")
                    objENT.DeviceID = Guid.NewGuid();
                else
                    objENT.DeviceID = new Guid(txtDeviceId.Text);

                objENT.DeviceName = txtDeviceName.Text.Trim();
                objENT.DeviceIP = txtDeviceIP.Text.Trim();
                objENT.DeviceTypeID = Convert.ToInt32(cmbDeviceType.SelectedValue);
                objENT.DeviceStatus = 1;
                objENT.Mode = strMode;

                if (objDAL.getDuplicateDeviceByName(objENT) > 0)
                {
                    MessageBox.Show("Duplicate device found. Try another type.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDeviceName.Focus();
                    return;
                }

                if (objDAL.InsertUpdateDeleteDeviceMaster(objENT))
                {
                    SendDeviceToTab();
                    ClearData();
                    MessageBox.Show("Device saved successfully.", "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Problem in save data.", "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvItem.Rows.Count > 0)
                {
                    strMode = "UPDATE";
                    cmbDeviceType.SelectedValue = dgvItem.Rows[dgvItem.CurrentRow.Index].Cells["DeviceTypeID"].Value.ToString();
                    txtDeviceName.Text = dgvItem.Rows[dgvItem.CurrentRow.Index].Cells["DeviceName"].Value.ToString();
                    txtDeviceIP.Text = dgvItem.Rows[dgvItem.CurrentRow.Index].Cells["DeviceIP"].Value.ToString();
                    txtDeviceId.Text = dgvItem.Rows[dgvItem.CurrentRow.Index].Cells["DeviceID"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvItem.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure to delete this device.", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Guid deviceid = new Guid(dgvItem.Rows[dgvItem.CurrentRow.Index].Cells["DeviceID"].Value.ToString());
                        string deviceip = dgvItem.Rows[dgvItem.CurrentRow.Index].Cells["DeviceIP"].Value.ToString();

                        objENT.Mode = "DELETE";
                        objENT.DeviceID = deviceid;

                        List<ENT.DeviceMaster> lstDelete = new DAL.DeviceMaster().getDeviceMaster(new ENT.DeviceMaster { Mode = "GetByID", DeviceID = deviceid });

                        if (objDAL.InsertUpdateDeleteDeviceMaster(objENT))
                        {
                            List<ENT.RemoveDevice> lstRD = new List<ENT.RemoveDevice>();
                            ENT.REMOVE_DEVICE_602 objRemove = new ENT.REMOVE_DEVICE_602();
                            objRemove.ackGuid = Guid.NewGuid().ToString(); ;
                            objRemove.ipAddress = GlobalVariable.getSystemIP();
                            objRemove.syncCode = ENT.SyncCode.C_REMOVE_DEVICE;
                            foreach (ENT.DeviceMaster item in lstDelete)
                            {
                                lstRD.Add(new ENT.RemoveDevice { id = item.DeviceID.ToString(), ip = item.DeviceIP});
                            }
                            ENT.RemoveDeviceList objRDList = new ENT.RemoveDeviceList { removeDevices = lstRD };
                            objRemove.Object= objRDList;

                            // Send Remove Device Json To All Connected Devices
                            string sendJson = JsonConvert.SerializeObject(objRemove);
                            ClientServerDataParsing.SendJsonTo(sendJson, objRemove.ipAddress, objRemove.ackGuid);

                            ClearData();
                            MessageBox.Show("Device deleted successfully.", "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Problem in delete data.", "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void frmDeviceMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
