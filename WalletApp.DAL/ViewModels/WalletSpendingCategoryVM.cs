using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp.DAL.ViewModels
{
    public class WalletSpendingCategoryVM
    {
        public WalletVM Wallet { get; set; }
        public SpendingCategoryVM SpendingCategory { get; set; }
    }
}
