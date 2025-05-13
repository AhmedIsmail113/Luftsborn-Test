using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.Note;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQuery : IRequest<Response<NoteDetailsDto>>
    {
        public GetNoteDetailsQuery(Guid id)
        {
            Id = id;
        }
        public GetNoteDetailsQuery(): this(Guid.Empty)
        {
            
        }
        public Guid Id { get; set; }
    }
}
