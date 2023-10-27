using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.User.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmationPassword { get; set; } = string.Empty; 
        public List<string> Roles { get; set; } = new List<string>();
    }
}
