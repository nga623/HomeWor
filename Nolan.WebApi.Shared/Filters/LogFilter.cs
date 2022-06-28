using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
//using Nolan.WebApi.Shared.Log;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nolan.WebApi.Shared.Filters
{

    public class LogFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _logger = AutofacUtil.GetService<ILogger<LogFilter>>();
            _logger.LogInformation("这里是action执行的方法");
        }
    }
}
