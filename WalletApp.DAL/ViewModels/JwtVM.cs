using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp.DAL.ViewModels
{
    public class JwtVM
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
