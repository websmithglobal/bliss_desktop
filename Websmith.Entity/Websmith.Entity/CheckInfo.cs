using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class CheckInfo
    {
        public string TableName { get; set; }
        public Guid TableID { get; set; }

        public string CustomerName { get; set; }
        public Guid CustomerID { get; set; }

        public int CustomerType { get; set; }
        public string CustomerTypeName { get; set; }

        public int DelieveryType { get; set; }
        public string DelieveryTypeName { get; set; }

        public int DeliveryAddressType { get; set; }
        public string DeliveryAddressTypeName { get; set; }

        public bool IsCurrentQueue { get; set; }
        public string QueueDatetime { get; set; }

        public int QueueType { get; set; }
        public string QueueTypeName { get; set; }

        public int TableStatusID { get; set; }
        public int OrderActions { get; set; }
    }
}
