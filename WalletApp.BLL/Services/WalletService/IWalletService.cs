using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Services.WalletService
{
    public interface IWalletService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetByNameAsync(string name);
        Task<ServiceResponse> CreateAsync(WalletVM model);
        Task<ServiceResponse> UpdateAsync(WalletVM model);
        Task<ServiceResponse> DeleteAsync(string id);
        Task<ServiceResponse> AddCardToWalletAsync(WalletVM walletModel, CardVM cardModel);
    }
}
