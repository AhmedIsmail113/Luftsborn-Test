using Luftsborn.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Domain.Entities
{
    public class Tag : ModifiableEntity
    {
        public Tag(Guid creatorUserId, string name):base(creatorUserId)
        {
            Name = name;
            notes = new List<Note>();
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
        private Tag(): this(Guid.Empty, string.Empty)
        {

        }
        public string Name { get; private set; }
        private IList<Note> notes;
        public IEnumerable<Note> Notes { get { return notes; } }

        public void RenameTag(string newName, Guid modifierUserId)
        {
            Name = newName;
            ModifiedOn = new DateTimeOffset(DateTime.Now);
            ModifierUserId = modifierUserId;
        }
        public void Delete(Guid deletingUserId)
        {
            IsDeleted = true;
            DeletedOn = DateTimeOffset.Now;
            DeletingUserId = deletingUserId;
        }
        public void AddNote(Note note) 
        { 
            notes.Add(note);
        }
        public void RemoveNote(Guid noteId) 
        {
            var note = notes.FirstOrDefault(x => x.Id == noteId);
            if (note != null)
            {
                notes.Remove(note);
            }
            else 
            {
                throw new Exception($"There is no note with this id: {noteId}");
            }
        }
    }
}
