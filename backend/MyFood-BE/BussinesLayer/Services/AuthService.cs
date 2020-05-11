using BussinesLayer.Interfaces;
using DataBaseLayer.Models.Users;
using DataBaseLayer.Options;
using DataBaseLayer.Persistence;
using DataBaseLayer.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services
{
    public class AuthService : IAuthService
    {
        public readonly ApplicationDbContext _dbContext;
        private readonly JwtConfig _options;
        private readonly UserManager<AppUser> _userManager;
        public AuthService(ApplicationDbContext dbContext, UserManager<AppUser> userManager, IOptions<JwtConfig> options)
        {
            _dbContext = dbContext;
            _options = options.Value;
            _userManager = userManager;
        }

        public async Task<string> BuildToken(LoginUserVm user)
        {
            var userResult = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName.Equals(user.UserName));
            var rols = await _userManager.GetRolesAsync(userResult);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,rols[0])
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: _options.ValidIssuer,
               audience: _options.ValidAudience,
               claims: claims,
               signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
