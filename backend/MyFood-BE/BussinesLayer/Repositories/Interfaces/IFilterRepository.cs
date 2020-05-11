using DataBaseLayer.ViewModels.Pagination;
using System;
using System.Collections.Generic;
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
}
