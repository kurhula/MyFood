using Commons.Helpers;
using DataBaseLayer.Models.Clients;
using DataBaseLayer.Models.Commons;
using DataBaseLayer.Models.Foods;
using DataBaseLayer.Models.Orders;
using DataBaseLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer.Models.Restaurants
{
    public class Restaurant : CommonsProperty
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public long Latitude { get; set; }
        [Required]
        public long Longitude { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Code { get; set; } = StringHelper.GetCode();
        public string Logo { get; set; }
        [Required]
        public string Title { get; set; }
        public decimal Stars { get; set; }
        public int StarsQuantity { get; set; } = 0;
        public int StartsTotal { get; set; }
        public string AppUserId { get; set; }
        public AppUser User { get; set; }
        public virtual IEnumerable<Food> Foods { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
        public virtual IEnumerable<Client> Clients { get; set; }
    }
}
