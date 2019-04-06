using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public partial class frmGeneral : Form
    {
        Guid paymentGatewayID;
        string paymentGatewayName="";

        DAL.GeneralSetting objDAL = new DAL.GeneralSetting();
        ENT.GeneralSetting objENT = new ENT.GeneralSetting();

        public frmGeneral()
        {
            InitializeComponent();
        }

        private void GetGeneralSetting()
        {
            try
            {
                List<ENT.GeneralSetting> lstENT = new List<ENT.GeneralSetting>();
                objENT.Mode = "GetAll";
                lstENT = objDAL.GetGeneralSetting(objENT);
                if (lstENT.Count > 0)
                {
                    paymentGatewayID = lstENT[0].PaymentGatewayID;
                    paymentGatewayName = lstENT[0].PaymentGatewayName;
                    if (Convert.ToString(paymentGatewayID) != string.Empty || paymentGatewayID != null)
                    {
                        foreach (RadioButton rdo in grpPayConfig.Controls)
                        {
                            if (paymentGatewayID.ToString().Equals(rdo.Tag.ToString()))
                            {
                                rdo.Checked = true;
                                break;
                            }
                        }
                    }

                    txtHeader.Text = lstENT[0].PrintHeader;
                    txtFooter.Text = lstENT[0].PrintFooter;
                    chkDuplicatePrint.Checked = lstENT[0].DuplicatePrint;
                    cmbKOTCount.SelectedIndex = lstENT[0].KOTCount - 1;
                    txtPrefix.Text = lstENT[0].OrderPrefix;
                    cmbFontSize.SelectedIndex = lstENT[0].KOTFontSize - 1;
                    chkKOTServerName.Checked = lstENT[0].KOTServerName;
                    chkKOTDateTime.Checked = lstENT[0].KOTDateTime;
                    chkKOTOrderType.Checked = lstENT[0].KOTOrderType;
                    chkKDSWithoutDisplay.Checked = lstENT[0].KDSWithoutDisplay;
                    chkRoundingTotal.Checked = lstENT[0].RoundingTotal;
                    chkDisplayCardNo.Checked = lstENT[0].DisplayCardNo;
                    chkPrintOnPayment.Checked = lstENT[0].PrintOnPaymentDone;
                    chkRunningOrderOnKOT.Checked = lstENT[0].RunningOrderDisplayOnKOT;
                    chkKDSWithoutPrinter.Checked = lstENT[0].KDSWithoutPrinter;
                    chkCustNameOnKOT.Checked = lstENT[0].CustomerNameOnKOT;
                    cmbFormat.SelectedItem = lstENT[0].DateTimeFormat;
                    cmbLanguage.SelectedItem = lstENT[0].Language;
                    txtTillCur1.Text = Convert.ToString(lstENT[0].TillCur1);
                    txtTillCur2.Text =Convert.ToString(lstENT[0].TillCur2);
                    txtTillCur3.Text =Convert.ToString(lstENT[0].TillCur3);
                    txtTillCur4.Text =Convert.ToString(lstENT[0].TillCur4);
                    txtTillCur5.Text =Convert.ToString(lstENT[0].TillCur5);
                    txtTillCur6.Text =Convert.ToString(lstENT[0].TillCur6);
                    txtTillCur7.Text =Convert.ToString(lstENT[0].TillCur7);
                    txtTillCur8.Text =Convert.ToString(lstENT[0].TillCur8);
                    txtTillCur9.Text = Convert.ToString(lstENT[0].TillCur9);
                    chkDineIn.Checked = lstENT[0].DineIn;
                    chkTakeOut.Checked = lstENT[0].TakeOut;
                    chkDelivery.Checked = lstENT[0].Delivery;
                    chkOrderAhead.Checked = lstENT[0].OrderAhead;
                    chkQueue.Checked = lstENT[0].Queue;
                    chkPartyEvent.Checked = lstENT[0].PartyEvent;
                    txtTaxLabel1.Text = lstENT[0].TaxLabel1;
                    txtPer1.Text = Convert.ToString(lstENT[0].TaxPercentage1);
                    txtTaxLabel2.Text = lstENT[0].TaxLabel2;
                    txtPer2.Text = Convert.ToString(lstENT[0].TaxPercentage2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "General Setting", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetPaymentGateway()
        {
            try
            {
                ENT.PaymentGatewayMaster objENT = new ENT.PaymentGatewayMaster();
                DAL.PaymentGatewayMaster objDAL = new DAL.PaymentGatewayMaster();
                List<ENT.PaymentGatewayMaster> lstENT = new List<ENT.PaymentGatewayMaster>();

                objENT.Mode = "GetAllRecord";
                lstENT = objDAL.getPaymentGatewayMaster(objENT);

                int x = 20;
                int y = 25;

                for (int i = 0; i < lstENT.Count; i++)
                {
                    RadioButton RB = new System.Windows.Forms.RadioButton();
                    RB.AutoSize = true;
                    RB.Location = new System.Drawing.Point(x, y);
                    RB.Size = new System.Drawing.Size(150, 25);
                    RB.CheckedChanged += new EventHandler(RB_CheckedChanged);
                    RB.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
                    RB.Name = "RB" + i;
                    RB.TabIndex = i;
                    RB.TabStop = true;
                    RB.Text = lstENT[i].PaymentGatewayName;
                    RB.Tag = lstENT[i].PaymentGatewayID;
                    RB.UseVisualStyleBackColor = true;
                    grpPayConfig.Controls.Add(RB);
                    x += 120;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "General Setting", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton rb = (RadioButton)sender;
                if (rb != null)
                {
                    paymentGatewayID = new Guid(rb.Tag.ToString());
                    paymentGatewayName = rb.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "General Setting", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmGeneral_Load(object sender, EventArgs e)
        {
            try
            {
                cmbFontSize.SelectedIndex = 0;
                cmbFormat.SelectedIndex = 0;
                cmbKOTCount.SelectedIndex = 0;
                cmbLanguage.SelectedIndex = 0;
                GetPaymentGateway();
                GetGeneralSetting();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "General Setting", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<ENT.GeneralSetting> lstENT = new List<ENT.GeneralSetting>();
                objENT.Mode = "GetAll";
                lstENT = objDAL.GetGeneralSetting(objENT);
                if (lstENT.Count > 0)
                    objENT.Mode = "UPDATE";
                else
                    objENT.Mode = "ADD";

                objENT.PaymentGatewayID = paymentGatewayID;
                objENT.PaymentGatewayName = paymentGatewayName.Trim();
                objENT.PrintHeader = txtHeader.Text.Trim();
                objENT.PrintFooter = txtFooter.Text.Trim();
                objENT.DuplicatePrint = chkDuplicatePrint.Checked;
                objENT.KOTCount = cmbKOTCount.SelectedIndex + 1;
                objENT.OrderPrefix = txtPrefix.Text.Trim();
                objENT.KOTFontSize = cmbFontSize.SelectedIndex + 1;
                objENT.KOTServerName = chkKOTServerName.Checked;
                objENT.KOTDateTime = chkKOTDateTime.Checked;
                objENT.KOTOrderType = chkKOTOrderType.Checked;
                objENT.KDSWithoutDisplay = chkKDSWithoutDisplay.Checked;
                objENT.RoundingTotal = chkRoundingTotal.Checked;
                objENT.DisplayCardNo = chkDisplayCardNo.Checked;
                objENT.PrintOnPaymentDone = chkPrintOnPayment.Checked;
                objENT.RunningOrderDisplayOnKOT = chkRunningOrderOnKOT.Checked;
                objENT.KDSWithoutPrinter = chkKDSWithoutPrinter.Checked;
                objENT.CustomerNameOnKOT = chkCustNameOnKOT.Checked;
                objENT.DateTimeFormat = cmbFormat.SelectedItem.ToString();
                objENT.Language = cmbLanguage.SelectedItem.ToString();
                objENT.TillCur1 = txtTillCur1.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtTillCur1.Text.Trim());
                objENT.TillCur2 = txtTillCur2.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtTillCur2.Text.Trim());
                objENT.TillCur3 = txtTillCur3.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtTillCur3.Text.Trim());
                objENT.TillCur4 = txtTillCur4.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtTillCur4.Text.Trim());
                objENT.TillCur5 = txtTillCur5.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtTillCur5.Text.Trim());
                objENT.TillCur6 = txtTillCur6.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtTillCur6.Text.Trim());
                objENT.TillCur7 = txtTillCur7.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtTillCur7.Text.Trim());
                objENT.TillCur8 = txtTillCur8.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtTillCur8.Text.Trim());
                objENT.TillCur9 = txtTillCur9.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtTillCur9.Text.Trim());
                objENT.DineIn = chkDineIn.Checked;
                objENT.TakeOut = chkTakeOut.Checked;
                objENT.Delivery = chkDelivery.Checked;
                objENT.OrderAhead = chkOrderAhead.Checked;
                objENT.Queue = chkQueue.Checked;
                objENT.PartyEvent = chkPartyEvent.Checked;
                objENT.TaxLabel1 = txtTaxLabel1.Text.Trim();
                objENT.TaxPercentage1 = Convert.ToDecimal(txtPer1.Text.Trim());
                objENT.TaxLabel2 = txtTaxLabel2.Text.Trim();
                objENT.TaxPercentage2 = Convert.ToDecimal(txtPer2.Text.Trim());
                objENT.RUserID = new Guid(GlobalVariable.BranchID);
                objENT.RUserType = GlobalVariable.RUserType;
              
                if (objDAL.InsertUpdateDeleteGeneralSetting(objENT))
                {
                    MessageBox.Show("General Setting Save Successfully.");
                }
                else
                {
                    MessageBox.Show("Problem in save setting.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "General Setting", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmGeneral_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();

                if (e.KeyCode == Keys.Enter)
                    SendKeys.Send("{TAB}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtPer1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtPer1.Text.Contains(".")) && (e.KeyChar == '.'))
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

        private void txtPer2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!txtPer2.Text.Contains(".")) && (e.KeyChar == '.'))
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
