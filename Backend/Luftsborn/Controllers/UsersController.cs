using Luftsborn.Application.Features.Users.Commands.Login;
using Luftsborn.Application.Features.Users.Commands.RefreshToken;
using Luftsborn.Application.Features.Users.Commands.Register;
using Luftsborn.Dtos.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luftsborn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("Register")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(IMediator mediator, [FromBody] RegisterCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserTokenDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(IMediator mediator, [FromBody] LoginCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("RefreshToken")]
        [ProducesResponseType(typeof(UserTokenDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken(IMediator mediator, [FromBody] RefreshTokenCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
