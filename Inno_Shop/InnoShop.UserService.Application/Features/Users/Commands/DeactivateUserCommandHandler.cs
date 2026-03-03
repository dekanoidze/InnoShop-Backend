using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using InnoShop.UserService.Application.Features;
using InnoShop.UserService.Application.Interfaces;
using InnoShop.UserService.Domain.Entities;

namespace InnoShop.UserService.Application.Features.Users.Commands
{
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public DeactivateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null) { throw new Exception("User not found"); }

            user.IsActive = false;

            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
