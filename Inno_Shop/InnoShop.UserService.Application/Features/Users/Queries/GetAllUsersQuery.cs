using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using InnoShop.UserService.Domain.Entities;

namespace InnoShop.UserService.Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {

    }
}
