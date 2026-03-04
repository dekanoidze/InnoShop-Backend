using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoShop.UserService.Domain.Entities;
using InnoShop.UserService.Application.Interfaces;
using InnoShop.UserService.Application.Services;
using MediatR;

namespace InnoShop.UserService.Application.Features.Auth.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        public ResetPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByResetTokenAsync(request.Token);
            if (existingUser == null) { throw new Exception("User not found"); }
            if(existingUser.PasswordResetTokenExpiry<DateTime.UtcNow)
            { throw new Exception("Session Expired"); }
            var passWordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            existingUser.PasswordHash= passWordHash;
            existingUser.PasswordResetToken = null;
            existingUser.PasswordResetTokenExpiry = null;
            await _userRepository.UpdateAsync(existingUser);
            return "Password was changed successfully";
        }
    }
}
