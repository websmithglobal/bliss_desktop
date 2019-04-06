using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class LoginDetail
    {
        public Guid BranchID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string App_Version { get; set; }
        public string Login_Via { get; set; }
        public string Device_ID { get; set; }
        public string IMEI_No { get; set; }
        public string Mode { get; set; }
    }
}
