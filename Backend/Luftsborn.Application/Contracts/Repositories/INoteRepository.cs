using Luftsborn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Contracts.Repositories
{
    public interface INoteRepository : ICommonRepository<Note>
    {
        public Task<IQueryable<Note>> FilterAsync(string? filter = null, Guid? userId = null, Guid? tagId = null, bool? isDeleted = false);
    }
}
