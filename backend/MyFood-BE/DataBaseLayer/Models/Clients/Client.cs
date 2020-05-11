using DataBaseLayer.Models.Commons;
using DataBaseLayer.Models.Restaurants;
using DataBaseLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.Models.Clients
{
    public class Client : CommonsProperty
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
