using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class MergeTable
    {
        public Guid ID { get; set; }
        public Guid OrderID { get; set; }
        public Guid TableID { get; set; }
        public Guid OldTableID { get; set; }
        public int TableStatusID { get; set; }
        public string TableName { get; set; }
        public int IsVacant { get; set; }
        public string Mode { get; set; }
    }
}
