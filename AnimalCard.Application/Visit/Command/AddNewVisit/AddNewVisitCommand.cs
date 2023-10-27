using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Visit.Command.AddNewVisit
{
    public class AddNewVisitCommand : IRequest<bool>
    {
        public int Vet { get; set; }
        public int VisitType { get; set; }
        public string CustomType { get; set; } = String.Empty;
        public int Owner { get; set; }
        public int Patient { get; set; }
        public DateTime DateVisit { get; set; }
        public DateTime TimeStartVisit { get; set; }
        public DateTime TimeEndVisit { get; set; }
        public string ExtraInfo { get; set; } = String.Empty;
    }
}
