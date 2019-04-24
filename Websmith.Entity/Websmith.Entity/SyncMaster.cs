using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class SyncMaster
    {
        /// <summary>
        /// Predefined sync code For communication identification
        /// </summary>
        public int SyncCode { get; set; } = 0;

        /// <summary>
        /// Main id of Object
        /// </summary>
        public string batchCode { get; set; } = string.Empty;

        /// <summary>
        /// Current System Date
        /// </summary>
        public string date { get; set; } = string.Empty;

        /// <summary>
        /// New GUID
        /// </summary>
        public string id { get; set; } = string.Empty;

        public SyncMaster()
        { }

        public SyncMaster(int _SyncCode, string _batchCode, string _date, string _id)
        {
            this.SyncCode = _SyncCode;
            this.batchCode = _batchCode;
            this.date = _date;
            this.id = _id;
        }
    }
}
