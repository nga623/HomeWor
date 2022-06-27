using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.Domain.Entities;
using Nolan.HK.Domain.Enum;
using Nolan.Infra.EfCore.PostGresSql;
using Nolan.Infra.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public static string UserName;
        public static string UserType;


        public IActionResult Index()
        {

            if (UserName != null)
            {
                TimeSheetSearchDto timeSheetSearchDto = new TimeSheetSearchDto();
                timeSheetSearchDto.User = UserName;
                timeSheetSearchDto.UserType = Convert.ToInt32(UserType);
                var list = _TimeSheetService.GetListAsync(timeSheetSearchDto);
                foreach (var item in list)
                {
                    item.UserType = Convert.ToInt32(UserType);
                }
                var projectList = _Project.Where(p => p.ProjectName != "").ToList();
                var selectItemList = new List<SelectListItem>()
                {

                };
                var selectList = new SelectList(projectList, "Id", "ProjectName");
                selectItemList.AddRange(selectList);
                ViewBag.database = selectItemList;
                return View(list.ToList());
            }
            else
            {
                return Content("没有权限");
            }

        }
        [Authorize]
        public ActionResult Create(List<TimeSheetCreateDto> timeSheetCreateDto )
        {
           
           
            var s = _TimeSheetDetailService.CreateAsync(timeSheetCreateDto, UserName).Result;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var model = _TimeSheet.GetAsync(id).Result;
            var s = _TimeSheet.RemoveAsync(model).Result;
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
