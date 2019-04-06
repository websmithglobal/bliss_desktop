using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;
using System.Net;
using NativeWifi;
using OnBarcode.Barcode;
using System.Globalization;
using System.Drawing;
using System.Drawing.Printing;

namespace Websmith.Bliss
{
    public partial class frmAbout : Form
    {
        ENT.BranchMasterSetting objENTBMS = new ENT.BranchMasterSetting();
        DAL.BranchMasterSetting objDALBMS = new DAL.BranchMasterSetting();
        List<ENT.BranchMasterSetting> lstENTBMS = new List<ENT.BranchMasterSetting>();
        Linear barcode = new Linear();
        //PosPrinter m_Printer = null;

        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            try
            {
                objENTBMS.BranchID = new Guid(GlobalVariable.BranchID);
                objENTBMS.Mode = "GetBranchMasterAndSettingByID";
                lstENTBMS = objDALBMS.getBranchMasterSetting(objENTBMS);
                if (lstENTBMS.Count > 0)
                {
                    lblBranchName.Text = lstENTBMS[0].BranchName;
                    lblAddress.Text = lstENTBMS[0].Address;
                    lblUser.Text = GlobalVariable.EmployeeName;
                    lblCurrentIP.Text = getSystemInfo();
                    lblAppID.Text = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
                    lblCurrentWiFi.Text = GetAvailableNetworkSSIDs();
                    lblCurrentVersion.Text = System.Windows.Forms.Application.ProductVersion;
                    lblAppVersion.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    string base64Decoded = DAL.SecurityManager.Base64Decode(DAL.SecurityManager.Base64Decode(lstENTBMS[0].ExpireDate));
                    lblExpireDate.Text = base64Decoded;   
                    lblPhoneNo.Text = lstENTBMS[0].MobileNo;
                    lblEmail.Text = lstENTBMS[0].BranchEmailID;

                    string str = lblBranchName.Text + ", " + lblAddress.Text + ", " + lblUser.Text + ", " + lblCurrentIP.Text + 
                        ", " + lblAppID.Text + ", " + lblCurrentWiFi.Text + ", " + lblCurrentVersion.Text + ", " + lblAppVersion.Text + 
                        ", " + lblExpireDate.Text + ", " + lblPhoneNo.Text + ", " + lblEmail.Text;
                    //str = GetQRCode(str);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "About", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string getSystemInfo()
        {
            string IP="";
            try
            {
                string strHostName = "";
                strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
                IPAddress[] addr = ipEntry.AddressList;
                //lblHostName.Text = "Host Name : " + strHostName.ToString();
                if (addr.Length > 2)
                {
                    IP= addr[2].ToString();
                }
                else
                {
                    IP= addr[1].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Internet connection problem.", "Branch Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return IP;
        }

        public string GetAvailableNetworkSSIDs()
        {
            string connectedWifi = "";
            try
            {
                WlanClient client = new WlanClient();
                Collection<string> connectedSsids = new Collection<string>();
                foreach (WlanClient.WlanInterface wlanInterface in client.Interfaces)
                {
                    Wlan.Dot11Ssid ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid;
                    connectedWifi = new String(Encoding.ASCII.GetChars(ssid.SSID, 0, (int)ssid.SSIDLength));
                }                
            }
            catch (Exception)
            {
                connectedWifi = "WiFi Not Avalable Or Not Connected";
            }
            return connectedWifi;
        }

        private string GetQRCode(string lstENT)
        {
            var code = new QRCode();
            code.Data = lstENT;
            code.drawBarcode(Application.StartupPath + "/Barcode/BlissInfo.jpg");
            if (File.Exists(Application.StartupPath + "/Barcode/BlissInfo.jpg"))
            {
                pictureBox1.ImageLocation = Application.StartupPath + "/Barcode/BlissInfo.jpg";
            }
            return code.Data;
        }

        private void frmAbout_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
                
    }
}
