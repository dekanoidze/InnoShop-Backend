using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoShop.UserService.Application.Services;
using InnoShop.UserService.Domain.Entities;
using InnoShop.UserService.Application.Interfaces;
using MediatR;

namespace InnoShop.UserService.Application.Features.Auth.Commands
{
    public class RegisterCommandHandler(IUserRepository userRepository,IJwtService jwtService,IEmailService emailService) : IRequestHandler<RegisterCommand, string>
    {
        public async Task<String> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null) { throw new Exception("User with this Email already exists"); }

            var passWordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = passWordHash,
                Role = "User"
            };

            await userRepository.AddAsync(user);

            var confirmationToken=Guid.NewGuid().ToString();
            user.EmailConfirmationToken = confirmationToken;
            user.EmailConfirmationTokenExpiry = DateTime.UtcNow.AddDays(1);
            await userRepository.UpdateAsync(user);

            await emailService.SendEmailAsync(
                user.Email, "Confrim your email",
                $"<h1>Welcome {user.Name}!</h1><p>Your confirmation token is : {confirmationToken}</p>"
                );
            return jwtService.GenerateToken(user);
        }
    }
}
