using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Infrastructure.Managers.RepositoriesManagers
{
    public class NoteRepository : CommonRepository<Note>, INoteRepository
    {
        public NoteRepository(LuftsbornDbContext context): base(context)
        {
            
        }

        public async Task<IQueryable<Note>> FilterAsync(string? filter = null, Guid? userId = null, bool? isDeleted = false)
        {
            return (await GetAsync()).Where(a => filter == null || a.Title.ToLower().Contains(filter.ToLower()))
                .Where(a => userId == null || a.CreatorUserId == userId)
                .Where(a => a.IsDeleted == isDeleted);
        }

        public override IQueryable<Note> OrderBy(IQueryable<Note> entities, string? orderBy, bool isAccending = true)
        {
            if (orderBy != null)
            {
                switch (orderBy?.ToLower())
                {
                    case "title":
                        entities = isAccending ? entities.OrderBy(a => a.Title) : entities.OrderByDescending(a => a.Title);
                        break;
                    case "date":
                        entities = isAccending ? entities.OrderBy(a => a.ModifiedOn) : entities.OrderByDescending (a => a.ModifiedOn); 
                        break;
                }
            }
            return entities;
        }
    }
}
