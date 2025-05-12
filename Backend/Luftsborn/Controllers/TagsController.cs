using Luftsborn.Application.Features.Tags.Commands.AddNote;
using Luftsborn.Application.Features.Tags.Commands.CreateTag;
using Luftsborn.Application.Features.Tags.Commands.DeleteTag;
using Luftsborn.Application.Features.Tags.Commands.RemoveNote;
using Luftsborn.Application.Features.Tags.Commands.RenameTag;
using Luftsborn.Application.Features.Tags.Queries.FilterTags;
using Luftsborn.Application.Features.Tags.Queries.FilterTagsPaginated;
using Luftsborn.Application.Features.Tags.Queries.GetTagDetails;
using Luftsborn.Application.Features.Users.Commands.Register;
using Luftsborn.Dtos.Common;
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
    public class TagsController : ControllerBase
    {
        [HttpPost("CreateTag")]
        [ProducesResponseType(typeof(Response<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTag(IMediator mediator, [FromBody] CreateTagCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("DeleteTag")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTag(IMediator mediator, [FromBody]  DeleteTagCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("DeleteTagPhysically")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTagPhysically(IMediator mediator, [FromBody] DeleteTagPhysicallyCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("RenameTag")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RenameTag(IMediator mediator, [FromBody] RenameTagCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("AddNote")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddNote(IMediator mediator, [FromBody] AddNoteCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("RemoveNote")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveNote(IMediator mediator, [FromBody] RemoveNoteCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet("FilterTags")]
        [ProducesResponseType(typeof(Response<List<TagBasicDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FilterTags(IMediator mediator, [FromQuery] FilterTagsQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpGet("FilterTagsPaginated")]
        [ProducesResponseType(typeof(Response<List<TagBasicDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FilterTagsPaginated(IMediator mediator, [FromQuery] FilterTagsPaginatedQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpGet("GetTagDetails")]
        [ProducesResponseType(typeof(Response<TagDetailsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTagDetails(IMediator mediator, [FromQuery] GetTagDetailsQuery query)
        {
            return Ok(await mediator.Send(query));
        }

    }
}
