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
using Nolan.HK.Domain.Enum;

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
        public Task<int> CreateAsync(List<TimeSheetDto> input)
        {
            throw new NotImplementedException();
        }

        public List<TimeSheetDto> GetListAsync(TimeSheetSearchDto input)
        {
            var userName = input.User;
            var userTpye = input.UserType;
            var listUser = _User.Where(p => p.Id != Guid.Empty).ToList();
            var cureetUser = listUser.Where(p => p.Name == userName).FirstOrDefault();
            var list = new List<TimeSheet>();
            if (userTpye == 1)
            {
                list = _TimeSheet.Where(p => p.Id != Guid.Empty).ToList();
            }
            else
            {
                list = _TimeSheet.Where(p => p.ApproveStatusEnum == ApproveStatusEnum.UnApprove).ToList();
                list = list.Where(p => p.Userid==cureetUser.Id).ToList();
            }

            foreach (var item in list)
            {

                var listDetail = _TimeSheetDetail.Where(p => p.Id != Guid.Empty).ToList();
               
                listDetail = listDetail.Where(p => p.TimesheetID == item.Id  ).OrderBy(p => p.Date).ToList();
                item.ListTimeSheetDetails = listDetail;
            }
            return Mapper.Map<List<TimeSheetDto>>(list);
        }
    }
}
