using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Vet.Queries.GetVetDetails
{
    public class GetVetDetailsQuery : IRequest<VetVm>
    {
        public int VetId { get; set; }
    }
}
