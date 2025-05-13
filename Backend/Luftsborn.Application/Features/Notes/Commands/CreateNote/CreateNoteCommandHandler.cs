using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Application.Extensions;
using Luftsborn.Domain.Entities;
using Luftsborn.Dtos.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Response<Guid>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateNoteCommandHandler(INoteRepository noteRepository, ITagRepository tagRepository, IHttpContextAccessor httpContextAccessor)
        {
            _noteRepository = noteRepository;
            _tagRepository = tagRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<Guid>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tag = (await _tagRepository.GetAsync(t => t.Id == request.TagId)).FirstOrDefault();
                if (tag != null)
                {
                    var userId = _httpContextAccessor.GetCurrentUserId();
                    if (userId != null && userId == tag.CreatorUserId)
                    {
                        var note = await _noteRepository.CreateAsync(new Note((Guid)userId, request.Title, request.Content, tag));
                        return new Response<Guid>() { Data = note.Id, Status = true };
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
            catch (Exception ex)
            {
                return new Response<Guid>() { Message = ex.Message, Status = false };
            }
        }
    }
}
