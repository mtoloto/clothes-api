
using Clothes.Core.Models.Entities;
using AutoMapper; 

namespace Clothes.AdministratorArea.ViewModels.Mappings
{
    public class NewLaundryViewModelMappingProfile : Profile
    {
        public NewLaundryViewModelMappingProfile()
        {
            CreateMap<NewLaundryViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
            CreateMap<NewLaundryViewModel, Laundry>();
        }
    }
}
