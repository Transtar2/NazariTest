using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NazariTest.Application.Interfaces;
using NazariTest.Domain.Common;

namespace NazariTest.Persistence.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
                return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.AsNoTracking().ToListAsync();
        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.AsNoTracking().ToList();
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(TEntity entity)
        {
            try
            {
                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }


        IQueryable<TResult> IRepository<TEntity>.GetQuery<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            var query = _dbSet.AsQueryable();
            return query.Select(selector);
        }

        IQueryable<TEntity> IRepository<TEntity>.GetQuery()
        {
            return _dbSet.AsQueryable();
        }
    }
}
