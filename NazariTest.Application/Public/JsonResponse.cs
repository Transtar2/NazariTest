using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NazariTest.Application.Enums;

namespace NazariTest.Application.Public
{
    public class JsonResponse
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
