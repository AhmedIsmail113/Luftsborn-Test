using Luftsborn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Contracts.Repositories
{
    public interface ITagRepository : ICommonRepository<Tag>
    {
        public Task<IQueryable<Tag>> FilterAsync(string? filter = null, Guid? userId = null, bool? isDeleted = false);
    }
}
