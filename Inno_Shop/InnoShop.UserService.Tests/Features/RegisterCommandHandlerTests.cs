using FluentAssertions;
using InnoShop.UserService.Application.Features.Auth.Commands;
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
    public class RegisterCommandHandlerTests()
    {
        private readonly Mock<IUserRepository> userRepositoryMock = new();
        private readonly Mock<IJwtService> jwtServiceMock = new();
        private readonly Mock<IEmailService> emailServiceMock = new();
        [Fact]
        public async Task Handle_ShouldReturnToken_WhenRegistrationIsSuccessful()
        {
            userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User?)null);
            jwtServiceMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("fake_token");

            var handler = new RegisterCommandHandler(
                userRepositoryMock.Object,
                jwtServiceMock.Object,
                emailServiceMock.Object
                );
            var command = new RegisterCommand()
            {
                Name = "Test User",
                Email = "test@test.com",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().Be("fake_token");
        }
        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmailIsAlreadyExists()
        {
            userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User { Email = "test@test.com" });

            var handler = new RegisterCommandHandler
                (
                userRepositoryMock.Object,
                jwtServiceMock.Object,
                emailServiceMock.Object
                );
            var command = new RegisterCommand
            {
                Name = "Test User",
                Email = "test@test.com",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            var act = async () => await handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>().WithMessage("*already exists*");
        }
    }
}
