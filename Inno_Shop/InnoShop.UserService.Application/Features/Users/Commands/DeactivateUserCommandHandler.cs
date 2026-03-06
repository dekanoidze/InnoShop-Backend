using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using InnoShop.UserService.Application.Features;
using InnoShop.UserService.Application.Interfaces;
using InnoShop.UserService.Domain.Entities;
using InnoShop.UserService.Application.Services;

namespace InnoShop.UserService.Application.Features.Users.Commands
{
    public class DeactivateUserCommandHandler(IUserRepository userRepository,IProductServiceClient productServiceClient) : IRequestHandler<DeactivateUserCommand, bool>
    {
        public async Task<bool> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id)
            ?? throw new Exception("User not found");

            user.IsActive = false;

            await userRepository.UpdateAsync(user);
            await productServiceClient.HideUserProductsAsync(user.Id);
            return true;
        }
    }
}
