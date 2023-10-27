using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Vet.Queries.GetDiseasesAndServicesTreatments
{
    public class DiseasesAndServicesTreatmentsVm
    {
        public List<string> Diseases { get; set; } = new List<string>();
        public List<string> ServicesTreatments { get; set; } = new List<string>();
    }
}
