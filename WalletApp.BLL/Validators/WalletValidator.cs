using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Validators
{
    public class WalletValidator : AbstractValidator<WalletVM>
    {
        public WalletValidator()
        {
            RuleFor(w => w.Name)
                .NotEmpty().WithMessage("Name of the wallet cannot be empty");
            RuleFor(w => w.Id)
                .NotEmpty().WithMessage("Id of the wallet cannot be empty");
        }
    }
}
