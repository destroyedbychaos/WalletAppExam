using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Data;
using WalletApp.DAL.Models.Identity;
using WalletApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace WalletApp.DAL.Repositories.CardRepository
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        public CardRepository(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public async Task<Card?> GetByIdAsync(string id)
        {
            return await _appDbContext.Cards.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Card?> GetByUsernameAsync(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return await _appDbContext.Cards.FirstOrDefaultAsync(c => c.UserId == user.Id);
        }
        public async Task CreateAsync(Card card)
        {
            await _appDbContext.Cards.AddAsync(card);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Card card)
        {
            _appDbContext.Update(card);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(string id)
        {
            var card = await GetByIdAsync(id);
            if (card != null)
            {
                _appDbContext.Cards.Remove(card);
                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task SetCurrency(Card card, Currency currency)
        {
            card.Currency = currency;
            await _appDbContext.SaveChangesAsync();
        }
        public async Task ConvertCurrencies(Card card, Currency convertToCurrency)
        {
            var balance = (card.Balance / card.Currency.ExchangeRateUSD) * convertToCurrency.ExchangeRateUSD;
            card.Balance = balance;
            card.Currency = convertToCurrency;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
