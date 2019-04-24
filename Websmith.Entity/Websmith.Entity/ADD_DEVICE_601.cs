﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ADD_DEVICE_601
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
        /// Predefined sync code
        /// </summary>
        public int syncCode { get; set; }

        /// <summary>
        /// Device list object
        /// </summary>
        public DevicesList Object { get; set; }

        /// <summary>
        /// Sync master data object
        /// </summary>
        public SyncMaster syncMaster { get; set; }
    }

    public class Device
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
        public List<Device> addDevices { get; set; }
    }
}
