using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoShop.UserService.Application.Interfaces;
using InnoShop.UserService.Domain.Entities;
using MediatR;

namespace InnoShop.UserService.Application.Features.Users.Queries
{
    public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, User?>
    {
        public async Task<User?> Handle(GetUserByIdQuery request,CancellationToken cancellationToken)
        {
            return await userRepository.GetByIdAsync(request.Id);
        }

    }
}
