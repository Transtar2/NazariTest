using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NazariTest.Application.Extensions;
using NazariTest.Application.Models;
using NazariTest.Domain.Entities;

namespace NazariTest.Application.Mappings
{
    public class FinancialYearProfile : Profile
    {
        public FinancialYearProfile()
        {
            CreateMap<FinancialYearCreateRequest, FinancialYear>()
                .ForPath(dest=>dest.StartDate,src=> src.MapFrom(x=>x.StartDate.ShamsiToGregorian()))
                .ForPath(dest=>dest.EndDate,src=> src.MapFrom(x=>x.EndDate.ShamsiToGregorian()));

            CreateMap<FinancialYearUpdateRequest, FinancialYear>()
                .ForPath(dest => dest.StartDate, src => src.MapFrom(x => x.StartDate.ShamsiToGregorian()))
                .ForPath(dest => dest.EndDate, src => src.MapFrom(x => x.EndDate.ShamsiToGregorian()));
            
            CreateMap<FinancialYear,FinancialYearResponse>()
                .ForPath(dest=> dest.StartDate,src=>src.MapFrom(x=>x.StartDate.GregorianToShamsi(true)))
                .ForPath(dest=> dest.EndDate,src=>src.MapFrom(x=>x.EndDate.GregorianToShamsi(true)));
        }
    }
}
