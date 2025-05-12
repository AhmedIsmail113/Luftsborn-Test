using Luftsborn.Application.Common;
using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.Tag;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Queries.FilterTagsPaginated
{
    public class FilterTagsPaginatedQuery : PageRequest, IRequest<Response<List<TagBasicDto>>>
    {
        public FilterTagsPaginatedQuery(string? filter = null)
        {
            Filter = filter;
        }
        public FilterTagsPaginatedQuery() : this(null)
        {

        }
        public string? Filter { get; set; }
    }
}
