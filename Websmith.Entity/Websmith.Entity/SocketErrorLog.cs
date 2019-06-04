using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class SocketErrorLog
    {
        public Int64 log_id { get; set; }
        public string log_error_location { get; set; }
        public string log_exception { get; set; }
        public string Mode { get; set; }
    }
}
