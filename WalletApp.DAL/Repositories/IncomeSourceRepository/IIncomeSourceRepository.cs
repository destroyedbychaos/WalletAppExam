using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;

namespace WalletApp.DAL.Repositories.IncomeSourceRepository
{
    public interface IIncomeSourceRepository
    {
        Task<List<IncomeSource?>> GetAllAsync();
        Task<IncomeSource?> GetByIdAsync(string id);
        Task<IncomeSource?> GetByNameAsync(string name);
        Task CreateAsync(IncomeSource incomeSource);
        Task UpdateAsync(IncomeSource incomeSource);
        Task DeleteAsync(string id);
    }
}
