using Nolan.Domain.Shared;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Domain.Entities;

using Nolan.Infra.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Domain.Services
{

    public class TimeSheetDetailManager : IDomainService
    {
        private readonly IEfBasicRepository<TimeSheetDetail> _TimeSheetDetailManager;
        private readonly IEfBasicRepository<TimeSheet> _TimeSheet;
        public TimeSheetDetailManager(
          IEfBasicRepository<TimeSheetDetail> timeSheetDetailManager
           , IEfBasicRepository<TimeSheet> timeSheet
             )
        {
            _TimeSheetDetailManager = timeSheetDetailManager;
            _TimeSheet = timeSheet;
        }
        public async Task<int> CreateAsync(List<TimeSheet> input)
        {

            foreach (var item in input)
            {
                var count = item.ListTimeSheetDetails.Sum(p => p.TimesheetCount);
                item.Id = Guid.NewGuid();
                item.TotalCount = count;
                item.CreateTime = DateTime.Now;
                item.ApproveStatusEnum = Enum.ApproveStatusEnum.UnApprove;
                item.ApproveStatus = Enum.ApproveStatusEnum.UnApprove.ToString();
                foreach (var detail in item.ListTimeSheetDetails)
                {
                    detail.Id = Guid.NewGuid();
                    detail.ProjectID = item.ProjectID;
                    detail.Userid = new Guid("6ecd8c99-4036-403d-bf84-cf8400f67836");
                    detail.TimesheetID = item.Id;
                }
                await _TimeSheet.InsertAsync(item);
              //  await _TimeSheetDetailManager.InsertRangeAsync(item.ListTimeSheetDetails);
            }
            return 1;
            
            //   return await _TimeSheetDetailManager.InsertRangeAsync(list);
            //  return 

            // _TimeSheetDetailManager.InsertAsync();
            //var model = Mapper.Map<TimeSheetDetail>(input);

            //return  await _service.InsertAsync(model);

        }
        public List<TimeSheetDetail> GetListAsync(TimeSheetDetailSearchDto input)
        {
            return _TimeSheetDetailManager.Where(p => p.TimesheetCount != 0).ToList();
        }
    }

}
