using Luftsborn.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public Tag(string name)
        {
            Name = name;
            notes = new List<Note>();
        }
        private Tag(): this(string.Empty)
        {

        }
        public string Name { get; private set; }
        private IList<Note> notes;
        public IEnumerable<Note> Notes { get { return notes; } }

        public void RenameTag(string newName)
        {
            Name = newName;
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
