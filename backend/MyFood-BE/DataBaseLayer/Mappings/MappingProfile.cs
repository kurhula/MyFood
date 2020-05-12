using AutoMapper;
using DataBaseLayer.Models.Restaurants;
using DataBaseLayer.Models.Users;
using DataBaseLayer.Options;
using DataBaseLayer.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UserVM>().ReverseMap();
            CreateMap<Restaurant, RestaurantConfig>().ReverseMap();

        }
    }
}
