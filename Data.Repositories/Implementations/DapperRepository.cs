using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Dapper.Contrib.Extensions;
using Data.Repositories.Contracts;
using Domain.Entities.Extensions;
using Microsoft.Extensions.Configuration;


namespace Data.Repositories.Implementations
{
    public class DapperRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IConfiguration Configuration;
        internal IDbConnection Connection => new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));

        public DapperRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<object> AddAsync(TEntity entity)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var result = await conn.InsertAsync(entity);
                conn.Close();
                return result;
            }
        }

        public async Task<bool> AddListAsync(IEnumerable<TEntity> entities)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    foreach (var entity in entities)
                    {
                        await conn.InsertAsync(entity);
                    }

                    scope.Complete();
                    conn.Close();

                    return true;
                }
            }
        }

        public async Task<bool> DeleteAsync(object id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var tableName = typeof(TEntity).GetTableName();
                string query = string.Format("UPDATE [{0}].[{1}] SET [IsActive] = 0 WHERE [ID] = @Id", tableName.Schema, tableName.Name);

                bool result = await conn.ExecuteAsync(query,
                    param: new
                    {
                        Id = id
                    }) > 0;

                conn.Close();

                return result;
            }
        }

        public async Task<TEntity> GetAsync(object id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var result = await conn.GetAsync<TEntity>(id);
                conn.Close();
                return result;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var result = await conn.UpdateAsync(entity);
                conn.Close();

                return result;
            }
        }

        public async Task<bool> UpdateListAsync(IEnumerable<TEntity> entities)
        {
            using (var conn = Connection)
            {
                var success = true;
                conn.Open();

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    foreach (var entity in entities)
                    {
                        var updated = await conn.UpdateAsync(entity);

                        if (updated)
                        {
                            continue;
                        }

                        success = false;
                    }

                    scope.Complete();
                }

                conn.Close();

                return success;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (var conn = Connection)
            {
                conn.Open();
                var result = await conn.GetAllAsync<TEntity>();
                conn.Close();

                return result;
            }
        }

        public async Task<bool> AddOrUpdate(TEntity entity)
        {
            var updateEntityResult = await UpdateAsync(entity);

            if (updateEntityResult)
            {
                return true;
            }

            var inserted = await AddAsync(entity);

            return inserted != null;
        }

        public async Task<bool> AddOrUpdateListAsync(IEnumerable<TEntity> list)
        {
            var success = true;

            foreach (var entity in list)
            {
                var saved = await AddOrUpdate(entity);

                if (saved)
                {
                    continue;
                }

                success = false;
                break;
            }

            return success;
        }

        public async Task<bool> DeleteListAsync(IEnumerable<int> ids)
        {
            var success = true;

            foreach (var entity in ids)
            {
                var deleted = await DeleteAsync(entity);

                if (deleted)
                {
                    continue;
                }

                success = false;
                break;
            }

            return success;
        }

        public async Task<TEntity> GetByAsync(string whereConditions, object parameters)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var tableName = typeof(TEntity).GetTableName();
                string query = string.Format("SELECT * FROM [{0}].[{1}]", tableName.Schema, tableName.Name);

                if (!string.IsNullOrEmpty(whereConditions))
                {
                    query += $" WHERE {whereConditions}";
                }

                var result = await conn.QueryAsync(query, parameters);

                conn.Close();

                return result.FirstOrDefault();
            }
        }
    }
}