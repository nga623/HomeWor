using Nolan.Domain.Shared;
using Nolan.HK.Domain.Entities;
using Nolan.Infra.Repository.IRepositories;


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
         
         
    }

}
