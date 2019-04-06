using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Websmith.DataLayer
{
    public class GetConnection
    {
        [SettingsDescription("This properties will return connection is open or not.")]
        [DefaultSettingValue("false")]
        public static Boolean isConnectionOpen { get; set; }

        private static string ReadConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; 
        }

        public static SqlConnection GetDBConnection()
        {
            SqlConnection sqlcon = null;
            try
            {
                sqlcon = new SqlConnection(ReadConnectionString());
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    isConnectionOpen = true;
                }
                else { isConnectionOpen = false; }
            }
            catch (Exception)
            {
                isConnectionOpen = false;
            }
            return sqlcon;
        }

        public static Boolean OpenConnection(SqlConnection sqlCon)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                if (sqlCon.State == ConnectionState.Open)
                {
                    isConnectionOpen = true;
                }
                else
                {
                    isConnectionOpen = false;
                }
            }
            catch (Exception)
            {
                isConnectionOpen = false;
            }
            return isConnectionOpen;
        }

        public static Boolean CloseConnection(SqlConnection sqlCon)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();

                if (sqlCon.State == ConnectionState.Closed)
                {
                    isConnectionOpen = false;
                }
                else
                {
                    isConnectionOpen = true;
                }
            }
            catch (Exception)
            {
                isConnectionOpen = true;
            }
            return isConnectionOpen;
        }
        
    }
}
