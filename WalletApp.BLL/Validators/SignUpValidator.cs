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
    public class SignUpValidator : AbstractValidator<SignUpVM>
    {
        public SignUpValidator()
        {
            RuleFor(m => m.Email)
                .EmailAddress().WithMessage("Wrong email format specified")
                .NotEmpty().WithMessage("Please specify the right email");
            RuleFor(m => m.UserName)
                .NotEmpty().WithMessage("Please specify username");
            RuleFor(m => m.Password)
                .MinimumLength(Settings.PasswordLength).WithMessage("Password must be at least 6 characters long");
            RuleFor(m => m.ConfirmPassword)
                .Equal(p => p.Password).WithMessage("Passwords do not match");
        }
    }
}
