using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class CategoryWiseProduct
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();
        
        public bool InsertUpdateDeleteProduct(ENT.CategoryWiseProduct objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteProduct";
                sqlCMD.Parameters.AddWithValue("@DiscountID", objENT.DiscountID);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@CategoryID", objENT.CategoryID);
                sqlCMD.Parameters.AddWithValue("@ProductName", objENT.ProductName);
                sqlCMD.Parameters.AddWithValue("@Price", objENT.Price);
                sqlCMD.Parameters.AddWithValue("@ImgPath", objENT.ImgPath);
                sqlCMD.Parameters.AddWithValue("@IsUrl", objENT.IsUrl);
                sqlCMD.Parameters.AddWithValue("@Calorie", objENT.Calorie);
                sqlCMD.Parameters.AddWithValue("@ShortDescription", objENT.ShortDescription);
                sqlCMD.Parameters.AddWithValue("@IsNonVeg", objENT.IsNonVeg);
                sqlCMD.Parameters.AddWithValue("@IsTrendingItem", objENT.IsTrendingItem);
                sqlCMD.Parameters.AddWithValue("@ApproxCookingTime", objENT.ApproxCookingTime.ToString());
                sqlCMD.Parameters.AddWithValue("@IsAellergic", objENT.IsAellergic);
                sqlCMD.Parameters.AddWithValue("@Extras", objENT.Extras);
                sqlCMD.Parameters.AddWithValue("@IsVisibleToB2C", objENT.IsVisibleToB2C);
                sqlCMD.Parameters.AddWithValue("@ExpiryDateFrom", objENT.ExpiryDateFrom.ToString());
                sqlCMD.Parameters.AddWithValue("@ExpiryDateTo", objENT.ExpiryDateTo.ToString());
                sqlCMD.Parameters.AddWithValue("@StationID", objENT.StationID);
                sqlCMD.Parameters.AddWithValue("@SuggestiveItems", objENT.SuggestiveItems);
                sqlCMD.Parameters.AddWithValue("@IsCold", objENT.IsCold);
                sqlCMD.Parameters.AddWithValue("@IsDrink", objENT.IsDrink);
                sqlCMD.Parameters.AddWithValue("@DiningOptions", objENT.DiningOptions);
                sqlCMD.Parameters.AddWithValue("@AllowPriceOverride", objENT.AllowPriceOverride);
                sqlCMD.Parameters.AddWithValue("@IsAgeValidation", objENT.IsAgeValidation);
                sqlCMD.Parameters.AddWithValue("@AgeForValidation", objENT.AgeForValidation);
                sqlCMD.Parameters.AddWithValue("@OverridePrice", objENT.OverridePrice);
                sqlCMD.Parameters.AddWithValue("@IsCombo", objENT.IsCombo);
                sqlCMD.Parameters.AddWithValue("@IsDisplayModifire", objENT.IsDisplayModifire);
                sqlCMD.Parameters.AddWithValue("@ProductCode", objENT.ProductCode);
                sqlCMD.Parameters.AddWithValue("@TaxPercentage", objENT.TaxPercentage);
                sqlCMD.Parameters.AddWithValue("@Sort", objENT.Sort);
                sqlCMD.Parameters.AddWithValue("@Priority", objENT.Priority);
                sqlCMD.Parameters.AddWithValue("@RUserID", objENT.RUserID);
                sqlCMD.Parameters.AddWithValue("@RUserType", objENT.RUserType);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }

        public List<ENT.CategoryWiseProduct> getCategoryWiseProduct(ENT.CategoryWiseProduct objENT)
        {
            List<ENT.CategoryWiseProduct> lstENT = new List<ENT.CategoryWiseProduct>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetCategoryWiseProduct";
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@CategoryID", objENT.CategoryID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                //DataTable dt = objCRUD.getDataTable(sqlCMD);
                lstENT = DBHelper.GetEntityList<ENT.CategoryWiseProduct>(sqlCMD);

                //lstENT = (from DataRow dr in dt.Rows
                //          select new ENT.CategoryWiseProduct()
                //          {
                //              DiscountID = new Guid(dr["DiscountID"].ToString()),
                //              ProductID = new Guid(dr["ProductID"].ToString()),
                //              CategoryID = new Guid(dr["CategoryID"].ToString()),
                //              ProductName = Convert.ToString(dr["ProductName"]),
                //              Price = Convert.ToDecimal(dr["Price"]),
                //              ImgPath = Convert.ToString(dr["ImgPath"]),
                //              IsUrl = Convert.ToBoolean(dr["IsUrl"]),
                //              Calorie = Convert.ToString(dr["Calorie"]),
                //              ShortDescription = Convert.ToString(dr["ShortDescription"]),
                //              IsNonVeg = Convert.ToBoolean(dr["IsNonVeg"]),
                //              IsTrendingItem = Convert.ToBoolean(dr["IsTrendingItem"]),
                //              ApproxCookingTime = Convert.ToString(dr["ApproxCookingTime"]),
                //              IsAellergic = Convert.ToBoolean(dr["IsAellergic"]),
                //              Extras = Convert.ToString(dr["Extras"]),
                //              IsVisibleToB2C = Convert.ToBoolean(dr["IsVisibleToB2C"]),
                //              ExpiryDateFrom = Convert.ToString(dr["ExpiryDateFrom"]),
                //              ExpiryDateTo = Convert.ToString(dr["ExpiryDateTo"]),
                //              StationID = new Guid(dr["StationID"].ToString()),
                //              SuggestiveItems = Convert.ToString(dr["SuggestiveItems"]),
                //              IsCold = Convert.ToBoolean(dr["IsCold"]),
                //              IsDrink = Convert.ToBoolean(dr["IsDrink"]),
                //              DiningOptions = Convert.ToInt32(dr["DiningOptions"]),
                //              AllowPriceOverride = Convert.ToInt32(dr["AllowPriceOverride"]),
                //              IsAgeValidation = Convert.ToBoolean(dr["IsAgeValidation"]),
                //              AgeForValidation = Convert.ToInt32(dr["AgeForValidation"]),
                //              OverridePrice = Convert.ToDecimal(dr["OverridePrice"]),
                //              IsCombo = Convert.ToBoolean(dr["IsCombo"]),
                //              IsDisplayModifire = Convert.ToBoolean(dr["IsDisplayModifire"]),
                //              ProductCode = Convert.ToString(dr["ProductCode"]),
                //              TaxPercentage = Convert.ToDecimal(dr["TaxPercentage"]),
                //              Sort = Convert.ToInt32(dr["Sort"]),
                //              Priority = Convert.ToInt32(dr["Priority"]),
                //              CategoryWiseProduct_Id = Convert.ToInt32(dr["CategoryWiseProduct_Id"]),
                //          }).ToList();

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.CategoryWiseProduct>(sdr);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public List<ENT.ProductUpStream> getProductForUpStream(ENT.ProductUpStream objENT)
        {
            List<ENT.ProductUpStream> lstENT = new List<ENT.ProductUpStream>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetCategoryWiseProduct";
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@CategoryID", objENT.CategoryID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.ProductUpStream>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateProductByID(string ProductID, string CategoryID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [CategoryWiseProduct] WHERE ProductID = '" + ProductID + "' AND CategoryID = '" + CategoryID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }
        
        public int DeleteProductForDownStream()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "DELETE FROM [CategoryWiseProduct]";
                duplicateCount = objCRUD.ExecuteQuery(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateProductByName(string ProductName)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [CategoryWiseProduct] WHERE ProductName = '" + ProductName + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int IsUpStreamTrue()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "UPDATE [CategoryWiseProduct] SET IsUPStream = 1 WHERE IsUPStream = 0";
                duplicateCount = objCRUD.ExecuteQuery(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }
    }
}
