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
    public class GetAllUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetAllUserAsync();
        }
    }
}
