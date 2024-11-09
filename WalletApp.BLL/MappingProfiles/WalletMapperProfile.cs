using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.MappingProfiles
{
    public class WalletMapperProfile : Profile
    {
        public WalletMapperProfile()
        {
            CreateMap<Wallet, WalletVM>();

            CreateMap<WalletVM, Wallet>();
        }
    }
}
