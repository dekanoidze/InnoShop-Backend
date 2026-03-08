using FluentAssertions;
using InnoShop.ProductService.Application.Features.Commands;
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
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> productRepositoryMock = new();

        [Fact]
        public async Task Handle_ShouldReturn_ProductCreatedSuccessfuly()
        {
            productRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            var handler = new CreateProductCommandHandler
                (
                productRepositoryMock.Object
                );
            var command = new CreateProductCommand
            {
                Name = "name",
                Description = "description",
                Price=10,
                UserId = Guid.NewGuid()
            };

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().Be("Product created successfully");

        }
    }
}
