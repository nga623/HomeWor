using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Nolan.Infra.Repository;
using Nolan.Infra.Repository.IRepositories;
using Nolan.Infra.Repository.IRepositories.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nolan.Infra.EfCore.PostGresSql.Repositories
{
    /// <summary>
    /// Ef仓储的基类实现,抽象类
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class AbstractEfBaseRepository<TDbContext, TEntity> : IEfBaseRepository<TEntity>
       where TDbContext : DbContext
       where TEntity : Entity//, IEfEntity<string>
    {
        protected virtual TDbContext DbContext { get; }

        protected AbstractEfBaseRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
        /// <summary>
        /// 获取事务
        /// </summary>
        /// <returns></returns>
        public async Task<IDbContextTransaction> GetTransactionAsync()
        {
            return await DbContext.Database.BeginTransactionAsync();
        }

        protected virtual IQueryable<TEntity> GetDbSet(bool writeDb, bool noTracking)
        {
            //if (noTracking && writeDb)
            //    return DbContext.Set<TEntity>().AsNoTracking().TagWith(EfCoreConsts.MAXSCALE_ROUTE_TO_MASTER);
            //else if (noTracking)
            //    return DbContext.Set<TEntity>().AsNoTracking();
            //else if (writeDb)
            //    return DbContext.Set<TEntity>().TagWith(EfCoreConsts.MAXSCALE_ROUTE_TO_MASTER);
            //else
                return DbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, bool writeDb = false, bool noTracking = true)
        {
            return DbContext.Set<TEntity>().Where(expression); //this.GetDbSet(writeDb, noTracking).Where(expression);
        }

        /// <summary>
        ///重要的事情说3遍 事务里不要使用这个方法查询 事务里不要使用这个方法查询 事务里不要使用这个方法查询
        /// </summary>
        //public virtual async Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, bool writeDb = false)
        //{
        //    if (writeDb)
        //        sql = string.Concat("/* ", EfCoreConsts.MAXSCALE_ROUTE_TO_MASTER, " */", sql);
        //    var result = await DbContext.Database.GetDbConnection().QueryAsync<TResult>(sql, param, null, commandTimeout, commandType);

        //    return result;
        //}

        //public virtual async Task<List<TResult>> QueryListAsync<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, bool writeDb = false)
        //{
        //    if (writeDb)
        //        sql = string.Concat("/* ", EfCoreConsts.MAXSCALE_ROUTE_TO_MASTER, " */", sql);
        //    var result = await DbContext.Database.GetDbConnection().QueryAsync<TResult>(sql, param, null, commandTimeout, commandType);

        //    return result.ToList();
        //}

        /// <summary>
        ///重要的事情说3遍 事务里不要使用这个方法查询 事务里不要使用这个方法查询 事务里不要使用这个方法查询
        /// </summary>
        //public virtual async Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, bool writeDb = false)
        //{
        //    if (writeDb)
        //        sql = string.Concat("/* ", EfCoreConsts.MAXSCALE_ROUTE_TO_MASTER, " */", sql);
        //    var result = await DbContext.Database.GetDbConnection().QueryFirstOrDefaultAsync<TResult>(sql, param, null, commandTimeout, commandType);

        //    return result;
        //}

        public virtual async Task<bool> ExecuteAsync(string sql, CancellationToken cancellationToken = default)
        {
            var result = await DbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
            return await DbContext.SaveChangesAsync(cancellationToken) >= 0;
        }


        public virtual async Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().AddRangeAsync(entities);
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            var dbSet = DbContext.Set<TEntity>().AsNoTracking();
            //if (writeDb)
            //    dbSet = dbSet.TagWith(EfCoreConsts.MAXSCALE_ROUTE_TO_MASTER);
            return await EntityFrameworkQueryableExtensions.AnyAsync(dbSet, whereExpression, cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            var dbSet = DbContext.Set<TEntity>().AsNoTracking();
            //if (writeDb)
            //    dbSet = dbSet.TagWith(EfCoreConsts.MAXSCALE_ROUTE_TO_MASTER);
            return await EntityFrameworkQueryableExtensions.CountAsync(dbSet, whereExpression, cancellationToken);
        }

        public virtual Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            //获取实体状态
            var entry = DbContext.Entry(entity);

            //如果实体没有被跟踪，必须指定需要更新的列
            if (entry.State == EntityState.Detached)
                throw new ArgumentException($"实体没有被跟踪，需要指定更新的列");

            #region removed code
#pragma warning disable S125 // Sections of code should not be commented out
            //实体没有被更改
            //if (entry.State == EntityState.Unchanged)
            //{
            //    var navigations = entry.Navigations.Where(x => x.CurrentValue is ValueObject);
            //    if (navigations?.Count() > 0)
            //    {
            //        foreach (var navigation in navigations)
            //        {
            //            DbContext.Add(navigation.CurrentValue);
            //        }
            //    }
            //    else
            //        return await Task.FromResult(0);
            //}
#pragma warning restore S125 // Sections of code should not be commented out
            #endregion

            //实体被标记为Added或者Deleted，抛出异常，ADNC应该不会出现这种状态。
            if (entry.State == EntityState.Added || entry.State == EntityState.Deleted)
                throw new ArgumentException($"{nameof(entity)},实体状态为{nameof(entry.State)}");

            return this.UpdateInternalAsync(entity, cancellationToken);
        }

        protected virtual async Task<int> UpdateInternalAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await DbContext.SaveChangesAsync(cancellationToken);

        public virtual async Task<IPagedModel<TEntity>> PagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool ascending = false, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            var dbSet = this.GetDbSet(writeDb, false);

            var total = await EntityFrameworkQueryableExtensions.CountAsync(dbSet, whereExpression, cancellationToken);
            if (total == 0)
                return new PagedModel<TEntity>() { PageSize = pageSize };

            if (pageIndex <= 0)
                pageIndex = 1;

            if (pageSize <= 0)
                pageSize = 10;

            var query = dbSet.Where(whereExpression);
            query = ascending ? query.OrderBy(orderByExpression) : query.OrderByDescending(orderByExpression);
            var data = await EntityFrameworkQueryableExtensions.ToArrayAsync(
                                    query.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                   , cancellationToken);

            return new PagedModel<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = total,
                Data = data
            };
        }

        public virtual async Task<IPagedModel<TEntity>> PagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IQueryable<TEntity>> expression, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            var dbSet = this.GetDbSet(writeDb, false);

            var total = await EntityFrameworkQueryableExtensions.CountAsync(dbSet, whereExpression, cancellationToken);
            if (total == 0)
                return new PagedModel<TEntity>() { PageSize = pageSize };

            if (pageIndex <= 0)
                pageIndex = 1;

            if (pageSize <= 0)
                pageSize = 10;

            var query = dbSet.Where(whereExpression);
            query = expression(query);
            var data = await EntityFrameworkQueryableExtensions.ToArrayAsync(
                                    query.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                   , cancellationToken);

            return new PagedModel<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = total,
                Data = data
            };
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, bool writeDb = false)
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> QueryListAsync<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, bool writeDb = false)
        {
            throw new NotImplementedException();
        }

        public Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, bool writeDb = false)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedModel<T>> PagedByMySqlAsync<T>(int pageIndex, int pageSize, string sqlcount, string sql, bool writeDb = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        //public virtual async Task<IPagedModel<T>> PagedByMySqlAsync<T>(int pageIndex, int pageSize, string sqlcount, string sql, bool writeDb = false, CancellationToken cancellationToken = default)
        //{
        //    if (pageIndex <= 0)
        //        pageIndex = 1;

        //    if (pageSize <= 0)
        //        pageSize = 10;
        //    sql = $"{sqlcount};{sql} limit {pageSize * (pageIndex - 1)},{pageSize}";
        //    if (writeDb)
        //        sql = string.Concat("/* ", EfCoreConsts.MAXSCALE_ROUTE_TO_MASTER, " */", sql);
        //    var reader = await DbContext.Database.GetDbConnection().QueryMultipleAsync(sql);
        //    var total = reader.ReadFirst<int>();
        //    if (total == 0)
        //        return new PagedModel<T>() { PageSize = pageSize };
        //    var data = reader.Read<T>().AsList();
        //    return new PagedModel<T>()
        //    {
        //        PageIndex = pageIndex,
        //        PageSize = pageSize,
        //        TotalCount = total,
        //        Data = data
        //    };
        //}
    }
}
