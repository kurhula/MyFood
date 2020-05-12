using DataBaseLayer.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Repositories.Interfaces
{
    public interface IFilterRepository<TEntity, TFilter> where TEntity : class where TFilter : class
    {
        /// <summary>
        /// To filters operations
        /// </summary>
        /// <param name="filters">params to filter</param>
        /// <returns></returns>
        Task<Pagination<TEntity>> GetAllPaginated(TFilter filters = null);
    }

    public interface IFilterRepository<TEntity> where TEntity : class
    { 
        Task<Pagination<TEntity>> GetAllPaginated(Expression<Func<TEntity,bool>> expression = null , int page = 0 , int quantity = 10);
    }
}
