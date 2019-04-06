using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class OrderCombo
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteOrderCombo(ENT.OrderCombo objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteOrderCombo";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                row = objCRUD.InsertUpdateDelete(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }
        
        public List<ENT.OrderCombo> getOrderComboForUpStream(ENT.OrderCombo objENT)
        {
            List<ENT.OrderCombo> lstENT = new List<ENT.OrderCombo>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOrderCombo";
                sqlCMD.Parameters.AddWithValue("@OrderID", objENT.OrderID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.OrderCombo>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

    }
}
