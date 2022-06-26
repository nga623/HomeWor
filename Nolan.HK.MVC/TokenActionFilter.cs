using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nolan.HK.MVC
{
    public class TokenActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var test = context.HttpContext.Request.Path;
            string bearer = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(bearer) || !bearer.Contains("Bearer")) return;
            string[] jwt = bearer.Split(' ');
            var tokenObj = new JwtSecurityToken(jwt[1]);

            var claimsIdentity = new ClaimsIdentity(tokenObj.Claims);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            context.HttpContext.User = claimsPrincipal;
        }
    }
    public static class TokenActionFilterExtension
    {
        public static CureetUser GetUser(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                var name = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "name");
                var UserTypeEnum = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "UserTypeEnum");
                if (name  == null || UserTypeEnum == null)
                {
                    return null;
                }

                return new CureetUser() { Name = name.Value, UserTypeEnum = UserTypeEnum.Value };
            }
            catch
            {
                return null;
            }
        }
    }
    public class CureetUser
    {
        public string Name { get; set; }
        public string UserTypeEnum { get; set; }
    }

}
