using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetByEmailAsync(string email);
        Task<ServiceResponse> GetByUserNameAsync(string userName);
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> DeleteAsync(string id);
        Task<ServiceResponse> CreateAsync(CreateUpdateUserVM model);
        Task<ServiceResponse> UpdateAsync(CreateUpdateUserVM model);
        Task<ServiceResponse> AddWalletToUserAsync(UserVM userModel, WalletVM walletModel);
        Task<ServiceResponse> DeleteWalletFromUserAsync(UserVM userModel, WalletVM walletModel);
    }
}
