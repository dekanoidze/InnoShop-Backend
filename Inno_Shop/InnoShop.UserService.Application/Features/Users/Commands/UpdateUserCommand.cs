using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InnoShop.UserService.Application.Features.Users.Commands
{
    public class UpdateUserCommand :IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public string Email {  get; set; }= string.Empty;
    }
}
