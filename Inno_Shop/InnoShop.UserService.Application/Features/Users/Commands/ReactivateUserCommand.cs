using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.UserService.Application.Features.Users.Commands
{
    public class ReactivateUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
