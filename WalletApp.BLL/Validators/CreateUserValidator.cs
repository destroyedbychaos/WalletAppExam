using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.ViewModels;
using WalletApp.DAL;

namespace WalletApp.BLL.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUpdateUserVM>
    {
        public CreateUserValidator()
        {
            RuleFor(m => m.Email)
                .EmailAddress().WithMessage("Wrong email format")
                .NotEmpty().WithMessage("Please specify your email");
            RuleFor(m => m.UserName)
                .NotEmpty().WithMessage("Please specify user name");
            RuleFor(m => m.Password)
                .MinimumLength(Settings.PasswordLength).WithMessage("Minimum password length is 6 characters");
            RuleFor(m => m.Role)
                .NotEmpty().WithMessage("Please specify role");
        }
    }
}
