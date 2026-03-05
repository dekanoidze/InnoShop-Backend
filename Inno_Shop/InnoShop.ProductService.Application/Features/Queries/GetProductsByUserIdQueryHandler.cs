using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoShop.ProductService.Application.Interfaces;
using InnoShop.ProductService.Domain.Entities;
using MediatR;

namespace InnoShop.ProductService.Application.Features.Queries
{
    public class GetProductsByUserIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsByUserIdQuery, IEnumerable<Product>>
    {
        public async Task<IEnumerable<Product>> Handle(GetProductsByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await productRepository.GetByUserIdAsync(request.UserId);
        }
    }
}
