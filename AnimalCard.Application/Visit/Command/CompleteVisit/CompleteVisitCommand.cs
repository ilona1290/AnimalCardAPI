using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Visit.Command.CompleteVisit
{
    public class CompleteVisitCommand : IRequest<CompleteVisitResponse>
    {
        public int VisitId { get; set; }
        public int PetId { get; set; }
        public int VetId { get; set; }
        public RabiesVaccinationDTO RabiesVaccination { get; set; } = new RabiesVaccinationDTO();
        public List<OtherVaccinationDTO> OtherVaccinations { get; set; } = new List<OtherVaccinationDTO>();
        public List<TreatmentDTO> Treatments { get; set; } = new List<TreatmentDTO>();
        public List<TreatedDiseaseDTO> TreatedDiseases { get; set; } = new List<TreatedDiseaseDTO>();
        public ResearchDTO Research { get; set; } = new ResearchDTO();
        public int Weight { get; set; }
        //public CompletedVisitDTO CompletedVisit { get; set; } = new CompletedVisitDTO();
    }
}
