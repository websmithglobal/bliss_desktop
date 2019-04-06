using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class ViewKOTRoutingPrint
    {
        public static List<ENT.ViewKOTRoutingPrint> GetKOTRoutingPrint(ENT.ViewKOTRoutingPrint objENT)
        {
            List<ENT.ViewKOTRoutingPrint> lstENT = new List<ENT.ViewKOTRoutingPrint>();
            try
            {
                SqlCommand sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetKOTRoutingPrint";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@DeviceID", objENT.DeviceID);
                sqlCMD.Parameters.AddWithValue("@PrinterID", objENT.PrinterID);
                sqlCMD.Parameters.AddWithValue("@TableID", objENT.TableID);
                sqlCMD.Parameters.AddWithValue("@CategoryID", objENT.CategoryID);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@OrderActions", objENT.OrderActions);
                sqlCMD.Parameters.AddWithValue("@OrderStatus", objENT.OrderStatus);
                sqlCMD.Parameters.AddWithValue("@IsPrint", objENT.IsPrint);
                sqlCMD.Parameters.AddWithValue("@IsSendToKitchen", objENT.IsSendToKitchen);
                sqlCMD.Parameters.AddWithValue("@HeaderStatus", objENT.HeaderStatus);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.ViewKOTRoutingPrint>(sqlCMD);
            }
            catch (Exception)
            {
                throw;
            }
            return lstENT;
        }
    }
}
