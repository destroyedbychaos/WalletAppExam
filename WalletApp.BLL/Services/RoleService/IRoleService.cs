using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models.Identity;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Services.RoleRepository
{
    public interface IRoleService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetByNameAsync(string name);
        Task<ServiceResponse> DeleteAsync(string id);
        Task<ServiceResponse> CreteAsync(RoleVM model);
        Task<ServiceResponse> UpdateAsync(RoleVM model);
    }
}
