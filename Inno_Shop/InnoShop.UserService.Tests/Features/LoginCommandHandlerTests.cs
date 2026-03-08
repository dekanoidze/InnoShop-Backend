using BCrypt.Net;
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
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock = new();
        private readonly Mock<IJwtService> jwtServiceMock = new();

        [Fact]
        public async Task Handle_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var password = "Password123";
            var user = new User
            {
                Email = "test@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };
            userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            jwtServiceMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("fake_roken");

            var handler = new LoginCommandHandler
                (
                userRepositoryMock.Object,
                jwtServiceMock.Object
                );
            var command = new LoginCommand
            {
                Email = "test@test.com",
                Password = "WrongPassword123"
            };

            var act = async () => await handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("*Invalid email or password*");
        }
    }
}
