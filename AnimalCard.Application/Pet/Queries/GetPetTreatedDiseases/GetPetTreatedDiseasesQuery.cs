using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetTreatedDiseases
{
    public class GetPetTreatedDiseasesQuery : IRequest<PetTreatedDiseasesVm>
    {
        public int PetId { get; set; }
    }
}
