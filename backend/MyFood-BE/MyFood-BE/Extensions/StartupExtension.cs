using AutoMapper;
using BussinesLayer.UnitOfWork;
using DataBaseLayer.Enums.Auth;
using DataBaseLayer.Mappings;
using DataBaseLayer.Models.Users;
using DataBaseLayer.Options;
using DataBaseLayer.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
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

        public static void ServiceImplementations(this IServiceCollection services) => services.AddTransient<IUnitOfWork, UnitOfWork>();

        public static void AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)));
        }

        public static void AddDocApi(this IServiceCollection services, IConfiguration configuration)
        {
            var result = configuration.GetSection(nameof(SwaggerConfig));
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(result[nameof(SwaggerConfig.Version)],
                    new OpenApiInfo
                    {
                        Title = "Api Doc",
                        Version = result[nameof(SwaggerConfig.Version)]
                    });
            });
        }

        public static void AuthoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public static void ClaimsAuth(this IServiceCollection services)
            => services.AddAuthorization(x => x.AddPolicy("Rol", pol => pol.RequireRole(nameof(AuthLevel.Admin), nameof(AuthLevel.Restaurant), nameof(AuthLevel.User))));

    }
}
