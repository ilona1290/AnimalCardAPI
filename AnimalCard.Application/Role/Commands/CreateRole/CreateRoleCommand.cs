using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Role.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<int>
    {
        public string RoleName { get; set; } = string.Empty;
    }
}
