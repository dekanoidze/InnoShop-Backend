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
    public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserCommand request,CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id)
            ?? throw new Exception("User not found");
            
            await userRepository.DeleteAsync(user.Id);
            return true;
        }
    }
}
