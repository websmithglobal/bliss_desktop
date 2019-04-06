using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class UpStreamResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public VersionDetail VersionDetail { get; set; }
    }
}
