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

namespace Luftsborn.Application.Features.Tags.Commands.DeleteTag
{
    public class DeleteTagPhysicallyCommandHandler: IRequestHandler<DeleteTagPhysicallyCommand, Response<bool>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeleteTagPhysicallyCommandHandler(ITagRepository tagRepository, IHttpContextAccessor httpContextAccessor)
        {
            _tagRepository = tagRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> Handle(DeleteTagPhysicallyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tag = (await _tagRepository.GetAsync(t => t.Id == request.Id)).FirstOrDefault();
                if (tag != null)
                {
                    var userId = _httpContextAccessor.GetCurrentUserId();
                    if (userId != null && userId == tag.CreatorUserId)
                    {
                        await _tagRepository.DeletePhysicallyAsync(tag.Id);
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
