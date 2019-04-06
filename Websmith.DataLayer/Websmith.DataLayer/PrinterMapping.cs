using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class PrinterMapping
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeletePrinterMapping(ENT.PrinterMapping objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeletePrinterMapping";
                sqlCMD.Parameters.AddWithValue("@PrinterMappingID", objENT.PrinterMappingID);
                sqlCMD.Parameters.AddWithValue("@DeviceID", objENT.DeviceID);
                sqlCMD.Parameters.AddWithValue("@PrinterID", objENT.PrinterID);
                sqlCMD.Parameters.AddWithValue("@PartID", objENT.PartID);
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

        public List<ENT.PrinterMapping> GetPrinterMapping(ENT.PrinterMapping objENT)
        {
            List<ENT.PrinterMapping> lstENT = new List<ENT.PrinterMapping>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetPrinterMapping";
                sqlCMD.Parameters.AddWithValue("@PrinterMappingID", objENT.PrinterMappingID);
                sqlCMD.Parameters.AddWithValue("@DeviceID", objENT.DeviceID);
                sqlCMD.Parameters.AddWithValue("@PrinterID", objENT.PrinterID);
                sqlCMD.Parameters.AddWithValue("@PartID", objENT.PartID);
                sqlCMD.Parameters.AddWithValue("@DeviceTypeID", objENT.DeviceTypeID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.PrinterMapping>(sqlCMD);
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

        public int getDuplicatePrinterMapping(ENT.PrinterMapping objENT)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT * FROM [PrinterMapping] WHERE DeviceID='" + objENT.DeviceID + "' AND PartID = " + objENT.PartID + "";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

    }
}
