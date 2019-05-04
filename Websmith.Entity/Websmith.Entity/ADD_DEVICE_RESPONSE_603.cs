using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ADD_DEVICE_RESPONSE_603
    {
        public string ackGuid { get; set; }
        public string ipAddress { get; set; }
        public DevicesList Object { get; set; }
        public int syncCode { get; set; }
    }
}
