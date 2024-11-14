using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Validators
{
    public class CardValidator : AbstractValidator<CardVM>
    {
        public CardValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("The id of the card cannot be empty");
            RuleFor(c => c.CardNumber)
                .NotEmpty().WithMessage("The card number cnanot be empty");
            RuleFor(c => c.CurrencyCode)
                .NotEmpty().WithMessage("The card must have a currency code assigned");
            RuleFor(c => c.ExpirationDate.ToString())
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date.ToString());
        }
    }
}
