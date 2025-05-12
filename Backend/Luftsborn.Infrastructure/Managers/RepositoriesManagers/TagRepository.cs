using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Infrastructure.Managers.RepositoriesManagers
{
    public class TagRepository : CommonRepository<Tag>, ITagRepository
    {
        public TagRepository(LuftsbornDbContext context): base(context)
        {
            
        }
        public async Task<IQueryable<Tag>> FilterAsync(string? filter = null, Guid? userId = null)
        {
            return (await GetAsync()).Where(a => filter == null || a.Name.ToLower().Contains(filter.ToLower()))
                .Where(a => userId == null || a.CreatorUserId == userId);
        }

        public override IQueryable<Tag> OrderBy(IQueryable<Tag> entities, string? orderBy, bool isAccending = true)
        {
            if (orderBy != null)
            {
                switch (orderBy?.ToLower())
                {
                    case "name":
                        entities = isAccending ? entities.OrderBy(a => a.Name) : entities.OrderByDescending(a => a.Name);
                        break;
                }
            }
            return entities;
        }
    }
}
