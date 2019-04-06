using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class CategoryMaster
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();
    
        public bool InsertUpdateDeleteCategoryMaster(ENT.CategoryMaster objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteCategoryMaster";
                sqlCMD.Parameters.AddWithValue("@CategoryID", objENT.CategoryID);
                sqlCMD.Parameters.AddWithValue("@CategoryName", objENT.CategoryName);
                sqlCMD.Parameters.AddWithValue("@ImgPath", objENT.ImgPath);
                sqlCMD.Parameters.AddWithValue("@ParentID", objENT.ParentID);
                sqlCMD.Parameters.AddWithValue("@ClassMasterID", objENT.ClassMasterID);
                sqlCMD.Parameters.AddWithValue("@Priority", objENT.Priority);
                sqlCMD.Parameters.AddWithValue("@IsCategory", objENT.IsCategory);
                sqlCMD.Parameters.AddWithValue("@RUserID", objENT.RUserID);
                sqlCMD.Parameters.AddWithValue("@RUserType", objENT.RUserType);
                sqlCMD.Parameters.AddWithValue("@IsUPStream", objENT.IsUPStream);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCMD.Connection.Close();
            }
            return row;
        }

        public List<ENT.CategoryMaster> getCategoryMaster(ENT.CategoryMaster objENT)
        {
            List<ENT.CategoryMaster> lstENT = new List<ENT.CategoryMaster>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetCategoryMaster";
                sqlCMD.Parameters.AddWithValue("@CategoryID", objENT.CategoryID);
                sqlCMD.Parameters.AddWithValue("@ParentID", objENT.ParentID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                //DataTable dt = objCRUD.getDataTable(sqlCMD);
                lstENT = DBHelper.GetEntityList<ENT.CategoryMaster>(sqlCMD);

                //lstENT = (from DataRow dr in dt.Rows
                //          select new ENT.CategoryMaster()
                //          {
                //              CategoryID = new Guid(dr["CategoryID"].ToString()),
                //              CategoryName = Convert.ToString(dr["CategoryName"]),
                //              ImgPath = Convert.ToString(dr["ImgPath"]),
                //              ParentID = new Guid(dr["ParentID"].ToString()),
                //              ClassMasterID = new Guid(dr["ClassMasterID"].ToString()),
                //              Priority = Convert.ToInt32(dr["Priority"]),
                //              IsCategory = Convert.ToInt32(dr["IsCategory"])
                //          }).ToList();

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.CategoryMaster>(sdr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                sqlCMD.Connection.Close();
            }
            return lstENT;
        }

        public List<ENT.CategoryMaster> getDisplayCategoryButton(ENT.CategoryMaster objENT)
        {
            List<ENT.CategoryMaster> lstENT = new List<ENT.CategoryMaster>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetCategoryMaster";
                sqlCMD.Parameters.AddWithValue("@CategoryID", objENT.CategoryID);
                sqlCMD.Parameters.AddWithValue("@ParentID", objENT.ParentID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.CategoryMaster>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCMD.Connection.Close();
            }
            return lstENT;
        }

        public int getDuplicateCategoryByID(string CategoryID)
        {
            int duplicateCount=0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [CategoryMaster] WHERE IsCategory=1 AND CategoryID = '" + CategoryID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public DataTable getMainCategory()
        {
            DataTable dt = new DataTable();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [CategoryMaster] WHERE IsCategory=1";
                dt = objCRUD.getDataTableByQuery(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable getCategoryByParentID(string ParentID)
        {
            DataTable dt = new DataTable();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT a.CategoryID, a.CategoryName, Deriv1.Count FROM ViewCategoryWiseProduct a LEFT OUTER JOIN (SELECT ParentID, COUNT(*) AS Count FROM ViewCategoryWiseProduct GROUP BY ParentID) Deriv1 ON a.CategoryID = Deriv1.ParentID WHERE a.ParentID = '" + ParentID + "'";
                dt = objCRUD.getDataTableByQuery(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public int getDuplicateSubCategoryByID(string CategoryID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [CategoryMaster] WHERE IsCategory=2 AND CategoryID = '" + CategoryID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateCategoryByName(string CategoryName)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [CategoryMaster] WHERE IsCategory=1 AND CategoryName = '" + CategoryName + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateCategoryByName(string CategoryName, int IsCategory)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [CategoryMaster] WHERE IsCategory=" + IsCategory + " AND CategoryName = '" + CategoryName + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int DeleteCategoryForDownStream(int IsCategory)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "DELETE FROM [CategoryMaster] WHERE IsCategory = " + IsCategory + "";
                duplicateCount = objCRUD.ExecuteQuery(sqlCMD);
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
                sqlCMD.CommandText = "UPDATE [CategoryMaster] SET IsUPStream = 1 WHERE IsUPStream = 0";
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
