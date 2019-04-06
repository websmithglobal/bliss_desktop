using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Websmith.DataLayer
{
    public class OutwardDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteOutwardDetail(ENT.OutwardDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteOutwardDetail";
                sqlCMD.Parameters.AddWithValue("@OutwardDetailID", objENT.OutwardDetailID);
                sqlCMD.Parameters.AddWithValue("@OutwardIDF", objENT.OutwardIDF);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@UnitTypeID", objENT.UnitTypeID);
                sqlCMD.Parameters.AddWithValue("@Qty", objENT.Qty);
                sqlCMD.Parameters.AddWithValue("@Rate", objENT.Rate);
                sqlCMD.Parameters.AddWithValue("@TotalAmount", objENT.TotalAmount);
                sqlCMD.Parameters.AddWithValue("@Sort", objENT.Sort);
                sqlCMD.Parameters.AddWithValue("@IsUpStream", objENT.IsUpStream);
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

        public List<ENT.OutwardDetail> GetOutwardDetail(ENT.OutwardDetail objENT)
        {
            List<ENT.OutwardDetail> lstENT = new List<ENT.OutwardDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetOutwardDetail";
                sqlCMD.Parameters.AddWithValue("@OutwardIDF", objENT.OutwardIDF);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.OutwardDetail>(sqlCMD);
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
