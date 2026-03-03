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
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser == null) { throw new Exception("Invalid email or password"); }
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, existingUser.PasswordHash);
            if (!isPasswordValid) { throw new Exception("Invalid email or password"); }

            return _jwtService.GenerateToken(existingUser);

        }
    }
}
