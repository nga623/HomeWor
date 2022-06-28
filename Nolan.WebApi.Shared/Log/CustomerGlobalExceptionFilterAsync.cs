using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Nolan.WebApi.Shared.Filters;
using Nolan.WebApi.Shared.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.WebApi.Shared.Log
{
    public class CustomerGlobalExceptionFilterAsync : IAsyncExceptionFilter
    {

        private readonly INLogHelper _logHelper;

        public CustomerGlobalExceptionFilterAsync(INLogHelper logHelper)
        {
            _logHelper = logHelper;
        }

        /// <summary>
        /// 重新OnExceptionAsync方法
        /// </summary>
        /// <param name="context">异常信息</param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            // 如果异常没有被处理，则进行处理
            if (context.ExceptionHandled == false)
            {
                Robj<object> robj = new Robj<object>();
                robj.Error(context.Exception.Message.ToString());
                ObjectResult objectResult = new ObjectResult(robj);
                context.Result = objectResult;
                 
                context.ExceptionHandled = true;
                var _logger = AutofacUtil.GetService<ILogger<LogFilter>>();
                _logger.LogError("记录异常");
            }

            return Task.CompletedTask;
        }
    }
}
