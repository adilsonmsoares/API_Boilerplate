using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Models;

namespace Data.Repositories.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<object> AddAsync(TEntity entity);

        Task<bool> AddListAsync(IEnumerable<TEntity> entities);

        Task<bool> DeleteAsync(object id);

        Task<TEntity> GetAsync(object id);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> UpdateListAsync(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<bool> AddOrUpdate(TEntity entity);

        Task<bool> AddOrUpdateListAsync(IEnumerable<TEntity> list);

        Task<bool> DeleteListAsync(IEnumerable<int> ids);

        Task<TEntity> GetByAsync(string whereConditions, object parameters);
    }
}