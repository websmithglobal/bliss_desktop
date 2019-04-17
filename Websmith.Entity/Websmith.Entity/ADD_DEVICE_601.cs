using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ADD_DEVICE_601
    {
        public DevicesList Object { get; set; }
        public string ackGuid { get; set; }
        public string ipAddress { get; set; }
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

    public class DevicesList
    {
        public List<AddDevice> addDevices { get; set; }
    }
}
