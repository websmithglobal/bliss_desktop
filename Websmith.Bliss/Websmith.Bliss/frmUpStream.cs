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
    public partial class frmUpStream : Form
    {
        ENT.LoginDetail objENTAPI = new ENT.LoginDetail();

        public frmUpStream()
        {
            InitializeComponent();
        }

        public void WriteLog(string strLog)
        {
            try
            {
                MessageBox.Show(strLog,"Up Stream", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //string path = Application.StartupPath + "\\SyncLog.txt";
                //System.IO.StreamWriter file = new System.IO.StreamWriter(Application.StartupPath + "\\SyncLog.txt");
                //file.WriteLine(strLog);
                //file.Close();
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
                else
                { objENTAPI = null; }
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
                    //DAL.DownStream.EmployeeID = GlobalVariable.EmployeeID;
                    //DAL.DownStream objDS = new DAL.DownStream(objENTAPI);
                    //string lines = objDS.SyncAllData();
                    //this.WriteLog(lines);
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

        private void btnStaff_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSyncFrom.SelectedIndex != 0)
                {
                    string lines = "";
                    DAL.EmployeeMasterList objDALEmp = new DAL.EmployeeMasterList();
                    ENT.EmployeeMasterList objENTEmp = new ENT.EmployeeMasterList();
                    List<ENT.EmployeeMasterList> lstENTEmp = new List<ENT.EmployeeMasterList>();
                    objENTEmp.Mode = "GetRecordForUPStream";
                    lstENTEmp = objDALEmp.getEmployeeMasterList(objENTEmp);
                    if (lstENTEmp.Count == 0)
                    {
                        MessageBox.Show("Data not found for up stream.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DataSet ds = DAL.UpStream.UpStreamData(ENT.APIStream.US_MANAGE_STAFF, lstENTEmp);
                    foreach (DataTable dt in ds.Tables)
                    {
                        switch (dt.TableName.ToString())
                        {
                            case "VersionDetail":
                                break;
                            case "RootObject":
                                string strCode = Convert.ToString(dt.Rows[0]["Code"]);
                                if (strCode == "1")
                                {
                                    if (objDALEmp.IsUpStreamTrue() > 0)
                                    {
                                        lines = Convert.ToString(dt.Rows[0]["Message"]) + Environment.NewLine + "IsUPStream status changed successfully.";
                                    }
                                    else
                                    {
                                        lines = Convert.ToString(dt.Rows[0]["Message"]) + Environment.NewLine + "Problem in update IsUPStream status.";
                                    }
                                }
                                else if (strCode == "0")
                                {
                                    lines = Convert.ToString(dt.Rows[0]["Message"]);
                                }
                                break;
                            default:
                                lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                                break;
                        }
                    }
                    this.WriteLog(lines);
                }
                else
                {
                    MessageBox.Show("Select option sync from.", "UP Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "UP Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSyncFrom.SelectedIndex != 0)
                {
                    string lines = "";
                    DAL.CustomerMasterData objDALCustomers = new DAL.CustomerMasterData();
                    ENT.CustomerMasterData objENTCustomers = new ENT.CustomerMasterData();
                    List<ENT.CustomerMasterData> lstENTCustomers = new List<ENT.CustomerMasterData>();
                    objENTCustomers.Mode = "GetRecordForUPStream";
                    lstENTCustomers = objDALCustomers.getCustomerData(objENTCustomers);
                    if (lstENTCustomers.Count == 0)
                    {
                        MessageBox.Show("Data not found for up stream.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DataSet ds = DAL.UpStream.UpStreamData(ENT.APIStream.US_MANAGE_CUSTOMER, lstENTCustomers);
                    foreach (DataTable dt in ds.Tables)
                    {
                        switch (dt.TableName.ToString())
                        {
                            case "VersionDetail":
                                break;
                            case "RootObject":
                                string strCode = Convert.ToString(dt.Rows[0]["Code"]);
                                if (strCode == "1")
                                {
                                    if (objDALCustomers.IsUpStreamTrue() > 0)
                                    {
                                        lines = Convert.ToString(dt.Rows[0]["Message"]) + Environment.NewLine + "IsUPStream status changed successfully.";
                                    }
                                    else
                                    {
                                        lines = Convert.ToString(dt.Rows[0]["Message"]) + Environment.NewLine + "Problem in update IsUPStream status.";
                                    }
                                }
                                else if (strCode == "0")
                                {
                                    lines = Convert.ToString(dt.Rows[0]["Message"]);
                                }
                                break;
                            default:
                                lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                                break;
                        }
                    }
                    this.WriteLog(lines);
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

        private void btnCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSyncFrom.SelectedIndex != 0)
                {
                    string lines = "";
                    DAL.CategoryMaster objDALCM = new DAL.CategoryMaster();
                    ENT.CategoryMaster objENTCM = new ENT.CategoryMaster();
                    List<ENT.CategoryMaster> lstENTCM = new List<ENT.CategoryMaster>();
                    objENTCM.Mode = "GetRecordForUPStream";
                    lstENTCM = objDALCM.getCategoryMaster(objENTCM);
                    if (lstENTCM.Count == 0)
                    {
                        MessageBox.Show("Data not found for up stream.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DataSet ds = DAL.UpStream.UpStreamData(ENT.APIStream.US_MANAGE_CATEGORY, lstENTCM);
                    foreach (DataTable dt in ds.Tables)
                    {
                        switch (dt.TableName.ToString())
                        {
                            case "VersionDetail":
                                break;
                            case "RootObject":
                                string strCode = Convert.ToString(dt.Rows[0]["Code"]);
                                if (strCode == "1")
                                {
                                    if (objDALCM.IsUpStreamTrue() > 0)
                                    {
                                        lines = Convert.ToString(dt.Rows[0]["Message"]) + Environment.NewLine + "IsUPStream status changed successfully.";
                                    }
                                    else
                                    {
                                        lines = Convert.ToString(dt.Rows[0]["Message"]) + Environment.NewLine + "Problem in update IsUPStream status.";
                                    }
                                }
                                else if (strCode == "0")
                                {
                                    lines = Convert.ToString(dt.Rows[0]["Message"]);
                                }
                                break;
                            default:
                                lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                                break;
                        }
                    }
                    this.WriteLog(lines);
                }
                else
                {
                    MessageBox.Show("Select option sync from.", "UP Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "UP Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSyncFrom.SelectedIndex != 0)
                {
                    string lines = "";
                    DAL.CategoryWiseProduct objDALCWP = new DAL.CategoryWiseProduct();
                    ENT.ProductUpStream objENTCWP = new ENT.ProductUpStream();
                    List<ENT.ProductUpStream> lstENTCWP = new List<ENT.ProductUpStream>();
                    objENTCWP.Mode = "GetRecordForUPStream";
                    lstENTCWP = objDALCWP.getProductForUpStream(objENTCWP);
                    if (lstENTCWP.Count == 0)
                    {
                        MessageBox.Show("Data not found for up stream.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DataSet ds = DAL.UpStream.UpStreamData(ENT.APIStream.US_MANAGE_PRODUCT, lstENTCWP);
                    foreach (DataTable dt in ds.Tables)
                    {
                        switch (dt.TableName.ToString())
                        {
                            case "VersionDetail":
                                break;
                            case "RootObject":
                                string strCode = Convert.ToString(dt.Rows[0]["Code"]);
                                if (strCode == "1")
                                {
                                    if (objDALCWP.IsUpStreamTrue() > 0)
                                    {
                                        lines = Convert.ToString(dt.Rows[0]["Message"]) + Environment.NewLine + "IsUPStream status changed successfully.";
                                    }
                                    else
                                    {
                                        lines = Convert.ToString(dt.Rows[0]["Message"]) + Environment.NewLine + "Problem in update IsUPStream status.";
                                    }
                                }
                                else if (strCode == "0")
                                {
                                    lines = Convert.ToString(dt.Rows[0]["Message"]);
                                }
                                break;
                            default:
                                lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                                break;
                        }
                    }
                    this.WriteLog(lines);
                }
                else
                {
                    MessageBox.Show("Select option sync from.", "UP Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "UP Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVendor_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSyncFrom.SelectedIndex != 0)
                {
                    string lines = "";
                    DAL.VendorMasterData objDALVendor = new DAL.VendorMasterData();
                    ENT.VendorMasterData objENTVendor = new ENT.VendorMasterData();
                    List<ENT.VendorMasterData> lstENTVendor = new List<ENT.VendorMasterData>();
                    objENTVendor.Mode = "GetRecordForUPStream";
                    lstENTVendor = objDALVendor.getVendorMaster(objENTVendor);
                    if (lstENTVendor.Count == 0)
                    {
                        MessageBox.Show("Data not found for up stream.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DataSet ds = DAL.UpStream.UpStreamData(ENT.APIStream.US_MANAGE_VENDOR, lstENTVendor);
                    foreach (DataTable dt in ds.Tables)
                    {
                        switch (dt.TableName.ToString())
                        {
                            case "VersionDetail":
                                break;
                            case "RootObject":
                                string strCode = Convert.ToString(dt.Rows[0]["Code"]);
                                if (strCode == "1")
                                {
                                    if (objDALVendor.IsUpStreamTrue() > 0)
                                    {
                                        lines = Convert.ToString(dt.Rows[0]["Message"]) + Environment.NewLine + "IsUPStream status changed successfully.";
                                    }
                                    else
                                    {
                                        lines = Convert.ToString(dt.Rows[0]["Message"]) + Environment.NewLine + "Problem in update IsUPStream status.";
                                    }
                                }
                                else if (strCode == "0")
                                {
                                    lines = Convert.ToString(dt.Rows[0]["Message"]);
                                }
                                break;
                            default:
                                lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                                break;
                        }
                    }
                    this.WriteLog(lines);
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
   
        private void btnOrders_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSyncFrom.SelectedIndex != 0)
                {
                    string lines = "";
                    ENT.ManageOrder lstENTManageOrder = new ENT.ManageOrder();

                    #region Order Master
                    DAL.OrderBook objDALOrder = new DAL.OrderBook();
                    ENT.OrderMaster objENTOrder = new ENT.OrderMaster();
                    List<ENT.OrderMaster> lstENTOrder = new List<ENT.OrderMaster>();
                    objENTOrder.Mode = "GetRecordForUPStream";
                    lstENTOrder = objDALOrder.getOrderForUpStream(objENTOrder);
                    lstENTManageOrder.OrderMaster = lstENTOrder;
                    #endregion

                    #region Order Transaction
                    DAL.Transaction objDALTran = new DAL.Transaction();
                    ENT.OrderTransaction objENTTran = new ENT.OrderTransaction();
                    List<ENT.OrderTransaction> lstENTTran = new List<ENT.OrderTransaction>();
                    objENTTran.Mode = "GetRecordForUPStream";
                    lstENTTran = objDALTran.getOrderTransactionForUpStream(objENTTran);
                    lstENTManageOrder.OrderTransaction = lstENTTran;
                    #endregion

                    #region Order Master Tax
                    ENT.OrderMasterTax objENTTax = new ENT.OrderMasterTax();
                    List<ENT.OrderMasterTax> lstENTTax = new List<ENT.OrderMasterTax>();
                    objENTTax.Mode = "GetRecordForUPStreamTax";
                    lstENTTax = objDALOrder.getOrderForUpStreamTax(objENTTax);
                    lstENTManageOrder.OrderMasterTax = lstENTTax;
                    #endregion

                    #region Order Ingredient
                    DAL.OrderWiseModifier objDALOWM = new DAL.OrderWiseModifier();
                    ENT.OrderIngredient objENTIng = new ENT.OrderIngredient();
                    List<ENT.OrderIngredient> lstENTIng = new List<ENT.OrderIngredient>();
                    objENTIng.Mode = "GetRecordForUPStream";
                    lstENTIng = objDALOWM.GetOrderWiseModifierForUpStream(objENTIng);
                    lstENTManageOrder.OrderIngredients = lstENTIng;
                    #endregion

                    #region Order Combo
                    DAL.OrderCombo objDALOC = new DAL.OrderCombo();
                    ENT.OrderCombo objENTOC = new ENT.OrderCombo();
                    List<ENT.OrderCombo> lstENTOC = new List<ENT.OrderCombo>();
                    objENTOC.Mode = "GetRecordForUPStream";
                    lstENTOC = objDALOC.getOrderComboForUpStream(objENTOC);
                    lstENTManageOrder.OrderCombo = lstENTOC;
                    #endregion

                    #region Order Payment
                    DAL.CheckOutDetail objDALCOD = new DAL.CheckOutDetail();
                    ENT.OrderPayment objENTCOD = new ENT.OrderPayment();
                    List<ENT.OrderPayment> lstENTCOD = new List<ENT.OrderPayment>();
                    objENTCOD.Mode = "GetRecordForUPStream";
                    lstENTCOD = objDALCOD.getCheckOutDetailForUpStream(objENTCOD);
                    lstENTManageOrder.OrderPayment = lstENTCOD;
                    #endregion

                    DataSet ds = DAL.UpStream.UpStreamData(ENT.APIStream.US_MANAGE_ORDER, lstENTManageOrder);
                    foreach (DataTable dt in ds.Tables)
                    {
                        switch (dt.TableName.ToString())
                        {
                            case "VersionDetail":
                                break;
                            case "RootObject":
                                string strCode = Convert.ToString(dt.Rows[0]["Code"]);
                                if (strCode == "1")
                                {
                                    lines = "Code => " + strCode + " => " + Convert.ToString(dt.Rows[0]["Message"]);
                                }
                                else if (strCode == "0")
                                {
                                    lines = "Code => " + strCode + " => " + Convert.ToString(dt.Rows[0]["Message"]);
                                }
                                else
                                {
                                    lines = "Code => " + strCode + " => " + Convert.ToString(dt.Rows[0]["Message"]);
                                }
                                break;
                            default:
                                lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                                break;
                        }
                    }
                    this.WriteLog(lines);
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

        private void btnTill_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSyncFrom.SelectedIndex != 0)
                {
                    string lines = "";
                    DAL.TillManage objDALTill = new DAL.TillManage();
                    ENT.TillManageUpStream objENTTill = new ENT.TillManageUpStream();
                    List<ENT.TillManageUpStream> lstENTTill = new List<ENT.TillManageUpStream>();
                    objENTTill.Mode = "GetRecordForUPStream";
                    lstENTTill = objDALTill.getTillManageForUpStream(objENTTill);
                    if (lstENTTill.Count == 0)
                    {
                        MessageBox.Show("Data not found for up stream.", "Down Stream", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DataSet ds = DAL.UpStream.UpStreamData(ENT.APIStream.US_MANAGE_TILL, lstENTTill);
                    foreach (DataTable dt in ds.Tables)
                    {
                        switch (dt.TableName.ToString())
                        {
                            case "VersionDetail":
                                break;
                            case "RootObject":
                                string strCode = Convert.ToString(dt.Rows[0]["Code"]);
                                if (strCode == "1")
                                {
                                    if (objDALTill.IsUpStreamTrue() > 0)
                                    {
                                        lines = Convert.ToString(dt.Rows[0]["Message"]) + Environment.NewLine + "IsUPStream status changed successfully.";
                                    }
                                    else
                                    {
                                        lines = Convert.ToString(dt.Rows[0]["Message"]) + Environment.NewLine + "Problem in update IsUPStream status.";
                                    }
                                }
                                else if (strCode == "0")
                                {
                                    lines = Convert.ToString(dt.Rows[0]["Message"]);
                                }
                                break;
                            default:
                                lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                                break;
                        }
                    }
                    this.WriteLog(lines);
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
                if (cmbSyncFrom.SelectedIndex != 0)
                {
                    
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
        
        private void frmUpStream_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
