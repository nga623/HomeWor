using AutoMapper;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.Domain.Entities;
using Nolan.Infra.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nolan.Infra.Mapper;
using Nolan.Application.Shared;
using Nolan.HK.Domain.Services;

namespace Nolan.HK.Application.Services
{
    public class TimeSheetDetailService : AbstractAppService, ITimeSheetDetailService
    {
        private readonly TimeSheetDetailManager _TimeSheetDetailManager;
        public TimeSheetDetailService(
            TimeSheetDetailManager timeSheetDetailManager
            )
        {
            _TimeSheetDetailManager = timeSheetDetailManager;
        }
        public async Task<int> CreateAsync(List<TimeSheetCreateDto> input)
        {
             var list = Mapper.Map<List<TimeSheet>>(input);
            //foreach (var item in list)
            //{
            //    item.Id = Guid.NewGuid();
            //    item.pro
            //    foreach (var detail in item.ListTimeSheetDetails)
            //    {
            //        detail.Id = Guid.NewGuid();
            //        detail.TimesheetID = item.Id;
                     
            //    }
            //}
            //list = list.Where(p => p.TimesheetCount != 0).ToList();
            return await _TimeSheetDetailManager.CreateAsync(list);
        }

        public List<TimeSheetDetailDto> GetListAsync(TimeSheetDetailSearchDto input)
        {
            var list = _TimeSheetDetailManager.GetListAsync(input);
            return Mapper.Map<List<TimeSheetDetailDto>>(list);
        }
    }
}
