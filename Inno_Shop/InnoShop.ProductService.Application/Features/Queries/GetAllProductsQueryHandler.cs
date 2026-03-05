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
    public class GetAllProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request,CancellationToken cancellationToken)
        {
            return await productRepository.GetAllProductsAsync();
        }

    }
}
