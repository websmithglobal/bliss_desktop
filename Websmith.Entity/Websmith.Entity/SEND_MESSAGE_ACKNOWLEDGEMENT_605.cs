using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    class SEND_MESSAGE_ACKNOWLEDGEMENT_605
    {
        public string ackGuid { get; set; }
        public string ipAddress { get; set; }
        public ReceiverClient Object { get; set; }
        public int syncCode { get; set; }
    }

    public class ReceiverClient
    {
        public string guid { get; set; }
        public string receiverClientIp { get; set; }
    }
}
