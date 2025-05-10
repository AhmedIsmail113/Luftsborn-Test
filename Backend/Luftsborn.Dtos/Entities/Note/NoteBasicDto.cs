using Luftsborn.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Dtos.Entities.Note
{
    public class NoteBasicDto : IMapFrom<Domain.Entities.Note>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
