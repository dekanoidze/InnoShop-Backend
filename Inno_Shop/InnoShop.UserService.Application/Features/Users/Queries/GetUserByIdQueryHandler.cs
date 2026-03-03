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
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Handle(GetUserByIdQuery request,CancellationToken cancellationToken)
        {
            return await _userRepository.GetByIdAsync(request.Id);
        }

    }
}
