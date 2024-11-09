using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models.Identity;

namespace WalletApp.DAL.Models
{
    public class Wallet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Card> Cards { get; set; }
        public ICollection<IncomeSource> IncomeSources { get; set; } 
        public ICollection<SpendingCategory> SpendingCategories { get; set; }
    }
}
