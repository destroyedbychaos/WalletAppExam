using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Data;
using WalletApp.DAL.Models;

namespace WalletApp.DAL.Repositories.SpendingCategoryRepository
{
    public class SpendingCategoryRepository : ISpendingCategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public SpendingCategoryRepository(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }
        public async Task<SpendingCategory?> GetByIdAsync(string id)
        {
            return await _appDbContext.SpendingCategories.FirstOrDefaultAsync(sc => sc.Id == id);
        }
        public async Task<SpendingCategory?> GetByNameAsync(string name)
        {
            return await _appDbContext.SpendingCategories.FirstOrDefaultAsync(sc => sc.Name  == name);
        }
        public async Task CreateAsync(SpendingCategory spendingCategory)
        {
            await _appDbContext.SpendingCategories.AddAsync(spendingCategory);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(SpendingCategory spendingCategory)
        {
            _appDbContext.SpendingCategories.Update(spendingCategory);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(string id)
        {
            var spendingCategory = await GetByIdAsync(id);
            if (spendingCategory != null)
            {
                _appDbContext.SpendingCategories.Remove(spendingCategory);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
