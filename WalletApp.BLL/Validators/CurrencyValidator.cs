using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Validators
{
    public class CurrencyValidator : AbstractValidator<CurrencyVM>
    {
        public CurrencyValidator() 
        {
            RuleFor(cur => cur.Id)
                .NotEmpty().WithMessage("Id of the currency cannot be empty");
            RuleFor(cur => cur.Name)
                .NotEmpty().WithMessage("Name of the currency cannot be empty");
            RuleFor(cur => cur.Code)
                .NotEmpty().WithMessage("Currency must have a code")
                .GreaterThan("0.0");
        }
    }
}
