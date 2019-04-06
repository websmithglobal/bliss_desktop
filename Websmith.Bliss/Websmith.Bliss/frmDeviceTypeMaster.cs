using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL = Websmith.DataLayer;
using ENT = Websmith.Entity;

namespace Websmith.Bliss
{
    public partial class frmDeviceTypeMaster : Form
    {
        DAL.DeviceTypeMaster objDAL = new DAL.DeviceTypeMaster();
        ENT.DeviceTypeMaster objENT = new ENT.DeviceTypeMaster();
        string strMode;

        public frmDeviceTypeMaster()
        {
            InitializeComponent();
        }

        public void getDataGrid()
        {
            try
            {
                List<ENT.DeviceTypeMaster> lstENT = new List<Entity.DeviceTypeMaster>();
                objENT.Mode = "GetAll";
                lstENT = objDAL.getDeviceTypeMaster(objENT);

                dgvItem.Rows.Clear();
                for (int i = 0; i < lstENT.Count; i++)
                {
                    dgvItem.Rows.Add();
                    dgvItem.Rows[i].Cells["DeviceTypeID"].Value = lstENT[i].DeviceTypeID.ToString();
                    dgvItem.Rows[i].Cells["DeviceType"].Value = lstENT[i].DeviceType.ToString();
                    dgvItem.Rows[i].Cells["DeviceStatus"].Value = lstENT[i].DeviceStatus.ToString();
                }
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
                txtTypeName.Text = "";
                txtTypeId.Text = "";
                getDataGrid();
                txtTypeName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDeviceTypeMaster_Load(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTypeName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Device type should not be empty.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTypeName.Focus();
                    return;
                }

                if (strMode == "ADD")
                    objENT.DeviceTypeID = Guid.NewGuid();
                else
                    objENT.DeviceTypeID = new Guid(txtTypeId.Text);

                objENT.DeviceType = txtTypeName.Text.Trim();
                objENT.DeviceStatus = Convert.ToInt32(GlobalVariable.DeviceTypeStatus.True);
                objENT.Mode = strMode;

                if (objDAL.getDuplicateDeviceTypeByName(objENT) > 0)
                {
                    MessageBox.Show("Duplicate device type found. Try another type.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTypeName.Focus();
                    return;
                }

                if (objDAL.InsertUpdateDeleteDeviceTypeMaster(objENT))
                {
                    ClearData();
                    MessageBox.Show("Device type saved successfully.", "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    txtTypeId.Text = dgvItem.Rows[dgvItem.CurrentRow.Index].Cells["DeviceTypeID"].Value.ToString();
                    txtTypeName.Text = dgvItem.Rows[dgvItem.CurrentRow.Index].Cells["DeviceType"].Value.ToString();
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
                    if (MessageBox.Show("Are you sure to delete this device type.", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        objENT.Mode = "DELETE";
                        objENT.DeviceTypeID = new Guid(dgvItem.Rows[dgvItem.CurrentRow.Index].Cells["DeviceTypeID"].Value.ToString());
                        if (objDAL.InsertUpdateDeleteDeviceTypeMaster(objENT))
                        {
                            ClearData();
                            MessageBox.Show("Device type deleted successfully.", "Device Type Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void frmDeviceTypeMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
