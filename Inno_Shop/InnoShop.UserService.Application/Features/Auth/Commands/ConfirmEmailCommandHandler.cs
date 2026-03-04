using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoShop.UserService.Domain.Entities;
using InnoShop.UserService.Application.Interfaces;
using MediatR;

namespace InnoShop.UserService.Application.Features.Auth.Commands
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, string>
    {
        private readonly IUserRepository _userRepository;

        public ConfirmEmailCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(ConfirmEmailCommand request,CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailConfirmationTokenAsync(request.Token);
            if (existingUser == null) { throw new Exception("Invalid confrimation token"); }
            if (existingUser.EmailConfirmationTokenExpiry<DateTime.UtcNow) { throw new Exception("Session expired"); }
            existingUser.IsEmailConfirmed = true;
            existingUser.EmailConfirmationToken = null;
            existingUser.EmailConfirmationTokenExpiry = null;
            await _userRepository.UpdateAsync(existingUser);
            return "Email is confirmed";
        }
    }
}
