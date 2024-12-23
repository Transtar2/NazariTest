using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NazariTest.Domain.Common;

namespace NazariTest.Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        Task AddAsync(TEntity entity);
        void Add(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        IQueryable<TEntity> GetQuery();
        IQueryable<TResult> GetQuery<TResult>(Expression<Func<TEntity, TResult>> selector = null);

    }
}
