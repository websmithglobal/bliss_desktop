using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Websmith.DataLayer
{
    public class InwardDetail
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();

        public bool InsertUpdateDeleteInwardDetail(ENT.InwardDetail objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteInwardDetail";
                sqlCMD.Parameters.AddWithValue("@InwardDetailID", objENT.InwardDetailID);
                sqlCMD.Parameters.AddWithValue("@InwardIDF", objENT.InwardIDF);
                sqlCMD.Parameters.AddWithValue("@ProductID", objENT.ProductID);
                sqlCMD.Parameters.AddWithValue("@UnitTypeID", objENT.UnitTypeID);
                sqlCMD.Parameters.AddWithValue("@RecQty", objENT.RecQty);
                sqlCMD.Parameters.AddWithValue("@RejQty", objENT.RejQty);
                sqlCMD.Parameters.AddWithValue("@TotQty", objENT.TotQty);
                sqlCMD.Parameters.AddWithValue("@Rate", objENT.Rate);
                sqlCMD.Parameters.AddWithValue("@SubTotal", objENT.SubTotal);
                sqlCMD.Parameters.AddWithValue("@TaxAmount", objENT.TaxAmount);
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

        public List<ENT.InwardDetail> GetInwardDetail(ENT.InwardDetail objENT)
        {
            List<ENT.InwardDetail> lstENT = new List<ENT.InwardDetail>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetInwardDetail";
                sqlCMD.Parameters.AddWithValue("@InwardIDF", objENT.InwardIDF);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.InwardDetail>(sqlCMD);
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
