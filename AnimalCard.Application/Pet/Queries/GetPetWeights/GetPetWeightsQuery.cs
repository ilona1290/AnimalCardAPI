using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetWeights
{
    public class GetPetWeightsQuery : IRequest<PetWeightsVm>
    {
        public int PetId { get; set; }
    }
}
