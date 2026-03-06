using InnoShop.ProductService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.ProductService.Application.Features.Queries
{
    public class SearchProductsQuery : IRequest<IEnumerable<Product>>
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set;}
        public bool? IsAvailable {  get; set; }
    }
}
