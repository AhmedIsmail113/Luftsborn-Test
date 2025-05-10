using Luftsborn.Dtos.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Notes.Commands.EditTitle
{
    public class EditTitleCommand : IRequest<Response<bool>>
    {
        public EditTitleCommand(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
