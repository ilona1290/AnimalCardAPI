﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPet
{
    public class GetPetQuery : IRequest<PetVm>
    {
        public int PetId { get; set; }
    }
}
