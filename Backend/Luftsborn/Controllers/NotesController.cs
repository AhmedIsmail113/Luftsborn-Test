using Luftsborn.Application.Features.Notes.Commands.CreateNote;
using Luftsborn.Application.Features.Notes.Commands.EditContent;
using Luftsborn.Application.Features.Notes.Commands.EditTitle;
using Luftsborn.Application.Features.Tags.Commands.CreateTag;
using Luftsborn.Dtos.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luftsborn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        [HttpPost("CreateNote")]
        [ProducesResponseType(typeof(Response<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTag(IMediator mediator, [FromBody] CreateNoteCommand command)
        {
            return Ok(await mediator.Send(command));
        }
        [HttpPost("EditTitle")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditTitle(IMediator mediator, [FromBody] EditTitleCommand command)
        {
            return Ok(await mediator.Send(command));
        }
        [HttpPost("EditContnet")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditContnet(IMediator mediator, [FromBody] EditContentCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
