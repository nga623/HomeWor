using Nolan.Domain.Shared;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Domain.Entities;
using Nolan.HK.Domain.Enum;
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
        private readonly IEfBasicRepository<User> _User;
        public TimeSheetDetailManager(
          IEfBasicRepository<TimeSheetDetail> timeSheetDetailManager
           , IEfBasicRepository<TimeSheet> timeSheet
            , IEfBasicRepository<User> user
             )
        {
            _TimeSheetDetailManager = timeSheetDetailManager;
            _TimeSheet = timeSheet;
            _User = user;
        }
        public async Task<int> CreateAsync(List<TimeSheet> input, string userName)
        {
            var listUser = _User.Where(p => p.Name != null).ToList();
            var user = listUser.Where(p => p.Name == userName).FirstOrDefault();

            var listSheet = _TimeSheet.Where(p => p.Id != Guid.Empty).ToList();
            listSheet = listSheet.Where(p => p.ApproveStatusEnum == ApproveStatusEnum.UnApprove&&p.Userid==user.Id).ToList();
            var del = _TimeSheet.RemoveRangeAsync(listSheet).Result;

            foreach (var item in input)
            {
                var count = item.ListTimeSheetDetails.Sum(p => p.TimesheetCount);
                item.Id = Guid.NewGuid();
                item.TotalCount = count;
                item.Userid = user.Id;
                item.CreateTime = DateTime.Now;
                item.ApproveStatusEnum = Enum.ApproveStatusEnum.UnApprove;
                item.ApproveStatus = Enum.ApproveStatusEnum.UnApprove.ToString();
                foreach (var detail in item.ListTimeSheetDetails)
                {
                    detail.Id = Guid.NewGuid();
                    detail.ProjectID = item.ProjectID;
                    detail.Userid = user.Id;
                    detail.TimesheetID = item.Id;
                }
                await _TimeSheet.InsertAsync(item);
            }
            return 1;
        }
        public List<TimeSheetDetail> GetListAsync(TimeSheetDetailSearchDto input)
        {
            return _TimeSheetDetailManager.Where(p => p.TimesheetCount != 0).ToList();
        }
    }

}
