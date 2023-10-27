using AnimalCard.Application.Visit.Command.AddNewVisit;
using AnimalCard.Application.Visit.Command.AddVisitCard;
using AnimalCard.Application.Visit.Command.CompleteVisit;
using AnimalCard.Application.Visit.Command.DeleteScheduledVisit;
using AnimalCard.Application.Visit.Query.GetDataToNewVisit;
using AnimalCard.Application.Visit.Query.GetScheduledVisits;
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
    public class VisitController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VisitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetDataToNewVisit/{vetId}")]
        public async Task<ActionResult<DataToNewVisitVm>> GetDataToNewVisit(int vetId)
        {
            return Ok(await _mediator.Send(new GetDataToNewVisitQuery { VetId = vetId}));
        }

        [HttpPost("AddNewVisit")]
        public async Task<ActionResult> AddNewVisit(AddNewVisitCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}/DeleteVisit")]
        public async Task<ActionResult> DeleteVisit(int id)
        {
            return Ok(await _mediator.Send(new DeleteScheduledVisitCommand { VisitId = id }));
        }

        [HttpGet("{vetId}/GetVetScheduledVisits")]
        public async Task<ActionResult<ScheduledVisitsVm>> GetVetScheduledVisits(int vetId)
        {
            return Ok(await _mediator.Send(new GetScheduledVisitsQuery { VetId = vetId }));
        }

        [HttpGet("{ownerId}/GetOwnerScheduledVisits")]
        public async Task<ActionResult<ScheduledVisitsVm>> GetOwnerScheduledVisitsOwner(int ownerId)
        {
            return Ok(await _mediator.Send(new GetScheduledVisitsQuery { OwnerId = ownerId }));
        }

        [HttpPost("{vetId}/CompleteVisit")]
        public async Task<ActionResult> CompleteVisit(CompleteVisitCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("{vetId}/AddVisitCard")]
        public async Task<ActionResult> AddVisitCard(AddVisitCardCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
