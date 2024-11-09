using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;

namespace WalletApp.DAL.Repositories.CurrencyRepository
{
    public interface ICurrencyRepository
    {
        Task<Currency?> GetByIdAsync(string id);
        Task<Currency?> GetByCodeAsync(string code);
        Task CreateAsync(Currency currency);
        Task UpdateAsync(Currency currency);
        Task DeleteAsync(string id);
    }
}
