using Luftsborn.Dtos.Entities.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Dtos.Entities.Note
{
    public class NoteDetailsDto : NoteBasicDto
    {
        public TagBasicDto? Tag { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
