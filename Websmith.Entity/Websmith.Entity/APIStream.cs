using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class APIStream
    {
        private APIStream() { }

        // Down Stream Object
        public static readonly string METHOD_RESTAURANT_PROFILE = "RestruantProfile_DS";
        public static readonly string METHOD_STAFF = "Staff_DS";
        public static readonly string METHOD_MENU = "Menu_DS";
        public static readonly string METHOD_TABLE = "Tables_DS";
        public static readonly string METHOD_TAX = "Tax_DS";
        public static readonly string METHOD_DISCOUNT = "Discount_DS";
        public static readonly string METHOD_MODIFIER = "Modifier_DS";
        public static readonly string METHOD_INGREDIENTS = "Ingredients_DS";
        public static readonly string METHOD_ORDER = "Order_DS";
        public static readonly string METHOD_CUSTOMER = "Customer_DS";
        public static readonly string METHOD_VENDOR = "Vendor_DS";
        public static readonly string METHOD_RECIPE = "Recipe_DS";
        public static readonly string METHOD_SHIFT_EMPLOYEE = "ShiftWiseEmployee_DS";
        public static readonly string METHOD_SHIFT = "Shift_DS";
        public static readonly string METHOD_CLASS = "Class_DS";
        public static readonly string METHOD_MODULE = "Module_DS";

        // Up Stream Object
        public static readonly string US_MANAGE_CATEGORY = "ManageCategory";
        public static readonly string US_MANAGE_STAFF = "ManageStaff";
        public static readonly string US_MANAGE_VENDOR = "ManageVendor";
        public static readonly string US_MANAGE_CUSTOMER = "ManageCustomer";
        public static readonly string US_MANAGE_PRODUCT = "ManageProduct";
        public static readonly string US_MANAGE_TILL = "ManageTill";
        public static readonly string US_MANAGE_ORDER = "ManageOrder";
        public static readonly string US_MANAGE_STOCK = "ManageStock_PO";
        public static readonly string US_MANAGE_INVENTORY = "ManageInventory";
        public static readonly string US_MANAGE_ATTENDANCE = "ManageAttendance";

        public enum TableStatus
        {
            Vacant = 1,
            Occupied = 2
        }

        public enum R_USER_TYPE
        {
            BRANCH = 2,
            FRANCHISE = 3
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
                throw ex;
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
                throw ex;
            }
            return temp.ToString();
        }

        
    }
}
