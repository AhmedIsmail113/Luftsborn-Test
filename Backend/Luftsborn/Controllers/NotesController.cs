using Luftsborn.Application.Features.Notes.Commands.CreateNote;
using Luftsborn.Application.Features.Notes.Commands.DeleteNote;
using Luftsborn.Application.Features.Notes.Commands.DeleteNotePhysically;
using Luftsborn.Application.Features.Notes.Commands.EditContent;
using Luftsborn.Application.Features.Notes.Commands.EditTitle;
using Luftsborn.Application.Features.Notes.Queries.FilterNotes;
using Luftsborn.Application.Features.Notes.Queries.FilterNotesPaginated;
using Luftsborn.Application.Features.Notes.Queries.GetNoteDetails;
using Luftsborn.Application.Features.Tags.Commands.CreateTag;
using Luftsborn.Application.Features.Tags.Queries.FilterTags;
using Luftsborn.Application.Features.Tags.Queries.GetTagDetails;
using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.Note;
using Luftsborn.Dtos.Entities.Tag;
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
        [HttpPut("EditTitle")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditTitle(IMediator mediator, [FromBody] EditTitleCommand command)
        {
            return Ok(await mediator.Send(command));
        }
        [HttpPut("EditContnet")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditContnet(IMediator mediator, [FromBody] EditContentCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("DeleteNote")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteNote(IMediator mediator, [FromBody] DeleteNoteCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("DeleteNotePhysically")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteNotePhysically(IMediator mediator, [FromBody] DeleteNotePhysicallyCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet("FilterNotes")]
        [ProducesResponseType(typeof(Response<List<NoteBasicDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FilterNotes(IMediator mediator, [FromQuery] FilterNotesQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpGet("FilterNotesPaginated")]
        [ProducesResponseType(typeof(Response<List<NoteBasicDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FilterNotesPaginated(IMediator mediator, [FromQuery] FilterNotesPaginatedQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpGet("GetNoteDetails")]
        [ProducesResponseType(typeof(Response<NoteDetailsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTagDetails(IMediator mediator, [FromQuery] GetNoteDetailsQuery query)
        {
            return Ok(await mediator.Send(query));
        }
    }
}
