using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetCompletedVisits
{
    public class GetPetCompletedVisitsQuery : IRequest<PetCompletedVisitsVm>
    {
        public int PetId { get; set; }
    }
}
