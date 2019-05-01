using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Websmith.Entity
{
    public class SyncGeneralJson
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
        /// Any type of object
        /// </summary>
        public object Object { get; set; }

        /// <summary>
        /// Predefined sync code
        /// </summary>
        public int syncCode { get; set; }

        /// <summary>
        /// Sync master data object
        /// </summary>
        public SyncMaster syncMaster { get; set; }
    }

}
