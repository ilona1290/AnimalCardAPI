using AnimalCard.Application.User.Commands.ConfirmEmail;
using AnimalCard.Application.User.Commands.CreateUser;
using AnimalCard.Application.User.Commands.UpdateContactData;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AnimalCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    { 
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<ActionResult> CreateUser(CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("UpdateContact")]
        public async Task<ActionResult> UpdateUserContact(UpdateContactDataCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string activationToken)
        {
            ConfirmEmailCommand command = new ConfirmEmailCommand() { Email= email, ActivationToken = activationToken };
            return Ok(await _mediator.Send(command));
        }
    }
}
