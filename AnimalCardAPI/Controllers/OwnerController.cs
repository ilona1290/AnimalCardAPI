using AnimalCard.Application.Owner.Queries.GetOwner;
using AnimalCard.Application.Owner.Queries.GetOwners;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OwnerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OwnerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<OwnersVm>> GetOwners()
        {
            return Ok(await _mediator.Send(new GetOwnersQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerVm>> GetOwner(int id)
        {
            return Ok(await _mediator.Send(new GetOwnerQuery { OwnerId = id }));
        }
    }
}
