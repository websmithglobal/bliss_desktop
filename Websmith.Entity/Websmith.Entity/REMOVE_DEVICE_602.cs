using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class REMOVE_DEVICE_602
    {
        /// <summary>
        /// New GUID
        /// </summary>
        public string ackGuid { get; set; }

        /// <summary>
        /// Self IP Address
        /// </summary>
        public string ipAddress { get; set; }

        /// <summary>
        /// Remove Devices list object
        /// </summary>
        public RemoveDevicesList Object { get; set; }

        /// <summary>
        /// Predefined sync code
        /// </summary>
        public int syncCode { get; set; }
    }

    public class RemoveDevice
    {
        /// <summary>
        /// device id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Device IP
        /// </summary>
        public string ip { get; set; }
    }

    public class RemoveDevicesList
    {
        public List<RemoveDevice> removeDevices { get; set; }
    }
}
