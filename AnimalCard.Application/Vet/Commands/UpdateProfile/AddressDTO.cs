using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Vet.Commands.UpdateProfile
{
    public class AddressDTO
    {
        public int? Id { get; set; }
        public string? NameOfPlace { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string? District { get; set; } = String.Empty;
        public string Street { get; set; } = String.Empty;
        public int HouseNumber { get; set; }
        public int? PremisesNumber { get; set; }
    }
}
