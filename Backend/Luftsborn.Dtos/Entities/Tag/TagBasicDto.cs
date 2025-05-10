using Luftsborn.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Dtos.Entities.Tag
{
    public class TagBasicDto : IMapFrom<Domain.Entities.Tag>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
