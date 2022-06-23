using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.Domain.Entities;
 
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
        private readonly IEfBasicRepository<Project> _Project;
        public TimeCardController(
        HomeWorkContext context,
        
            ITimeSheetDetailService timeSheetDetailService
            , IEfBasicRepository<Project> project
            )
        {
            _TimeSheetDetailService = timeSheetDetailService;
            _context = context;
            _Project = project;
        }
        public  IActionResult  Index()
        {
            var list = _TimeSheetDetailService.GetListAsync(null);
          var projectList=  _Project.Where(p => p.ProjectName != "").ToList();
            var selectItemList = new List<SelectListItem>() {
                new SelectListItem(){Value="0",Text="全部" }
            };
            var selectList = new SelectList(projectList, "Id", "ProjectName");
            selectItemList.AddRange(selectList);
            ViewBag.database = selectItemList;
            return View(list.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Create(List<TimeSheetCreateDto> timeSheetCreateDto)
        {
            await _TimeSheetDetailService.CreateAsync(timeSheetCreateDto);
            return View();
        }
    }

}
