using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nolan.Domain.Shared.ConfigModels;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.WebApi.Shared.Filters;
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
        [ApiResultFilter]
        public async Task<ActionResult<bool>> Create([FromBody] UserDto userDto)
        {
            return await _UserService.CreateAsync(userDto);
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
