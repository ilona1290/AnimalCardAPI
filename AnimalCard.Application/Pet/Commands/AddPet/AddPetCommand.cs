using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Commands.AddPet
{
    public class AddPetCommand : IRequest<int>
    {
        public int Vet { get; set; }
        public int Owner { get; set; }
        public string IdentityNumber { get; set; } = String.Empty;
        public string Photo { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Sex { get; set; } = String.Empty;
        public DateTime DateBirth { get; set; }
        public string Breed { get; set; } = String.Empty;
        public string Color { get; set; } = String.Empty;
        public string HairType { get; set; } = String.Empty;
        public string Trademarks { get; set; } = String.Empty;
        public string Allergies { get; set; } = String.Empty;
        public string ExtraInfo { get; set; } = String.Empty;
    }
}
