using Luftsborn.Dtos.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Notes.Commands.DeleteNotePhysically
{
    public class DeleteNotePhysicallyCommand : IRequest<Response<bool>>
    {
        public DeleteNotePhysicallyCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
