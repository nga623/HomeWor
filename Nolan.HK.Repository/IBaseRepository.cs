using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Repository
{
    public interface IBaseRepository<T, TKey> : IDisposable where T : class
    {
        T Find(TKey id);
        T Find(Expression<Func<T, bool>> wherelamb);
        void Add(T entity, bool isSaveChage = true);
        bool Update(T entity, bool isSaveChage = true);
        bool Delete(T entity, bool isSaveChage = true);
        int Delete(params int[] ids);
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadPageEntities<S>(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc);
        int SaveChange();

    }

}
