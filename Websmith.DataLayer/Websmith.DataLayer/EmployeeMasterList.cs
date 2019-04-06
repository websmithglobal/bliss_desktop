using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Websmith.Entity;
using System.Data;

namespace Websmith.DataLayer
{
    public class EmployeeMasterList
    {
        SqlCommand sqlCMD;
        CRUDOperation objCRUD = new CRUDOperation();
        ENT.EmployeeMasterList objENTEmp = new ENT.EmployeeMasterList();

        public bool InsertUpdateDeleteEmployee(ENT.EmployeeMasterList objENT)
        {
            bool row = false;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "InsertUpdateDeleteEmployee";
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@EmpCode", objENT.EmpCode);
                sqlCMD.Parameters.AddWithValue("@EmpName", objENT.EmpName);
                sqlCMD.Parameters.AddWithValue("@Password", objENT.Password);
                sqlCMD.Parameters.AddWithValue("@Mobile", objENT.Mobile);
                sqlCMD.Parameters.AddWithValue("@Email", objENT.Email);
                sqlCMD.Parameters.AddWithValue("@RepotingTo", objENT.RepotingTo);
                sqlCMD.Parameters.AddWithValue("@RoleID", objENT.RoleID);
                sqlCMD.Parameters.AddWithValue("@RoleName", objENT.RoleName);
                sqlCMD.Parameters.AddWithValue("@SalaryAmt", objENT.SalaryAmt);
                sqlCMD.Parameters.AddWithValue("@SalaryType", objENT.SalaryType);
                sqlCMD.Parameters.AddWithValue("@ShiftID", objENT.ShiftID);
                sqlCMD.Parameters.AddWithValue("@Address", objENT.Address);
                sqlCMD.Parameters.AddWithValue("@JoinDate", objENT.JoinDate);
                sqlCMD.Parameters.AddWithValue("@IsDisplayInKDS", objENT.IsDisplayInKDS);
                sqlCMD.Parameters.AddWithValue("@ClassID", objENT.ClassID);
                sqlCMD.Parameters.AddWithValue("@Gender", objENT.Gender);
                sqlCMD.Parameters.AddWithValue("@TotalHourInADay", objENT.TotalHourInADay);
                sqlCMD.Parameters.AddWithValue("@RUserID", objENT.RUserID);
                sqlCMD.Parameters.AddWithValue("@RUserType", objENT.RUserType);
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

        public List<ENT.EmployeeMasterList> getEmployeeMasterList(ENT.EmployeeMasterList objENT)
        {
            List<ENT.EmployeeMasterList> lstENT = new List<ENT.EmployeeMasterList>();
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.Connection = GetConnection.GetDBConnection();
                sqlCMD.CommandType = CommandType.StoredProcedure;
                sqlCMD.CommandText = "GetEmployeeMasterList";
                sqlCMD.Parameters.AddWithValue("@EmployeeID", objENT.EmployeeID);
                sqlCMD.Parameters.AddWithValue("@EmpCode", objENT.EmpCode);
                sqlCMD.Parameters.AddWithValue("@Mode", objENT.Mode);

                lstENT = DBHelper.GetEntityList<ENT.EmployeeMasterList>(sqlCMD);

                //DataTable dt = objCRUD.getDataTable(sqlCMD);

                //lstENT = (from DataRow dr in dt.Rows
                //          select new ENT.EmployeeMasterList()
                //              {
                //                  EmployeeID = new Guid(dr["EmployeeID"].ToString()),
                //                  EmpCode = Convert.ToString(dr["EmpCode"]),
                //                  EmpName = Convert.ToString(dr["EmpName"]),
                //                  Password = Convert.ToString(dr["Password"]),
                //                  Mobile = Convert.ToString(dr["Mobile"]),
                //                  Email = Convert.ToString(dr["Email"]),
                //                  RepotingTo = new Guid(dr["RepotingTo"].ToString()),
                //                  RoleID = new Guid(dr["RoleID"].ToString()),
                //                  RoleName = dr["RoleName"] == DBNull.Value ? "" : Convert.ToString(dr["RoleName"]),
                //                  SalaryAmt = Convert.ToDecimal(dr["SalaryAmt"]),
                //                  SalaryType = Convert.ToInt32(dr["SalaryType"]),
                //                  Address = dr["Address"] == DBNull.Value ? "" : Convert.ToString(dr["Address"]),
                //                  JoinDate = Convert.ToString(dr["JoinDate"]),
                //                  IsDisplayInKDS = Convert.ToInt32(dr["IsDisplayInKDS"]),
                //                  ClassID = new Guid(dr["ClassID"].ToString()),
                //                  Gender = Convert.ToInt32(dr["Gender"]),
                //                  TotalHourInADay = Convert.ToInt32(dr["TotalHourInADay"]),
                //                  EmployeeMasterList_Id = dr["EmployeeMasterList_Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EmployeeMasterList_Id"])
                //              }).ToList();

                //SqlDataReader sdr = sqlCMD.ExecuteReader();
                //lstENT = DBHelper.CopyDataReaderToEntity<ENT.EmployeeMasterList>(sdr);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstENT;
        }

        public int getDuplicateEmployeeByID(string EmployeeID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [EmployeeMasterList] WHERE EmployeeID = '" + EmployeeID + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateEmployeeByName(string EmployeeName)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [EmployeeMasterList] WHERE EmpName = '" + EmployeeName + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateEmployeeByCode(string EmployeeCode)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [EmployeeMasterList] WHERE EmpCode = '" + EmployeeCode + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int getDuplicateEmployeeByIDCode(string EmployeeCode, string EmployeeID)
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "SELECT  * FROM [EmployeeMasterList] WHERE EmployeeID <> '" + EmployeeID + "' AND EmpCode = '" + EmployeeCode + "'";
                DataTable dt = objCRUD.getDataTableByQuery(sqlCMD);
                duplicateCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }

        public int IsUpStreamTrue()
        {
            int duplicateCount = 0;
            try
            {
                sqlCMD = new SqlCommand();
                sqlCMD.CommandText = "UPDATE [EmployeeMasterList] SET IsUPStream = 1 WHERE IsUPStream = 0";
                duplicateCount = objCRUD.ExecuteQuery(sqlCMD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duplicateCount;
        }
    }
}
