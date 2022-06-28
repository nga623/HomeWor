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
        
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                Robj<object> robj = new Robj<object>();
                robj.Error(context.Exception.Message.ToString());
                ObjectResult objectResult = new ObjectResult(robj);
                context.Result = objectResult;

                context.ExceptionHandled = true;
                var _logger = AutofacUtil.GetService<ILogger<LogFilter>>();
                _logger.LogError("record exception");
            }
            return Task.CompletedTask;
        }
    }
}
