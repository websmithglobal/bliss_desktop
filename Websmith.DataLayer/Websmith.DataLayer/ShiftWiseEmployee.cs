using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class ShiftWiseEmployee
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteShiftWiseEmployee(ENT.ShiftWiseEmployee objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteShiftWiseEmployee";
                sqlCMD.Parameters.AddWithValue("@ShiftWiseEmployeeID", objENT.ShiftWiseEmployeeID);
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@ShiftID", objENT.ShiftID);
                sqlCMD.Parameters.AddWithValue("@RUserID", objENT.RUserID);
                sqlCMD.Parameters.AddWithValue("@RUserType", objENT.RUserType);
                //sqlCMD.Parameters.AddWithValue("@CreatedDate", objENT.CreatedDate);
                sqlCMD.Parameters.AddWithValue("@IsStatus", objENT.IsStatus);
                sqlCMD.Parameters.AddWithValue("@ShiftDay", objENT.ShiftDay);
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

        public List<ENT.ShiftWiseEmployee> getShiftWiseEmployee(ENT.ShiftWiseEmployee objENT)
        {
            List<ENT.ShiftWiseEmployee> lstENT = new List<ENT.ShiftWiseEmployee>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetShiftWiseEmployee";
                sqlCMD.Parameters.AddWithValue("@ShiftWiseEmployeeID", objENT.ShiftWiseEmployeeID);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.ShiftWiseEmployee>(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateByShiftWiseEmployeeID(string ShiftWiseEmployeeID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [ShiftWiseEmployee] WHERE ShiftWiseEmployeeID = '" + ShiftWiseEmployeeID + "'";
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
