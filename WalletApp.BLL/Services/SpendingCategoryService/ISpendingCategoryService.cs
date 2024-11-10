using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Services.SpendingCategoryService
{
    public interface ISpendingCategoryService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetByNameAsync(string name);
        Task<ServiceResponse> CreateAsync(SpendingCategoryVM model);
        Task<ServiceResponse> UpdateAsync(SpendingCategoryVM model);
        Task<ServiceResponse> DeleteAsync(string id);
    }
}
