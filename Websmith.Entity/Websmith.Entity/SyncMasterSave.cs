using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class SyncMasterSave
    {
        public Guid SYNC_MASTER_ID { get; set; }
        public string SYNC_MASTER_DEVICE_IP { get; set; }
        public string SYNC_MASTER_SYNC_CODE { get; set; }
        public Guid SYNC_MASTER_BATCH_CODE { get; set; }
        public string SYNC_MASTER_DATE_TIME { get; set; }
        public string Mode { get; set; }
    }
}
