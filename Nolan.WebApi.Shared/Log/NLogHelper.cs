using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nolan.Domain.Shared.Entities.Log;
using Nolan.WebApi.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.WebApi.Shared.Log
{
    public class NLogHelper : INLogHelper
    {
        //public static Logger logger { get; private set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogger<NLogHelper> _logger;

        public NLogHelper(IHttpContextAccessor httpContextAccessor, ILogger<NLogHelper> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public void LogError(Exception ex)
        {
            LogMessage logMessage = new LogMessage();
            logMessage.IpAddress = _httpContextAccessor.HttpContext.Request.Host.Host;
            var user = _httpContextAccessor.HttpContext.User.GetUser();
            if (ex.InnerException != null)
                logMessage.LogInfo = ex.InnerException.Message;
            else
                logMessage.LogInfo = ex.Message;
            logMessage.StackTrace = ex.StackTrace;
            logMessage.OperationTime = DateTime.Now;
            logMessage.OperationName = user.Name;
            _logger.LogError(LogFormat.ErrorFormat(logMessage));
        }
    }
}
