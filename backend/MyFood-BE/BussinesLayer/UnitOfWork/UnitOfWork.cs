using BussinesLayer.Interfaces;
using BussinesLayer.Services;
using DataBaseLayer.Models.Users;
using DataBaseLayer.Options;
using DataBaseLayer.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BussinesLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IOptions<JwtConfig> _options;
        private readonly AuthService _authService;
        private readonly UserManager<AppUser> _userManager;

        public UnitOfWork(ApplicationDbContext dbContext, UserManager<AppUser> userManager, IOptions<JwtConfig> options)
        {
            _options = options;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IAuthService AuthService => _authService ?? (new AuthService(_dbContext, _userManager, _options));
    }
}
