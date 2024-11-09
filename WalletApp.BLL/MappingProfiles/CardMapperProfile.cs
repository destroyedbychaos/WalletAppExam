using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.MappingProfiles
{
    public class CardMapperProfile : Profile
    {
        public CardMapperProfile() 
        {
            CreateMap<Card, CardVM>()
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Currency.Code));
            //CreateMap<CardVM, Card>()
                //.ForMember(dest => dest.CurrencyId = _currencyService.GetByCode(src => src.CurrencyCode).Id);
        }

    }
}
