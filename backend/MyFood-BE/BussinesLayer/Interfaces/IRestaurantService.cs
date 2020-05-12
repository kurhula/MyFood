using BussinesLayer.Repositories.Interfaces;
using DataBaseLayer.Models.Restaurants;
using DataBaseLayer.Persistence;
using DataBaseLayer.ViewModels.Restaurant;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Interfaces
{
    public interface IRestaurantService : IBaseRepository<Restaurant,ApplicationDbContext> , IFilterRepository<Restaurant> 
    {
        Task<bool> SoftRemove(Guid id);
        Task<Restaurant> GetByUserId(string userId);
    }
}
