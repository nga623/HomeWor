using Microsoft.AspNetCore.Mvc;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.Domain.Entities;
 
using Nolan.Infra.EfCore.PostGresSql;
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
        public TimeCardController(
        HomeWorkContext context,
            ITimeSheetDetailService timeSheetDetailService
            )
        {
            _TimeSheetDetailService = timeSheetDetailService;
            _context = context;
        }
        public  IActionResult  Index()
        {
            var list = _TimeSheetDetailService.GetListAsync(null);

            //var timeSheetDetail = from m in _conte
            //
            //         select m;
            return View(list.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Create(List<TimeSheetDetailCreateDto> TimeSheetDetailDto)
        {
            await _TimeSheetDetailService.CreateAsync(TimeSheetDetailDto[0]);
            return View();
        }
    }

}
