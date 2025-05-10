using Luftsborn.Domain.Entities;
using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Response<UserTokenDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommandHandler(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<Response<UserTokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var inputToken = new JwtSecurityTokenHandler().ReadJwtToken(request.Token);
                var userEmail = inputToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value;
                var user = _userManager.Users.FirstOrDefault(a => a.Email == userEmail);
                if (user == null)
                {
                    throw new Exception("Invalid Token");
                }
                if (user.Token != request.Token || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryDate < DateTime.UtcNow)
                {
                    throw new Exception("Invalid Token");
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
                        issuer: "Quizoo-BackEnd",
                        expires: exp,
                        signingCredentials: usedMethodForGeneratingToken
                    );
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.WriteToken(jwt);
                var refreshToken = Guid.NewGuid();
                user.SetRefreshToken(refreshToken);
                user.SetRefreshTokenExpiryDate(DateTime.UtcNow.AddMonths(1));
                var updatingResult = await _userManager.UpdateAsync(user);
                if (updatingResult.Succeeded)
                {
                    var userToken = new UserTokenDto() { Id = user.Id, Token = token, RefreshToken = user.RefreshToken, ExpiryDate = exp };
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
