using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.User.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<string>
    {
        public string Email { get; set; } = string.Empty;
        public string ActivationToken { get; set; } = string.Empty;
    }
}
