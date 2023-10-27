using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetOtherVaccinations
{
    public class GetPetOtherVaccinationsQuery : IRequest<PetOtherVaccinationsVm>
    {
        public int PetId { get; set; }
    }
}
