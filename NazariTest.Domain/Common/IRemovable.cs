using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NazariTest.Domain.Common
{
    internal interface IRemovable
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeleterUserId { get; set; }
    }
}
