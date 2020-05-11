using Commons.Helpers;
using DataBaseLayer.Models.Commons;
using DataBaseLayer.Models.Foods;
using DataBaseLayer.Models.Restaurants;
using DataBaseLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer.Models.Orders
{
    public class Order : CommonsProperty
    {
        [Required]
        public string Code { get; set; } = StringHelper.GetCode(5);
        [Required]
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid FoodId { get; set; }
        public Food Food { get; set; }
        [Required]
        public int TableNumber { get; set; }
        [Required]
        public DateTime RemainingTime { get; set; }
        public decimal Stars { get; set; }
    }
}
