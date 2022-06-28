using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Nolan.WebApi.Shared.Filters;
using Nolan.WebApi.Shared.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nolan.HK.MVC
{
    public class CustomerGlobalExceptionFilterAsync : IAsyncExceptionFilter
    {
        /// <summary>
        /// 重新OnExceptionAsync方法
        /// </summary>
        /// <param name="context">异常信息</param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                Robj<object> robj = new Robj<object>();
                robj.Error(context.Exception.ToString());
                ObjectResult objectResult = new ObjectResult(robj);
                context.Result = objectResult;

                // 设置为true，表示异常已经被处理了，其它捕获异常的地方就不会再处理了
                context.ExceptionHandled = true;
                var _logger = AutofacUtil.GetService<ILogger<LogFilter>>();
                _logger.LogError("记录异常");
            }

            return Task.CompletedTask;
        }
    }
}
