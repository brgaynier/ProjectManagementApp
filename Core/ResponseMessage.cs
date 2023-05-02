using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ResponseMessage
    {
        public ResponseMessageType MessageType { get; set; }
        public int Code { get; set; }
        public string? Service { get; set; }
        public string? Name { get; set; }
        public string? DebugDetails { get; set; }
        public string? DisplayText { get; set; }
        public ResponseMessage? InnerMessage { get; set; }
    }
}
