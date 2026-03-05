using InnoShop.ProductService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.ProductService.Application.Features.Queries
{
    public class GetProductsByUserIdQuery : IRequest<IEnumerable<Product>>
    {
        public Guid UserId {  get; set; }
    }
}
