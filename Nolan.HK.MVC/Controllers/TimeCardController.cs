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
        public TimeCardController
            (
              HomeWorkContext context
            , ITimeSheetDetailService timeSheetDetailService
            , IEfBasicRepository<Project> project
            , IEfBasicRepository<TimeSheet> timeSheet
            , ITimeSheetService timeSheetService
             )
        {
            _TimeSheetDetailService = timeSheetDetailService;
            _context = context;
            _Project = project;
            _TimeSheet = timeSheet;
            _TimeSheetService = timeSheetService;
        }
        public IActionResult Index()
        {
            var list = _TimeSheetService.GetListAsync(null);
            var projectList = _Project.Where(p => p.ProjectName != "").ToList();
            var selectItemList = new List<SelectListItem>()
            {

            };
            var selectList = new SelectList(projectList, "Id", "ProjectName");
            selectItemList.AddRange(selectList);
            ViewBag.database = selectItemList;
            return View(list.ToList());
        }

        public ActionResult Create(List<TimeSheetCreateDto> timeSheetCreateDto)
        {
            var list = _TimeSheet.Where(p => p.Id != Guid.Empty).ToList();
            list = list.Where(p => p.ApproveStatusEnum == ApproveStatusEnum.UnApprove).ToList();
            var del = _TimeSheet.RemoveRangeAsync(list).Result;
            var s = _TimeSheetDetailService.CreateAsync(timeSheetCreateDto).Result;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var model = _TimeSheet.GetAsync(id).Result;
            var s = _TimeSheet.RemoveAsync(model).Result;
            return RedirectToAction("Index");
        }
    }

}
