using AutoMapper;
using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Application.Extensions;
using Luftsborn.Application.Features.Notes.Queries.FilterNotes;
using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.Note;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Notes.Queries.FilterNotesPaginated
{
    public class FilterNotesPaginatedQueryHandler : IRequestHandler<FilterNotesPaginatedQuery, Response<List<NoteBasicDto>>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public FilterNotesPaginatedQueryHandler(INoteRepository noteRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<Response<List<NoteBasicDto>>> Handle(FilterNotesPaginatedQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.GetCurrentUserId();
                if (userId != null)
                {
                    var tags = await _noteRepository.FilterAsync(request.Filter, userId, request.TagId, request.IsDeleted);
                    var orderedNotes = (_noteRepository.OrderBy(tags, request.OrderBy, request.IsAcending)).Paging(request.PageNumber, request.PageSize).ToList();
                    return new Response<List<NoteBasicDto>>() { Data = _mapper.Map<List<NoteBasicDto>>(orderedNotes), Status = true };
                }
                else
                {
                    throw new Exception("UnAuthorized");
                }

            }
            catch (Exception ex)
            {
                return new Response<List<NoteBasicDto>>() { Message = ex.Message, Status = false };
            }
        }
    }
}
