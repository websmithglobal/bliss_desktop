using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.Bliss
{
    public class GlobalVariable
    {
        public static string Dashboard = "DASHBOARD";
        public static string BranchID = "";
        public static string BranchName = "";
        public static int RUserType = 0;
        public static string BranchAddress = "";
        public static string EmployeeID = "";
        public static string EmployeeCode = "";
        public static string EmployeeName = "";
        public static string ClassID = "";
        public static bool IsOk = false;
        public static bool IsOrderPaid = false;
        public static decimal decModifierAmount = 0;
        public static string BranchUserName = "";
        public static string BranchUserPassword = "";

        public static ENT.LoginResponse objLoginResponse = new ENT.LoginResponse();
        public static ENT.EmployeeMasterList objEmployeeMasterList = new ENT.EmployeeMasterList();
        public static ENT.CheckInfo objCheckInfo = new ENT.CheckInfo();

        public static List<ENT.GeneralSetting> GetGeneralSetting()
        {
            List<ENT.GeneralSetting> lstENTGS = new List<ENT.GeneralSetting>();
            try
            {
                ENT.GeneralSetting objENTGS = new ENT.GeneralSetting();
                DAL.GeneralSetting objDALGS = new DAL.GeneralSetting();
                objENTGS.Mode = "GetAll";
                lstENTGS = objDALGS.GetGeneralSetting(objENTGS);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lstENTGS;
        }

        public enum DeliveryType
        {
            All = 0,
            DineIn = 1,
            Delivery = 2,
            TakeOut = 3,
            Cancel = 4
        }

        public enum DiscountType
        {
            OnOrder = 1,
            OnItem = 2
        }

        public enum TableStatus
        {
            Vacant = 1,
            Occupied = 2
        }

        public enum DeviceTypeStatus
        {
            False = 0,
            True = 1
        }

        public enum DeviceType
        {
            POS = 1,
            KDS = 2,
            PRINTER = 3
        }

        public enum DeviceStatus
        {
            Disconneted = 0,
            Conneted = 1
        }

        public enum OrderActions
        {
            Pay = 1,
            Paid = 2,
            Cancel = 3
        }

        public enum OrderStatus
        {
            CANCEL = 0,
            OPEN = 1,
            HOLD = 2,
            CLOSE = 3
        }

        public enum PrintStatus
        {
            NotPrinted = 0,
            Printed = 1,
            DuplicatePrinted = 2
        }

        public enum DayFilter
        {
            Today = 0,
            Yesterday = 1,
            Custom = 2
        }

        public enum CheckOutCRMMethod
        {
            GiftCard = 1,
            RewardPoint = 2,
            Other = 3
        }

        public enum CheckOutPaymentMethod
        {
            Cash = 1,
            CreditCard = 2,
            Cheque = 3
        }

        public enum SendToKDSPrintStatus
        {
            NotPrinted = 0,
            Printed = 1
        }

        public enum KOTPrintHeaderStatus
        {
            NewOrder = 0,
            RunningOrder = 1,
            ResendItem = 2,
            CancelItem = 3,
            CancelOrder = 4
        }

        public static string ChangeDate(string date)
        {
            string temp = "";
            try
            {
                string tmpDate = date.Substring(0, 10);
                temp = tmpDate.Substring(0, 2);
                switch (Convert.ToInt32(tmpDate.Substring(3, 2)))
                {
                    case 1:
                        temp = temp + "-JAN-";
                        break;
                    case 2:
                        temp = temp + "-FEB-";
                        break;
                    case 3:
                        temp = temp + "-MAR-";
                        break;
                    case 4:
                        temp = temp + "-APR-";
                        break;
                    case 5:
                        temp = temp + "-MAY-";
                        break;
                    case 6:
                        temp = temp + "-JUN-";
                        break;
                    case 7:
                        temp = temp + "-JUL-";
                        break;
                    case 8:
                        temp = temp + "-AUG-";
                        break;
                    case 9:
                        temp = temp + "-SEP-";
                        break;
                    case 10:
                        temp = temp + "-OCT-";
                        break;
                    case 11:
                        temp = temp + "-NOV-";
                        break;
                    case 12:
                        temp = temp + "-DEC-";
                        break;
                }
                temp = temp + tmpDate.Substring(6, 4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Change Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return temp.ToString();
        }

        public static string ChangeDateTime(string date)
        {
            string temp = "";
            try
            {
                string tmpDate = date.Substring(0, 10);   // 04/10/2017 12:43 PM
                string tmpTime = date.Substring(11, 8);
                temp = tmpDate.Substring(0, 2);
                switch (Convert.ToInt32(tmpDate.Substring(3, 2)))
                {
                    case 1:
                        temp = temp + "-JAN-";
                        break;
                    case 2:
                        temp = temp + "-FEB-";
                        break;
                    case 3:
                        temp = temp + "-MAR-";
                        break;
                    case 4:
                        temp = temp + "-APR-";
                        break;
                    case 5:
                        temp = temp + "-MAY-";
                        break;
                    case 6:
                        temp = temp + "-JUN-";
                        break;
                    case 7:
                        temp = temp + "-JUL-";
                        break;
                    case 8:
                        temp = temp + "-AUG-";
                        break;
                    case 9:
                        temp = temp + "-SEP-";
                        break;
                    case 10:
                        temp = temp + "-OCT-";
                        break;
                    case 11:
                        temp = temp + "-NOV-";
                        break;
                    case 12:
                        temp = temp + "-DEC-";
                        break;
                }
                temp = temp + tmpDate.Substring(6, 4);
                temp = temp.Trim() + " " + tmpTime.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Change Datetime", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return temp.ToString();
        }

        public static bool IsDate(string date)
        {
            bool result = false;
            try
            {
                DateTime temp;
                if (DateTime.TryParse(date, out temp))
                    result = true;
                else
                    result = false;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public static bool IsNumeric(string value)
        {
            decimal val;
            if (decimal.TryParse(value, out val))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public static bool CheckValidate(Control[] CtrlFld)
        {
            for (int tf = 0; tf < CtrlFld.Length; tf++)
            {
                if (CtrlFld[tf].GetType().ToString() == "System.Windows.Forms.TextBox" && CtrlFld[tf].Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter " + CtrlFld[tf].Tag + " !!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
                    CtrlFld[tf].Text = "";
                    CtrlFld[tf].Focus();
                    return false;
                }
                else if (CtrlFld[tf].GetType().ToString() == "System.Windows.Forms.ComboBox" && (((ComboBox)CtrlFld[tf]).SelectedIndex < 0 || CtrlFld[tf].Text.Trim() == "-Select-" || CtrlFld[tf].Text.Trim() == ""))
                {
                    MessageBox.Show("Select Valid " + CtrlFld[tf].Tag + " !!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
                    CtrlFld[tf].Text = "";
                    CtrlFld[tf].Focus();
                    return false;
                }
                else if (CtrlFld[tf].GetType().ToString() == "System.Windows.Forms.DataGridView" && (((DataGridView)CtrlFld[tf]).RowCount <= 0))
                {
                    MessageBox.Show("Please Enter " + CtrlFld[tf].Tag + " !!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
                    CtrlFld[tf].Focus();
                    return false;
                }
                else if (CtrlFld[tf].GetType().ToString() == "System.Windows.Forms.DateTimePicker" && CtrlFld[tf].Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter " + CtrlFld[tf].Tag + " !!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
                    CtrlFld[tf].Text = "";
                    CtrlFld[tf].Focus();
                    return false;
                }
            }
            return true;
        }

        public static List<Control> FindControlRecursive(List<Control> list, Control parent, System.Type ctrlType)
        {
            if (parent == null)
                return list;
            if (object.ReferenceEquals(parent.GetType(), ctrlType))
            {
                list.Add(parent);
            }
            foreach (Control child in parent.Controls)
            {
                FindControlRecursive(list, child, ctrlType);
            }
            return list;
        }

        public static void Theme(Form frm)
        {
            try
            {
                // code for form
                frm.BackColor = Color.FromArgb(0, 192, 192);

                // code for form all button
                List<Control> allBtn = new List<Control>();
                foreach (Button btn in FindControlRecursive(allBtn, frm, typeof(Button)))
                {
                    btn.BackColor = Color.Teal;
                    btn.FlatAppearance.BorderColor = Color.OrangeRed;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Font = new Font(btn.Font, FontStyle.Bold);
                }

                List<Control> allDGV = new List<Control>();
                foreach (DataGridView dgv in FindControlRecursive(allDGV, frm, typeof(DataGridView)))
                {
                    dgv.BackgroundColor = Color.FromArgb(0, 192, 192);
                    dgv.BackColor = Color.FromArgb(0, 192, 192);
                }

                List<Control> allTab = new List<Control>();
                foreach (TabControl tab in FindControlRecursive(allTab, frm, typeof(TabControl)))
                {
                    List<Control> allTabPage = new List<Control>();
                    foreach (TabPage tabPage in FindControlRecursive(allTabPage, tab, typeof(TabPage)))
                    {
                        tabPage.BackColor = Color.PaleGreen;
                    }
                }

                if (frm.Name == "frmBranchLogin" || frm.Name == "frmEmployeeLogin")
                {
                    List<Control> BranchButton = new List<Control>();
                    foreach (Button btn in FindControlRecursive(BranchButton, frm, typeof(Button)))
                    {
                        btn.BackColor = Color.OrangeRed;
                        btn.ForeColor = Color.White;
                        btn.FlatAppearance.BorderColor = Color.Orange;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.Font = new Font(btn.Font, FontStyle.Bold);
                    }
                }

                //List<Control> allPnl = new List<Control>();
                //foreach (Panel pnl in FindControlRecursive(allPnl, frm, typeof(Panel)))
                //{
                //    // code for panel
                //    pnl.BackColor = Color.Teal;

                //    // code for all label
                //    List<Control> allPnlLbl = new List<Control>();
                //    foreach (Label pnllbl in FindControlRecursive(allPnlLbl, pnl, typeof(Label)))
                //    {
                //        pnllbl.ForeColor = Color.White;
                //    }

                //    // code for only panel button
                //    List<Control> allPnlBtn = new List<Control>();
                //    foreach (Button pnlbtn in FindControlRecursive(allPnlBtn, pnl, typeof(Button)))
                //    {
                //        pnlbtn.BackColor = Color.OrangeRed;    // HotTrack
                //        pnlbtn.ForeColor = Color.White;
                //        pnlbtn.FlatStyle = FlatStyle.Flat;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        
    }

    public sealed class APIStream
    {
        private APIStream() { }

        public static readonly string METHOD_RESTAURANT_PROFILE = "RestruantProfile_DS";
        public static readonly string METHOD_STAFF = "Staff_DS";
        public static readonly string METHOD_SHIFT = "Shift_DS";
        public static readonly string METHOD_MENU = "Menu_DS";
        public static readonly string METHOD_TABLE = "Tables_DS";
        public static readonly string METHOD_TAX = "Tax_DS";
        public static readonly string METHOD_DISCOUNT = "Discount_DS";
        public static readonly string METHOD_MODIFIER = "Modifier_DS";
        public static readonly string METHOD_MODULE = "Module_DS";
        public static readonly string METHOD_CLASS = "Class_DS";
        public static readonly string METHOD_INGREDIENTS = "Ingredients_DS";
        public static readonly string METHOD_ORDER = "Order_DS";
        public static readonly string METHOD_CUSTOMER = "Customer_DS";
        public static readonly string METHOD_VENDOR = "Vendor_DS";
        public static readonly string METHOD_RECIPE = "Recipe_DS";
        public static readonly string METHOD_SHIFT_EMPLOYEE = "ShiftWiseEmployee_DS";
    }
}
