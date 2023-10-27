using AnimalCard.Application.Admin.Commands.ConfirmOrRejectCustoms;
using AnimalCard.Application.Admin.Queries.GetCustomsToConfirm;
using AnimalCard.Application.Vet.Queries.GetDiseasesAndServicesTreatments;
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
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetDiseasesAndServicesTreatmentsToConfirm")]
        public async Task<ActionResult<CustomsToConfirmVm>> GetDiseasesAndServicesTreatmentsToConfirm()
        {
            return Ok(await _mediator.Send(new GetCustomsToConfirmQuery()));
        }

        [HttpPost("ConfirmOrRejectCustoms")]
        public async Task<ActionResult<bool>> ConfirmOrRejectCustoms(ConfirmOrRejectCustomsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
