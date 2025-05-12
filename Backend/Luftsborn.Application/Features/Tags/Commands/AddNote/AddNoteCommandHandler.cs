using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Application.Extensions;
using Luftsborn.Dtos.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Commands.AddNote
{
    public class AddNoteCommandHandler: IRequestHandler<AddNoteCommand, Response<bool>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddNoteCommandHandler(INoteRepository noteRepository, ITagRepository tagRepository, IHttpContextAccessor httpContextAccessor)
        {
            _noteRepository = noteRepository;
            _tagRepository = tagRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> Handle(AddNoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var note = (await _noteRepository.GetAsync(t => t.Id == request.NoteId)).FirstOrDefault();
                if (note != null) 
                {
                    var tag = (await _tagRepository.GetAsync(t => t.Id == request.TagId)).FirstOrDefault();
                    if (tag != null)
                    {
                        var userId = _httpContextAccessor.GetCurrentUserId();
                        if (userId != null && userId == note.CreatorUserId && userId == tag.CreatorUserId)
                        {
                            tag.AddNote(note);
                            await _tagRepository.SaveChangesAsync();
                            return new Response<bool>() { Data = true, Status = true };
                        }
                        else
                        {
                            throw new Exception("UnAuthorized");
                        }
                    }
                    else
                    {
                        throw new Exception("Not Found");
                    }
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
