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

        //protected string GetClientIpAddress()
        //{
        //    var context = HttpContextUtility.GetCurrentHttpContext();
        //    var ip = context.Request.Headers["X-Real-IP"].FirstOrDefault();
        //    if (string.IsNullOrWhiteSpace(ip))
        //    {
        //        return context.Connection.RemoteIpAddress.MapToIPv4().ToString();
        //    }

        //    return ip;
        //}

        //private string GetCmsAppletScope()
        //{
        //    var context = HttpContextUtility.GetCurrentHttpContext();
        //    var cas = context.Request.Headers["CmsAppletScope"].FirstOrDefault();
        //    var message = $@"CmsAppletScope{cas}";
        //    WjtLog(message, null, "cappmlets");
        //    if (string.IsNullOrWhiteSpace(cas))
        //    {
        //        throw new ArgumentException("没有指定小程序");
        //    }

        //    if (cas == "null" || cas == "undefined")
        //    {
        //        throw new ArgumentException("没有指定小程序,参数不正确");
        //    }

        //    return cas;
        //}


        //protected void WjtLog(string message, IUserContext _userContext, string flag)
        //{
        //    var appcode = string.Empty;
        //    if (!string.IsNullOrWhiteSpace(flag))
        //    {
        //        if (flag == "cms" || flag == "CMS")
        //        {
        //            //CMS的appcode 自定义的,避免和现有的冲突,专为程序员看
        //            appcode = "CCustomerMAppCodeS";
        //        }
        //        else if (flag == "cappmlets")
        //        {
        //            appcode = "Cappmlets";
        //        }
        //        else if (flag == "b2b" || flag == "B2B")
        //        {
        //            appcode = "BCustomer2AppCodeB";
        //        }
        //        else if (flag == "qa" || flag == "QA")
        //        {
        //            appcode = "QCustomerAAppCode";
        //        }
        //        else if (flag == "usc" || flag == "USC")
        //        {
        //            appcode = "UCustomerSAppCodeC";
        //        }
        //        else
        //        {
        //            appcode = flag;
        //        }
        //    }
        //    else
        //    {
        //        appcode = "CustomerAppCode";
        //    }

        //    if (_userContext == null)
        //    {
        //        var operationLog5 = new AutonomyLog
        //        {
        //            AppName = flag,
        //            AppCode = appcode,
        //            CustomerId = flag,
        //            CreateTime = DateTime.Now,
        //            Message = message,
        //            UserId = flag,
        //            UserName = flag,
        //            Account = flag,
        //        };
        //        var operationLogWriter5 = ChannelHelper<AutonomyLog>.Instance.Writer;
        //        operationLogWriter5.WriteAsync(operationLog5).GetAwaiter().GetResult();
        //    }
        //    else
        //    {
        //        var operationLog5 = new AutonomyLog
        //        {
        //            AppName = _userContext.AppName,
        //            AppCode = _userContext.AppCode,
        //            CustomerId = _userContext.CustomerId,
        //            CreateTime = DateTime.Now,
        //            Message = message,
        //            UserId = _userContext.Id,
        //            UserName = _userContext.Name,
        //            Account = _userContext.Account,
        //        };
        //        var operationLogWriter5 = ChannelHelper<AutonomyLog>.Instance.Writer;
        //        operationLogWriter5.WriteAsync(operationLog5).GetAwaiter().GetResult();
        //    }

        //}
    }
}
