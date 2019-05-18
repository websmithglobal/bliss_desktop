using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class SendMessageData
    {
        public Int64 A_id { get; set; }
        public string msg_guid { get; set; }
        public string message_data { get; set; }
        public string client_ip { get; set; }
        public int message_send_status { get; set; }
        public int message_acknowledge_status { get; set; }
        public string Mode { get; set; }
    }
}
