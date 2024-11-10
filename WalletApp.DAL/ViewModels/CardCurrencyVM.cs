using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp.DAL.ViewModels
{
    public class CardCurrencyVM
    {
        public CardVM Card { get; set; }
        public CurrencyVM Currency { get; set; }
    }
}
