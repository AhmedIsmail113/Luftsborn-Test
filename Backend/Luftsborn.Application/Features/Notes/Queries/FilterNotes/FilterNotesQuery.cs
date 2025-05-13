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
        public FilterNotesQuery(string? filter = null, Guid? tagId = null, bool isDeleted = false)
        {
            Filter = filter;
            TagId = tagId;
            IsDeleted = isDeleted;
        }
        public FilterNotesQuery() : this(null, null, false)
        {

        }
        public string? Filter { get; set; }
        public Guid? TagId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
