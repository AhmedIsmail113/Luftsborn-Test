using Luftsborn.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Infrastructure.Managers
{ 
    public abstract class CommonRepository<TEntity> : ICommonRepository<TEntity> where TEntity : class
    {
        private readonly LuftsbornDbContext _context;
        public CommonRepository(LuftsbornDbContext context)
        {
            _context = context;
        }
        public virtual async Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await Task.FromResult(query);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).Reload();
            return entity;
        }

        public virtual async Task<bool> DeletePhysicallyAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity is null)
            {
                return false;
            }
            else
            {
                _context.Set<TEntity>().Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }

        }
        public abstract IQueryable<TEntity> OrderBy(IQueryable<TEntity> entities, string? orderBy, bool isAccending = true);

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
    
}
