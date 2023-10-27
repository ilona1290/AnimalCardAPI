using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Admin.Commands.ConfirmOrRejectCustoms
{
    public class ConfirmOrRejectCustomsCommand : IRequest<bool>
    {
        public List<int> ConfirmedDiseases { get; set; } = new List<int>();
        public List<int> RejectedDiseases { get; set; } = new List<int>();
        public List<int> ConfirmedServicesTreatments { get; set; } = new List<int>();
        public List<int> RejectedServicesTreatments { get; set; } = new List<int>();
    }
}
