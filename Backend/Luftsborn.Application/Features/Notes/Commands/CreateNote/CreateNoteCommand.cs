using Luftsborn.Dtos.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Notes.Commands.CreateNote
{
    public class CreateNoteCommand : IRequest<Response<Guid>>
    {
        public CreateNoteCommand(string title, string content)
        {
            Title = title;
            Content = content;
        }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
