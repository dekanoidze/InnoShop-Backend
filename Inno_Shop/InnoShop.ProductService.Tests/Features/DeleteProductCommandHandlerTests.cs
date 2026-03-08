using InnoShop.ProductService.Application.Interfaces;
using InnoShop.ProductService.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using InnoShop.ProductService.Application.Features.Commands;

namespace InnoShop.ProductService.Tests.Features
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> productRepositoryMock = new();

        [Fact]
        public async Task Handle_ShouldReturn_ProductDeletedSuccessfuly()
        {
            var product = new Product { Id = Guid.NewGuid(), Name = "Test Product" };
            productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);
            productRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            var handler = new DeleteProductCommandHandler
                (
                productRepositoryMock.Object
                );
            var command = new DeleteProductCommand
            {
                Id = product.Id
            };

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeTrue();
        }
        [Fact]
        public async Task Handle_ShouldThrowExecption_WhenKeyNotFound()
        {
            productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product?)null);

            var handler = new DeleteProductCommandHandler
                (
                productRepositoryMock.Object
                );
            var command = new DeleteProductCommand()
            {
                Id = Guid.NewGuid()
            };

            var act = async () => await handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("*not found*");
        }
    }
}
