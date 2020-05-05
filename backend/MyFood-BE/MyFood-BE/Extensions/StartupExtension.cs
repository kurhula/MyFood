using DataBaseLayer.Models.Users;
using DataBaseLayer.Options;
using DataBaseLayer.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFood_BE.Extensions
{
    public static class StartupExtension
    {
        public static void ConfigureCords(this IServiceCollection services)
          => services.AddCors(options =>
          {
              options.AddPolicy(nameof(Startup),
                    builder => builder
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowAnyOrigin());
          });

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        public static void ImplementCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                      .AddEntityFrameworkStores<ApplicationDbContext>()
                      .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = true;
            });
        }

        public static void ImplementJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = false,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = configuration.GetSection(nameof(JwtConfig))[nameof(JwtConfig.ValidIssuer)],
                   ValidAudience = configuration.GetSection(nameof(JwtConfig))[nameof(JwtConfig.ValidAudience)],
                   IssuerSigningKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(configuration.GetSection(nameof(JwtConfig))[nameof(JwtConfig.SecretKey)])),
                   ClockSkew = TimeSpan.Zero
               });
        }


    }
}
