using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public partial class frmOrderBook : Form
    {
        #region Global Variable
        ENT.CategoryMaster objENTCategoryMaster = new ENT.CategoryMaster();
        List<ENT.CategoryMaster> lstCategoryMaster = new List<ENT.CategoryMaster>();
        DAL.CategoryMaster objDALCategoryMaster = new DAL.CategoryMaster();
        List<ENT.CategoryAddress> lstCatAddr = new List<ENT.CategoryAddress>();
        bool tblVisible = false;
        bool IsQtyCellChange = false;
        bool IsRateCellChange = false;
        bool IsCmbIndexChange = false;
        bool IsRefreshMenuPanel = false;
        int itemCount;
        int increaseLength;
        int intSort;
        string strOrderPrefix = "";
        string strMode = "";
        string strModeDetail = "";
        Panel pnlMainMenu;
        Panel pnlTableAll;
        Panel pnlTableVacant;
        Panel pnlOccupied;
        Panel pnlAddressBar;

        #endregion

        #region Form Event

        public frmOrderBook()
        {
            InitializeComponent();
            dgvItem.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
        }

        private void frmOrderBook_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "DASHBOARD || " + GlobalVariable.BranchName.ToUpper() + " || " + GlobalVariable.EmployeeName.ToUpper();
                tpMenu.BackColor = Color.Black;
                tpTabAll.BackColor = Color.Black;
                tpTabVacant.BackColor = Color.Black;
                tpTabOccupied.BackColor = Color.Black;
                IsRefreshMenuPanel = true;
                this.ClearData();
                this.StartSocketServerClient();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmOrderBook_SizeChanged(object sender, EventArgs e)
        {
            //this.GridColumnWidthChange();
            this.TabControlIndexChanged();
        }

        private void frmOrderBook_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    tcMain.SelectedIndex = 0;
                }
                else if (e.KeyCode == Keys.F6)
                {
                    tcMain.SelectedIndex = 1;
                }
                else if (e.KeyCode == Keys.F7)
                {
                    tcMain.SelectedIndex = 2;
                }
                else if (e.KeyCode == Keys.F8)
                {
                    this.VisibleTaxTable();
                }
                else if (e.KeyCode == Keys.F4)
                {
                    txtSpecReqForOrder.Focus();
                }
                else if (e.KeyCode == Keys.F3)
                {
                    tcMain.SelectedIndex = 0;
                    pnlModifier.Visible = false;
                    pnlSearch.Visible = true;
                    cmbProduct.Focus();
                }
                else if (e.KeyCode == Keys.F10)
                {
                    this.btnCheckOut_Click(btnCheckOut, null);
                }
                else if (e.KeyCode == Keys.F9)
                {
                    this.btnWalkIn_Click(btnWalkIn, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OrderBook_FormClosed(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Common Function

        private string GetOrderNo()
        {
            string strOrderNo = "";
            try
            {
                DateTime nx = new DateTime(1970, 1, 1); // UNIX epoch date
                TimeSpan ts = DateTime.UtcNow - nx; // UtcNow, because timestamp is in GMT
                string tsOrder = ((int)ts.TotalSeconds).ToString();
                if (strOrderPrefix.Trim() != "")
                { strOrderNo = strOrderPrefix + "/" + GlobalVariable.EmployeeCode + "/" + tsOrder; }
                else
                { strOrderNo = GlobalVariable.EmployeeCode + "/" + tsOrder; }

            }
            catch (Exception)
            {

                throw;
            }
            return strOrderNo;
        }

        private int GetOrderStatus(string OrderID)
        {
            int result = 0;
            try
            {
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                List<ENT.OrderBook> lstENTOrder = new List<ENT.OrderBook>();
                DAL.OrderBook objDALOrder = new DAL.OrderBook();
                objENTOrder.OrderID = new Guid(OrderID);
                objENTOrder.Mode = "GetRecordByOrderID";
                lstENTOrder = objDALOrder.getOrder(objENTOrder);
                if (lstENTOrder.Count > 0)
                {
                    if (lstENTOrder[0].OrderActions == Convert.ToInt32(GlobalVariable.OrderActions.Paid))
                    {
                        result = Convert.ToInt32(GlobalVariable.OrderActions.Paid);
                    }
                    else if (lstENTOrder[0].OrderActions == Convert.ToInt32(GlobalVariable.OrderActions.Pay))
                    {
                        result = Convert.ToInt32(GlobalVariable.OrderActions.Pay);
                    }
                    else
                    {
                        result = Convert.ToInt32(GlobalVariable.OrderActions.Cancel);
                    }
                }
                else
                {
                    result = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private void GridColumnWidthChange()
        {
            try
            {
                dgvItem.Columns["ordItemName"].Width = Convert.ToInt16(dgvItem.Width * 32 / 100);
                dgvItem.Columns["ordQty"].Width = Convert.ToInt16(dgvItem.Width * 14 / 100);
                dgvItem.Columns["ordRate"].Width = Convert.ToInt16(dgvItem.Width * 12 / 100);
                dgvItem.Columns["ordTotal"].Width = Convert.ToInt16(dgvItem.Width * 14 / 100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VisibleTaxTable()
        {
            try
            {
                if (tblVisible)
                {
                    tblEditor.RowStyles[1].SizeType = SizeType.Percent;
                    tblEditor.RowStyles[1].Height = 50;

                    tblCalculate.RowStyles[1].SizeType = SizeType.Percent;
                    tblCalculate.RowStyles[1].Height = 51;
                    pnlExtraCharge.Visible = true;
                    //pnlTotalTax.Visible = true;
                    lblDeliveryCharge.Visible = true;
                    txtDeliveryCharge.Visible = true;
                    txtTotalTax.Visible = true;
                    btnCancel.Visible = false;
                    tblVisible = false;
                }
                else
                {
                    tblEditor.RowStyles[1].SizeType = SizeType.Percent;
                    tblEditor.RowStyles[1].Height = 140;
                    tblCalculate.RowStyles[1].SizeType = SizeType.Percent;
                    tblCalculate.RowStyles[1].Height = 0;
                    pnlExtraCharge.Visible = false;
                    //pnlTotalTax.Visible = false;
                    lblDeliveryCharge.Visible = false;
                    txtDeliveryCharge.Visible = false;
                    txtTotalTax.Visible = false;
                    btnCancel.Visible = true;
                    btnCancel.Width = btnWalkIn.Width;
                    tblVisible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TabControlIndexChanged()
        {
            try
            {
                if (tcMain.SelectedIndex == 0)   // Main Menu
                {
                    #region Display Category And Product

                    if (IsRefreshMenuPanel)
                    {
                        tpMenu.Controls.Remove(pnlAddressBar);
                        this.FillAllComboProductLookup();
                        this.drawAddressBar("pnlAddressBar");
                        this.getCategory("00000000-0000-0000-0000-000000000000");
                    }

                    #endregion
                }
                else if (tcMain.SelectedIndex == 1)   // Table
                {
                    #region Table Menu
                    if (tcTable.SelectedIndex == 0)     // ALL
                    {
                        this.getTableAll();
                    }
                    else if (tcTable.SelectedIndex == 1)    // Occupied
                    {
                        this.getTableOccupied();
                    }
                    else if (tcTable.SelectedIndex == 2)    // Vacant
                    {
                        this.getTableVacant();
                    }
                    else
                    {
                        MessageBox.Show("Selection not valid.");
                    }
                    #endregion
                }
                else if (tcMain.SelectedIndex == 2)   // Recent Order
                {
                    #region Recent Order
                    IsCmbIndexChange = true;
                    if (tcRecentOrder.SelectedIndex == 0)   // ALL
                    {
                        if (odCmbAll.SelectedIndex < 0)
                            this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Today), Convert.ToInt32(GlobalVariable.DeliveryType.All), txtAllSearch.Text);
                        else
                            this.ComboAllIndexChange(Convert.ToInt32(odCmbAll.SelectedIndex), Convert.ToInt32(GlobalVariable.DeliveryType.All), txtAllSearch.Text);
                    }
                    else if (tcRecentOrder.SelectedIndex == 1)  // DIN IN
                    {
                        if (odCmbDineIn.SelectedIndex < 0)
                            this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Today), Convert.ToInt32(GlobalVariable.DeliveryType.DineIn), txtDineInSearch.Text);
                        else
                            this.ComboAllIndexChange(odCmbDineIn.SelectedIndex, Convert.ToInt32(GlobalVariable.DeliveryType.DineIn), txtDineInSearch.Text);
                    }
                    else if (tcRecentOrder.SelectedIndex == 2)  // TAKE OUT
                    {
                        if (odCmbTakeOut.SelectedIndex < 0)
                            this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Today), Convert.ToInt32(GlobalVariable.DeliveryType.TakeOut), txtTakeOutSearch.Text);
                        else
                            this.ComboAllIndexChange(odCmbTakeOut.SelectedIndex, Convert.ToInt32(GlobalVariable.DeliveryType.TakeOut), txtTakeOutSearch.Text);
                    }
                    else if (tcRecentOrder.SelectedIndex == 3)  // DELIVERY
                    {
                        if (odCmbDelivery.SelectedIndex < 0)
                            this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Today), Convert.ToInt32(GlobalVariable.DeliveryType.Delivery), txtDeliverySearch.Text);
                        else
                            this.ComboAllIndexChange(odCmbDelivery.SelectedIndex, Convert.ToInt32(GlobalVariable.DeliveryType.Delivery), txtDeliverySearch.Text);
                    }
                    else if (tcRecentOrder.SelectedIndex == 4)  // CANCEL
                    {
                        if (odCmbCancel.SelectedIndex < 0)
                            this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Today), Convert.ToInt32(GlobalVariable.DeliveryType.Cancel), txtCancelSearch.Text);
                        else
                            this.ComboAllIndexChange(odCmbCancel.SelectedIndex, Convert.ToInt32(GlobalVariable.DeliveryType.Cancel), txtCancelSearch.Text);
                    }
                    else
                    {
                        MessageBox.Show("Selection not valid.");
                    }
                    #endregion
                }
                else if (tcMain.SelectedIndex == 3)   // More
                {

                }
                else
                {
                    MessageBox.Show("Selection not valid.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Image DownloadImageFromUrl(string imageUrl)
        {
            Image image = null;
            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;
                System.Net.WebResponse webResponse = webRequest.GetResponse();
                System.IO.Stream stream = webResponse.GetResponseStream();
                image = Image.FromStream(stream);
                webResponse.Close();
            }
            catch (Exception)
            {
                return null;
            }
            return image;
        }

        private void EnableButton(bool _walkIn, bool _selectCust, bool _sendKDS, bool _checkOut)
        {
            try
            {
                btnWalkIn.Enabled = _walkIn;
                btnSelectCust.Enabled = _selectCust;
                btnCancelOrder.Enabled = _sendKDS;
                btnSendToKDS.Enabled = _sendKDS;
                btnCheckOut.Enabled = _checkOut;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillAllComboRecentOrder()
        {
            try
            {
                ENT.OrderBook objEnt = new ENT.OrderBook();
                odCmbAll.DataSource = objEnt.RecentOrderComboItem;
                odCmbAll.DisplayMember = "Name";
                odCmbAll.ValueMember = "ID";

                objEnt = new ENT.OrderBook();
                odCmbDineIn.DataSource = objEnt.RecentOrderComboItem;
                odCmbDineIn.DisplayMember = "Name";
                odCmbDineIn.ValueMember = "ID";

                objEnt = new ENT.OrderBook();
                odCmbTakeOut.DataSource = objEnt.RecentOrderComboItem;
                odCmbTakeOut.DisplayMember = "Name";
                odCmbTakeOut.ValueMember = "ID";

                objEnt = new ENT.OrderBook();
                odCmbDelivery.DataSource = objEnt.RecentOrderComboItem;
                odCmbDelivery.DisplayMember = "Name";
                odCmbDelivery.ValueMember = "ID";

                objEnt = new ENT.OrderBook();
                odCmbCancel.DataSource = objEnt.RecentOrderComboItem;
                odCmbCancel.DisplayMember = "Name";
                odCmbCancel.ValueMember = "ID";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillAllComboProductLookup()
        {
            try
            {
                DAL.CategoryWiseProduct objDALProduct = new DAL.CategoryWiseProduct();
                ENT.CategoryWiseProduct objENTProduct = new ENT.CategoryWiseProduct();
                List<ENT.CategoryWiseProduct> lstENTProduct = new List<ENT.CategoryWiseProduct>();
                objENTProduct.Mode = "GetProductForLookupCombo";
                lstENTProduct = objDALProduct.getCategoryWiseProduct(objENTProduct);
                if (lstENTProduct.Count > 0)
                {
                    cmbProduct.DataSource = lstENTProduct;
                    cmbProduct.DisplayMember = "ProductName";
                    cmbProduct.ValueMember = "ProductID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcTotal()
        {
            try
            {
                txtSubTotal.Text = "0";
                txtCGST.Text = "0";
                txtSGST.Text = "0";
                txtTotalTax.Text = "0";
                txtPayAmt.Text = "0";
                decimal discTotal = 0;
                this.GetTaxFromGeneralSetting();

                int discType = txtDiscountType.Text == "" ? 0 : Convert.ToInt32(txtDiscountType.Text);
                decimal discPer = txtDiscountPer.Text == "" ? 0 : Convert.ToDecimal(txtDiscountPer.Text);
                decimal discountTotal = txtDiscount.Text == "" ? 0 : Convert.ToDecimal(txtDiscount.Text);
                decimal charge = txtExtraCharge.Text == "" ? 0 : Convert.ToDecimal(txtExtraCharge.Text);
                decimal tip = txtTip.Text == "" ? 0 : Convert.ToDecimal(txtTip.Text);
                decimal deliveryCharge = txtDeliveryCharge.Text == "" ? 0 : Convert.ToDecimal(txtDeliveryCharge.Text);

                for (int j = 0; j < dgvItem.RowCount; j++)
                {
                    dgvItem.Rows[j].Cells["ordTotal"].Value = Convert.ToDecimal(dgvItem.Rows[j].Cells["ordQty"].Value.ToString()) * Convert.ToDecimal(dgvItem.Rows[j].Cells["ordRate"].Value.ToString());
                    txtSubTotal.Text = (Convert.ToDecimal(txtSubTotal.Text) + Convert.ToDecimal(dgvItem.Rows[j].Cells["ordTotal"].Value.ToString())).ToString();
                }

                decimal subTotal = Convert.ToDecimal(txtSubTotal.Text) + charge;
                if (discType == 2)
                    discTotal = Math.Round((subTotal * discPer) / 100, 2, MidpointRounding.AwayFromZero);
                else if (discType == 1)
                    discTotal = discountTotal;
                else
                    discTotal = 0;

                decimal SubTotalDiscount = subTotal - discTotal;

                txtDiscount.Text = discTotal.ToString();
                txtExtraCharge.Text = charge.ToString();
                txtTip.Text = tip.ToString();
                txtDeliveryCharge.Text = deliveryCharge.ToString();

                txtCGST.Text = Math.Round((Convert.ToDecimal(SubTotalDiscount) * Convert.ToDecimal(txtCGSTPer.Text)) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                txtSGST.Text = Math.Round((Convert.ToDecimal(SubTotalDiscount) * Convert.ToDecimal(txtSGSTPer.Text)) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                txtTotalTax.Text = Math.Round(Convert.ToDecimal(txtCGST.Text) + Convert.ToDecimal(txtSGST.Text), 2, MidpointRounding.AwayFromZero).ToString();
                txtPayAmt.Text = Convert.ToString(SubTotalDiscount + Convert.ToDecimal(txtTotalTax.Text) + tip + deliveryCharge);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetTaxFromGeneralSetting()
        {
            try
            {
                DAL.GeneralSetting objDAL = new DAL.GeneralSetting();
                ENT.GeneralSetting objENT = new ENT.GeneralSetting();
                List<ENT.GeneralSetting> lstENT = new List<ENT.GeneralSetting>();
                objENT.Mode = "GetAll";
                lstENT = objDAL.GetGeneralSetting(objENT);
                if (lstENT.Count > 0)
                {
                    lblTax1.Text = lstENT[0].TaxLabel1; // + ":";
                    txtSGSTPer.Text = Convert.ToString(lstENT[0].TaxPercentage1);
                    lblTax2.Text = lstENT[0].TaxLabel2; // + ":";
                    txtCGSTPer.Text = Convert.ToString(lstENT[0].TaxPercentage2);
                    strOrderPrefix = lstENT[0].OrderPrefix != null ? lstENT[0].OrderPrefix : "";
                }
                else
                {
                    lblTax1.Text = "Tax-1:"; // + ":";
                    txtSGSTPer.Text = "0";
                    lblTax2.Text = "Tax-2:"; // + ":";
                    txtCGSTPer.Text = "0";
                    strOrderPrefix = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearData()
        {
            try
            {
                GlobalVariable.IsOrderPaid = false;
                IsCmbIndexChange = false;
                this.EnableButton(true, true, false, false);
                strMode = "ADD";
                strModeDetail = "ADD";
                txtDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                txtInvoiceNo.Text = string.Empty;
                txtOrderId.Text = string.Empty;
                txtCustId.Text = string.Empty;
                txtCustName.Text = string.Empty;
                txtTableId.Text = string.Empty;
                txtTableNo.Text = "Type : ";
                txtSpecReqForOrder.Text = string.Empty;

                txtSubTotal.Text = string.Empty;
                txtExtraCharge.Text = string.Empty;

                txtDiscount.Text = string.Empty;
                txtDiscountPer.Text = string.Empty;
                txtDiscountType.Text = string.Empty;

                lblTax1.Text = "Tax :";
                txtSGSTPer.Text = string.Empty;
                txtSGST.Text = string.Empty;
                lblTax2.Text = "Tax :";
                txtCGSTPer.Text = string.Empty;
                txtCGST.Text = string.Empty;
                txtTotalTax.Text = string.Empty;

                txtTip.Text = string.Empty;
                txtDeliveryCharge.Text = string.Empty;
                txtPayAmt.Text = string.Empty;

                // Modifiers Panel Textbox Clear
                txtProdID.Text = string.Empty;
                txtTransID.Text = string.Empty;
                txtSpecialRequestItem.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtProductDetail.Text = string.Empty;
                txtSearchQty.Text = string.Empty;

                dgvItem.Rows.Clear();
                intSort = 0;
                tblVisible = false;
                //btnCancel.Visible = false;
                pnlModifier.Visible = false;
                pnlSearch.Visible = true;
                this.FillAllComboRecentOrder();
                this.FillAllComboProductLookup();
                cmbProduct.SelectedIndex = -1;
                this.VisibleTaxTable();
                this.GetTaxFromGeneralSetting();
                this.TabControlIndexChanged();
                IsCmbIndexChange = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteCartItem()
        {
            try
            {
                if (MessageBox.Show("Are you sure! You want to delete selected item ?", "Item Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                DAL.Transaction objDALTrans = new DAL.Transaction();
                ENT.Transaction objENTTrans = new ENT.Transaction();
                List<ENT.Transaction> lstENTTrans = new List<ENT.Transaction>();

                objENTTrans.Mode = "DELETE";
                objENTTrans.OrderID = new Guid(txtOrderId.Text.Trim());
                objENTTrans.TransactionID = new Guid(dgvItem.Rows[dgvItem.CurrentRow.Index].Cells["ordTransID"].Value.ToString());
                if (objDALTrans.InsertUpdateDeleteOrderTransaction(objENTTrans))
                {
                    this.GetCartItemInList(txtOrderId.Text.Trim());
                    this.UpdateOrderTransaction();
                    if (intSort != 0)
                    { intSort -= 1; }
                    tcMain.SelectedIndex = 0;
                    pnlModifier.Visible = false;
                    pnlSearch.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// this function is used to start client and server when form load.
        /// this function called from frmOrderBook_Load event.
        /// </summary>
        public void StartSocketServerClient()
        {
            try
            {
                ENT.DeviceMaster objENT = new ENT.DeviceMaster();
                List<ENT.DeviceMaster> lstENT = new List<ENT.DeviceMaster>();
                objENT.Mode = "GetByTypeID";
                objENT.DeviceTypeID = (int)GlobalVariable.DeviceType.POS;
                lstENT = new DAL.DeviceMaster().getDeviceMaster(objENT);

                for (int i = 0; i < lstENT.Count; i++)
                {
                    AsynchronousServer.console = this.serverConsole;   //link forms for static usage
                    AsynchronousClient.console = this.clientConsole;
                    String Addr = $"{ lstENT[i].DeviceIP}:{Properties.Settings.Default.Port}";
                    String[] split = Addr.Split(':');
                    if (split.Length == 2)
                    {
                        //Start Server and connect to client with given port
                        AsynchronousServer.consoleContainer = this.panel58;
                        AsynchronousServer.list = this.panel60;   //link list of all connected clients
                        if (!AsynchronousServer.runningServer)
                        {
                            AsynchronousServer.port = Int32.Parse(split[1]);
                            AsynchronousServer.StartListening();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bad adress, only allowed ip:port combination.", "Bliss", MessageBoxButtons.OK);
                    }

                    // Start Client
                    if (!AsynchronousClient.connected)
                    {
                        if (split.Length == 2)
                        {
                            try
                            {
                                //start client and connect to server with given ip and port
                                AsynchronousClient.keepConnection = true;
                                AsynchronousClient.ipAddress = IPAddress.Parse(split[0]);
                                AsynchronousClient.port = Int32.Parse(split[1]);
                                AsynchronousClient.console = this.clientConsole;
                                AsynchronousClient.consoleContainer = this.panel59;
                                AsynchronousClient.StartClient();
                            }
                            catch (System.FormatException)
                            {
                                MessageBox.Show("Invalid ip/port.", "Bliss", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bad adress, only allowed ip:port combination", "Bliss", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                //if (AsynchronousServer.runningServer)
                //{
                //    AsynchronousServer.Send(ClientServerDataParsing.AddDeviceRequest(), -1);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetNewOrderDetailForSocket()
        {
            try
            {
                ENT.NEW_ORDER_101 objNEWORDER = new ENT.NEW_ORDER_101();
                objNEWORDER.ackGuid = Guid.NewGuid().ToString();
                objNEWORDER.ipAddress = getSystemIP();
                objNEWORDER.syncCode = ENT.SyncCode.C_NEW_ORDER;

                ENT.SyncMaster objSyncMaster = new ENT.SyncMaster();
                objSyncMaster.SyncCode = ENT.SyncCode.C_NEW_ORDER;
                objSyncMaster.batchCode = txtOrderId.Text.Trim();
                objSyncMaster.date = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                objSyncMaster.id = Guid.NewGuid().ToString();
                objNEWORDER.syncMaster = objSyncMaster;

                List<ENT.OrderData> lstENTOrder = new List<ENT.OrderData>();
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                objENTOrder.OrderID = new Guid(txtOrderId.Text);
                objENTOrder.Mode = "GetRecordByOrderIDForSocket";
                lstENTOrder = new DAL.OrderBook().getOrderForSocket(objENTOrder);
                for (int i = 0; i < lstENTOrder.Count; i++)
                {
                    ENT.CustomerMasterData objENTCustomers = new ENT.CustomerMasterData();
                    objENTCustomers.Mode = "GetRecordByIDForSocket";
                    objENTCustomers.CustomerID = new Guid(lstENTOrder[i].customerId);
                    ENT.Customer objCustomers = new DAL.CustomerMasterData().getCustomerForSocket(objENTCustomers);
                    lstENTOrder[i].customer = objCustomers;

                    List<ENT.ItemsList> lstItemList = new List<ENT.ItemsList>();
                    ENT.Transaction objTran = new ENT.Transaction();
                    objTran.Mode = "GetRecordForNewOrderSocket";
                    objTran.OrderID = new Guid(lstENTOrder[i].orderId);
                    lstItemList = new DAL.Transaction().getItemListForSocket(objTran);
                    lstENTOrder[i].itemsList = lstItemList;

                    for (int n = 0; n < lstItemList.Count; n++)
                    {
                        List<ENT.ComboProductDetailItem> lstCPDIList = new List<ENT.ComboProductDetailItem>();
                        ENT.ComboProductDetail objCPD = new ENT.ComboProductDetail();
                        objCPD.Mode = "GetRecordByProductIDForSocket";
                        objCPD.ProductID = new Guid(lstItemList[n].itemId);
                        lstCPDIList = new DAL.ComboProductDetail().getComboItemForSocket(objCPD);
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

                if (AsynchronousClient.connected)
                {
                    AsynchronousClient.Send(Newtonsoft.Json.JsonConvert.SerializeObject(objNEWORDER));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string getSystemIP()
        {
            string IP = "";
            try
            {
                string strHostName = "";
                strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
                IPAddress[] addr = ipEntry.AddressList;
                if (addr.Length > 2)
                {
                    IP = addr[2].ToString();
                }
                else
                {
                    IP = addr[1].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Internet connection problem.", "Branch Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return IP;
        }

        #endregion

        #region Breadcrumb Panel

        private void drawAddressBar(string pnlName)
        {
            try
            {
                pnlSearch.Size = new System.Drawing.Size(tpMenu.Width - 15, 32);  // 62
                itemCount = 0;

                lstCatAddr = new List<ENT.CategoryAddress>();
                pnlAddressBar = new Panel();
                pnlAddressBar.AutoScroll = true;
                pnlAddressBar.Location = new System.Drawing.Point(6, pnlSearch.Height + 12);  // pnlMainMenu.Height + 12 // 510
                pnlAddressBar.Size = new System.Drawing.Size(tpMenu.Width - 15, 62);  // 62
                pnlAddressBar.BorderStyle = BorderStyle.FixedSingle;
                pnlAddressBar.Name = pnlName;
                pnlAddressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)))));
                this.tpMenu.Controls.Add(pnlAddressBar);
                lstCatAddr.Add(new ENT.CategoryAddress() { CategoryID = "00000000-0000-0000-0000-000000000000", CategoryName = "Home", Index = itemCount });
                addAddressbutton();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addAddressbutton()
        {
            try
            {
                increaseLength = 6;
                Button button = new Button();
                pnlAddressBar.Controls.Remove(button);
                for (int i = 0; i < lstCatAddr.Count; i++)
                {
                    button.Location = new Point(increaseLength, 6);
                    if (i == 0)
                    {
                        button.Click += new EventHandler(btnHome_Click);
                        button.ImageAlign = ContentAlignment.MiddleLeft;
                        button.ImageKey = "home.png";
                        button.ImageList = this.imageList1;
                        button.BackColor = Color.Green;
                    }
                    else
                    {
                        button.Click += new EventHandler(btnAddressBar_Click);
                        button.ImageAlign = ContentAlignment.MiddleRight;
                        button.ImageKey = "right.png";
                        button.ImageList = this.imageList1;
                        button.BackColor = Color.FromArgb(9, 89, 79); //Color.Teal; //DarkOrange;
                    }
                    button.Name = "btn" + i;
                    button.TabIndex = i;
                    button.Font = new Font("Microsoft Sans Serif", 11);
                    button.Tag = lstCatAddr[i].CategoryID;
                    button.Text = lstCatAddr[i].CategoryName;
                    button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)))));
                    button.Size = new System.Drawing.Size(120, 50);

                    button.ForeColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.Font = new Font(button.Font, FontStyle.Bold);

                    pnlAddressBar.Controls.Add(button);
                    increaseLength += 126;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveAddressbutton(int Index)
        {
            try
            {
                for (int i = lstCatAddr.Count - 1; i > 0; i--)
                {
                    if (Index <= i)
                    {
                        increaseLength -= 126;
                        lstCatAddr.RemoveAt(i);
                        pnlAddressBar.Controls.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddressBar_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                ENT.CategoryMaster objENTCat = new ENT.CategoryMaster();
                List<ENT.CategoryMaster> lstCat = new List<ENT.CategoryMaster>();
                DAL.CategoryMaster objDALCat = new DAL.CategoryMaster();

                objENTCat.Mode = "GetRecordByCategoryID";
                objENTCat.CategoryID = new Guid(button.Tag.ToString());
                lstCat = objDALCat.getCategoryMaster(objENTCat);
                if (lstCat.Count > 0)
                {
                    RemoveAddressbutton(button.TabIndex);
                    this.getCategory(lstCat[0].ParentID.ToString());
                }
                else
                {
                    MessageBox.Show("No More Parent Category Found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnHome = sender as Button;
                this.RemoveAddressbutton(btnHome.TabIndex);
                this.getCategory(btnHome.Tag.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Main Menu Tab

        private void drawPanelMenu(string pnlName)
        {
            try
            {
                pnlMainMenu = new Panel();
                pnlMainMenu.AutoScroll = true;
                pnlMainMenu.Location = new System.Drawing.Point(6, pnlAddressBar.Height + pnlSearch.Height + 18);
                pnlMainMenu.Size = new System.Drawing.Size(tpMenu.Width - 15, tpMenu.Height - 118);  // w-490 // h-470
                pnlMainMenu.BorderStyle = BorderStyle.FixedSingle;
                pnlMainMenu.Name = pnlName;
                pnlMainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)))));
                this.tpMenu.Controls.Add(pnlMainMenu);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getCategory(string ParentID)
        {
            try
            {
                tpMenu.Controls.Remove(pnlMainMenu);
                this.drawPanelMenu("pnlMainMenu");

                objENTCategoryMaster.Mode = "GetRecordByParentID";
                objENTCategoryMaster.ParentID = new Guid(ParentID);
                lstCategoryMaster = objDALCategoryMaster.getDisplayCategoryButton(objENTCategoryMaster).OrderBy(ord => ord.CategoryName).ToList();

                int pnl_width = 0;
                int x = 0;
                int y = 12;
                int i = 0;
                int col = 0;
                double row = 0;

                if (pnlMainMenu.Width > 525)
                {
                    col = 4;
                    pnl_width = (pnlMainMenu.Width - 498) / 2;
                    if (lstCategoryMaster.Count <= col)
                        row = 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstCategoryMaster.Count) / col, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    col = 3;
                    pnl_width = (pnlMainMenu.Width - 372) / 2;
                    if (lstCategoryMaster.Count <= col)
                        row = 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstCategoryMaster.Count) / col, 2, MidpointRounding.AwayFromZero);
                }

                for (int h = 0; h < row; h++)
                {
                    x = pnl_width;
                    for (int v = 0; v < col; v++)
                    {
                        Button button = new Button();
                        button.Location = new Point(x, y);
                        button.Click += new EventHandler(ButtonClickOneEvent);
                        button.Tag = lstCategoryMaster[i].CategoryID;
                        button.Text = lstCategoryMaster[i].CategoryName;
                        button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)))));
                        button.Size = new System.Drawing.Size(120, 100);
                        if (lstCategoryMaster[i].IsCategory == 0)
                        {
                            button.BackColor = Color.Green; // Color for Product
                            button.Text = button.Text + "\n Rs. " + lstCategoryMaster[i].Price;
                        }
                        else
                        {
                            button.BackColor = Color.FromArgb(58, 173, 158); //Color.Teal;  //DarkOrange; // Color for Category
                        }
                        button.Font = new Font("Microsoft Sans Serif", 11);
                        button.ForeColor = Color.White;
                        button.FlatStyle = FlatStyle.Flat;
                        button.Font = new Font(button.Font, FontStyle.Bold);
                        pnlMainMenu.Controls.Add(button);
                        if (i == lstCategoryMaster.Count - 1)
                        {
                            break;
                        }
                        i++;
                        x = x + 126;
                    }
                    y = y + 106;
                }
                itemCount += 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonClickOneEvent(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button != null)
                {
                    for (int n = 0; n <= lstCategoryMaster.Count - 1; n++)
                    {
                        if (button.Tag.ToString() == lstCategoryMaster[n].CategoryID.ToString())
                        {
                            if (lstCategoryMaster[n].Count != 0)
                            {
                                lstCatAddr.Add(new ENT.CategoryAddress() { CategoryID = lstCategoryMaster[n].CategoryID.ToString(), CategoryName = lstCategoryMaster[n].CategoryName.ToString(), Index = itemCount });
                                this.getCategory(lstCategoryMaster[n].CategoryID.ToString());
                                addAddressbutton();
                            }
                            else
                            {
                                Guid ProdId = new Guid(button.Tag.ToString());
                                this.AddCartItem(ProdId);
                                GetNewOrderDetailForSocket();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(cmbProduct.SelectedValue) != "" && cmbProduct.SelectedValue != null)
                {
                    Guid ProdId = new Guid(Convert.ToString(cmbProduct.SelectedValue));
                    this.AddCartItem(ProdId);
                    cmbProduct.SelectedIndex = -1;
                    txtSearchQty.Text = string.Empty;
                    cmbProduct.Focus();

                    // send order data to connected client
                    GetNewOrderDetailForSocket();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Table Menu Tab

        private void drawPanelTableAll(string pnlName)
        {
            try
            {
                pnlTableAll = new Panel();
                pnlTableAll.AutoScroll = true;
                pnlTableAll.Location = new System.Drawing.Point(6, 6);
                pnlTableAll.Size = new System.Drawing.Size(tpTabAll.Width - 15, tpTabAll.Height - 15);
                pnlTableAll.BorderStyle = BorderStyle.FixedSingle;
                pnlTableAll.Name = pnlName;
                pnlTableAll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)))));
                this.tpTabAll.Controls.Add(pnlTableAll);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getTableAll()
        {
            try
            {
                tpTabAll.Controls.Remove(pnlTableAll);
                this.drawPanelTableAll("pnlTableAll");

                DAL.TableMasterDetail objDALTable = new DAL.TableMasterDetail();
                ENT.TableMasterDetail objENTTable = new ENT.TableMasterDetail();
                List<ENT.TableMasterDetail> lstENTTable = new List<ENT.TableMasterDetail>();

                //objENTTable.Mode = "GetTableForViewByEmpID";
                objENTTable.Mode = "GetTableForViewByClassID";
                objENTTable.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTTable.ClassID = new Guid(GlobalVariable.ClassID);
                lstENTTable = objDALTable.getTableMasterDetail(objENTTable);

                int pnl_width = 0;
                int x = 0;
                int y = 12;
                int i = 0;
                int col = 0;
                double row = 0;

                if (pnlTableAll.Width > 600)
                {
                    col = 4;
                    pnl_width = (pnlTableAll.Width - 578) / 2;
                    if (lstENTTable.Count <= col)
                        row = lstENTTable.Count == 0 ? 0 : 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstENTTable.Count) / col, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    col = 3;
                    pnl_width = (pnlTableAll.Width - 432) / 2;
                    if (lstENTTable.Count <= col)
                        row = lstENTTable.Count == 0 ? 0 : 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstENTTable.Count) / col, 2, MidpointRounding.AwayFromZero);
                }

                for (int h = 0; h < row; h++)
                {
                    x = pnl_width;
                    for (int v = 0; v < col; v++)
                    {
                        Label lblTableName = new Label(); Label lblTime = new Label();
                        Label lblName = new Label(); Label lblAmount = new Label();
                        Panel pnlButton = new Panel();

                        if (lstENTTable[i].StatusID == Convert.ToInt32(GlobalVariable.TableStatus.Occupied))
                        {
                            pnlButton.BackColor = Color.DarkOrange;
                            pnlButton.Tag = lstENTTable[i].OrderID.ToString();
                            pnlButton.Name = lstENTTable[i].TableID.ToString(); //

                            lblTableName.AutoSize = true;
                            lblTableName.Location = new System.Drawing.Point(3, 3);
                            lblTableName.Name = lstENTTable[i].TableID.ToString(); //"lblTableName" + i;
                            lblTableName.Tag = lstENTTable[i].OrderID;
                            lblTableName.Text = lstENTTable[i].TableName;
                            lblTableName.ForeColor = Color.White;
                            lblTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            lblTableName.Click += new EventHandler(PanelLabelAll_ClickEvent);

                            lblTime.AutoSize = true;
                            lblTime.Location = new System.Drawing.Point(60, 3);
                            lblTime.Name = lstENTTable[i].TableID.ToString(); //"lblTime" + i;
                            lblTime.Tag = lstENTTable[i].OrderID;
                            lblTime.Text = lstENTTable[i].OrderDate == null ? "" : lstENTTable[i].OrderDate.ToString("hh:mm tt");
                            lblTime.ForeColor = Color.White;
                            lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            lblTime.Click += new EventHandler(PanelLabelAll_ClickEvent);

                            lblName.AutoSize = true;
                            lblName.Location = new System.Drawing.Point(3, 30);
                            lblName.Name = lstENTTable[i].TableID.ToString(); //"lblName" + i;
                            lblName.Tag = lstENTTable[i].OrderID;
                            lblName.Text = lstENTTable[i].CustomerName;
                            lblName.ForeColor = Color.White;
                            lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            lblName.Click += new EventHandler(PanelLabelAll_ClickEvent);

                            lblAmount.AutoSize = true;
                            lblAmount.Location = new System.Drawing.Point(3, 57);
                            lblAmount.Name = lstENTTable[i].TableID.ToString(); //"lblAmount" + i;
                            lblAmount.Tag = lstENTTable[i].OrderID;
                            lblAmount.Text = lstENTTable[i].PayableAmount.ToString();
                            lblAmount.ForeColor = Color.White;
                            lblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            lblAmount.Click += new EventHandler(PanelLabelAll_ClickEvent);
                        }
                        else
                        {
                            pnlButton.BackColor = Color.Green;
                            pnlButton.Tag = "";

                            lblTableName.AutoSize = true;
                            lblTableName.Location = new System.Drawing.Point(44, 32);
                            lblTableName.Name = "lblVCTableName" + i;
                            lblTableName.Tag = "";
                            lblTableName.Text = lstENTTable[i].TableName;
                            lblTableName.ForeColor = Color.White;
                            lblTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                            lblName.Name = "";
                            lblAmount.Name = "";
                            lblTime.Text = "";
                        }

                        pnlButton.Location = new Point(x, y);
                        pnlButton.Click += new EventHandler(PanelTableAll_ClickEvent);
                        pnlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)))));
                        pnlButton.Size = new System.Drawing.Size(140, 84);
                        pnlButton.BorderStyle = BorderStyle.FixedSingle;
                        pnlButton.Controls.Add(lblTableName);
                        pnlButton.Controls.Add(lblTime);
                        pnlButton.Controls.Add(lblName);
                        pnlButton.Controls.Add(lblAmount);
                        pnlButton.TabIndex = i;
                        pnlTableAll.Controls.Add(pnlButton);

                        if (i == lstENTTable.Count - 1)
                        {
                            break;
                        }
                        i++;
                        x = x + 146;
                    }
                    y = y + 90;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PanelLabelAll_ClickEvent(object sender, EventArgs e)
        {
            try
            {
                Label button = sender as Label;
                if (button.Tag.ToString() != string.Empty && button.Name.ToString() != string.Empty)
                {
                    frmMergeTable frmVOD = new frmMergeTable(button.Tag.ToString(), button.Name.ToString());
                    frmVOD.ShowDialog();
                    this.getTableAll();
                }
                else
                {
                    MessageBox.Show("This is vacant table.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PanelTableAll_ClickEvent(object sender, EventArgs e)
        {
            try
            {
                Panel button = sender as Panel;
                if (button.Tag.ToString() != string.Empty && button.Name.ToString() != string.Empty)
                {
                    frmMergeTable frmVOD = new frmMergeTable(button.Tag.ToString(), button.Name.ToString());
                    frmVOD.ShowDialog();
                    this.getTableAll();
                }
                else
                {
                    MessageBox.Show("This is vacant table.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void drawPanelTableVacant(string pnlName)
        {
            try
            {
                pnlTableVacant = new Panel();
                pnlTableVacant.AutoScroll = true;
                pnlTableVacant.Location = new System.Drawing.Point(6, 6);
                pnlTableVacant.Size = new System.Drawing.Size(tpTabVacant.Width - 15, tpTabVacant.Height - 15);
                pnlTableVacant.BorderStyle = BorderStyle.FixedSingle;
                pnlTableVacant.Name = pnlName;
                pnlTableVacant.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)))));
                this.tpTabVacant.Controls.Add(pnlTableVacant);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getTableVacant()
        {
            try
            {
                tpTabVacant.Controls.Remove(pnlTableVacant);
                this.drawPanelTableVacant("pnlTableVacant");

                DAL.TableMasterDetail objDALTable = new DAL.TableMasterDetail();
                ENT.TableMasterDetail objENTTable = new ENT.TableMasterDetail();
                List<ENT.TableMasterDetail> lstENTTable = new List<ENT.TableMasterDetail>();

                //objENTTable.Mode = "GetTableForViewByEmpID";
                objENTTable.Mode = "GetTableForViewByClassID";
                objENTTable.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTTable.ClassID = new Guid(GlobalVariable.ClassID);
                lstENTTable = objDALTable.getTableMasterDetail(objENTTable);
                lstENTTable = lstENTTable.Where(tbl => tbl.StatusID == Convert.ToInt32(GlobalVariable.TableStatus.Vacant)).ToList();

                int pnl_width = 0;
                int x = 0;
                int y = 12;
                int i = 0;
                int col = 0;
                double row = 0;

                if (pnlTableVacant.Width > 600)
                {
                    col = 4;
                    pnl_width = (pnlTableVacant.Width - 578) / 2;
                    if (lstENTTable.Count <= col)
                        row = lstENTTable.Count == 0 ? 0 : 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstENTTable.Count) / col, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    col = 3;
                    pnl_width = (pnlTableVacant.Width - 432) / 2;
                    if (lstENTTable.Count <= col)
                        row = lstENTTable.Count == 0 ? 0 : 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstENTTable.Count) / col, 2, MidpointRounding.AwayFromZero);
                }

                for (int h = 0; h < row; h++)
                {
                    x = pnl_width;
                    for (int v = 0; v < col; v++)
                    {
                        Label lblTableName = new Label();
                        lblTableName.AutoSize = true;
                        lblTableName.Location = new System.Drawing.Point(44, 32);
                        lblTableName.Name = "lblVCTableName" + i;
                        lblTableName.Tag = lstENTTable[i].TableID;
                        lblTableName.Text = lstENTTable[i].TableName;
                        lblTableName.ForeColor = Color.White;
                        lblTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        Panel pnlButton = new Panel();
                        pnlButton.Location = new Point(x, y);
                        pnlButton.Click += new EventHandler(PanelTableVacant_ClickEvent);
                        pnlButton.Tag = lstENTTable[i].TableID;
                        pnlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)))));
                        pnlButton.Size = new System.Drawing.Size(140, 84);
                        pnlButton.BackColor = Color.Green;
                        pnlButton.BorderStyle = BorderStyle.FixedSingle;
                        pnlButton.TabIndex = i;
                        pnlButton.Controls.Add(lblTableName);
                        pnlTableVacant.Controls.Add(pnlButton);
                        if (i == lstENTTable.Count - 1)
                        {
                            break;
                        }
                        i++;
                        x = x + 146;
                    }
                    y = y + 90;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PanelTableVacant_ClickEvent(object sender, EventArgs e)
        {
            Panel button = sender as Panel;
            //MessageBox.Show("Click On " + button.Tag.ToString());
        }

        private void drawPanelTableOccupied(string pnlName)
        {
            try
            {
                pnlOccupied = new Panel();
                pnlOccupied.AutoScroll = true;
                pnlOccupied.Location = new System.Drawing.Point(6, 6);
                pnlOccupied.Size = new System.Drawing.Size(tpTabOccupied.Width - 15, tpTabOccupied.Height - 15);
                pnlOccupied.BorderStyle = BorderStyle.FixedSingle;
                pnlOccupied.Name = pnlName;
                pnlOccupied.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)))));
                this.tpTabOccupied.Controls.Add(pnlOccupied);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getTableOccupied()
        {
            try
            {
                tpTabOccupied.Controls.Remove(pnlOccupied);
                this.drawPanelTableOccupied("pnlOccupied");

                DAL.TableMasterDetail objDALTable = new DAL.TableMasterDetail();
                ENT.TableMasterDetail objENTTable = new ENT.TableMasterDetail();
                List<ENT.TableMasterDetail> lstENTTable = new List<ENT.TableMasterDetail>();

                //objENTTable.Mode = "GetTableForViewByEmpID";
                objENTTable.Mode = "GetTableForViewByClassID";
                objENTTable.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTTable.ClassID = new Guid(GlobalVariable.ClassID);
                lstENTTable = objDALTable.getTableMasterDetail(objENTTable);
                lstENTTable = lstENTTable.Where(tbl => tbl.StatusID == Convert.ToInt32(GlobalVariable.TableStatus.Occupied)).ToList();

                int pnl_width = 0;
                int x = 0;
                int y = 12;
                int i = 0;
                int col = 0;
                double row = 0;

                if (pnlTableAll.Width > 600)  //525
                {
                    col = 4;
                    pnl_width = (pnlTableAll.Width - 578) / 2;
                    if (lstENTTable.Count <= col)
                        row = lstENTTable.Count == 0 ? 0 : 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstENTTable.Count) / col, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    col = 3;
                    pnl_width = (pnlTableAll.Width - 432) / 2;
                    if (lstENTTable.Count <= col)
                        row = lstENTTable.Count == 0 ? 0 : 1;
                    else
                        row = Math.Round(Convert.ToDouble(lstENTTable.Count) / col, 2, MidpointRounding.AwayFromZero);
                }

                for (int h = 0; h < row; h++)
                {
                    x = pnl_width;
                    for (int v = 0; v < col; v++)
                    {
                        Label lblTableName = new Label();
                        lblTableName.AutoSize = true;
                        lblTableName.Location = new System.Drawing.Point(3, 3);
                        lblTableName.Name = lstENTTable[i].TableID.ToString(); //"lblOCTableName" + i;
                        lblTableName.Tag = lstENTTable[i].OrderID;
                        lblTableName.Text = lstENTTable[i].TableName;
                        lblTableName.ForeColor = Color.White;
                        lblTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblTableName.Click += new EventHandler(PanelLabelOccupied_ClickEvent);

                        Label lblTime = new Label();
                        lblTime.AutoSize = true;
                        lblTime.Location = new System.Drawing.Point(60, 3);
                        lblTime.Name = lstENTTable[i].TableID.ToString(); // "lblOCTime" + i;
                        lblTime.Tag = lstENTTable[i].OrderID;
                        lblTime.Text = lstENTTable[i].OrderDate == null ? "" : lstENTTable[i].OrderDate.ToString("hh:mm tt");
                        lblTime.ForeColor = Color.White;
                        lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblTime.Click += new EventHandler(PanelLabelOccupied_ClickEvent);

                        Label lblName = new Label();
                        lblName.AutoSize = true;
                        lblName.Location = new System.Drawing.Point(3, 30);
                        lblName.Name = lstENTTable[i].TableID.ToString(); //"lblOCName" + i;
                        lblName.Tag = lstENTTable[i].OrderID;
                        lblName.Text = lstENTTable[i].CustomerName;
                        lblName.ForeColor = Color.White;
                        lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblName.Click += new EventHandler(PanelLabelOccupied_ClickEvent);

                        Label lblAmount = new Label();
                        lblAmount.AutoSize = true;
                        lblAmount.Location = new System.Drawing.Point(3, 57);
                        lblAmount.Name = lstENTTable[i].TableID.ToString(); //"lblOCAmount" + i;
                        lblAmount.Tag = lstENTTable[i].OrderID;
                        lblAmount.Text = lstENTTable[i].PayableAmount.ToString();
                        lblAmount.ForeColor = Color.White;
                        lblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblAmount.Click += new EventHandler(PanelLabelOccupied_ClickEvent);

                        Panel pnlButton = new Panel();
                        pnlButton.Location = new Point(x, y);
                        pnlButton.Click += new EventHandler(PanelTableOccupied_ClickEvent);
                        pnlButton.Tag = lstENTTable[i].OrderID;
                        pnlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)))));
                        pnlButton.Size = new System.Drawing.Size(140, 84);
                        pnlButton.BackColor = Color.DarkOrange;
                        pnlButton.BorderStyle = BorderStyle.FixedSingle;
                        pnlButton.Name = lstENTTable[i].TableID.ToString();

                        pnlButton.Controls.Add(lblTableName);
                        pnlButton.Controls.Add(lblTime);
                        pnlButton.Controls.Add(lblName);
                        pnlButton.Controls.Add(lblAmount);
                        pnlButton.TabIndex = i;
                        pnlOccupied.Controls.Add(pnlButton);

                        if (i == lstENTTable.Count - 1)
                        {
                            break;
                        }
                        i++;
                        x = x + 146;
                    }
                    y = y + 90;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PanelLabelOccupied_ClickEvent(object sender, EventArgs e)
        {
            try
            {
                Label button = sender as Label;
                if (button.Tag.ToString() != string.Empty && button.Name.ToString() != string.Empty)
                {
                    frmMergeTable frmVOD = new frmMergeTable(button.Tag.ToString(), button.Name.ToString());
                    frmVOD.ShowDialog();
                    this.getTableOccupied();
                }
                else
                {
                    MessageBox.Show("This is vacant table.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PanelTableOccupied_ClickEvent(object sender, EventArgs e)
        {
            try
            {
                Panel button = sender as Panel;
                if (button.Tag.ToString() != string.Empty && button.Name.ToString() != string.Empty)
                {
                    frmMergeTable frmVOD = new frmMergeTable(button.Tag.ToString(), button.Name.ToString());
                    frmVOD.ShowDialog();
                    this.getTableOccupied();
                }
                else
                {
                    MessageBox.Show("This is vacant table.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Recent Order Tab

        private void ComboAllIndexChange(int intDayFilter, int intDeliveryType, string strSearch)
        {
            try
            {
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                List<ENT.OrderBook> lstENTOrder = new List<ENT.OrderBook>();
                DAL.OrderBook objDALOrder = new DAL.OrderBook();

                objENTOrder.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTOrder.DeliveryType = intDeliveryType;

                if (intDeliveryType == 0)
                    objENTOrder.Mode = "GetAllRecordByDateFilter";
                else
                    objENTOrder.Mode = "GetRecordByTypeAndDateFilter";

                if (intDayFilter == Convert.ToInt32(GlobalVariable.DayFilter.Today) && intDeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.Cancel))
                {
                    objENTOrder.Mode = "GetCancelOrderByDateFilter";
                    objENTOrder.DeliveryType = 0;
                    objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(DateTime.Now.ToString("dd/MM/yyyy"));
                    objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(DateTime.Now.ToString("dd/MM/yyyy"));
                }
                else if (intDayFilter == Convert.ToInt32(GlobalVariable.DayFilter.Yesterday) && intDeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.Cancel))
                {
                    objENTOrder.Mode = "GetCancelOrderByDateFilter";
                    objENTOrder.DeliveryType = 0;
                    objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
                    objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
                }
                else if (intDayFilter == Convert.ToInt32(GlobalVariable.DayFilter.Custom) && intDeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.Cancel))
                {
                    objENTOrder.Mode = "GetCancelOrderByDateFilter";
                    objENTOrder.DeliveryType = 0;
                    objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(dtpCancelTo.Text);
                    objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(dtpCancelTo.Text);
                }

                else if (intDayFilter == Convert.ToInt32(GlobalVariable.DayFilter.Custom) && intDeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.All))
                {
                    objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(dtpAllTo.Text);
                    objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(dtpAllTo.Text);
                }
                else if (intDayFilter == Convert.ToInt32(GlobalVariable.DayFilter.Custom) && intDeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.DineIn))
                {
                    objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(dtpDineInTo.Text);
                    objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(dtpDineInTo.Text);
                }
                else if (intDayFilter == Convert.ToInt32(GlobalVariable.DayFilter.Custom) && intDeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.TakeOut))
                {
                    objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(dtpTakeOutTo.Text);
                    objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(dtpTakeOutTo.Text);
                }
                else if (intDayFilter == Convert.ToInt32(GlobalVariable.DayFilter.Custom) && intDeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.Delivery))
                {
                    objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(dtpDeliveryTo.Text);
                    objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(dtpDeliveryTo.Text);
                }
                else if (intDayFilter == Convert.ToInt32(GlobalVariable.DayFilter.Today))
                {
                    objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(DateTime.Now.ToString("dd/MM/yyyy"));
                    objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(DateTime.Now.ToString("dd/MM/yyyy"));
                }
                else if (intDayFilter == Convert.ToInt32(GlobalVariable.DayFilter.Yesterday))
                {
                    objENTOrder.OrderDateFrom = GlobalVariable.ChangeDate(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
                    objENTOrder.OrderDateTo = GlobalVariable.ChangeDate(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
                }

                lstENTOrder = objDALOrder.getOrder(objENTOrder);
                if (strSearch != "")
                    lstENTOrder = lstENTOrder.Where(cust => cust.OrderNo.ToString().Contains(strSearch.ToLower())).ToList();

                if (intDeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.All))
                    this.GetAllOrder(lstENTOrder);
                else if (intDeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.DineIn))
                {
                    //lstENTOrder = lstENTOrder.Where(cust => cust.OrderStatus != Convert.ToInt32(GlobalVariable.OrderStatus.CANCEL)).ToList();
                    this.GetDineInOrder(lstENTOrder);
                }
                else if (intDeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.TakeOut))
                {
                    //lstENTOrder = lstENTOrder.Where(cust => cust.OrderStatus != Convert.ToInt32(GlobalVariable.OrderStatus.CANCEL)).ToList();
                    this.GetTakeOutOrder(lstENTOrder);
                }
                else if (intDeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.Delivery))
                {
                    //lstENTOrder = lstENTOrder.Where(cust => cust.OrderStatus != Convert.ToInt32(GlobalVariable.OrderStatus.CANCEL)).ToList();
                    this.GetDeliveryOrder(lstENTOrder);
                }
                else if (intDeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.Cancel))
                {
                    //lstENTOrder = lstENTOrder.Where(cust => Convert.ToInt32(cust.OrderStatus) == 0).ToList();
                    this.GetCancelOrder(lstENTOrder);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Filter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetAllOrder(List<ENT.OrderBook> lstENTOrder)
        {
            try
            {
                txtTotalAll.Text = "0";
                odDgvAll.Rows.Clear();
                for (int i = 0; i < lstENTOrder.Count; i++)
                {
                    odDgvAll.Rows.Add();
                    odDgvAll.Rows[i].Cells["AllInvoiceNo"].Value = lstENTOrder[i].OrderNo.ToString();
                    odDgvAll.Rows[i].Cells["AllCustomer"].Value = lstENTOrder[i].Name.ToString();
                    odDgvAll.Rows[i].Cells["AllTable"].Value = Convert.ToString(lstENTOrder[i].TableName);
                    odDgvAll.Rows[i].Cells["AllType"].Value = lstENTOrder[i].DeliveryTypeName.ToString();
                    odDgvAll.Rows[i].Cells["AllTotal"].Value = lstENTOrder[i].PayableAmount.ToString();
                    odDgvAll.Rows[i].Cells["AllOrderID"].Value = lstENTOrder[i].OrderID.ToString();
                    txtTotalAll.Text = Convert.ToString(Convert.ToDecimal(txtTotalAll.Text) + lstENTOrder[i].PayableAmount);
                    DataGridViewButtonCell bc = new DataGridViewButtonCell();
                    bc.FlatStyle = FlatStyle.Flat;
                    bc.Value = lstENTOrder[i].OrderActionsName;
                    if (lstENTOrder[i].OrderActions == 1)
                    {
                        bc.Style.BackColor = Color.Green;
                    }
                    else if (lstENTOrder[i].OrderActions == 2)
                    {
                        bc.Style.BackColor = Color.DarkOrange;
                    }
                    else if (lstENTOrder[i].OrderActions == 3)
                    {
                        bc.Style.BackColor = Color.DarkSlateGray;
                    }
                    odDgvAll.Rows[i].Cells["AllAction"] = bc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetDineInOrder(List<ENT.OrderBook> lstENTOrder)
        {
            try
            {
                txtTotalDinIn.Text = "0";
                odDgvDineIn.Rows.Clear();
                for (int i = 0; i < lstENTOrder.Count; i++)
                {
                    odDgvDineIn.Rows.Add();
                    odDgvDineIn.Rows[i].Cells["DIInvoiceNo"].Value = lstENTOrder[i].OrderNo.ToString();
                    odDgvDineIn.Rows[i].Cells["DICustomer"].Value = lstENTOrder[i].Name.ToString();
                    odDgvDineIn.Rows[i].Cells["DITable"].Value = Convert.ToString(lstENTOrder[i].TableName);
                    odDgvDineIn.Rows[i].Cells["DIType"].Value = lstENTOrder[i].DeliveryTypeName.ToString();
                    odDgvDineIn.Rows[i].Cells["DITotal"].Value = lstENTOrder[i].PayableAmount.ToString();
                    odDgvDineIn.Rows[i].Cells["DIOrderID"].Value = lstENTOrder[i].OrderID.ToString();
                    txtTotalDinIn.Text = Convert.ToString(Convert.ToDecimal(txtTotalDinIn.Text) + lstENTOrder[i].PayableAmount);
                    DataGridViewButtonCell bc = new DataGridViewButtonCell();
                    bc.FlatStyle = FlatStyle.Flat;
                    bc.Value = lstENTOrder[i].OrderActionsName.ToString();
                    if (lstENTOrder[i].OrderActions == 1)
                    {
                        bc.Style.BackColor = Color.Green;
                    }
                    else if (lstENTOrder[i].OrderActions == 2)
                    {
                        bc.Style.BackColor = Color.DarkOrange;
                    }
                    else if (lstENTOrder[i].OrderActions == 3)
                    {
                        bc.Style.BackColor = Color.DarkSlateGray;
                    }
                    odDgvDineIn.Rows[i].Cells["DIAction"] = bc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetTakeOutOrder(List<ENT.OrderBook> lstENTOrder)
        {
            try
            {
                txtTotalTakeOut.Text = "0";
                odDgvTakeOut.Rows.Clear();
                for (int i = 0; i < lstENTOrder.Count; i++)
                {
                    odDgvTakeOut.Rows.Add();
                    odDgvTakeOut.Rows[i].Cells["TOInvoiceNo"].Value = lstENTOrder[i].OrderNo.ToString();
                    odDgvTakeOut.Rows[i].Cells["TOCustomer"].Value = lstENTOrder[i].Name.ToString();
                    odDgvTakeOut.Rows[i].Cells["TOType"].Value = lstENTOrder[i].DeliveryTypeName.ToString();
                    odDgvTakeOut.Rows[i].Cells["TOTotal"].Value = lstENTOrder[i].PayableAmount.ToString();
                    odDgvTakeOut.Rows[i].Cells["TOOrderID"].Value = lstENTOrder[i].OrderID.ToString();
                    txtTotalTakeOut.Text = Convert.ToString(Convert.ToDecimal(txtTotalTakeOut.Text) + lstENTOrder[i].PayableAmount);
                    DataGridViewButtonCell bc = new DataGridViewButtonCell();
                    bc.FlatStyle = FlatStyle.Flat;
                    bc.Value = lstENTOrder[i].OrderActionsName.ToString();
                    if (lstENTOrder[i].OrderActions == 1)
                    {
                        bc.Style.BackColor = Color.Green;
                    }
                    else if (lstENTOrder[i].OrderActions == 2)
                    {
                        bc.Style.BackColor = Color.DarkOrange;
                    }
                    else if (lstENTOrder[i].OrderActions == 3)
                    {
                        bc.Style.BackColor = Color.DarkSlateGray;
                    }
                    odDgvTakeOut.Rows[i].Cells["TOAction"] = bc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetDeliveryOrder(List<ENT.OrderBook> lstENTOrder)
        {
            try
            {
                txtTotalDelivery.Text = "0";
                odDgvDelivery.Rows.Clear();
                for (int i = 0; i < lstENTOrder.Count; i++)
                {
                    odDgvDelivery.Rows.Add();
                    odDgvDelivery.Rows[i].Cells["DelInvoiceNo"].Value = lstENTOrder[i].OrderNo.ToString();
                    odDgvDelivery.Rows[i].Cells["DelCustomer"].Value = lstENTOrder[i].Name.ToString();
                    odDgvDelivery.Rows[i].Cells["DelType"].Value = lstENTOrder[i].DeliveryTypeName.ToString();
                    odDgvDelivery.Rows[i].Cells["DelTotal"].Value = lstENTOrder[i].PayableAmount.ToString();
                    odDgvDelivery.Rows[i].Cells["DelOrderID"].Value = lstENTOrder[i].OrderID.ToString();
                    txtTotalDelivery.Text = Convert.ToString(Convert.ToDecimal(txtTotalDelivery.Text) + lstENTOrder[i].PayableAmount);
                    DataGridViewButtonCell bc = new DataGridViewButtonCell();
                    bc.FlatStyle = FlatStyle.Flat;
                    bc.Value = lstENTOrder[i].OrderActionsName.ToString();
                    if (lstENTOrder[i].OrderActions == 1)
                    {
                        bc.Style.BackColor = Color.Green;
                    }
                    else if (lstENTOrder[i].OrderActions == 2)
                    {
                        bc.Style.BackColor = Color.DarkOrange;
                    }
                    else if (lstENTOrder[i].OrderActions == 3)
                    {
                        bc.Style.BackColor = Color.DarkSlateGray;
                    }
                    odDgvDelivery.Rows[i].Cells["DelAction"] = bc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetCancelOrder(List<ENT.OrderBook> lstENTOrder)
        {
            try
            {
                txtTotalCancel.Text = "0";
                odDgvCalcel.Rows.Clear();
                for (int i = 0; i < lstENTOrder.Count; i++)
                {
                    odDgvCalcel.Rows.Add();
                    odDgvCalcel.Rows[i].Cells["CanInvoiceNo"].Value = lstENTOrder[i].OrderNo.ToString();
                    odDgvCalcel.Rows[i].Cells["CanCustomer"].Value = lstENTOrder[i].Name.ToString();
                    odDgvCalcel.Rows[i].Cells["CanType"].Value = lstENTOrder[i].DeliveryTypeName.ToString();
                    odDgvCalcel.Rows[i].Cells["CanTotal"].Value = lstENTOrder[i].PayableAmount.ToString();
                    odDgvCalcel.Rows[i].Cells["CanOrderID"].Value = lstENTOrder[i].OrderID.ToString();
                    txtTotalCancel.Text = Convert.ToString(Convert.ToDecimal(txtTotalCancel.Text) + lstENTOrder[i].PayableAmount);
                    DataGridViewButtonCell bc = new DataGridViewButtonCell();
                    bc.FlatStyle = FlatStyle.Flat;
                    bc.Value = lstENTOrder[i].OrderActionsName.ToString();
                    if (lstENTOrder[i].OrderActions == 1)
                    {
                        bc.Style.BackColor = Color.Green;
                    }
                    else if (lstENTOrder[i].OrderActions == 2)
                    {
                        bc.Style.BackColor = Color.DarkOrange;
                    }
                    else if (lstENTOrder[i].OrderActions == 3)
                    {
                        bc.Style.BackColor = Color.DarkSlateGray;
                    }
                    odDgvCalcel.Rows[i].Cells["CanAction"] = bc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAllSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ComboAllIndexChange(Convert.ToInt32(odCmbAll.SelectedIndex), Convert.ToInt32(GlobalVariable.DeliveryType.All), txtAllSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDineInSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ComboAllIndexChange(Convert.ToInt32(odCmbDineIn.SelectedValue), Convert.ToInt32(GlobalVariable.DeliveryType.DineIn), txtDineInSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTakeOutSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ComboAllIndexChange(Convert.ToInt32(odCmbTakeOut.SelectedValue), Convert.ToInt32(GlobalVariable.DeliveryType.TakeOut), txtTakeOutSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDeliverySearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ComboAllIndexChange(Convert.ToInt32(odCmbDelivery.SelectedValue), Convert.ToInt32(GlobalVariable.DeliveryType.Delivery), txtDeliverySearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCancelSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ComboAllIndexChange(Convert.ToInt32(odCmbCancel.SelectedValue), Convert.ToInt32(GlobalVariable.DeliveryType.Cancel), txtCancelSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Insert Update Order Transation

        private void GenerateOrder()
        {
            try
            {
                if (0 < new DAL.OrderBook().getDuplicateOrderByOrderNo(txtInvoiceNo.Text))
                {
                    MessageBox.Show("Duplicate Order Number Found.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                DAL.OrderBook objDALOrder = new DAL.OrderBook();
                bool result = false;
                if (txtInvoiceNo.Text.Length == 0)
                {
                    MessageBox.Show("Invoice number should not blank.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (!GlobalVariable.IsDate(txtDateTime.Text))
                {
                    MessageBox.Show("Select valid date and time.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (GlobalVariable.objCheckInfo.DelieveryType <= 0)
                {
                    MessageBox.Show("Delivery type is not valid.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (strMode == "ADD")
                {
                    objENTOrder.OrderID = Guid.NewGuid();
                    txtOrderId.Text = objENTOrder.OrderID.ToString();
                }
                else
                    objENTOrder.OrderID = new Guid(txtOrderId.Text);

                objENTOrder.OrderNo = txtInvoiceNo.Text.Trim();
                objENTOrder.OrderDate = GlobalVariable.IsDate(txtDateTime.Text) == true ? GlobalVariable.ChangeDateTime(txtDateTime.Text) : "NULL";
                objENTOrder.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTOrder.CustomerID = new Guid(txtCustId.Text);
                objENTOrder.DeliveryType = GlobalVariable.objCheckInfo.DelieveryType;
                objENTOrder.DeliveryTypeName = GlobalVariable.objCheckInfo.DelieveryTypeName;
                objENTOrder.TableID = GlobalVariable.objCheckInfo.TableID;
                objENTOrder.TableStatusID = GlobalVariable.objCheckInfo.TableStatusID;
                objENTOrder.OrderActions = GlobalVariable.objCheckInfo.OrderActions;
                objENTOrder.OrderSpecialRequest = txtSpecReqForOrder.Text.Trim();
                objENTOrder.RUserID = new Guid(GlobalVariable.BranchID);
                objENTOrder.RUserType = GlobalVariable.RUserType;
                objENTOrder.StartTime = GlobalVariable.IsDate(txtDateTime.Text) == true ? GlobalVariable.ChangeDateTime(txtDateTime.Text) : "NULL";
                objENTOrder.OrderStatus = Convert.ToInt32(GlobalVariable.OrderStatus.OPEN);
                objENTOrder.IsPrint = Convert.ToInt32(GlobalVariable.PrintStatus.NotPrinted);
                objENTOrder.Mode = strMode;
                if (objDALOrder.InsertUpdateDeleteOrder(objENTOrder))
                {
                    strMode = "UPDATE";
                    strModeDetail = "ADD";
                    this.EnableButton(true, true, true, true);
                    btnCancel.Visible = true;
                    result = true;
                }
                else
                {
                    result = false;
                }
                if (result)
                {
                    //MessageBox.Show("Order is generated.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Problem in Send KDS", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddCartItem(Guid ProductID)
        {
            try
            {
                if (strModeDetail == "ADD" && strMode == "UPDATE")
                {
                    ENT.Transaction objENTTrans = new ENT.Transaction();
                    DAL.Transaction objDALTrans = new DAL.Transaction();

                    ENT.CategoryWiseProduct objENTProduct = new ENT.CategoryWiseProduct();
                    DAL.CategoryWiseProduct objDALProduct = new DAL.CategoryWiseProduct();
                    List<ENT.CategoryWiseProduct> lstENTProduct = new List<ENT.CategoryWiseProduct>();

                    objENTProduct.Mode = "GetRecordByProductID";
                    objENTProduct.ProductID = ProductID;
                    lstENTProduct = objDALProduct.getCategoryWiseProduct(objENTProduct);
                    if (lstENTProduct.Count > 0)
                    {
                        objENTTrans.TransactionID = Guid.NewGuid(); 
                        objENTTrans.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                        objENTTrans.OrderID = new Guid(txtOrderId.Text);
                        objENTTrans.CategoryID = lstENTProduct[0].CategoryID; 
                        objENTTrans.ProductID = lstENTProduct[0].ProductID;  
                        objENTTrans.Quantity = txtSearchQty.Text == string.Empty ? 1 : Convert.ToInt32(txtSearchQty.Text);
                        objENTTrans.Rate = lstENTProduct[0].Price; 
                        objENTTrans.TotalAmount = lstENTProduct[0].Price; 
                        objENTTrans.Sort = intSort;
                        objENTTrans.RUserID = new Guid(GlobalVariable.BranchID);
                        objENTTrans.RUserType = GlobalVariable.RUserType;
                        objENTTrans.StartDate = GlobalVariable.IsDate(txtDateTime.Text) == true ? GlobalVariable.ChangeDateTime(txtDateTime.Text) : "NULL";
                        objENTTrans.EndDate = GlobalVariable.IsDate(txtDateTime.Text) == true ? GlobalVariable.ChangeDateTime(txtDateTime.Text) : "NULL";
                        objENTTrans.Mode = strModeDetail;
                        if (objDALTrans.InsertUpdateDeleteOrderTransaction(objENTTrans))
                        {
                            this.GetCartItemInList(txtOrderId.Text.Trim());
                            this.UpdateOrderTransaction();
                            dgvItem.CurrentCell = dgvItem.Rows[intSort].Cells["ordQty"];
                            dgvItem.Focus();
                            intSort += 1;
                        }
                        else
                        {
                            MessageBox.Show("Problem in update order item.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Order must be generated before you add item.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetOrderTransactionDataForEdit(string orderID)
        {
            DAL.OrderBook objDALOrder = new DAL.OrderBook();
            ENT.OrderBook objENTOrder = new ENT.OrderBook();
            List<ENT.OrderBook> lstENTOrder = new List<ENT.OrderBook>();

            objENTOrder.Mode = "GetRecordByOrderID";
            objENTOrder.OrderID = new Guid(orderID);
            lstENTOrder = objDALOrder.getOrder(objENTOrder);
            if (lstENTOrder.Count > 0)
            {
                txtOrderId.Text = Convert.ToString(lstENTOrder[0].OrderID);
                txtInvoiceNo.Text = Convert.ToString(lstENTOrder[0].OrderNo);
                txtDateTime.Text = lstENTOrder[0].OrderDate != null ? Convert.ToDateTime(lstENTOrder[0].OrderDate).ToString("dd/MM/yyyy hh:mm tt") : DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                txtCustId.Text = Convert.ToString(lstENTOrder[0].CustomerID);
                txtCustName.Text = Convert.ToString(lstENTOrder[0].Name);
                txtTableId.Text = Convert.ToString(lstENTOrder[0].TableID);

                GlobalVariable.objCheckInfo.DelieveryType = lstENTOrder[0].DeliveryType;
                GlobalVariable.objCheckInfo.DelieveryTypeName = lstENTOrder[0].DeliveryTypeName;
                GlobalVariable.objCheckInfo.TableID = lstENTOrder[0].TableID;

                if (lstENTOrder[0].DeliveryType == Convert.ToInt32(GlobalVariable.DeliveryType.DineIn))
                {
                    txtTableNo.Text = "Type : " + lstENTOrder[0].DeliveryTypeName;
                    ENT.MergeTable objENTMT = new ENT.MergeTable();
                    List<ENT.MergeTable> lstENTMT = new List<ENT.MergeTable>();
                    DAL.MergeTable objDALMT = new DAL.MergeTable();
                    objENTMT.OrderID = new Guid(txtOrderId.Text.Trim());
                    objENTMT.Mode = "GetTableByOrderID";
                    lstENTMT = objDALMT.getMergeTable(objENTMT);
                    for (int i = 0; i < lstENTMT.Count; i++)
                    {
                        txtTableNo.Text = txtTableNo.Text + " / " + lstENTMT[i].TableName;
                    }

                }
                else
                    txtTableNo.Text = "Type : " + lstENTOrder[0].DeliveryTypeName;

                txtSubTotal.Text = Convert.ToString(lstENTOrder[0].SubTotal);
                txtExtraCharge.Text = Convert.ToString(lstENTOrder[0].ExtraCharge);
                lblTax1.Text = Convert.ToString(lstENTOrder[0].TaxLabel1);
                txtSGSTPer.Text = Convert.ToString(lstENTOrder[0].TaxPercent1);
                txtSGST.Text = Convert.ToString(lstENTOrder[0].SGSTAmount);
                lblTax2.Text = Convert.ToString(lstENTOrder[0].TaxLabel2);
                txtCGSTPer.Text = Convert.ToString(lstENTOrder[0].TaxPercent2);
                txtCGST.Text = Convert.ToString(lstENTOrder[0].CGSTAmount);
                txtTotalTax.Text = Convert.ToString(lstENTOrder[0].TotalTax);
                if (lstENTOrder[0].DiscountType == 1)
                {
                    lblDiscount.Text = "Discount (Amt.): ";
                }
                else if (lstENTOrder[0].DiscountType == 2)
                {
                    lblDiscount.Text = "Discount (" + Convert.ToString(lstENTOrder[0].DiscountPer) + "%): ";
                }
                else
                {
                    lblDiscount.Text = "Discount: ";
                }
                txtDiscountType.Text = Convert.ToString(lstENTOrder[0].DiscountType);
                txtDiscountPer.Text = Convert.ToString(lstENTOrder[0].DiscountPer);
                txtDiscount.Text = Convert.ToString(lstENTOrder[0].Discount);
                txtTip.Text = Convert.ToString(lstENTOrder[0].TipGratuity);
                txtPayAmt.Text = Convert.ToString(lstENTOrder[0].PayableAmount);
                txtSpecReqForOrder.Text = lstENTOrder[0].OrderSpecialRequest;
                txtDeliveryCharge.Text = Convert.ToString(lstENTOrder[0].DeliveryCharge);
                this.GetCartItemInList(orderID);
                intSort = dgvItem.Rows.Count == 0 ? 0 : dgvItem.Rows.Count - 1;
                strMode = "UPDATE";
                strModeDetail = "ADD";
            }
        }

        private void GetCartItemInList(string orderID)
        {
            try
            {
                IsQtyCellChange = false;
                IsRateCellChange = false;
                DAL.Transaction objDALTrans = new DAL.Transaction();
                ENT.Transaction objENTTrans = new ENT.Transaction();
                List<ENT.Transaction> lstENTTrans = new List<ENT.Transaction>();

                objENTTrans.Mode = "GetRecordByOrderID";
                objENTTrans.OrderID = new Guid(orderID);
                lstENTTrans = objDALTrans.getOrderTransaction(objENTTrans);

                dgvItem.Rows.Clear();
                for (int i = 0; i < lstENTTrans.Count; i++)
                {
                    dgvItem.Rows.Add();
                    string strSpecReq = lstENTTrans[i].SpecialRequest == null ? "" : "Spec. Req.: " + lstENTTrans[i].SpecialRequest;
                    dgvItem.Rows[i].Cells["ordTransID"].Value = lstENTTrans[i].TransactionID.ToString();
                    dgvItem.Rows[i].Cells["ordCategoryID"].Value = lstENTTrans[i].CategoryID;
                    dgvItem.Rows[i].Cells["ordProdID"].Value = lstENTTrans[i].ProductID;
                    dgvItem.Rows[i].Cells["ordItemName"].Value = lstENTTrans[i].ProductName + Environment.NewLine + strSpecReq;
                    dgvItem.Rows[i].Cells["ordQty"].Value = lstENTTrans[i].Quantity;
                    dgvItem.Rows[i].Cells["ordRate"].Value = lstENTTrans[i].Rate;
                    dgvItem.Rows[i].Cells["ordTotal"].Value = lstENTTrans[i].TotalAmount;
                    dgvItem.Rows[i].Cells["ordMode"].Value = "UPDATE";
                }
                this.CalcTotal();
                IsQtyCellChange = true;
                IsRateCellChange = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetOrderTaxDiscount(string orderID)
        {
            try
            {
                DAL.OrderBook objDALOrder = new DAL.OrderBook();
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                List<ENT.OrderBook> lstENTOrder = new List<ENT.OrderBook>();

                objENTOrder.Mode = "GetRecordByOrderID";
                objENTOrder.OrderID = new Guid(orderID);
                lstENTOrder = objDALOrder.getOrder(objENTOrder);
                if (lstENTOrder.Count > 0)
                {
                    txtSubTotal.Text = Convert.ToString(lstENTOrder[0].SubTotal);
                    txtExtraCharge.Text = Convert.ToString(lstENTOrder[0].ExtraCharge);
                    lblTax1.Text = lstENTOrder[0].TaxLabel1;
                    txtSGSTPer.Text = Convert.ToString(lstENTOrder[0].TaxPercent1);
                    txtSGST.Text = Convert.ToString(lstENTOrder[0].SGSTAmount);
                    lblTax2.Text = lstENTOrder[0].TaxLabel2;
                    txtCGSTPer.Text = Convert.ToString(lstENTOrder[0].TaxPercent2);
                    txtCGST.Text = Convert.ToString(lstENTOrder[0].CGSTAmount);
                    txtTotalTax.Text = Convert.ToString(lstENTOrder[0].TotalTax);
                    if (lstENTOrder[0].DiscountType == 1)
                    {
                        lblDiscount.Text = "Discount (Amt.): ";
                    }
                    else if (lstENTOrder[0].DiscountType == 2)
                    {
                        lblDiscount.Text = "Discount (" + Convert.ToString(lstENTOrder[0].DiscountPer) + "%): ";
                    }
                    else
                    {
                        lblDiscount.Text = "Discount: ";
                    }
                    txtDiscountType.Text = Convert.ToString(lstENTOrder[0].DiscountType);
                    txtDiscountPer.Text = Convert.ToString(lstENTOrder[0].DiscountPer);
                    txtDiscount.Text = Convert.ToString(lstENTOrder[0].Discount);
                    txtTip.Text = Convert.ToString(lstENTOrder[0].TipGratuity);
                    txtDeliveryCharge.Text = Convert.ToString(lstENTOrder[0].DeliveryCharge);
                    txtPayAmt.Text = Convert.ToString(lstENTOrder[0].PayableAmount);
                    this.CalcTotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateOrderTransaction()
        {
            try
            {
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                DAL.OrderBook objDALOrder = new DAL.OrderBook();
                ENT.Transaction objENTTrans = new ENT.Transaction();
                DAL.Transaction objDALTrans = new DAL.Transaction();

                if (txtInvoiceNo.Text.Length == 0)
                {
                    MessageBox.Show("Invoice number should not blank.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (!GlobalVariable.IsDate(txtDateTime.Text))
                {
                    MessageBox.Show("Select valid date and time.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (txtCustId.Text.Length == 0)
                {
                    MessageBox.Show("Select valid customer.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (GlobalVariable.objCheckInfo.DelieveryType <= 0)
                {
                    MessageBox.Show("Delivery type is not valid.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (dgvItem.Rows.Count <= 0)
                {
                    MessageBox.Show("Atleast one item must be in cart.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                objENTOrder.OrderID = new Guid(txtOrderId.Text);
                objENTOrder.OrderNo = txtInvoiceNo.Text.Trim();
                objENTOrder.OrderDate = GlobalVariable.IsDate(txtDateTime.Text) == true ? GlobalVariable.ChangeDateTime(txtDateTime.Text) : "NULL";
                objENTOrder.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTOrder.CustomerID = new Guid(txtCustId.Text);
                objENTOrder.DeliveryType = GlobalVariable.objCheckInfo.DelieveryType;
                objENTOrder.DeliveryTypeName = GlobalVariable.objCheckInfo.DelieveryTypeName;
                objENTOrder.TableID = GlobalVariable.objCheckInfo.TableID;
                objENTOrder.TableStatusID = GlobalVariable.objCheckInfo.TableStatusID;
                objENTOrder.SubTotal = Convert.ToDecimal(txtSubTotal.Text);
                objENTOrder.ExtraCharge = txtExtraCharge.Text.Trim().Length > 0 ? Convert.ToDecimal(txtExtraCharge.Text) : 0;
                objENTOrder.TaxLabel1 = lblTax1.Text.Trim();
                objENTOrder.TaxPercent1 = Convert.ToDecimal(txtSGSTPer.Text);
                objENTOrder.SGSTAmount = Convert.ToDecimal(txtSGST.Text);
                objENTOrder.TaxLabel2 = lblTax2.Text.Trim();
                objENTOrder.TaxPercent2 = Convert.ToDecimal(txtCGSTPer.Text);
                objENTOrder.CGSTAmount = Convert.ToDecimal(txtCGST.Text);
                objENTOrder.TotalTax = Convert.ToDecimal(txtTotalTax.Text);
                objENTOrder.DiscountPer = txtDiscountPer.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDiscountPer.Text) : 0;
                objENTOrder.Discount = txtDiscount.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDiscount.Text) : 0;
                objENTOrder.TipGratuity = txtTip.Text.Trim().Length > 0 ? Convert.ToDecimal(txtTip.Text) : 0;
                objENTOrder.PayableAmount = Convert.ToDecimal(txtPayAmt.Text);
                objENTOrder.OrderSpecialRequest = txtSpecReqForOrder.Text.Trim();
                objENTOrder.DeliveryCharge = txtDeliveryCharge.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDeliveryCharge.Text) : 0;
                objENTOrder.Mode = strMode;
                if (objDALOrder.InsertUpdateDeleteOrder(objENTOrder))
                {
                    for (int i = 0; i < dgvItem.Rows.Count; i++)
                    {
                        objENTTrans.TransactionID = new Guid(dgvItem.Rows[i].Cells["ordTransID"].Value.ToString());
                        objENTTrans.OrderID = new Guid(txtOrderId.Text);
                        objENTTrans.CategoryID = new Guid(dgvItem.Rows[i].Cells["ordCategoryID"].Value.ToString());
                        objENTTrans.ProductID = new Guid(dgvItem.Rows[i].Cells["ordProdID"].Value.ToString());
                        objENTTrans.Quantity = Convert.ToInt32(dgvItem.Rows[i].Cells["ordQty"].Value);
                        objENTTrans.Rate = Convert.ToDecimal(dgvItem.Rows[i].Cells["ordRate"].Value);
                        objENTTrans.TotalAmount = Convert.ToDecimal(dgvItem.Rows[i].Cells["ordTotal"].Value);
                        objENTTrans.Sort = i;
                        objENTTrans.Mode = dgvItem.Rows[i].Cells["ordMode"].Value.ToString();
                        if (!objDALTrans.InsertUpdateDeleteOrderTransaction(objENTTrans))
                        {
                            MessageBox.Show("Problem in update order item.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Problem in update order.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateOrder()
        {
            try
            {
                ENT.OrderBook objENTOrder = new ENT.OrderBook();
                DAL.OrderBook objDALOrder = new DAL.OrderBook();

                if (txtInvoiceNo.Text.Length == 0)
                {
                    MessageBox.Show("Invoice number should not blank.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (!GlobalVariable.IsDate(txtDateTime.Text))
                {
                    MessageBox.Show("Select valid date and time.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (txtCustId.Text.Length == 0)
                {
                    MessageBox.Show("Select valid customer.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (GlobalVariable.objCheckInfo.DelieveryType <= 0)
                {
                    MessageBox.Show("Delivery type is not valid.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                objENTOrder.OrderID = new Guid(txtOrderId.Text);
                objENTOrder.OrderNo = txtInvoiceNo.Text.Trim();
                objENTOrder.OrderDate = GlobalVariable.IsDate(txtDateTime.Text) == true ? GlobalVariable.ChangeDateTime(txtDateTime.Text) : "NULL";
                objENTOrder.EmployeeID = new Guid(GlobalVariable.EmployeeID);
                objENTOrder.CustomerID = new Guid(txtCustId.Text);
                objENTOrder.DeliveryType = GlobalVariable.objCheckInfo.DelieveryType;
                objENTOrder.DeliveryTypeName = GlobalVariable.objCheckInfo.DelieveryTypeName;
                objENTOrder.TableID = GlobalVariable.objCheckInfo.TableID;
                objENTOrder.TableStatusID = GlobalVariable.objCheckInfo.TableStatusID;
                objENTOrder.SubTotal = txtSubTotal.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtSubTotal.Text);
                objENTOrder.ExtraCharge = txtExtraCharge.Text.Trim().Length > 0 ? Convert.ToDecimal(txtExtraCharge.Text) : 0;
                objENTOrder.TaxLabel1 = lblTax1.Text.Trim();
                objENTOrder.TaxPercent1 = txtSGSTPer.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtSGSTPer.Text);
                objENTOrder.SGSTAmount = txtSGST.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtSGST.Text);
                objENTOrder.TaxLabel2 = lblTax2.Text.Trim();
                objENTOrder.TaxPercent2 = txtCGSTPer.Text == "" ? 0 : Convert.ToDecimal(txtCGSTPer.Text);
                objENTOrder.CGSTAmount = txtCGST.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtCGST.Text);
                objENTOrder.TotalTax = txtTotalTax.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtTotalTax.Text);
                objENTOrder.DiscountPer = txtDiscountPer.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDiscountPer.Text) : 0;
                objENTOrder.Discount = txtDiscount.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtDiscount.Text);
                objENTOrder.TipGratuity = txtTip.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtTip.Text);
                objENTOrder.PayableAmount = txtPayAmt.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtPayAmt.Text);
                objENTOrder.OrderSpecialRequest = txtSpecReqForOrder.Text.Trim();
                objENTOrder.DeliveryCharge = txtDeliveryCharge.Text.Trim().Length > 0 ? Convert.ToDecimal(txtDeliveryCharge.Text) : 0;
                objENTOrder.Mode = strMode;
                objDALOrder.InsertUpdateDeleteOrder(objENTOrder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region ALL Button Event Main Screen

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOrderId.Text.Trim()))
                {
                    MessageBox.Show("Order not found for payment.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (GetOrderStatus(txtOrderId.Text.Trim()) == Convert.ToInt32(GlobalVariable.OrderActions.Paid))
                {
                    MessageBox.Show("Order already paid.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (GetOrderStatus(txtOrderId.Text.Trim()) == Convert.ToInt32(GlobalVariable.OrderActions.Pay))
                {
                    if (txtPayAmt.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Payable amount should not be empty.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (Convert.ToDecimal(txtPayAmt.Text) <= 0)
                    {
                        MessageBox.Show("Payable amount must be greater than zero.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (txtOrderId.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Please create new order.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    ENT.PaymentDetail pmt = new ENT.PaymentDetail();
                    pmt.OrderID = txtOrderId.Text.Trim();
                    pmt.CustomerID = txtCustId.Text.Trim();
                    pmt.TableID = txtTableId.Text.Trim();
                    pmt.PayAmount = Convert.ToDecimal(txtPayAmt.Text);
                    pmt.SumSubTotal = Convert.ToDecimal(txtSubTotal.Text);
                    pmt.ExtraCharge = Convert.ToDecimal(txtExtraCharge.Text);
                    pmt.Tax1 = Convert.ToDecimal(txtSGSTPer.Text);
                    pmt.Tax2 = Convert.ToDecimal(txtCGSTPer.Text);
                    pmt.SumTaxTotal = Convert.ToDecimal(txtTotalTax.Text);
                    pmt.OTTip = Convert.ToDecimal(txtTip.Text);
                    pmt.DiscountType = txtDiscountType.Text != string.Empty ? Convert.ToInt32(txtDiscountType.Text) : 1;
                    pmt.DiscountPer = txtDiscountPer.Text != string.Empty ? Convert.ToDecimal(txtDiscountPer.Text) : 0;
                    pmt.DiscManual = txtDiscount.Text != string.Empty ? Convert.ToDecimal(txtDiscount.Text) : 0;
                    pmt.SalesAmountTotal = Convert.ToDecimal(txtPayAmt.Text);

                    frmCheckOut frmCO = new frmCheckOut(pmt);
                    frmCO.ShowDialog();

                    if (GetOrderStatus(txtOrderId.Text.Trim()) == Convert.ToInt32(GlobalVariable.OrderActions.Paid))
                        this.ClearData();
                    else
                        GetOrderTaxDiscount(txtOrderId.Text.Trim());
                }
                else if (GetOrderStatus(txtOrderId.Text.Trim()) == Convert.ToInt32(GlobalVariable.OrderActions.Cancel))
                {
                    MessageBox.Show("Order is canceled.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Order not found.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectCust_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                {
                    if (txtInvoiceNo.Text.Trim() == GetOrderNo().Trim())
                    {
                        MessageBox.Show("Duplicate Order Number Found.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                IsRefreshMenuPanel = false;
                this.ClearData();
                frmCustomerMaster frmCustList = new frmCustomerMaster();
                frmCustList.ShowDialog();
                if (GlobalVariable.IsOk)
                {
                    txtInvoiceNo.Text = GetOrderNo();
                    txtDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");

                    if (GlobalVariable.objCheckInfo.DelieveryType == Convert.ToInt32(GlobalVariable.DeliveryType.DineIn))
                    {
                        txtCustId.Text = GlobalVariable.objCheckInfo.CustomerID.ToString();
                        txtCustName.Text = GlobalVariable.objCheckInfo.CustomerName.ToString();
                        txtTableId.Text = Convert.ToString(GlobalVariable.objCheckInfo.TableID);
                        txtTableNo.Text = "Type : " + Convert.ToString(GlobalVariable.objCheckInfo.DelieveryTypeName) + " - " + Convert.ToString(GlobalVariable.objCheckInfo.TableName);
                    }
                    else
                    {
                        txtCustId.Text = GlobalVariable.objCheckInfo.CustomerID.ToString();
                        txtCustName.Text = GlobalVariable.objCheckInfo.CustomerName.ToString();
                        txtTableNo.Text = "Type : " + Convert.ToString(GlobalVariable.objCheckInfo.DelieveryTypeName);
                    }
                    GlobalVariable.objCheckInfo.OrderActions = Convert.ToInt32(GlobalVariable.OrderActions.Pay);
                    strMode = "ADD";
                    this.GenerateOrder();
                    tcMain.SelectedIndex = 0;
                    pnlSearch.Visible = true;
                    pnlModifier.Visible = false;
                    this.GetNewOrderDetailForSocket();
                }
                IsRefreshMenuPanel = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWalkIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                {
                    if (txtInvoiceNo.Text.Trim() == GetOrderNo().Trim())
                    {
                        MessageBox.Show("Duplicate Order Number Found.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                IsRefreshMenuPanel = false;
                this.ClearData();
                GlobalVariable.objCheckInfo = new ENT.CheckInfo();
                DAL.CustomerMasterData objDALCustomers = new DAL.CustomerMasterData();
                ENT.CustomerMasterData objENTCustomers = new ENT.CustomerMasterData();
                List<ENT.CustomerMasterData> lstENTCustomers = new List<ENT.CustomerMasterData>();

                objENTCustomers.Mode = "GetAllRecord";
                lstENTCustomers = objDALCustomers.getCustomerData(objENTCustomers);
                lstENTCustomers = lstENTCustomers.Where(cust => cust.Name.ToLower() == "guest").ToList();

                if (lstENTCustomers.Count > 0)
                {
                    txtInvoiceNo.Text = GetOrderNo();
                    txtDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");

                    txtCustId.Text = lstENTCustomers[0].CustomerID.ToString();
                    txtCustName.Text = lstENTCustomers[0].Name.ToString();
                    txtTableNo.Text = "Type : Take Out";
                    txtTableId.Text = "00000000-0000-0000-0000-000000000000";

                    GlobalVariable.objCheckInfo.CustomerID = lstENTCustomers[0].CustomerID;
                    GlobalVariable.objCheckInfo.CustomerName = lstENTCustomers[0].Name;
                    GlobalVariable.objCheckInfo.DelieveryType = Convert.ToInt32(GlobalVariable.DeliveryType.TakeOut);
                    GlobalVariable.objCheckInfo.DelieveryTypeName = "Take Out";
                    GlobalVariable.objCheckInfo.TableID = new Guid("00000000-0000-0000-0000-000000000000");
                    GlobalVariable.objCheckInfo.TableName = "";
                    GlobalVariable.objCheckInfo.OrderActions = Convert.ToInt32(GlobalVariable.OrderActions.Pay);
                    strMode = "ADD";
                    this.GenerateOrder();
                    tcMain.SelectedIndex = 0;
                    pnlSearch.Visible = true;
                    pnlModifier.Visible = false;
                    
                    // send order data to connected client
                    GetNewOrderDetailForSocket();
                }
                else
                {
                    MessageBox.Show("Guest not found.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                IsRefreshMenuPanel = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure! You want to cancel order ?", "Cancel Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DAL.OrderBook objDALOB = new DAL.OrderBook();
                    ENT.OrderBook objENTOB = new ENT.OrderBook();
                    objENTOB.OrderID = new Guid(txtOrderId.Text);
                    objENTOB.TableID = new Guid(txtTableId.Text);
                    objENTOB.TableStatusID = Convert.ToInt32(GlobalVariable.TableStatus.Vacant);
                    objENTOB.OrderActions = Convert.ToInt32(GlobalVariable.OrderActions.Cancel);
                    objENTOB.OrderStatus = Convert.ToInt32(GlobalVariable.OrderStatus.CANCEL);
                    objENTOB.Mode = "UPDATE_ORDER_STATUS";
                    if (objDALOB.InsertUpdateDeleteOrder(objENTOB))
                    {
                        MessageBox.Show("Order Cancelled Successfully.", "Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ClearData();
                    }
                    else
                    {
                        MessageBox.Show("Order Is Not Cancel.", "Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            this.VisibleTaxTable();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClearData();
        }

        private void btnModifierBack_Click(object sender, EventArgs e)
        {
            try
            {
                tcMain.SelectedIndex = 0;
                pnlModifier.Visible = false;
                pnlSearch.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantity.Text.Trim() != string.Empty)
                {
                    int intQuantity = Convert.ToInt32(txtQuantity.Text);
                    if (intQuantity >= 0)
                    {
                        intQuantity += 1;
                        txtQuantity.Text = intQuantity.ToString();
                        dgvItem.Rows[Convert.ToInt32(btnPlus.Tag)].Cells["ordQty"].Value = txtQuantity.Text;
                        this.CalcTotal();
                        this.UpdateOrderTransaction();
                    }
                }
                else
                {
                    txtQuantity.Text = "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantity.Text.Trim() != string.Empty)
                {
                    int intQuantity = Convert.ToInt32(txtQuantity.Text);
                    if (intQuantity > 1)
                    {
                        intQuantity -= 1;
                        txtQuantity.Text = intQuantity.ToString();
                        dgvItem.Rows[Convert.ToInt32(btnPlus.Tag)].Cells["ordQty"].Value = txtQuantity.Text;
                        this.CalcTotal();
                        this.UpdateOrderTransaction();
                    }
                }
                else
                {
                    txtQuantity.Text = "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvItem.Rows.Count > 0)
                {
                    DeleteCartItem();
                }
                else
                {
                    MessageBox.Show("No Item Found For Delete.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnModifiers_Click(object sender, EventArgs e)
        {
            DAL.ModifierCategoryDetail objDALMCD = new DAL.ModifierCategoryDetail();
            ENT.ModifierCategoryDetail objENTMCD = new ENT.ModifierCategoryDetail();
            List<ENT.ModifierCategoryDetail> lstENTMCD = new List<ENT.ModifierCategoryDetail>();

            objENTMCD.Mode = "GetRecordByProductID";
            objENTMCD.ProductID = new Guid(txtProdID.Text.Trim());
            lstENTMCD = objDALMCD.getModifierCategoryDetail(objENTMCD);

            if (lstENTMCD.Count == 0)
            {
                MessageBox.Show("Modifiers category not found for selected product.", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (txtOrderId.Text.Trim() != string.Empty && txtTransID.Text.Trim() != string.Empty && txtProdID.Text.Trim() != string.Empty)
            {
                frmModifiers frmIU = new frmModifiers(txtOrderId.Text.Trim(), txtTransID.Text.Trim(), txtProdID.Text.Trim());
                frmIU.ShowDialog();
                txtExtraCharge.Text = Convert.ToString(GlobalVariable.decModifierAmount);
                this.CalcTotal();
                this.UpdateOrder();
            }
        }

        private void btnSpecialRequestSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOrderId.Text.Trim() != string.Empty && txtTransID.Text.Trim() != string.Empty)
                {
                    ENT.Transaction objENTTrans = new ENT.Transaction();
                    DAL.Transaction objDALTrans = new DAL.Transaction();

                    objENTTrans.OrderID = new Guid(txtOrderId.Text);
                    objENTTrans.TransactionID = new Guid(txtTransID.Text);
                    objENTTrans.SpecialRequest = txtSpecialRequestItem.Text.Trim();
                    objENTTrans.Mode = "UPDATE_SP_REQ";
                    if (objDALTrans.InsertUpdateDeleteOrderTransaction(objENTTrans))
                    {
                        txtSpecialRequestItem.Text = "";
                        //notifyIcon1.BalloonTipText = "Special Request Saved Successfully.";
                        //notifyIcon1.ShowBalloonTip(2000);
                    }
                    this.GetCartItemInList(txtOrderId.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSendToKDS_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsCount = dgvItem.Rows.Count;
                if (rowsCount > 0)
                {
                    PrintReceipt pr = new PrintReceipt(txtOrderId.Text.Trim());
                    pr.PrintSendToKDS();
                }
                else
                {
                    MessageBox.Show("Cart must have atleast one item.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDateWiseItemSale_Click(object sender, EventArgs e)
        {
            try
            {
                frmItemWiseSalesReport frmIWSR = new frmItemWiseSalesReport();
                frmIWSR.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnInward_Click(object sender, EventArgs e)
        {
            frmInwardView frmIV = new frmInwardView();
            frmIV.ShowDialog();
        }

        private void btnOutward_Click(object sender, EventArgs e)
        {
            frmOutwardView frmOV = new frmOutwardView();
            frmOV.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            frmDateWiseStock frmDWS = new frmDateWiseStock();
            frmDWS.ShowDialog();
        }

        private void btnVendorMaster_Click(object sender, EventArgs e)
        {
            frmVendorMaster frmVM = new frmVendorMaster();
            frmVM.ShowDialog();
        }

        #endregion

        #region TextBox Events

        private void txtExtraCharge_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (txtExtraCharge.Text.Trim().Length > 0)
                //{
                //    if (!GlobalVariable.IsNumeric(txtExtraCharge.Text))
                //    {
                //        MessageBox.Show("Enter numeric value only.");
                //        txtDiscount.Focus();
                //        return;
                //    }
                //}
                //this.CalcTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtExtraCharge_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtExtraCharge.Text.Trim() == string.Empty)
                {
                    txtExtraCharge.Text = "0";
                }
                if (GlobalVariable.IsNumeric(txtExtraCharge.Text))
                {
                    this.CalcTotal();
                    this.UpdateOrder();
                }
                else
                {
                    MessageBox.Show("Enter numeric value only.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtExtraCharge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtExtraCharge.Text.Contains(".")) && (e.KeyChar == '.'))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (txtDiscount.Text.Trim().Length > 0)
                //{
                //    if (!GlobalVariable.IsNumeric(txtDiscount.Text))
                //    {
                //        MessageBox.Show("Enter numeric value only.");
                //        txtDiscount.Focus();
                //        return;
                //    }
                //}
                //this.CalcTotal();
                //txtDiscount.SelectionStart = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtDiscount.Text.Trim() == string.Empty)
                {
                    txtDiscount.Text = "0";
                }
                if (GlobalVariable.IsNumeric(txtDiscount.Text))
                {
                    this.CalcTotal();
                    this.UpdateOrder();
                }
                else
                {
                    MessageBox.Show("Enter numeric value only.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtDiscount.Text.Contains(".")) && (e.KeyChar == '.'))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtTip_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (txtTip.Text.Trim().Length > 0)
                //{
                //    if (!GlobalVariable.IsNumeric(txtTip.Text))
                //    {
                //        MessageBox.Show("Enter numeric value only.");
                //        txtTip.Focus();
                //        return;
                //    }
                //}
                //this.CalcTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtTip_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtTip.Text.Trim() == string.Empty)
                {
                    txtTip.Text = "0";
                }
                if (GlobalVariable.IsNumeric(txtTip.Text))
                {
                    this.CalcTotal();
                    this.UpdateOrder();
                }
                else
                {
                    MessageBox.Show("Enter numeric value only.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtTip.Text.Contains(".")) && (e.KeyChar == '.'))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtSpecReqForOrder_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtInvoiceNo.Text.Length > 0)
                {
                    this.UpdateOrder();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        
        #endregion

        #region Datagridview Event

        private void dgvItem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);

                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.ContextMenuStrip = new ContextMenuStrip();
                    tb.KeyDown -= TextBox_KeyDown;
                    tb.KeyDown += TextBox_KeyDown;
                }
            }
            catch { }
        }

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C | e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void dgvItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (dgvItem.Rows.Count > 0)
                    {
                        DeleteCartItem();
                        //if (MessageBox.Show("Are you sure! You want to delete selected item ?", "Item Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        //{
                        //    return;
                        //}

                        //DAL.Transaction objDALTrans = new DAL.Transaction();
                        //ENT.Transaction objENTTrans = new ENT.Transaction();
                        //List<ENT.Transaction> lstENTTrans = new List<ENT.Transaction>();

                        //objENTTrans.Mode = "DELETE";
                        //objENTTrans.OrderID = new Guid(txtOrderId.Text.Trim());
                        //objENTTrans.TransactionID = new Guid(dgvItem.Rows[dgvItem.CurrentRow.Index].Cells["ordTransID"].Value.ToString());
                        //if (objDALTrans.InsertUpdateDeleteOrderTransaction(objENTTrans))
                        //{
                        //    this.GetCartItemInList(txtOrderId.Text.Trim());
                        //    this.UpdateOrderTransaction();
                        //    if (intSort != 0)
                        //    { intSort -= 1; }

                        //}
                    }
                    else
                    {
                        MessageBox.Show("No Item Found For Delete.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //ordRate
                if (IsQtyCellChange)
                {
                    #region Quantity Cell Change
                    decimal qty = 0;
                    decimal rate = 0;

                    if (dgvItem.Rows.Count > 0 && dgvItem.Columns[e.ColumnIndex].Name == "ordQty")
                    {
                        if (dgvItem.Rows[e.RowIndex].Cells["ordQty"].Value != null)
                        {
                            if (dgvItem.Rows[e.RowIndex].Cells["ordQty"].Value.ToString().Length != 0)
                            {
                                qty = Convert.ToDecimal(dgvItem.Rows[e.RowIndex].Cells["ordQty"].Value.ToString());
                                txtQuantity.Text = qty.ToString();
                                if (qty == 0)
                                {
                                    IsQtyCellChange = false;
                                    dgvItem.Rows[e.RowIndex].Cells["ordQty"].Value = 1;
                                    txtQuantity.Text = "1";
                                    MessageBox.Show("Quantity must be greater than zero. Default quantity is one.");
                                    IsQtyCellChange = true;
                                }
                            }
                            else
                            {
                                dgvItem.Rows[e.RowIndex].Cells["ordQty"].Value = 1;
                                txtQuantity.Text = "1";
                                MessageBox.Show("Quantity must be greater than zero. Default quantity is one.");
                                return;
                            }
                        }
                        if (dgvItem.Rows[e.RowIndex].Cells["ordRate"].Value != null)
                        {
                            if (dgvItem.Rows[e.RowIndex].Cells["ordRate"].Value.ToString().Length != 0)
                            {
                                rate = Convert.ToDecimal(dgvItem.Rows[e.RowIndex].Cells["ordRate"].Value.ToString());
                            }
                            else
                            {
                                MessageBox.Show("Rate must be greater than zero.");
                                return;
                            }
                        }
                        dgvItem.Rows[e.RowIndex].Cells["ordTotal"].Value = Convert.ToString(qty * rate);
                        this.CalcTotal();
                        this.UpdateOrderTransaction();
                    }
                    #endregion
                }
                if (IsRateCellChange)
                {
                    #region Rate Cell Change
                    decimal qty = 0;
                    decimal rate = 0;
                    if (dgvItem.Rows.Count > 0 && dgvItem.Columns[e.ColumnIndex].Name == "ordRate")
                    {
                        if (dgvItem.Rows[e.RowIndex].Cells["ordRate"].Value != null)
                        {
                            if (dgvItem.Rows[e.RowIndex].Cells["ordRate"].Value.ToString().Length != 0)
                            {
                                rate = Convert.ToDecimal(dgvItem.Rows[e.RowIndex].Cells["ordRate"].Value.ToString());
                                if (rate == 0)
                                {
                                    IsRateCellChange = false;
                                    dgvItem.Rows[e.RowIndex].Cells["ordRate"].Value = 1;
                                    MessageBox.Show("Rate must be greater than zero. Default rate is one.");
                                    IsRateCellChange = true;
                                }
                            }
                            else
                            {
                                dgvItem.Rows[e.RowIndex].Cells["ordRate"].Value = 1;
                                MessageBox.Show("Rate must be greater than zero. Default rate is one.");
                                return;
                            }
                        }
                        if (dgvItem.Rows[e.RowIndex].Cells["ordQty"].Value != null)
                        {
                            if (dgvItem.Rows[e.RowIndex].Cells["ordQty"].Value.ToString().Length != 0)
                            {
                                qty = Convert.ToDecimal(dgvItem.Rows[e.RowIndex].Cells["ordQty"].Value.ToString());
                            }
                            else
                            {
                                MessageBox.Show("Quantity must be greater than zero.");
                                return;
                            }
                        }
                        dgvItem.Rows[e.RowIndex].Cells["ordTotal"].Value = Convert.ToString(qty * rate);
                        this.CalcTotal();
                        this.UpdateOrderTransaction();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string strImgUrl = "";
                if (e.ColumnIndex < 0)
                {
                    return;
                }
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (dgvItem.Rows.Count > 0)
                {
                    txtTransID.Text = dgvItem.Rows[e.RowIndex].Cells["ordTransID"].Value.ToString();
                    txtProdID.Text = dgvItem.Rows[e.RowIndex].Cells["ordProdID"].Value.ToString();
                    int intQty = Convert.ToInt32(dgvItem.Rows[e.RowIndex].Cells["ordQty"].Value);
                    if (txtTransID.Text.Trim() != string.Empty && txtProdID.Text.Trim() != string.Empty)
                    {
                        ENT.CategoryWiseProduct objENTProd = new ENT.CategoryWiseProduct();
                        List<ENT.CategoryWiseProduct> lstENTProd = new List<ENT.CategoryWiseProduct>();
                        DAL.CategoryWiseProduct objDALProd = new DAL.CategoryWiseProduct();

                        objENTProd.Mode = "GetRecordByProductID";
                        objENTProd.ProductID = new Guid(txtProdID.Text.Trim());
                        lstENTProd = objDALProd.getCategoryWiseProduct(objENTProd);
                        if (lstENTProd.Count > 0)
                        {
                            lblProductName.Text = lstENTProd[0].ProductName;
                            lblProductName.ForeColor = Color.White;
                            txtProductDetail.Text = lstENTProd[0].ShortDescription;
                            strImgUrl = lstENTProd[0].ImgPath.ToString();
                            txtQuantity.Text = intQty.ToString();
                            btnPlus.Tag = e.RowIndex;
                            btnMinus.Tag = e.RowIndex;
                            pnlModifier.Visible = true;
                            pnlSearch.Visible = false;
                            tcMain.SelectedIndex = 0;
                        }
                        else
                        {
                            pnlModifier.Visible = false;
                            pnlSearch.Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("This is not an order.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                string rootPath = Application.StartupPath + "\\ProductImages";
                if (strImgUrl != string.Empty)
                {

                    string strFileName = strImgUrl.Substring(strImgUrl.LastIndexOf("/") + 1);
                    if (strFileName == "")
                        strFileName = lblProductName.Text + ".jpg";

                    string strCombinePath = Path.Combine(rootPath, "not_available.jpg");
                    pbProductImage.ImageLocation = strCombinePath;
                    pbProductImage.BackgroundImageLayout = ImageLayout.Stretch;
                    //if (!File.Exists(strCombinePath))
                    //{
                    //    Image image = DownloadImageFromUrl(strImgUrl.Trim());
                    //    if (image != null)
                    //    {
                    //        image.Save(strCombinePath);
                    //        pbProductImage.Image = image;
                    //        pbProductImage.BackgroundImageLayout = ImageLayout.Stretch;
                    //    }
                    //}
                    //else
                    //{
                    //    pbProductImage.ImageLocation = strCombinePath;
                    //    pbProductImage.BackgroundImageLayout = ImageLayout.Stretch;
                    //}
                }
                else
                {
                    string strCombinePath = Path.Combine(rootPath, "not_available.jpg");
                    pbProductImage.ImageLocation = strCombinePath;
                    pbProductImage.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void odDgvAll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0)
                {
                    return;
                }
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (odDgvAll.Rows.Count > 0 && odDgvAll.Columns[e.ColumnIndex].Name == "AllView")
                {
                    string strOrderId = odDgvAll.Rows[e.RowIndex].Cells["AllOrderID"].Value.ToString();
                    string strOrderAction = odDgvAll.Rows[e.RowIndex].Cells["AllAction"].Value.ToString();
                    if (strOrderId != string.Empty && strOrderAction == "PAY")
                    {
                        this.ClearData();
                        this.GetOrderTransactionDataForEdit(strOrderId);
                        tcMain.SelectedIndex = 0;
                        pnlModifier.Visible = false;
                        pnlSearch.Visible = true;
                        btnCancel.Visible = true;
                        btnCheckOut.Enabled = true;
                        btnCancelOrder.Enabled = true;
                        btnSendToKDS.Enabled = true;
                    }
                    else
                    {
                        frmViewOrderDetail frmVOD = new frmViewOrderDetail(strOrderId);
                        frmVOD.ShowDialog();
                    }
                }
                else if (odDgvAll.Rows.Count > 0 && odDgvAll.Columns[e.ColumnIndex].Name == "AllAction")
                {
                    try
                    {
                        if (GetOrderStatus(odDgvAll.Rows[e.RowIndex].Cells["AllOrderID"].Value.ToString()) == Convert.ToInt32(GlobalVariable.OrderActions.Paid))
                        {
                            MessageBox.Show("Order already paid.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else if (GetOrderStatus(odDgvAll.Rows[e.RowIndex].Cells["AllOrderID"].Value.ToString()) == Convert.ToInt32(GlobalVariable.OrderActions.Pay))
                        {
                            if (odDgvAll.Rows[e.RowIndex].Cells["AllTotal"].Value.ToString() == string.Empty)
                            {
                                MessageBox.Show("Payable amount should not be empty.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (Convert.ToDecimal(odDgvAll.Rows[e.RowIndex].Cells["AllTotal"].Value.ToString()) <= 0)
                            {
                                MessageBox.Show("Payable amount must be greater than zero.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (odDgvAll.Rows[e.RowIndex].Cells["AllOrderID"].Value.ToString() == string.Empty)
                            {
                                MessageBox.Show("Please create new order.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            ENT.PaymentDetail pmt = new ENT.PaymentDetail();
                            pmt.OrderID = odDgvAll.Rows[e.RowIndex].Cells["AllOrderID"].Value.ToString();
                            pmt.PayAmount = Convert.ToDecimal(odDgvAll.Rows[e.RowIndex].Cells["AllTotal"].Value.ToString());

                            frmCheckOut frmCO = new frmCheckOut(pmt);
                            frmCO.ShowDialog();
                            this.TabControlIndexChanged();
                        }
                        else if (GetOrderStatus(odDgvAll.Rows[e.RowIndex].Cells["AllOrderID"].Value.ToString()) == Convert.ToInt32(GlobalVariable.OrderActions.Cancel))
                        {
                            MessageBox.Show("Order is canceled.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Order not found.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void odDgvDineIn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0)
                {
                    return;
                }
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (odDgvDineIn.Rows.Count > 0 && odDgvDineIn.Columns[e.ColumnIndex].Name == "DIView")
                {
                    string strOrderId = odDgvDineIn.Rows[e.RowIndex].Cells["DIOrderID"].Value.ToString();
                    string strOrderAction = odDgvDineIn.Rows[e.RowIndex].Cells["DIAction"].Value.ToString();
                    if (strOrderId != string.Empty && strOrderAction == "PAY")
                    {
                        this.ClearData();
                        this.GetOrderTransactionDataForEdit(strOrderId);
                        tcMain.SelectedIndex = 0;
                        pnlModifier.Visible = false;
                        pnlSearch.Visible = true;
                        btnCancel.Visible = true;
                        btnCheckOut.Enabled = true;
                        btnCancelOrder.Enabled = true;
                        btnSendToKDS.Enabled = true;
                    }
                    else
                    {
                        frmViewOrderDetail frmVOD = new frmViewOrderDetail(strOrderId);
                        frmVOD.ShowDialog();
                    }
                }
                else if (odDgvDineIn.Rows.Count > 0 && odDgvDineIn.Columns[e.ColumnIndex].Name == "DIAction")
                {
                    try
                    {
                        if (GetOrderStatus(odDgvDineIn.Rows[e.RowIndex].Cells["DIOrderID"].Value.ToString()) == Convert.ToInt32(GlobalVariable.OrderActions.Paid))
                        {
                            MessageBox.Show("Order already paid.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else if (GetOrderStatus(odDgvDineIn.Rows[e.RowIndex].Cells["DIOrderID"].Value.ToString()) == Convert.ToInt32(GlobalVariable.OrderActions.Pay))
                        {
                            if (odDgvDineIn.Rows[e.RowIndex].Cells["DITotal"].Value.ToString() == string.Empty)
                            {
                                MessageBox.Show("Payable amount should not be empty.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (Convert.ToDecimal(odDgvDineIn.Rows[e.RowIndex].Cells["DITotal"].Value.ToString()) <= 0)
                            {
                                MessageBox.Show("Payable amount must be greater than zero.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (odDgvDineIn.Rows[e.RowIndex].Cells["DIOrderID"].Value.ToString() == string.Empty)
                            {
                                MessageBox.Show("Please create new order.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            ENT.PaymentDetail pmt = new ENT.PaymentDetail();
                            pmt.OrderID = odDgvDineIn.Rows[e.RowIndex].Cells["DIOrderID"].Value.ToString();
                            pmt.PayAmount = Convert.ToDecimal(odDgvDineIn.Rows[e.RowIndex].Cells["DITotal"].Value.ToString());

                            frmCheckOut frmCO = new frmCheckOut(pmt);
                            frmCO.ShowDialog();
                            this.TabControlIndexChanged();
                        }
                        else if (GetOrderStatus(odDgvDineIn.Rows[e.RowIndex].Cells["DIOrderID"].Value.ToString()) == Convert.ToInt32(GlobalVariable.OrderActions.Cancel))
                        {
                            MessageBox.Show("Order is canceled.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Order not found.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void odDgvTakeOut_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0)
                {
                    return;
                }
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (odDgvTakeOut.Rows.Count > 0 && odDgvTakeOut.Columns[e.ColumnIndex].Name == "TOView")
                {
                    string strOrderId = odDgvTakeOut.Rows[e.RowIndex].Cells["TOOrderID"].Value.ToString();
                    string strOrderAction = odDgvTakeOut.Rows[e.RowIndex].Cells["TOAction"].Value.ToString();
                    if (strOrderId != string.Empty && strOrderAction == "PAY")
                    {
                        this.ClearData();
                        this.GetOrderTransactionDataForEdit(strOrderId);
                        tcMain.SelectedIndex = 0;
                        pnlModifier.Visible = false;
                        pnlSearch.Visible = true;
                        btnCancel.Visible = true;
                        btnCheckOut.Enabled = true;
                        btnCancelOrder.Enabled = true;
                        btnSendToKDS.Enabled = true;
                    }
                    else
                    {
                        frmViewOrderDetail frmVOD = new frmViewOrderDetail(strOrderId);
                        frmVOD.ShowDialog();
                    }
                }
                else if (odDgvTakeOut.Rows.Count > 0 && odDgvTakeOut.Columns[e.ColumnIndex].Name == "TOAction")
                {
                    try
                    {
                        if (GetOrderStatus(odDgvTakeOut.Rows[e.RowIndex].Cells["TOOrderID"].Value.ToString()) == Convert.ToInt32(GlobalVariable.OrderActions.Paid))
                        {
                            MessageBox.Show("Order already paid.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else if (GetOrderStatus(odDgvTakeOut.Rows[e.RowIndex].Cells["TOOrderID"].Value.ToString()) == Convert.ToInt32(GlobalVariable.OrderActions.Pay))
                        {
                            if (odDgvTakeOut.Rows[e.RowIndex].Cells["TOTotal"].Value.ToString() == string.Empty)
                            {
                                MessageBox.Show("Payable amount should not be empty.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (Convert.ToDecimal(odDgvTakeOut.Rows[e.RowIndex].Cells["TOTotal"].Value.ToString()) <= 0)
                            {
                                MessageBox.Show("Payable amount must be greater than zero.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (odDgvTakeOut.Rows[e.RowIndex].Cells["TOOrderID"].Value.ToString() == string.Empty)
                            {
                                MessageBox.Show("Please create new order.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            ENT.PaymentDetail pmt = new ENT.PaymentDetail();
                            pmt.OrderID = odDgvTakeOut.Rows[e.RowIndex].Cells["TOOrderID"].Value.ToString();
                            pmt.PayAmount = Convert.ToDecimal(odDgvTakeOut.Rows[e.RowIndex].Cells["TOTotal"].Value.ToString());

                            frmCheckOut frmCO = new frmCheckOut(pmt);
                            frmCO.ShowDialog();
                            this.TabControlIndexChanged();
                        }
                        else if (GetOrderStatus(odDgvTakeOut.Rows[e.RowIndex].Cells["TOOrderID"].Value.ToString()) == Convert.ToInt32(GlobalVariable.OrderActions.Cancel))
                        {
                            MessageBox.Show("Order is canceled.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Order not found.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void odDgvDelivery_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0)
                {
                    return;
                }
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (odDgvDelivery.Rows.Count > 0 && odDgvDelivery.Columns[e.ColumnIndex].Name == "DelView")
                {
                    string strOrderId = odDgvDelivery.Rows[e.RowIndex].Cells["DelOrderID"].Value.ToString();
                    string strOrderAction = odDgvDelivery.Rows[e.RowIndex].Cells["DelAction"].Value.ToString();
                    if (strOrderId != string.Empty && strOrderAction == "PAY")
                    {
                        this.ClearData();
                        this.GetOrderTransactionDataForEdit(strOrderId);
                        tcMain.SelectedIndex = 0;
                        pnlModifier.Visible = false;
                        pnlSearch.Visible = true;
                        btnCancel.Visible = true;
                        btnCheckOut.Enabled = true;
                        btnCancelOrder.Enabled = true;
                        btnSendToKDS.Enabled = true;
                    }
                    else
                    {
                        frmViewOrderDetail frmVOD = new frmViewOrderDetail(strOrderId);
                        frmVOD.ShowDialog();
                        MessageBox.Show("This is paid order.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (odDgvDelivery.Rows.Count > 0 && odDgvDelivery.Columns[e.ColumnIndex].Name == "DelAction")
                {
                    try
                    {
                        if (GetOrderStatus(odDgvDelivery.Rows[e.RowIndex].Cells["DelOrderID"].Value.ToString()) == Convert.ToInt32(GlobalVariable.OrderActions.Paid))
                        {
                            MessageBox.Show("Order already paid.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else if (GetOrderStatus(odDgvDelivery.Rows[e.RowIndex].Cells["DelOrderID"].Value.ToString()) == Convert.ToInt32(GlobalVariable.OrderActions.Pay))
                        {
                            if (odDgvDelivery.Rows[e.RowIndex].Cells["DelTotal"].Value.ToString() == string.Empty)
                            {
                                MessageBox.Show("Payable amount should not be empty.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (Convert.ToDecimal(odDgvDelivery.Rows[e.RowIndex].Cells["DelTotal"].Value.ToString()) <= 0)
                            {
                                MessageBox.Show("Payable amount must be greater than zero.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (odDgvDelivery.Rows[e.RowIndex].Cells["DelOrderID"].Value.ToString() == string.Empty)
                            {
                                MessageBox.Show("Please create new order.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            ENT.PaymentDetail pmt = new ENT.PaymentDetail();
                            pmt.OrderID = odDgvDelivery.Rows[e.RowIndex].Cells["DelOrderID"].Value.ToString();
                            pmt.PayAmount = Convert.ToDecimal(odDgvDelivery.Rows[e.RowIndex].Cells["DelTotal"].Value.ToString());

                            frmCheckOut frmCO = new frmCheckOut(pmt);
                            frmCO.ShowDialog();
                            this.TabControlIndexChanged();
                        }
                        else if (GetOrderStatus(odDgvDelivery.Rows[e.RowIndex].Cells["TOOrderID"].Value.ToString()) == Convert.ToInt32(GlobalVariable.OrderActions.Cancel))
                        {
                            MessageBox.Show("Order is canceled.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Order not found.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void odDgvCalcel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0)
                {
                    return;
                }
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (odDgvCalcel.Rows.Count > 0 && odDgvCalcel.Columns[e.ColumnIndex].Name == "CanView")
                {
                    string strOrderId = odDgvCalcel.Rows[e.RowIndex].Cells["CanOrderID"].Value.ToString();
                    string strOrderAction = odDgvCalcel.Rows[e.RowIndex].Cells["CanAction"].Value.ToString();
                    if (strOrderId != string.Empty && strOrderAction == "PAY")
                    {
                        this.ClearData();
                        this.GetOrderTransactionDataForEdit(strOrderId);
                        tcMain.SelectedIndex = 0;
                        pnlModifier.Visible = false;
                        pnlSearch.Visible = true;
                        btnCancel.Visible = true;
                        btnCheckOut.Enabled = true;
                        btnSendToKDS.Enabled = true;
                    }
                    else
                    {
                        frmViewOrderDetail frmVOD = new frmViewOrderDetail(strOrderId);
                        frmVOD.ShowDialog();
                        //MessageBox.Show("This is paid order.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Menu Tab Control Events

        private void tcRecentOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TabControlIndexChanged();
        }

        private void tcTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TabControlIndexChanged();
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TabControlIndexChanged();
        }

        #endregion

        #region Dropdown SelectedIndexChanged For Recent order

        private void odCmbAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlAllCustomSearch.Visible = false;
                if (IsCmbIndexChange)
                {
                    if (odCmbAll.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Today))
                    {
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Today), Convert.ToInt32(GlobalVariable.DeliveryType.All), txtAllSearch.Text);
                    }
                    else if (odCmbAll.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Yesterday))
                    {
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Yesterday), Convert.ToInt32(GlobalVariable.DeliveryType.All), txtAllSearch.Text);
                    }
                    else if (odCmbAll.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Custom))
                    {
                        pnlAllCustomSearch.Visible = true;
                        if (!GlobalVariable.IsDate(dtpAllTo.Text))
                        {
                            MessageBox.Show("Enter Valid To Date.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dtpAllTo.Focus();
                            return;
                        }
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Custom), Convert.ToInt32(GlobalVariable.DeliveryType.All), txtAllSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void odCmbDineIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlDineInCustomSearch.Visible = false;
                if (IsCmbIndexChange)
                {
                    if (odCmbDineIn.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Today))
                    {
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Today), Convert.ToInt32(GlobalVariable.DeliveryType.DineIn), txtDineInSearch.Text);
                    }
                    else if (odCmbDineIn.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Yesterday))
                    {
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Yesterday), Convert.ToInt32(GlobalVariable.DeliveryType.DineIn), txtDineInSearch.Text);
                    }
                    else if (odCmbDineIn.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Custom))
                    {
                        pnlDineInCustomSearch.Visible = true;
                        if (!GlobalVariable.IsDate(dtpDineInTo.Text))
                        {
                            MessageBox.Show("Enter Valid To Date.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dtpDineInTo.Focus();
                            return;
                        }
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Custom), Convert.ToInt32(GlobalVariable.DeliveryType.DineIn), txtDineInSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Index Change", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void odCmbTakeOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlTakeOutCustomSearch.Visible = false;
                if (IsCmbIndexChange)
                {
                    if (odCmbTakeOut.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Today))
                    {
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Today), Convert.ToInt32(GlobalVariable.DeliveryType.TakeOut), txtTakeOutSearch.Text);
                    }
                    else if (odCmbTakeOut.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Yesterday))
                    {
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Yesterday), Convert.ToInt32(GlobalVariable.DeliveryType.TakeOut), txtTakeOutSearch.Text);
                    }
                    else if (odCmbTakeOut.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Custom))
                    {
                        pnlTakeOutCustomSearch.Visible = true;
                        if (!GlobalVariable.IsDate(dtpTakeOutTo.Text))
                        {
                            MessageBox.Show("Enter Valid To Date.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dtpTakeOutTo.Focus();
                            return;
                        }
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Custom), Convert.ToInt32(GlobalVariable.DeliveryType.TakeOut), txtTakeOutSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void odCmbDelivery_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlDeliveryCustomSearch.Visible = false;
                if (IsCmbIndexChange)
                {
                    if (odCmbDelivery.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Today))
                    {
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Today), Convert.ToInt32(GlobalVariable.DeliveryType.Delivery), txtDeliverySearch.Text);
                    }
                    else if (odCmbDelivery.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Yesterday))
                    {
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Yesterday), Convert.ToInt32(GlobalVariable.DeliveryType.Delivery), txtDeliverySearch.Text);
                    }
                    else if (odCmbDelivery.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Custom))
                    {
                        pnlDeliveryCustomSearch.Visible = true;
                        if (!GlobalVariable.IsDate(dtpDeliveryTo.Text))
                        {
                            MessageBox.Show("Enter Valid To Date.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dtpDeliveryTo.Focus();
                            return;
                        }
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Custom), Convert.ToInt32(GlobalVariable.DeliveryType.Delivery), txtDeliverySearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void odCmbCalcel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlCancelCustomSearch.Visible = false;
                if (IsCmbIndexChange)
                {
                    if (odCmbCancel.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Today))
                    {
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Today), Convert.ToInt32(GlobalVariable.DeliveryType.Cancel), txtCancelSearch.Text);
                    }
                    else if (odCmbCancel.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Yesterday))
                    {
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Yesterday), Convert.ToInt32(GlobalVariable.DeliveryType.Cancel), txtCancelSearch.Text);
                    }
                    else if (odCmbCancel.SelectedIndex == Convert.ToInt32(GlobalVariable.DayFilter.Custom))
                    {
                        pnlCancelCustomSearch.Visible = true;
                        if (!GlobalVariable.IsDate(dtpCancelTo.Text))
                        {
                            MessageBox.Show("Enter Valid To Date.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dtpCancelTo.Focus();
                            return;
                        }
                        this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Custom), Convert.ToInt32(GlobalVariable.DeliveryType.Cancel), txtCancelSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Calender Event From Recent Order

        private void dtpAllTo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalVariable.IsDate(dtpAllTo.Text))
                {
                    MessageBox.Show("Enter Valid To Date.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpAllTo.Focus();
                    return;
                }
                this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Custom), Convert.ToInt32(GlobalVariable.DeliveryType.All), txtAllSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpDineInTo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalVariable.IsDate(dtpDineInTo.Text))
                {
                    MessageBox.Show("Enter Valid To Date.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpDineInTo.Focus();
                    return;
                }
                this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Custom), Convert.ToInt32(GlobalVariable.DeliveryType.DineIn), txtDineInSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpTakeOutTo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalVariable.IsDate(dtpTakeOutTo.Text))
                {
                    MessageBox.Show("Enter Valid To Date.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpTakeOutTo.Focus();
                    return;
                }
                this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Custom), Convert.ToInt32(GlobalVariable.DeliveryType.TakeOut), txtTakeOutSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpDeliveryTo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalVariable.IsDate(dtpDeliveryTo.Text))
                {
                    MessageBox.Show("Enter Valid To Date.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpDeliveryTo.Focus();
                    return;
                }
                this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Custom), Convert.ToInt32(GlobalVariable.DeliveryType.Delivery), txtDeliverySearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpCancelTo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalVariable.IsDate(dtpCancelTo.Text))
                {
                    MessageBox.Show("Enter Valid To Date.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpCancelTo.Focus();
                    return;
                }
                this.ComboAllIndexChange(Convert.ToInt32(GlobalVariable.DayFilter.Custom), Convert.ToInt32(GlobalVariable.DeliveryType.Cancel), txtCancelSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region More Menu Tab

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure? You want to logout.", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                frmEmployeeLogin frmEmp = new frmEmployeeLogin();
                frmEmp.FormClosed += new FormClosedEventHandler(OrderBook_FormClosed);
                frmEmp.Show();
                this.Hide();
            }
        }

        private void btnSalesReport_Click(object sender, EventArgs e)
        {
            frmSalesReport frmSR = new frmSalesReport();
            frmSR.ShowDialog();
        }

        private void btnDeviceType_Click(object sender, EventArgs e)
        {
            frmDeviceMaster frmDM = new frmDeviceMaster();
            frmDM.ShowDialog();
        }

        private void btnKitchenRouting_Click(object sender, EventArgs e)
        {
            frmItemChefRouting frmICR = new frmItemChefRouting();
            frmICR.ShowDialog();
        }

        private void btnPrinterRouting_Click(object sender, EventArgs e)
        {
            frmPrinterMapping frmPM = new frmPrinterMapping();
            frmPM.ShowDialog();
        }

        private void btnGenSetting_Click(object sender, EventArgs e)
        {
            frmGeneral frmGS = new frmGeneral();
            frmGS.ShowDialog();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            frmAbout frmAbt = new frmAbout();
            frmAbt.ShowDialog();
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            frmEmployee frmEmp = new frmEmployee();
            frmEmp.ShowDialog();
        }

        private void btnItemDiscount_Click(object sender, EventArgs e)
        {
            //frmDiscountSelect frmDS = new frmDiscountSelect(Convert.ToInt32(GlobalVariable.DiscountType.OnItem));
            //frmDS.ShowDialog();
        }

        private void btnStaffList_Click(object sender, EventArgs e)
        {
            frmStaffList frmSL = new frmStaffList();
            frmSL.ShowDialog();
        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            frmAddMenu frmAM = new frmAddMenu();
            frmAM.ShowDialog();
        }

        private void btnTill_Click(object sender, EventArgs e)
        {
            frmTill frmT = new frmTill();
            frmT.ShowDialog();
        }

        private void btnDownStream_Click(object sender, EventArgs e)
        {
            frmDownStream frmDS = new frmDownStream();
            frmDS.ShowDialog();
        }

        private void btnUpStream_Click(object sender, EventArgs e)
        {
            frmUpStream frmUPS = new frmUpStream();
            frmUPS.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            frmExport frmEE = new frmExport();
            frmEE.ShowDialog();
        }

        private void btnSocketTest_Click(object sender, EventArgs e)
        {
            //frmSocketServer frmDM = new frmSocketServer();
            frmSocketTest frmDM = new frmSocketTest();
            frmDM.ShowDialog();
        }

        private void BtnAddRemoveDevice_Click(object sender, EventArgs e)
        {
            frmDeviceMaster frmDM = new frmDeviceMaster();
            frmDM.ShowDialog();
        }

        private void BtnDeviceConnection_Click(object sender, EventArgs e)
        {
            pnlSocketCoonection.Visible = true;
            tableSettingMenu.Visible = false;
        }

        private void BtnBackToSetting_Click(object sender, EventArgs e)
        {
            pnlSocketCoonection.Visible = false;
            tableSettingMenu.Visible = true;
        }

        #endregion

        #region Print Duplicate Order Receipt

        public void DuplicatePrint(string OrderID)
        {
            try
            {                
                if (OrderID != string.Empty)
                {
                    ENT.OrderBook objENT = new ENT.OrderBook();
                    DAL.OrderBook objDAL = new DAL.OrderBook();
                    List<ENT.OrderBook> lstOrder = new List<ENT.OrderBook>();
                    objENT.OrderID = new Guid(OrderID.Trim());
                    objENT.Mode = "GetRecordByOrderID";
                    lstOrder = objDAL.getOrder(objENT);
                    if (lstOrder.Count > 0)
                    {
                        if (lstOrder[0].IsPrint != Convert.ToInt32(GlobalVariable.PrintStatus.NotPrinted))
                        {
                            PrintReceipt pr = new PrintReceipt(OrderID.Trim(), lstOrder[0].IsPrint);
                            pr.PrintOrderReceipt();
                        }
                        else if (lstOrder[0].IsPrint == Convert.ToInt32(GlobalVariable.PrintStatus.NotPrinted))
                        {
                            PrintReceipt pr = new PrintReceipt(OrderID.Trim(), Convert.ToInt32(GlobalVariable.PrintStatus.Printed));
                            pr.PrintOrderReceipt();
                        }
                        else
                        {
                            MessageBox.Show("Order payment must be done by customer.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select Valid Order From List.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAllPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string OrderID = "";
                if (odDgvAll.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure! You want to print duplicate receipt.", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        OrderID = Convert.ToString(odDgvAll.Rows[odDgvAll.CurrentRow.Index].Cells["AllOrderID"].Value);
                        DuplicatePrint(OrderID);
                    }
                }
                else
                {
                    MessageBox.Show("Order Not Available For Print.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDineInPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string OrderID = "";
                if (odDgvDineIn.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure! You want to print duplicate receipt.", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        OrderID = Convert.ToString(odDgvDineIn.Rows[odDgvDineIn.CurrentRow.Index].Cells["DIOrderID"].Value);
                        DuplicatePrint(OrderID);
                    }
                }
                else
                {
                    MessageBox.Show("Order Not Available For Print.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTakeOutPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string OrderID = "";
                if (odDgvTakeOut.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure! You want to print duplicate receipt.", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        OrderID = Convert.ToString(odDgvTakeOut.Rows[odDgvTakeOut.CurrentRow.Index].Cells["TOOrderID"].Value);
                        DuplicatePrint(OrderID);
                    }
                }
                else
                {
                    MessageBox.Show("Order Not Available For Print.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeliveryPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string OrderID = "";
                if (odDgvDelivery.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure! You want to print duplicate receipt.", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        OrderID = Convert.ToString(odDgvDelivery.Rows[odDgvDelivery.CurrentRow.Index].Cells["DelOrderID"].Value);
                        DuplicatePrint(OrderID);
                    }
                }
                else
                {
                    MessageBox.Show("Order Not Available For Print.", "Order Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

       
    }

    //helper class to modify form object property from another thread than the one from wich form was created
    class TestFormCotrolHelper
    {
        delegate void UniversalVoidDelegate();

        /// <summary>
        /// Call form controll action from different thread
        /// </summary>
        public static void ControlInvike(Control control, Action function)
        {
            if (control.IsDisposed || control.Disposing)
                return;

            if (control.InvokeRequired)
            {
                control.Invoke(new UniversalVoidDelegate(() => ControlInvike(control, function)));
                return;
            }
            function();
        }
    }
}