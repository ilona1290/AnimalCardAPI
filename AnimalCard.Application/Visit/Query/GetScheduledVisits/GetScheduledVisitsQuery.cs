using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Visit.Query.GetScheduledVisits
{
    public class GetScheduledVisitsQuery : IRequest<ScheduledVisitsVm>
    {
        public int VetId { get; set; }
        public int OwnerId { get; set; }
    }
}
