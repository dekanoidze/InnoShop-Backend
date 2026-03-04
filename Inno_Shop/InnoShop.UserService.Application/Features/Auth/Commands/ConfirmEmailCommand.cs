using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InnoShop.UserService.Application.Features.Auth.Commands
{
    public class ConfirmEmailCommand : IRequest<string>
    {
        public string Token {  get; set; }=string.Empty;
    }
}
