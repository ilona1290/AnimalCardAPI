using AnimalCard.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.User.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, string>
    {
        private readonly IIdentityService _identityService;
        public ConfirmEmailCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<string> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ConfirmEmail(request.Email, request.ActivationToken);
            return result.ToString();
        }
    }
}
