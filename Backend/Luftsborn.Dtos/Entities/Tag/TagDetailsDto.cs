using Luftsborn.Dtos.Entities.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Dtos.Entities.Tag
{
    public class TagDetailsDto : TagBasicDto
    {
        public List<NoteBasicDto>? Notes;
    }
}
