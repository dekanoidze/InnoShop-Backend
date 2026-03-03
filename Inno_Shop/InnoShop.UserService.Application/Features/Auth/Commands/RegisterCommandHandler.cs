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
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        public RegisterCommandHandler(IUserRepository userRepository, IJwtService jwtService, IEmailService emailService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _emailService = emailService;
        }
        public async Task<String> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
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

            await _userRepository.AddAsync(user);

            await _emailService.SendEmailAsync(
                user.Email, "Welcome to Inno Shop",
                $"<h1>Welcome {user.Name}!</h1><p>Your account has been created successfully.</p>"
                );
            return _jwtService.GenerateToken(user);
        }
    }
}
