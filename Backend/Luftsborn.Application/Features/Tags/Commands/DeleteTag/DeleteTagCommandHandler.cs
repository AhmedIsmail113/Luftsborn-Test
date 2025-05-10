using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Dtos.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandHandler: IRequestHandler<DeleteTagCommand, Response<bool>>
    {
        private readonly ITagRepository _tagRepository;
        public DeleteTagCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Response<bool>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tag = (await _tagRepository.GetAsync(t => t.Id == request.Id)).FirstOrDefault();
                if (tag != null)
                {
                    var deleted = await _tagRepository.DeletePhysicallyAsync(tag.Id);
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
