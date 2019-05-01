using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Websmith.Entity
{
    public class REMOVE_DEVICE_602
    {
        [JsonProperty(PropertyName ="Object")]
        public RemoveDeviceList Object { get; set; }
        public string ackGuid { get; set; }
        public string ipAddress { get; set; }
        public int syncCode { get; set; }
    }

    public class RemoveDevice
    {
        public string id { get; set; }
        public string ip { get; set; }
    }

    public class RemoveDeviceList
    {
        public List<RemoveDevice> removeDevices { get; set; }
    }

}
