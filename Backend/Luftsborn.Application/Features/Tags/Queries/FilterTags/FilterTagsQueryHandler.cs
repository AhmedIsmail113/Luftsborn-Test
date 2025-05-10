using AutoMapper;
using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.Tag;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Queries.FilterTags
{
    public class FilterTagsQueryHandler : IRequestHandler<FilterTagsQuery, Response<List<TagBasicDto>>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        public FilterTagsQueryHandler(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<TagBasicDto>>> Handle(FilterTagsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tags = await _tagRepository.FilterAsync(request.Filter);
                var orderedTags = (_tagRepository.OrderBy(tags, request.OrderBy, request.IsAcending)).ToList();
                return new Response<List<TagBasicDto>>() { Data = _mapper.Map<List<TagBasicDto>>(orderedTags), Status = true };

            }
            catch (Exception ex)
            {
                return new Response<List<TagBasicDto>>() { Message = ex.Message, Status = false };
            }
        }
    }
}
