using AutoMapper;
using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Application.Extensions;
using Luftsborn.Domain.Entities;
using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.Note;
using Luftsborn.Dtos.Entities.Tag;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Notes.Queries.FilterNotes
{
    public class FilterNotesQueryHandler: IRequestHandler<FilterNotesQuery, Response<List<NoteBasicDto>>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public FilterNotesQueryHandler(INoteRepository noteRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Response<List<NoteBasicDto>>> Handle(FilterNotesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.GetCurrentUserId();
                if (userId != null) 
                {
                    var tags = await _noteRepository.FilterAsync(request.Filter, userId, request.TagId, request.IsDeleted);
                    var t = tags.ToList();
                    var orderedNotes = (_noteRepository.OrderBy(tags, request.OrderBy, request.IsAcending)).ToList();
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
