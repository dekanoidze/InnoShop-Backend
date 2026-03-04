using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace InnoShop.UserService.Application.Behaviors
{
    public class ValidationBehavior<Trequest,Tresponse> : IPipelineBehavior<Trequest,Tresponse> where Trequest : IRequest<Tresponse>
    {
        private readonly IEnumerable<IValidator<Trequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<Trequest>> validators)
        {
            _validators = validators;
        }
        public async Task<Tresponse> Handle(Trequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<Trequest>(request);
                var failures =_validators.Select(v=>v.Validate(context))
                    .SelectMany(r=>r.Errors).Where(f=>f!=null)
                    .ToList();
                if(failures.Any())
                { throw new ValidationException(failures); }
            }
            return await next();
        }
    }
}
