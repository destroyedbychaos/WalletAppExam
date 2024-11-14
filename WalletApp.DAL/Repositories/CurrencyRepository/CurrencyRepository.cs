using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Data;
using WalletApp.DAL.Models;
using WalletApp.DAL.Models.Identity;

namespace WalletApp.DAL.Repositories.CurrencyRepository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _appDbContext;
        public CurrencyRepository(AppDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }
        public async Task<List<Currency?>?> GetAllAsync()
        {
            return await _appDbContext.Currencies.ToListAsync();
        }
        public async Task<Currency?> GetByIdAsync(string id)
        {
            return await _appDbContext.Currencies.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Currency?> GetByCodeAsync(string code)
        {
            return await _appDbContext.Currencies.FirstOrDefaultAsync(c => c.Code == code);
        }
        public async Task CreateAsync(Currency currency)
        {
            await _appDbContext.Currencies.AddAsync(currency);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Currency currency)
        {
            _appDbContext.Currencies.Update(currency);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(string id)
        {
            var currency = await GetByIdAsync(id);
            if (currency != null)
            {
                _appDbContext.Currencies.Remove(currency);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
