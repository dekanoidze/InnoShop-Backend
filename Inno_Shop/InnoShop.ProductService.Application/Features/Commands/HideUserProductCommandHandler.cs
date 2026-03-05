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
    public class HideUserProductCommandHandler(IProductRepository productRepository) : IRequestHandler<HideUserProductsCommand, bool>
    {
        public async Task<bool> Handle(HideUserProductsCommand request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetByUserIdAsync(request.UserId)
            ??throw new Exception("Product not found");
            
            foreach(var productItem in products)
            {
                productItem.IsActive = false;
            }
            await productRepository.UpdateRangeAsync(products);
            return true;
        }
    }
}
