using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class DownStream
    {

        public static string EmployeeID;
        ENT.LoginDetail objENTAPI = new ENT.LoginDetail();
        string BranchID = "";

        public DownStream(ENT.LoginDetail objLoginDetail)
        {
            objENTAPI = objLoginDetail;
            BranchID = objLoginDetail.BranchID.ToString();
        }

        private DataSet SyncByOption(string strAPIMethos)
        {
            string responseFromServer = "";
            DataSet ds = new DataSet("JsonData");
            try
            {
                //WebRequest tRequest = WebRequest.Create("http://api.possoftwareindia.com/api/" + strAPIMethos);
                WebRequest tRequest = WebRequest.Create("http://localhost:15367/api/" + strAPIMethos);
                tRequest.Method = "POST";
                tRequest.ContentType = "application/json";

                string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(objENTAPI);

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
                                XmlDocument xml = JsonConvert.DeserializeXmlNode(responseFromServer, "RootObject");
                                ds = new DataSet("JsonData");
                                XmlReader xr = new XmlNodeReader(xml);
                                ds.ReadXml(xr);
                                //response = Newtonsoft.Json.JsonConvert.DeserializeObject<ENT.LoginResponse>(responseFromFirebaseServer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ds = new DataSet();
                throw ex;
            }
            return ds;
        }

        private DataSet SyncAll()
        {
            string responseFromServer = "";
            DataSet ds = new DataSet("JsonData");
            try
            {
                WebRequest tRequest = WebRequest.Create("http://api.possoftwareindia.com/api/BranchAuthentication");
                tRequest.Method = "POST";
                tRequest.ContentType = "application/json";

                string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(objENTAPI);

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
                                XmlDocument xml = JsonConvert.DeserializeXmlNode(responseFromServer, "RootObject");
                                ds = new DataSet("JsonData");
                                XmlReader xr = new XmlNodeReader(xml);
                                ds.ReadXml(xr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ds = new DataSet();
                throw ex;
            }
            return ds;
        }

        public string SyncAllData()
        {
            string lines = "";
            try
            {
                DataSet ds = SyncAll();
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    try
                    {
                        #region Table Wise Entry
                        switch (dt.TableName.ToString())
                        {
                            case "VendorMasterData":
                                #region VendorMasterData
                                ENT.VendorMasterData objENTVendor = new ENT.VendorMasterData();
                                VendorMasterData objDALVendor = new VendorMasterData();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALVendor.getDuplicateVendorByID(dt.Rows[n]["VendorID"].ToString()) > 0)
                                    { objENTVendor.Mode = "UPDATE"; }
                                    else
                                    { objENTVendor.Mode = "ADD"; }
                                    objENTVendor.VendorID = new Guid(dt.Rows[n]["VendorID"].ToString());
                                    objENTVendor.VendorName = Convert.ToString(dt.Rows[n]["VendorName"]);
                                    objENTVendor.IsUPStream = 1;
                                    if (objDALVendor.InsertUpdateDeleteVendorMaster(objENTVendor))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "BranchMasterSetting":
                                #region BranchMasterSetting
                                ENT.BranchMasterSetting objENTBranch = new ENT.BranchMasterSetting();
                                BranchMasterSetting objDALBranch = new BranchMasterSetting();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALBranch.getDuplicateBranchMasterSettingByID(dt.Rows[n]["BranchID"].ToString()) > 0)
                                    { objENTBranch.Mode = "UPDATE"; }
                                    else
                                    { objENTBranch.Mode = "ADD"; }
                                    objENTBranch.BranchID = new Guid(dt.Rows[n]["BranchID"].ToString());
                                    objENTBranch.RestaurantID = new Guid(dt.Rows[n]["RestaurantID"].ToString());
                                    objENTBranch.ContactNoForService = Convert.ToString(dt.Rows[n]["ContactNoForService"]);
                                    objENTBranch.DeliveryCharges = Convert.ToInt32(dt.Rows[n]["DeliveryCharges"]);
                                    objENTBranch.DeliveryTime = dt.Rows[n]["DeliveryTime"].ToString() == "" ? null : Convert.ToString(dt.Rows[n]["DeliveryTime"]);
                                    objENTBranch.PickupTime = dt.Rows[n]["PickupTime"].ToString() == "" ? null : Convert.ToString(dt.Rows[n]["PickupTime"]);
                                    objENTBranch.CurrencyName = Convert.ToString(dt.Rows[n]["CurrencyName"].ToString());
                                    objENTBranch.CurrencySymbol = Convert.ToString(dt.Rows[n]["CurrencySymbol"]);
                                    objENTBranch.WorkingDays = Convert.ToString(dt.Rows[n]["WorkingDays"]);
                                    objENTBranch.TagLine = Convert.ToString(dt.Rows[n]["TagLine"]);
                                    objENTBranch.Footer = Convert.ToString(dt.Rows[n]["Footer"]);
                                    objENTBranch.DeliveryAreaRedius = Convert.ToInt32(dt.Rows[n]["DeliveryAreaRedius"]);
                                    objENTBranch.DeliveryAreaTitle = Convert.ToString(dt.Rows[n]["DeliveryAreaTitle"]);
                                    objENTBranch.DistanceType = Convert.ToInt32(dt.Rows[n]["DistanceType"]);
                                    objENTBranch.DistanceName = Convert.ToString(dt.Rows[n]["DistanceName"]);
                                    objENTBranch.FreeDeliveryUpto = Convert.ToInt32(dt.Rows[n]["FreeDeliveryUpto"]);
                                    objENTBranch.BranchName = Convert.ToString(dt.Rows[n]["BranchName"]);
                                    objENTBranch.BranchEmailID = Convert.ToString(dt.Rows[n]["BranchEmailID"]);
                                    objENTBranch.MobileNo = Convert.ToString(dt.Rows[n]["MobileNo"]);
                                    // 12/14/2017 6:30:44 PM
                                    // 8/24/2017 6:30:44 PM
                                    DateTime dt1;
                                    DateTime date = new DateTime();
                                    //if (DateTime.TryParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "M/dd/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                    //{
                                    //    date = DateTime.ParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "M/dd/yyyy hh:mm:ss tt", null);
                                    //}
                                    //else if (DateTime.TryParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "M/dd/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                    //{
                                    //    date = DateTime.ParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "M/dd/yyyy h:mm:ss tt", null);
                                    //}
                                    //else if (DateTime.TryParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "MM/dd/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                    //{
                                    //    date = DateTime.ParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "MM/dd/yyyy hh:mm:ss tt", null);
                                    //}
                                    //else if (DateTime.TryParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "MM/dd/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                    //{
                                    //    date = DateTime.ParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "MM/dd/yyyy h:mm:ss tt", null);
                                    //}
                                    //else if (DateTime.TryParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                    //{
                                    //    date = DateTime.ParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "dd/MM/yyyy hh:mm:ss tt", null);
                                    //}

                                    objENTBranch.LastSyncDate = dt.Rows[n]["LastSyncDate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["LastSyncDate"].ToString());
                                    objENTBranch.VatNo = Convert.ToString(dt.Rows[n]["VatNo"]);
                                    objENTBranch.CSTNo = Convert.ToString(dt.Rows[n]["CSTNo"]);
                                    objENTBranch.ServiceTaxNo = Convert.ToString(dt.Rows[n]["ServiceTaxNo"]);
                                    objENTBranch.TinGSTNo = Convert.ToString(dt.Rows[n]["TinGSTNo"]);
                                    objENTBranch.Address = Convert.ToString(dt.Rows[n]["Address"]);
                                    objENTBranch.SubAreaStreet = Convert.ToString(dt.Rows[n]["SubAreaStreet"]);
                                    objENTBranch.PinCode = Convert.ToString(dt.Rows[n]["PinCode"]);
                                    objENTBranch.VersionCode = Convert.ToString(dt.Rows[n]["VersionCode"]);
                                    objENTBranch.BranchMasterSetting_Id = Convert.ToInt32(dt.Rows[n]["BranchMasterSetting_Id"]);
                                    objENTBranch.IsUPStream = 1;

                                    if (objDALBranch.InsertUpdateDeleteBranchMasterSetting(objENTBranch))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "BranchSettingDetail":
                                #region BranchSettingDetail
                                ENT.BranchSettingDetail objENTBranchSetting = new ENT.BranchSettingDetail();
                                BranchSettingDetail objDALBranchSetting = new BranchSettingDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALBranchSetting.getCoutBranchSettingByBranchID(BranchID) > 0)
                                    { objENTBranchSetting.Mode = "UPDATE"; }
                                    else
                                    { objENTBranchSetting.Mode = "ADD"; }
                                    objENTBranchSetting.IsFranchise = Convert.ToBoolean(dt.Rows[n]["IsFranchise"]);
                                    objENTBranchSetting.IsReservationOn = Convert.ToBoolean(dt.Rows[n]["IsReservationOn"]);
                                    objENTBranchSetting.IsOrderBookingOn = Convert.ToBoolean(dt.Rows[n]["IsOrderBookingOn"]);
                                    objENTBranchSetting.IsAutoAcceptOrderOn = Convert.ToBoolean(dt.Rows[n]["IsAutoAcceptOrderOn"]);
                                    objENTBranchSetting.IsAutoRoundOffTotalOn = Convert.ToBoolean(dt.Rows[n]["IsAutoRoundOffTotalOn"]);
                                    objENTBranchSetting.TaxGroupId = Convert.ToInt32(dt.Rows[n]["TaxGroupId"]);
                                    objENTBranchSetting.IsDemoVersion = Convert.ToBoolean(dt.Rows[n]["IsDemoVersion"]);
                                    objENTBranchSetting.ExpireDate = Convert.ToString(dt.Rows[n]["ExpireDate"]);
                                    objENTBranchSetting.BranchID = new Guid(BranchID);
                                    objENTBranchSetting.IsUPStream = 1;

                                    if (objDALBranchSetting.InsertUpdateDeleteVersionDetail(objENTBranchSetting))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "EmployeeMasterList":
                                #region EmployeeMasterList
                                ENT.EmployeeMasterList objENTEmployee = new ENT.EmployeeMasterList();
                                EmployeeMasterList objDALEmployee = new EmployeeMasterList();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALEmployee.getDuplicateEmployeeByID(dt.Rows[n]["EmployeeID"].ToString()) > 0)
                                    { objENTEmployee.Mode = "UPDATE"; }
                                    else
                                    { objENTEmployee.Mode = "ADD"; }
                                    objENTEmployee.EmployeeID = new Guid(dt.Rows[n]["EmployeeID"].ToString());
                                    objENTEmployee.EmpCode = Convert.ToString(dt.Rows[n]["EmpCode"]);
                                    objENTEmployee.EmpName = Convert.ToString(dt.Rows[n]["EmpName"]);
                                    objENTEmployee.Password = Convert.ToString(dt.Rows[n]["Password"]);
                                    objENTEmployee.Mobile = Convert.ToString(dt.Rows[n]["Mobile"]);
                                    objENTEmployee.Email = Convert.ToString(dt.Rows[n]["Email"]);
                                    objENTEmployee.RepotingTo = new Guid(dt.Rows[n]["RepotingTo"].ToString());
                                    objENTEmployee.RoleID = new Guid(dt.Rows[n]["RoleID"].ToString());
                                    objENTEmployee.RoleName = Convert.ToString(dt.Rows[n]["RoleName"]);
                                    objENTEmployee.SalaryAmt = Convert.ToDecimal(dt.Rows[n]["SalaryAmt"]);
                                    objENTEmployee.SalaryType = Convert.ToInt32(dt.Rows[n]["SalaryType"]);
                                    DateTime dt1;  // 15/12/2017
                                    DateTime date = new DateTime();
                                    //if (DateTime.TryParseExact(dt.Rows[n]["JoinDate"].ToString(), "M/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                    //{
                                    //    date = DateTime.ParseExact(dt.Rows[n]["JoinDate"].ToString(), "M/dd/yyyy", null);
                                    //}
                                    //else if (DateTime.TryParseExact(dt.Rows[n]["JoinDate"].ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                    //{
                                    //    date = DateTime.ParseExact(dt.Rows[n]["JoinDate"].ToString(), "MM/dd/yyyy", null);
                                    //}
                                    //else if (DateTime.TryParseExact(dt.Rows[n]["JoinDate"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                    //{
                                    //    date = DateTime.ParseExact(dt.Rows[n]["JoinDate"].ToString(), "dd/MM/yyyy", null);
                                    //}
                                    objENTEmployee.JoinDate = dt.Rows[n]["JoinDate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["JoinDate"].ToString());
                                    objENTEmployee.IsDisplayInKDS = Convert.ToInt32(dt.Rows[n]["IsDisplayInKDS"]);
                                    objENTEmployee.ClassID = new Guid(dt.Rows[n]["ClassID"].ToString());
                                    objENTEmployee.Gender = Convert.ToInt32(dt.Rows[n]["Gender"]);
                                    objENTEmployee.TotalHourInADay = Convert.ToInt32(dt.Rows[n]["TotalHourInADay"]);
                                    objENTEmployee.IsUPStream = 1;

                                    if (objDALEmployee.InsertUpdateDeleteEmployee(objENTEmployee))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "EmployeeShift":
                                #region EmployeeShift
                                ENT.EmployeeShift objENTEmpShift = new ENT.EmployeeShift();
                                EmployeeShift objDALEmpShift = new EmployeeShift();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALEmpShift.getDuplicateEmployeeShiftByEmpID(EmployeeID) > 0)
                                    { objENTEmpShift.Mode = "UPDATE"; }
                                    else
                                    { objENTEmpShift.Mode = "ADD"; }
                                    objENTEmpShift.ShiftID = new Guid(dt.Rows[n]["ShiftID"].ToString());
                                    objENTEmpShift.EmployeeID = new Guid(EmployeeID);

                                    if (objDALEmpShift.InsertUpdateDeleteEmployeeShift(objENTEmpShift))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "VersionDetail":
                                #region VersionDetail
                                ENT.VersionDetail objENTVersion = new ENT.VersionDetail();
                                VersionDetail objDALVersion = new VersionDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALVersion.getDuplicateVersionByCode(dt.Rows[n]["Version_Code"].ToString()) > 0)
                                    { objENTVersion.Mode = "UPDATE"; }
                                    else
                                    { objENTVersion.Mode = "ADD"; }
                                    objENTVersion.Version_Code = Convert.ToString(dt.Rows[n]["Version_Code"]);
                                    objENTVersion.IsMandtory = Convert.ToBoolean(dt.Rows[n]["IsMandtory"]);
                                    if (objDALVersion.InsertUpdateDeleteVersionDetail(objENTVersion))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "CategoryDetails":
                                #region CategoryMaster
                                ENT.CategoryMaster objENTCat = new ENT.CategoryMaster();
                                CategoryMaster objDALCat = new CategoryMaster();
                                objDALCat.DeleteCategoryForDownStream(1);
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    //if (objDALCat.getDuplicateCategoryByID(dt.Rows[n]["CategoryID"].ToString()) > 0)
                                    //{ objENTCat.Mode = "UPDATE"; }
                                    //else
                                    //{ objENTCat.Mode = "ADD"; }
                                    objENTCat.Mode = "ADD";
                                    objENTCat.CategoryID = new Guid(dt.Rows[n]["CategoryID"].ToString());
                                    objENTCat.CategoryName = Convert.ToString(dt.Rows[n]["CategoryName"]);
                                    objENTCat.ImgPath = Convert.ToString(dt.Rows[n]["ImgPath"]);
                                    objENTCat.ParentID = new Guid(dt.Rows[n]["MainCategoryID"].ToString());
                                    objENTCat.ClassMasterID = new Guid(dt.Rows[n]["ClassMasterID"].ToString());
                                    objENTCat.Priority = Convert.ToInt32(dt.Rows[n]["Priority"].ToString());
                                    objENTCat.IsCategory = 1;
                                    objENTCat.IsUPStream = 1;

                                    if (objDALCat.InsertUpdateDeleteCategoryMaster(objENTCat))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "SubCategoryDetail":
                                #region SubCategoryDetail
                                ENT.CategoryMaster objENTSubCat = new ENT.CategoryMaster();
                                CategoryMaster objDALSubCat = new CategoryMaster();
                                objDALSubCat.DeleteCategoryForDownStream(2);
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    //if (objDALSubCat.getDuplicateSubCategoryByID(dt.Rows[n]["CategoryID"].ToString()) > 0)
                                    //{ objENTSubCat.Mode = "UPDATE"; }
                                    //else
                                    //{ objENTSubCat.Mode = "ADD"; }
                                    objENTSubCat.Mode = "ADD";
                                    objENTSubCat.CategoryID = new Guid(dt.Rows[n]["CategoryID"].ToString());
                                    objENTSubCat.CategoryName = Convert.ToString(dt.Rows[n]["CategoryName"]);
                                    objENTSubCat.ImgPath = Convert.ToString(dt.Rows[n]["ImgPath"]);
                                    objENTSubCat.ParentID = new Guid(dt.Rows[n]["MainCategoryID"].ToString());
                                    objENTSubCat.ClassMasterID = new Guid(dt.Rows[n]["ClassMasterID"].ToString());
                                    objENTSubCat.Priority = Convert.ToInt32(dt.Rows[n]["Priority"].ToString());
                                    objENTSubCat.IsCategory = 2;
                                    objENTSubCat.IsUPStream = 1;
                                    if (objDALSubCat.InsertUpdateDeleteCategoryMaster(objENTSubCat))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "CategoryWiseProduct":
                                #region CategoryWiseProduct
                                ENT.CategoryWiseProduct objENTProduct = new ENT.CategoryWiseProduct();
                                CategoryWiseProduct objDALProduct = new CategoryWiseProduct();
                                objDALProduct.DeleteProductForDownStream();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    //if (objDALProduct.getDuplicateProductByID(dt.Rows[n]["ProductID"].ToString(), dt.Rows[n]["CategoryID"].ToString()) > 0)
                                    //{ objENTProduct.Mode = "UPDATE"; }
                                    //else
                                    //{ objENTProduct.Mode = "ADD"; }
                                    objENTProduct.Mode = "ADD";
                                    objENTProduct.CategoryID = new Guid(dt.Rows[n]["CategoryID"].ToString());
                                    objENTProduct.DiscountID = new Guid(dt.Rows[n]["DiscountID"].ToString());
                                    objENTProduct.ProductID = new Guid(dt.Rows[n]["ProductID"].ToString());
                                    objENTProduct.ProductName = Convert.ToString(dt.Rows[n]["ProductName"]);
                                    objENTProduct.Price = Convert.ToDecimal(dt.Rows[n]["Price"].ToString());
                                    objENTProduct.ImgPath = Convert.ToString(dt.Rows[n]["ImgPath"]);
                                    objENTProduct.IsUrl = Convert.ToBoolean(dt.Rows[n]["IsUrl"]);
                                    objENTProduct.Calorie = Convert.ToString(dt.Rows[n]["Calorie"]);
                                    objENTProduct.ShortDescription = Convert.ToString(dt.Rows[n]["ShortDescription"]);
                                    objENTProduct.IsNonVeg = Convert.ToBoolean(dt.Rows[n]["IsNonVeg"]);
                                    objENTProduct.IsTrendingItem = Convert.ToBoolean(dt.Rows[n]["IsTrendingItem"]);
                                    objENTProduct.ApproxCookingTime = dt.Rows[n]["ApproxCookingTime"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["ApproxCookingTime"].ToString());
                                    objENTProduct.IsAellergic = Convert.ToBoolean(dt.Rows[n]["IsAellergic"]);
                                    objENTProduct.Extras = dt.Rows[n]["Extras"].ToString() == "" ? null : Convert.ToString(dt.Rows[n]["Extras"]);
                                    objENTProduct.IsVisibleToB2C = Convert.ToBoolean(dt.Rows[n]["IsVisibleToB2C"]);
                                    objENTProduct.ExpiryDateFrom = dt.Rows[n]["ExpiryDateFrom"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["ExpiryDateFrom"].ToString());
                                    objENTProduct.ExpiryDateTo = dt.Rows[n]["ExpiryDateTo"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["ExpiryDateTo"].ToString());
                                    objENTProduct.StationID = new Guid(dt.Rows[n]["StationID"].ToString());
                                    objENTProduct.SuggestiveItems = Convert.ToString(dt.Rows[n]["SuggestiveItems"]);
                                    objENTProduct.IsCold = Convert.ToBoolean(dt.Rows[n]["IsCold"]);
                                    objENTProduct.IsDrink = Convert.ToBoolean(dt.Rows[n]["IsDrink"]);
                                    objENTProduct.DiningOptions = Convert.ToInt32(dt.Rows[n]["DiningOptions"]);
                                    objENTProduct.AllowPriceOverride = Convert.ToInt32(dt.Rows[n]["AllowPriceOverride"]);
                                    objENTProduct.IsAgeValidation = Convert.ToBoolean(dt.Rows[n]["IsAgeValidation"]);
                                    objENTProduct.AgeForValidation = Convert.ToInt32(dt.Rows[n]["AgeForValidation"]);
                                    objENTProduct.OverridePrice = Convert.ToDecimal(dt.Rows[n]["OverridePrice"]);
                                    objENTProduct.IsCombo = Convert.ToBoolean(dt.Rows[n]["IsCombo"]);
                                    objENTProduct.IsDisplayModifire = Convert.ToBoolean(dt.Rows[n]["IsDisplayModifire"]);
                                    objENTProduct.ProductCode = Convert.ToString(dt.Rows[n]["ProductCode"]);
                                    objENTProduct.TaxPercentage = Convert.ToDecimal(dt.Rows[n]["TaxPercentage"]);
                                    objENTProduct.Sort = Convert.ToInt32(dt.Rows[n]["Sort"]);
                                    objENTProduct.Priority = Convert.ToInt32(dt.Rows[n]["Priority"]);
                                    objENTProduct.IsUPStream = 1;
                                    if (objDALProduct.InsertUpdateDeleteProduct(objENTProduct))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "ComboDetail":
                                #region ComboDetail
                                ENT.ComboDetail objENTCombo = new ENT.ComboDetail();
                                ComboDetail objDALCombo = new ComboDetail();
                                objDALCombo.DeleteComboDetailForDownStream();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    //if (objDALCombo.getDuplicateComboDetailBySetID(dt.Rows[n]["ComboSetID"].ToString()) > 0)
                                    //{ objENTCombo.Mode = "UPDATE"; }
                                    //else
                                    //{ objENTCombo.Mode = "ADD"; }
                                    objENTCombo.Mode = "ADD";
                                    objENTCombo.ComboSetID = new Guid(dt.Rows[n]["ComboSetID"].ToString());
                                    objENTCombo.ComboSetName = Convert.ToString(dt.Rows[n]["ComboSetName"]);
                                    objENTCombo.CategoryID = new Guid(dt.Rows[n]["CategoryID"].ToString());
                                    objENTCombo.CProductID = new Guid(dt.Rows[n]["CProductID"].ToString());
                                    objENTCombo.ComboDetail_Id = Convert.ToInt32(dt.Rows[n]["ComboDetail_Id"].ToString());
                                    objENTCombo.CategoryWiseProduct_Id = Convert.ToInt32(dt.Rows[n]["CategoryWiseProduct_Id"]);
                                    objENTCombo.IsUPStream = 1;
                                    if (objDALCombo.InsertUpdateDeleteComboDetail(objENTCombo))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "ComboProductDetail":
                                #region ComboProductDetail
                                ENT.ComboProductDetail objENTCPD = new ENT.ComboProductDetail();
                                ComboProductDetail objDALCPD = new ComboProductDetail();
                                objDALCPD.DeleteComboProductDetailForDownStream();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    //if (objDALCPD.getDuplicateComboDetailBySetID(dt.Rows[n]["ProductID"].ToString()) > 0)
                                    //{ objENTCPD.Mode = "UPDATE"; }
                                    //else
                                    //{ objENTCPD.Mode = "ADD"; }
                                    objENTCPD.Mode = "ADD";
                                    objENTCPD.ProductID = new Guid(dt.Rows[n]["ProductID"].ToString());
                                    objENTCPD.IsDefault = Convert.ToBoolean(dt.Rows[n]["IsDefault"]);
                                    objENTCPD.ProductName = Convert.ToString(dt.Rows[n]["ProductName"]);
                                    objENTCPD.ComboDetail_Id = Convert.ToInt32(dt.Rows[n]["ComboDetail_Id"].ToString());
                                    objENTCPD.IsUPStream = 1;

                                    if (objDALCPD.InsertUpdateDeleteComboProductDetail(objENTCPD))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "DiscountMasterDetail":
                                #region DiscountMasterDetail
                                ENT.DiscountMasterDetail objENTDiscount = new ENT.DiscountMasterDetail();
                                DiscountMasterDetail objDALDiscount = new DiscountMasterDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALDiscount.getCoutDiscountMasterByDiscountID(dt.Rows[n]["DiscountID"].ToString()) > 0)
                                    { objENTDiscount.Mode = "UPDATE"; }
                                    else
                                    { objENTDiscount.Mode = "ADD"; }
                                    objENTDiscount.DiscountID = new Guid(dt.Rows[n]["DiscountID"].ToString());
                                    objENTDiscount.DiscountName = Convert.ToString(dt.Rows[n]["DiscountName"]);
                                    objENTDiscount.DiscountType = Convert.ToInt32(dt.Rows[n]["DiscountType"]);
                                    objENTDiscount.AmountOrPercentage = Convert.ToDecimal(dt.Rows[n]["AmountOrPercentage"]);
                                    objENTDiscount.QualificationType = Convert.ToInt32(dt.Rows[n]["QualificationType"]);
                                    objENTDiscount.IsTaxed = Convert.ToBoolean(dt.Rows[n]["IsTaxed"]);
                                    objENTDiscount.Barcode = Convert.ToString(dt.Rows[n]["Barcode"]);
                                    objENTDiscount.DiscountCode = Convert.ToString(dt.Rows[n]["DiscountCode"]);
                                    objENTDiscount.PasswordRequired = Convert.ToBoolean(dt.Rows[n]["PasswordRequired"]);
                                    objENTDiscount.DisplayOnPOS = Convert.ToBoolean(dt.Rows[n]["DisplayOnPOS"]);
                                    objENTDiscount.AutoApply = Convert.ToBoolean(dt.Rows[n]["AutoApply"]);
                                    objENTDiscount.DisplayToCustomer = Convert.ToBoolean(dt.Rows[n]["DisplayToCustomer"]);
                                    objENTDiscount.IsTimeBase = Convert.ToBoolean(dt.Rows[n]["IsTimeBase"]);
                                    objENTDiscount.IsLoyaltyRewards = Convert.ToBoolean(dt.Rows[n]["IsLoyaltyRewards"]);
                                    objENTDiscount.DiscountMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["DiscountMasterDetail_Id"]);
                                    objENTDiscount.IsUPStream = 1;
                                    if (objDALDiscount.InsertUpdateDeleteDiscountMasterDetail(objENTDiscount))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "TimeSheetWiseDiscount":
                                #region TimeSheetWiseDiscount
                                ENT.TimeSheetWiseDiscount objENTTSWD = new ENT.TimeSheetWiseDiscount();
                                TimeSheetWiseDiscount objDALTSWD = new TimeSheetWiseDiscount();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (n == 0)
                                    {
                                        objDALTSWD.deleteTimeSheetWiseDiscount();
                                    }
                                    objENTTSWD.Mode = "ADD";
                                    objENTTSWD.FromTime = dt.Rows[n]["FromTime"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["FromTime"]);
                                    objENTTSWD.ToTime = dt.Rows[n]["ToTime"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["ToTime"]);
                                    objENTTSWD.StartDate = dt.Rows[n]["StartDate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["StartDate"].ToString());
                                    objENTTSWD.EndDate = dt.Rows[n]["EndDate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["EndDate"].ToString());
                                    objENTTSWD.Day = Convert.ToInt32(dt.Rows[n]["Day"]);
                                    objENTTSWD.DiscountMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["DiscountMasterDetail_Id"]);
                                    objENTTSWD.IsUPStream = 1;

                                    if (objDALTSWD.InsertUpdateDeleteTimeSheetWiseDiscount(objENTTSWD))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "ModifierCategoryDetail":
                                #region ModifierCategoryDetail
                                ENT.ModifierCategoryDetail objENTModifier = new ENT.ModifierCategoryDetail();
                                ModifierCategoryDetail objDALModifier = new ModifierCategoryDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALModifier.getDuplicateModifierCategoryByID(dt.Rows[n]["ModifierCategoryID"].ToString()) > 0)
                                    { objENTModifier.Mode = "UPDATE"; }
                                    else
                                    { objENTModifier.Mode = "ADD"; }
                                    objENTModifier.ModifierCategoryID = new Guid(dt.Rows[n]["ModifierCategoryID"].ToString());
                                    objENTModifier.ModifierCategoryName = Convert.ToString(dt.Rows[n]["ModifierCategoryName"]);
                                    objENTModifier.IsForced = Convert.ToBoolean(dt.Rows[n]["IsForced"].ToString());
                                    objENTModifier.ProductID = new Guid(dt.Rows[n]["ProductID"].ToString());
                                    objENTModifier.Sort = Convert.ToInt32(dt.Rows[n]["Sort"]);
                                    objENTModifier.ModifierCategoryDetail_Id = Convert.ToInt32(dt.Rows[n]["ModifierCategoryDetail_Id"]);
                                    objENTModifier.IsUPStream = 1;
                                    if (objDALModifier.InsertUpdateDeleteModifierCategoryDetail(objENTModifier))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "ModifierDetail":
                                #region ModifierDetail
                                ENT.ModifierDetail objENTModifierDetail = new ENT.ModifierDetail();
                                ModifierDetail objDALModifierDetail = new ModifierDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALModifierDetail.getDuplicateModifierDetailByID(dt.Rows[n]["IngredientsID"].ToString()) > 0)
                                    { objENTModifierDetail.Mode = "UPDATE"; }
                                    else
                                    { objENTModifierDetail.Mode = "ADD"; }
                                    objENTModifierDetail.IngredientsID = new Guid(dt.Rows[n]["IngredientsID"].ToString());
                                    objENTModifierDetail.Name = Convert.ToString(dt.Rows[n]["Name"]);
                                    objENTModifierDetail.Qty = Convert.ToInt32(dt.Rows[n]["Qty"]);
                                    objENTModifierDetail.Price = Convert.ToDecimal(dt.Rows[n]["Price"]);
                                    objENTModifierDetail.IsDefault = Convert.ToBoolean(dt.Rows[n]["IsDefault"]);
                                    objENTModifierDetail.IsQty = Convert.ToBoolean(dt.Rows[n]["IsQty"]);
                                    objENTModifierDetail.UnitTypeID = new Guid(dt.Rows[n]["UnitTypeID"].ToString());
                                    objENTModifierDetail.UnitType = Convert.ToString(dt.Rows[n]["UnitType"]);
                                    objENTModifierDetail.Sort = Convert.ToInt32(dt.Rows[n]["Sort"]);
                                    objENTModifierDetail.ModifierCategoryDetail_Id = Convert.ToInt32(dt.Rows[n]["ModifierCategoryDetail_Id"]);
                                    objENTModifierDetail.IsUPStream = 1;
                                    if (objDALModifierDetail.InsertUpdateDeleteModifierDetail(objENTModifierDetail))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "TaxGroupDetail":
                                #region TaxGroupDetail
                                ENT.TaxGroupDetail objENTTaxGroup = new ENT.TaxGroupDetail();
                                TaxGroupDetail objDALTaxGroup = new TaxGroupDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALTaxGroup.getDuplicateTaxGroupByTaxGroupID(dt.Rows[n]["TaxGroupID"].ToString()) > 0)
                                    { objENTTaxGroup.Mode = "UPDATE"; }
                                    else
                                    { objENTTaxGroup.Mode = "ADD"; }
                                    objENTTaxGroup.TaxGroupID = new Guid(dt.Rows[n]["TaxGroupID"].ToString());
                                    objENTTaxGroup.Name = Convert.ToString(dt.Rows[n]["Name"]);
                                    objENTTaxGroup.Percentage = Convert.ToDecimal(dt.Rows[n]["Percentage"]);
                                    objENTTaxGroup.ParentID = new Guid(dt.Rows[n]["ParentID"].ToString());
                                    objENTTaxGroup.PartnerID = new Guid(dt.Rows[n]["PartnerID"].ToString());
                                    objENTTaxGroup.Action = Convert.ToString(dt.Rows[n]["Action"]);
                                    objENTTaxGroup.Sign = Convert.ToString(dt.Rows[n]["Sign"]);
                                    objENTTaxGroup.Sort = Convert.ToInt32(dt.Rows[n]["Sort"]);
                                    objENTTaxGroup.TaxOnProductType = Convert.ToInt32(dt.Rows[n]["TaxOnProductType"]);
                                    objENTTaxGroup.IsUPStream = 1;
                                    if (objDALTaxGroup.InsertUpdateDeleteTaxGroupDetail(objENTTaxGroup))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "PaymentGatewayMaster":
                                #region PaymentGatewayMaster
                                ENT.PaymentGatewayMaster objENTPayment = new ENT.PaymentGatewayMaster();
                                PaymentGatewayMaster objDALPayment = new PaymentGatewayMaster();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALPayment.getDuplicatePaymentGatewayByID(dt.Rows[n]["PaymentGatewayID"].ToString()) > 0)
                                    { objENTPayment.Mode = "UPDATE"; }
                                    else
                                    { objENTPayment.Mode = "ADD"; }
                                    objENTPayment.PaymentGatewayID = new Guid(dt.Rows[n]["PaymentGatewayID"].ToString());
                                    objENTPayment.PaymentGatewayName = Convert.ToString(dt.Rows[n]["PaymentGatewayName"]);
                                    objENTPayment.MerchantID = Convert.ToString(dt.Rows[n]["MerchantID"]);
                                    objENTPayment.TokenKey = Convert.ToString(dt.Rows[n]["TokenKey"]);
                                    objENTPayment.UserName = Convert.ToString(dt.Rows[n]["UserName"]);
                                    objENTPayment.Password = Convert.ToString(dt.Rows[n]["Password"]);
                                    objENTPayment.ResponseUrl = Convert.ToString(dt.Rows[n]["ResponseUrl"]);
                                    objENTPayment.ATOMTransactionType = Convert.ToString(dt.Rows[n]["ATOMTransactionType"]);
                                    objENTPayment.Productid = Convert.ToString(dt.Rows[n]["Productid"]);
                                    objENTPayment.Version = Convert.ToString(dt.Rows[n]["Version"]);
                                    objENTPayment.ServiceID = Convert.ToString(dt.Rows[n]["ServiceID"]);
                                    objENTPayment.ApplicationProfileId = Convert.ToString(dt.Rows[n]["ApplicationProfileId"]);
                                    objENTPayment.MerchantProfileId = Convert.ToString(dt.Rows[n]["MerchantProfileId"]);
                                    objENTPayment.MerchantProfileName = Convert.ToString(dt.Rows[n]["MerchantProfileName"]);
                                    objENTPayment.IsUPStream = 1;
                                    if (objDALPayment.InsertUpdateDeletePaymentGatewayMaster(objENTPayment))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "IngredientsMasterDetail":
                                #region IngredientsMasterDetail
                                ENT.IngredientsMasterDetail objENTIngredients = new ENT.IngredientsMasterDetail();
                                IngredientsMasterDetail objDALIngredients = new IngredientsMasterDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALIngredients.getDuplicateIngredientsMasterByID(dt.Rows[n]["IngredientsID"].ToString()) > 0)
                                    { objENTIngredients.Mode = "UPDATE"; }
                                    else
                                    { objENTIngredients.Mode = "ADD"; }
                                    objENTIngredients.IngredientsID = new Guid(dt.Rows[n]["IngredientsID"].ToString());
                                    objENTIngredients.IngredientName = Convert.ToString(dt.Rows[n]["IngredientName"]);
                                    objENTIngredients.IngredientsMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["IngredientsMasterDetail_Id"]);
                                    objENTIngredients.IsUPStream = 1;
                                    if (objDALIngredients.InsertUpdateDeleteIngredientsMasterDetail(objENTIngredients))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "IngredientUnitTypeDetail":
                                #region IngredientUnitTypeDetail
                                ENT.IngredientUnitTypeDetail objENTUnitType = new ENT.IngredientUnitTypeDetail();
                                IngredientUnitTypeDetail objDALUnitType = new IngredientUnitTypeDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (n == 0)
                                    {
                                        objDALUnitType.deleteIngredientUnitTypeDetail();
                                    }
                                    //if (objDALUnitType.getDuplicateIngredientUnitTypeByID(dt.Rows[n]["UnitTypeID"].ToString()) > 0)
                                    //{ objENTUnitType.Mode = "UPDATE"; }
                                    //else
                                    //{ objENTUnitType.Mode = "ADD"; }
                                    objENTUnitType.Mode = "ADD";
                                    objENTUnitType.UnitTypeID = new Guid(dt.Rows[n]["UnitTypeID"].ToString());
                                    objENTUnitType.UnitType = Convert.ToString(dt.Rows[n]["UnitType"]);
                                    objENTUnitType.Qty = Convert.ToDecimal(dt.Rows[n]["Qty"]);
                                    objENTUnitType.IngredientsMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["IngredientsMasterDetail_Id"]);
                                    objENTUnitType.IsUPStream = 1;
                                    if (objDALUnitType.InsertUpdateDeleteIngredientUnitTypeDetail(objENTUnitType))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "ShiftMaster":
                                #region ShiftMaster
                                ENT.ShiftMaster objENTShift = new ENT.ShiftMaster();
                                ShiftMaster objDALShift = new ShiftMaster();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALShift.getDuplicateShiftByID(dt.Rows[n]["ShiftID"].ToString()) > 0)
                                    { objENTShift.Mode = "UPDATE"; }
                                    else
                                    { objENTShift.Mode = "ADD"; }
                                    objENTShift.ShiftID = new Guid(dt.Rows[n]["ShiftID"].ToString());
                                    objENTShift.ShiftName = Convert.ToString(dt.Rows[n]["ShiftName"]);
                                    objENTShift.ShiftMaster_Id = Convert.ToInt32(dt.Rows[n]["ShiftMaster_Id"]);

                                    if (objDALShift.InsertUpdateDeleteShiftMaster(objENTShift))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "ShiftMasterDetail":
                                #region ShiftMasterDetail
                                ENT.ShiftMasterDetail objENTShiftDetail = new ENT.ShiftMasterDetail();
                                ShiftMasterDetail objDALShiftDetail = new ShiftMasterDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALShiftDetail.getDuplicateShiftMasterDetailByID(dt.Rows[n]["ShiftDetailsID"].ToString()) > 0)
                                    { objENTShiftDetail.Mode = "UPDATE"; }
                                    else
                                    { objENTShiftDetail.Mode = "ADD"; }
                                    objENTShiftDetail.ShiftDetailsID = new Guid(dt.Rows[n]["ShiftDetailsID"].ToString());
                                    objENTShiftDetail.ShiftFromTime = Convert.ToString(dt.Rows[n]["ShiftFromTime"]);
                                    objENTShiftDetail.ShiftToTime = Convert.ToString(dt.Rows[n]["ShiftToTime"]);
                                    objENTShiftDetail.ShiftDay = Convert.ToInt32(dt.Rows[n]["ShiftDay"]);
                                    objENTShiftDetail.FirstSlot = Convert.ToDecimal(dt.Rows[n]["FirstSlot"]);
                                    objENTShiftDetail.SecondSlot = Convert.ToDecimal(dt.Rows[n]["SecondSlot"]);
                                    objENTShiftDetail.FinalSlot = Convert.ToDecimal(dt.Rows[n]["FinalSlot"]);
                                    objENTShiftDetail.ShiftDiff = Convert.ToString(dt.Rows[n]["ShiftDiff"]);
                                    objENTShiftDetail.ShiftMaster_ID = Convert.ToInt32(dt.Rows[n]["ShiftMaster_ID"]);

                                    if (objDALShiftDetail.InsertUpdateDeleteShiftMasterDetail(objENTShiftDetail))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "ShiftWiseEmployee":
                                #region ShiftWiseEmployee
                                ENT.ShiftWiseEmployee objENTSWE = new ENT.ShiftWiseEmployee();
                                ShiftWiseEmployee objDALSWE = new ShiftWiseEmployee();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALSWE.getDuplicateByShiftWiseEmployeeID(dt.Rows[n]["ShiftWiseEmployeeID"].ToString()) > 0)
                                    { objENTSWE.Mode = "UPDATE"; }
                                    else
                                    { objENTSWE.Mode = "ADD"; }
                                    objENTSWE.ShiftWiseEmployeeID = new Guid(dt.Rows[n]["ShiftWiseEmployeeID"].ToString());
                                    objENTSWE.EmployeeID = new Guid(dt.Rows[n]["EmployeeID"].ToString());
                                    objENTSWE.ShiftID = new Guid(dt.Rows[n]["ShiftID"].ToString());
                                    //objENTSWE.RUserID = new Guid(dt.Rows[n]["RUserID"].ToString());
                                    //objENTSWE.RUserType = Convert.ToInt32(dt.Rows[n]["RUserType"]);
                                    //objENTSWE.CreatedDate = dt.Rows[n]["CreatedDate"] == null ? DateTime.Now : Convert.ToDateTime(dt.Rows[n]["CreatedDate"]);
                                    //objENTSWE.IsStatus = Convert.ToBoolean(dt.Rows[n]["IsStatus"]);
                                    objENTSWE.ShiftDay = Convert.ToInt32(dt.Rows[n]["ShiftDay"]);
                                    objENTSWE.IsUPStream = 1;
                                    if (objDALSWE.InsertUpdateDeleteShiftWiseEmployee(objENTSWE))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "ModuleMasterDetail":
                                #region ModuleMasterDetail
                                ENT.ModuleMasterDetail objENTModule = new ENT.ModuleMasterDetail();
                                ModuleMasterDetail objDALModule = new ModuleMasterDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALModule.getDuplicateModuleMasterDetailByID(dt.Rows[n]["ModuleID"].ToString()) > 0)
                                    { objENTModule.Mode = "UPDATE"; }
                                    else
                                    { objENTModule.Mode = "ADD"; }
                                    objENTModule.ModuleID = new Guid(dt.Rows[n]["ModuleID"].ToString());
                                    objENTModule.ModuleName = Convert.ToString(dt.Rows[n]["ModuleName"]);
                                    objENTModule.NoOfModule = Convert.ToInt32(dt.Rows[n]["NoOfModule"]);
                                    objENTModule.ModuleMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["ModuleMasterDetail_Id"]);

                                    if (objDALModule.InsertUpdateDeleteModuleMasterDetail(objENTModule))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "RoleMasterDetail":
                                #region RoleMasterDetail
                                ENT.RoleMasterDetail objENTRole = new ENT.RoleMasterDetail();
                                RoleMasterDetail objDALRole = new RoleMasterDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALRole.getDuplicateRoleMasterDetailByID(dt.Rows[n]["RoleID"].ToString()) > 0)
                                    { objENTRole.Mode = "UPDATE"; }
                                    else
                                    { objENTRole.Mode = "ADD"; }
                                    objENTRole.RoleID = new Guid(dt.Rows[n]["RoleID"].ToString());
                                    objENTRole.RoleName = Convert.ToString(dt.Rows[n]["RoleName"]);
                                    objENTRole.RoleMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["RoleMasterDetail_Id"]);
                                    objENTRole.ModuleMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["ModuleMasterDetail_Id"]);
                                    if (objDALRole.InsertUpdateDeleteRoleMasterDetail(objENTRole))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "FeatureDetail":
                                #region FeatureDetail
                                ENT.FeatureDetail objENTFeature = new ENT.FeatureDetail();
                                FeatureDetail objDALFeature = new FeatureDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALFeature.getDuplicateFeatureDetailByID(dt.Rows[n]["FeatureDetail_Id"].ToString()) > 0)
                                    { objENTFeature.Mode = "UPDATE"; }
                                    else
                                    { objENTFeature.Mode = "ADD"; }
                                    objENTFeature.FeatureCode = Convert.ToInt32(dt.Rows[n]["FeatureCode"]);
                                    objENTFeature.FeatureName = Convert.ToString(dt.Rows[n]["FeatureName"]);
                                    objENTFeature.FeatureDetail_Id = Convert.ToInt32(dt.Rows[n]["FeatureDetail_Id"]);
                                    objENTFeature.RoleMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["RoleMasterDetail_Id"]);
                                    if (objDALFeature.InsertUpdateDeleteFeatureDetail(objENTFeature))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "SubFeatureDetail":
                                #region SubFeatureDetail
                                ENT.SubFeatureDetail objENTSubFeature = new ENT.SubFeatureDetail();
                                SubFeatureDetail objDALSubFeature = new SubFeatureDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALSubFeature.getDuplicateSubFeatureDetailByID(dt.Rows[n]["SubFeatureDetail_Id"].ToString()) > 0)
                                    { objENTSubFeature.Mode = "UPDATE"; }
                                    else
                                    { objENTSubFeature.Mode = "ADD"; }
                                    objENTSubFeature.SubFeatureCode = Convert.ToInt32(dt.Rows[n]["SubFeatureCode"]);
                                    objENTSubFeature.SubFeatureName = Convert.ToString(dt.Rows[n]["SubFeatureName"]);
                                    objENTSubFeature.SubFeatureDetail_Id = Convert.ToInt32(dt.Rows[n]["SubFeatureDetail_Id"]);
                                    objENTSubFeature.FeatureDetail_Id = Convert.ToInt32(dt.Rows[n]["FeatureDetail_Id"]);
                                    if (objDALSubFeature.InsertUpdateDeleteSubFeatureDetail(objENTSubFeature))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "RightDetail":
                                #region RightDetail
                                ENT.RightDetail objENTRight = new ENT.RightDetail();
                                RightDetail objDALRight = new RightDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALRight.getDuplicateRightDetailByID(dt.Rows[n]["SubFeatureDetail_Id"].ToString(), dt.Rows[n]["RightCode"].ToString()) > 0)
                                    { objENTRight.Mode = "UPDATE"; }
                                    else
                                    { objENTRight.Mode = "ADD"; }
                                    objENTRight.RightCode = Convert.ToInt32(dt.Rows[n]["RightCode"]);
                                    objENTRight.RightName = Convert.ToString(dt.Rows[n]["RightName"]);
                                    objENTRight.SubFeatureDetail_Id = Convert.ToInt32(dt.Rows[n]["SubFeatureDetail_Id"]);
                                    if (objDALRight.InsertUpdateDeleteRightDetail(objENTRight))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "ModuleAppIDDetail":
                                #region ModuleAppIDDetail
                                ENT.ModuleAppIDDetail objENTApp = new ENT.ModuleAppIDDetail();
                                ModuleAppIDDetail objDALApp = new ModuleAppIDDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (n == 0)
                                    {
                                        objDALApp.deleteModuleAppIDDetail();
                                    }
                                    objENTApp.Mode = "ADD";
                                    objENTApp.AppID = Convert.ToString(dt.Rows[n]["AppID"]);
                                    objENTApp.DeviceName = Convert.ToString(dt.Rows[n]["DeviceName"]);
                                    objENTApp.ModuleMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["ModuleMasterDetail_Id"]);
                                    if (objDALApp.InsertUpdateDeleteModuleAppIDDetail(objENTApp))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "TableMasterDetail":
                                #region TableMasterDetail
                                ENT.TableMasterDetail objENTTable = new ENT.TableMasterDetail();
                                TableMasterDetail objDALTable = new TableMasterDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALTable.getDuplicateTableMasterDetailByID(dt.Rows[n]["TableID"].ToString()) > 0)
                                    { objENTTable.Mode = "UPDATE"; }
                                    else
                                    { objENTTable.Mode = "ADD"; }
                                    objENTTable.TableID = new Guid(dt.Rows[n]["TableID"].ToString());
                                    objENTTable.EmployeeID = new Guid(dt.Rows[n]["EmployeeID"].ToString());
                                    objENTTable.TableName = Convert.ToString(dt.Rows[n]["TableName"]);
                                    objENTTable.NoOfSeats = Convert.ToInt32(dt.Rows[n]["NoOfSeats"]);
                                    objENTTable.Location = Convert.ToString(dt.Rows[n]["Location"]);
                                    objENTTable.ClassID = new Guid(dt.Rows[n]["ClassID"].ToString());
                                    objENTTable.Sort = Convert.ToInt32(dt.Rows[n]["Sort"]);
                                    objENTTable.StatusID = Convert.ToInt32(ENT.APIStream.TableStatus.Vacant);
                                    objENTTable.IsUPStream = 1;
                                    if (objDALTable.InsertUpdateDeleteTableMasterDetail(objENTTable))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "ProductClassMasterDetail":
                                #region ProductClassMasterDetail
                                ENT.ProductClassMasterDetail objENTProductClass = new ENT.ProductClassMasterDetail();
                                ProductClassMasterDetail objDALProductClass = new ProductClassMasterDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALProductClass.getDuplicateProductClassMasterDetailByID(dt.Rows[n]["ClassID"].ToString()) > 0)
                                    { objENTProductClass.Mode = "UPDATE"; }
                                    else
                                    { objENTProductClass.Mode = "ADD"; }
                                    objENTProductClass.ClassID = new Guid(dt.Rows[n]["ClassID"].ToString());
                                    objENTProductClass.ClassName = Convert.ToString(dt.Rows[n]["ClassName"]);
                                    objENTProductClass.IsUPStream = 1;
                                    if (objDALProductClass.InsertUpdateDeleteProductClassMasterDetail(objENTProductClass))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "RecipeMasterData":
                                #region RecipeMasterData
                                ENT.RecipeMasterData objENTRecipe = new ENT.RecipeMasterData();
                                RecipeMasterData objDALRecipe = new RecipeMasterData();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALRecipe.getDuplicateRecipeMasterDataByID(dt.Rows[n]["RecipeID"].ToString()) > 0)
                                    { objENTRecipe.Mode = "UPDATE"; }
                                    else
                                    { objENTRecipe.Mode = "ADD"; }
                                    objENTRecipe.ProductID = new Guid(dt.Rows[n]["ProductID"].ToString());
                                    objENTRecipe.ProductName = Convert.ToString(dt.Rows[n]["ProductName"]);
                                    objENTRecipe.RecipeID = new Guid(dt.Rows[n]["RecipeID"].ToString());
                                    objENTRecipe.RecipeText = Convert.ToString(dt.Rows[n]["RecipeText"]);
                                    objENTRecipe.RecipeMasterData_Id = Convert.ToInt32(dt.Rows[n]["RecipeMasterData_Id"]);

                                    if (objDALRecipe.InsertUpdateDeleteRecipeMasterData(objENTRecipe))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "RecipeMasterDetail":
                                #region RecipeMasterDetail
                                ENT.RecipeMasterDetail objENTRecipeDetail = new ENT.RecipeMasterDetail();
                                RecipeMasterDetail objDALRecipeDetail = new RecipeMasterDetail();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALRecipeDetail.getDuplicateRecipeMasterDetailByID(dt.Rows[n]["IngredientsID"].ToString()) > 0)
                                    { objENTRecipeDetail.Mode = "UPDATE"; }
                                    else
                                    { objENTRecipeDetail.Mode = "ADD"; }
                                    objENTRecipeDetail.IngredientsID = new Guid(dt.Rows[n]["IngredientsID"].ToString());
                                    objENTRecipeDetail.Name = Convert.ToString(dt.Rows[n]["Name"]);
                                    objENTRecipeDetail.Qty = Convert.ToInt32(dt.Rows[n]["Qty"]);
                                    objENTRecipeDetail.Price = Convert.ToDecimal(dt.Rows[n]["Price"]);
                                    objENTRecipeDetail.IsDefault = Convert.ToBoolean(dt.Rows[n]["IsDefault"]);
                                    objENTRecipeDetail.IsQty = Convert.ToBoolean(dt.Rows[n]["IsQty"]);
                                    objENTRecipeDetail.UnitTypeID = new Guid(dt.Rows[n]["UnitTypeID"].ToString());
                                    objENTRecipeDetail.UnitType = Convert.ToString(dt.Rows[n]["UnitType"]);
                                    objENTRecipeDetail.RecipeMasterData_Id = Convert.ToInt32(dt.Rows[n]["RecipeMasterData_Id"]);
                                    objENTRecipeDetail.IsUPStream = 1;
                                    if (objDALRecipeDetail.InsertUpdateDeleteRecipeMasterDetail(objENTRecipeDetail))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "CustomerMasterData":
                                #region CustomerMasterData
                                ENT.CustomerMasterData objENTCustomer = new ENT.CustomerMasterData();
                                CustomerMasterData objDALCustomer = new CustomerMasterData();
                                for (int n = 0; n < dt.Rows.Count; n++)
                                {
                                    if (objDALCustomer.getDuplicateCustomerByID(dt.Rows[n]["CustomerID"].ToString()) > 0)
                                    { objENTCustomer.Mode = "UPDATE"; }
                                    else
                                    { objENTCustomer.Mode = "ADD"; }
                                    objENTCustomer.CustomerID = new Guid(dt.Rows[n]["CustomerID"].ToString());
                                    objENTCustomer.Name = Convert.ToString(dt.Rows[n]["Name"]);
                                    objENTCustomer.MobileNo = Convert.ToString(dt.Rows[n]["MobileNo"]);
                                    objENTCustomer.EmailID = Convert.ToString(dt.Rows[n]["EmailID"]);
                                    objENTCustomer.Birthdate = dt.Rows[n]["Birthdate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["Birthdate"].ToString());
                                    objENTCustomer.Address = Convert.ToString(dt.Rows[n]["Address"]);
                                    objENTCustomer.RUserID = new Guid(dt.Rows[n]["RUserID"].ToString());
                                    if (BranchSettingDetail.IsFranchise(BranchID))
                                    { objENTCustomer.RUserType = Convert.ToInt32(ENT.APIStream.R_USER_TYPE.FRANCHISE); }
                                    else { objENTCustomer.RUserType = Convert.ToInt32(ENT.APIStream.R_USER_TYPE.BRANCH); }
                                    objENTCustomer.IsUPStream = 1;
                                    if (objDALCustomer.InsertUpdateDeleteCustomer(objENTCustomer))
                                    {
                                        lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                    }
                                    else
                                    {
                                        lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                    }
                                }
                                #endregion
                                break;
                            case "RootObject":
                                break;
                            default:
                                lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                                break;
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        lines = lines + "Error => " + dt.TableName.ToString() + " => " + ex.Message.ToString() + "\r\n";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lines;
        }

        public string RestruantProfile_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_RESTAURANT_PROFILE);
                int i = 0;
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    strTable = dt.TableName.ToString();
                    switch (dt.TableName.ToString())
                    {
                        case "BranchMasterSetting":
                            #region BranchMasterSetting
                            ENT.BranchMasterSetting objENTBranch = new ENT.BranchMasterSetting();
                            BranchMasterSetting objDALBranch = new BranchMasterSetting();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALBranch.getDuplicateBranchMasterSettingByID(dt.Rows[n]["BranchID"].ToString()) > 0)
                                { objENTBranch.Mode = "UPDATE"; }
                                else
                                { objENTBranch.Mode = "ADD"; }
                                objENTBranch.BranchID = new Guid(dt.Rows[n]["BranchID"].ToString());
                                objENTBranch.RestaurantID = new Guid(dt.Rows[n]["RestaurantID"].ToString());
                                objENTBranch.ContactNoForService = Convert.ToString(dt.Rows[n]["ContactNoForService"]);
                                objENTBranch.DeliveryCharges = Convert.ToInt32(dt.Rows[n]["DeliveryCharges"]);
                                objENTBranch.DeliveryTime = dt.Rows[n]["DeliveryTime"].ToString() == "" ? null : Convert.ToString(dt.Rows[n]["DeliveryTime"]);
                                objENTBranch.PickupTime = dt.Rows[n]["PickupTime"].ToString() == "" ? null : Convert.ToString(dt.Rows[n]["PickupTime"]);
                                objENTBranch.CurrencyName = Convert.ToString(dt.Rows[n]["CurrencyName"].ToString());
                                objENTBranch.CurrencySymbol = Convert.ToString(dt.Rows[n]["CurrencySymbol"]);
                                objENTBranch.WorkingDays = Convert.ToString(dt.Rows[n]["WorkingDays"]);
                                objENTBranch.TagLine = Convert.ToString(dt.Rows[n]["TagLine"]);
                                objENTBranch.Footer = Convert.ToString(dt.Rows[n]["Footer"]);
                                objENTBranch.DeliveryAreaRedius = Convert.ToInt32(dt.Rows[n]["DeliveryAreaRedius"]);
                                objENTBranch.DeliveryAreaTitle = Convert.ToString(dt.Rows[n]["DeliveryAreaTitle"]);
                                objENTBranch.DistanceType = Convert.ToInt32(dt.Rows[n]["DistanceType"]);
                                objENTBranch.DistanceName = Convert.ToString(dt.Rows[n]["DistanceName"]);
                                objENTBranch.FreeDeliveryUpto = Convert.ToInt32(dt.Rows[n]["FreeDeliveryUpto"]);
                                objENTBranch.BranchName = Convert.ToString(dt.Rows[n]["BranchName"]);
                                objENTBranch.BranchEmailID = Convert.ToString(dt.Rows[n]["BranchEmailID"]);
                                objENTBranch.MobileNo = Convert.ToString(dt.Rows[n]["MobileNo"]);

                                DateTime dt1;
                                DateTime date = new DateTime();
                                //if (DateTime.TryParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "M/dd/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                //{
                                //    date = DateTime.ParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "M/dd/yyyy hh:mm:ss tt", null);
                                //}
                                //else if (DateTime.TryParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "M/dd/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                //{
                                //    date = DateTime.ParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "M/dd/yyyy h:mm:ss tt", null);
                                //}
                                //else if (DateTime.TryParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "MM/dd/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                //{
                                //    date = DateTime.ParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "MM/dd/yyyy hh:mm:ss tt", null);
                                //}
                                //else if (DateTime.TryParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "MM/dd/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                //{
                                //    date = DateTime.ParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "MM/dd/yyyy h:mm:ss tt", null);
                                //}
                                //else if (DateTime.TryParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                //{
                                //    date = DateTime.ParseExact(dt.Rows[n]["LastSyncDate"].ToString(), "dd/MM/yyyy hh:mm:ss tt", null);
                                //}

                                objENTBranch.LastSyncDate = dt.Rows[n]["LastSyncDate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["LastSyncDate"].ToString());
                                objENTBranch.VatNo = Convert.ToString(dt.Rows[n]["VatNo"]);
                                objENTBranch.CSTNo = Convert.ToString(dt.Rows[n]["CSTNo"]);
                                objENTBranch.ServiceTaxNo = Convert.ToString(dt.Rows[n]["ServiceTaxNo"]);
                                objENTBranch.TinGSTNo = Convert.ToString(dt.Rows[n]["TinGSTNo"]);
                                objENTBranch.Address = Convert.ToString(dt.Rows[n]["Address"]);
                                objENTBranch.SubAreaStreet = Convert.ToString(dt.Rows[n]["SubAreaStreet"]);
                                objENTBranch.PinCode = Convert.ToString(dt.Rows[n]["PinCode"]);
                                objENTBranch.VersionCode = Convert.ToString(dt.Rows[n]["VersionCode"]);
                                objENTBranch.BranchMasterSetting_Id = Convert.ToInt32(dt.Rows[n]["BranchMasterSetting_Id"]);

                                if (objDALBranch.InsertUpdateDeleteBranchMasterSetting(objENTBranch))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                                i++;
                            }
                            #endregion
                            break;
                        case "BranchSettingDetail":
                            #region BranchSettingDetail
                            ENT.BranchSettingDetail objENTBranchSetting = new ENT.BranchSettingDetail();
                            BranchSettingDetail objDALBranchSetting = new BranchSettingDetail();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALBranchSetting.getCoutBranchSettingByBranchID(BranchID) > 0)
                                { objENTBranchSetting.Mode = "UPDATE"; }
                                else
                                { objENTBranchSetting.Mode = "ADD"; }
                                objENTBranchSetting.IsFranchise = Convert.ToBoolean(dt.Rows[n]["IsFranchise"]);
                                objENTBranchSetting.IsReservationOn = Convert.ToBoolean(dt.Rows[n]["IsReservationOn"]);
                                objENTBranchSetting.IsOrderBookingOn = Convert.ToBoolean(dt.Rows[n]["IsOrderBookingOn"]);
                                objENTBranchSetting.IsAutoAcceptOrderOn = Convert.ToBoolean(dt.Rows[n]["IsAutoAcceptOrderOn"]);
                                objENTBranchSetting.IsAutoRoundOffTotalOn = Convert.ToBoolean(dt.Rows[n]["IsAutoRoundOffTotalOn"]);
                                objENTBranchSetting.TaxGroupId = Convert.ToInt32(dt.Rows[n]["TaxGroupId"]);
                                objENTBranchSetting.IsDemoVersion = Convert.ToBoolean(dt.Rows[n]["IsDemoVersion"]);
                                objENTBranchSetting.ExpireDate = Convert.ToString(dt.Rows[n]["ExpireDate"]);
                                objENTBranchSetting.BranchID = new Guid(BranchID);

                                if (objDALBranchSetting.InsertUpdateDeleteVersionDetail(objENTBranchSetting))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                                i++;
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string Staff_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_STAFF);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    strTable = dt.TableName.ToString();
                    switch (dt.TableName.ToString())
                    {
                        case "EmployeeMasterList":
                            #region EmployeeMasterList
                            ENT.EmployeeMasterList objENTEmployee = new ENT.EmployeeMasterList();
                            EmployeeMasterList objDALEmployee = new EmployeeMasterList();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALEmployee.getDuplicateEmployeeByID(dt.Rows[n]["EmployeeID"].ToString()) > 0)
                                { objENTEmployee.Mode = "UPDATE"; }
                                else
                                { objENTEmployee.Mode = "ADD"; }
                                objENTEmployee.EmployeeID = new Guid(dt.Rows[n]["EmployeeID"].ToString());
                                objENTEmployee.EmpCode = Convert.ToString(dt.Rows[n]["EmpCode"]);
                                objENTEmployee.EmpName = Convert.ToString(dt.Rows[n]["EmpName"]);
                                objENTEmployee.Password = Convert.ToString(dt.Rows[n]["Password"]);
                                objENTEmployee.Mobile = Convert.ToString(dt.Rows[n]["Mobile"]);
                                objENTEmployee.Email = Convert.ToString(dt.Rows[n]["Email"]);
                                objENTEmployee.RepotingTo = new Guid(dt.Rows[n]["RepotingTo"].ToString());
                                objENTEmployee.RoleID = new Guid(dt.Rows[n]["RoleID"].ToString());
                                objENTEmployee.RoleName = Convert.ToString(dt.Rows[n]["RoleName"]);
                                objENTEmployee.SalaryAmt = Convert.ToDecimal(dt.Rows[n]["SalaryAmt"]);
                                objENTEmployee.SalaryType = Convert.ToInt32(dt.Rows[n]["SalaryType"]);
                                DateTime dt1;  // 15/12/2017
                                DateTime date = new DateTime();
                                //if (DateTime.TryParseExact(dt.Rows[n]["JoinDate"].ToString(), "M/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                //{
                                //    date = DateTime.ParseExact(dt.Rows[n]["JoinDate"].ToString(), "M/dd/yyyy", null);
                                //}
                                //else if (DateTime.TryParseExact(dt.Rows[n]["JoinDate"].ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                //{
                                //    date = DateTime.ParseExact(dt.Rows[n]["JoinDate"].ToString(), "MM/dd/yyyy", null);
                                //}
                                //else if (DateTime.TryParseExact(dt.Rows[n]["JoinDate"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1))
                                //{
                                //    date = DateTime.ParseExact(dt.Rows[n]["JoinDate"].ToString(), "dd/MM/yyyy", null);
                                //}
                                objENTEmployee.JoinDate = dt.Rows[n]["JoinDate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["JoinDate"].ToString());
                                objENTEmployee.IsDisplayInKDS = Convert.ToInt32(dt.Rows[n]["IsDisplayInKDS"]);
                                objENTEmployee.ClassID = new Guid(dt.Rows[n]["ClassID"].ToString());
                                objENTEmployee.Gender = Convert.ToInt32(dt.Rows[n]["Gender"]);
                                objENTEmployee.TotalHourInADay = Convert.ToInt32(dt.Rows[n]["TotalHourInADay"]);
                                objENTEmployee.IsUPStream = 1;
                                if (objDALEmployee.InsertUpdateDeleteEmployee(objENTEmployee))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "EmployeeShift":
                            #region EmployeeShift
                            ENT.EmployeeShift objENTEmpShift = new ENT.EmployeeShift();
                            EmployeeShift objDALEmpShift = new EmployeeShift();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALEmpShift.getDuplicateEmployeeShiftByEmpID(dt.Rows[n]["EmployeeID"].ToString()) > 0)
                                { objENTEmpShift.Mode = "UPDATE"; }
                                else
                                { objENTEmpShift.Mode = "ADD"; }
                                objENTEmpShift.ShiftID = new Guid(dt.Rows[n]["ShiftID"].ToString());
                                objENTEmpShift.EmployeeID = new Guid(dt.Rows[n]["EmployeeID"].ToString());
                                if (objDALEmpShift.InsertUpdateDeleteEmployeeShift(objENTEmpShift))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string Menu_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_MENU);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG========================\r\n\n";
                CategoryMaster objCat = new CategoryMaster();

                foreach (DataTable dt in ds.Tables)
                {
                    strTable = dt.TableName.ToString();
                    switch (dt.TableName.ToString())
                    {
                        case "CategoryDetails":
                            #region CategoryMaster
                            ENT.CategoryMaster objENTCat = new ENT.CategoryMaster();
                            CategoryMaster objDALCat = new CategoryMaster();
                            objCat.DeleteCategoryForDownStream(1);
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                //if (objDALCat.getDuplicateCategoryByID(dt.Rows[n]["CategoryID"].ToString()) > 0)
                                //{ objENTCat.Mode = "UPDATE"; }
                                //else
                                //{ objENTCat.Mode = "ADD"; }
                                objENTCat.Mode = "ADD";
                                objENTCat.CategoryID = new Guid(dt.Rows[n]["CategoryID"].ToString());
                                objENTCat.CategoryName = Convert.ToString(dt.Rows[n]["CategoryName"]);
                                objENTCat.ImgPath = Convert.ToString(dt.Rows[n]["ImgPath"]);
                                objENTCat.ParentID = new Guid(dt.Rows[n]["MainCategoryID"].ToString());
                                objENTCat.ClassMasterID = new Guid(dt.Rows[n]["ClassMasterID"].ToString());
                                objENTCat.Priority = Convert.ToInt32(dt.Rows[n]["Priority"].ToString());
                                objENTCat.IsCategory = 1;
                                objENTCat.IsUPStream = 1;
                                if (objDALCat.InsertUpdateDeleteCategoryMaster(objENTCat))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "SubCategoryDetail":
                            #region SubCategoryDetail
                            ENT.CategoryMaster objENTSubCat = new ENT.CategoryMaster();
                            CategoryMaster objDALSubCat = new CategoryMaster();
                            objCat.DeleteCategoryForDownStream(2);
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                //if (objDALSubCat.getDuplicateSubCategoryByID(dt.Rows[n]["CategoryID"].ToString()) > 0)
                                //{ objENTSubCat.Mode = "UPDATE"; }
                                //else
                                //{ objENTSubCat.Mode = "ADD"; }
                                objENTSubCat.Mode = "ADD";
                                objENTSubCat.CategoryID = new Guid(dt.Rows[n]["CategoryID"].ToString());
                                objENTSubCat.CategoryName = Convert.ToString(dt.Rows[n]["CategoryName"]);
                                objENTSubCat.ImgPath = Convert.ToString(dt.Rows[n]["ImgPath"]);
                                objENTSubCat.ParentID = new Guid(dt.Rows[n]["MainCategoryID"].ToString());
                                objENTSubCat.ClassMasterID = new Guid(dt.Rows[n]["ClassMasterID"].ToString());
                                objENTSubCat.Priority = Convert.ToInt32(dt.Rows[n]["Priority"].ToString());
                                objENTSubCat.IsCategory = 2;
                                objENTSubCat.IsUPStream = 1;
                                if (objDALSubCat.InsertUpdateDeleteCategoryMaster(objENTSubCat))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "CategoryWiseProduct":
                            #region CategoryWiseProduct
                            ENT.CategoryWiseProduct objENTProduct = new ENT.CategoryWiseProduct();
                            CategoryWiseProduct objDALProduct = new CategoryWiseProduct();
                            objDALProduct.DeleteProductForDownStream();

                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                //if (objDALProduct.getDuplicateProductByID(dt.Rows[n]["ProductID"].ToString(), dt.Rows[n]["CategoryID"].ToString()) > 0)
                                //{ objENTProduct.Mode = "UPDATE"; }
                                //else
                                //{ objENTProduct.Mode = "ADD"; }
                                objENTProduct.Mode = "ADD";
                                objENTProduct.CategoryID = new Guid(dt.Rows[n]["CategoryID"].ToString());
                                objENTProduct.DiscountID = new Guid(dt.Rows[n]["DiscountID"].ToString());
                                objENTProduct.ProductID = new Guid(dt.Rows[n]["ProductID"].ToString());
                                objENTProduct.ProductName = Convert.ToString(dt.Rows[n]["ProductName"]);
                                objENTProduct.Price = Convert.ToDecimal(dt.Rows[n]["Price"].ToString());
                                objENTProduct.ImgPath = Convert.ToString(dt.Rows[n]["ImgPath"]);
                                objENTProduct.IsUrl = Convert.ToBoolean(dt.Rows[n]["IsUrl"]);
                                objENTProduct.Calorie = Convert.ToString(dt.Rows[n]["Calorie"]);
                                objENTProduct.ShortDescription = Convert.ToString(dt.Rows[n]["ShortDescription"]);
                                objENTProduct.IsNonVeg = Convert.ToBoolean(dt.Rows[n]["IsNonVeg"]);
                                objENTProduct.IsTrendingItem = Convert.ToBoolean(dt.Rows[n]["IsTrendingItem"]);
                                objENTProduct.ApproxCookingTime = dt.Rows[n]["ApproxCookingTime"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["ApproxCookingTime"].ToString());
                                objENTProduct.IsAellergic = Convert.ToBoolean(dt.Rows[n]["IsAellergic"]);
                                objENTProduct.Extras = dt.Rows[n]["Extras"].ToString() == "" ? null : Convert.ToString(dt.Rows[n]["Extras"]);
                                objENTProduct.IsVisibleToB2C = Convert.ToBoolean(dt.Rows[n]["IsVisibleToB2C"]);
                                objENTProduct.ExpiryDateFrom = dt.Rows[n]["ExpiryDateFrom"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["ExpiryDateFrom"].ToString());
                                objENTProduct.ExpiryDateTo = dt.Rows[n]["ExpiryDateTo"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["ExpiryDateTo"].ToString());
                                objENTProduct.StationID = new Guid(dt.Rows[n]["StationID"].ToString());
                                objENTProduct.SuggestiveItems = Convert.ToString(dt.Rows[n]["SuggestiveItems"]);
                                objENTProduct.IsCold = Convert.ToBoolean(dt.Rows[n]["IsCold"]);
                                objENTProduct.IsDrink = Convert.ToBoolean(dt.Rows[n]["IsDrink"]);
                                objENTProduct.DiningOptions = Convert.ToInt32(dt.Rows[n]["DiningOptions"]);
                                objENTProduct.AllowPriceOverride = Convert.ToInt32(dt.Rows[n]["AllowPriceOverride"]);
                                objENTProduct.IsAgeValidation = Convert.ToBoolean(dt.Rows[n]["IsAgeValidation"]);
                                objENTProduct.AgeForValidation = Convert.ToInt32(dt.Rows[n]["AgeForValidation"]);
                                objENTProduct.OverridePrice = Convert.ToDecimal(dt.Rows[n]["OverridePrice"]);
                                objENTProduct.IsCombo = Convert.ToBoolean(dt.Rows[n]["IsCombo"]);
                                objENTProduct.IsDisplayModifire = Convert.ToBoolean(dt.Rows[n]["IsDisplayModifire"]);
                                objENTProduct.ProductCode = Convert.ToString(dt.Rows[n]["ProductCode"]);
                                objENTProduct.TaxPercentage = Convert.ToDecimal(dt.Rows[n]["TaxPercentage"]);
                                objENTProduct.Sort = Convert.ToInt32(dt.Rows[n]["Sort"]);
                                objENTProduct.Priority = Convert.ToInt32(dt.Rows[n]["Priority"]);
                                objENTProduct.IsUPStream = 1;

                                if (objDALProduct.InsertUpdateDeleteProduct(objENTProduct))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "ComboDetail":
                            #region ComboDetail
                            ENT.ComboDetail objENTCombo = new ENT.ComboDetail();
                            ComboDetail objDALCombo = new ComboDetail();
                            objDALCombo.DeleteComboDetailForDownStream();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                //if (objDALCombo.getDuplicateComboDetailBySetID(dt.Rows[n]["ComboSetID"].ToString()) > 0)
                                //{ objENTCombo.Mode = "UPDATE"; }
                                //else
                                //{ objENTCombo.Mode = "ADD"; }
                                objENTCombo.Mode = "ADD";
                                objENTCombo.ComboSetID = new Guid(dt.Rows[n]["ComboSetID"].ToString());
                                objENTCombo.ComboSetName = Convert.ToString(dt.Rows[n]["ComboSetName"]);
                                objENTCombo.CategoryID = new Guid(dt.Rows[n]["CategoryID"].ToString());
                                objENTCombo.CProductID = new Guid(dt.Rows[n]["CProductID"].ToString());
                                objENTCombo.ComboDetail_Id = Convert.ToInt32(dt.Rows[n]["ComboDetail_Id"].ToString());
                                objENTCombo.CategoryWiseProduct_Id = Convert.ToInt32(dt.Rows[n]["CategoryWiseProduct_Id"]);
                                objENTCombo.IsUPStream = 1;
                                if (objDALCombo.InsertUpdateDeleteComboDetail(objENTCombo))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "ComboProductDetail":
                            #region ComboProductDetail
                            ENT.ComboProductDetail objENTCPD = new ENT.ComboProductDetail();
                            ComboProductDetail objDALCPD = new ComboProductDetail();
                            objDALCPD.DeleteComboProductDetailForDownStream();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                //if (objDALCPD.getDuplicateComboDetailBySetID(dt.Rows[n]["ProductID"].ToString()) > 0)
                                //{ objENTCPD.Mode = "UPDATE"; }
                                //else
                                //{ objENTCPD.Mode = "ADD"; }
                                objENTCPD.Mode = "ADD";
                                objENTCPD.ProductID = new Guid(dt.Rows[n]["ProductID"].ToString());
                                objENTCPD.IsDefault = Convert.ToBoolean(dt.Rows[n]["IsDefault"]);
                                objENTCPD.ProductName = Convert.ToString(dt.Rows[n]["ProductName"]);
                                objENTCPD.ComboDetail_Id = Convert.ToInt32(dt.Rows[n]["ComboDetail_Id"].ToString());
                                objENTCPD.IsUPStream = 1;
                                if (objDALCPD.InsertUpdateDeleteComboProductDetail(objENTCPD))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string Ingredients_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_INGREDIENTS);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    strTable = dt.TableName.ToString();
                    switch (dt.TableName.ToString())
                    {
                        case "IngredientsMasterDetail":
                            #region IngredientsMasterDetail
                            ENT.IngredientsMasterDetail objENTIngredients = new ENT.IngredientsMasterDetail();
                            IngredientsMasterDetail objDALIngredients = new IngredientsMasterDetail();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALIngredients.getDuplicateIngredientsMasterByID(dt.Rows[n]["IngredientsID"].ToString()) > 0)
                                { objENTIngredients.Mode = "UPDATE"; }
                                else
                                { objENTIngredients.Mode = "ADD"; }
                                objENTIngredients.IngredientsID = new Guid(dt.Rows[n]["IngredientsID"].ToString());
                                objENTIngredients.IngredientName = Convert.ToString(dt.Rows[n]["IngredientName"]);
                                objENTIngredients.IngredientsMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["IngredientsMasterDetail_Id"]);
                                objENTIngredients.IsUPStream = 1;
                                if (objDALIngredients.InsertUpdateDeleteIngredientsMasterDetail(objENTIngredients))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "IngredientUnitTypeDetail":
                            #region IngredientUnitTypeDetail
                            ENT.IngredientUnitTypeDetail objENTUnitType = new ENT.IngredientUnitTypeDetail();
                            IngredientUnitTypeDetail objDALUnitType = new IngredientUnitTypeDetail();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (n == 0)
                                {
                                    objDALUnitType.deleteIngredientUnitTypeDetail();
                                }
                                objENTUnitType.Mode = "ADD";
                                objENTUnitType.UnitTypeID = new Guid(dt.Rows[n]["UnitTypeID"].ToString());
                                objENTUnitType.UnitType = Convert.ToString(dt.Rows[n]["UnitType"]);
                                objENTUnitType.Qty = Convert.ToDecimal(dt.Rows[n]["Qty"]);
                                objENTUnitType.IngredientsMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["IngredientsMasterDetail_Id"]);
                                objENTUnitType.IsUPStream = 1;
                                if (objDALUnitType.InsertUpdateDeleteIngredientUnitTypeDetail(objENTUnitType))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string Modifier_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_MODIFIER);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    strTable = dt.TableName.ToString();
                    switch (dt.TableName.ToString())
                    {
                        case "ModifierCategoryDetail":
                            #region ModifierCategoryDetail
                            ENT.ModifierCategoryDetail objENTModifier = new ENT.ModifierCategoryDetail();
                            ModifierCategoryDetail objDALModifier = new ModifierCategoryDetail();
                            objDALModifier.DeleteModifierCategoryForDownStream();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                //if (objDALModifier.getDuplicateModifierCategoryByID(dt.Rows[n]["ModifierCategoryID"].ToString()) > 0)
                                //{ objENTModifier.Mode = "UPDATE"; }
                                //else
                                //{ objENTModifier.Mode = "ADD"; }
                                objENTModifier.Mode = "ADD";
                                objENTModifier.ModifierCategoryID = new Guid(dt.Rows[n]["ModifierCategoryID"].ToString());
                                objENTModifier.ModifierCategoryName = Convert.ToString(dt.Rows[n]["ModifierCategoryName"]);
                                objENTModifier.IsForced = Convert.ToBoolean(dt.Rows[n]["IsForced"].ToString());
                                objENTModifier.ProductID = new Guid(dt.Rows[n]["ProductID"].ToString());
                                //objENTModifier.Sort = Convert.ToInt32(dt.Rows[n]["Sort"]);
                                objENTModifier.ModifierCategoryDetail_Id = Convert.ToInt32(dt.Rows[n]["ModifierCategoryDetail_Id"]);
                                objENTModifier.IsUPStream = 1;

                                if (objDALModifier.InsertUpdateDeleteModifierCategoryDetail(objENTModifier))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "ModifierDetail":
                            #region ModifierDetail
                            ENT.ModifierDetail objENTModifierDetail = new ENT.ModifierDetail();
                            ModifierDetail objDALModifierDetail = new ModifierDetail();
                            objDALModifierDetail.DeleteModifierDetailForDownStream();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                //if (objDALModifierDetail.getDuplicateModifierDetailByID(dt.Rows[n]["IngredientsID"].ToString()) > 0)
                                //{ objENTModifierDetail.Mode = "UPDATE"; }
                                //else
                                //{ objENTModifierDetail.Mode = "ADD"; }
                                objENTModifierDetail.Mode = "ADD";
                                objENTModifierDetail.IngredientsID = new Guid(dt.Rows[n]["IngredientsID"].ToString());
                                objENTModifierDetail.Name = Convert.ToString(dt.Rows[n]["Name"]);
                                objENTModifierDetail.Qty = Convert.ToInt32(dt.Rows[n]["Qty"]);
                                objENTModifierDetail.Price = Convert.ToDecimal(dt.Rows[n]["Price"]);
                                objENTModifierDetail.IsDefault = Convert.ToBoolean(dt.Rows[n]["IsDefault"]);
                                objENTModifierDetail.IsQty = Convert.ToBoolean(dt.Rows[n]["IsQty"]);
                                objENTModifierDetail.UnitTypeID = new Guid(dt.Rows[n]["UnitTypeID"].ToString());
                                objENTModifierDetail.UnitType = Convert.ToString(dt.Rows[n]["UnitType"]);
                                objENTModifierDetail.Sort = Convert.ToInt32(dt.Rows[n]["Sort"]);
                                objENTModifierDetail.ModifierCategoryDetail_Id = Convert.ToInt32(dt.Rows[n]["ModifierCategoryDetail_Id"]);
                                objENTModifierDetail.IsUPStream = 1;

                                if (objDALModifierDetail.InsertUpdateDeleteModifierDetail(objENTModifierDetail))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string Tables_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_TABLE);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    strTable = dt.TableName.ToString();
                    switch (dt.TableName.ToString())
                    {
                        case "TableMasterDetail":
                            #region TableMasterDetail
                            ENT.TableMasterDetail objENTTable = new ENT.TableMasterDetail();
                            TableMasterDetail objDALTable = new TableMasterDetail();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALTable.getDuplicateTableMasterDetailByID(dt.Rows[n]["TableID"].ToString()) > 0)
                                { objENTTable.Mode = "UPDATE"; }
                                else
                                { objENTTable.Mode = "ADD"; }
                                objENTTable.TableID = new Guid(dt.Rows[n]["TableID"].ToString());
                                objENTTable.EmployeeID = new Guid(dt.Rows[n]["EmployeeID"].ToString());
                                objENTTable.TableName = Convert.ToString(dt.Rows[n]["TableName"]);
                                objENTTable.NoOfSeats = Convert.ToInt32(dt.Rows[n]["NoOfSeats"]);
                                objENTTable.Location = Convert.ToString(dt.Rows[n]["Location"]);
                                objENTTable.ClassID = new Guid(dt.Rows[n]["ClassID"].ToString());
                                objENTTable.Sort = Convert.ToInt32(dt.Rows[n]["Sort"]);
                                objENTTable.StatusID = Convert.ToInt32(ENT.APIStream.TableStatus.Vacant);
                                objENTTable.IsUPStream = 1;

                                if (objDALTable.InsertUpdateDeleteTableMasterDetail(objENTTable))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string Customer_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_CUSTOMER);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    strTable = dt.TableName.ToString();
                    switch (dt.TableName.ToString())
                    {
                        case "CustomerMasterData":
                            #region CustomerMasterData
                            ENT.CustomerMasterData objENTCustomer = new ENT.CustomerMasterData();
                            CustomerMasterData objDALCustomer = new CustomerMasterData();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALCustomer.getDuplicateCustomerByID(dt.Rows[n]["CustomerID"].ToString()) > 0)
                                { objENTCustomer.Mode = "UPDATE"; }
                                else
                                { objENTCustomer.Mode = "ADD"; }
                                objENTCustomer.CustomerID = new Guid(dt.Rows[n]["CustomerID"].ToString());
                                objENTCustomer.Name = Convert.ToString(dt.Rows[n]["Name"]);
                                objENTCustomer.MobileNo = Convert.ToString(dt.Rows[n]["MobileNo"]);
                                objENTCustomer.EmailID = Convert.ToString(dt.Rows[n]["EmailID"]);
                                objENTCustomer.Birthdate = dt.Rows[n]["Birthdate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["Birthdate"].ToString());
                                objENTCustomer.Address = Convert.ToString(dt.Rows[n]["Address"]);
                                objENTCustomer.RUserID = new Guid(dt.Rows[n]["RUserID"].ToString());
                                if (BranchSettingDetail.IsFranchise(BranchID))
                                { objENTCustomer.RUserType = Convert.ToInt32(ENT.APIStream.R_USER_TYPE.FRANCHISE); }
                                else { objENTCustomer.RUserType = Convert.ToInt32(ENT.APIStream.R_USER_TYPE.BRANCH); }
                                objENTCustomer.IsUPStream = 1;

                                if (objDALCustomer.InsertUpdateDeleteCustomer(objENTCustomer))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string Order_DS()
        {
            string lines = "";
            int line_no = 0;
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_ORDER);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    strTable = dt.TableName.ToString();
                    switch (dt.TableName.ToString())
                    {
                        case "OrderMaster":
                            #region CustomerMasterData
                            line_no = 0;
                            ENT.OrderBook objENTOrder = new ENT.OrderBook();
                            OrderBook objDALOrder = new OrderBook();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALOrder.getDuplicateOrderByID(dt.Rows[n]["OrderID"].ToString()) > 0)
                                { objENTOrder.Mode = "UPDATE"; }
                                else
                                { objENTOrder.Mode = "ADD"; }
                                objENTOrder.OrderID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["OrderID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["OrderID"]));
                                objENTOrder.CustomerID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["CustomerID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["CustomerID"]));
                                DateTime date = Convert.ToDateTime(Convert.ToString(dt.Rows[n]["OrderDate"]));
                                objENTOrder.OrderDate = ENT.APIStream.ChangeDateTime(date.ToString("dd/MM/yyyy hh:mm tt"));
                                objENTOrder.EmployeeID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["EmployeeID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["EmployeeID"]));
                                objENTOrder.OrderStatus = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["OrderStatus"])) ? 0 : Convert.ToInt32(dt.Rows[n]["OrderStatus"]);
                                objENTOrder.TableID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["TableID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["TableID"]));
                                objENTOrder.OrderNo = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["InvoiceNo"])) ? "" : Convert.ToString(dt.Rows[n]["InvoiceNo"]);
                                objENTOrder.DiscountID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["DiscountID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["DiscountID"]));
                                objENTOrder.Discount = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["DiscountAmount"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["DiscountAmount"]);
                                objENTOrder.OrderSpecialRequest = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["SpecialRequest"])) ? "" : Convert.ToString(dt.Rows[n]["SpecialRequest"]);
                                objENTOrder.TipGratuity = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["TipAmount"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["TipAmount"]);
                                objENTOrder.DeliveryCharge = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["DeliveryCharge"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["DeliveryCharge"]);
                                objENTOrder.SubTotal = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["SubTotal"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["SubTotal"]);
                                objENTOrder.PayableAmount = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["FinalTotal"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["FinalTotal"]);
                                objENTOrder.DeliveryType = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["OrderTypeID"])) ? 0 : Convert.ToInt32(dt.Rows[n]["OrderTypeID"]);
                                objENTOrder.ExtraCharge = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["ExtraCharge"])) ? 0 : Convert.ToInt32(dt.Rows[n]["ExtraCharge"]);
                                objENTOrder.TaxLabel1 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["TaxLable1"])) ? "" : Convert.ToString(dt.Rows[n]["TaxLable1"]);
                                objENTOrder.TaxPercent1 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["TaxPercentage1"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["TaxPercentage1"]);
                                objENTOrder.SGSTAmount = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["TaxAmount1"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["TaxAmount1"]);
                                objENTOrder.TaxLabel2 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["TaxLable2"])) ? "" : Convert.ToString(dt.Rows[n]["TaxLable2"]);
                                objENTOrder.TaxPercent2 = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["TaxPercentage2"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["TaxPercentage2"]);
                                objENTOrder.CGSTAmount = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["TaxAmount2"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["TaxAmount2"]);
                                objENTOrder.TotalTax = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["OrderTaxTotal"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["OrderTaxTotal"]);
                                objENTOrder.RUserID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["RUserID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(dt.Rows[n]["RUserID"].ToString());
                                objENTOrder.RUserType = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["RUserType"])) ? 0 : Convert.ToInt32(dt.Rows[n]["RUserType"]);
                                if (objDALOrder.InsertUpdateDeleteOrder(objENTOrder))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                                line_no = n;
                            }
                            #endregion
                            break;
                        case "OrderTransaction":
                            #region CustomerMasterData
                            line_no = 0;
                            ENT.Transaction objENTTran = new ENT.Transaction();
                            Transaction objDALTran = new Transaction();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALTran.getDuplicateTransactionByID(dt.Rows[n]["OrderTransactionID"].ToString()) > 0)
                                { objENTTran.Mode = "UPDATE"; }
                                else
                                { objENTTran.Mode = "ADD"; }
                                objENTTran.TransactionID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["OrderTransactionID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["OrderTransactionID"]));
                                objENTTran.OrderID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["OrderID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["OrderID"]));
                                objENTTran.CategoryID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["CategoryID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["CategoryID"]));
                                objENTTran.ProductID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["ProductID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["ProductID"]));
                                objENTTran.EmployeeID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["EmployeeID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["EmployeeID"]));
                                objENTTran.Quantity = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["Quantity"])) ? 0 : Convert.ToInt32(dt.Rows[n]["Quantity"].ToString().Substring(0, dt.Rows[n]["Quantity"].ToString().Length - 3));
                                objENTTran.Rate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["ItemPrice"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["ItemPrice"]);
                                objENTTran.SpecialRequest = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["SpecialRemarks"])) ? "" : Convert.ToString(dt.Rows[n]["SpecialRemarks"]);
                                DateTime date = Convert.ToDateTime(Convert.ToString(dt.Rows[n]["StartDate"]));
                                objENTTran.StartDate = ENT.APIStream.ChangeDateTime(date.ToString("dd/MM/yyyy hh:mm tt"));
                                date = Convert.ToDateTime(Convert.ToString(dt.Rows[n]["EndDate"]));
                                objENTTran.EndDate = ENT.APIStream.ChangeDateTime(date.ToString("dd/MM/yyyy hh:mm tt"));
                                objENTTran.TotalAmount = Convert.ToDecimal(objENTTran.Quantity) * Convert.ToDecimal(objENTTran.Rate);
                                objENTTran.RUserID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["RUserID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["RUserID"]));
                                objENTTran.RUserType = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["RUserType"])) ? 0 : Convert.ToInt32(dt.Rows[n]["RUserType"]);
                                if (objDALTran.InsertUpdateDeleteOrderTransaction(objENTTran))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                                line_no = n;
                            }
                            #endregion
                            break;
                        case "OrderIngredients":
                            #region OrderIngredients
                            line_no = 0;
                            ENT.OrderWiseModifier objENTOWM = new ENT.OrderWiseModifier();
                            OrderWiseModifier objDALOWM = new OrderWiseModifier();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALOWM.getDuplicateOrderWiseModifierByID(Convert.ToString(dt.Rows[n]["TransactionID"]), Convert.ToString(dt.Rows[n]["IngredientsID"])) > 0)
                                { objENTOWM.Mode = "UPDATE"; }
                                else
                                { objENTOWM.Mode = "ADD"; }
                                objENTOWM.ModifierID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["OrderIngredientsID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["OrderIngredientsID"]));
                                objENTOWM.TransactionID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["TransactionID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["TransactionID"]));
                                objENTOWM.IngredientsID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["IngredientsID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["IngredientsID"]));
                                objENTOWM.Quantity = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["Quantity"])) ? 0 : Convert.ToInt32(dt.Rows[n]["Quantity"]);
                                objENTOWM.Price = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["Rate"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["Rate"]);
                                objENTOWM.Total = Convert.ToDecimal(objENTOWM.Quantity) * Convert.ToDecimal(objENTOWM.Price);
                                objENTOWM.ModifierOption = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["ModifierID"])) ? "" : Convert.ToString(dt.Rows[n]["ModifierID"]);
                                objENTOWM.RUserID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["RUserID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["RUserID"]));
                                objENTOWM.RUserType = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["RUserType"])) ? 0 : Convert.ToInt32(dt.Rows[n]["RUserType"]);
                                if (objDALOWM.InsertUpdateDeleteOrderWiseModifier(objENTOWM))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                                line_no = n;
                            }
                            #endregion
                            break;
                        case "OrderPayment":
                            #region OrderIngredients
                            line_no = 0;
                            ENT.CheckOutDetail objENTCO = new ENT.CheckOutDetail();
                            CheckOutDetail objDALCO = new CheckOutDetail();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALCO.getDuplicateCheckOutDetailByOrderID(Convert.ToString(dt.Rows[n]["OrderID"])) > 0)
                                { objENTCO.Mode = "UPDATE"; }
                                else
                                { objENTCO.Mode = "ADD"; }
                                objENTCO.TransactionID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["OrderPaymentID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["OrderPaymentID"]));
                                objENTCO.OrderID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["OrderID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["OrderID"]));
                                objENTCO.PaymentTransID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["PaymentTransID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["PaymentTransID"]));
                                objENTCO.PaymentID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["PaymentID"])) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(Convert.ToString(dt.Rows[n]["PaymentID"]));
                                objENTCO.PaymentToken = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["PaymentToken"])) ? "" : Convert.ToString(dt.Rows[n]["PaymentToken"]);
                                objENTCO.IsSwipe = Convert.ToBoolean(dt.Rows[n]["IsSwipe"]);
                                objENTCO.PaymentCardTypeID = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["PaymentCardTypeID"])) ? "" : Convert.ToString(dt.Rows[n]["PaymentCardTypeID"]);
                                objENTCO.CardNumber = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["CardNo"])) ? "" : Convert.ToString(dt.Rows[n]["CardNo"]);
                                objENTCO.PaidAmount = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["Amount"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["Amount"]);
                                objENTCO.ExpireDate = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["ExpireDate"])) ? "" : Convert.ToString(dt.Rows[n]["ExpireDate"]);
                                objENTCO.CVV = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["CVV"])) ? "" : Convert.ToString(dt.Rows[n]["CVV"]);
                                objENTCO.SwipeStr = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["SwipeStr"])) ? "" : Convert.ToString(dt.Rows[n]["SwipeStr"]);
                                objENTCO.CardHolderName = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["CardHolderName"])) ? "" : Convert.ToString(dt.Rows[n]["CardHolderName"]);
                                objENTCO.TransactionResult = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["TransactionResult"])) ? "" : Convert.ToString(dt.Rows[n]["TransactionResult"]);
                                objENTCO.CustomerTransactionId = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["CustomerTransactionId"])) ? "" : Convert.ToString(dt.Rows[n]["CustomerTransactionId"]);
                                objENTCO.TransactionAction = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["TransactionAction"])) ? "" : Convert.ToString(dt.Rows[n]["TransactionAction"]);
                                objENTCO.TranstationMessage = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["TranstationMessage"])) ? "" : Convert.ToString(dt.Rows[n]["TranstationMessage"]);
                                objENTCO.IsPartialAuthorized = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["IsPartialAuthorized"])) ? "" : Convert.ToString(dt.Rows[n]["IsPartialAuthorized"]);
                                objENTCO.RequestedAmount = string.IsNullOrEmpty(Convert.ToString(dt.Rows[n]["RequestedAmount"])) ? 0 : Convert.ToDecimal(dt.Rows[n]["RequestedAmount"]);
                                objENTCO.RUserID = string.IsNullOrEmpty(BranchID) ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(BranchID);
                                objENTCO.RUserType = 2;
                                if (objDALCO.InsertUpdateDeleteCheckOutDetail(objENTCO))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                                line_no = n;
                            }
                            #endregion
                            break;
                        case "OrderCombo":
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] => LineNo[" + line_no + "] => " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string Tax_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_TAX);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    switch (dt.TableName.ToString())
                    {
                        case "TaxGroupDetail":
                            #region TaxGroupDetail
                            ENT.TaxGroupDetail objENTTaxGroup = new ENT.TaxGroupDetail();
                            TaxGroupDetail objDALTaxGroup = new TaxGroupDetail();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALTaxGroup.getDuplicateTaxGroupByTaxGroupID(dt.Rows[n]["TaxGroupID"].ToString()) > 0)
                                { objENTTaxGroup.Mode = "UPDATE"; }
                                else
                                { objENTTaxGroup.Mode = "ADD"; }
                                objENTTaxGroup.TaxGroupID = new Guid(dt.Rows[n]["TaxGroupID"].ToString());
                                objENTTaxGroup.Name = Convert.ToString(dt.Rows[n]["Name"]);
                                objENTTaxGroup.Percentage = Convert.ToDecimal(dt.Rows[n]["Percentage"]);
                                objENTTaxGroup.ParentID = new Guid(dt.Rows[n]["ParentID"].ToString());
                                objENTTaxGroup.PartnerID = new Guid(dt.Rows[n]["PartnerID"].ToString());
                                objENTTaxGroup.Action = Convert.ToString(dt.Rows[n]["Action"]);
                                objENTTaxGroup.Sign = Convert.ToString(dt.Rows[n]["Sign"]);
                                objENTTaxGroup.Sort = Convert.ToInt32(dt.Rows[n]["Sort"]);
                                objENTTaxGroup.TaxOnProductType = Convert.ToInt32(dt.Rows[n]["TaxOnProductType"]);
                                objENTTaxGroup.IsUPStream = 1;

                                if (objDALTaxGroup.InsertUpdateDeleteTaxGroupDetail(objENTTaxGroup))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string Discount_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_DISCOUNT);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    strTable = dt.TableName.ToString();
                    switch (dt.TableName.ToString())
                    {
                        case "DiscountMasterDetail":
                            #region DiscountMasterDetail
                            ENT.DiscountMasterDetail objENTDiscount = new ENT.DiscountMasterDetail();
                            DiscountMasterDetail objDALDiscount = new DiscountMasterDetail();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALDiscount.getCoutDiscountMasterByDiscountID(dt.Rows[n]["DiscountID"].ToString()) > 0)
                                { objENTDiscount.Mode = "UPDATE"; }
                                else
                                { objENTDiscount.Mode = "ADD"; }
                                objENTDiscount.DiscountID = new Guid(dt.Rows[n]["DiscountID"].ToString());
                                objENTDiscount.DiscountName = Convert.ToString(dt.Rows[n]["DiscountName"]);
                                objENTDiscount.DiscountType = Convert.ToInt32(dt.Rows[n]["DiscountType"]);
                                objENTDiscount.AmountOrPercentage = Convert.ToDecimal(dt.Rows[n]["AmountOrPercentage"]);
                                objENTDiscount.QualificationType = Convert.ToInt32(dt.Rows[n]["QualificationType"]);
                                objENTDiscount.IsTaxed = Convert.ToBoolean(dt.Rows[n]["IsTaxed"]);
                                objENTDiscount.Barcode = Convert.ToString(dt.Rows[n]["Barcode"]);
                                objENTDiscount.DiscountCode = Convert.ToString(dt.Rows[n]["DiscountCode"]);
                                objENTDiscount.PasswordRequired = Convert.ToBoolean(dt.Rows[n]["PasswordRequired"]);
                                objENTDiscount.DisplayOnPOS = Convert.ToBoolean(dt.Rows[n]["DisplayOnPOS"]);
                                objENTDiscount.AutoApply = Convert.ToBoolean(dt.Rows[n]["AutoApply"]);
                                objENTDiscount.DisplayToCustomer = Convert.ToBoolean(dt.Rows[n]["DisplayToCustomer"]);
                                objENTDiscount.IsTimeBase = Convert.ToBoolean(dt.Rows[n]["IsTimeBase"]);
                                objENTDiscount.IsLoyaltyRewards = Convert.ToBoolean(dt.Rows[n]["IsLoyaltyRewards"]);
                                objENTDiscount.DiscountMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["DiscountMasterDetail_Id"]);
                                objENTDiscount.IsUPStream = 1;

                                if (objDALDiscount.InsertUpdateDeleteDiscountMasterDetail(objENTDiscount))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "TimeSheetWiseDiscount":
                            #region TimeSheetWiseDiscount
                            ENT.TimeSheetWiseDiscount objENTTSWD = new ENT.TimeSheetWiseDiscount();
                            TimeSheetWiseDiscount objDALTSWD = new TimeSheetWiseDiscount();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (n == 0)
                                {
                                    objDALTSWD.deleteTimeSheetWiseDiscount();
                                }
                                objENTTSWD.Mode = "ADD";
                                objENTTSWD.FromTime = dt.Rows[n]["FromTime"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["FromTime"]);
                                objENTTSWD.ToTime = dt.Rows[n]["ToTime"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["ToTime"]);
                                objENTTSWD.StartDate = dt.Rows[n]["StartDate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["StartDate"].ToString());
                                objENTTSWD.EndDate = dt.Rows[n]["EndDate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[n]["EndDate"].ToString());
                                objENTTSWD.Day = Convert.ToInt32(dt.Rows[n]["Day"]);
                                objENTTSWD.DiscountMasterDetail_Id = Convert.ToInt32(dt.Rows[n]["DiscountMasterDetail_Id"]);
                                objENTTSWD.IsUPStream = 1;

                                if (objDALTSWD.InsertUpdateDeleteTimeSheetWiseDiscount(objENTTSWD))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string Vendor_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_VENDOR);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    strTable = dt.TableName.ToString();
                    switch (dt.TableName.ToString())
                    {
                        case "VendorMasterData":
                            #region VendorMasterData
                            ENT.VendorMasterData objENTVendor = new ENT.VendorMasterData();
                            VendorMasterData objDALVendor = new VendorMasterData();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALVendor.getDuplicateVendorByID(dt.Rows[n]["VendorID"].ToString()) > 0)
                                { objENTVendor.Mode = "UPDATE"; }
                                else
                                { objENTVendor.Mode = "ADD"; }
                                objENTVendor.VendorID = new Guid(dt.Rows[n]["VendorID"].ToString());
                                objENTVendor.VendorName = Convert.ToString(dt.Rows[n]["VendorName"]);
                                objENTVendor.VendorAddress = Convert.ToString(dt.Rows[n]["VendorAddress"]);
                                objENTVendor.MobileNo = Convert.ToString(dt.Rows[n]["MobileNo"]);
                                objENTVendor.EmailID = Convert.ToString(dt.Rows[n]["EmailID"]);
                                objENTVendor.CompanyName = Convert.ToString(dt.Rows[n]["CompanyName"]);
                                objENTVendor.RUserID = new Guid(BranchID);
                                if (BranchSettingDetail.IsFranchise(BranchID))
                                { objENTVendor.RUserType = Convert.ToInt32(ENT.APIStream.R_USER_TYPE.FRANCHISE); }
                                else { objENTVendor.RUserType = Convert.ToInt32(ENT.APIStream.R_USER_TYPE.BRANCH); }
                                objENTVendor.IsUPStream = 1;

                                if (objDALVendor.InsertUpdateDeleteVendorMaster(objENTVendor))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string Recipe_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_RECIPE);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    switch (dt.TableName.ToString())
                    {
                        case "RecipeMasterData":
                            #region RecipeMasterData
                            ENT.RecipeMasterData objENTRecipe = new ENT.RecipeMasterData();
                            RecipeMasterData objDALRecipe = new RecipeMasterData();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALRecipe.getDuplicateRecipeMasterDataByID(dt.Rows[n]["RecipeID"].ToString()) > 0)
                                { objENTRecipe.Mode = "UPDATE"; }
                                else
                                { objENTRecipe.Mode = "ADD"; }
                                objENTRecipe.ProductID = new Guid(dt.Rows[n]["ProductID"].ToString());
                                objENTRecipe.ProductName = Convert.ToString(dt.Rows[n]["ProductName"]);
                                objENTRecipe.RecipeID = new Guid(dt.Rows[n]["RecipeID"].ToString());
                                objENTRecipe.RecipeText = Convert.ToString(dt.Rows[n]["RecipeText"]);
                                objENTRecipe.RecipeMasterData_Id = Convert.ToInt32(dt.Rows[n]["RecipeMasterData_Id"]);

                                if (objDALRecipe.InsertUpdateDeleteRecipeMasterData(objENTRecipe))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "RecipeMasterDetail":
                            #region RecipeMasterDetail
                            ENT.RecipeMasterDetail objENTRecipeDetail = new ENT.RecipeMasterDetail();
                            RecipeMasterDetail objDALRecipeDetail = new RecipeMasterDetail();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALRecipeDetail.getDuplicateRecipeMasterDetailByID(dt.Rows[n]["IngredientsID"].ToString()) > 0)
                                { objENTRecipeDetail.Mode = "UPDATE"; }
                                else
                                { objENTRecipeDetail.Mode = "ADD"; }
                                objENTRecipeDetail.IngredientsID = new Guid(dt.Rows[n]["IngredientsID"].ToString());
                                objENTRecipeDetail.Name = Convert.ToString(dt.Rows[n]["Name"]);
                                objENTRecipeDetail.Qty = Convert.ToInt32(dt.Rows[n]["Qty"]);
                                objENTRecipeDetail.Price = Convert.ToDecimal(dt.Rows[n]["Price"]);
                                objENTRecipeDetail.IsDefault = Convert.ToBoolean(dt.Rows[n]["IsDefault"]);
                                objENTRecipeDetail.IsQty = Convert.ToBoolean(dt.Rows[n]["IsQty"]);
                                objENTRecipeDetail.UnitTypeID = new Guid(dt.Rows[n]["UnitTypeID"].ToString());
                                objENTRecipeDetail.UnitType = Convert.ToString(dt.Rows[n]["UnitType"]);
                                objENTRecipeDetail.RecipeMasterData_Id = 0;
                                objENTRecipeDetail.IsUPStream = 1;

                                if (objDALRecipeDetail.InsertUpdateDeleteRecipeMasterDetail(objENTRecipeDetail))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string Class_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_CLASS);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    strTable = dt.TableName.ToString();
                    switch (dt.TableName.ToString())
                    {
                        case "ProductClassMasterDetail":
                            #region ProductClassMasterDetail
                            ENT.ProductClassMasterDetail objENTProductClass = new ENT.ProductClassMasterDetail();
                            ProductClassMasterDetail objDALProductClass = new ProductClassMasterDetail();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALProductClass.getDuplicateProductClassMasterDetailByID(dt.Rows[n]["ClassID"].ToString()) > 0)
                                { objENTProductClass.Mode = "UPDATE"; }
                                else
                                { objENTProductClass.Mode = "ADD"; }
                                objENTProductClass.ClassID = new Guid(dt.Rows[n]["ClassID"].ToString());
                                objENTProductClass.ClassName = Convert.ToString(dt.Rows[n]["ClassName"]);
                                objENTProductClass.IsUPStream = 1;

                                if (objDALProductClass.InsertUpdateDeleteProductClassMasterDetail(objENTProductClass))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }

        public string ShiftWiseEmployee_DS()
        {
            string lines = "";
            string strTable = "";
            try
            {
                DataSet ds = SyncByOption(ENT.APIStream.METHOD_CLASS);
                lines = lines + "Last Sync Date And Time : " + DateTime.Now.ToString() + ".\r\n";
                lines = lines + "========================LOG=========================\r\n\n";
                foreach (DataTable dt in ds.Tables)
                {
                    strTable = dt.TableName.ToString();
                    switch (dt.TableName.ToString())
                    {
                        case "ShiftWiseEmployee":
                            #region ShiftWiseEmployee
                            ENT.ShiftWiseEmployee objENTSWE = new ENT.ShiftWiseEmployee();
                            ShiftWiseEmployee objDALSWE = new ShiftWiseEmployee();
                            for (int n = 0; n < dt.Rows.Count; n++)
                            {
                                if (objDALSWE.getDuplicateByShiftWiseEmployeeID(dt.Rows[n]["ShiftWiseEmployeeID"].ToString()) > 0)
                                { objENTSWE.Mode = "UPDATE"; }
                                else
                                { objENTSWE.Mode = "ADD"; }
                                objENTSWE.ShiftWiseEmployeeID = new Guid(dt.Rows[n]["ShiftWiseEmployeeID"].ToString());
                                objENTSWE.EmployeeID = new Guid(dt.Rows[n]["EmployeeID"].ToString());
                                objENTSWE.ShiftID = new Guid(dt.Rows[n]["ShiftID"].ToString());
                                //objENTSWE.RUserID = new Guid(dt.Rows[n]["RUserID"].ToString());
                                //objENTSWE.RUserType = Convert.ToInt32(dt.Rows[n]["RUserType"]);
                                //objENTSWE.CreatedDate = dt.Rows[n]["CreatedDate"] == null ? DateTime.Now : Convert.ToDateTime(dt.Rows[n]["CreatedDate"]);
                                //objENTSWE.IsStatus = Convert.ToBoolean(dt.Rows[n]["IsStatus"]);
                                objENTSWE.ShiftDay = Convert.ToInt32(dt.Rows[n]["ShiftDay"]);
                                objENTSWE.IsUPStream = 1;
                                if (objDALSWE.InsertUpdateDeleteShiftWiseEmployee(objENTSWE))
                                {
                                    lines = lines + "Table => " + dt.TableName.ToString() + " Data Inserted.\r\n";
                                }
                                else
                                {
                                    lines = lines + "Problem => " + dt.TableName.ToString() + " Data Insert.\r\n";
                                }
                            }
                            #endregion
                            break;
                        case "VersionDetail":
                            break;
                        case "RootObject":
                            break;
                        default:
                            lines = lines + "Table => " + dt.TableName.ToString() + " Not Found In Database. Please Create Table.\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lines = lines + "Table[" + strTable + "] =>  " + ex.Message.ToString() + " \r\n";
            }
            return lines;
        }
    }
}
