using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.Options
{
    public class RestaurantConfig
    {
        public string AppUserId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
    }
}
