using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.ProductService.Application.Features.Commands
{
     public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id {  get; set; }
    }
}
