using Luftsborn.Dtos.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Commands.DeleteTag
{
    public class DeleteTagPhysicallyCommand : IRequest<Response<bool>>
    {
        public DeleteTagPhysicallyCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
