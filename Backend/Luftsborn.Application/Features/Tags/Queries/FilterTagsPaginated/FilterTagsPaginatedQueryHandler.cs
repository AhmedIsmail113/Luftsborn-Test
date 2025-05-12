using AutoMapper;
using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Application.Extensions;
using Luftsborn.Application.Features.Tags.Queries.FilterTags;
using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.Tag;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Queries.FilterTagsPaginated
{
    public class FilterTagsPaginatedQueryHandler: IRequestHandler<FilterTagsPaginatedQuery, Response<List<TagBasicDto>>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public FilterTagsPaginatedQueryHandler(ITagRepository tagRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Response<List<TagBasicDto>>> Handle(FilterTagsPaginatedQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.GetCurrentUserId();
                if (userId != null)
                {
                    var tags = await _tagRepository.FilterAsync(request.Filter, userId);
                    var orderedTags = (_tagRepository.OrderBy(tags, request.OrderBy, request.IsAcending)).Paging(request.PageNumber, request.PageSize).ToList();
                    return new Response<List<TagBasicDto>>() { Data = _mapper.Map<List<TagBasicDto>>(orderedTags), Status = true };
                }
                else
                {
                    throw new Exception("UnAuthorized");
                }
            }
            catch (Exception ex)
            {
                return new Response<List<TagBasicDto>>() { Message = ex.Message, Status = false };
            }
        }
    }
}
