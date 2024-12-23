using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NazariTest.Domain.Common;

namespace NazariTest.Domain.Entities
{
    public class FinancialYear : BaseEntity, IRemovable
    {
        public string Title  { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }=false;
        public DateTime? DeletedAt { get; set; }
        public int? DeleterUserId { get; set; }
    }
}
