using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp.DAL.Models
{
    public class IncomeSource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
