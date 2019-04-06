using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;
namespace Websmith.DataLayer
{
    public class TimeSheetWiseDiscount
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteTimeSheetWiseDiscount(ENT.TimeSheetWiseDiscount objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteTimeSheetWiseDiscount";
                sqlCMD.Parameters.AddWithValue("@FromTime", objENT.FromTime);
                sqlCMD.Parameters.AddWithValue("@ToTime", objENT.ToTime);
                sqlCMD.Parameters.AddWithValue("@StartDate", objENT.StartDate);
                sqlCMD.Parameters.AddWithValue("@EndDate", objENT.EndDate);
                sqlCMD.Parameters.AddWithValue("@Day", objENT.Day);
                sqlCMD.Parameters.AddWithValue("@DiscountMasterDetail_Id", objENT.DiscountMasterDetail_Id);
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

        public List<ENT.TimeSheetWiseDiscount> getTimeSheetWiseDiscount(ENT.TimeSheetWiseDiscount objENT)
        {
            List<ENT.TimeSheetWiseDiscount> lstENT = new List<ENT.TimeSheetWiseDiscount>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetTimeSheetWiseDiscount";
                sqlCMD.Parameters.AddWithValue("@DiscountMasterDetail_Id", objENT.DiscountMasterDetail_Id);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.TimeSheetWiseDiscount>(sqlCMD);

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.TimeSheetWiseDiscount>(sdr);

                //lstENTTSWD = (from DataRow dr in dt.Rows
                //               select new ENT.TimeSheetWiseDiscount()
                //               {
                //                   FromTime = Convert.ToString(dr["FromTime"]),
                //                   ToTime = Convert.ToString(dr["ToTime"]),
                //                   StartDate = Convert.ToString(dr["StartDate"]),
                //                   EndDate = Convert.ToString(dr["EndDate"]),
                //                   Day = Convert.ToInt32(dr["Day"]),
                //                   DiscountMasterDetail_Id = Convert.ToInt32(dr["DiscountMasterDetail_Id"])
                //               }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int deleteTimeSheetWiseDiscount()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "DELETE FROM [TimeSheetWiseDiscount]";
                sqlCMD.Connection = GetConnection.GetDBConnection();
                duplicateCount = sqlCMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }
    }
}
