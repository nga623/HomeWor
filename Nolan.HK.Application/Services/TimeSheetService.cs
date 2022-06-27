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

        public List<TimeSheetDto> GetListAsync(TimeSheetSearchDto input)
        {
            var cureetUser = _User.Where(p => p.Id != Guid.Empty && p.Name == input.User).FirstOrDefault();
            var list = _TimeSheet.Where(p => p.Id != Guid.Empty).Include(p => p.ListTimeSheetDetails).ToList();
            if (input.UserType == 0)
            {
                list = _TimeSheet.Where(p => p.ApproveStatusEnum == ApproveStatusEnum.UnApprove && p.Userid == cureetUser.Id).ToList();
            }
            var listDto = Mapper.Map<List<TimeSheetDto>>(list);
            listDto.ForEach(p => p.UserType = Convert.ToInt32(input.UserType));
            return listDto;
        }
    }
}
