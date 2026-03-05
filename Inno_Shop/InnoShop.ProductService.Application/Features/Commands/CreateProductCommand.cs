using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InnoShop.ProductService.Application.Features.Commands
{
    public class CreateProductCommand : IRequest<string>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public decimal Price { get; set; }
        public Guid UserId {  get; set; }
    }
}
