using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models.Identity;

namespace WalletApp.DAL.Repositories.RoleRepository
{
    public interface IRoleRepository
    {
        Task<Role?> GetAsync(Expression<Func<Role, bool>> predicate);
        Task<Role?> GetByIdAsync(string id);
        Task<Role?> GetByNameAsync(string name);
        Task<List<Role>> GetAllAsync();
        Task<IdentityResult> UpdateAsync(Role model);
        Task<IdentityResult> CreateAsync(Role model);
        Task<IdentityResult> DeleteAsync(Role model);
        Task<bool> IsUniqueNameAsync(string name);
    }
}
