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
        private readonly IEfBasicRepository<TimeSheet> _timeSheetEntity;
        private readonly IEfBasicRepository<TimeSheetDetail> _timeSheetDetailEntity;
        private readonly IEfBasicRepository<User> _userEntity;
        public TimeSheetService
            (
              IEfBasicRepository<TimeSheet> timeSheetEntity
            , IEfBasicRepository<TimeSheetDetail> timeSheetDetailEntity
            , IEfBasicRepository<User> userEntity
            )
        {
            _timeSheetEntity = timeSheetEntity;
            _timeSheetDetailEntity = timeSheetDetailEntity;
            _userEntity = userEntity;
        }

        public async Task<List<TimeSheetDto>> GetListAsync(TimeSheetSearchDto input)
        {
            var cureetUser = _userEntity
                .Where(p => p.Id != Guid.Empty && p.Name == input.User)
                .FirstOrDefault();
            var listTimeSheet = await _timeSheetEntity.Where(p => p.Id != Guid.Empty)
                .Include(p => p.User)
                .Include(p => p.Project)
                .Include(p => p.ListTimeSheetDetails.OrderBy(p => p.Date))
                .ThenInclude(p => p.User)
                .OrderBy(p => p.CreateTime)
                .ToListAsync();
            if (input.UserType ==(int)UserTypeEnum.staff && cureetUser != null)
            {
                listTimeSheet = listTimeSheet.Where(p => p.ApproveStatusEnum == ApproveStatusEnum.UnApprove && p.Userid == cureetUser.Id).ToList();
            }
            var listDto = Mapper.Map<List<TimeSheetDto>>(listTimeSheet);
            return listDto;
        }

        public async Task<int> CreateAsync(List<TimeSheetCreateDto> input, string userName)
        {
            var listTimeSheet = Mapper.Map<List<TimeSheet>>(input);
            var user = _userEntity.Where(p => p.Name == userName).FirstOrDefault();
            var listSheetRemove = _timeSheetEntity.Where(p => p.ApproveStatusEnum == ApproveStatusEnum.UnApprove && p.Userid == user.Id).ToList();
            await _timeSheetEntity.RemoveRangeAsync(listSheetRemove);
            if (user == null)
            {
                throw new Exception("user is not null");
            }
            foreach (var item in listTimeSheet)
            {
                var count = item.ListTimeSheetDetails.Sum(p => p.TimesheetCount);
                item.Id = Guid.NewGuid();
                item.TotalCount = count;
                item.Userid =   user.Id;
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
            }
            return await _timeSheetEntity.InsertRangeAsync(listTimeSheet);
        }

        public async Task<int> AuditTimeCard(Guid id)
        {
            var model = await _timeSheetEntity.GetAsync(id);
            model.ApproveStatusEnum = ApproveStatusEnum.Approve;
            model.ApproveStatus = ApproveStatusEnum.Approve.ToString();
            return await _timeSheetEntity.UpdateAsync(model);
        }
    }
}
