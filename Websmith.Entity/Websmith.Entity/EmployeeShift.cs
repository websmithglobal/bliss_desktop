using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class EmployeeShift
    {
        public Guid ShiftID { get; set; }
        public Guid EmployeeID { get; set; }
        public int EmployeeMasterList_ID { get; set; }
        public string Mode { get; set; }
    }
}
