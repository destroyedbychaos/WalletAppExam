using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp.DAL.ViewModels
{
    public class CardVM
    {
        public string Id { get; set; }
        [MaxLength(16)]
        public string CardNumber { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyCode { get; set; }
    }
}
