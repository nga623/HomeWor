using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
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
       

        
    }
}
