using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using InnoShop.UserService.Application.Services;
using InnoShop.UserService.Domain.Entities;
using InnoShop.UserService.Application.Interfaces;
using BCrypt.Net;

namespace InnoShop.UserService.Application.Features.Auth.Commands
{
    public class LoginCommandHandler(IUserRepository userRepository,IJwtService jwtService) : IRequestHandler<LoginCommand, string>
    {
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByEmailAsync(request.Email)
            ?? throw new UnauthorizedAccessException("Invalid email or password");
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, existingUser.PasswordHash);
            if (!isPasswordValid) { throw new UnauthorizedAccessException("Invalid email or password"); }

            return jwtService.GenerateToken(existingUser);

        }
    }
}
