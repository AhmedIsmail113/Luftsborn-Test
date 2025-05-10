using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Domain.Common
{
    public class ModifiableEntity : BaseEntity
    {
        public ModifiableEntity(Guid creatorUserId)
        {
            CreatorUserId = creatorUserId;
            CreatedOn = DateTimeOffset.Now;
            IsDeleted = false;
        }
        public Guid CreatorUserId { get; set; }
        public Guid? ModifierUserId { get; set; }
        public Guid? DeletingUserId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
