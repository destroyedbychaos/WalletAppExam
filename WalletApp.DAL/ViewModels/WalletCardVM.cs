using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp.DAL.ViewModels
{
    public class WalletCardVM
    {
        public WalletVM Wallet { get; set; }
        public CardVM Card { get; set; }
    }
}
