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
    public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, Product?>
    {
        public async Task<Product?> Handle(GetProductByIdQuery request,CancellationToken cancellationToken)
        {
            return await productRepository.GetByIdAsync(request.Id);
        }
    }
}
