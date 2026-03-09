using FluentValidation;
using InnoShop.ProductService.Application.Features.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.ProductService.Application.Validators
{
    public class SearchProductsQueryValidator : AbstractValidator<SearchProductsQuery>
    {
        public SearchProductsQueryValidator()
        {
            RuleFor(x => x.MinPrice).GreaterThanOrEqualTo(0)
            .WithMessage("Minimum price can not be negative")
            .When(x => x.MinPrice.HasValue);
            RuleFor(x => x.MaxPrice).GreaterThanOrEqualTo(0)
                .WithMessage("Maximum price can not be negative")
                .When(x => x.MaxPrice.HasValue);
            RuleFor(x => x.MinPrice)
                .LessThanOrEqualTo(x => x.MaxPrice).WithMessage("Minimum price can not be greater than Maximum price")
                .When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue);

        }

    }
}
