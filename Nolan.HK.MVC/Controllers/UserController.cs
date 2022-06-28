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
        public UserController
            (
              IUserService userService
             , ILogger<UserController> logger
            )
        {
            _UserService = userService;
            _logger = logger;
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
            if ( !ModelState.IsValid)
            {
                return View(false);
            }
            return await _UserService.CreateAsync(userDto);
        }

        [HttpPost]
        [LogFilter]
        [ApiResultFilter]
        public async Task<ActionResult<string>> Login([FromBody] UserDto userDto)
        {
            _logger.LogError("this is error message");
            _logger.LogInformation("this is info message");
            if (!ModelState.IsValid)
            {
                return View(null);
            }
            return await _UserService.LoginAsync(userDto);
        }

    }
}
