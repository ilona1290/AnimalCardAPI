using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Visit.Command.DeleteScheduledVisit
{
    public class DeleteScheduledVisitCommand : IRequest<bool>
    {
        public int VisitId { get; set; }
    }
}
