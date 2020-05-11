using AutoMapper;
using DataBaseLayer.Models.Users;
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
        }
    }
}
