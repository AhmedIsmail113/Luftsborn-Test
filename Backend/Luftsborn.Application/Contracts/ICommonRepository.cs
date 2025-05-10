using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Contracts
{
    public interface ICommonRepository<TEntity>
    {
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);
        Task<TEntity> CreateAsync(TEntity entity);
        IQueryable<TEntity> OrderBy(IQueryable<TEntity> entities, string? orderBy, bool isAccending = true);
        Task<bool> DeletePhysicallyAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
