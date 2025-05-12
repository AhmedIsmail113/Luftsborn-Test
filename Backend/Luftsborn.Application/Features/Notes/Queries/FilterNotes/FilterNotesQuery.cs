using Luftsborn.Application.Common;
using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.Note;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Notes.Queries.FilterNotes
{
    public class FilterNotesQuery : BaseQueryRequest, IRequest<Response<List<NoteBasicDto>>>
    {
        public FilterNotesQuery(string? filter = null, Guid? userId = null, Guid? tagId = null, bool? isDeleted = false)
        {
            Filter = filter;
            UserId = userId;
            TagId = tagId;
            IsDeleted = isDeleted;
        }
        public FilterNotesQuery() : this(null, null, null, null)
        {

        }
        public string? Filter { get; set; }
        public Guid? UserId { get; set; }
        public Guid? TagId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
