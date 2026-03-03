using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InnoShop.UserService.Application.Features.Users.Commands
{
    public class DeactivateUserCommand : IRequest<bool>
    {
        public Guid Id {  get; set; }
    }
}
