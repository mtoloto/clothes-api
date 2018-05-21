
using Clothes.Core.Models.Entities;
using AutoMapper; 

namespace Clothes.AdministratorArea.ViewModels.Mappings
{
    public class UpdateLaundryViewModelMappingProfile : Profile
    {
        public UpdateLaundryViewModelMappingProfile()
        {
            CreateMap<UpdateLaundryViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
            CreateMap<UpdateLaundryViewModel, Laundry>();
        }
    }
}
