using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Common.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateJWTToken((string userId, string email, IList<string> roles) userDetails);
    }
}
