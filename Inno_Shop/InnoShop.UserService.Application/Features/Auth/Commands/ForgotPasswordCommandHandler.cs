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
    public class ForgotPasswordCommandHandler(IUserRepository userRepository,IEmailService emailService) : IRequestHandler<ForgotPasswordCommand, string>
    {
        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var existingUser= await userRepository.GetByEmailAsync(request.Email)
                ?? throw new Exception("User not dound");
            var resetToken =Guid.NewGuid().ToString();
            existingUser.PasswordResetToken = resetToken;
            existingUser.PasswordResetTokenExpiry= DateTime.UtcNow.AddHours(1);
            await userRepository.UpdateAsync(existingUser);
            await emailService.SendEmailAsync(
                existingUser.Email,
                "Password Reset",
                $"Your reset token is: {resetToken}"
                );
            return "Password reset email sent";

        }

    }
}
