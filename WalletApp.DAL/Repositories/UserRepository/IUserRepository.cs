﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models.Identity;

namespace WalletApp.DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string id, bool includes = false);
        Task<User?> GetByEmailAsync(string email, bool includes = false);
        Task<User?> GetByUsernameAsync(string userName, bool includes = false);
        Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate, bool includes = false);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<bool> IsUniqueEmailAsync(string email);
        Task<bool> IsUniqueUserNameAsync(string userName);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<List<User>> GetAllAsync();
        Task<IdentityResult> UpdateAsync(User model);
        Task<IdentityResult> DeleteAsync(User model);
        Task<IdentityResult> AddToRoleAsync(User model, string role);
    }
}
