using InnoShop.ProductService.Application.Interfaces;
using InnoShop.ProductService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.ProductService.Application.Features.Queries
{
    public class SearchProductsQueryHandler(IProductRepository productRepository): IRequestHandler<SearchProductsQuery, IEnumerable<Product>>
    {
        public async Task<IEnumerable<Product>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            return await productRepository.SearchAsync(request.Name,request.MinPrice,request.MaxPrice,request.IsAvailable);
        }
    }
}
