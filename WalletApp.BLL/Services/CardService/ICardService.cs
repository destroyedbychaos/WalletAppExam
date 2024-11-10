using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Services.CardService
{
    public interface ICardService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetByUsernameAsync(string name);
        Task<ServiceResponse> GetByCardNumberAsync(int cardNumber);
        Task<ServiceResponse> CreateAsync(CardVM model);
        Task<ServiceResponse> UpdateAsync(CardVM model);
        Task<ServiceResponse> DeleteAsync(string id);
        Task<ServiceResponse> SetCurrency(CardVM model, CurrencyVM currency);
        Task<ServiceResponse> ConvertCurrencies(CardVM card, CurrencyVM convertToCurrency);
    }
}
