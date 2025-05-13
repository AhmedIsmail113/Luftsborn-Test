using AutoMapper;
using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Application.Extensions;
using Luftsborn.Domain.Entities;
using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.Note;
using Luftsborn.Dtos.Entities.Tag;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Queries.GetTagDetails
{
    public class GetTagDetailsQueryHandler : IRequestHandler<GetTagDetailsQuery, Response<TagDetailsDto>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetTagDetailsQueryHandler(ITagRepository tagRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Response<TagDetailsDto>> Handle(GetTagDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tag = (await _tagRepository.GetAsync(a => a.Id == request.Id)).IncludeProperties("Notes").FirstOrDefault();
                if (tag != null)
                {
                    var userId = _httpContextAccessor.GetCurrentUserId();
                    if (userId != null && userId == tag.CreatorUserId)
                    {
                        var returnedTag = _mapper.Map<TagDetailsDto>(tag);
                        return new Response<TagDetailsDto>() { Data = returnedTag, Status = true };
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
                return new Response<TagDetailsDto>() { Message = ex.Message, Status = false };
            }
        }
    }
}
