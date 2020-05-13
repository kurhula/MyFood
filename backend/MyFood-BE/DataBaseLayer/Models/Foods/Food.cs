using Commons.Helpers;
using DataBaseLayer.Models.Commons;
using DataBaseLayer.Models.Restaurants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer.Models.Foods
{
    public class Food : CommonsProperty
    {
        [Required]
        public string Code { get; set; } = StringHelper.GetCode(5);
        public decimal Stars { get; set; }
        public int StarsQuantity { get; set; }
        public int StartsTotal { get; set; }


        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Pic { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
