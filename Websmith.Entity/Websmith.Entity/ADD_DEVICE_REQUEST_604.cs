using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ADD_DEVICE_REQUEST_604
    {
        public string ackGuid { get; set; }
        public string ipAddress { get; set; }

        [JsonProperty(PropertyName = "Object")]
        public AddDeviceList Object { get; set; }

        public int syncCode { get; set; }
    }
}
