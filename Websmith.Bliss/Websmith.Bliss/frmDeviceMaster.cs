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
                    if (lstENT[i].DeviceStatusText.Contains("Connected"))
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
                objENT.DeviceStatus = (int)GlobalVariable.DeviceStatus.Disconneted;
                objENT.Mode = strMode;

                if (objDAL.getDuplicateDeviceByName(objENT) > 0)
                {
                    MessageBox.Show("Duplicate device found. Try another type.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDeviceName.Focus();
                    return;
                }

                if (objDAL.InsertUpdateDeleteDeviceMaster(objENT))
                {
                    new frmOrderBook().StartSocketServerClient();
                    this.SendNewAddedDeviceToClient(objENT);
                    ClientServerDataParsing.AddDeviceRequest();
                    ClearData();
                    MessageBox.Show("Device saved successfully.", "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //ClientServerDataParsing.SendConnectedDeviceToClient();
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

                        List<ENT.DeviceMaster> lstDelete = new List<ENT.DeviceMaster>();
                        using (DAL.DeviceMaster objDAL = new DAL.DeviceMaster())
                        {
                            lstDelete = objDAL.getDeviceMaster(new ENT.DeviceMaster { Mode = "GetByID", DeviceID = deviceid });
                        }

                        if (objDAL.InsertUpdateDeleteDeviceMaster(objENT))
                        {
                            ENT.REMOVE_DEVICE_602 objRemove = new ENT.REMOVE_DEVICE_602
                            {
                                ackGuid = Guid.NewGuid().ToString(),
                                ipAddress = GlobalVariable.getSystemIP(),
                                syncCode = ENT.SyncCode.C_REMOVE_DEVICE
                            };

                            // create list of remove devices
                            List<ENT.RemoveDevice> lstRD = new List<ENT.RemoveDevice>();
                            foreach (ENT.DeviceMaster item in lstDelete)
                            {
                                lstRD.Add(new ENT.RemoveDevice { id = item.DeviceID.ToString(), ip = item.DeviceIP });
                            }

                            ENT.RemoveDeviceList objRDList = new ENT.RemoveDeviceList { removeDevices = lstRD };
                            objRemove.Object = objRDList;

                            // Send Remove Device Json To All Connected Devices
                            ClientServerDataParsing.SendJsonTo(JsonConvert.SerializeObject(objRemove), objRemove.ackGuid);

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

        private void SendNewAddedDeviceToClient(ENT.DeviceMaster objNEW)
        {
            try
            {
                List<ENT.AddDevice> lstDevice = new List<ENT.AddDevice>();
                lstDevice.Add(new ENT.AddDevice
                {
                    guId = objNEW.DeviceID.ToString(),
                    ip = objNEW.DeviceIP,
                    station = "POS",
                    stationname = objNEW.DeviceName,
                    status = objNEW.DeviceStatus,
                    type = objNEW.DeviceTypeID
                });

                ENT.AddDeviceList objDevicesList = new ENT.AddDeviceList { addDevices = lstDevice };
                ENT.ADD_DEVICE_601 obj601 = new ENT.ADD_DEVICE_601
                {
                    ackGuid = Guid.NewGuid().ToString(),
                    ipAddress = GlobalVariable.getSystemIP(),
                    syncCode = ENT.SyncCode.C_ADD_DEVICE_RESPONSE,
                    Object = objDevicesList,
                    syncMaster = new ENT.SyncMaster {
                        batchCode = objNEW.DeviceID.ToString(),
                        date = DateTime.Now.ToString(),
                        id = Guid.NewGuid().ToString(),
                        SyncCode =ENT.SyncCode.C_ADD_DEVICE
                    }
                };

                string newJson = JsonConvert.SerializeObject(obj601);

                // Send Add Device Json To All Connected devices
                ClientServerDataParsing.SendJsonTo(newJson, obj601.ackGuid);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SendNewAddedDeviceToClient: {ex.Message}");
            }
        }

    }
}
