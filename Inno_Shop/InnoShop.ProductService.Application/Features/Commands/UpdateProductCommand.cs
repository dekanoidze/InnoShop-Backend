using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.ProductService.Application.Features.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public string Name { get; set; } = string.Empty;
        public Guid Id {  get; set; }
        public string Description { get; set; }=string.Empty;
        public decimal Price {  get; set; }
    }
}
