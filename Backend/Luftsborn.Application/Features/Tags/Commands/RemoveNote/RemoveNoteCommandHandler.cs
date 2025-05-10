using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Application.Features.Tags.Commands.AddNote;
using Luftsborn.Dtos.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Commands.RemoveNote
{
    public class RemoveNoteCommandHandler : IRequestHandler<RemoveNoteCommand, Response<bool>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly ITagRepository _tagRepository;
        public RemoveNoteCommandHandler(INoteRepository noteRepository, ITagRepository tagRepository)
        {
            _noteRepository = noteRepository;
            _tagRepository = tagRepository;
        }

        public async Task<Response<bool>> Handle(RemoveNoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tag = (await _tagRepository.GetAsync(t => t.Id == request.TagId)).FirstOrDefault();
                if (tag != null)
                {
                    tag.RemoveNote(request.NoteId);
                    await _tagRepository.SaveChangesAsync();
                    return new Response<bool>() { Data = true, Status = true };
                }
                else
                {
                    throw new Exception("Not Found");
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>() { Message = ex.Message, Status = false };
            }
        }
    }
}
