using Nolan.Infra.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nolan.Infra.Repository.IRepositories
{

    public interface IEfBasicRepository<TEntity> : IEfBaseRepository<TEntity>
                where TEntity : Entity, IEfEntity<Guid>
    {
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        Task<int> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        Task<TEntity> GetAsync(Guid keyValue, Expression<Func<TEntity, dynamic>> navigationPropertyPath = null, bool writeDb = false, CancellationToken cancellationToken = default);
    }
}
