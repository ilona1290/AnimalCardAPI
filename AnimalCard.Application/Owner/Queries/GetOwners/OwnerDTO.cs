using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Owner.Queries.GetOwners
{
    public class OwnerDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public string ProfilePicture { get; set; } = String.Empty; 
    }
}
