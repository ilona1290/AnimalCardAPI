using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Admin.Queries.GetCustomsToConfirm
{
    public class GetCustomsToConfirmQuery : IRequest<CustomsToConfirmVm>
    {
    }
}
