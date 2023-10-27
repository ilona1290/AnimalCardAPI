using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Commands.AddResearchResult
{
    public class AddResearchResultCommand : IRequest<bool>
    {
        public string ResultFileName { get; set; } = String.Empty;
        public string ResultPath { get; set; } = String.Empty;
        public int PetId { get; set; }
    }
}
