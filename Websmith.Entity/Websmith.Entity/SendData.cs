using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class SendData
    {
        public string ipAddress { get; set; }
        public string ackGuid { get; set; }
        public object Object { get; set; }
        public int syncCode { get; set; }
        public SyncMaster syncMaster { get; set; }

        public SendData() { }

        public SendData(int _syncCode, string _ipAddress, string _ackGuid, object _Object, SyncMaster _syncMaster)
        {
            this.syncCode = _syncCode;
            this.ipAddress = _ipAddress;
            this.ackGuid = _ackGuid;
            this.Object = _Object;
            this.syncMaster = _syncMaster;
        }
    }
}
