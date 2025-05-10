using Luftsborn.Application.Contracts.Repositories;
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
        private readonly UserManager<User> _userManager;
        public CreateNoteCommandHandler(INoteRepository noteRepository, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _noteRepository = noteRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<Response<Guid>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var note = await _noteRepository.CreateAsync(new Note(GetCurrentUserId(), request.Title, request.Content));
                return new Response<Guid>() { Data = note.Id, Status = true };
            }
            catch (Exception ex)
            {
                return new Response<Guid>() { Message = ex.Message, Status = false };
            }
        }
        private Guid GetCurrentUserId()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
            if (userEmail == null)
            {
                throw new Exception("Invalid credentials");
            }
            else
            {
                var user = _userManager.FindByEmailAsync(userEmail).Result;
                if (user == null)
                {
                    throw new Exception("Invalid credentials");
                }
                else
                {
                    return user.Id;
                }
            }
        }
    }
}
