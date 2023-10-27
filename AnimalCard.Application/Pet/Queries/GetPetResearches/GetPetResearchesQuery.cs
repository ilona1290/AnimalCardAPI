using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetResearches
{
    public class GetPetResearchesQuery : IRequest<PetResearchesVm>
    {
        public int PetId { get; set; }
    }
}
