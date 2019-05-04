using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ReceiveMessageData
    {
        public Guid a_id { get; set; }
        public Guid msg_guid { get; set; }
        public string client_ip { get; set; }
        public string message { get; set; }
        public int send_acknowledge_status { get; set; }
        public string Mode { get; set; }
    }
}
