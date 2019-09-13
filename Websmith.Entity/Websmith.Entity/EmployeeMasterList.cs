using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class EmployeeMasterList
    {
        public Guid EmployeeID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Guid RepotingTo { get; set; }
        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public decimal SalaryAmt { get; set; }
        public string SalaryTypeName { get; set; }
        public int SalaryType { get; set; }
        public Guid ShiftID { get; set; }
        public string Address { get; set; }
        public DateTime JoinDate { get; set; }
        public int IsDisplayInKDS { get; set; }
        public Guid ClassID { get; set; }
        public int Gender { get; set; }
        public string GenderName { get; set; }
        public int TotalHourInADay { get; set; }
        public int EmployeeMasterList_Id { get; set; }
        public Guid RUserID { get; set; }
        public int RUserType { get; set; }
        public string Mode { get; set; }
        public int IsUPStream { get; set; } = 0;
        public List<EmployeeShift> EmployeeShift { get; set; }
        public List<object> EmployeeProduct { get; set; }
    }
}
