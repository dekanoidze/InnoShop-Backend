using InnoShop.ProductService.Application.Interfaces;
using InnoShop.ProductService.Domain.Entities;
using InnoShop.ProductService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.ProductService.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
       public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await context.Products.FindAsync(id);
        }
        public async Task<IEnumerable<Product>> GetByUserIdAsync(Guid UserId)
        {
            return await context.Products.Where(p => p.UserId == UserId).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await context.Products.ToListAsync();
        }
        public async Task AddAsync(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var user = await context.Products.FindAsync(id);
            if (user != null)
            {
                context.Products.Remove(user);
                await context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }
        public async Task UpdateRangeAsync(IEnumerable<Product> products)
        {
            context.Products.UpdateRange(products);
            await context.SaveChangesAsync();
        }
    }
}
