using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models.Identity;

namespace WalletApp.DAL.Models
{
    public class Card
    {
        public string Id { get; set; }

        [MaxLength(16)]
        public int CardNumber { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyCode { get; set; }
        public string WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public string CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
