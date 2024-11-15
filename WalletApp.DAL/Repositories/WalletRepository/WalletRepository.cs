﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Data;
using WalletApp.DAL.Models;
using WalletApp.DAL.Models.Identity;

namespace WalletApp.DAL.Repositories.WalletRepository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        public WalletRepository(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;

        }
        public async Task<List<Wallet?>> GetAllAsync()
        {
            return await _appDbContext.Wallets.ToListAsync();
        }
        public async Task<Wallet?> GetByIdAsync(string id)
        {
            return await _appDbContext.Wallets.FirstOrDefaultAsync(w => w.UserId == id);
        }

        public async Task<Wallet?> GetByNameAsync(string name)
        {
            User user = await _userManager.FindByNameAsync(name);
            return await _appDbContext.Wallets.FirstOrDefaultAsync(w => w.UserId == user.Id);
        }
        public async Task CreateAsync(Wallet wallet)
        {
            await _appDbContext.Wallets.AddAsync(wallet);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Wallet wallet)
        {
            _appDbContext.Wallets.Update(wallet);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(string id)
        {
            var wallet = await GetByIdAsync(id);
            if (wallet != null)
            {
                _appDbContext.Wallets.Remove(wallet);
                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task AddCardToWalletAsync(Wallet wallet, Card card)
        {
            wallet.Cards.Add(card);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteCardFromWalletAsync(Wallet wallet, Card card)
        {
            wallet.Cards.Remove(card);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task AddIncomeSourceToWalletAsync(Wallet wallet, IncomeSource incomeSource)
        {
            wallet.IncomeSources.Add(incomeSource);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteIncomeSourceFromWalletAsync(Wallet wallet, IncomeSource incomeSource)
        {
            wallet.IncomeSources.Remove(incomeSource);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task AddSpendingCategoryToWalletAsync(Wallet wallet, SpendingCategory spendingCategory)
        {
            wallet.SpendingCategories.Add(spendingCategory);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteSpendingCategoryFromWalletAsync(Wallet wallet, SpendingCategory spendingCategory)
        {
            wallet.SpendingCategories.Remove(spendingCategory);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
