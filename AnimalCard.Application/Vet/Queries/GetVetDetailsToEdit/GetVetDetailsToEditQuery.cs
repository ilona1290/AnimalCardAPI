using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Vet.Queries.GetVetDetailsToEdit
{
    public class GetVetDetailsToEditQuery : IRequest<VetToEditVm>
    {
        public int VetId { get; set; }
    }
}
