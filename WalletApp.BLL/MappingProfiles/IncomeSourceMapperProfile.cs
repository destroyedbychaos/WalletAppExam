using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.MappingProfiles
{
    public class IncomeSourceMapperProfile : Profile
    {
        public IncomeSourceMapperProfile() 
        {
            CreateMap<IncomeSource, IncomeSourceVM>();

            CreateMap<IncomeSourceVM, IncomeSource>();
        }
    }
}
