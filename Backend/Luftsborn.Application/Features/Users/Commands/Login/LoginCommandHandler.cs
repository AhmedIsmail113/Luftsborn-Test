using Luftsborn.Domain.Entities;
using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<UserTokenDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public LoginCommandHandler(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<Response<UserTokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(a => a.Email == request.Email);
                if (user == null)
                {
                    throw new Exception("There is no user with this email");
                }
                if (await _userManager.IsLockedOutAsync(user))
                {
                    throw new Exception("Try again later");
                }
                var isAuthenticated = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!isAuthenticated)
                {
                    await _userManager.AccessFailedAsync(user);
                    throw new Exception("Wrong Password");
                }
                var userClaims = await _userManager.GetClaimsAsync(user);

                //Generate Key
                var secretKey = _configuration.GetRequiredSection("SecretKey").Value;
                var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey!);
                var key = new SymmetricSecurityKey(secretKeyInBytes);

                //Hashing Algorithm 
                var usedMethodForGeneratingToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                //Generate Token
                var exp = DateTime.UtcNow.AddHours(1);
                var jwt = new JwtSecurityToken
                    (
                        claims: userClaims,
                        notBefore: DateTime.UtcNow,
                        issuer: "Luftsborn-BackEnd",
                        expires: exp,
                        signingCredentials: usedMethodForGeneratingToken
                    );
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.WriteToken(jwt);
                user.SetToken(token);
                var refreshToken = Guid.NewGuid();
                user.SetRefreshToken(refreshToken);
                user.SetRefreshTokenExpiryDate(DateTime.UtcNow.AddMonths(1));
                var updatingResult = await _userManager.UpdateAsync(user);
                if (updatingResult.Succeeded)
                {
                    var userToken = new UserTokenDto() { Id = user.Id, Token = token, RefreshToken = refreshToken, ExpiryDate = exp };
                    return new Response<UserTokenDto>() { Data = userToken, Status = true };
                }
                else
                {
                    throw new Exception("Try again later");
                }  
            }
            catch (Exception ex)
            {
                return new Response<UserTokenDto>() { Message = ex.Message, Status = false };
            }
        }
    }
}
