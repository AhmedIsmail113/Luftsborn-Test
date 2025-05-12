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

namespace Luftsborn.Application.Features.Notes.Commands.EditContent
{
    public class EditContentCommandHandler : IRequestHandler<EditContentCommand, Response<bool>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EditContentCommandHandler(INoteRepository noteRepository, IHttpContextAccessor httpContextAccessor)
        {
            _noteRepository = noteRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> Handle(EditContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var note = (await _noteRepository.GetAsync(n => n.Id == request.Id)).FirstOrDefault();
                if (note != null)
                {
                    var userId = _httpContextAccessor.GetCurrentUserId();
                    if (userId != null && userId == note.CreatorUserId) 
                    {                     
                        note.EditContent(request.Content, (Guid)userId);
                        await _noteRepository.SaveChangesAsync();
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
            catch (Exception ex) 
            {
                return new Response<bool>() { Message = ex.Message, Status = false };
            }
        }
    }
}
