using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NazariTest.Application.AutoFac;
using NazariTest.Application.Enums;
using NazariTest.Application.Extensions;
using NazariTest.Application.Interfaces;
using NazariTest.Application.Models;
using NazariTest.Application.Public;
using NazariTest.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NazariTest.Application.Services
{
    public interface IFinancialYearService : IScopedDependency
    {
        Task<JsonResponse> CreateAsync(FinancialYearCreateRequest model);
        Task<FinancialYearResponse> GetByIdAsync(int id);
        Task<IEnumerable<FinancialYearResponse>> GetAllAsync();
        Task<JsonResponse> UpdateAsync(FinancialYearCreateRequest model);
        Task<JsonResponse> DeleteAsync(Guid id);
        Task DeleteRangeAsync(IEnumerable<Guid> ids);

    }

    public class FinancialYearService
        (
          IUnitOfWork unitOfWork,
           IMapper mapper
        ) : IFinancialYearService
    {
        public async Task<JsonResponse> CreateAsync(FinancialYearCreateRequest model)
        {
            var repo = unitOfWork.Repository<FinancialYear>();
            var validationResult = DateOverlapValidation(model);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            model.Title = model.Title.Trim();
            var entity = mapper.Map<FinancialYear>(model);
            await repo.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();
            return new JsonResponse
            {
                IsSuccess = true,
                Message = MessageType.InsertOk.ToDescription()
            };
        }
        private JsonResponse DateOverlapValidation(FinancialYearCreateRequest model)
        {
            var repo = unitOfWork.Repository<FinancialYear>();
            var startDate = model.StartDate.ShamsiToGregorian();
            var endDate = model.EndDate.ShamsiToGregorian();

            var financialYearList = repo.GetAll().Where(x=> !x.IsDeleted).ToList();
            if (model.Id is not null)
            {
                financialYearList= financialYearList.Where(x=>x.Id != model.Id).ToList();
            }
            

            if (financialYearList.Any(x => x.Title == model.Title.Trim()))
            {
                return new JsonResponse
                {
                    IsSuccess = false,
                    Message = "دوره ای با این عنوان قبلا تعریف شده است"
                };
            }
            if (startDate > endDate)
            {
                return new JsonResponse
                {
                    IsSuccess = false,
                    Message = "تاریخ شروع از تاریخ پایان نمیتواند بزرگتر باشد"
                };
            }
            if (financialYearList.Any(x => x.StartDate.Date <= startDate.Date && x.EndDate.Date > endDate.Date))
            {
                return new JsonResponse
                {
                    IsSuccess = false,
                    Message = "این رنج تاریخ با دوره های قبلی تداخل دارد"
                };
            }
            if (financialYearList.Any(x => x.StartDate.Date <= endDate.Date && x.EndDate.Date >= endDate.Date))
            {
                return new JsonResponse
                {
                    IsSuccess = false,
                    Message = "این رنج تاریخ با دوره های قبلی تداخل دارد"
                };
            }
            return new JsonResponse
            {
                IsSuccess = true
            };

        }
        public async Task<JsonResponse> DeleteAsync(Guid id)
        {
            // var currentUserId = get current user id from current context ;
            var repo = unitOfWork.Repository<FinancialYear>();
            var entity = await repo.GetByIdAsync(id);
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.Now;
            // entity.DeleterUserId = id set this user id;
            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();
            return new JsonResponse
            {
                IsSuccess = true,
                Message= MessageType.DeleteOk.ToDescription()
            };
        }

        public async Task DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            var repo = unitOfWork.Repository<FinancialYear>();
            var entities = await repo.GetAllAsync();
            var entitiesToRemove = entities.Where(e => ids.Contains(e.Id));
            repo.RemoveRange(entitiesToRemove);
            await unitOfWork.SaveChangesAsync();
        }
        public async Task<IEnumerable<FinancialYearResponse>> GetAllAsync()
        {
            var repo = unitOfWork.Repository<FinancialYear>();
            var query = repo.GetQuery().Where(x => !x.IsDeleted);
            var result = mapper.Map<List<FinancialYearResponse>>(query.ToList());
            return result;
        }

        public async Task<FinancialYearResponse> GetByIdAsync(int id)
        {
            var repo = unitOfWork.Repository<FinancialYear>();
            var entity = await repo.GetByIdAsync(id);
            return mapper.Map<FinancialYearResponse>(entity);
        }

        public async Task<JsonResponse> UpdateAsync(FinancialYearCreateRequest model)
        {
            var repo = unitOfWork.Repository<FinancialYear>();
            var entity = mapper.Map<FinancialYear>(model);

            var validationResult = DateOverlapValidation(model);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }
            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();
            return new JsonResponse
            {
                IsSuccess = true,
                Message = MessageType.UpdateOk.ToDescription()
            };
        }
    }
}
