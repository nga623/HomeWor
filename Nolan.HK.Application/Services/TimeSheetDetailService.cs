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

namespace Nolan.HK.Application.Services
{
    public class TimeSheetDetailService : AbstractAppService, ITimeSheetDetailService
    {
         private readonly IEfBasicRepository<TimeSheetDetail> _service;
        public TimeSheetDetailService(
            IEfBasicRepository<TimeSheetDetail> service
            )
        {
            _service = service;
        }
        public async Task<int> CreateAsync(TimeSheetDetailCreateDto input)
        {
             
             //var model = Mapper.Map<TimeSheetDetail>(input);
            return 1;
            //return  await _service.InsertAsync(model);

        }

        public Task<List<TimeSheetDetailDto>> GetListAsync(TimeSheetDetailSearchDto input)
        {
            throw new NotImplementedException();
        }
    }
}
