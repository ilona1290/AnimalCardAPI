using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Visit.Command.CompleteVisit
{
    public class CompletedVisitDTO
    {
        public string VisitCardFileName { get; set; } = String.Empty;
        public string VisitCardPath { get; set; } = String.Empty;
    }
}
