using InnoShop.ProductService.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using InnoShop.ProductService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.ProductService.Tests.Integration
{
    public static class SeedHelper
    {
        public static async Task<List<Product>> SeedProductsAsync(IServiceProvider service, Guid userId, int count = 2)
        {
            using var scope = service.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var products = Enumerable.Range(1, count).Select(i => new Product
            {
                Id = Guid.NewGuid(),
                Name = $"Test Product {i}",
                Description = $"Description {i}",
                Price = 10m * i,
                IsAvailable = true,
                IsActive = true,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            db.Products.AddRange(products);
            await db.SaveChangesAsync();
            return products;
        }
        public static async Task CleanProductsAsync(IServiceProvider services, Guid userId)
        {
            using var scope=services.CreateScope();
            var db=scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var toRemove = db.Products.Where(p => p.UserId == userId);
            db.Products.RemoveRange(toRemove);
            await db.SaveChangesAsync();
        }
    }
}
