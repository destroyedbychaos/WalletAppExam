using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Repositories.SpendingCategoryRepository;
using WalletApp.DAL.ViewModels;
using WalletApp.DAL.Models;

namespace WalletApp.BLL.MappingProfiles
{
    public class SpendingCategoryMapperProfile : Profile
    {
        public SpendingCategoryMapperProfile() 
        {
            CreateMap<SpendingCategory, SpendingCategoryVM>();

            CreateMap<SpendingCategoryVM, SpendingCategory>();
        }
    }
}
