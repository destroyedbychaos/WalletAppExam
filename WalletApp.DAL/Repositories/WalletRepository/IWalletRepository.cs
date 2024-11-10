using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;

namespace WalletApp.DAL.Repositories.WalletRepository
{
    public interface IWalletRepository
    {
        Task<List<Wallet?>> GetAllAsync();
        Task<Wallet?> GetByIdAsync(string id);
        Task<Wallet?> GetByNameAsync(string name);
        Task CreateAsync(Wallet wallet);
        Task UpdateAsync(Wallet wallet);
        Task DeleteAsync(string id);
        Task AddCardToWalletAsync(Wallet wallet, Card card);
        Task DeleteCardFromWalletAsync(Wallet wallet, Card card);
        Task AddIncomeSourceToWalletAsync(Wallet wallet, IncomeSource incomeSource);
        Task DeleteIncomeSourceFromWalletAsync(Wallet wallet, IncomeSource incomeSource);
        Task AddSpendingCategoryToWalletAsync(Wallet wallet, SpendingCategory spendingCategory);
        Task DeleteSpendingCategoryFromWalletAsync(Wallet wallet, SpendingCategory spendingCategory);
    }
}
