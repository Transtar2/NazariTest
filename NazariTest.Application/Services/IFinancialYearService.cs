﻿using System;
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

namespace NazariTest.Application.Services
{
    public interface IFinancialYearService : IScopedDependency
    {
        Task<JsonResponse> CreateAsync(FinancialYearCreateRequest model);
        Task<FinancialYearResponse> GetByIdAsync(int id);
        Task<IEnumerable<FinancialYearResponse>> GetAllAsync();
        Task<JsonResponse> UpdateAsync(FinancialYearUpdateRequest model);
        Task DeleteAsync(int id);
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
            var entity = mapper.Map<FinancialYear>(model);
            
            await repo.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();
            return new JsonResponse
            {
                IsSuccess = true,
                Message = MessageType.InsertOk.ToDescription()
            };
        }

        public async Task DeleteAsync(int id)
        {
           // var currentUserId = get current user id from current context ;
            var repo = unitOfWork.Repository<FinancialYear>();
            var entity = await repo.GetByIdAsync(id);
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.Now;
            // entity.DeleterUserId = id set this user id;
            repo.Update(entity);
            await unitOfWork.SaveChangesAsync();
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

        public async Task<JsonResponse> UpdateAsync(FinancialYearUpdateRequest model)
        {
            var repo = unitOfWork.Repository<FinancialYear>();
            var entity = mapper.Map<FinancialYear>(model);
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