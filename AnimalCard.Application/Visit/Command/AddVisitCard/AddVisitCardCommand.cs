using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Visit.Command.AddVisitCard
{
    public class AddVisitCardCommand : IRequest<bool>
    {
        public int VisitId { get; set; }
        public int PetId { get; set; }
        public int VetId { get; set; }
        public string VisitCardFileName { get; set; } = String.Empty;
        public string VisitCardPath { get; set; } = String.Empty;
    }
}
