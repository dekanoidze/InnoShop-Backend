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
    public class CreateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand, string>
    {
        public async Task<string> Handle(CreateProductCommand request,CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description=request.Description,
                Price= request.Price,
                UserId=request.UserId
            };
            await productRepository.AddAsync(product);
            return "Product created successfully";
        }
    }
}
