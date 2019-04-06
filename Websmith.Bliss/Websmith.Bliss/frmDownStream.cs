using System;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using System.Net;
using System.Data.SqlClient;
using DAL = Websmith.DataLayer;
using ENT = Websmith.Entity;
using System.Collections.Generic;

namespace Websmith.Bliss
{
    public partial class frmDownStream : Form
    {
        //string lines = "";
        ENT.LoginDetail objENTAPI = new ENT.LoginDetail();

        public frmDownStream()
        {
            InitializeComponent();
        }

        public void WriteLog(string strLog)
        {
            try
            {
                string path = Application.StartupPath + "\\SyncLog.txt";
                System.IO.StreamWriter file = new System.IO.StreamWriter(Application.StartupPath + "\\SyncLog.txt");
                file.WriteLine(strLog);
                file.Close();
                MessageBox.Show("Down Stream Process Completed", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //if (MessageBox.Show("You want to view log file of down stream.", "Down Stream", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    if (File.Exists(path))
                //    {
                //        System.Diagnostics.Process.Start(path);
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string SyncAll(ENT.LoginDetail dataObject)
        {
            string responseFromServer = "";
            try
            {
                WebRequest tRequest = WebRequest.Create("http://api.possoftwareindia.com/api/BranchAuthentication");
                tRequest.Method = "POST";
                tRequest.ContentType = "application/json";

                string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(dataObject);

                Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);

                tRequest.ContentLength = byteArray.Length;
                tRequest.ContentType = "application/json";
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                responseFromServer = tReader.ReadToEnd();
                                //response = Newtonsoft.Json.JsonConvert.DeserializeObject<ENT.LoginResponse>(responseFromFirebaseServer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseFromServer = "";
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return responseFromServer;
        }

        private void frmDownStream_Load(object sender, EventArgs e)
        {
            try
            {
                cmbSyncFrom.SelectedIndex = 0;
                ENT.LoginDetail objENTLog = new ENT.LoginDetail();
                List<ENT.LoginDetail> lstENTLog = new List<ENT.LoginDetail>();
                DAL.LoginDetail objDALLog = new DAL.LoginDetail();
                objENTLog.Mode = "GetRecordByID";
                objENTLog.BranchID = new Guid(GlobalVariable.BranchID);
                lstENTLog = objDALLog.getLoginDetail(objENTLog);
                if (lstENTLog.Count > 0)
                {
                    objENTAPI.BranchID = new Guid(GlobalVariable.BranchID);
                    objENTAPI.Username = lstENTLog[0].Username;
                    objENTAPI.Password = lstENTLog[0].Password;
                    objENTAPI.App_Version = "";
                    objENTAPI.Device_ID = "";
                    objENTAPI.IMEI_No = "";
                    objENTAPI.Login_Via = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSyncAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSyncFrom.SelectedIndex != 0)
                {
                    DAL.DownStream.EmployeeID = GlobalVariable.EmployeeID;
                    DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                    string lines = objDS.SyncAllData();
                    this.WriteLog(lines);
                    //MessageBox.Show("Sync All Process Completed.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        string lines = objDS.RestruantProfile_DS();
                        this.WriteLog(lines);
                        //MessageBox.Show("Restaurant Profile Sync Process Completed.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        string lines = objDS.Staff_DS();
                        this.WriteLog(lines);
                        //MessageBox.Show("Staff Sync Process Completed.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        string lines = objDS.Menu_DS();
                        this.WriteLog(lines);
                        //MessageBox.Show("Menu Sync Process Completed.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIngredients_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        string lines = objDS.Ingredients_DS();
                        this.WriteLog(lines);
                        //MessageBox.Show("Ingredients Sync Process Completed.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModifiers_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        string lines = objDS.Modifier_DS();
                        this.WriteLog(lines);
                        //MessageBox.Show("Modifier Sync Process Completed.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTables_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        string lines = objDS.Tables_DS();
                        this.WriteLog(lines);
                        //MessageBox.Show("Modifier Sync Process Completed.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        string lines = objDS.Customer_DS();
                        this.WriteLog(lines);
                        //MessageBox.Show("Modifier Sync Process Completed.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        string lines = objDS.Order_DS();
                        this.WriteLog(lines);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTax_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        string lines = objDS.Tax_DS();
                        this.WriteLog(lines);
                        //MessageBox.Show("Tax Sync Process Completed.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        string lines = objDS.Discount_DS();
                        this.WriteLog(lines);
                        //MessageBox.Show("Discount Sync Process Completed.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVendor_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        string lines = objDS.Vendor_DS();
                        this.WriteLog(lines);
                        //MessageBox.Show("Vendor Sync Process Completed.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecipe_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        string lines = objDS.Recipe_DS();
                        this.WriteLog(lines);
                        //MessageBox.Show("Recipe Sync Process Completed.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            try
            {
                if (objENTAPI != null)
                {
                    if (cmbSyncFrom.SelectedIndex != 0)
                    {
                        //DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                        //string lines = objDS.Class_DS();
                        //this.WriteLog(lines);
                        //MessageBox.Show("This function is temporary unavailable.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Select option sync from.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDownStream_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
