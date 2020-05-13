using BussinesLayer.Interfaces;
using BussinesLayer.Repositories.Implementations;
using BussinesLayer.Repositories.Interfaces;
using Commons.Helpers;
using DataBaseLayer.Models.Restaurants;
using DataBaseLayer.Persistence;
using DataBaseLayer.ViewModels.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services
{
    public class RestaurantService : BaseService<Restaurant, ApplicationDbContext>, IRestaurantService
    {
        private readonly ApplicationDbContext _dbcontext;
        public RestaurantService(ApplicationDbContext dbcontext) : base(dbcontext) => _dbcontext = dbcontext;

        public async Task<Pagination<Restaurant>> GetAllPaginated(Expression<Func<Restaurant, bool>> expression = null, int page = 0, int quantity = 10)
        {
            var result = GetAllQueryable();
            if (expression != null) result = result.Where(expression);

            var total = await result.CountAsync();
            var pages = total / quantity;

            return new Pagination<Restaurant>
            {
                ActualPage = page,
                Pages = pages,
                Entities = await result.Take(quantity).Skip((page - 0) * quantity).ToListAsync(),
                Total = total
            };
        }

        public async Task<Restaurant> GetByUserId(string userId) => await _dbcontext.Restaurants.FirstOrDefaultAsync(x => x.AppUserId == userId);

        public async Task<bool> SetRating(Guid id, int newRating)
        {
            var restaurant = await GetById(id);
            restaurant.StarsQuantity += 1;
            restaurant.StartsTotal += newRating;
            restaurant.Stars = MathHelper.Rating(restaurant.StartsTotal, restaurant.StarsQuantity);
            return await Update(restaurant);
        }

        public async Task<bool> SoftRemove(Guid id)
        {
            var result = await GetById(id);
            if (result == null) return false;
            result.State = DataBaseLayer.Enums.State.Deleted;
            return await Update(result);
        }
    }
}
