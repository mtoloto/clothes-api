﻿
using Clothes.Core.Models.Entities;
using AutoMapper;


namespace Clothes.ViewModels.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email)); 
        }
    }
}
