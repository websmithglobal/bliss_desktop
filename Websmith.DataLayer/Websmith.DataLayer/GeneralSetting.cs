using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class GeneralSetting
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();
        
        public bool InsertUpdateDeleteGeneralSetting(ENT.GeneralSetting objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteGeneralSetting";
                sqlCMD.Parameters.AddWithValue("@PaymentGatewayID", objENT.PaymentGatewayID);
                sqlCMD.Parameters.AddWithValue("@PaymentGatewayName", objENT.PaymentGatewayName);
                sqlCMD.Parameters.AddWithValue("@PrintHeader", objENT.PrintHeader);
                sqlCMD.Parameters.AddWithValue("@PrintFooter", objENT.PrintFooter);
                sqlCMD.Parameters.AddWithValue("@DuplicatePrint", objENT.DuplicatePrint);
                sqlCMD.Parameters.AddWithValue("@KOTCount", objENT.KOTCount);
                sqlCMD.Parameters.AddWithValue("@OrderPrefix", objENT.OrderPrefix);
                sqlCMD.Parameters.AddWithValue("@KOTFontSize", objENT.KOTFontSize);
                sqlCMD.Parameters.AddWithValue("@KOTServerName", objENT.KOTServerName);
                sqlCMD.Parameters.AddWithValue("@KOTDateTime", objENT.KOTDateTime);
                sqlCMD.Parameters.AddWithValue("@KOTOrderType", objENT.KOTOrderType);
                sqlCMD.Parameters.AddWithValue("@KDSWithoutDisplay", objENT.KDSWithoutDisplay);
                sqlCMD.Parameters.AddWithValue("@RoundingTotal", objENT.RoundingTotal);
                sqlCMD.Parameters.AddWithValue("@DisplayCardNo", objENT.DisplayCardNo);
                sqlCMD.Parameters.AddWithValue("@PrintOnPaymentDone", objENT.PrintOnPaymentDone);
                sqlCMD.Parameters.AddWithValue("@RunningOrderDisplayOnKOT", objENT.RunningOrderDisplayOnKOT);
                sqlCMD.Parameters.AddWithValue("@KDSWithoutPrinter", objENT.KDSWithoutPrinter);
                sqlCMD.Parameters.AddWithValue("@CustomerNameOnKOT", objENT.CustomerNameOnKOT);
                sqlCMD.Parameters.AddWithValue("@DateTimeFormat", objENT.DateTimeFormat);
                sqlCMD.Parameters.AddWithValue("@Language", objENT.Language);
                sqlCMD.Parameters.AddWithValue("@TillCur1", objENT.TillCur1);
                sqlCMD.Parameters.AddWithValue("@TillCur2", objENT.TillCur2);
                sqlCMD.Parameters.AddWithValue("@TillCur3", objENT.TillCur3);
                sqlCMD.Parameters.AddWithValue("@TillCur4", objENT.TillCur4);
                sqlCMD.Parameters.AddWithValue("@TillCur5", objENT.TillCur5);
                sqlCMD.Parameters.AddWithValue("@TillCur6", objENT.TillCur6);
                sqlCMD.Parameters.AddWithValue("@TillCur7", objENT.TillCur7);
                sqlCMD.Parameters.AddWithValue("@TillCur8", objENT.TillCur8);
                sqlCMD.Parameters.AddWithValue("@TillCur9", objENT.TillCur9);
                sqlCMD.Parameters.AddWithValue("@DineIn", objENT.DineIn);
                sqlCMD.Parameters.AddWithValue("@TakeOut", objENT.TakeOut);
                sqlCMD.Parameters.AddWithValue("@Delivery", objENT.Delivery);
                sqlCMD.Parameters.AddWithValue("@OrderAhead", objENT.OrderAhead);
                sqlCMD.Parameters.AddWithValue("@Queue", objENT.Queue);
                sqlCMD.Parameters.AddWithValue("@PartyEvent", objENT.PartyEvent);
                sqlCMD.Parameters.AddWithValue("@TaxLabel1", objENT.TaxLabel1);
                sqlCMD.Parameters.AddWithValue("@TaxPercentage1", objENT.TaxPercentage1);
                sqlCMD.Parameters.AddWithValue("@TaxLabel2", objENT.TaxLabel2);
                sqlCMD.Parameters.AddWithValue("@TaxPercentage2", objENT.TaxPercentage2);
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

        public List<ENT.GeneralSetting> GetGeneralSetting(ENT.GeneralSetting objENT)
        {
            List<ENT.GeneralSetting> lstENT = new List<ENT.GeneralSetting>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetGeneralSetting";
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);
                lstENT = DBHelper.GetEntityList<ENT.GeneralSetting>(sqlCMD);
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
