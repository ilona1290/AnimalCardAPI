using AnimalCard.Application.Pet.Queries.GetPetsByUserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Visit.Query.GetDataToNewVisit
{
    public class OwnerWithPetsDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = String.Empty;
        public List<UserPetDTO> Pets { get; set; } = new List<UserPetDTO>();
    }
}
