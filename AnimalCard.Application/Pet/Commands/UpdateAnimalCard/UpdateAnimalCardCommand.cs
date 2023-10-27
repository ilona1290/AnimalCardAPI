using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Commands.UpdateAnimalCard
{
    public class UpdateAnimalCardCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public DateTime DateBirth { get; set; }
        public string Breed { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string HairType { get; set; } = string.Empty;
        public string Trademarks { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;
        public string ExtraInfo { get; set; } = string.Empty;
        public int Owner { get; set; }
    }
}
