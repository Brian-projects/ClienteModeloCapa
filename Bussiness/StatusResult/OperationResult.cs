using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.StatusResult
{
    class OperationResult<T>
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public T Data { get; set; }
    }
}
