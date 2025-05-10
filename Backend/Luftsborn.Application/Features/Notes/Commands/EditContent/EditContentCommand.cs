using Luftsborn.Dtos.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Notes.Commands.EditContent
{
    public class EditContentCommand: IRequest<Response<bool>>
    {
        public EditContentCommand(Guid id, string contnet)
        {
            Id = id;
            Content = contnet;
        }
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}
