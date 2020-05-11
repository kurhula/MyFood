using DataBaseLayer.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFood_BE.Extensions;
using MyFood_BE.Seeds;

namespace MyFood_BE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptionsConfiguration(Configuration);
            services.ConfigureDbContext(Configuration);
            services.ConfigureCords();
            services.ClaimsAuth();
            services.AuthoMapperConfiguration();
            services.ImplementJWT(Configuration);
            services.ImplementCustomIdentity();
            services.ServiceImplementations();
            services.AddDocApi(Configuration);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            DataSeedMethods.SeedRolesAndUsers(app);
            var section = Configuration.GetSection(nameof(SwaggerConfig));
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseCors(nameof(Startup));
            app.UseSwagger(x => x.RouteTemplate = section[nameof(SwaggerConfig.RouteTemplate)]);
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint(section[nameof(SwaggerConfig.RouteDev)], section[nameof(SwaggerConfig.Title)]);
                x.RoutePrefix = section[nameof(SwaggerConfig.Prefix)];
            });
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
