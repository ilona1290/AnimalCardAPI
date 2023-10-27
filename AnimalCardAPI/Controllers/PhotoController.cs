using AnimalCard.Application.ProfilePicture.Commands.UpdateProfilePicture;
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
    public class PhotoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PhotoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("UpdatePhoto")]
        public async Task<ActionResult> UpdatePhoto(UpdateProfilePictureCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
