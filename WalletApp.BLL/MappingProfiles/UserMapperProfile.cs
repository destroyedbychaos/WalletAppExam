using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models.Identity;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.MappingProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile() 
        {
            CreateMap<User, UserVM>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(
                    src => src.UserRoles.Count > 0 ? src.UserRoles.First().Role.Name : "no role"))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Image) ? "avatar.png" : src.Image));

            CreateMap<CreateUpdateUserVM, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }

    }
}
