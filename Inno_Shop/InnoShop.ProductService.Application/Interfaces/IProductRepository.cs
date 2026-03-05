using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoShop.ProductService.Domain.Entities;

namespace InnoShop.ProductService.Application.Interfaces
{
    public interface  IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetByUserIdAsync(Guid UserId);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task AddAsync(Product product);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Product product);
        Task UpdateRangeAsync(IEnumerable<Product> products);
    }
}
