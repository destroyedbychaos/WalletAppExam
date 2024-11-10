using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;

namespace WalletApp.DAL.Repositories.CardRepository
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetAll();
        Task<Card?> GetByIdAsync(string id);
        Task<Card?> GetByUsernameAsync(string name);
        Task<Card?> GetByCardNumberAsync(string cardNumber);
        Task CreateAsync(Card card);
        Task UpdateAsync(Card card);
        Task DeleteAsync(string id);
        Task SetCurrency(Card card, Currency currency);
        Task ConvertCurrencies(Card card, Currency convertToCurrency);
    }
}
