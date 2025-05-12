using Luftsborn.Dtos.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommand: IRequest<Response<bool>>
    {
        public DeleteNoteCommand(Guid id)
        {
            Id = id;   
        }
        public Guid Id { get; set; }
    }
}
