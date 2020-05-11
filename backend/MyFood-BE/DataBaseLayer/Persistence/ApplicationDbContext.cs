using DataBaseLayer.Enums;
using DataBaseLayer.Models.Clients;
using DataBaseLayer.Models.Foods;
using DataBaseLayer.Models.Orders;
using DataBaseLayer.Models.Restaurants;
using DataBaseLayer.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Order>()
              .HasOne(c => c.Restaurant)
              .WithMany(x => x.Orders).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Food>().HasQueryFilter(x => x.State != State.Deleted);
            builder.Entity<Restaurant>().HasQueryFilter(x => x.State != State.Deleted);
            builder.Entity<Order>().HasQueryFilter(x => x.State != State.Deleted);
            builder.Entity<Client>().HasQueryFilter(x => x.State != State.Deleted);

        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }


    }
}
