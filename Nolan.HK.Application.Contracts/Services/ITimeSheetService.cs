using Nolan.HK.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Application.Contracts.Services
{
     
    public interface ITimeSheetService
    {
        
        List<TimeSheetDto> GetListAsync(TimeSheetSearchDto input);
        Task<int> CreateAsync(List<TimeSheetCreateDto> input, string userName);
    }
}
