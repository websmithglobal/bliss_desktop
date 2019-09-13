using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class CustomerMasterData
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();
        ENT.CustomerMasterData objENTCust = new ENT.CustomerMasterData();

        public bool InsertUpdateDeleteCustomer(ENT.CustomerMasterData objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteCustomer";
                sqlCMD.Parameters.AddWithValue("@CustomerID", objENT.CustomerID);
                sqlCMD.Parameters.AddWithValue("@Name", objENT.Name);
                sqlCMD.Parameters.AddWithValue("@MobileNo", objENT.MobileNo);
                sqlCMD.Parameters.AddWithValue("@EmailID", objENT.EmailID);
                sqlCMD.Parameters.AddWithValue("@Address", objENT.Address);
                sqlCMD.Parameters.AddWithValue("@CardNo", objENT.CardNo);
                sqlCMD.Parameters.AddWithValue("@Birthdate", "");
                sqlCMD.Parameters.AddWithValue("@ShippingAddress", objENT.ShippingAddress);
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
            return row;
        }

        public List<ENT.CustomerMasterData> getCustomerData(ENT.CustomerMasterData objENT)
        {
            List<ENT.CustomerMasterData> lstENT = new List<Entity.CustomerMasterData>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetCustomerMasterData";
                sqlCMD.Parameters.AddWithValue("@CustomerID", objENT.CustomerID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.CustomerMasterData>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public ENT.Customer getCustomerForSocket(ENT.CustomerMasterData objENT)
        {
            ENT.Customer lstENT = new ENT.Customer();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetCustomerMasterData";
                sqlCMD.Parameters.AddWithValue("@CustomerID", objENT.CustomerID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                SqlDataReader reader = sqlCMD.ExecuteReader();
                lstENT = DBHelper.GetSingleEntity<ENT.Customer>(reader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateCustomerByID(string CustomerID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [CustomerMasterData] WHERE CustomerID = '" + CustomerID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateCustomerByName(string CustomerName)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [CustomerMasterData] WHERE Name = '" + CustomerName + "'";
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
                sqlCMD.CommandText = "UPDATE [CustomerMasterData] SET IsUPStream = 1 WHERE IsUPStream = 0";
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
