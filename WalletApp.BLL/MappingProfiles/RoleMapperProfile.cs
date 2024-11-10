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
    public class RoleMapperProfile : Profile
    {
        public RoleMapperProfile()
        {
            // Role -> RoleVM
            CreateMap<Role, RoleVM>();

            // RoleVM -> Role
            CreateMap<RoleVM, Role>();
        }
    }
}
