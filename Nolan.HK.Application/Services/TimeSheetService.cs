using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.Domain.Entities;
using Nolan.Infra.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nolan.Application.Shared;
using Nolan.HK.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Nolan.HK.Application.Services
{
    public class TimeSheetService : AbstractAppService, ITimeSheetService
    {
        private readonly IEfBasicRepository<TimeSheet> _TimeSheet;
        private readonly IEfBasicRepository<TimeSheetDetail> _TimeSheetDetail;
        private readonly IEfBasicRepository<User> _User;
        public TimeSheetService(
              IEfBasicRepository<TimeSheet> timeSheet
            , IEfBasicRepository<TimeSheetDetail> timeSheetDetail
            , IEfBasicRepository<User> user
            )
        {
            _TimeSheet = timeSheet;
            _TimeSheetDetail = timeSheetDetail;
            _User = user;
        }

        public async Task<List<TimeSheetDto>> GetListAsync(TimeSheetSearchDto input)
        {
            var cureetUser = _User
                .Where(p => p.Id != Guid.Empty && p.Name == input.User)
                .FirstOrDefault();
            var list = await _TimeSheet.Where(p => p.Id != Guid.Empty)
                .Include(p => p.User)
                .Include(p => p.Project)
                .Include(p => p.ListTimeSheetDetails.OrderBy(p => p.Date))
                .ThenInclude(p => p.User)
                .OrderBy(p => p.CreateTime)
                .ToListAsync();
            if (input.UserType == 0)
            {
                list = list.Where(p => p.ApproveStatusEnum == ApproveStatusEnum.UnApprove && p.Userid == cureetUser.Id).ToList();
            }
            var listDto = Mapper.Map<List<TimeSheetDto>>(list);
            listDto.ForEach(p => p.UserType = Convert.ToInt32(input.UserType));
            return listDto;
        }

        public async Task<int> CreateAsync(List<TimeSheetCreateDto> input, string userName)
        {
            var list = Mapper.Map<List<TimeSheet>>(input);
            var user = _User.Where(p => p.Name == userName).FirstOrDefault();
            var listSheet = _TimeSheet.Where(p => p.ApproveStatusEnum == ApproveStatusEnum.UnApprove && p.Userid == user.Id).ToList();
            await _TimeSheet.RemoveRangeAsync(listSheet);

            foreach (var item in list)
            {
                var count = item.ListTimeSheetDetails.Sum(p => p.TimesheetCount);
                item.Id = Guid.NewGuid();
                item.TotalCount = count;
                item.Userid = user.Id;
                item.CreateTime = DateTime.Now;
                item.ApproveStatusEnum = ApproveStatusEnum.UnApprove;
                item.ApproveStatus = ApproveStatusEnum.UnApprove.ToString();
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

        public async Task<int> AuditTimeCard(Guid id)
        {
            var model = await _TimeSheet.GetAsync(id);
            model.ApproveStatusEnum = ApproveStatusEnum.Approve;
            model.ApproveStatus = ApproveStatusEnum.Approve.ToString();
            return await _TimeSheet.UpdateAsync(model);
        }
    }
}
