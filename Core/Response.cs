using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Response<T>
    {
        public List<ResponseMessage> ErrorMessages { get; set; } = new List<ResponseMessage>();
        public List<ResponseMessage> SuccessMessages { get; set; } = new List<ResponseMessage>();
        public List<ResponseMessage> WarningMessages { get; set; } = new List<ResponseMessage>();
        public ResponseStatus Status { get; set; } = 0;
        public string? Message { get; set; }
        public T? Value { get; set; }
    }
}
