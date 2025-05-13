using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.Tag;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Queries.GetTagDetails
{
    public class GetTagDetailsQuery : IRequest<Response<TagDetailsDto>>
    {
        public GetTagDetailsQuery(Guid id)
        {
            Id = id;
        }
        public GetTagDetailsQuery() : this(Guid.Empty)
        {
            
        }
        public Guid Id { get; set; }
    }
}
