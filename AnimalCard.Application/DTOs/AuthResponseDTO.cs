using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.DTOs
{
    public class AuthResponseDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsCompletedVetProfile { get; set; }
        public string ProfilePicture { get; set; } = string.Empty;
    }
}
