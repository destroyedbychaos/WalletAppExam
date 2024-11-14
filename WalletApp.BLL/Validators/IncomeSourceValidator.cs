using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.ViewModels;

namespace WalletApp.BLL.Validators
{
    public class IncomeSourceValidator : AbstractValidator<IncomeSourceVM>
    {
        public IncomeSourceValidator() 
        {
            RuleFor(inc => inc.Id)
                .NotEmpty().WithMessage("The id of the income cannot be empty");

            RuleFor(inc => inc.Name)
                .NotEmpty().WithMessage("The name of the income source cannot be empty");
        }
    }
}
