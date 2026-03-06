using InnoShop.UserService.Application.Interfaces;
using InnoShop.UserService.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.UserService.Application.Features.Users.Commands
{
    public class ReactivateUserCommandHandler(IUserRepository userRepository, IProductServiceClient productServiceClient) : IRequestHandler<ReactivateUserCommand, bool>
    {
        public async Task<bool> Handle(ReactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id)
                ?? throw new Exception("User not found");

            user.IsActive=true;

            await userRepository.UpdateAsync(user);
            await productServiceClient.ShowUserProductsAsync(user.Id);
            return true;
        }
    }
}
