using FluentValidation;
using InnoShop.ProductService.Application.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.ProductService.Application.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required").MaximumLength(500);
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price can not be less then 0");
        }
    }
}
