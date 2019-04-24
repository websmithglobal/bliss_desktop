using System;
using System.Text;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using DAL = Websmith.DataLayer;
using ENT = Websmith.Entity;
using System.Collections.Generic;

namespace Websmith.Bliss
{
    public partial class frmSocketServer : Form
    {
        public frmSocketServer()
        {
            InitializeComponent();
        }

        private void frmSocketServer_Load(object sender, EventArgs e)
        {
            AsynchronousServer.console = serverConsole;
            AsynchronousClient.console = this.clientConsole;
            String data = tbPort.Text.Trim();

            // Start Server
            if (!AsynchronousServer.runningServer)
            {
                AsynchronousServer.port = Int32.Parse(data);
                AsynchronousServer.StartListening();
                Send_Button.Enabled = true;
                Start_Button.Enabled = false;
                Close_Button.Enabled = false;
            }

            // Start Client
            if (!AsynchronousClient.connected)
            {
                String Addr = this.tbIpPort.Text;
                String[] split = Addr.Split(':');
                if (split.Length == 2)
                {
                    try
                    {
                        AsynchronousClient.keepConnection = true;
                        AsynchronousClient.ipAddress = IPAddress.Parse(split[0]);
                        AsynchronousClient.port = Int32.Parse(split[1]);
                        AsynchronousClient.console = this.clientConsole;
                        AsynchronousClient.StartClient();
                        btnClient.Enabled = false;
                        btnDisconnect.Enabled = true;
                    }
                    catch (System.FormatException)
                    {
                        MessageBox.Show("Invalid ip/port", "Socket", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    btnClient.Enabled = true;
                    btnDisconnect.Enabled = false;
                    MessageBox.Show("Bad adress, only allowed ip:port combination", "Socket", MessageBoxButtons.OK);
                }
            }
        }

        #region Server

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate
                    (SetControlPropertyThreadSafe),
                    new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { propertyValue });
            }
        }
        
        private void Start_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (AsynchronousServer.runningServer)
                {
                    MessageBox.Show("Server already running", "Socket", MessageBoxButtons.OK);
                }
                else
                {
                    String data = tbPort.Text.Trim();
                    if (data.Length > 0)
                    {
                        serverConsole.Text = string.Empty;
                        AsynchronousServer.port = Int32.Parse(data);
                        AsynchronousServer.StartListening();
                        Send_Button.Enabled = true;
                        Start_Button.Enabled = false;
                        Close_Button.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Bad adress, only allowed ip:port combination", "Socket", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Socket", MessageBoxButtons.OK);
            }
        }

        private void Send_Button_Click(object sender, EventArgs e)
        {
            if (AsynchronousServer.clients.Count == 0)
            {
                MessageBox.Show("No client is connected to server", "Socket", MessageBoxButtons.OK);
            }
            else
            {
                String data = this.tbMessageSend.Text; //GetOrderDetail(); 
                AsynchronousServer.Send(data, -1);
                this.tbMessageSend.Text = "";
            }
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            if (AsynchronousServer.runningServer)
            {
                AsynchronousServer.close();
                Start_Button.Enabled = true;
                Send_Button.Enabled = false;
                Close_Button.Enabled = true;
            }
            else
            {
                MessageBox.Show("You are not connected to any server", "Socket", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region Client

        private void btnClient_Click(object sender, EventArgs e)
        {
            if (AsynchronousClient.connected)
            {
                MessageBox.Show("You are already connected", "Socket", MessageBoxButtons.OK);
            }
            else
            {
                String data = this.tbIpPort.Text;
                String[] split = data.Split(':');
                if (split.Length == 2)
                {
                    try
                    {
                        AsynchronousClient.keepConnection = true;
                        AsynchronousClient.ipAddress = IPAddress.Parse(split[0]);
                        AsynchronousClient.port = Int32.Parse(split[1]);
                        AsynchronousClient.console = this.clientConsole;
                        AsynchronousClient.StartClient();
                    }
                    catch (System.FormatException)
                    {
                        MessageBox.Show("Invalid ip/port", "Socket", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Bad adress, only allowed ip:port combination", "Socket", MessageBoxButtons.OK);
                }
            }
        }

        private void btnSendClient_Click(object sender, EventArgs e)
        {
            if (AsynchronousClient.client == null)
            {
                MessageBox.Show("Client is not connected to server", "Socket", MessageBoxButtons.OK);
            }
            else
            {
                String data = this.tbClientSend.Text;  // GetOrderDetail();
                AsynchronousClient.Send(data);
                this.tbClientSend.Text = "";
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (AsynchronousClient.connected)
            {
                AsynchronousClient.keepConnection = false;
                AsynchronousClient.close();
            }
            else
            {
                if (AsynchronousClient.keepConnection)
                {
                    AsynchronousClient.keepConnection = false;
                }
                else
                {
                    MessageBox.Show("You are not connected to any server", "Socket", MessageBoxButtons.OK);
                }
            }
            btnClient.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        #endregion

        private string GetOrderDetail()
        {
            string jsondata = "";
            ENT.ManageOrder lstENTManageOrder = new ENT.ManageOrder();
            try
            {
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

                jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(lstENTManageOrder);
            }
            catch (Exception)
            {

            }
            return jsondata;
        }
    }
}
