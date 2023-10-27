using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetTreatments
{
    public class GetPetTreatmentsQuery : IRequest<PetTreatmentsVm>
    {
        public int PetId { get; set; }
    }
}
