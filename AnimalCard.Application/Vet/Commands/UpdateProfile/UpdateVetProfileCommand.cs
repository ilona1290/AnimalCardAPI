using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Vet.Commands.UpdateProfile
{
    public class UpdateVetProfileCommand : IRequest<string>
    {
        public int IdVet { get; set; }
        public string? ProfilePicture { get; set; } = String.Empty;
        public string? AboutMe { get; set; } = String.Empty;
        public List<AddressDTO> Addresses { get; set; } = new List<AddressDTO>();
        public List<string> TreatedDiseases { get; set; } = new List<string>();
        public List<string> ProvidedServicesTreatments { get; set; } = new List<string>();
    }
}
