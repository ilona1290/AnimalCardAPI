using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.ProfilePicture.Commands.UpdateProfilePicture
{
    public class UpdateProfilePictureCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Who { get; set; } = String.Empty;
        public string Photo { get; set; } = String.Empty;
    }
}
