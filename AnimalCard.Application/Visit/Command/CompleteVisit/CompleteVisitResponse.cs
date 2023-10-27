using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Visit.Command.CompleteVisit
{
    public class CompleteVisitResponse
    {
        public bool Result { get; set; }
        public string GeneratedCardPath { get; set; } = String.Empty;
        public string GeneratedCardFileName { get; set; } = String.Empty;

    }
}
