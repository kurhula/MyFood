using DataBaseLayer.Enums.Auth;
using DataBaseLayer.Models.Users;
using DataBaseLayer.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFood_BE.Seeds
{
    public class DataSeedMethods
    {
        public static void SeedRolesAndUsers(IApplicationBuilder app)
        {
            using var appScoped = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            ApplicationDbContext _dbContext = appScoped.ServiceProvider.GetService<ApplicationDbContext>();
            UserManager<AppUser> _userManager = appScoped.ServiceProvider.GetService<UserManager<AppUser>>();
            if (!_dbContext.Roles.Any())
            {
                var roleList = new List<IdentityRole> {
                            new IdentityRole { Name = nameof(AuthLevel.Admin) , NormalizedName = nameof(AuthLevel.Admin) },
                            new IdentityRole { Name = nameof(AuthLevel.Restaurant) , NormalizedName = nameof(AuthLevel.Restaurant) },
                            new IdentityRole { Name = nameof(AuthLevel.User) , NormalizedName = nameof(AuthLevel.User) }};
                _dbContext.Roles.AddRange(roleList);
                _dbContext.SaveChanges();
            }

            SeedUsers(_dbContext, _userManager);
        }

        private static void SeedUsers(ApplicationDbContext _dbContext, UserManager<AppUser> _userManager)
        {

            if (!_dbContext.Users.Any())
            {
                var user = new AppUser
                {
                    CreatedAt = DateTime.Now,
                    UserName = "admin@admin.com",
                    NormalizedUserName = nameof(AuthLevel.Admin),
                    EmailConfirmed = true,
                    Email = "admin@admin.com",
                    LockoutEnabled = false,
                    PhoneNumber = "000-000-0000",
                    FullName = nameof(AuthLevel.Admin),
                    NormalizedEmail = "admin@admin.com",
                };

                var result = _userManager.CreateAsync(user, "admin1234");
                var r = user;
                _dbContext.Users.Add(r);
                var saved = _dbContext.SaveChanges() > 0;
                if (saved)
                {
                    var role = _dbContext.Roles.FirstOrDefault(x => x.Name.Equals(nameof(AuthLevel.Admin)));
                    _dbContext.UserRoles.Add(new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id });
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
