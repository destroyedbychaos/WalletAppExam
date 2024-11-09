using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp.DAL.ViewModels
{
    public class WalletVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Balance { get; set; }
        public List<CardVM> Cards { get; set; }
        public List<IncomeSourceVM> IncomeSources { get; set; }
        public List<SpendingCategoryVM> SpendingCategories { get; set; }
    }
}
