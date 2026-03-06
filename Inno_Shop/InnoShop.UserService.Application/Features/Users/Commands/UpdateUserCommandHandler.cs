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
    public class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, bool>
    {
        public async Task<bool> Handle(UpdateUserCommand request,CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id)
            ?? throw new Exception("User not found");

            user.Name=request.Name;
            user.Email=request.Email;

            await userRepository.UpdateAsync(user);
            return true;
        }
    }
}
