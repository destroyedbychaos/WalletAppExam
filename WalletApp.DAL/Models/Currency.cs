using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp.DAL.Models
{
    public class Currency
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal ExchangeRateUSD { get; set; } //1 USD == ?? currency

        public ICollection<Card> Cards { get; set; }
    }
}
