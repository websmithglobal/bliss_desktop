using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Websmith.Entity
{
    public class ADD_DEVICE_601
    {
        public string ackGuid { get; set; }
        public string ipAddress { get; set; }

        [JsonProperty(PropertyName = "Object")]
        public AddDeviceList Object { get; set; }

        public int syncCode { get; set; }
        public SyncMaster syncMaster { get; set; }
    }

    public class AddDevice
    {
        public string guId { get; set; }
        public string ip { get; set; }
        public string lastSync { get; set; }
        public string station { get; set; }
        public string stationname { get; set; }
        public int status { get; set; }
        public int type { get; set; }
    }

    public class AddDeviceList
    {
        public List<AddDevice> addDevices { get; set; }
    }
  
}
