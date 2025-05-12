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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateNoteCommandHandler(INoteRepository noteRepository, IHttpContextAccessor httpContextAccessor)
        {
            _noteRepository = noteRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<Guid>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.GetCurrentUserId();
                if (userId != null)
                {
                    var note = await _noteRepository.CreateAsync(new Note((Guid)userId, request.Title, request.Content));
                    return new Response<Guid>() { Data = note.Id, Status = true };
                }
                else 
                {
                    throw new Exception("UnAuthorized");
                }
            }
            catch (Exception ex)
            {
                return new Response<Guid>() { Message = ex.Message, Status = false };
            }
        }
    }
}
