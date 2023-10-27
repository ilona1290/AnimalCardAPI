using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetsByUserRole
{
    public class GetPetsByUserRoleQuery : IRequest<UserPetsVm>
    {
        public string Role { get; set; } = String.Empty;
        public int UserId { get; set; }

    }
}
