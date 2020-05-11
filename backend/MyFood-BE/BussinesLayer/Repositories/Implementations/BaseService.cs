using BussinesLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BussinesLayer.Repositories.Implementations
{
    public class BaseService<TEntity, TContext> : IBaseRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
    {
        private readonly TContext _dbContext;
        public BaseService(TContext context) => _dbContext = context;
        public virtual async Task<bool> Add(TEntity model)
        {
            await _dbContext.Set<TEntity>().AddAsync(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity,bool>> expression = null)
        {
            if (expression == null) return await _dbContext.Set<TEntity>().ToListAsync();
            return await _dbContext.Set<TEntity>().Where(expression).ToListAsync();
        }

        public virtual IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null) return _dbContext.Set<TEntity>();
            return _dbContext.Set<TEntity>().Where(expression);
        }

        public virtual async Task<TEntity> GetById(Guid id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public virtual async Task<bool> Remove(TEntity model)
        {
            _dbContext.Remove(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Update(TEntity model)
        {
            _dbContext.Set<TEntity>().Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
