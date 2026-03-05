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
    public class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, bool>
    {
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id)
            ?? throw new Exception("Product not found");

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            
            await productRepository.UpdateAsync(product);
            return true;
        }
    }
}
