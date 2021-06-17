using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Comeinda.Data.Repositories.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task RemoveByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindByIdAsync(Guid id);
        Task UpdateAsync(TEntity entity);
        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
