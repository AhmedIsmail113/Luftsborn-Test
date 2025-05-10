using Luftsborn.Dtos.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagCommand : IRequest<Response<Guid>>
    {
        public CreateTagCommand(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
