using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace InnoShop.UserService.Application.Features.Auth.Commands
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
               .MaximumLength(30).WithMessage("Name can not exceed 30 characters");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email adress");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters");
        }
    }
}
