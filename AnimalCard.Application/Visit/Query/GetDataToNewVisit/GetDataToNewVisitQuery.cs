using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Visit.Query.GetDataToNewVisit
{
    public class GetDataToNewVisitQuery : IRequest<DataToNewVisitVm>
    {
        public int VetId { get; set; }
    }
}
