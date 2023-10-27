using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetRabiesVaccinations
{
    public class GetPetRabiesVaccinationsQuery : IRequest<PetRabiesVaccinationsVm>
    {
        public int PetId { get; set; }
    }
}
