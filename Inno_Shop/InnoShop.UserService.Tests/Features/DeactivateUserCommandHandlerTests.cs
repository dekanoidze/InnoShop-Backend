using FluentAssertions;
using InnoShop.UserService.Application.Features.Users.Commands;
using InnoShop.UserService.Application.Interfaces;
using InnoShop.UserService.Application.Services;
using InnoShop.UserService.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.UserService.Tests.Features
{
    public class DeactivateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock = new();
        private readonly Mock<IProductServiceClient> productServiceClientMock = new();

        [Fact]
        public async Task Handle_ShouldDeactivateUSer_WhenUserExists()
        {
            var user = new User
            {
                Id= Guid.NewGuid(),
                IsActive = true,
                Email="test@test.com"
            };
            userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user); 
            productServiceClientMock.Setup(x=>x.HideUserProductsAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            var handler = new DeactivateUserCommandHandler
                (
                userRepositoryMock.Object,
                productServiceClientMock.Object
                );
            var command = new DeactivateUserCommand { Id = user.Id };

            var result = await handler.Handle(command, CancellationToken.None);

            user.IsActive.Should().BeFalse();
            result.Should().BeTrue();
        }
        [Fact]
        public async Task Handle_ShouldThrowException_WhenUserNotFOund()
        {
            userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User?)null);

            var handler = new DeactivateUserCommandHandler
                (
                userRepositoryMock.Object,
                productServiceClientMock.Object
                );

            var command = new DeactivateUserCommand { Id = Guid.NewGuid() };

            var act = async () => await handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>().WithMessage("*not found*");
        }
    }
}
