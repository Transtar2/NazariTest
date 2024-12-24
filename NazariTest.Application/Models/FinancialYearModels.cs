using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NazariTest.Application.Models
{
    public class FinancialYearCreateRequest
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage ="الزامی")]
        [Display(Name = "نام")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "الزامی")]
        [Display(Name = "تاریخ شروع")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "الزامی")]
        [Display(Name ="تاریخ پایان")]
        public string EndDate { get; set; }
    }
    public class FinancialYearUpdateRequest: FinancialYearCreateRequest
    {
        public Guid Id { get; set; }
    }
    public class FinancialYearResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
