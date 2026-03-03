using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoShop.UserService.Application.Features;
using InnoShop.UserService.Application.Interfaces;
using InnoShop.UserService.Domain.Entities;
using MediatR;

namespace InnoShop.UserService.Application.Features.Users.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(DeleteUserCommand request,CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null) { throw new Exception("User not found"); }
            
            await _userRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}
