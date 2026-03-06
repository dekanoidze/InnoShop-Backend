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
    public class ResetPasswordCommandHandler(IUserRepository userRepository) : IRequestHandler<ResetPasswordCommand, string>
    {
        public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByResetTokenAsync(request.Token)
            ?? throw new Exception("User not found");
            if(existingUser.PasswordResetTokenExpiry<DateTime.UtcNow)
            { throw new Exception("Session Expired"); }
            var passWordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            existingUser.PasswordHash= passWordHash;
            existingUser.PasswordResetToken = null;
            existingUser.PasswordResetTokenExpiry = null;
            await userRepository.UpdateAsync(existingUser);
            return "Password was changed successfully";
        }
    }
}
