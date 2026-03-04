using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InnoShop.UserService.Application.Features.Auth.Commands
{
    public class ForgotPasswordCommand : IRequest<string>
    {
        public string Email {  get; set; }=string.Empty;
    }
}
