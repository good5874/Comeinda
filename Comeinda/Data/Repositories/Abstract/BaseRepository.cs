using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Comeinda.Data.Repositories.Abstract
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ComeindaDbContext context;
        protected readonly DbSet<TEntity> dbSet;

        public BaseRepository(ComeindaDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            dbSet = this.context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveByIdAsync(Guid id)
        {
            TEntity entity = await FindByIdAsync(id);
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> FindByIdAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
