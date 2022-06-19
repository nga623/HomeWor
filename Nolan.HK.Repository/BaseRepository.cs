 
using Microsoft.EntityFrameworkCore;
using Nolan.HK.Migrations;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nolan.HK.Repository
{
    public class BaseRepository<T, TKey> : IBaseRepository<T, TKey> where T : class
    {
        private HomeWorkContext _context;

        protected bool disposedValue;

        public BaseRepository(HomeWorkContext context)
        {
            _context = context;

        }
        public T Find(TKey id)
        {
            return _context.Set<T>().Find(id);
        }

        public T Find(Expression<Func<T, bool>> wherelamb)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault();
        }

        public virtual bool Update(T entity, bool isSaveChage = true)
        {
            _context.Entry(entity).State = EntityState.Modified;
            if (isSaveChage)
            {
                SaveChange();
            }
            return true;
        }

        public virtual bool Delete(T entity, bool isSaveChage = true)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            if (isSaveChage)
            {
                SaveChange();
            }
            return true;

        }

        public virtual int Delete(params int[] ids)
        {
            foreach (var item in ids)
            {
                var entity = _context.Set<T>().Find(item);
                _context.Set<T>().Remove(entity);
            }
            SaveChange();
            return ids.Count();
        }

        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return _context.Set<T>().Where(whereLambda).AsQueryable();
        }

        public IQueryable<T> LoadPageEntities<S>(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc)
        {
            total = _context.Set<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                return
                _context.Set<T>()
                  .Where(whereLambda)
                  .OrderBy(orderbyLambda)
                  .Skip(pageSize * (pageIndex - 1))
                  .Take(pageSize)
                  .AsQueryable();
            }
            else
            {
                return
               _context.Set<T>()
                 .Where(whereLambda)
                 .OrderByDescending(orderbyLambda)
                 .Skip(pageSize * (pageIndex - 1))
                 .Take(pageSize)
                 .AsQueryable();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Add(T entity, bool isSaveChage = true)
        {
            _context.Set<T>().Add(entity);
            if (isSaveChage)
            {
                SaveChange();
            }
        }

        public int SaveChange()
        {
            return _context.SaveChanges();
        }
    }

}
