using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Websmith.DataLayer
{
    public class VendorTaxsNo
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteVendorTaxsNo(ENT.VendorTaxsNo objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteVendorTaxsNo";
                sqlCMD.Parameters.AddWithValue("@VendorTaxsNoID", objENT.VendorTaxsNoID);
                sqlCMD.Parameters.AddWithValue("@VendorID", objENT.VendorID);
                sqlCMD.Parameters.AddWithValue("@TaxName", objENT.TaxName);
                sqlCMD.Parameters.AddWithValue("@TaxNo", objENT.TaxNo);
                sqlCMD.Parameters.AddWithValue("@TaxPercentage", objENT.TaxPercentage);
                sqlCMD.Parameters.AddWithValue("@RUserID", objENT.RUserID);
                sqlCMD.Parameters.AddWithValue("@RUserType", objENT.RUserType);
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

        public List<ENT.VendorTaxsNo> GetVendorTaxsNo(ENT.VendorTaxsNo objENT)
        {
            List<ENT.VendorTaxsNo> lstENT = new List<ENT.VendorTaxsNo>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetVendorTaxsNo";
                sqlCMD.Parameters.AddWithValue("@VendorTaxsNoID", objENT.VendorTaxsNoID);
                sqlCMD.Parameters.AddWithValue("@VendorID", objENT.VendorID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.VendorTaxsNo>(sqlCMD);
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
    }
}
