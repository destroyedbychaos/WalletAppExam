using FluentValidation;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Validators
{
    public class SpendingCategoryValidator : AbstractValidator<SpendingCategoryVM>
    {
        public SpendingCategoryValidator()
        {
            RuleFor(sp => sp.Id)
                .NotEmpty().WithMessage("The id of the spending category cannot be empty");
            RuleFor(sp => sp.Name)
                .NotEmpty().WithMessage("The name of the spending category cnanot be empty");
        }
    }
}
