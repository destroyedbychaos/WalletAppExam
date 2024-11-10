using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Services.IncomeSourceService
{
    public interface IIncomeSourceService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetByNameAsync(string name);
        Task<ServiceResponse> CreateAsync(IncomeSourceVM incomeSource);
        Task<ServiceResponse> UpdateAsync(IncomeSourceVM incomeSource);
        Task<ServiceResponse> DeleteAsync(string id);
    }
}
