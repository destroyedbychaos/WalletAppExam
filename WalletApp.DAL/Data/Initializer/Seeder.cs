using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.Models.Identity;
using WalletApp.DAL.Repositories.CardRepository;
using WalletApp.DAL.Repositories.CurrencyRepository;
using WalletApp.DAL.Repositories.IncomeSourceRepository;
using WalletApp.DAL.Repositories.SpendingCategoryRepository;
using WalletApp.DAL.Repositories.WalletRepository;

namespace WalletApp.DAL.Data.Initializer
{
    public static class Seeder
    {
        public static async void SeedData(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var walletManager = scope.ServiceProvider.GetRequiredService<WalletRepository>();
                var cardManager = scope.ServiceProvider.GetRequiredService<CardRepository>();
                var incomeSourceManager = scope.ServiceProvider.GetRequiredService<IncomeSourceRepository>();
                var spendingCategoryManager = scope.ServiceProvider.GetRequiredService<SpendingCategoryRepository>();
                var currencyManager = scope.ServiceProvider.GetRequiredService<CurrencyRepository>();


                if (!roleManager.Roles.Any())
                {
                    var adminRole = new Role
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = Settings.AdminRole
                    };

                    var userRole = new Role
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = Settings.UserRole
                    };

                    await roleManager.CreateAsync(adminRole);
                    await roleManager.CreateAsync(userRole);
                }

                if (!userManager.Users.Any())
                {
                    var admin = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "admin@gmail.com",
                        UserName = "admin",
                        EmailConfirmed = true,
                        FirstName = "Admin",
                        LastName = "Dashboard"
                    };

                    var user = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "user@gmail.com",
                        UserName = "user",
                        EmailConfirmed = true,
                        FirstName = "User",
                        LastName = "Dashboard"
                    };
                }

                if(currencyManager.GetAllAsync == null)
                {
                    var currency = new Currency
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Hryvnia",
                        Code = "UAH",
                        ExchangeRateUSD = 0.025m
                    };
                }


                if(walletManager.GetAllAsync == null)
                {
                    var card = new Card
                    {
                        Id = Guid.NewGuid().ToString(),
                        //CurrencyId = 
                        //CurrencyCode = 
                        //UserId = 
                        //CardNumber = int.TryParse(1234567812345678, out int CardNumber),
                        Balance = 0.00m
                    };
                    var wallet = new Wallet
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "admin wallet",
                        Balance = 0.00m,
                        Cards = new Card[] { card },
                        //UserId =
                    };
                }
                
            }
        }
    }
}
