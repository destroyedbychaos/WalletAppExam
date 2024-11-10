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
    public class SignInValidator : AbstractValidator<SignInVM>
    {
        public SignInValidator()
        {
            RuleFor(m => m.Email)
                .EmailAddress().WithMessage("Wrong email format")
                .NotEmpty().WithMessage("Please enter email");
            RuleFor(m => m.Password)
                .MinimumLength(Settings.PasswordLength).WithMessage("Password must be at least 6 symbols");
        }
    }
}
