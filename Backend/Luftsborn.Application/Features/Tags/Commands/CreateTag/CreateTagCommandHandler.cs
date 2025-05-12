using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Application.Extensions;
using Luftsborn.Domain.Entities;
using Luftsborn.Dtos.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Response<Guid>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateTagCommandHandler(ITagRepository tagRepository, IHttpContextAccessor httpContextAccessor)
        {
            _tagRepository = tagRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<Guid>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.GetCurrentUserId();
                if(userId != null)
                {
                    var tag = await _tagRepository.CreateAsync(new Tag((Guid)userId, request.Name));
                    return new Response<Guid>() { Data = tag.Id, Status = true };
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
