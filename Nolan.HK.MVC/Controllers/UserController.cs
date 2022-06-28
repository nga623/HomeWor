using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nolan.Domain.Shared.ConfigModels;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.WebApi.Shared.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _UserService;
        private readonly ILogger<UserController> _logger;
        IOptions<JwtSetting> _settings;
        public UserController
            (
              IUserService userService
            , ILogger<UserController> logger
            , IOptions<JwtSetting> settings
            )
        {
            _UserService = userService;
            _logger = logger;
            _settings = settings;
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
        [ValidateAntiForgeryToken]
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
    new Claim("name", user.Name.ToString(), ClaimValueTypes.Integer32),
    new Claim("UserTypeEnum", user.UserTypeEnum.ToString(),ClaimValueTypes.Integer32)
            };
            var _jwtSetting = new JwtSetting();
            _jwtSetting.SecurityKey = _settings.Value.SecurityKey;
            _jwtSetting.Issuer = _settings.Value.Issuer;
            _jwtSetting.Audience = _settings.Value.Audience;
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
        [LogFilter]
        [ApiResultFilter]
        public async Task<ActionResult<string>> Login([FromBody] UserDto userDto)
        {

            _logger.LogError("这是错误信息");
            _logger.LogInformation("这是提示信息");
             return await _UserService.LoginAsync(userDto);
            
           
        }

    }
}
