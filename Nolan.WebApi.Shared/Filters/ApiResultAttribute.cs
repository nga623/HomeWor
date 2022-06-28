using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nolan.WebApi.Shared.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
 
namespace Nolan.WebApi.Shared.Filters
{

    /// <summary>
    /// ApiResult
    /// </summary>
    public class ApiResultFilter : Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            ObjectResult result = actionExecutedContext.Result as ObjectResult;
            if (result != null)
            {   
                Robj<object> robj = new Robj<object>();
                robj.Success(result.Value);
                ObjectResult objectResult = new ObjectResult(robj);
                actionExecutedContext.Result = objectResult;
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }

}
