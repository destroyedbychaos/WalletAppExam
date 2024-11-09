using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Data;
using WalletApp.DAL.Models;

namespace WalletApp.DAL.Repositories.IncomeSourceRepository
{
    public class IncomeSourceRepository : IIncomeSourceRepository
    {
        private readonly AppDbContext _appDbContext;
        public IncomeSourceRepository(AppDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }
        public async Task<IncomeSource?> GetByIdAsync(string id)
        {
            return await _appDbContext.IncomeSources.FirstOrDefaultAsync(inc => inc.Id == id);
        }
        public async Task<IncomeSource?> GetByNameAsync(string name)
        {
            return await _appDbContext.IncomeSources.FirstOrDefaultAsync(inc => inc.Name == name);
        }
        public async Task CreateAsync(IncomeSource incomeSource)
        {
            await _appDbContext.IncomeSources.AddAsync(incomeSource);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(IncomeSource incomeSource)
        {
            _appDbContext.IncomeSources.Update(incomeSource);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(string id)
        {
            var incomeSource = await GetByIdAsync(id);
            if (incomeSource != null)
            {
                _appDbContext.IncomeSources.Remove(incomeSource);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
