using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoShop.ProductService.Application.Features;
using InnoShop.ProductService.Application.Interfaces;
using InnoShop.ProductService.Domain.Entities;
using MediatR;

namespace InnoShop.ProductService.Application.Features.Commands
{
    public class ShowUserProductsCommandHandler(IProductRepository productRepository) : IRequestHandler<ShowUserProductsCommand, bool>
    {
        public async Task<bool> Handle(ShowUserProductsCommand request,CancellationToken cancellationToken)
        {
            var products = await productRepository.GetByUserIdAsync(request.UserId)
                ?? throw new Exception("Product not found");
            foreach (var product in products)
            {
                product.IsActive = true;
            }
            await productRepository.UpdateRangeAsync(products);
            return true;
        }
    }
}
