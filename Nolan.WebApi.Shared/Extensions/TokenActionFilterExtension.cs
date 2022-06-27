using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.WebApi.Shared.Extensions
{
    public static class TokenActionFilterExtension
    {
        public static CureetUser GetUser(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                var name = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "name");
                var UserTypeEnum = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "UserTypeEnum");
                if (name == null || UserTypeEnum == null)
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
