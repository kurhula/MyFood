using AutoMapper;
using AutoMapper.QueryableExtensions;
using BussinesLayer.Interfaces;
using BussinesLayer.Repositories.Implementations;
using DataBaseLayer.Enums.Auth;
using DataBaseLayer.Models.Restaurants;
using DataBaseLayer.Models.Users;
using DataBaseLayer.Options;
using DataBaseLayer.Persistence;
using DataBaseLayer.ViewModels.Pagination;
using DataBaseLayer.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services
{
    public class UserService : BaseService<AppUser, ApplicationDbContext>, IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RestaurantConfig _options;
        public UserService(ApplicationDbContext context, UserManager<AppUser> userManager , IMapper mapper , IOptions<RestaurantConfig> options) : base(context)
        {
            _dbContext = context;
            _userManager = userManager;
            _mapper = mapper;
            _options = options.Value;
        }

        public async Task<bool> CreateUser(CreateUserVM model)
        {
            var user = new AppUser
            {
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email,
                Email = model.Email
            };
            await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, model.Rol == AuthLevel.Restaurant ? nameof(AuthLevel.Restaurant) : nameof(AuthLevel.User));
            if(model.Rol == AuthLevel.Restaurant)
            {
                _options.AppUserId = user.Id;
                var restaurant = _mapper.Map<Restaurant>(_options);
                await _dbContext.Restaurants.AddAsync(restaurant);
            }
            return await CommitAsync();
        }

        public async Task<ICollection<AppUser>> GetAll(FilterUserVM filters)
        {
            var result = GetAllQueryable();
            if (!string.IsNullOrEmpty(filters.FullName)) result = GetAllQueryable(x => x.FullName.Contains(filters.FullName));
            if (!string.IsNullOrEmpty(filters.UserName)) result = result.Where(x => x.UserName.Contains(filters.UserName));
            return await result.ToListAsync();
        }

        public async Task<Pagination<UserVM>> GetAllPaginated(FilterUserVM filters = null)
        {
            var result = GetAllQueryable();
            if (!string.IsNullOrEmpty(filters.FullName)) result = GetAllQueryable(x => x.FullName.Contains(filters.FullName));
            if (!string.IsNullOrEmpty(filters.UserName)) result = result.Where(x => x.UserName.Contains(filters.UserName));

            var total = await result.CountAsync();
            var pages = total / filters.QuantityByPage;

            return new Pagination<UserVM>
            {
                ActualPage = filters.Page,
                Pages = pages,
                Total = total,
                Entities = await result.ProjectTo<UserVM>(_mapper.ConfigurationProvider).Take(filters.QuantityByPage).Skip((filters.Page - 1) * filters.QuantityByPage).ToListAsync()
            };

        }

        public async Task<AppUser> GetByEmail(string email) => await _userManager.FindByEmailAsync(email);

        public async Task<AppUser> GetUser(string id) => await _userManager.FindByIdAsync(id);

        public async Task<bool> SoftDelete(string id)
        {
            var user = await GetUser(id);
            user.State = DataBaseLayer.Enums.State.Deleted;
            return await Update(user);
        }

        public override async Task<bool> Update(AppUser model)
        {
            var user = await GetUser(model.Id);
            user.FullName = model.FullName;
            user.PhoneNumber = model.PhoneNumber;
            user.State = model.State;
            user.Avatar = model.Avatar;
            _dbContext.Users.Update(user);
            return await CommitAsync();
        }





    }
}
