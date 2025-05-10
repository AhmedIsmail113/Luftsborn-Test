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

namespace Luftsborn.Application.Features.Notes.Commands.EditContent
{
    public class EditContentCommandHandler : IRequestHandler<EditContentCommand, Response<bool>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        public EditContentCommandHandler(INoteRepository noteRepository, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _noteRepository = noteRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<Response<bool>> Handle(EditContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var note = (await _noteRepository.GetAsync(n => n.Id == request.Id)).FirstOrDefault();
                if (note != null)
                {
                    note.EditContent(request.Content, GetCurrentUserId());
                    await _noteRepository.SaveChangesAsync();
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
