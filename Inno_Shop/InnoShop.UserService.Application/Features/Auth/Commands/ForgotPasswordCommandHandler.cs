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
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public ForgotPasswordCommandHandler(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var existingUser=await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser == null) { throw new Exception("User not dound"); }
            var resetToken=Guid.NewGuid().ToString();
            existingUser.PasswordResetToken = resetToken;
            existingUser.PasswordResetTokenExpiry= DateTime.UtcNow.AddHours(1);
            await _userRepository.UpdateAsync(existingUser);
            await _emailService.SendEmailAsync(
                existingUser.Email,
                "Password Reset",
                $"Your reset token is: {resetToken}"
                );
            return "Password reset email sent";

        }

    }
}
