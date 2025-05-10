using Luftsborn.Domain.Entities;
using Luftsborn.Dtos.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<Guid>>
    {
        private readonly UserManager<User> _userManager;
        public RegisterCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userName = request.FirstName + " " + request.LastName;
                var newUser = new User(userName, request.Email);    
                var creationResult = await _userManager.CreateAsync(newUser, request.Password);
                if (creationResult.Succeeded)
                {
                    var userClaims = new List<Claim>
                {
                    new Claim (ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Email, newUser.Email!),
                };
                    await _userManager.AddClaimsAsync(newUser, userClaims);
                    return new Response<Guid>() { Data = newUser.Id, Status = true };
                }
                else
                {
                    throw new Exception(creationResult.Errors.FirstOrDefault()!.Description);
                }
            }
            catch (Exception ex)
            {
                return new Response<Guid>() { Message = ex.Message, Status = false };
            }
        }
    }
}
