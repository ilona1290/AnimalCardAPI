using AnimalCard.Application.Pet.Commands.AddPet;
using AnimalCard.Application.Pet.Commands.AddResearchResult;
using AnimalCard.Application.Pet.Commands.UpdateAnimalCard;
using AnimalCard.Application.Pet.Queries.GetPet;
using AnimalCard.Application.Pet.Queries.GetPetCompletedVisits;
using AnimalCard.Application.Pet.Queries.GetPetOtherVaccinations;
using AnimalCard.Application.Pet.Queries.GetPetRabiesVaccinations;
using AnimalCard.Application.Pet.Queries.GetPetResearches;
using AnimalCard.Application.Pet.Queries.GetPetsByUserRole;
using AnimalCard.Application.Pet.Queries.GetPetTreatedDiseases;
using AnimalCard.Application.Pet.Queries.GetPetTreatments;
using AnimalCard.Application.Pet.Queries.GetPetWeights;
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
    public class PetController : ControllerBase
    {

        private readonly IMediator _mediator;
        public PetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddPet")]
        public async Task<ActionResult> AddPet(AddPetCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("UpdatePet")]
        public async Task<ActionResult> UpdatePet(UpdateAnimalCardCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("GetPetsByUserRole/{role}/{userId}")]
        public async Task<ActionResult<UserPetsVm>> GetPetsByUserRole(string role, int userId)
        {
            return Ok(await _mediator.Send(new GetPetsByUserRoleQuery { Role = role, UserId = userId}));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserPetsVm>> GetPet(int id)
        {
            return Ok(await _mediator.Send(new GetPetQuery { PetId = id }));
        }

        [HttpGet("{id}/CompletedVisits")]
        public async Task<ActionResult<PetCompletedVisitsVm>> GetCompletedVisits(int id)
        {
            return Ok(await _mediator.Send(new GetPetCompletedVisitsQuery { PetId = id }));
        }

        [HttpGet("{id}/RabiesVaccinations")]
        public async Task<ActionResult<PetRabiesVaccinationsVm>> GetRabiesVaccinations(int id)
        {
            return Ok(await _mediator.Send(new GetPetRabiesVaccinationsQuery { PetId = id }));
        }

        [HttpGet("{id}/OtherVaccinations")]
        public async Task<ActionResult<PetOtherVaccinationsVm>> GetOtherVaccinations(int id)
        {
            return Ok(await _mediator.Send(new GetPetOtherVaccinationsQuery { PetId = id }));
        }

        [HttpGet("{id}/TreatedDiseases")]
        public async Task<ActionResult<PetTreatedDiseasesVm>> GetTreatedDiseases(int id)
        {
            return Ok(await _mediator.Send(new GetPetTreatedDiseasesQuery { PetId = id }));
        }

        [HttpGet("{id}/Treatments")]
        public async Task<ActionResult<PetTreatmentsVm>> GetTreatments(int id)
        {
            return Ok(await _mediator.Send(new GetPetTreatmentsQuery { PetId = id }));
        }

        [HttpGet("{id}/Researches")]
        public async Task<ActionResult<PetResearchesVm>> GetResearches(int id)
        {
            return Ok(await _mediator.Send(new GetPetResearchesQuery { PetId = id }));
        }

        [HttpPost("AddResearchResult")]
        public async Task<ActionResult> AddResult(AddResearchResultCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{id}/Weights")]
        public async Task<ActionResult<PetWeightsVm>> GetWeights(int id)
        {
            return Ok(await _mediator.Send(new GetPetWeightsQuery { PetId = id }));
        }
    }
}
