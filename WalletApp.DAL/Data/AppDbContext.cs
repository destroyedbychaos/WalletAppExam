using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WalletApp.DAL.Models;
using WalletApp.DAL.Models.Identity;

namespace WalletApp.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<IncomeSource> IncomeSources { get; set; }
        public DbSet<SpendingCategory> SpendingCategories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Role>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            builder.Entity<Wallet>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Name).IsRequired().HasMaxLength(50);
                entity.Property(w => w.Balance).HasColumnType("decimal(18,2)");

                entity.HasOne(w => w.User)
                      .WithMany(u => u.Wallets)
                      .HasForeignKey(e => e.UserId);
            });

            builder.Entity<Card>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.CardNumber).HasMaxLength(16);
                entity.Property(c => c.ExpirationDate).HasColumnType("date");
                entity.Property(c => c.Currency).IsRequired().HasMaxLength(3);
                entity.Property(c => c.Balance).HasColumnType("decimal(18,2)");

                entity.HasOne(c => c.Wallet)
                      .WithMany(w => w.Cards)
                      .HasForeignKey(c => c.WalletId);
                entity.HasOne(c => c.Currency)
                      .WithMany(cur => cur.Cards)
                      .HasForeignKey(c => c.CurrencyId);
                entity.HasOne(c => c.User)
                    .WithMany(u => u.Cards)
                    .HasForeignKey(c => c.UserId);
            });

            builder.Entity<IncomeSource>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(50);

                entity.HasOne(s => s.Wallet)
                      .WithMany(w => w.IncomeSources)
                      .HasForeignKey(s => s.WalletId);
            });

            builder.Entity<SpendingCategory>(entity =>
            {
                entity.HasKey(sc => sc.Id);
                entity.Property(sc => sc.Name).IsRequired().HasMaxLength(50);

                entity.HasOne(sc => sc.Wallet)
                      .WithMany(w => w.SpendingCategories)
                      .HasForeignKey(sc => sc.WalletId);
            });

            builder.Entity<Currency>(entity =>
            {
                entity.HasKey(cur => cur.Id);
                entity.Property(cur => cur.Name).IsRequired().HasMaxLength(50);
                entity.Property(cur => cur.Code).IsRequired().HasMaxLength(3);
                entity.Property(e => e.ExchangeRateUSD).HasColumnType("decimal(18,6)");


                entity.HasMany(cur => cur.Cards)
                      .WithOne(c => c.Currency)
                      .HasForeignKey(cur => cur.CurrencyId);
            });
        }
    }
}
