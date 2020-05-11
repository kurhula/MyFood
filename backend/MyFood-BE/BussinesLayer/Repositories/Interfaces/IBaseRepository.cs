using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
    {
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression = null);
        IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity> GetById(Guid id);
        Task<bool> Add(TEntity model);
        Task<bool> Update(TEntity model);
        Task<bool> Remove(TEntity model);
    }
}
