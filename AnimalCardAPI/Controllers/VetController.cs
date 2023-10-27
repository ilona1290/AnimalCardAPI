using AnimalCard.Application.User.Commands.CreateUser;
using AnimalCard.Application.Vet.Commands.CreateVet;
using AnimalCard.Application.Vet.Commands.UpdateProfile;
using AnimalCard.Application.Vet.Queries.GetDiseasesAndServicesTreatments;
using AnimalCard.Application.Vet.Queries.GetVetDetails;
using AnimalCard.Application.Vet.Queries.GetVetDetailsToEdit;
using AnimalCard.Application.Vet.Queries.GetVets;
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
    public class VetController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("CreateVet")]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<ActionResult> CreateVet(CreateVetCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("GetDiseasesAndServicesTreatments")]
        public async Task<ActionResult<DiseasesAndServicesTreatmentsVm>> GetDiseasesAndServicesTreatments()
        {
            return Ok(await _mediator.Send(new GetDiseasesAndServicesTreatmentsQuery()));
        }

        [HttpPost("UpdateVetProfile")]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<ActionResult> UpdateVetProfile(UpdateVetProfileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("GetAllVets")]
        public async Task<ActionResult<VetsVm>> GetVets()
        {
            return Ok(await _mediator.Send(new GetVetsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VetVm>> GetVetDetail(int id)
        {
            return Ok(await _mediator.Send(new GetVetDetailsQuery() { VetId = id }));
        }

        [HttpGet("{id}/ToEdit")]
        public async Task<ActionResult<VetVm>> GetVetDetailsToEdit(int id)
        {
            return Ok(await _mediator.Send(new GetVetDetailsToEditQuery() { VetId = id }));
        }
    }
}
