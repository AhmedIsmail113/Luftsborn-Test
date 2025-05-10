using Luftsborn.Dtos.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Commands.RemoveNote
{
    public class RemoveNoteCommand: IRequest<Response<bool>>
    {
        public RemoveNoteCommand(Guid noteId, Guid tagId)
        {
            NoteId = noteId;
            TagId = tagId;
        }
        public Guid NoteId { get; set; }
        public Guid TagId { get; set; }
    }
}
