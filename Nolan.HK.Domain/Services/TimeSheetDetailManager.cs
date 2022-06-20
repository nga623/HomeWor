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
         
        public TimeSheetDetailManager(
          IEfBasicRepository<TimeSheetDetail> timeSheetDetailManager
             )
        {
             _TimeSheetDetailManager = timeSheetDetailManager;
        }
        public async Task<int> CreateAsync(TimeSheetDetailCreateDto input)
        {
           // _TimeSheetDetailManager.InsertAsync();
             //var model = Mapper.Map<TimeSheetDetail>(input);
            return 1;
            //return  await _service.InsertAsync(model);

        }
        public   List<TimeSheetDetail> GetListAsync(TimeSheetDetailSearchDto input)
        {
         return   _TimeSheetDetailManager.Where(p => p.Note != "").ToList();
        }
    }

}
