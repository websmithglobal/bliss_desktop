using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public partial class frmPrinterMapping : Form
    {
        public frmPrinterMapping()
        {
            InitializeComponent();
        }

        #region Your Station Mapping

        private void GetSelectedPrinterForYourStation()
        {
            try
            {
                DAL.PrinterMapping objDALYour = new DAL.PrinterMapping();
                ENT.PrinterMapping objENTYour = new ENT.PrinterMapping();
                List<ENT.PrinterMapping> lstENTYour = new List<ENT.PrinterMapping>();

                objENTYour.Mode = "GetAll";
                objENTYour.PartID = 1;
                lstENTYour = objDALYour.GetPrinterMapping(objENTYour);

                for (int i = 0; i < lstENTYour.Count; i++)
                {
                    for (int j = 0; j < dgvYourStation.Rows.Count; j++)
                    {
                        if (dgvYourStation.Rows[j].Cells["YDeviceID"].Value.ToString() == lstENTYour[i].DeviceID.ToString())
                        {
                            (dgvYourStation.Rows[j].Cells["YPrinterName"] as DataGridViewComboBoxCell).Value = lstENTYour[i].PrinterID;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillGridYourStationToPrinter()
        {
            try
            {
                DAL.DeviceMaster objDALDM = new DAL.DeviceMaster();
                ENT.DeviceMaster objENTDM = new ENT.DeviceMaster();
                List<ENT.DeviceMaster> lstENTDM = new List<ENT.DeviceMaster>();

                objENTDM.Mode = "GetByTypeID";
                objENTDM.DeviceTypeID = Convert.ToInt32(ENT.DeviceMaster.DeviceTypeName.POS);
                lstENTDM = objDALDM.getDeviceMaster(objENTDM);

                dgvYourStation.Rows.Clear();
                for (int i = 0; i < lstENTDM.Count; i++)
                {
                    dgvYourStation.Rows.Add();
                    dgvYourStation.Rows[i].Cells["YDeviceID"].Value = lstENTDM[i].DeviceID;
                    dgvYourStation.Rows[i].Cells["YDeviceName"].Value = lstENTDM[i].DeviceName;
                    (dgvYourStation.Rows[i].Cells["YPrinterName"] as DataGridViewComboBoxCell).DataSource = FillComboInDgvPrinter();
                    (dgvYourStation.Rows[i].Cells["YPrinterName"] as DataGridViewComboBoxCell).ValueMember = "DeviceID";
                    (dgvYourStation.Rows[i].Cells["YPrinterName"] as DataGridViewComboBoxCell).DisplayMember = "DeviceName";
                    (dgvYourStation.Rows[i].Cells["YPrinterName"] as DataGridViewComboBoxCell).Value = new Guid("00000000-0000-0000-0000-000000000000");
                }
                GetSelectedPrinterForYourStation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveYourStation_Click(object sender, EventArgs e)
        {
            try
            {
                bool Result = false;
                DAL.PrinterMapping objDALPM = new DAL.PrinterMapping();
                ENT.PrinterMapping objENTPM = new ENT.PrinterMapping();
                for (int i = 0; i < dgvYourStation.Rows.Count; i++)
                {
                    if (Convert.ToString(dgvYourStation.Rows[i].Cells["YPrinterName"].Value) != "00000000-0000-0000-0000-000000000000")
                    {
                        if (dgvYourStation.Rows[i].Cells["YPrinterName"].Value == null)
                        {
                            continue;
                        }
                        objENTPM.PrinterMappingID = Guid.NewGuid();
                        objENTPM.DeviceID = new Guid(dgvYourStation.Rows[i].Cells["YDeviceID"].Value.ToString());
                        objENTPM.PrinterID = new Guid(dgvYourStation.Rows[i].Cells["YPrinterName"].Value.ToString());
                        objENTPM.PartID = 1;
                        if (objDALPM.getDuplicatePrinterMapping(objENTPM) > 0)
                        {
                            objENTPM.Mode = "UPDATE";
                            Result = objDALPM.InsertUpdateDeletePrinterMapping(objENTPM);
                        }
                        else
                        {
                            objENTPM.Mode = "ADD";
                            Result = objDALPM.InsertUpdateDeletePrinterMapping(objENTPM);
                        }
                    }
                    else
                    {
                        objENTPM.DeviceID = new Guid(dgvYourStation.Rows[i].Cells["YDeviceID"].Value.ToString());
                        objENTPM.PartID = 1;
                        objENTPM.Mode = "DELETE";
                        Result = objDALPM.InsertUpdateDeletePrinterMapping(objENTPM);
                    }
                }
                if (Result)
                {
                    MessageBox.Show("Saved Successfully", "Routing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Other Station Mapping

        private void GetSelectedPrinterForOtherStation()
        {
            try
            {
                DAL.PrinterMapping objDALOther = new DAL.PrinterMapping();
                ENT.PrinterMapping objENTOther = new ENT.PrinterMapping();
                List<ENT.PrinterMapping> lstENTOther = new List<ENT.PrinterMapping>();

                objENTOther.Mode = "GetAll";
                objENTOther.PartID = 2;
                lstENTOther = objDALOther.GetPrinterMapping(objENTOther);

                for (int i = 0; i < lstENTOther.Count; i++)
                {
                    for (int j = 0; j < dgvOtherStation.Rows.Count; j++)
                    {
                        if (dgvOtherStation.Rows[j].Cells["ODeviceID"].Value.ToString() == lstENTOther[i].DeviceID.ToString())
                        {
                            (dgvOtherStation.Rows[j].Cells["OPrinterName"] as DataGridViewComboBoxCell).Value = lstENTOther[i].PrinterID;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillGridOtherStationToPrinter()
        {
            try
            {
                DAL.DeviceMaster objDALDM = new DAL.DeviceMaster();
                ENT.DeviceMaster objENTDM = new ENT.DeviceMaster();
                List<ENT.DeviceMaster> lstENTDM = new List<ENT.DeviceMaster>();

                objENTDM.Mode = "GetByTypeID";
                objENTDM.DeviceTypeID = Convert.ToInt32(ENT.DeviceMaster.DeviceTypeName.KDS);
                lstENTDM = objDALDM.getDeviceMaster(objENTDM);

                dgvOtherStation.Rows.Clear();
                for (int i = 0; i < lstENTDM.Count; i++)
                {
                    dgvOtherStation.Rows.Add();
                    dgvOtherStation.Rows[i].Cells["ODeviceID"].Value = lstENTDM[i].DeviceID;
                    dgvOtherStation.Rows[i].Cells["ODeviceName"].Value = lstENTDM[i].DeviceName;
                    (dgvOtherStation.Rows[i].Cells["OPrinterName"] as DataGridViewComboBoxCell).DataSource = FillComboInDgvPrinter();
                    (dgvOtherStation.Rows[i].Cells["OPrinterName"] as DataGridViewComboBoxCell).ValueMember = "DeviceID";
                    (dgvOtherStation.Rows[i].Cells["OPrinterName"] as DataGridViewComboBoxCell).DisplayMember = "DeviceName";
                    (dgvOtherStation.Rows[i].Cells["OPrinterName"] as DataGridViewComboBoxCell).Value = new Guid("00000000-0000-0000-0000-000000000000");
                }
                GetSelectedPrinterForOtherStation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveOtherStation_Click(object sender, EventArgs e)
        {
            try
            {
                bool Result = false;
                DAL.PrinterMapping objDALPM = new DAL.PrinterMapping();
                ENT.PrinterMapping objENTPM = new ENT.PrinterMapping();
                for (int i = 0; i < dgvOtherStation.Rows.Count; i++)
                {
                    if (Convert.ToString(dgvOtherStation.Rows[i].Cells["OPrinterName"].Value) != "00000000-0000-0000-0000-000000000000")
                    {
                        if (dgvOtherStation.Rows[i].Cells["OPrinterName"].Value == null)
                        {
                            continue;
                        }
                        objENTPM.PrinterMappingID = Guid.NewGuid();
                        objENTPM.DeviceID = new Guid(dgvOtherStation.Rows[i].Cells["ODeviceID"].Value.ToString());
                        objENTPM.PrinterID = new Guid(dgvOtherStation.Rows[i].Cells["OPrinterName"].Value.ToString());
                        objENTPM.PartID = 2;
                        if (objDALPM.getDuplicatePrinterMapping(objENTPM) > 0)
                        {
                            objENTPM.Mode = "UPDATE";
                            Result = objDALPM.InsertUpdateDeletePrinterMapping(objENTPM);
                        }
                        else
                        {
                            objENTPM.Mode = "ADD";
                            Result = objDALPM.InsertUpdateDeletePrinterMapping(objENTPM);
                        }
                    }
                    else
                    {
                        objENTPM.DeviceID = new Guid(dgvOtherStation.Rows[i].Cells["ODeviceID"].Value.ToString());
                        objENTPM.PartID = 2;
                        objENTPM.Mode = "DELETE";
                        Result = objDALPM.InsertUpdateDeletePrinterMapping(objENTPM);
                    }
                }
                if (Result)
                {
                    MessageBox.Show("Saved Successfully", "Routing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private List<ENT.DeviceMaster> FillComboInDgvPrinter()
        {
            List<ENT.DeviceMaster> lstENTDiv = new List<ENT.DeviceMaster>();
            try
            {
                DAL.DeviceMaster objDALDiv = new DAL.DeviceMaster();
                ENT.DeviceMaster objENTDiv = new ENT.DeviceMaster();

                objENTDiv.Mode = "GetByTypeID";
                objENTDiv.DeviceTypeID = Convert.ToInt32(ENT.DeviceMaster.DeviceTypeName.PRINTER);
                lstENTDiv = objDALDiv.getDeviceMaster(objENTDiv);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lstENTDiv.Add(new ENT.DeviceMaster { DeviceID = new Guid("00000000-0000-0000-0000-000000000000"), DeviceName = "-Select-" });
            return lstENTDiv.OrderBy(ent => ent.DeviceID).ToList(); ;
        }

        private void frmPrinterMapping_Load(object sender, EventArgs e)
        {
            try
            {
                FillGridYourStationToPrinter();
                FillGridOtherStationToPrinter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Routing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrinterMapping_KeyDown(object sender, KeyEventArgs e)
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
