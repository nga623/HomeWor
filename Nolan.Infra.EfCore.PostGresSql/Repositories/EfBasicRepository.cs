using Microsoft.EntityFrameworkCore;
using Nolan.Infra.Repository;
using Nolan.Infra.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nolan.Infra.EfCore.PostGresSql.Repositories
{
    /// <summary>
    /// Ef简单的、基础的，初级的仓储接口
    /// 适合DDD开发模式,实体必须继承AggregateRoot
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class EfBasicRepository<TEntity> : AbstractEfBaseRepository<NolanDbContext, TEntity>, IEfBasicRepository<TEntity>
            where TEntity : Entity, IEntity<Guid>
    {
        public EfBasicRepository(NolanDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            this.DbContext.UpdateRange(entities);
            return await this.DbContext.SaveChangesAsync(cancellationToken);
        }

         

        public async Task<int> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.DbContext.Remove(entity);
            return await this.DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            this.DbContext.RemoveRange(entities);
            return await this.DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> GetAsync(Guid keyValue, Expression<Func<TEntity, dynamic>> navigationPropertyPath = null, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            var query = this.GetDbSet(writeDb, false).Where(t => t.Id == keyValue);
            if (navigationPropertyPath == null)
                return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, cancellationToken);
            else
                return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(EntityFrameworkQueryableExtensions.Include(query, navigationPropertyPath), cancellationToken);
        }

        public async Task<TEntity> GetAsync(Guid keyValue, IEnumerable<Expression<Func<TEntity, dynamic>>> navigationPropertyPaths = null, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            if (navigationPropertyPaths == null || navigationPropertyPaths.Count() <= 1)
                return await this.GetAsync(keyValue, navigationPropertyPaths.FirstOrDefault(), writeDb, cancellationToken);

            var query = this.GetDbSet(writeDb, false).Where(t => t.Id == keyValue);
            foreach (var navigationPath in navigationPropertyPaths)
            {
                query = EntityFrameworkQueryableExtensions.Include(query, navigationPath);
            }
            return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, cancellationToken);
        }

         
    }
}
