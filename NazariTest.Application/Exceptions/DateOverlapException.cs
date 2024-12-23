using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NazariTest.Application.Exceptions
{
    public class DateOverlapException : Exception
    {
        public DateOverlapException(string message) : base(message) 
        {
            
        }
        public DateOverlapException() : this("خطای در همپوشانی تاریخ ها")
        {

        }
    }
}
