using AutoMapper;
using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Application.Extensions;
using Luftsborn.Application.Features.Tags.Queries.GetTagDetails;
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

namespace Luftsborn.Application.Features.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery, Response<NoteDetailsDto>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetNoteDetailsQueryHandler(INoteRepository noteRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Response<NoteDetailsDto>> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var note = (await _noteRepository.GetAsync(a => a.Id == request.Id)).FirstOrDefault();
                if (note != null)
                {
                    var userId = _httpContextAccessor.GetCurrentUserId();
                    if (userId != null && userId == note.CreatorUserId)
                    {
                        var returnedNote = _mapper.Map<NoteDetailsDto>(note);
                        return new Response<NoteDetailsDto>() { Data = returnedNote, Status = true };
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
                return new Response<NoteDetailsDto>() { Message = ex.Message, Status = false };
            }
        }
    }
}
