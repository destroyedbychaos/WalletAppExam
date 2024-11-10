using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;

namespace WalletApp.DAL.Repositories.SpendingCategoryRepository
{
    public interface ISpendingCategoryRepository
    {
        Task<List<SpendingCategory?>> GetAll();
        Task<SpendingCategory?> GetByIdAsync(string id);
        Task<SpendingCategory?> GetByNameAsync(string name);
        Task CreateAsync(SpendingCategory spendingCategory);
        Task UpdateAsync(SpendingCategory spendingCategory);
        Task DeleteAsync(string id);
    }
}
