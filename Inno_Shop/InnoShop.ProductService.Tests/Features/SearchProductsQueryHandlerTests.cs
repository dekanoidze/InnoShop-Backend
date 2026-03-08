using FluentAssertions;
using InnoShop.ProductService.Application.Features.Queries;
using InnoShop.ProductService.Application.Interfaces;
using InnoShop.ProductService.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.ProductService.Tests.Features
{
    public class SearchProductsQueryHandlerTests
    {
        private readonly Mock<IProductRepository> productRepositoryMock = new();

        [Fact]
        public async Task Handle_ShouldReturn_SearchedProduct()
        {
            var products = new List<Product>()
            {
                new Product {Id=Guid.NewGuid(),Name="Water Bottle",Price=10},
                new Product {Id=Guid.NewGuid(),Name="Phone Case",Price=20}
            };
            productRepositoryMock.Setup(x => x.SearchAsync(
                It.IsAny<string>(),It.IsAny<Decimal?>(),
                It.IsAny<decimal?>(),It.IsAny<bool?>()))
                .ReturnsAsync(products);

            var handler = new SearchProductsQueryHandler
                (
                productRepositoryMock.Object
                );
            var query = new SearchProductsQuery()
            {
               IsAvailable = true,
               MaxPrice = 20,
               MinPrice = 10,
               Name= "Name"
            };
            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().HaveCount(2);
        }
    }
}
