using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
 
 
namespace Nolan.HK.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _UserService;
        public UserController
            (
              IUserService userService
            )
        {
            _UserService = userService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registered()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserDto userDto)
        {
            try
            {
                var r = _UserService.CreateAsync(userDto).Result;
                if (r > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    return Content(@"created!");
                }
            }
            catch
            {
                return View();
            }
        }

        public string GetToken(UserDto user)
        {
            //创建用户身份标识，可按需要添加更多信息
            var claims = new Claim[]
            {
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    new Claim("name", user.Name.ToString(), ClaimValueTypes.Integer32), // 用户id
   // new Claim("name", user.Name), // 用户名
    new Claim("UserTypeEnum", user.UserTypeEnum.ToString(),ClaimValueTypes.Integer32) // 是否是管理员
            };
            var _jwtSetting = new JwtSetting();
            _jwtSetting.SecurityKey = "d0ecd23c-dfdb-4005-a2ea-0fea210c858a";
            _jwtSetting.Issuer = "jwtIssuertest";
            _jwtSetting.Audience = "jwtAudiencetest";
            var algorithm = SecurityAlgorithms.HmacSha256;
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey));
            var signingCredentials = new SigningCredentials(secretKey, algorithm);
            //创建令牌
            var token = new JwtSecurityToken(
              issuer: _jwtSetting.Issuer,
              audience: _jwtSetting.Audience,
              signingCredentials: signingCredentials,
              claims: claims,
              notBefore: DateTime.Now,
              expires: DateTime.Now.AddSeconds(10000000)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
        [HttpPost]
        public ActionResult Login( [FromBody] UserDto userDto)
        {
            string token = "";
            try
            {
                var user = _UserService.LoginAsync(userDto).Result;
                if (user!=null)
                {
                    token = GetToken(user);
                }
                else
                {
                    return Content("nopass");
                }
            }
            catch
            {
            }
            return Content(token);
        }
        
    }
}
