using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp.DAL.ViewModels
{
    public class WalletIncomeSourceVM
    {
        public WalletVM Wallet { get; set; }
        public IncomeSourceVM IncomeSource { get; set; }
    }
}
