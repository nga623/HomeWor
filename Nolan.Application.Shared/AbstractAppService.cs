using Nolan.Infra.Mapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.Application.Shared
{
    public abstract class AbstractAppService : IAppService
    {
        //public string OrderAppletId => GetCmsAppletScope();
        public string logMessage { get; set; }
        public IObjectMapper Mapper { get; set; }

        protected AppSrvResult AppSrvResult()
            => new AppSrvResult();

        protected AppSrvResult<TValue> AppSrvResult<TValue>([NotNull] TValue value)
            => new AppSrvResult<TValue>(value);

        protected ProblemDetails Problem(HttpStatusCode? statusCode = null, string detail = null, string title = null, string instance = null, string type = null)
            => new ProblemDetails(statusCode, detail, title, instance, type);

        protected Expression<Func<TEntity, object>>[] UpdatingProps<TEntity>(params Expression<Func<TEntity, object>>[] expressions)
            => expressions;

         
    }
}
