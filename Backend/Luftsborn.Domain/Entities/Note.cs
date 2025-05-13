using Luftsborn.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Domain.Entities
{
    public class Note : ModifiableEntity
    {
        public Note(Guid creatorUserId, string title, string content, Tag tag): base(creatorUserId)
        {
            Title = title;
            Content = content;
            Tag = tag;
            CreatedOn = DateTimeOffset.Now;
            ModifiedOn = DateTimeOffset.Now;
        }
        private Note(): this(Guid.Empty, string.Empty,string.Empty, null!)
        {

        }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public Tag Tag { get; private set; }

        public void EditTitle(string title, Guid modifierUserId)
        {
            Title = title;
            ModifiedOn = new DateTimeOffset(DateTime.Now);
            ModifierUserId = modifierUserId;
        }
        public void EditContent(string content, Guid modifierUserId)
        {
            Content = content;
            ModifiedOn = new DateTimeOffset(DateTime.Now);
            ModifierUserId = modifierUserId;
        }
        public void Delete(Guid deletingUserId) 
        { 
            IsDeleted = true;
            DeletedOn = DateTimeOffset.Now;
            DeletingUserId = deletingUserId;
        }
    }
}
