using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Application.Users.Queries;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public UserController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(int[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUsername(int partyId) 
        {
            return Ok(await _mediator.Send(new UserPermissionsQuery(partyId)));
        }

    }
}