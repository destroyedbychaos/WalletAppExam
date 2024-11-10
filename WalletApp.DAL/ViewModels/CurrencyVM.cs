using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;

namespace WalletApp.DAL.ViewModels
{
    public class CurrencyVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal ExchangeRateUSD { get; set; } //1 USD == ?? currency
    }
}
