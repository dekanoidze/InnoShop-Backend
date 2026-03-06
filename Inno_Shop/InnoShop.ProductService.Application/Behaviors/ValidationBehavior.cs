using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace InnoShop.ProductService.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {           
                var context = new ValidationContext<TRequest>(request);
                var failures = validators.Select(v => v.Validate(context))
                    .SelectMany(r => r.Errors).Where(f => f != null)
                    .ToList();
                if (failures.Any())
                { throw new ValidationException(failures); }
            }
            return await next();
        }
    }
}
