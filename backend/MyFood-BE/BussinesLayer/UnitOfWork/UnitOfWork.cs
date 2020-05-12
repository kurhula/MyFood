using AutoMapper;
using BussinesLayer.Interfaces;
using BussinesLayer.Services;
using DataBaseLayer.Models.Users;
using DataBaseLayer.Options;
using DataBaseLayer.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace BussinesLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AuthService _authService;
        private readonly UserManager<AppUser> _userManager;
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly RestaurantService _restaurantService;

        #region Options
        private readonly IOptions<JwtConfig> _jwtOptions;
        private readonly IOptions<RestaurantConfig> _restaurantOptions;
        #endregion

        public UnitOfWork(ApplicationDbContext dbContext, UserManager<AppUser> userManager, IMapper mapper,
            IOptions<JwtConfig> jwtOptions,
            IOptions<RestaurantConfig> restaurantOptions)
        {
            _jwtOptions = jwtOptions;
            _restaurantOptions = restaurantOptions;
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IAuthService AuthService => _authService ?? (new AuthService(_dbContext, _userManager, _jwtOptions));

        public IUserService UserService => _userService ?? (new UserService(_dbContext, _userManager, _mapper, _restaurantOptions));

        public IRestaurantService RestaurantService => _restaurantService ?? (new RestaurantService(_dbContext));
    }
}
