using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Application.Features.Tags.Commands.DeleteTag;
using Luftsborn.Dtos.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Commands.RenameTag
{
    public class RenameTagCommandHandler: IRequestHandler<RenameTagCommand, Response<bool>>
    {
        private readonly ITagRepository _tagRepository;
        public RenameTagCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Response<bool>> Handle(RenameTagCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tag = (await _tagRepository.GetAsync(t => t.Id == request.Id)).FirstOrDefault();
                if (tag != null)
                {
                    tag.RenameTag(request.Name);
                    await _tagRepository.SaveChangesAsync();
                    return new Response<bool>() { Data = true, Status = true };
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
