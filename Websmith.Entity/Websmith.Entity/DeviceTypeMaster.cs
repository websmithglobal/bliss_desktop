using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class DeviceTypeMaster
    {
        public Guid DeviceTypeID { get; set; }
        public string DeviceType { get; set; }
        public int DeviceStatus { get; set; }
        public string Mode { get; set; }
    }
}
