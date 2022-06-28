using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.Domain.Entities;
using Nolan.Infra.EfCore.PostGresSql;
using Nolan.Infra.Repository.IRepositories;
using Nolan.WebApi.Shared.Extensions;
using Nolan.WebApi.Shared.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nolan.HK.MVC.Controllers
{
    public class TimeCardController : Controller
    {
        private readonly HomeWorkContext _context;
        private readonly ITimeSheetDetailService _TimeSheetDetailService;
        private readonly ITimeSheetService _TimeSheetService;
        private readonly IEfBasicRepository<TimeSheet> _TimeSheet;
        private readonly IEfBasicRepository<Project> _Project;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private readonly IEfBasicRepository<User> _User;
        public static string UserName;
        public static string UserType;
        public TimeCardController
            (
              HomeWorkContext context
            , ITimeSheetDetailService timeSheetDetailService
            , IEfBasicRepository<Project> project
            , IEfBasicRepository<TimeSheet> timeSheet
            , ITimeSheetService timeSheetService
            , IHttpContextAccessor httpContextAccessor
            , IEfBasicRepository<User> user
             )
        {
            _TimeSheetDetailService = timeSheetDetailService;
            _context = context;
            _Project = project;
            _TimeSheet = timeSheet;
            _TimeSheetService = timeSheetService;
            _IHttpContextAccessor = httpContextAccessor;
            _User = user;
        }

        public IActionResult Intail()
        {
            return View();
        }

        [TokenActionFilter]
        [HttpPost]
        public ActionResult GetToken()
        {
            var cureetUser = _IHttpContextAccessor.HttpContext.User.GetUser();
            UserName = cureetUser.Name;
            UserType = cureetUser.UserTypeEnum;
            return Json(null);
        }

        public IActionResult Index()
        {
            if (UserName != null)
            {
                TimeSheetSearchDto timeSheetSearchDto = new TimeSheetSearchDto();
                timeSheetSearchDto.User = UserName;
                timeSheetSearchDto.UserType = Convert.ToInt32(UserType);
                var listTimeSheet = _TimeSheetService.GetListAsync(timeSheetSearchDto).Result;
                var projectList = _Project.Where(p => p.ProjectName != "").ToList();
                var selectPrjectList = new List<SelectListItem>();
                var selectList = new SelectList(projectList, "Id", "ProjectName");
                selectPrjectList.AddRange(selectList);
                ViewBag.database = selectPrjectList;
                ViewBag.UserType = UserType;
                ViewBag.UserName = UserName;
                return View(listTimeSheet.ToList());
            }
            else
            {
                return Content("no promiss");
            }

        }

        [Authorize]
        public ActionResult Create(List<TimeSheetCreateDto> timeSheetCreateDto)
        {
            var s = _TimeSheetService.CreateAsync(timeSheetCreateDto, UserName).Result;
            return Json("ok");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var model = _TimeSheet.GetAsync(id).Result;
            var s = _TimeSheet.RemoveAsync(model).Result;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult<int>> AuditTimeCard(Guid id)
        {
            await _TimeSheetService.AuditTimeCard(id);
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            UserName = null;
            UserType = null;
            return Json(null);
        }
    }



}
