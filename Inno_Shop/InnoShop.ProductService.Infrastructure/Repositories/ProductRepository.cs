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
        public async Task<IEnumerable<Product>> SearchAsync(string? name, decimal? minPrice, decimal? maxPrice, bool? isAvailable)
        {
            var query = context.Products.AsQueryable().AsNoTracking();
            
            if (!string.IsNullOrWhiteSpace(name)) { query = query.Where(p => p.Name.Contains(name)); }
            if (minPrice.HasValue) { query = query.Where(p => p.Price >= minPrice.Value); }
            if (maxPrice.HasValue) { query = query.Where(p => p.Price <= maxPrice.Value); }
            if (isAvailable.HasValue) { query=query.Where(p => p.IsAvailable==isAvailable.Value); }

            return await query.ToListAsync();

        }
    }
}
