using Luftsborn.Application.Common;
using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.Tag;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Queries.FilterTags
{
    public class FilterTagsQuery : BaseQueryRequest, IRequest<Response<List<TagBasicDto>>>
    {
        public FilterTagsQuery(string? filter = null, bool isDeleted = false)
        {
            Filter = filter;
            IsDeleted = isDeleted;
        }
        public FilterTagsQuery() : this(null, false)
        {

        }
        public string? Filter { get; set; }
        public bool IsDeleted { get; set; }

    }
}
