using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Domain.Entities;
using Luftsborn.Dtos.Common;
using MediatR;
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
        public CreateTagCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Response<Guid>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tag = await _tagRepository.CreateAsync(new Tag(request.Name));
                return new Response<Guid>() { Data = tag.Id, Status = true };
            }
            catch (Exception ex) 
            {
                return new Response<Guid>() { Message = ex.Message, Status = false };
            }
        }
    }
}
